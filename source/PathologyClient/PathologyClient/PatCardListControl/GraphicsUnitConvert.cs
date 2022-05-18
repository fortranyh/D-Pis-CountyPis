
using System;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{
	
	public sealed class GraphicsUnitConvert
	{
		private static float float_0;

		public static float Dpi
		{
			get
			{
				return GraphicsUnitConvert.float_0;
			}
			set
			{
				GraphicsUnitConvert.float_0 = value;
			}
		}

		static GraphicsUnitConvert()
		{
			GraphicsUnitConvert.float_0 = 96f;
			using (Graphics graphics = Graphics.FromHwnd(IntPtr.Zero))
			{
				GraphicsUnitConvert.float_0 = graphics.DpiX;
			}
		}

		public static double Convert(double vValue, GraphicsUnit OldUnit, GraphicsUnit NewUnit)
		{
			double result;
			if (OldUnit == NewUnit)
			{
				result = vValue;
			}
			else
			{
				result = vValue * GraphicsUnitConvert.GetRate(NewUnit, OldUnit);
			}
			return result;
		}

		public static float Convert(float vValue, GraphicsUnit OldUnit, GraphicsUnit NewUnit)
		{
			float result;
			if (OldUnit == NewUnit)
			{
				result = vValue;
			}
			else
			{
				result = (float)((double)vValue * GraphicsUnitConvert.GetRate(NewUnit, OldUnit));
			}
			return result;
		}

		public static float ConvertToCM(float vValue, GraphicsUnit oldUnit)
		{
			return (float)((double)GraphicsUnitConvert.Convert(vValue, oldUnit, GraphicsUnit.Millimeter) / 10.0);
		}

		public static float ConvertFromCM(float cmValue, GraphicsUnit unit)
		{
			return GraphicsUnitConvert.Convert(cmValue * 10f, GraphicsUnit.Millimeter, unit);
		}

		public static int Convert(int vValue, GraphicsUnit OldUnit, GraphicsUnit NewUnit)
		{
			int result;
			if (OldUnit == NewUnit)
			{
				result = vValue;
			}
			else
			{
				result = (int)((double)vValue * GraphicsUnitConvert.GetRate(NewUnit, OldUnit));
			}
			return result;
		}

		public static Point Convert(Point point_0, GraphicsUnit OldUnit, GraphicsUnit NewUnit)
		{
			Point result;
			if (OldUnit == NewUnit)
			{
				result = point_0;
			}
			else
			{
				double rate = GraphicsUnitConvert.GetRate(NewUnit, OldUnit);
				result = new Point((int)((double)point_0.X * rate), (int)((double)point_0.Y * rate));
			}
			return result;
		}

		public static PointF Convert(PointF pointF_0, GraphicsUnit OldUnit, GraphicsUnit NewUnit)
		{
			PointF result;
			if (OldUnit == NewUnit)
			{
				result = pointF_0;
			}
			else
			{
				double rate = GraphicsUnitConvert.GetRate(NewUnit, OldUnit);
				result = new PointF((float)((double)pointF_0.X * rate), (float)((double)pointF_0.Y * rate));
			}
			return result;
		}

		public static Point Convert(int int_0, int int_1, GraphicsUnit OldUnit, GraphicsUnit NewUnit)
		{
			Point result;
			if (OldUnit == NewUnit)
			{
				result = new Point(int_0, int_1);
			}
			else
			{
				double rate = GraphicsUnitConvert.GetRate(NewUnit, OldUnit);
				result = new Point((int)((double)int_0 * rate), (int)((double)int_1 * rate));
			}
			return result;
		}

		public static Size Convert(Size size, GraphicsUnit OldUnit, GraphicsUnit NewUnit)
		{
			Size result;
			if (OldUnit == NewUnit)
			{
				result = size;
			}
			else
			{
				double rate = GraphicsUnitConvert.GetRate(NewUnit, OldUnit);
				result = new Size((int)((double)size.Width * rate), (int)((double)size.Height * rate));
			}
			return result;
		}

		public static SizeF Convert(SizeF size, GraphicsUnit OldUnit, GraphicsUnit NewUnit)
		{
			SizeF result;
			if (OldUnit == NewUnit)
			{
				result = size;
			}
			else
			{
				double rate = GraphicsUnitConvert.GetRate(NewUnit, OldUnit);
				result = new SizeF((float)((double)size.Width * rate), (float)((double)size.Height * rate));
			}
			return result;
		}

		public static Rectangle Convert(Rectangle rect, GraphicsUnit OldUnit, GraphicsUnit NewUnit)
		{
			Rectangle result;
			if (OldUnit == NewUnit)
			{
				result = rect;
			}
			else
			{
				double rate = GraphicsUnitConvert.GetRate(NewUnit, OldUnit);
				result = new Rectangle((int)((double)rect.X * rate), (int)((double)rect.Y * rate), (int)((double)rect.Width * rate), (int)((double)rect.Height * rate));
			}
			return result;
		}

		public static RectangleF Convert(RectangleF rect, GraphicsUnit OldUnit, GraphicsUnit NewUnit)
		{
			RectangleF result;
			if (OldUnit == NewUnit)
			{
				result = rect;
			}
			else
			{
				double rate = GraphicsUnitConvert.GetRate(NewUnit, OldUnit);
				result = new RectangleF((float)((double)rect.X * rate), (float)((double)rect.Y * rate), (float)((double)rect.Width * rate), (float)((double)rect.Height * rate));
			}
			return result;
		}

		public static double GetRate(GraphicsUnit NewUnit, GraphicsUnit OldUnit)
		{
			return GraphicsUnitConvert.GetUnit(OldUnit) / GraphicsUnitConvert.GetUnit(NewUnit);
		}

		public static double GetDpi(GraphicsUnit unit)
		{
			double result;
			switch (unit)
			{
			case GraphicsUnit.Display:
				result = (double)GraphicsUnitConvert.float_0;
				break;
			case GraphicsUnit.Pixel:
				result = (double)GraphicsUnitConvert.float_0;
				break;
			case GraphicsUnit.Point:
				result = 72.0;
				break;
			case GraphicsUnit.Inch:
				result = 1.0;
				break;
			case GraphicsUnit.Document:
				result = 300.0;
				break;
			case GraphicsUnit.Millimeter:
				result = 25.4;
				break;
			default:
				result = (double)GraphicsUnitConvert.float_0;
				break;
			}
			return result;
		}

		public static double GetUnit(GraphicsUnit unit)
		{
			double result;
			switch (unit)
			{
			case GraphicsUnit.Display:
				result = (double)(1f / GraphicsUnitConvert.float_0);
				break;
			case GraphicsUnit.Pixel:
				result = (double)(1f / GraphicsUnitConvert.float_0);
				break;
			case GraphicsUnit.Point:
				result = 0.013888888888888888;
				break;
			case GraphicsUnit.Inch:
				result = 1.0;
				break;
			case GraphicsUnit.Document:
				result = 0.0033333333333333335;
				break;
			case GraphicsUnit.Millimeter:
				result = 0.03937007874015748;
				break;
			default:
				throw new NotSupportedException("Not support " + unit.ToString());
			}
			return result;
		}

		public static double Convert(double Value, LengthUnit OldUnit, LengthUnit NewUnit)
		{
			double result;
			if (OldUnit == NewUnit)
			{
				result = Value;
			}
			else
			{
				result = Value * GraphicsUnitConvert.GetUnit(OldUnit) / GraphicsUnitConvert.GetUnit(NewUnit);
			}
			return result;
		}

		public static double GetUnit(LengthUnit unit)
		{
			double result;
			switch (unit)
			{
			case LengthUnit.Document:
				result = 0.0033333333333333335;
				break;
			case LengthUnit.Inch:
				result = 1.0;
				break;
			case LengthUnit.Millimeter:
				result = 0.03937007874015748;
				break;
			case LengthUnit.Pixel:
				result = (double)(1f / GraphicsUnitConvert.float_0);
				break;
			case LengthUnit.Point:
				result = 0.013888888888888888;
				break;
			case LengthUnit.Centimerter:
				result = 0.39370078740157477;
				break;
			case LengthUnit.Twips:
				result = 0.00069444444444444447;
				break;
			default:
				throw new NotSupportedException("Not support " + unit.ToString());
			}
			return result;
		}

		public static int ToTwips(int Value, GraphicsUnit unit)
		{
			double unit2 = GraphicsUnitConvert.GetUnit(unit);
			return (int)((double)Value * unit2 * 1440.0);
		}

		public static int ToTwips(float Value, GraphicsUnit unit)
		{
			double unit2 = GraphicsUnitConvert.GetUnit(unit);
			return (int)((double)Value * unit2 * 1440.0);
		}

		public static int FromTwips(int Twips, GraphicsUnit unit)
		{
			double unit2 = GraphicsUnitConvert.GetUnit(unit);
			return (int)((double)Twips / (unit2 * 1440.0));
		}

		public static double FromTwips(double twips, GraphicsUnit unit)
		{
			double unit2 = GraphicsUnitConvert.GetUnit(unit);
			return twips / (unit2 * 1440.0);
		}

        public static double ParseCSSLength(string CSSLength, GraphicsUnit unit, double DefaultValue)
        {
            CSSLength = CSSLength.Trim();
            int length = CSSLength.Length;
            double result = 0.0;
            for (int i = 0; i < length; i++)
            {
                char value = CSSLength[i];
                if ("-.0123456789".IndexOf(value) < 0 && i > 0 && double.TryParse(CSSLength.Substring(0, i), NumberStyles.Any, null, out result))
                {
                    string text = CSSLength.Substring(i).Trim().ToLower();
                    switch (text)
                    {
                        case "px":
                            return Convert(result, GraphicsUnit.Pixel, unit);
                        case "pc":
                            return Convert(result, GraphicsUnit.Point, unit) * 12.0;
                        case "pt":
                            return Convert(result, GraphicsUnit.Point, unit);
                        case "in":
                            return Convert(result, GraphicsUnit.Inch, unit);
                        case "mm":
                            return Convert(result, GraphicsUnit.Millimeter, unit);
                        case "cm":
                            return Convert(result, GraphicsUnit.Millimeter, unit) * 10.0;
                    }
                }
            }
            if (double.TryParse(CSSLength, NumberStyles.Any, null, out result))
            {
                return Convert(result, GraphicsUnit.Pixel, unit);
            }
            return DefaultValue;
        }
		public static string ToCSSLength(double Value, GraphicsUnit unit, CssLengthUnit cssUnit)
		{
			double num = 0.0;
			string str = "";
			switch (cssUnit)
			{
			case CssLengthUnit.Centimeters:
				num = GraphicsUnitConvert.Convert(Value, unit, GraphicsUnit.Millimeter) / 10.0;
				str = "cm";
				break;
			case CssLengthUnit.Millimeters:
				num = GraphicsUnitConvert.Convert(Value, unit, GraphicsUnit.Millimeter);
				str = "mm";
				break;
			case CssLengthUnit.Inches:
				num = GraphicsUnitConvert.Convert(Value, unit, GraphicsUnit.Inch);
				str = "in";
				break;
			case CssLengthUnit.Points:
				num = GraphicsUnitConvert.Convert(Value, unit, GraphicsUnit.Point);
				str = "pt";
				break;
			case CssLengthUnit.Picas:
				num = GraphicsUnitConvert.Convert(Value, unit, GraphicsUnit.Point) / 12.0;
				str = "pc";
				break;
			case CssLengthUnit.Pixels:
				num = GraphicsUnitConvert.Convert(Value, unit, GraphicsUnit.Pixel);
				str = "px";
				break;
			}
			return num.ToString("0.0000") + str;
		}

		private GraphicsUnitConvert()
		{
		}
	}
    public enum LengthUnit
    {
        Document,
        Inch,
        Millimeter,
        Pixel,
        Point,
        Centimerter,
        Twips
    }
    public enum CssLengthUnit
    {
        Centimeters,
        Millimeters,
        Inches,
        Points,
        Picas,
        Pixels
    }
}
