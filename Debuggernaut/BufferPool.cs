using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Debuggernaut
{
    class BufferPool
    {
        public BufferPool(int bufferSize)
        {
            _bufferSize = bufferSize;

            _availableBuffers = new Stack<byte[]>(MaxBufferCount);
            for (int i = 0; i < MaxBufferCount; ++i)
            {
                _availableBuffers.Push(new byte[_bufferSize]);
            }
        }

        public byte[] GetBuffer()
        {
            _bufferSemaphore.WaitOne();

            lock (_availableBuffersLock)
            {
                return _availableBuffers.Pop();
            }
        }

        public void ReturnBuffer(byte[] buffer)
        {
            Debug.Assert(buffer.Length == _bufferSize);

            lock (_availableBuffersLock)
            {
                _availableBuffers.Push(buffer);
            }

            _bufferSemaphore.Release();
        }

        int _bufferSize;

        Stack<byte[]> _availableBuffers;
        object _availableBuffersLock = new object();

        Semaphore _bufferSemaphore = new Semaphore(MaxBufferCount, MaxBufferCount);

        const int MaxBufferCount = 10;
    }
}
