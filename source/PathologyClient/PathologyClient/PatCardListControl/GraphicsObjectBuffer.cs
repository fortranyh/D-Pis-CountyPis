
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{

	public static class GraphicsObjectBuffer
	{
		private static Dictionary<Color, SolidBrush> dictionary_0 = new Dictionary<Color, SolidBrush>();

		private static Dictionary<Color, Pen> dictionary_1 = new Dictionary<Color, Pen>();

		public static SolidBrush GetSolidBrush(Color color)
		{
			SolidBrush result;
			if (GraphicsObjectBuffer.dictionary_0.ContainsKey(color))
			{
				result = GraphicsObjectBuffer.dictionary_0[color];
			}
			else
			{
				SolidBrush solidBrush = new SolidBrush(color);
				GraphicsObjectBuffer.dictionary_0[color] = solidBrush;
				result = solidBrush;
			}
			return result;
		}

		public static Pen GetPen(Color color)
		{
			Pen result;
			if (GraphicsObjectBuffer.dictionary_1.ContainsKey(color))
			{
				result = GraphicsObjectBuffer.dictionary_1[color];
			}
			else
			{
				Pen pen = new Pen(color);
				GraphicsObjectBuffer.dictionary_1[color] = pen;
				result = pen;
			}
			return result;
		}

		public static void Clear()
		{
			foreach (SolidBrush current in GraphicsObjectBuffer.dictionary_0.Values)
			{
				current.Dispose();
			}
			GraphicsObjectBuffer.dictionary_0.Clear();
			foreach (Pen current2 in GraphicsObjectBuffer.dictionary_1.Values)
			{
				current2.Dispose();
			}
			GraphicsObjectBuffer.dictionary_1.Clear();
		}
	}
}
