
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ASwartz.WinForms.Controls
{
    public class ASCardStringItem : ASCardContentItem
    {
        private string string_1 = null;

        private string string_2 = null;

        private float float_0 = 9f;

        private FontStyle fontStyle_0 = FontStyle.Regular;

        private StringAlignment stringAlignment_0 = StringAlignment.Near;

        private StringAlignment stringAlignment_1 = StringAlignment.Center;

        private Color color_0 = Color.Black;

        private bool bool_0 = false;

        public string Text
        {
            get
            {
                return this.string_1;
            }
            set
            {
                this.string_1 = value;
            }
        }

        public string FontName
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public float FontSize
        {
            get
            {
                return this.float_0;
            }
            set
            {
                this.float_0 = value;
            }
        }

        public FontStyle FontStyle
        {
            get
            {
                return this.fontStyle_0;
            }
            set
            {
                this.fontStyle_0 = value;
            }
        }

        public StringAlignment Align
        {
            get
            {
                return this.stringAlignment_0;
            }
            set
            {
                this.stringAlignment_0 = value;
            }
        }

        public StringAlignment LineAlign
        {
            get
            {
                return this.stringAlignment_1;
            }
            set
            {
                this.stringAlignment_1 = value;
            }
        }

        public Color Color
        {
            get
            {
                return this.color_0;
            }
            set
            {
                this.color_0 = value;
            }
        }

        public bool Multiline
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public override void OnPaint(ASCardContentItemPaintEventArgs args)
        {
            string text = this.Text;
            if (args.Value != null)
            {
                text = Convert.ToString(args.Value);
            }
            if (!string.IsNullOrEmpty(text))
            {
                using (StringFormat stringFormat = new StringFormat())
                {
                    stringFormat.Alignment = this.Align;
                    stringFormat.LineAlignment = this.LineAlign;
                    if (!this.Multiline)
                    {
                        stringFormat.FormatFlags = StringFormatFlags.NoWrap;
                    }
                    if (args.ListView != null && args.ListView.RightToLeft == RightToLeft.Yes)
                    {
                        stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;
                    }
                    XFontValue xFontValue = new XFontValue(this.FontName, this.FontSize, this.FontStyle);
                    SolidBrush solidBrush = GraphicsObjectBuffer.GetSolidBrush(this.Color);
                    if (args.Highlight)
                    {
                        solidBrush = GraphicsObjectBuffer.GetSolidBrush(SystemColors.HighlightText);
                    }
                    args.Graphics.DrawString(text, xFontValue.Value, solidBrush, new RectangleF((float)args.ViewBounds.Left, (float)args.ViewBounds.Top, (float)args.ViewBounds.Width, (float)args.ViewBounds.Height), stringFormat);
                }
            }
        }
    }
}
