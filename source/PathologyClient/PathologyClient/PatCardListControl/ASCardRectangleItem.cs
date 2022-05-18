
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{
	
	public class ASCardRectangleItem : ASCardContentItem
	{
		private Color color_0 = Color.Transparent;

		private Color color_1 = Color.Black;

		private int int_4 = 1;

		private DashStyle dashStyle_0 = DashStyle.Solid;

		public Color BackColor
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

		public Color BorderColor
		{
			get
			{
				return this.color_1;
			}
			set
			{
				this.color_1 = value;
			}
		}

		[DefaultValue(1)]
		public int BorderWidth
		{
			get
			{
				return this.int_4;
			}
			set
			{
				this.int_4 = value;
			}
		}

		[DefaultValue(DashStyle.Solid)]
		public DashStyle BorderStyle
		{
			get
			{
				return this.dashStyle_0;
			}
			set
			{
				this.dashStyle_0 = value;
			}
		}

		public override void OnPaint(ASCardContentItemPaintEventArgs args)
		{
			Rectangle viewBounds = args.ViewBounds;
			if (this.BackColor.A != 0)
			{
				using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
				{
					args.Graphics.FillRectangle(solidBrush, viewBounds);
				}
			}
			if (this.BorderColor.A != 0 && this.BorderWidth > 0)
			{
				using (Pen pen = new Pen(this.BorderColor, (float)this.BorderWidth))
				{
					viewBounds.Inflate(-this.BorderWidth, -this.BorderWidth);
					Rectangle rectangle = new Rectangle(viewBounds.Left + this.BorderWidth, viewBounds.Top + this.BorderWidth, viewBounds.Width - this.BorderWidth * 2, viewBounds.Height - this.BorderWidth * 2);
					args.Graphics.DrawRectangle(pen, viewBounds);
				}
			}
		}
	}
}
