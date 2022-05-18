using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{
	[Browsable(false), EditorBrowsable(EditorBrowsableState.Never), ComVisible(true), InterfaceType(ComInterfaceType.InterfaceIsDual)]
	public interface IXFontValue
	{
		[DispId(2)]
		bool Bold
		{
			get;
			set;
		}

		[DispId(3)]
		bool Italic
		{
			get;
			set;
		}

		[DispId(4)]
		string Name
		{
			get;
			set;
		}

		[DispId(5)]
		float Size
		{
			get;
			set;
		}

		[DispId(6)]
		bool Strikeout
		{
			get;
			set;
		}

		[DispId(7)]
		bool Underline
		{
			get;
			set;
		}
	}
}
