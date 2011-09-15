using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

namespace Debuggernaut
{
    public partial class GoToLineDialog : Form
    {
        public GoToLineDialog(int minLineNumber, int maxLineNumber, int currentLineNumber)
        {
            InitializeComponent();

            _lineTextBox.Text = currentLineNumber.ToString();
            _lineTextBox.SelectAll();

            _descriptionLabel.Text =
                string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.GoToLineDescription,
                    minLineNumber,
                    maxLineNumber
                );
        }

        public int LineNumber
        {
            get
            {
                string text = _lineTextBox.Text.Trim();
                Debug.Assert(text.All(c => char.IsDigit(c)));

                return int.Parse(text);
            }
        }
    }
}
