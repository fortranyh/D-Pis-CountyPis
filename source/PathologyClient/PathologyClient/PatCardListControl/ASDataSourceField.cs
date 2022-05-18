
using System;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{

	public class ASDataSourceField
	{
		internal bool bool_0 = false;

        internal ASDataSource.Enum17 enum17_0 = ASDataSource.Enum17.const_0;

		internal int int_0 = 0;

		private string string_0 = null;

		private string string_1 = null;

		internal int int_1 = 0;

		public bool Invalidate
		{
			get
			{
				return this.bool_0;
			}
		}

		public string FieldName
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

		public string BindingPath
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

		public int Index
		{
			get
			{
				return this.int_1;
			}
		}
	}
}
