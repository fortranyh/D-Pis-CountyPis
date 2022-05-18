
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASwartz.WinForms.Controls
{
	
	public class ASCardListViewDragEventArgs : EventArgs
	{
		private DragEventArgs dragEventArgs_0 = null;

		private int int_0 = 0;

		private int int_1 = 0;

		private ASCardListViewControl ASCardListViewControl_0 = null;

		private ASCardListViewItem ASCardListViewItem_0 = null;

		private IDataObject idataObject_0 = null;

		public IDataObject Data
		{
			get
			{
				return this.dragEventArgs_0.Data;
			}
		}

		public DragDropEffects AllowedEffect
		{
			get
			{
				return this.dragEventArgs_0.AllowedEffect;
			}
		}

		public DragDropEffects Effect
		{
			get
			{
				return this.dragEventArgs_0.Effect;
			}
			set
			{
				this.dragEventArgs_0.Effect = value;
			}
		}

		public int KeyState
		{
			get
			{
				return this.dragEventArgs_0.KeyState;
			}
		}

		public int X
		{
			get
			{
				return this.dragEventArgs_0.X;
			}
		}

		public int Y
		{
			get
			{
				return this.dragEventArgs_0.Y;
			}
		}

		public int ClientX
		{
			get
			{
				return this.int_0;
			}
		}

		public int ClientY
		{
			get
			{
				return this.int_1;
			}
		}

		public ASCardListViewControl ListView
		{
			get
			{
				return this.ASCardListViewControl_0;
			}
		}

		public ASCardListViewItem Item
		{
			get
			{
				return this.ASCardListViewItem_0;
			}
		}

		public IDataObject DataObject
		{
			get
			{
				return this.idataObject_0;
			}
			set
			{
				this.idataObject_0 = value;
			}
		}

		public ASCardListViewDragEventArgs(ASCardListViewControl ASCardListViewControl_1, DragEventArgs args, ASCardListViewItem item)
		{
			this.ASCardListViewControl_0 = ASCardListViewControl_1;
			this.dragEventArgs_0 = args;
			Point p = new Point(args.X, args.Y);
			p = ASCardListViewControl_1.PointToClient(p);
			this.int_0 = p.X;
			this.int_1 = p.Y;
			this.ASCardListViewItem_0 = item;
		}
	}
}
