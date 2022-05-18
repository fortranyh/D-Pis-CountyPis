using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{
	[ComVisible(false)]
	public class ShapeDrawer
	{
		private RectangleF rectangleF_0 = RectangleF.Empty;

		private float float_0 = 0f;

		private ShapeTypes shapeTypes_0 = ShapeTypes.Rectangle;

		private Pen pen_0 = null;

		private Brush brush_0 = null;

		public RectangleF Bounds
		{
			get
			{
				return this.rectangleF_0;
			}
			set
			{
				this.rectangleF_0 = value;
			}
		}

		public float Left
		{
			get
			{
				return this.rectangleF_0.Left;
			}
			set
			{
				this.rectangleF_0.X = value;
			}
		}

		public float Top
		{
			get
			{
				return this.rectangleF_0.Top;
			}
			set
			{
				this.rectangleF_0.Y = value;
			}
		}

		public float Width
		{
			get
			{
				return this.rectangleF_0.Width;
			}
			set
			{
				this.rectangleF_0.Width = value;
			}
		}

		public float Height
		{
			get
			{
				return this.rectangleF_0.Height;
			}
			set
			{
				this.rectangleF_0.Height = value;
			}
		}

		public float RoundRadio
		{
			get
			{
				return this.float_0;
			}
			set
			{
				this.float_0 = value;
			}
		}

		public ShapeTypes Type
		{
			get
			{
				return this.shapeTypes_0;
			}
			set
			{
				this.shapeTypes_0 = value;
			}
		}

		public Pen BorderPen
		{
			get
			{
				return this.pen_0;
			}
			set
			{
				this.pen_0 = value;
			}
		}

		public Brush FillBrush
		{
			get
			{
				return this.brush_0;
			}
			set
			{
				this.brush_0 = value;
			}
		}

		public ShapeDrawer()
		{
		}

		public ShapeDrawer(RectangleF rect, ShapeTypes type, Color BorderColor, Color FillColor)
		{
			this.rectangleF_0 = rect;
			this.shapeTypes_0 = type;
			this.pen_0 = new Pen(BorderColor);
			this.brush_0 = new SolidBrush(FillColor);
		}

		public GraphicsPath CreatePath()
		{
			GraphicsPath result;
			switch (this.shapeTypes_0)
			{
			case ShapeTypes.Rectangle:
				result = ShapeDrawer.CreateRoundRectanglePath(this.rectangleF_0, this.float_0);
				break;
			case ShapeTypes.Square:
				result = ShapeDrawer.CreateRoundRectanglePath(this.method_0(), this.float_0);
				break;
			case ShapeTypes.Ellipse:
			{
				GraphicsPath graphicsPath = new GraphicsPath();
				graphicsPath.AddEllipse(this.rectangleF_0);
				result = graphicsPath;
				break;
			}
			case ShapeTypes.Circle:
			{
				GraphicsPath graphicsPath2 = new GraphicsPath();
				graphicsPath2.AddEllipse(this.method_0());
				result = graphicsPath2;
				break;
			}
			case ShapeTypes.Diamond:
				result = ShapeDrawer.CreateDiamondPath(this.rectangleF_0);
				break;
			case ShapeTypes.TriangleUp:
				result = ShapeDrawer.CreateTrianglePath(this.rectangleF_0, 0);
				break;
			case ShapeTypes.TriangleRight:
				result = ShapeDrawer.CreateTrianglePath(this.rectangleF_0, 1);
				break;
			case ShapeTypes.TriangleDown:
				result = ShapeDrawer.CreateTrianglePath(this.rectangleF_0, 2);
				break;
			case ShapeTypes.TriangleLeft:
				result = ShapeDrawer.CreateTrianglePath(this.rectangleF_0, 3);
				break;
			case ShapeTypes.Cross:
			{
				GraphicsPath graphicsPath3 = new GraphicsPath();
				graphicsPath3.AddLine(this.rectangleF_0.Left, this.rectangleF_0.Top + this.rectangleF_0.Height / 2f, this.rectangleF_0.Right, this.rectangleF_0.Top + this.rectangleF_0.Height / 2f);
				graphicsPath3.AddLine(this.rectangleF_0.Left + this.rectangleF_0.Width / 2f, this.rectangleF_0.Top, this.rectangleF_0.Left + this.rectangleF_0.Width / 2f, this.rectangleF_0.Bottom);
				result = graphicsPath3;
				break;
			}
			case ShapeTypes.XCross:
			{
				GraphicsPath graphicsPath4 = new GraphicsPath();
				graphicsPath4.AddLine(this.rectangleF_0.Left, this.rectangleF_0.Top, this.rectangleF_0.Right, this.rectangleF_0.Bottom);
				graphicsPath4.AddLine(this.rectangleF_0.Right, this.rectangleF_0.Top, this.rectangleF_0.Left, this.rectangleF_0.Bottom);
				result = graphicsPath4;
				break;
			}
			case ShapeTypes.CircleCross:
			{
				RectangleF rect = this.method_0();
				GraphicsPath graphicsPath5 = new GraphicsPath();
				graphicsPath5.AddLine(rect.Left, rect.Top + rect.Height / 2f, rect.Right, rect.Top + rect.Height / 2f);
				graphicsPath5.AddLine(rect.Left + rect.Width / 2f, rect.Top, rect.Left + rect.Width / 2f, rect.Bottom);
				graphicsPath5.AddEllipse(rect);
				result = graphicsPath5;
				break;
			}
			case ShapeTypes.CircleXCross:
			{
				RectangleF rect2 = this.method_0();
				float num = rect2.Width / 2f;
				float num2 = (float)((int)((double)num * Math.Sin(0.78539816339744828)));
				GraphicsPath graphicsPath6 = new GraphicsPath();
				float num3 = this.rectangleF_0.Left + this.rectangleF_0.Width / 2f;
				float num4 = this.rectangleF_0.Top + this.rectangleF_0.Height / 2f;
				graphicsPath6.AddLine(num3 - num2, num4 - num2, num3 + num2, num4 + num2);
				graphicsPath6.AddLine(num3 + num2, num4 - num2, num3 - num2, num4 + num2);
				graphicsPath6.AddEllipse(rect2);
				result = graphicsPath6;
				break;
			}
			case ShapeTypes.Default:
				result = ShapeDrawer.CreateRoundRectanglePath(this.rectangleF_0, this.float_0);
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		private RectangleF method_0()
		{
			RectangleF result;
			if (this.rectangleF_0.Width > this.rectangleF_0.Height)
			{
				result = new RectangleF(this.rectangleF_0.Left + (this.rectangleF_0.Width - this.rectangleF_0.Height) / 2f, this.rectangleF_0.Top, this.rectangleF_0.Height, this.rectangleF_0.Height);
			}
			else
			{
				result = new RectangleF(this.rectangleF_0.Left, this.rectangleF_0.Top + (this.rectangleF_0.Height - this.rectangleF_0.Width) / 2f, this.rectangleF_0.Width, this.rectangleF_0.Width);
			}
			return result;
		}

		public void DrawBorder(Graphics graphics_0)
		{
			if (this.pen_0 != null && graphics_0 != null)
			{
				switch (this.shapeTypes_0)
				{
				case ShapeTypes.Cross:
					graphics_0.DrawLine(this.pen_0, this.rectangleF_0.Left, this.rectangleF_0.Top + this.rectangleF_0.Height / 2f, this.rectangleF_0.Right, this.rectangleF_0.Top + this.rectangleF_0.Height / 2f);
					graphics_0.DrawLine(this.pen_0, this.rectangleF_0.Left + this.rectangleF_0.Width / 2f, this.rectangleF_0.Top, this.rectangleF_0.Left + this.rectangleF_0.Width / 2f, this.rectangleF_0.Bottom);
					break;
				case ShapeTypes.XCross:
					graphics_0.DrawLine(this.pen_0, this.rectangleF_0.Left, this.rectangleF_0.Top, this.rectangleF_0.Right, this.rectangleF_0.Bottom);
					graphics_0.DrawLine(this.pen_0, this.rectangleF_0.Right, this.rectangleF_0.Top, this.rectangleF_0.Left, this.rectangleF_0.Bottom);
					break;
				case ShapeTypes.CircleCross:
				{
					RectangleF rect = this.method_0();
					graphics_0.DrawLine(this.pen_0, rect.Left, rect.Top + rect.Height / 2f, rect.Right, rect.Top + rect.Height / 2f);
					graphics_0.DrawLine(this.pen_0, rect.Left + rect.Width / 2f, rect.Top, rect.Left + rect.Width / 2f, rect.Bottom);
					graphics_0.DrawEllipse(this.pen_0, rect);
					break;
				}
				case ShapeTypes.CircleXCross:
				{
					RectangleF rect2 = this.method_0();
					float num = rect2.Width / 2f;
					float num2 = (float)((int)((double)num * Math.Sin(0.78539816339744828)));
					new GraphicsPath();
					float num3 = this.rectangleF_0.Left + this.rectangleF_0.Width / 2f;
					float num4 = this.rectangleF_0.Top + this.rectangleF_0.Height / 2f;
					graphics_0.DrawLine(this.pen_0, num3 - num2, num4 - num2, num3 + num2, num4 + num2);
					graphics_0.DrawLine(this.pen_0, num3 + num2, num4 - num2, num3 - num2, num4 + num2);
					graphics_0.DrawEllipse(this.pen_0, rect2);
					break;
				}
				default:
				{
					GraphicsPath graphicsPath = this.CreatePath();
					if (graphicsPath != null)
					{
						graphics_0.DrawPath(this.pen_0, graphicsPath);
						graphicsPath.Dispose();
					}
					break;
				}
				}
			}
		}

		public void Fill(Graphics graphics_0)
		{
			if (this.brush_0 != null && graphics_0 != null)
			{
				switch (this.shapeTypes_0)
				{
				case ShapeTypes.Cross:
				case ShapeTypes.XCross:
					break;
				case ShapeTypes.CircleCross:
				{
					RectangleF rect = this.method_0();
					graphics_0.FillEllipse(this.brush_0, rect);
					break;
				}
				case ShapeTypes.CircleXCross:
				{
					RectangleF rect2 = this.method_0();
					graphics_0.FillEllipse(this.brush_0, rect2);
					break;
				}
				default:
				{
					GraphicsPath graphicsPath = this.CreatePath();
					if (graphicsPath != null)
					{
						graphics_0.FillPath(this.brush_0, graphicsPath);
						graphicsPath.Dispose();
					}
					break;
				}
				}
			}
		}

		public void DrawAndFill(Graphics graphics_0)
		{
			if (graphics_0 != null)
			{
				if (this.brush_0 != null)
				{
					this.Fill(graphics_0);
				}
				if (this.pen_0 != null)
				{
					this.DrawBorder(graphics_0);
				}
			}
		}

		public static GraphicsPath CreateTrianglePath(RectangleF rect, int direction)
		{
			if (direction < 0 || direction > 3)
			{
				throw new ArgumentOutOfRangeException("direction", "0->3");
			}
			GraphicsPath graphicsPath = new GraphicsPath();
			float num = rect.Width / 2f;
			float num2 = rect.Height / 2f;
			if (direction == 0)
			{
				graphicsPath.AddLine(rect.Left + num, rect.Top, rect.Right, rect.Bottom);
				graphicsPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Bottom);
				graphicsPath.AddLine(rect.Left, rect.Bottom, rect.Left + num, rect.Top);
			}
			else if (direction == 1)
			{
				graphicsPath.AddLine(rect.Left, rect.Top, rect.Right, rect.Top + num2);
				graphicsPath.AddLine(rect.Right, rect.Top + num2, rect.Left, rect.Bottom);
				graphicsPath.AddLine(rect.Left, rect.Bottom, rect.Left, rect.Top);
			}
			else if (direction == 2)
			{
				graphicsPath.AddLine(rect.Left, rect.Top, rect.Right, rect.Top);
				graphicsPath.AddLine(rect.Right, rect.Top, rect.Left + num, rect.Bottom);
				graphicsPath.AddLine(rect.Left + num, rect.Bottom, rect.Left, rect.Top);
			}
			else if (direction == 3)
			{
				graphicsPath.AddLine(rect.Left, rect.Top + num2, rect.Right, rect.Top);
				graphicsPath.AddLine(rect.Right, rect.Top, rect.Right, rect.Bottom);
				graphicsPath.AddLine(rect.Right, rect.Bottom, rect.Left, rect.Top + num2);
			}
			return graphicsPath;
		}

		public static GraphicsPath CreateDiamondPath(RectangleF rect)
		{
			float num = rect.Width / 2f;
			float num2 = rect.Height / 2f;
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddLine(rect.Left + num, rect.Top, rect.Right, rect.Top + num2);
			graphicsPath.AddLine(rect.Right, rect.Top + num2, rect.Left + num, rect.Bottom);
			graphicsPath.AddLine(rect.Left + num, rect.Bottom, rect.Left, rect.Top + num2);
			graphicsPath.AddLine(rect.Left, rect.Top + num2, rect.Left + num, rect.Top);
			return graphicsPath;
		}

		public static GraphicsPath CreateRoundRectanglePath(RectangleF rect, float radio)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			GraphicsPath result;
			if (radio <= 0f)
			{
				graphicsPath.AddRectangle(rect);
				result = graphicsPath;
			}
			else
			{
				graphicsPath.AddArc(rect.Left, rect.Top, radio, radio, 270f, -90f);
				graphicsPath.AddLine(rect.Left, rect.Top + radio / 2f, rect.Left, rect.Bottom - radio / 2f);
				graphicsPath.AddArc(rect.Left, rect.Bottom - radio, radio, radio, 180f, -90f);
				graphicsPath.AddLine(rect.Left + radio / 2f, rect.Bottom, rect.Right - radio / 2f, rect.Bottom);
				graphicsPath.AddArc(rect.Right - radio, rect.Bottom - radio, radio, radio, 90f, -90f);
				graphicsPath.AddLine(rect.Right, rect.Bottom - radio / 2f, rect.Right, rect.Top + radio / 2f);
				graphicsPath.AddArc(rect.Right - radio, rect.Top, radio, radio, 0f, -90f);
				graphicsPath.AddLine(rect.Right - radio / 2f, rect.Top, rect.Left + radio / 2f, rect.Top);
				graphicsPath.CloseAllFigures();
				result = graphicsPath;
			}
			return result;
		}

		public static GraphicsPath CreateRoundRectanglePath(Rectangle rect, int radio)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			GraphicsPath result;
			if (radio <= 0)
			{
				graphicsPath.AddRectangle(rect);
				result = graphicsPath;
			}
			else
			{
				graphicsPath.AddArc(rect.Left, rect.Top, radio, radio, 270f, -90f);
				graphicsPath.AddLine(rect.Left, rect.Top + radio / 2, rect.Left, rect.Bottom - radio / 2);
				graphicsPath.AddArc(rect.Left, rect.Bottom - radio, radio, radio, 180f, -90f);
				graphicsPath.AddLine(rect.Left + radio / 2, rect.Bottom, rect.Right - radio / 2, rect.Bottom);
				graphicsPath.AddArc(rect.Right - radio, rect.Bottom - radio, radio, radio, 90f, -90f);
				graphicsPath.AddLine(rect.Right, rect.Bottom - radio / 2, rect.Right, rect.Top + radio / 2);
				graphicsPath.AddArc(rect.Right - radio, rect.Top, radio, radio, 0f, -90f);
				graphicsPath.AddLine(rect.Right - radio / 2, rect.Top, rect.Left + radio / 2, rect.Top);
				graphicsPath.CloseAllFigures();
				result = graphicsPath;
			}
			return result;
		}
	}
    public enum ShapeTypes
    {
        Rectangle,
        Square,
        Ellipse,
        Circle,
        Diamond,
        TriangleUp,
        TriangleRight,
        TriangleDown,
        TriangleLeft,
        Cross,
        XCross,
        CircleCross,
        CircleXCross,
        Default,
        None
    }
}
