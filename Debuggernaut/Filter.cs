using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Debuggernaut
{
    interface Constraint
    {
        bool Matches(int processId, string output);
    }

    class ProcessIdConstraint : Constraint
    {
        public ProcessIdConstraint(Predicate<int> predicate)
        {
            _predicate = predicate;
        }

        public bool Matches(int processId, string output)
        {
            return _predicate(processId);
        }

        Predicate<int> _predicate;
    }

    class ProcessNameConstraint : Constraint
    {
        public ProcessNameConstraint(Predicate<string> predicate)
        {
            _predicate = predicate;
        }

        public bool Matches(int processId, string output)
        {
            string processName;

            if (!_processNames.TryGetValue(processId, out processName))
            {
                processName = GetProcessName(processId);

                if (string.IsNullOrEmpty(processName))
                    processName = "(unknown)";

                _processNames[processId] = processName;
            }

            return _predicate(processName);
        }

        string GetProcessName(int processId)
        {
            try
            {
                Process process = Process.GetProcessById(processId);
                return process.ProcessName;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        Predicate<string> _predicate;

        Dictionary<int, string> _processNames = new Dictionary<int, string>();
    }

    class OutputConstraint : Constraint
    {
        public OutputConstraint(Predicate<string> predicate)
        {
            _predicate = predicate;
        }

        public bool Matches(int processId, string output)
        {
            return _predicate(output);
        }

        Predicate<string> _predicate;
    }

    class RegexOutputConstraint : Constraint
    {
        public RegexOutputConstraint(Regex regex)
        {
            _regex = regex;
        }

        public bool Matches(int processId, string output)
        {
            return _regex.IsMatch(output);
        }

        Regex _regex;
    }

    class NotConstraint : Constraint
    {
        public NotConstraint(Constraint constraint)
        {
            _constraint = constraint;
        }

        public bool Matches(int processId, string output)
        {
            return !_constraint.Matches(processId, output);
        }

        Constraint _constraint;
    }

    class BinaryConstraint : Constraint
    {
        public BinaryConstraint(Constraint left, Constraint right, Func<bool, bool, bool> combiner)
        {
            _left = left;
            _right = right;
            _combiner = combiner;
        }

        public bool Matches(int processId, string output)
        {
            return _combiner(
                _left.Matches(processId, output),
                _right.Matches(processId, output)
            );
        }

        Constraint _left;
        Constraint _right;
        Func<bool, bool, bool> _combiner;
    }

    class AndConstraint : BinaryConstraint
    {
        public AndConstraint(Constraint left, Constraint right)
            : base(left, right, (l, r) => l && r)
        {
        }
    }

    class OrConstraint : BinaryConstraint
    {
        public OrConstraint(Constraint left, Constraint right)
            : base(left, right, (l, r) => l || r)
        {
        }
    }

    class ConstConstraint : Constraint
    {
        public static ConstConstraint True = new ConstConstraint(true);
        public static ConstConstraint False = new ConstConstraint(false);

        public bool Matches(int processId, string output)
        {
            return _constant;
        }

        ConstConstraint(bool constant)
        {
            _constant = constant;
        }

        bool _constant;
    }

    class FilteredDebugOutputMonitor : DebugOutputMonitor
    {
        public FilteredDebugOutputMonitor(DebugOutputMonitor monitor, Constraint filter)
        {
            Filter = filter;

            _monitor = monitor;
            _monitor.OutputReceived += (processId, output) =>
            {
                if (Filter.Matches(processId, output))
                {
                    OutputReceived(processId, output);
                }
            };
        }

        public Constraint Filter
        {
            get;
            set;
        }

        public void Start()
        {
            _monitor.Start();
        }

        public void Stop()
        {
            _monitor.Stop();
        }

        public bool IsListening
        {
            get { return _monitor.IsListening; }
        }

        public event OutputReceivedHandler OutputReceived;

        DebugOutputMonitor _monitor;
    }
}
