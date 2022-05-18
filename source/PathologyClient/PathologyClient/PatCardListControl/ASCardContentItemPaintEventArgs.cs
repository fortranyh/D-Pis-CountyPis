
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{
	
	public class ASCardContentItemPaintEventArgs
	{
		private ASCardListViewControl ASCardListViewControl_0 = null;

		private Graphics graphics_0 = null;

		private Rectangle rectangle_0 = Rectangle.Empty;

		private ASCardListViewItem ASCardListViewItem_0 = null;

		private ASCardContentItem ASCardContentItem_0 = null;

		private Rectangle rectangle_1 = Rectangle.Empty;

		private bool bool_0 = false;

		private object object_0 = null;

		public ASCardListViewControl ListView
		{
			get
			{
				return this.ASCardListViewControl_0;
			}
		}

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

		public ASCardListViewItem ListViewItem
		{
			get
			{
				return this.ASCardListViewItem_0;
			}
		}

		public ASCardContentItem ContentItem
		{
			get
			{
				return this.ASCardContentItem_0;
			}
		}

		public Rectangle ViewBounds
		{
			get
			{
				return this.rectangle_1;
			}
			set
			{
				this.rectangle_1 = value;
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

		public object Value
		{
			get
			{
				return this.object_0;
			}
			set
			{
				this.object_0 = value;
			}
		}

		public ASCardContentItemPaintEventArgs(ASCardListViewControl ASCardListViewControl_1, Graphics graphics_1, Rectangle rectangle_2, ASCardListViewItem item, ASCardContentItem contentItem)
		{
			this.ASCardListViewControl_0 = ASCardListViewControl_1;
			this.graphics_0 = graphics_1;
			this.rectangle_0 = rectangle_2;
			this.ASCardListViewItem_0 = item;
			this.ASCardContentItem_0 = contentItem;
		}
	}
}
