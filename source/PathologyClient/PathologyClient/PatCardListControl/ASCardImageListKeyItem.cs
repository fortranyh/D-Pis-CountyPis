
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASwartz.WinForms.Controls
{
	
	public class ASCardImageListKeyItem : ASCardContentItem
	{
		private string string_1 = null;

		public string ImageKey
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

		public override void OnPaint(ASCardContentItemPaintEventArgs args)
		{
			ImageList imageList = args.ListView.ImageList;
			if (imageList != null)
			{
				int num = -1;
				if (args.Value is int)
				{
					num = (int)args.Value;
				}
				else
				{
					string text = Convert.ToString(args.Value);
					if (string.IsNullOrEmpty(text))
					{
						text = this.ImageKey;
					}
					if (!string.IsNullOrEmpty(text))
					{
						num = imageList.Images.IndexOfKey(text);
					}
				}
				if (num >= 0 && num < imageList.Images.Count)
				{
					imageList.Draw(args.Graphics, args.ViewBounds.Left, args.ViewBounds.Top, args.ViewBounds.Width, args.ViewBounds.Height, num);
				}
			}
		}
	}
}
