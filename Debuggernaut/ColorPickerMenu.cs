using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Debuggernaut
{
    class ColorPickerMenu : ToolStripDropDownMenu
    {
        public ColorPickerMenu(IEnumerable<Color> colors)
        {
            foreach (var color in colors)
            {
                if (color.IsEmpty)
                {
                    Items.Add(new ToolStripSeparator());
                }
                else
                {
                    var item = new ColorPickerMenuItem(color);
                    item.Click += new EventHandler(OnItemClick);

                    Items.Add(item);
                }
            }
        }

        void OnItemClick(object sender, EventArgs e)
        {
            var item = sender as ColorPickerMenuItem;
            if (item == null)
                return;

            ColorPicked(item, item.Color);
        }

        public event ColorPickedHandler ColorPicked;
    }

    delegate void ColorPickedHandler(ColorPickerMenuItem sender, Color color);

    class ColorPickerMenuItem : ToolStripMenuItem
    {
        public ColorPickerMenuItem(Color color)
        {
            Color = color;

            _fillBrush = new SolidBrush(Color);

            _borderPen = new Pen(
                Color.FromArgb(
                    (int)(color.R / 1.5f),
                    (int)(color.G / 1.5f),
                    (int)(color.B / 1.5f)
                )
            );
        }

        public Color Color
        {
            get;
            private set;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;

            // 24 for left pad
            // 36 for text start

            Rectangle r = e.ClipRectangle;
            Rectangle paint = new Rectangle(
                r.Left + 36,
                r.Top + 2,
                r.Width - 36 - 4,
                r.Height - 5
            );

            g.FillRectangle(_fillBrush, paint);
            g.DrawRectangle(_borderPen, paint);
        }

        Brush _fillBrush;
        Pen _borderPen;
    }
}
