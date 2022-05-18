
using System;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{
	
	public class ASCardContentItem
	{
		private string string_0 = null;

		private int int_0 = 0;

		private int int_1 = 0;

		private int int_2 = 0;

		private int int_3 = 0;

		public string DataField
		{
			get
			{
				return this.string_0;
			}
			set
			{
				this.string_0 = value;
			}
		}

		public int Left
		{
			get
			{
				return this.int_0;
			}
			set
			{
				this.int_0 = value;
			}
		}

		public int Top
		{
			get
			{
				return this.int_1;
			}
			set
			{
				this.int_1 = value;
			}
		}

		public int Width
		{
			get
			{
				return this.int_2;
			}
			set
			{
				this.int_2 = value;
			}
		}

		public int Height
		{
			get
			{
				return this.int_3;
			}
			set
			{
				this.int_3 = value;
			}
		}

		public virtual void OnPaint(ASCardContentItemPaintEventArgs args)
		{
		}
	}
}
