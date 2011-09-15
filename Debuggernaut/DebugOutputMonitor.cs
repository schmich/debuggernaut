using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;

namespace Debuggernaut
{
    interface DebugOutputMonitor
    {
        void Start();
        void Stop();

        bool IsListening { get; }

        event OutputReceivedHandler OutputReceived;
    }

    delegate void OutputReceivedHandler(int processId, string output);

    class Win32DebugOutputMonitor : DebugOutputMonitor
    {
        public void Start()
        {
            if (IsListening)
                return;

            Thread monitorThread = new Thread(new ThreadStart(MonitorOutput));
            monitorThread.IsBackground = true;
            monitorThread.Start();

            Thread notificationThread = new Thread(new ThreadStart(NotifyOutputReceived));
            notificationThread.IsBackground = true;
            notificationThread.Start();

            IsListening = true;
        }

        public void Stop()
        {
            if (!IsListening)
                return;

            _stopMonitoring.Release(2);
            IsListening = false;
        }

        public bool IsListening
        {
            get;
            private set;
        }

        public event OutputReceivedHandler OutputReceived;

        void MonitorOutput()
        {
            using (var bufferMapping = CreateBufferMapping())
            using (var bufferMemory = CreateBufferMemory(bufferMapping))
            using (var bufferReadyEvent = CreateBufferReadyEvent())
            using (var dataReadyEvent = CreateDataReadyEvent())
            {
                IntPtr buffer = bufferMemory.DangerousGetHandle();

                var handles = new IntPtr[]
                {
                    _stopMonitoring.SafeWaitHandle.DangerousGetHandle(),
                    dataReadyEvent.DangerousGetHandle()
                };

                while (true)
                {
                    Platform.SetEvent(bufferReadyEvent);
                    
                    uint signal = Platform.WaitForMultipleObjects(2, handles, false, Platform.INFINITE);
                    if (signal == Platform.WAIT_OBJECT_0)
                        break;

                    byte[] message = _bufferPool.GetBuffer();
                    Marshal.Copy(buffer, message, 0, BufferSize);

                    lock (_messages)
                    {
                        _messages.Enqueue(message);
                    }

                    _messagesAvailable.Release();
                }
            }
        }

        void NotifyOutputReceived()
        {
            var handles = new WaitHandle[]
            {
                _stopMonitoring,
                _messagesAvailable
            };

            while (true)
            {
                if (WaitHandle.WaitAny(handles) == 0)
                    break;

                byte[] message;
                lock (_messages)
                {
                    Debug.Assert(_messages.Count > 0);
                    message = _messages.Dequeue();
                }

                Debug.Assert(message.Length == BufferSize);
                if (message.Length != BufferSize)
                    continue;

                int processId = BitConverter.ToInt32(message, 0);
                string output = Encoding.ASCII.GetString(message, 4, GetStringLength(message, 4));

                _bufferPool.ReturnBuffer(message);

                OutputReceived(processId, output);
            }
        }

        int GetStringLength(byte[] buffer, int start)
        {
            for (int i = start; i < buffer.Length; ++i)
            {
                if (buffer[i] == 0)
                    return i - start;
            }

            return buffer.Length - start;
        }

        SafeWaitHandle CreateDataReadyEvent()
        {
            SafeWaitHandle dataReadyEvent = Platform.CreateEvent(
                IntPtr.Zero,
                false,
                false,
                DataReadyEventName
            );

            if (dataReadyEvent.IsInvalid)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }

            return dataReadyEvent;
        }

        SafeWaitHandle CreateBufferReadyEvent()
        {
            SafeWaitHandle bufferReadyEvent = Platform.CreateEvent(
                IntPtr.Zero,
                false,
                false,
                BufferReadyEventName
            );

            if (bufferReadyEvent.IsInvalid)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }

            return bufferReadyEvent;
        }

        SafeFileHandle CreateBufferMapping()
        {
            SafeFileHandle bufferMapping = Platform.CreateFileMapping(
                (IntPtr)Platform.INVALID_HANDLE_VALUE,
                IntPtr.Zero,
                Platform.FileMapProtection.PageReadWrite,
                0,
                BufferSize,
                BufferName
            );

            if (bufferMapping.IsInvalid)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }

            return bufferMapping;
        }

        SafeFileMapViewHandle CreateBufferMemory(SafeFileHandle fileMapping)
        {
            SafeFileMapViewHandle buffer = Platform.MapViewOfFile(
                fileMapping,
                Platform.FileMapAccess.FileMapAllAccess,
                0,
                0,
                BufferSize
            );

            if (buffer.IsInvalid)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }

            return buffer;
        }

        BufferPool _bufferPool = new BufferPool(BufferSize);
        Queue<byte[]> _messages = new Queue<byte[]>();
        Semaphore _messagesAvailable = new Semaphore(0, int.MaxValue);

        Semaphore _stopMonitoring = new Semaphore(0, 2);

        const int BufferSize = 4096;
        const string BufferName = "DBWIN_BUFFER";
        const string BufferReadyEventName = "DBWIN_BUFFER_READY";
        const string DataReadyEventName = "DBWIN_DATA_READY";
    }
}
