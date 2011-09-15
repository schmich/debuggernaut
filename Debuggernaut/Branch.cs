using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using Microsoft.VisualStudio.VirtualTreeGrid;

namespace Debuggernaut
{
    class DefaultBranch : IBranch, IMultiColumnBranch
    {
        public virtual VirtualTreeLabelEditData BeginLabelEdit(int row, int column, VirtualTreeLabelEditActivationStyles activationStyle)
        {
            return new VirtualTreeLabelEditData();
        }

        public virtual LabelEditResult CommitLabelEdit(int row, int column, string newText)
        {
            return new LabelEditResult();
        }

        public virtual BranchFeatures Features
        {
            get { return BranchFeatures.None; }
        }

        public virtual VirtualTreeAccessibilityData GetAccessibilityData(int row, int column)
        {
            return new VirtualTreeAccessibilityData();
        }

        public virtual VirtualTreeDisplayData GetDisplayData(int row, int column, VirtualTreeDisplayDataMasks requiredData)
        {
            return _emptyDisplayData;
        }

        public virtual object GetObject(int row, int column, ObjectStyle style, ref int options)
        {
            return null;
        }

        public virtual string GetText(int row, int column)
        {
            return string.Empty;
        }

        public virtual string GetTipText(int row, int column, ToolTipType tipType)
        {
            return string.Empty;
        }

        public virtual bool IsExpandable(int row, int column)
        {
            return false;
        }

        public virtual LocateObjectData LocateObject(object obj, ObjectStyle style, int locateOptions)
        {
            return new LocateObjectData();
        }

        public event BranchModificationEventHandler OnBranchModification;

        protected virtual void BranchModified(BranchModificationEventArgs e)
        {
            if (OnBranchModification != null)
                OnBranchModification(this, e);
        }

        public virtual void OnDragEvent(object sender, int row, int column, DragEventType eventType, DragEventArgs args)
        {
        }

        public virtual void OnGiveFeedback(GiveFeedbackEventArgs args, int row, int column)
        {
        }

        public virtual void OnQueryContinueDrag(QueryContinueDragEventArgs args, int row, int column)
        {
        }

        public virtual VirtualTreeStartDragData OnStartDrag(object sender, int row, int column, DragReason reason)
        {
            return new VirtualTreeStartDragData();
        }

        public virtual StateRefreshChanges SynchronizeState(int row, int column, IBranch matchBranch, int matchRow, int matchColumn)
        {
            return StateRefreshChanges.None;
        }

        public virtual StateRefreshChanges ToggleState(int row, int column)
        {
            return StateRefreshChanges.None;
        }

        public virtual int UpdateCounter
        {
            get { return 0; }
        }

        public virtual int VisibleItemCount
        {
            get { return 0; }
        }

        public virtual int ColumnCount
        {
            get { return 0; }
        }

        public virtual SubItemCellStyles ColumnStyles(int column)
        {
            return SubItemCellStyles.Simple;
        }

        public virtual int GetJaggedColumnCount(int row)
        {
            return 0;
        }

        VirtualTreeDisplayData _emptyDisplayData = new VirtualTreeDisplayData();
    }

    class MyVirtualTreeControl : Microsoft.VisualStudio.VirtualTreeGrid.VirtualTreeControl
    {
        public MyVirtualTreeControl()
        {
        }

        protected override VirtualTreeHeaderControl CreateHeaderControl()
        {
            return new VirtualTreeHeaderControl(this);
        }
    }

    class Branch : DefaultBranch
    {
        public Branch(DebugOutputManager manager, VirtualTreeControl parent)
        {
            _manager = manager;
            _manager.OutputReceived += delegate
            {
                parent.Invoke(new Action(delegate
                {
                    BranchModified(BranchModificationEventArgs.InsertItems(this, VisibleItemCount - 2, 1));
                }));
            };
        }

        public override string GetText(int row, int column)
        {
            return _manager.GetText(row, column);
            //return row.ToString();
        }

        public override BranchFeatures Features
        {
            get { return BranchFeatures.InsertsAndDeletes; }
        }

        public override int VisibleItemCount
        {
            get { return _manager.OutputCount; }
        }

        public override int ColumnCount
        {
            get { return 3; }
        }

        DebugOutputManager _manager;
    }
}