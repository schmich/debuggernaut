using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Debuggernaut
{
    partial class PatternDialog : Form
    {
        public PatternDialog(string description, string initialPattern)
        {
            InitializeComponent();

            _descriptionLabel.Text = description;

            _patternTextBox.Text = string.Format(".*{0}.*", initialPattern);
            _patternTextBox.Select(2, _patternTextBox.Text.Length - 4);
        }

        public string Pattern
        {
            get { return _patternTextBox.Text; }
        }
    }
}
