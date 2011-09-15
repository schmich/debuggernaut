using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Debuggernaut
{
    interface ListViewItemFactory
    {
        ListViewItem Create(int processId, string output);
    }

    class DefaultListViewItemFactory : ListViewItemFactory
    {
        public ListViewItem Create(int processId, string output)
        {
            string processName = GetProcessName(processId);

            var item = new ListViewItem(new string[]
            {
                processId.ToString(),
                string.IsNullOrEmpty(processName) ? "(unknown)" : processName,
                output
            });

            return item;
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
    }

    class FormattingListViewItemFactory : ListViewItemFactory
    {
        public FormattingListViewItemFactory(ListViewItemFactory factory)
        {
            _factory = factory;
        }

        public void AddFormatter(Constraint constraint, Action<ListViewItem> formatter)
        {
            _modifiers.Add(new KeyValuePair<Constraint, Action<ListViewItem>>(constraint, formatter));
        }

        public ListViewItem Create(int processId, string output)
        {
            var item = _factory.Create(processId, output);

            foreach (var modifier in _modifiers)
            {
                Constraint constraint = modifier.Key;
                Action<ListViewItem> formatter = modifier.Value;

                if (constraint.Matches(processId, output))
                    formatter(item);
            }

            return item;
        }

        ListViewItemFactory _factory;

        List<KeyValuePair<Constraint, Action<ListViewItem>>> _modifiers = new List<KeyValuePair<Constraint, Action<ListViewItem>>>();
    }
}
