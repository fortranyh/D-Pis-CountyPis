
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{
	
	public class ASCardListViewPaintItemEventArgs
	{
		private Graphics graphics_0 = null;

		private Rectangle rectangle_0 = Rectangle.Empty;

		private ASCardListViewItem ASCardListViewItem_0 = null;

		private bool bool_0 = false;

		private Rectangle rectangle_1 = Rectangle.Empty;

		public Graphics Graphics
		{
			get
			{
				return this.graphics_0;
			}
		}

		public Rectangle ClipRectangle
		{
			get
			{
				return this.rectangle_0;
			}
		}

		public ASCardListViewItem Item
		{
			get
			{
				return this.ASCardListViewItem_0;
			}
		}

		public bool Highlight
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

		private object DataBoundItem
		{
			get
			{
				object result;
				if (this.ASCardListViewItem_0 == null)
				{
					result = null;
				}
				else
				{
					result = this.ASCardListViewItem_0.DataBoundItem;
				}
				return result;
			}
		}

		public Rectangle ItemBounds
		{
			get
			{
				return this.rectangle_1;
			}
		}

		public ASCardListViewPaintItemEventArgs(Graphics graphics_1, Rectangle clip, ASCardListViewItem item, Rectangle itemBounds)
		{
			this.graphics_0 = graphics_1;
			this.rectangle_0 = clip;
			this.ASCardListViewItem_0 = item;
			this.rectangle_1 = itemBounds;
		}
	}
}
