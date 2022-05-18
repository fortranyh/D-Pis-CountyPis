
using System;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{
	
	public class ASCardListViewEventArgs : EventArgs
	{
		private ASCardListViewItem ASCardListViewItem_0 = null;

		public ASCardListViewItem Item
		{
			get
			{
				return this.ASCardListViewItem_0;
			}
		}

		public ASCardListViewEventArgs(ASCardListViewItem item)
		{
			this.ASCardListViewItem_0 = item;
		}
	}
}
