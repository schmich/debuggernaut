using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace Debuggernaut
{
    class StatusUpdater
    {
        public StatusUpdater(ToolStripItem statusControl, int statusDuration)
        {
            _statusItem = statusControl;
            _statusDuration = statusDuration;

            Thread statusUpdate = new Thread(new ThreadStart(UpdateStatus));
            statusUpdate.IsBackground = true;
            statusUpdate.Start();
        }

        public void AddStatus(string status)
        {
            _nextStatus = status;
            _preempted.Set();
            _hasStatus.Set();
        }

        void UpdateStatus()
        {
            while (true)
            {
                _hasStatus.WaitOne();
                _preempted.Reset();

                _statusItem.Text = _nextStatus;

                _preempted.WaitOne(_statusDuration);
                _statusItem.Text = string.Empty;
            }
        }

        string _nextStatus;
        int _statusDuration;
        ToolStripItem _statusItem;

        AutoResetEvent _hasStatus = new AutoResetEvent(false);
        AutoResetEvent _preempted = new AutoResetEvent(false);
    }
}
