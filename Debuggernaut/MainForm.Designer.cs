namespace Debuggernaut
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._debugOutput = new System.Windows.Forms.ListView();
            this._processIdColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._processNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._outputColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._contextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            this._copyCtxMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._separator3 = new System.Windows.Forms.ToolStripSeparator();
            this._includeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._includeProcessIdMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._includeProcessNameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._includeOutputMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._includeOutputPrefixMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._excludeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._excludeProcessIdMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._excludeProcessNameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._excludeExactOutputMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._excludeOutputMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._excludeOutputPrefixMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._highlightMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._highlightProcessIdMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._highlightProcessNameMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._highlightExactOutputMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._highlightOutputMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._highlightOutputPrefixMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this._fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._saveToFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._saveToDesktopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._separator0 = new System.Windows.Forms.ToolStripSeparator();
            this._exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._editMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._selectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._separator1 = new System.Windows.Forms.ToolStripSeparator();
            this._findMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._goToLineMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._outputMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._captureOutputMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._separator2 = new System.Windows.Forms.ToolStripSeparator();
            this._autoscrollMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._separator9 = new System.Windows.Forms.ToolStripSeparator();
            this._clearOutputMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._resetFilterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._debugMenu = new System.Windows.Forms.ToolStripMenuItem();
            this._steadyDebugOutputMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._statusBar = new System.Windows.Forms.StatusStrip();
            this._statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._filterStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this._captureStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.vtc = new Debuggernaut.MyVirtualTreeControl();
            this._singleShotDebugOutputMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._contextMenuStrip.SuspendLayout();
            this._mainMenuStrip.SuspendLayout();
            this._statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _debugOutput
            // 
            this._debugOutput.AllowColumnReorder = true;
            this._debugOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._debugOutput.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._processIdColumnHeader,
            this._processNameColumnHeader,
            this._outputColumnHeader});
            this._debugOutput.ContextMenuStrip = this._contextMenuStrip;
            this._debugOutput.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._debugOutput.FullRowSelect = true;
            this._debugOutput.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this._debugOutput.Location = new System.Drawing.Point(0, 22);
            this._debugOutput.Margin = new System.Windows.Forms.Padding(0);
            this._debugOutput.Name = "_debugOutput";
            this._debugOutput.Size = new System.Drawing.Size(684, 419);
            this._debugOutput.TabIndex = 0;
            this._debugOutput.UseCompatibleStateImageBehavior = false;
            this._debugOutput.View = System.Windows.Forms.View.Details;
            this._debugOutput.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.OnOutputItemSelectionChanged);
            // 
            // _processIdColumnHeader
            // 
            this._processIdColumnHeader.Text = "PID";
            this._processIdColumnHeader.Width = 38;
            // 
            // _processNameColumnHeader
            // 
            this._processNameColumnHeader.Text = "Process Name";
            this._processNameColumnHeader.Width = 146;
            // 
            // _outputColumnHeader
            // 
            this._outputColumnHeader.Text = "Output";
            this._outputColumnHeader.Width = 453;
            // 
            // _contextMenuStrip
            // 
            this._contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._copyCtxMenuItem,
            this._separator3,
            this._includeMenuItem,
            this._excludeMenuItem,
            this._highlightMenuItem});
            this._contextMenuStrip.Name = "_contextMenuStrip";
            this._contextMenuStrip.Size = new System.Drawing.Size(145, 98);
            this._contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.OnContextMenuOpening);
            // 
            // _copyCtxMenuItem
            // 
            this._copyCtxMenuItem.Name = "_copyCtxMenuItem";
            this._copyCtxMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this._copyCtxMenuItem.Size = new System.Drawing.Size(144, 22);
            this._copyCtxMenuItem.Text = "&Copy";
            this._copyCtxMenuItem.Click += new System.EventHandler(this.OnCopyClick);
            // 
            // _separator3
            // 
            this._separator3.Name = "_separator3";
            this._separator3.Size = new System.Drawing.Size(141, 6);
            // 
            // _includeMenuItem
            // 
            this._includeMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._includeProcessIdMenuItem,
            this._includeProcessNameMenuItem,
            this._includeOutputMenuItem,
            this._includeOutputPrefixMenuItem});
            this._includeMenuItem.Name = "_includeMenuItem";
            this._includeMenuItem.Size = new System.Drawing.Size(144, 22);
            this._includeMenuItem.Text = "Include Only";
            // 
            // _includeProcessIdMenuItem
            // 
            this._includeProcessIdMenuItem.Name = "_includeProcessIdMenuItem";
            this._includeProcessIdMenuItem.Size = new System.Drawing.Size(186, 22);
            this._includeProcessIdMenuItem.Text = "This Process ID";
            this._includeProcessIdMenuItem.Click += new System.EventHandler(this.OnIncludeProcessIdClick);
            // 
            // _includeProcessNameMenuItem
            // 
            this._includeProcessNameMenuItem.Name = "_includeProcessNameMenuItem";
            this._includeProcessNameMenuItem.Size = new System.Drawing.Size(186, 22);
            this._includeProcessNameMenuItem.Text = "This Process Name";
            this._includeProcessNameMenuItem.Click += new System.EventHandler(this.OnIncludeProcessNameClick);
            // 
            // _includeOutputMenuItem
            // 
            this._includeOutputMenuItem.Name = "_includeOutputMenuItem";
            this._includeOutputMenuItem.Size = new System.Drawing.Size(186, 22);
            this._includeOutputMenuItem.Text = "Similar Output...";
            this._includeOutputMenuItem.Click += new System.EventHandler(this.OnIncludeOutputClick);
            // 
            // _includeOutputPrefixMenuItem
            // 
            this._includeOutputPrefixMenuItem.Name = "_includeOutputPrefixMenuItem";
            this._includeOutputPrefixMenuItem.Size = new System.Drawing.Size(186, 22);
            this._includeOutputPrefixMenuItem.Text = "Output Starting With...";
            this._includeOutputPrefixMenuItem.Click += new System.EventHandler(this.OnIncludeOutputPrefixClick);
            // 
            // _excludeMenuItem
            // 
            this._excludeMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._excludeProcessIdMenuItem,
            this._excludeProcessNameMenuItem,
            this._excludeExactOutputMenuItem,
            this._excludeOutputMenuItem,
            this._excludeOutputPrefixMenuItem});
            this._excludeMenuItem.Name = "_excludeMenuItem";
            this._excludeMenuItem.Size = new System.Drawing.Size(144, 22);
            this._excludeMenuItem.Text = "Exclude";
            // 
            // _excludeProcessIdMenuItem
            // 
            this._excludeProcessIdMenuItem.Name = "_excludeProcessIdMenuItem";
            this._excludeProcessIdMenuItem.Size = new System.Drawing.Size(186, 22);
            this._excludeProcessIdMenuItem.Text = "This Process ID";
            this._excludeProcessIdMenuItem.Click += new System.EventHandler(this.OnExcludeProcessIdClick);
            // 
            // _excludeProcessNameMenuItem
            // 
            this._excludeProcessNameMenuItem.Name = "_excludeProcessNameMenuItem";
            this._excludeProcessNameMenuItem.Size = new System.Drawing.Size(186, 22);
            this._excludeProcessNameMenuItem.Text = "This Process Name";
            this._excludeProcessNameMenuItem.Click += new System.EventHandler(this.OnExcludeProcessNameClick);
            // 
            // _excludeExactOutputMenuItem
            // 
            this._excludeExactOutputMenuItem.Name = "_excludeExactOutputMenuItem";
            this._excludeExactOutputMenuItem.Size = new System.Drawing.Size(186, 22);
            this._excludeExactOutputMenuItem.Text = "This Exact Output";
            this._excludeExactOutputMenuItem.Click += new System.EventHandler(this.OnExcludeExactOutputClick);
            // 
            // _excludeOutputMenuItem
            // 
            this._excludeOutputMenuItem.Name = "_excludeOutputMenuItem";
            this._excludeOutputMenuItem.Size = new System.Drawing.Size(186, 22);
            this._excludeOutputMenuItem.Text = "Similar Output...";
            this._excludeOutputMenuItem.Click += new System.EventHandler(this.OnExcludeOutputClick);
            // 
            // _excludeOutputPrefixMenuItem
            // 
            this._excludeOutputPrefixMenuItem.Name = "_excludeOutputPrefixMenuItem";
            this._excludeOutputPrefixMenuItem.Size = new System.Drawing.Size(186, 22);
            this._excludeOutputPrefixMenuItem.Text = "Output Starting With...";
            this._excludeOutputPrefixMenuItem.Click += new System.EventHandler(this.OnExcludeOutputPrefixClick);
            // 
            // _highlightMenuItem
            // 
            this._highlightMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._highlightProcessIdMenuItem,
            this._highlightProcessNameMenuItem,
            this._highlightExactOutputMenuItem,
            this._highlightOutputMenuItem,
            this._highlightOutputPrefixMenuItem});
            this._highlightMenuItem.Name = "_highlightMenuItem";
            this._highlightMenuItem.Size = new System.Drawing.Size(144, 22);
            this._highlightMenuItem.Text = "Highlight";
            // 
            // _highlightProcessIdMenuItem
            // 
            this._highlightProcessIdMenuItem.Name = "_highlightProcessIdMenuItem";
            this._highlightProcessIdMenuItem.Size = new System.Drawing.Size(186, 22);
            this._highlightProcessIdMenuItem.Text = "This Process ID";
            // 
            // _highlightProcessNameMenuItem
            // 
            this._highlightProcessNameMenuItem.Name = "_highlightProcessNameMenuItem";
            this._highlightProcessNameMenuItem.Size = new System.Drawing.Size(186, 22);
            this._highlightProcessNameMenuItem.Text = "This Process Name";
            // 
            // _highlightExactOutputMenuItem
            // 
            this._highlightExactOutputMenuItem.Name = "_highlightExactOutputMenuItem";
            this._highlightExactOutputMenuItem.Size = new System.Drawing.Size(186, 22);
            this._highlightExactOutputMenuItem.Text = "This Exact Output";
            // 
            // _highlightOutputMenuItem
            // 
            this._highlightOutputMenuItem.Name = "_highlightOutputMenuItem";
            this._highlightOutputMenuItem.Size = new System.Drawing.Size(186, 22);
            this._highlightOutputMenuItem.Text = "Similar Output...";
            // 
            // _highlightOutputPrefixMenuItem
            // 
            this._highlightOutputPrefixMenuItem.Name = "_highlightOutputPrefixMenuItem";
            this._highlightOutputPrefixMenuItem.Size = new System.Drawing.Size(186, 22);
            this._highlightOutputPrefixMenuItem.Text = "Output Starting With...";
            // 
            // _mainMenuStrip
            // 
            this._mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileMenu,
            this._editMenu,
            this._outputMenu,
            this._debugMenu});
            this._mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this._mainMenuStrip.Name = "_mainMenuStrip";
            this._mainMenuStrip.Size = new System.Drawing.Size(684, 24);
            this._mainMenuStrip.TabIndex = 1;
            // 
            // _fileMenu
            // 
            this._fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveToFileMenuItem,
            this._saveToDesktopMenuItem,
            this._separator0,
            this._exitMenuItem});
            this._fileMenu.Name = "_fileMenu";
            this._fileMenu.Size = new System.Drawing.Size(35, 20);
            this._fileMenu.Text = "&File";
            // 
            // _saveToFileMenuItem
            // 
            this._saveToFileMenuItem.Name = "_saveToFileMenuItem";
            this._saveToFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this._saveToFileMenuItem.Size = new System.Drawing.Size(229, 22);
            this._saveToFileMenuItem.Text = "&Save Output to File...";
            this._saveToFileMenuItem.Click += new System.EventHandler(this.OnSaveToFileClick);
            // 
            // _saveToDesktopMenuItem
            // 
            this._saveToDesktopMenuItem.Name = "_saveToDesktopMenuItem";
            this._saveToDesktopMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this._saveToDesktopMenuItem.Size = new System.Drawing.Size(229, 22);
            this._saveToDesktopMenuItem.Text = "Save Output to &Desktop";
            this._saveToDesktopMenuItem.Click += new System.EventHandler(this.OnSaveToDesktopClcked);
            // 
            // _separator0
            // 
            this._separator0.Name = "_separator0";
            this._separator0.Size = new System.Drawing.Size(226, 6);
            // 
            // _exitMenuItem
            // 
            this._exitMenuItem.Name = "_exitMenuItem";
            this._exitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this._exitMenuItem.Size = new System.Drawing.Size(229, 22);
            this._exitMenuItem.Text = "E&xit";
            this._exitMenuItem.Click += new System.EventHandler(this.OnExitClick);
            // 
            // _editMenu
            // 
            this._editMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._copyMenuItem,
            this._selectAllMenuItem,
            this._separator1,
            this._findMenuItem,
            this._goToLineMenuItem});
            this._editMenu.Name = "_editMenu";
            this._editMenu.Size = new System.Drawing.Size(37, 20);
            this._editMenu.Text = "&Edit";
            // 
            // _copyMenuItem
            // 
            this._copyMenuItem.Name = "_copyMenuItem";
            this._copyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this._copyMenuItem.Size = new System.Drawing.Size(156, 22);
            this._copyMenuItem.Text = "C&opy";
            this._copyMenuItem.Click += new System.EventHandler(this.OnCopyClick);
            // 
            // _selectAllMenuItem
            // 
            this._selectAllMenuItem.Name = "_selectAllMenuItem";
            this._selectAllMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this._selectAllMenuItem.Size = new System.Drawing.Size(156, 22);
            this._selectAllMenuItem.Text = "Select &All";
            this._selectAllMenuItem.Click += new System.EventHandler(this.OnSelectAllClick);
            // 
            // _separator1
            // 
            this._separator1.Name = "_separator1";
            this._separator1.Size = new System.Drawing.Size(153, 6);
            // 
            // _findMenuItem
            // 
            this._findMenuItem.Name = "_findMenuItem";
            this._findMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this._findMenuItem.Size = new System.Drawing.Size(156, 22);
            this._findMenuItem.Text = "&Find";
            this._findMenuItem.Click += new System.EventHandler(this.OnFindClick);
            // 
            // _goToLineMenuItem
            // 
            this._goToLineMenuItem.Name = "_goToLineMenuItem";
            this._goToLineMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this._goToLineMenuItem.Size = new System.Drawing.Size(156, 22);
            this._goToLineMenuItem.Text = "&Go To...";
            this._goToLineMenuItem.Click += new System.EventHandler(this.OnGoToLineClick);
            // 
            // _outputMenu
            // 
            this._outputMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._captureOutputMenuItem,
            this._separator2,
            this._autoscrollMenuItem,
            this._separator9,
            this._clearOutputMenuItem,
            this._resetFilterMenuItem});
            this._outputMenu.Name = "_outputMenu";
            this._outputMenu.Size = new System.Drawing.Size(53, 20);
            this._outputMenu.Text = "&Output";
            // 
            // _captureOutputMenuItem
            // 
            this._captureOutputMenuItem.Checked = true;
            this._captureOutputMenuItem.CheckOnClick = true;
            this._captureOutputMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this._captureOutputMenuItem.Name = "_captureOutputMenuItem";
            this._captureOutputMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this._captureOutputMenuItem.Size = new System.Drawing.Size(222, 22);
            this._captureOutputMenuItem.Text = "Capture Debug Output";
            this._captureOutputMenuItem.CheckedChanged += new System.EventHandler(this.OnCaptureOutputCheckedChanged);
            // 
            // _separator2
            // 
            this._separator2.Name = "_separator2";
            this._separator2.Size = new System.Drawing.Size(219, 6);
            // 
            // _autoscrollMenuItem
            // 
            this._autoscrollMenuItem.Checked = true;
            this._autoscrollMenuItem.CheckOnClick = true;
            this._autoscrollMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this._autoscrollMenuItem.Name = "_autoscrollMenuItem";
            this._autoscrollMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.A)));
            this._autoscrollMenuItem.Size = new System.Drawing.Size(222, 22);
            this._autoscrollMenuItem.Text = "Autoscroll";
            this._autoscrollMenuItem.CheckedChanged += new System.EventHandler(this.OnAutoscrollCheckedChanged);
            // 
            // _separator9
            // 
            this._separator9.Name = "_separator9";
            this._separator9.Size = new System.Drawing.Size(219, 6);
            // 
            // _clearOutputMenuItem
            // 
            this._clearOutputMenuItem.Name = "_clearOutputMenuItem";
            this._clearOutputMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this._clearOutputMenuItem.Size = new System.Drawing.Size(222, 22);
            this._clearOutputMenuItem.Text = "&Clear Output";
            this._clearOutputMenuItem.Click += new System.EventHandler(this.OnClearOutputClick);
            // 
            // _resetFilterMenuItem
            // 
            this._resetFilterMenuItem.Enabled = false;
            this._resetFilterMenuItem.Name = "_resetFilterMenuItem";
            this._resetFilterMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.X)));
            this._resetFilterMenuItem.Size = new System.Drawing.Size(222, 22);
            this._resetFilterMenuItem.Text = "&Reset Filter";
            this._resetFilterMenuItem.Click += new System.EventHandler(this.OnResetFilterClick);
            // 
            // _debugMenu
            // 
            this._debugMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._steadyDebugOutputMenuItem,
            this._singleShotDebugOutputMenuItem});
            this._debugMenu.Name = "_debugMenu";
            this._debugMenu.Size = new System.Drawing.Size(50, 20);
            this._debugMenu.Text = "&Debug";
            // 
            // _steadyDebugOutputMenuItem
            // 
            this._steadyDebugOutputMenuItem.CheckOnClick = true;
            this._steadyDebugOutputMenuItem.Name = "_steadyDebugOutputMenuItem";
            this._steadyDebugOutputMenuItem.Size = new System.Drawing.Size(227, 22);
            this._steadyDebugOutputMenuItem.Text = "Generate Steady Debug Output";
            this._steadyDebugOutputMenuItem.CheckedChanged += new System.EventHandler(this.OnSteadyDebugOutputCheckedChanged);
            // 
            // _statusBar
            // 
            this._statusBar.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._statusLabel,
            this._filterStatus,
            this._captureStatus});
            this._statusBar.Location = new System.Drawing.Point(0, 440);
            this._statusBar.Name = "_statusBar";
            this._statusBar.Size = new System.Drawing.Size(684, 22);
            this._statusBar.TabIndex = 0;
            // 
            // _statusLabel
            // 
            this._statusLabel.Name = "_statusLabel";
            this._statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // _filterStatus
            // 
            this._filterStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._filterStatus.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this._filterStatus.Name = "_filterStatus";
            this._filterStatus.Size = new System.Drawing.Size(324, 17);
            this._filterStatus.Spring = true;
            this._filterStatus.Text = "Filtering";
            this._filterStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._filterStatus.Visible = false;
            // 
            // _captureStatus
            // 
            this._captureStatus.ActiveLinkColor = System.Drawing.Color.DarkSlateBlue;
            this._captureStatus.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._captureStatus.IsLink = true;
            this._captureStatus.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this._captureStatus.LinkColor = System.Drawing.Color.RoyalBlue;
            this._captureStatus.Margin = new System.Windows.Forms.Padding(0, 3, 3, 2);
            this._captureStatus.Name = "_captureStatus";
            this._captureStatus.Size = new System.Drawing.Size(666, 17);
            this._captureStatus.Spring = true;
            this._captureStatus.Text = "Capturing Output";
            this._captureStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._captureStatus.Click += new System.EventHandler(this.OnCaptureStatusClick);
            // 
            // vtc
            // 
            this.vtc.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vtc.Location = new System.Drawing.Point(12, 56);
            this.vtc.Name = "vtc";
            this.vtc.Size = new System.Drawing.Size(459, 373);
            this.vtc.TabIndex = 2;
            this.vtc.Text = "virtualTreeControl1";
            // 
            // _singleShotDebugOutputMenuItem
            // 
            this._singleShotDebugOutputMenuItem.Name = "_singleShotDebugOutputMenuItem";
            this._singleShotDebugOutputMenuItem.Size = new System.Drawing.Size(227, 22);
            this._singleShotDebugOutputMenuItem.Text = "Single-shot Debug Output";
            this._singleShotDebugOutputMenuItem.Click += new System.EventHandler(this.OnSingleShotDebugOutputClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this._statusBar);
            this.Controls.Add(this.vtc);
            this.Controls.Add(this._debugOutput);
            this.Controls.Add(this._mainMenuStrip);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this._mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Debuggernaut";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnFormKeyPress);
            this._contextMenuStrip.ResumeLayout(false);
            this._mainMenuStrip.ResumeLayout(false);
            this._mainMenuStrip.PerformLayout();
            this._statusBar.ResumeLayout(false);
            this._statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView _debugOutput;
        private System.Windows.Forms.ColumnHeader _processIdColumnHeader;
        private System.Windows.Forms.ColumnHeader _processNameColumnHeader;
        private System.Windows.Forms.ColumnHeader _outputColumnHeader;
        private System.Windows.Forms.MenuStrip _mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _fileMenu;
        private System.Windows.Forms.ToolStripMenuItem _exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _editMenu;
        private System.Windows.Forms.ToolStripMenuItem _saveToDesktopMenuItem;
        private System.Windows.Forms.ToolStripSeparator _separator0;
        private System.Windows.Forms.ToolStripMenuItem _saveToFileMenuItem;
        private System.Windows.Forms.StatusStrip _statusBar;
        private System.Windows.Forms.ToolStripStatusLabel _statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel _captureStatus;
        private System.Windows.Forms.ContextMenuStrip _contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem _copyCtxMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _copyMenuItem;
        private System.Windows.Forms.ToolStripSeparator _separator1;
        private System.Windows.Forms.ToolStripMenuItem _selectAllMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel _filterStatus;
        private System.Windows.Forms.ToolStripMenuItem _findMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _outputMenu;
        private System.Windows.Forms.ToolStripMenuItem _captureOutputMenuItem;
        private System.Windows.Forms.ToolStripSeparator _separator2;
        private System.Windows.Forms.ToolStripMenuItem _clearOutputMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _resetFilterMenuItem;
        private System.Windows.Forms.ToolStripSeparator _separator9;
        private System.Windows.Forms.ToolStripMenuItem _autoscrollMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _goToLineMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _includeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _includeProcessIdMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _includeProcessNameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _includeOutputMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _includeOutputPrefixMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _excludeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _highlightMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _excludeProcessIdMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _excludeProcessNameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _excludeOutputMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _excludeExactOutputMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _excludeOutputPrefixMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _highlightProcessIdMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _highlightProcessNameMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _highlightOutputMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _highlightExactOutputMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _highlightOutputPrefixMenuItem;
        private System.Windows.Forms.ToolStripSeparator _separator3;
        private MyVirtualTreeControl vtc;
        private System.Windows.Forms.ToolStripMenuItem _debugMenu;
        private System.Windows.Forms.ToolStripMenuItem _steadyDebugOutputMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _singleShotDebugOutputMenuItem;

    }
}

