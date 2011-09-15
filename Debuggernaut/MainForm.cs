using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Debuggernaut
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            EnableDoubleBuffering(_debugOutput);

            _statusUpdater = new StatusUpdater(_statusLabel, 3500);

            _monitor = new FilteredDebugOutputMonitor(
                new Win32DebugOutputMonitor(),
                ConstConstraint.True
            );

            _itemFactory = new FormattingListViewItemFactory(
                new DefaultListViewItemFactory()
            );

            _filterStatus.Image = Resources.Filter;

            _highlightProcessIdMenuItem.DropDown = CreateColorPickerMenu(OnHighlightProcessIdColorPicked);
            _highlightProcessNameMenuItem.DropDown = CreateColorPickerMenu(OnHighlightProcessNameColorPicked);
            _highlightOutputMenuItem.DropDown = CreateColorPickerMenu(OnHighlightOutputColorPicked);
            _highlightExactOutputMenuItem.DropDown = CreateColorPickerMenu(OnHighlightExactOutputColorPicked);
            _highlightOutputPrefixMenuItem.DropDown = CreateColorPickerMenu(OnHighlightOutputPrefixColorPicked);

            vtc.HasRootLines = false;
            vtc.MultiColumnHighlight = true;
            vtc.DistinguishFocusedColumn = false;
            vtc.SelectionMode = SelectionMode.MultiExtended;
            vtc.SetColumnHeaders(new Microsoft.VisualStudio.VirtualTreeGrid.VirtualTreeColumnHeader[] {
                new Microsoft.VisualStudio.VirtualTreeGrid.VirtualTreeColumnHeader("PID"),
                new Microsoft.VisualStudio.VirtualTreeGrid.VirtualTreeColumnHeader("Process Name"),
                new Microsoft.VisualStudio.VirtualTreeGrid.VirtualTreeColumnHeader("Output")
            }, true);

            vtc.MultiColumnTree = new Microsoft.VisualStudio.VirtualTreeGrid.MultiColumnTree(3);
            vtc.Tree.Root = new Branch(_outputManager, vtc);
            vtc.Visible = false;
        }

        ColorPickerMenu CreateColorPickerMenu(Action<Color> colorPickedHandler)
        {
            var menu = new ColorPickerMenu(new Color[] {
                Color.FromArgb(255, 170, 170),
                Color.FromArgb(255, 255, 170),
                Color.FromArgb(170, 255, 170),
                Color.FromArgb(170, 255, 255),
                Color.FromArgb(170, 170, 255),
                Color.FromArgb(255, 170, 255),
                Color.FromArgb(187, 187, 187)
            });

            menu.ColorPicked += (_, c) => { colorPickedHandler(c); };

            return menu;
        }

        void EnableDoubleBuffering(ListView listView)
        {
            if (listView == null)
                return;

            var doubleBufferedProperty = listView.GetType().GetProperty(
                "DoubleBuffered",
                System.Reflection.BindingFlags.NonPublic | BindingFlags.Instance
            );

            if (doubleBufferedProperty == null)
                return;

            doubleBufferedProperty.SetValue(listView, true, null);
        }

        void OnLoad(object sender, EventArgs e)
        {
            _monitor.OutputReceived += (processId, output) =>
            {
                var item = _itemFactory.Create(processId, output);

                BeginInvoke(new Action(() =>
                {
                    //_outputManager.AddDebugOutput(processId, output);

                    _debugOutput.Items.Add(item);

                    if (_autoscroll)
                        _debugOutput.EnsureVisible(item.Index);
                }));
            };

            StartMonitoring();
        }

        void StartMonitoring()
        {
            _monitor.Start();
            _captureStatus.Text = Resources.CapturingOutput;
            _captureStatus.Image = Resources.GreenBall;
        }

        void StopMonitoring()
        {
            _monitor.Stop();
            _captureStatus.Text = Resources.CapturingStopped;
            _captureStatus.Image = Resources.RedBall;
        }

        void ClearOutput()
        {
            _debugOutput.Items.Clear();
        }

        int CopySelection()
        {
            if (_debugOutput.SelectedItems.Count == 0)
                return 0;

            StringBuilder builder = new StringBuilder();

            var selectedItems = _debugOutput.SelectedItems.Cast<ListViewItem>();
            int count = selectedItems.Count();

            foreach (string line in GetListViewLines(selectedItems))
            {
                builder.Append(line);
                builder.Append("\r\n");
            }

            Clipboard.SetText(builder.ToString());

            return count;
        }

        void SaveOutputToFile(ListView listView, string fileName)
        {
            using (var file = File.Open(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                using (var stream = new StreamWriter(file))
                {
                    foreach (var line in GetListViewLines(listView))
                    {
                        stream.WriteLine(line);
                    }
                }
            }

            _statusUpdater.AddStatus(string.Format(Resources.OutputSavedTo, fileName));
        }

        IEnumerable<string> GetListViewLines(ListView listView)
        {
            return GetListViewLines(listView.Items.Cast<ListViewItem>());
        }

        IEnumerable<string> GetListViewLines(IEnumerable<ListViewItem> items)
        {
            foreach (ListViewItem item in items)
            {
                yield return string.Join(
                    "\t",
                    item.SubItems.Cast<ListViewItem.ListViewSubItem>().Select(s => s.Text).ToArray()
                );
            }
        }

        string GetUniqueFileName(string path, string filePrefix, string fileExtension)
        {
            string fileName = Path.Combine(path, filePrefix + fileExtension);

            int uniqueCounter = 0;

            while (File.Exists(fileName))
            {
                fileName = Path.Combine(
                    path,
                    string.Concat(filePrefix, uniqueCounter++, fileExtension)
                );
            }

            return fileName;
        }

        void ToggleMonitoring()
        {
            if (_monitor.IsListening)
            {
                StopMonitoring();
            }
            else
            {
                StartMonitoring();
            }
        }

        Regex GetPatternFromUser(string description, string initialPattern)
        {
            using (PatternDialog dialog = new PatternDialog(description, initialPattern))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    return new Regex(dialog.Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }

            return null;
        }

        int GetItemProcessId(ListViewItem item)
        {
            int processId;
            if (!int.TryParse(item.SubItems[0].Text, out processId))
                return -1;

            return processId;
        }

        int GetSelectedItemProcessId()
        {
            Debug.Assert(_debugOutput.SelectedItems.Count == 1);
            return GetItemProcessId(_debugOutput.SelectedItems[0]);
        }

        string GetItemProcessName(ListViewItem item)
        {
            return item.SubItems[1].Text;
        }

        string GetSelectedItemProcessName()
        {
            Debug.Assert(_debugOutput.SelectedItems.Count == 1);
            return GetItemProcessName(_debugOutput.SelectedItems[0]);
        }

        string GetItemOutput(ListViewItem item)
        {
            return item.SubItems[2].Text;
        }

        string GetSelectedItemOutput()
        {
            Debug.Assert(_debugOutput.SelectedItems.Count == 1);
            return GetItemOutput(_debugOutput.SelectedItems[0]);
        }

        string GetSelectedItemOutputPrefix()
        {
            string output = GetSelectedItemOutput();

            int index = output.IndexOf(':');
            if ((index != -1) && (index < 20))
            {
                return output.Substring(0, index);
            }

            return null;
        }

        void GoToItem(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= _debugOutput.Items.Count)
                return;

            _debugOutput.EnsureVisible(itemIndex);
            _debugOutput.SelectedItems.Clear();
            _debugOutput.SelectedIndices.Clear();
            _debugOutput.Items[itemIndex].Selected = true;
            _debugOutput.Items[itemIndex].Focused = true;

            _autoscroll = false;
            _autoscrollMenuItem.Checked = false;
        }

        void UpdateFilterStatus(string statusUpdate)
        {
            bool filtering = !(_monitor.Filter is ConstConstraint);

            _filterStatus.Visible = filtering;
            _captureStatus.Spring = !filtering;
            _resetFilterMenuItem.Enabled = filtering;

            _statusUpdater.AddStatus(statusUpdate);
        }

        void ExitApplication()
        {
            Application.Exit();
        }

        void OnSaveToDesktopClcked(object sender, EventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string fileName = GetUniqueFileName(desktopPath, Resources.DebugOutputFileName, ".txt");

            SaveOutputToFile(_debugOutput, fileName);
        }

        void OnSaveToFileClick(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.AddExtension = true;
                dialog.DefaultExt = ".txt";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SaveOutputToFile(_debugOutput, dialog.FileName);
                }
            }
        }

        void OnCaptureStatusClick(object sender, EventArgs e)
        {
            ToggleMonitoring();
        }

        void OnClearOutputClick(object sender, EventArgs e)
        {
            ClearOutput();
            _statusUpdater.AddStatus(Resources.OutputCleared);
        }

        void OnCaptureOutputCheckedChanged(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
                return;

            if (menuItem.Checked)
            {
                StartMonitoring();
            }
            else
            {
                StopMonitoring();
            }
        }

        void OnSelectAllClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in _debugOutput.Items)
                item.Selected = true;
        }

        void OnCopyClick(object sender, EventArgs e)
        {
            if (_debugOutput.SelectedItems.Count == 0)
            {
                _statusUpdater.AddStatus(Resources.SelectRowsToCopy);
            }
            else
            {
                int count = CopySelection();
                _statusUpdater.AddStatus(string.Format(Resources.RowsCopied, count));
            }
        }

        void OnExcludeProcessIdClick(object sender, EventArgs e)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            int processId = GetItemProcessId(_debugOutput.SelectedItems[0]);
            _monitor.Filter = new AndConstraint(_monitor.Filter, new ProcessIdConstraint(id => id != processId));

            UpdateFilterStatus(Resources.ProcessIdFilterAdded);
        }

        void OnIncludeProcessIdClick(object sender, EventArgs e)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            int processId = GetItemProcessId(_debugOutput.SelectedItems[0]);
            _monitor.Filter = new AndConstraint(_monitor.Filter, new ProcessIdConstraint(id => id == processId));

            UpdateFilterStatus(Resources.ProcessIdFilterAdded);
        }

        void OnExcludeProcessNameClick(object sender, EventArgs e)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            string processName = GetItemProcessName(_debugOutput.SelectedItems[0]);
            _monitor.Filter = new AndConstraint(_monitor.Filter, new ProcessNameConstraint(name => !StringComparer.OrdinalIgnoreCase.Equals(name, processName)));

            UpdateFilterStatus(Resources.ProcessNameFilterAdded);
        }

        void OnIncludeProcessNameClick(object sender, EventArgs e)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            string processName = GetItemProcessName(_debugOutput.SelectedItems[0]);
            _monitor.Filter = new AndConstraint(_monitor.Filter, new ProcessNameConstraint(name => StringComparer.OrdinalIgnoreCase.Equals(name, processName)));

            UpdateFilterStatus(Resources.ProcessNameFilterAdded);
        }

        void OnExcludeOutputClick(object sender, EventArgs e)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            string output = GetSelectedItemOutput();

            Regex pattern = GetPatternFromUser(Resources.ExcludePatternDescription, output);
            if (pattern == null)
                return;

            _monitor.Filter = new AndConstraint(
                _monitor.Filter,
                new NotConstraint(new RegexOutputConstraint(pattern))
            );

            UpdateFilterStatus(Resources.OutputFilterAdded);
        }


        void OnIncludeOutputClick(object sender, EventArgs e)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            string output = GetSelectedItemOutput();

            Regex pattern = GetPatternFromUser(Resources.IncludePatternDescription, output);
            if (pattern == null)
                return;

            _monitor.Filter = new AndConstraint(
                _monitor.Filter,
                new RegexOutputConstraint(pattern)
            );

            UpdateFilterStatus(Resources.OutputFilterAdded);
        }

        void OnExcludeExactOutputClick(object sender, EventArgs e)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            string output = GetSelectedItemOutput();

            _monitor.Filter = new AndConstraint(
                _monitor.Filter,
                new OutputConstraint(o => o != output)
            );

            UpdateFilterStatus(Resources.OutputFilterAdded);
        }

        void OnExcludeOutputPrefixClick(object sender, EventArgs e)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            string prefix = GetSelectedItemOutputPrefix();
            if (prefix == null)
                return;

            _monitor.Filter = new AndConstraint(
                _monitor.Filter,
                new OutputConstraint(o => !o.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            );

            UpdateFilterStatus(Resources.OutputFilterAdded);
        }

        void OnIncludeOutputPrefixClick(object sender, EventArgs e)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            string prefix = GetSelectedItemOutputPrefix();
            if (prefix == null)
                return;

            _monitor.Filter = new AndConstraint(
                _monitor.Filter,
                new OutputConstraint(o => o.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            );

            UpdateFilterStatus(Resources.OutputFilterAdded);
        }

        void OnResetFilterClick(object sender, EventArgs e)
        {
            _monitor.Filter = ConstConstraint.True;
            UpdateFilterStatus(Resources.FilterReset);
        }

        void OnContextMenuOpening(object sender, EventArgs e)
        {
            ContextMenuStrip menu = sender as ContextMenuStrip;
            if (menu == null)
                return;

            bool singleSelected = (_debugOutput.SelectedItems.Count == 1);

            foreach (ToolStripItem item in menu.Items)
            {
                item.Enabled = singleSelected;
            }

            _excludeOutputPrefixMenuItem.Enabled = _includeOutputPrefixMenuItem.Enabled = _highlightOutputPrefixMenuItem.Enabled = false;
            _excludeOutputPrefixMenuItem.Text = Resources.OutputPrefixMenuDisabled;
            _includeOutputPrefixMenuItem.Text = Resources.OutputPrefixMenuDisabled;
            _highlightOutputPrefixMenuItem.Text = Resources.OutputPrefixMenuDisabled;

            if (singleSelected)
            {
                string prefix = GetSelectedItemOutputPrefix();
                if (prefix != null)
                {
                    if (prefix.Length > 10)
                        prefix = prefix.Substring(0, 7) + "...";

                    string menuText = string.Format(Resources.OutputPrefixMenu, prefix);

                    _excludeOutputPrefixMenuItem.Text = menuText;
                    _includeOutputPrefixMenuItem.Text = menuText;
                    _highlightOutputPrefixMenuItem.Text = menuText;

                    _excludeOutputPrefixMenuItem.Enabled = _includeOutputPrefixMenuItem.Enabled = _highlightOutputPrefixMenuItem.Enabled = true;
                }
            }

            _copyCtxMenuItem.Enabled = true;
        }

        void OnHighlightProcessIdColorPicked(Color color)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            int processId = GetSelectedItemProcessId();

            _itemFactory.AddFormatter(
                new ProcessIdConstraint(id => id == processId),
                item => item.BackColor = color
            );

            _statusUpdater.AddStatus(Resources.ProcessIdHighlightAdded);
        }

        void OnHighlightProcessNameColorPicked(Color color)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            string processName = GetSelectedItemProcessName();

            _itemFactory.AddFormatter(
                new ProcessNameConstraint(name => StringComparer.OrdinalIgnoreCase.Equals(name, processName)),
                item => item.BackColor = color
            );

            _statusUpdater.AddStatus(Resources.ProcessNameHighlightAdded);
        }

        void OnHighlightOutputColorPicked(Color color)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            string output = GetSelectedItemOutput();

            Regex pattern = GetPatternFromUser(Resources.HighlightPatternDescription, output);
            if (pattern == null)
                return;

            _itemFactory.AddFormatter(
                new RegexOutputConstraint(pattern),
                item => item.BackColor = color
            );

            _statusUpdater.AddStatus(Resources.OutputHighlightAdded);
        }

        void OnHighlightExactOutputColorPicked(Color color)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            string output = GetSelectedItemOutput();

            _itemFactory.AddFormatter(
                new OutputConstraint(o => o == output),
                item => item.BackColor = color
            );

            _statusUpdater.AddStatus(Resources.OutputHighlightAdded);
        }

        void OnHighlightOutputPrefixColorPicked(Color color)
        {
            if (_debugOutput.SelectedItems.Count != 1)
                return;

            string prefix = GetSelectedItemOutputPrefix();
            if (prefix == null)
                return;

            _itemFactory.AddFormatter(
                new OutputConstraint(o => o.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)),
                item => item.BackColor = color
            );

            _statusUpdater.AddStatus(Resources.OutputHighlightAdded);
        }

        void OnAutoscrollCheckedChanged(object sender, EventArgs e)
        {
            _autoscroll = _autoscrollMenuItem.Checked;
            if (_autoscroll)
            {
                _debugOutput.EnsureVisible(_debugOutput.Items.Count - 1);
            }

            _statusUpdater.AddStatus(_autoscroll ? Resources.AutoscrollEnabled : Resources.AutoscrollDisabled);
        }

        void OnOutputItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (_debugOutput.SelectedItems.Count > 1)
                return;

            _autoscroll = (e.ItemIndex == _debugOutput.Items.Count - 1);
            _autoscrollMenuItem.Checked = _autoscroll;

            if (_autoscroll)
            {
                _debugOutput.SelectedIndices.Clear();
                _debugOutput.SelectedItems.Clear();

                if (_debugOutput.FocusedItem != null)
                    _debugOutput.FocusedItem.Focused = false;
            }
        }

        void OnSteadyDebugOutputCheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem == null)
                return;

            if (menuItem.Checked)
            {
                Thread producer = new Thread(new ThreadStart(delegate
                {
                    var random = new Random((int)DateTime.Now.Ticks);

                    while (true)
                    {
                        Debug.WriteLine(random.Next(0, 100));
                        Thread.Sleep(500);
                    }
                }));

                producer.IsBackground = true;
                producer.Start();
            }
        }

        void OnSingleShotDebugOutputClick(object sender, EventArgs e)
        {
            var random = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < 1000; ++i)
                Debug.WriteLine(random.Next(0, 100));
        }

        void OnGoToLineClick(object sender, EventArgs e)
        {
            if (_debugOutput.TopItem == null)
                return;

            int maxLineNumber = _debugOutput.Items.Count;
            int currentLineNumber = _debugOutput.TopItem.Index + 1;

            using (var dialog = new GoToLineDialog(1, maxLineNumber, currentLineNumber))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    GoToItem(dialog.LineNumber - 1);
                }
            }
        }

        void OnFindClick(object sender, EventArgs e)
        {
            throw new InvalidOperationException();
        }

        void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            StopMonitoring();
        }

        void OnFormKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 27 /* Escape */)
                ExitApplication();
        }

        void OnExitClick(object sender, EventArgs e)
        {
            ExitApplication();
        }

        StatusUpdater _statusUpdater;
        FilteredDebugOutputMonitor _monitor;
        FormattingListViewItemFactory _itemFactory;
        DebugOutputManager _outputManager = new DebugOutputManager();

        bool _autoscroll = true;
    }
}
