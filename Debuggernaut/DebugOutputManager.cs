using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debuggernaut
{
    class DebugOutput
    {
        public KeyValuePair<string, string> ProcessInfo { get; set; }
        public string Output { get; set; }
    }

    class DebugOutputManager
    {
        public event Action OutputReceived;

        public void AddDebugOutput(int processId, string output)
        {
            KeyValuePair<string, string> info;
            if (!_processInfo.TryGetValue(processId, out info))
            {
                info = new KeyValuePair<string, string>(processId.ToString(), "");
                _processInfo[processId] = info;
            }

            _output.Add(new DebugOutput { ProcessInfo = info, Output = output });

            OutputReceived();
        }

        public string GetText(int row, int col)
        {
            var output = _output[row];
            if (col == 0)
            {
                return output.ProcessInfo.Key;
            }
            else if (col == 2)
            {
                return output.Output;
            }

            return "";
        }

        public int OutputCount
        {
            get { return _output.Count; }
        }

        Dictionary<int, KeyValuePair<string, string>> _processInfo = new Dictionary<int, KeyValuePair<string, string>>();
        List<DebugOutput> _output = new List<DebugOutput>();
    }
}
