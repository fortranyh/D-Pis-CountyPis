
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{
	
	public class ASCardImageItem : ASCardContentItem
	{
		private Image image_0 = null;

		public Image Image
		{
			get
			{
				return this.image_0;
			}
			set
			{
				this.image_0 = value;
			}
		}

		public override void OnPaint(ASCardContentItemPaintEventArgs args)
		{
			Image image = this.Image;
			if (args.Value != null)
			{
				image = (args.Value as Image);
			}
			if (image != null)
			{
				if (args.ListView != null)
				{
					args.ListView.method_1(image, args.ListViewItem);
				}
				args.Graphics.DrawImage(image, args.ViewBounds);
			}
		}
	}
}
