
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ASwartz.WinForms.Controls
{
	[DefaultEvent("ItemClick"), ToolboxItem(true), ToolboxBitmap(typeof(ASCardListViewControl)), ComVisible(false)]
	public class ASCardListViewControl : UserControl
	{
		
		public static class GClass20
		{
			private class Class159
			{
				public Image image_0 = null;

				public int int_0 = 0;

				public int int_1 = 0;

				public FrameDimension frameDimension_0 = null;
			}

			private static List<ASCardListViewControl.GClass20.Class159> list_0 = new List<ASCardListViewControl.GClass20.Class159>();

			public static void smethod_0()
			{
				ASCardListViewControl.GClass20.list_0.Clear();
			}

			public static bool smethod_1(Image image_0)
			{
				if (image_0 == null)
				{
					throw new ArgumentNullException("img");
				}
				bool result;
				foreach (ASCardListViewControl.GClass20.Class159 current in ASCardListViewControl.GClass20.list_0)
				{
					if (current.image_0 == image_0)
					{
						ASCardListViewControl.GClass20.list_0.Remove(current);
						result = true;
						return result;
					}
				}
				result = false;
				return result;
			}

			public static bool smethod_2(Image image_0)
			{
				if (image_0 == null)
				{
					throw new ArgumentNullException("img");
				}
				bool result;
				foreach (ASCardListViewControl.GClass20.Class159 current in ASCardListViewControl.GClass20.list_0)
				{
					if (current.image_0 == image_0)
					{
						current.int_1++;
						current.image_0.SelectActiveFrame(current.frameDimension_0, current.int_1 % current.int_0);
						result = true;
						return result;
					}
				}
				result = false;
				return result;
			}

			public static bool smethod_3(Image image_0)
			{
				if (image_0 == null)
				{
					throw new ArgumentNullException("img");
				}
				bool result;
				foreach (ASCardListViewControl.GClass20.Class159 CIns in ASCardListViewControl.GClass20.list_0)
				{
                    if (CIns.image_0 == image_0)
					{
						result = true;
						return result;
					}
				}
				if (ImageAnimator.CanAnimate(image_0))
				{
                    ASCardListViewControl.GClass20.Class159 CIns = new ASCardListViewControl.GClass20.Class159();
                    CIns.image_0 = image_0;
                    CIns.frameDimension_0 = FrameDimension.Time;
                    CIns.int_0 = image_0.GetFrameCount(CIns.frameDimension_0);
                    ASCardListViewControl.GClass20.list_0.Add(CIns);
					result = true;
				}
				else
				{
					result = false;
				}
				return result;
			}
		}

		private System.Windows.Forms.Timer timer_0 = null;

		private int int_0 = 500;

		private ImageList imageList_0 = null;

		private int int_1 = 100;

		private int int_2 = 100;

		private int int_3 = 5;

		private int int_4 = 10;

		private Color color_0 = Color.White;

		private Color color_1 = SystemColors.Highlight;

		private Color color_2 = Color.Black;

		private int int_5 = 1;

		private Image image_0 = null;

		private int int_6 = 5;

		private bool bool_0 = true;

		private object object_0 = null;

		private string string_0 = null;

		private int int_7 = 0;

		private int int_8 = 0;

		private bool bool_1 = true;

		private int int_9 = 0;

		private System.Windows.Forms.Timer timer_1 = null;

		private int int_10 = 400;

		private Dictionary<Image, bool> dictionary_0 = new Dictionary<Image, bool>();

		private ASCardListViewItemCollection ASCardListViewItemCollection_0 = new ASCardListViewItemCollection();

		private ASCardListViewPaintItemEventHandler ASCardListViewPaintItemEventHandler_0 = null;

		private ASCardListViewItem ASCardListViewItem_0 = null;

		private ASCardListViewEventHandler ASCardListViewEventHandler_0 = null;

		private ASCardListViewEventHandler ASCardListViewEventHandler_1 = null;

		private ASCardListViewItem ASCardListViewItem_1 = null;

		private ASCardListViewDragEventHandler ASCardListViewDragEventHandler_0 = null;

		private ASCardListViewDragEventHandler ASCardListViewDragEventHandler_1 = null;

		private ASCardContentItemList ASCardContentItemList_0 = new ASCardContentItemList();

		private ASCardListViewItemCollection ASCardListViewItemCollection_1 = new ASCardListViewItemCollection();

		private ToolTip toolTip_0 = null;

		private bool bool_2 = true;

		private int int_11 = 100;

		private int int_12 = 100;

		private ASCardContentItemList ASCardContentItemList_1 = new ASCardContentItemList();

		public event ASCardListViewPaintItemEventHandler PaintCardItem
		{
			add
			{
				ASCardListViewPaintItemEventHandler ASCardListViewPaintItemEventHandler = this.ASCardListViewPaintItemEventHandler_0;
				ASCardListViewPaintItemEventHandler ASCardListViewPaintItemEventHandler2;
				do
				{
					ASCardListViewPaintItemEventHandler2 = ASCardListViewPaintItemEventHandler;
					ASCardListViewPaintItemEventHandler value2 = (ASCardListViewPaintItemEventHandler)Delegate.Combine(ASCardListViewPaintItemEventHandler2, value);
					ASCardListViewPaintItemEventHandler = Interlocked.CompareExchange<ASCardListViewPaintItemEventHandler>(ref this.ASCardListViewPaintItemEventHandler_0, value2, ASCardListViewPaintItemEventHandler2);
				}
				while (ASCardListViewPaintItemEventHandler != ASCardListViewPaintItemEventHandler2);
			}
			remove
			{
				ASCardListViewPaintItemEventHandler ASCardListViewPaintItemEventHandler = this.ASCardListViewPaintItemEventHandler_0;
				ASCardListViewPaintItemEventHandler ASCardListViewPaintItemEventHandler2;
				do
				{
					ASCardListViewPaintItemEventHandler2 = ASCardListViewPaintItemEventHandler;
					ASCardListViewPaintItemEventHandler value2 = (ASCardListViewPaintItemEventHandler)Delegate.Remove(ASCardListViewPaintItemEventHandler2, value);
					ASCardListViewPaintItemEventHandler = Interlocked.CompareExchange<ASCardListViewPaintItemEventHandler>(ref this.ASCardListViewPaintItemEventHandler_0, value2, ASCardListViewPaintItemEventHandler2);
				}
				while (ASCardListViewPaintItemEventHandler != ASCardListViewPaintItemEventHandler2);
			}
		}

		public event ASCardListViewEventHandler ItemClick
		{
			add
			{
				ASCardListViewEventHandler ASCardListViewEventHandler = this.ASCardListViewEventHandler_0;
				ASCardListViewEventHandler ASCardListViewEventHandler2;
				do
				{
					ASCardListViewEventHandler2 = ASCardListViewEventHandler;
					ASCardListViewEventHandler value2 = (ASCardListViewEventHandler)Delegate.Combine(ASCardListViewEventHandler2, value);
					ASCardListViewEventHandler = Interlocked.CompareExchange<ASCardListViewEventHandler>(ref this.ASCardListViewEventHandler_0, value2, ASCardListViewEventHandler2);
				}
				while (ASCardListViewEventHandler != ASCardListViewEventHandler2);
			}
			remove
			{
				ASCardListViewEventHandler ASCardListViewEventHandler = this.ASCardListViewEventHandler_0;
				ASCardListViewEventHandler ASCardListViewEventHandler2;
				do
				{
					ASCardListViewEventHandler2 = ASCardListViewEventHandler;
					ASCardListViewEventHandler value2 = (ASCardListViewEventHandler)Delegate.Remove(ASCardListViewEventHandler2, value);
					ASCardListViewEventHandler = Interlocked.CompareExchange<ASCardListViewEventHandler>(ref this.ASCardListViewEventHandler_0, value2, ASCardListViewEventHandler2);
				}
				while (ASCardListViewEventHandler != ASCardListViewEventHandler2);
			}
		}

		public event ASCardListViewEventHandler ItemDoubleClick
		{
			add
			{
				ASCardListViewEventHandler ASCardListViewEventHandler = this.ASCardListViewEventHandler_1;
				ASCardListViewEventHandler ASCardListViewEventHandler2;
				do
				{
					ASCardListViewEventHandler2 = ASCardListViewEventHandler;
					ASCardListViewEventHandler value2 = (ASCardListViewEventHandler)Delegate.Combine(ASCardListViewEventHandler2, value);
					ASCardListViewEventHandler = Interlocked.CompareExchange<ASCardListViewEventHandler>(ref this.ASCardListViewEventHandler_1, value2, ASCardListViewEventHandler2);
				}
				while (ASCardListViewEventHandler != ASCardListViewEventHandler2);
			}
			remove
			{
				ASCardListViewEventHandler ASCardListViewEventHandler = this.ASCardListViewEventHandler_1;
				ASCardListViewEventHandler ASCardListViewEventHandler2;
				do
				{
					ASCardListViewEventHandler2 = ASCardListViewEventHandler;
					ASCardListViewEventHandler value2 = (ASCardListViewEventHandler)Delegate.Remove(ASCardListViewEventHandler2, value);
					ASCardListViewEventHandler = Interlocked.CompareExchange<ASCardListViewEventHandler>(ref this.ASCardListViewEventHandler_1, value2, ASCardListViewEventHandler2);
				}
				while (ASCardListViewEventHandler != ASCardListViewEventHandler2);
			}
		}

		public event ASCardListViewDragEventHandler EventDetectDragItem
		{
			add
			{
				ASCardListViewDragEventHandler ASCardListViewDragEventHandler = this.ASCardListViewDragEventHandler_0;
				ASCardListViewDragEventHandler ASCardListViewDragEventHandler2;
				do
				{
					ASCardListViewDragEventHandler2 = ASCardListViewDragEventHandler;
					ASCardListViewDragEventHandler value2 = (ASCardListViewDragEventHandler)Delegate.Combine(ASCardListViewDragEventHandler2, value);
					ASCardListViewDragEventHandler = Interlocked.CompareExchange<ASCardListViewDragEventHandler>(ref this.ASCardListViewDragEventHandler_0, value2, ASCardListViewDragEventHandler2);
				}
				while (ASCardListViewDragEventHandler != ASCardListViewDragEventHandler2);
			}
			remove
			{
				ASCardListViewDragEventHandler ASCardListViewDragEventHandler = this.ASCardListViewDragEventHandler_0;
				ASCardListViewDragEventHandler ASCardListViewDragEventHandler2;
				do
				{
					ASCardListViewDragEventHandler2 = ASCardListViewDragEventHandler;
					ASCardListViewDragEventHandler value2 = (ASCardListViewDragEventHandler)Delegate.Remove(ASCardListViewDragEventHandler2, value);
					ASCardListViewDragEventHandler = Interlocked.CompareExchange<ASCardListViewDragEventHandler>(ref this.ASCardListViewDragEventHandler_0, value2, ASCardListViewDragEventHandler2);
				}
				while (ASCardListViewDragEventHandler != ASCardListViewDragEventHandler2);
			}
		}

		public event ASCardListViewDragEventHandler EventDragDropItem
		{
			add
			{
				ASCardListViewDragEventHandler ASCardListViewDragEventHandler = this.ASCardListViewDragEventHandler_1;
				ASCardListViewDragEventHandler ASCardListViewDragEventHandler2;
				do
				{
					ASCardListViewDragEventHandler2 = ASCardListViewDragEventHandler;
					ASCardListViewDragEventHandler value2 = (ASCardListViewDragEventHandler)Delegate.Combine(ASCardListViewDragEventHandler2, value);
					ASCardListViewDragEventHandler = Interlocked.CompareExchange<ASCardListViewDragEventHandler>(ref this.ASCardListViewDragEventHandler_1, value2, ASCardListViewDragEventHandler2);
				}
				while (ASCardListViewDragEventHandler != ASCardListViewDragEventHandler2);
			}
			remove
			{
				ASCardListViewDragEventHandler ASCardListViewDragEventHandler = this.ASCardListViewDragEventHandler_1;
				ASCardListViewDragEventHandler ASCardListViewDragEventHandler2;
				do
				{
					ASCardListViewDragEventHandler2 = ASCardListViewDragEventHandler;
					ASCardListViewDragEventHandler value2 = (ASCardListViewDragEventHandler)Delegate.Remove(ASCardListViewDragEventHandler2, value);
					ASCardListViewDragEventHandler = Interlocked.CompareExchange<ASCardListViewDragEventHandler>(ref this.ASCardListViewDragEventHandler_1, value2, ASCardListViewDragEventHandler2);
				}
				while (ASCardListViewDragEventHandler != ASCardListViewDragEventHandler2);
			}
		}

		[Category("Behavior"), DefaultValue(500)]
		public int BlinkTimerInterval
		{
			get
			{
				return this.int_0;
			}
			set
			{
				this.int_0 = value;
				if (this.timer_0 != null)
				{
					this.timer_0.Interval = value;
				}
			}
		}

		[Category("Appearance"), DefaultValue(null)]
		public ImageList ImageList
		{
			get
			{
				return this.imageList_0;
			}
			set
			{
				this.imageList_0 = value;
			}
		}

		[Category("Layout"), DefaultValue(100)]
		public int CardWidth
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

		[Category("Layout"), DefaultValue(100)]
		public int CardHeight
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

		[Category("Layout"), DefaultValue(5)]
		public int CardSpacing
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

		[Category("Layout"), DefaultValue(10)]
		public int CardLineSpacing
		{
			get
			{
				return this.int_4;
			}
			set
			{
				this.int_4 = value;
			}
		}

		[Category("Appearance"), DefaultValue(typeof(Color), "White")]
		public Color CardBackColor
		{
			get
			{
				return this.color_0;
			}
			set
			{
				if (this.color_0 != value)
				{
					this.color_0 = value;
					if (base.IsHandleCreated)
					{
						base.Invalidate();
					}
				}
			}
		}

		[Category("Appearance"), DefaultValue(typeof(Color), "Highlight")]
		public Color SelectedCardBackColor
		{
			get
			{
				return this.color_1;
			}
			set
			{
				if (this.color_1 != value)
				{
					this.color_1 = value;
					if (base.IsHandleCreated)
					{
						base.Invalidate();
					}
				}
			}
		}

		[Category("Appearance"), DefaultValue(typeof(Color), "Black")]
		public Color CardBorderColor
		{
			get
			{
				return this.color_2;
			}
			set
			{
				if (this.color_2 != value)
				{
					this.color_2 = value;
					if (base.IsHandleCreated)
					{
						base.Invalidate();
					}
				}
			}
		}

		[Category("Appearance"), DefaultValue(1)]
		public int CardBorderWith
		{
			get
			{
				return this.int_5;
			}
			set
			{
				if (this.int_5 != value)
				{
					this.int_5 = value;
					if (base.IsHandleCreated)
					{
						base.Invalidate();
					}
				}
			}
		}

		[Bindable(true), Category("Appearance"), DefaultValue(null), Localizable(true)]
		public Image CardBackgroundImage
		{
			get
			{
				return this.image_0;
			}
			set
			{
				if (this.image_0 != value)
				{
					this.image_0 = value;
					if (base.IsHandleCreated)
					{
						base.Invalidate();
					}
				}
			}
		}

		[Category("Appearance"), DefaultValue(5), Localizable(false)]
		public int CardRoundRadio
		{
			get
			{
				return this.int_6;
			}
			set
			{
				if (this.int_6 != value)
				{
					this.int_6 = value;
					if (base.IsHandleCreated)
					{
						base.Invalidate();
					}
				}
			}
		}

		[Category("Appearance"), DefaultValue(true), Localizable(false)]
		public bool ShowCardShade
		{
			get
			{
				return this.bool_0;
			}
			set
			{
				if (this.bool_0 != value)
				{
					this.bool_0 = value;
					if (base.IsHandleCreated)
					{
						base.Invalidate();
					}
				}
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object DataSource
		{
			get
			{
				return this.object_0;
			}
			set
			{
				this.object_0 = value;
				if (base.IsHandleCreated)
				{
					this.RefreshDataSource();
				}
			}
		}

		[Category("Data"), DefaultValue(null)]
		public string ItemTooltipDataFieldName
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

		[Browsable(false)]
		public int ViewWidth
		{
			get
			{
				return this.int_7;
			}
		}

		[Browsable(false)]
		public int ViewHeight
		{
			get
			{
				return this.int_8;
			}
		}

		[Category("Layout"), DefaultValue(true)]
		public bool JustifySpacing
		{
			get
			{
				return this.bool_1;
			}
			set
			{
				this.bool_1 = value;
			}
		}

		[Category("Behavior"), DefaultValue(400)]
		public int ImageAnimateInterval
		{
			get
			{
				return this.int_10;
			}
			set
			{
				this.int_10 = value;
				if (this.timer_1 != null)
				{
					this.timer_1.Interval = value;
				}
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ASCardListViewItem MouseHoverItem
		{
			get
			{
				return this.ASCardListViewItem_0;
			}
		}

		[Category("Data")]
		public ASCardContentItemList CardTemplate
		{
			get
			{
				return this.ASCardContentItemList_0;
			}
			set
			{
				this.ASCardContentItemList_0 = value;
			}
		}

		[Category("Data")]
		public ASCardListViewItemCollection Items
		{
			get
			{
				return this.ASCardListViewItemCollection_1;
			}
			set
			{
				this.ASCardListViewItemCollection_1 = value;
			}
		}

		[Category("Behavior"), DefaultValue(true)]
		public bool EnableSupperTooltip
		{
			get
			{
				return this.bool_2;
			}
			set
			{
				this.bool_2 = value;
			}
		}

		[Category("Layout"), DefaultValue(100)]
		public int TooltipWidth
		{
			get
			{
				return this.int_11;
			}
			set
			{
				this.int_11 = value;
			}
		}

		[Category("Layout"), DefaultValue(100)]
		public int TooltipHeight
		{
			get
			{
				return this.int_12;
			}
			set
			{
				this.int_12 = value;
			}
		}

		[Category("Data")]
		public ASCardContentItemList TooltipContentItems
		{
			get
			{
				if (this.ASCardContentItemList_1 == null)
				{
					this.ASCardContentItemList_1 = new ASCardContentItemList();
				}
				return this.ASCardContentItemList_1;
			}
			set
			{
				this.ASCardContentItemList_1 = value;
			}
		}

		public ASCardListViewControl()
		{
			this.AutoScroll = true;
			this.DoubleBuffered = true;
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.toolTip_0 != null)
			{
				this.toolTip_0.Dispose();
				this.toolTip_0 = null;
			}
			if (this.timer_0 != null)
			{
				this.timer_0.Dispose();
				this.timer_0 = null;
			}
			if (this.timer_1 != null)
			{
				this.timer_1.Dispose();
				this.timer_1 = null;
			}
		}

		protected override void OnLoad(EventArgs eventArgs_0)
		{
			this.timer_0 = new System.Windows.Forms.Timer();
			this.timer_0.Interval = this.int_0;
			this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
			this.timer_0.Start();
			this.timer_1 = new System.Windows.Forms.Timer();
			this.timer_1.Interval = this.int_10;
			this.timer_1.Tick += new EventHandler(this.timer_1_Tick);
			this.timer_1.Start();
			if (!base.DesignMode)
			{
				this.method_4();
			}
			base.OnLoad(eventArgs_0);
		}

		private void timer_0_Tick(object sender, EventArgs e)
		{
			foreach (ASCardListViewItem current in this.Items)
			{
				if (current.Blink)
				{
					current.bool_2 = !current.bool_2;
					this.RePaintItem(current);
				}
			}
		}

		public void Clear()
		{
			this.ASCardListViewItemCollection_1.Clear();
			this.method_0();
			this.ASCardListViewItem_1 = null;
			this.ASCardListViewItem_0 = null;
			this.int_8 = 0;
			this.int_7 = 0;
		}

		public void RefreshDataSource()
		{
			if (base.IsHandleCreated)
			{
				this.toolTip_0.Hide(this);
			}
			this.Clear();
			if (this.object_0 != null)
			{
				ASDataSource ASDataSource = new ASDataSource();
				ASDataSource.DataSource = this.DataSource;
				foreach (ASCardContentItem current in this.CardTemplate)
				{
					if (ASDataSource.Fields[current.DataField] == null)
					{
						ASDataSource.Fields.AddField(current.DataField);
					}
				}
				bool flag = false;
				if (!string.IsNullOrEmpty(this.ItemTooltipDataFieldName))
				{
					ASDataSource.Fields.AddField(this.ItemTooltipDataFieldName);
					flag = true;
				}
				if (this.TooltipContentItems != null && this.TooltipContentItems.Count > 0)
				{
					foreach (ASCardContentItem current in this.TooltipContentItems)
					{
						if (ASDataSource.Fields[current.DataField] == null)
						{
							ASDataSource.Fields.AddField(current.DataField);
						}
					}
				}
				ASDataSource.Reset();
				while (ASDataSource.MoveNext())
				{
					ASCardListViewItem ASCardListViewItem = new ASCardListViewItem();
					ASCardListViewItem.object_1 = ASDataSource.Current;
					this.Items.Add(ASCardListViewItem);
					ASCardListViewItem.Values = new Dictionary<ASCardContentItem, object>();
					foreach (ASCardContentItem current2 in this.CardTemplate)
					{
						object value = ASDataSource.ReadValue(current2.DataField);
						ASCardListViewItem.Values[current2] = value;
					}
					if (this.ASCardContentItemList_1 != null && this.ASCardContentItemList_1.Count > 0)
					{
						ASCardListViewItem.TooltipValues = new Dictionary<ASCardContentItem, object>();
						foreach (ASCardContentItem current2 in this.ASCardContentItemList_1)
						{
							object value = ASDataSource.ReadValue(current2.DataField);
							ASCardListViewItem.TooltipValues[current2] = value;
						}
					}
					if (flag)
					{
						ASCardListViewItem.ToolTip = Convert.ToString(ASDataSource.ReadValue(this.ItemTooltipDataFieldName));
					}
				}
				this.ExecuteLayout();
				base.Invalidate();
			}
		}

		protected override void OnResize(EventArgs eventArgs_0)
		{
			base.OnResize(eventArgs_0);
			this.ExecuteLayout();
			base.Invalidate();
		}

		public void ExecuteLayout()
		{
			if (this.CardWidth > 0 && this.CardHeight > 0)
			{
				this.int_8 = 0;
				this.int_7 = 0;
				int width = base.ClientSize.Width;
				int num = (int)Math.Floor((double)(width - this.CardSpacing) * 1.0 / (double)(this.CardWidth + this.CardSpacing));
				if (num < 1)
				{
					num = 1;
				}
				int num2 = this.CardLineSpacing;
				int num3 = (width - (this.CardWidth + this.CardSpacing) * num + this.CardSpacing) / 2;
				if (this.JustifySpacing)
				{
					num2 = (width - this.CardWidth * num) / (num + 1);
					num2 = Math.Max(num2, this.CardSpacing);
					num3 = num2;
				}
				num3 = Math.Max(num3, this.CardSpacing);
				int num4 = num3;
				int num5 = this.CardLineSpacing;
				int num6 = 0;
				int num7 = 0;
				for (int i = 0; i < this.Items.Count; i++)
				{
					ASCardListViewItem ASCardListViewItem = this.Items[i];
					ASCardListViewItem.int_1 = i;
					ASCardListViewItem.ASCardListViewControl_0 = this;
					if (i > 0 && (ASCardListViewItem.AutoLine || num4 + this.CardWidth + num2 > width))
					{
						num4 = num3;
						num5 = num5 + this.CardHeight + this.CardLineSpacing;
						num6++;
						num7 = 0;
					}
					ASCardListViewItem.int_2 = num6;
					ASCardListViewItem.int_3 = num7;
					ASCardListViewItem.Left = num4;
					ASCardListViewItem.Top = num5;
					ASCardListViewItem.Width = this.CardWidth;
					ASCardListViewItem.Height = this.CardHeight;
					this.int_7 = Math.Max(this.int_7, ASCardListViewItem.Left + ASCardListViewItem.Width);
					this.int_8 = Math.Max(this.int_8, ASCardListViewItem.Top + ASCardListViewItem.Height);
					num7++;
					num4 = num4 + this.CardWidth + num2;
				}
				this.int_9 = width;
				this.int_7 += num2;
				this.int_8 += this.CardLineSpacing;
				Size size = new Size(this.int_7, this.int_8);
				if (base.AutoScrollMinSize != size)
				{
					base.AutoScrollMinSize = new Size(size.Width + 1, size.Height + 1);
					base.AutoScrollMinSize = size;
				}
			}
		}

		public Rectangle GetItemClientBounds(ASCardListViewItem item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			Point autoScrollPosition = base.AutoScrollPosition;
			return new Rectangle(item.Left + autoScrollPosition.X, item.Top + autoScrollPosition.Y, item.Width, item.Height);
		}

		public ASCardListViewItem GetItemAt(int int_13, int int_14)
		{
			Point autoScrollPosition = base.AutoScrollPosition;
			int_13 -= autoScrollPosition.X;
			int_14 -= autoScrollPosition.Y;
			ASCardListViewItem result;
			foreach (ASCardListViewItem current in this.Items)
			{
				Rectangle rectangle = new Rectangle(current.Left, current.Top, current.Width, current.Height);
				if (rectangle.Contains(int_13, int_14))
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public void InvalidateItem(ASCardListViewItem item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (base.IsHandleCreated)
			{
				Rectangle itemClientBounds = this.GetItemClientBounds(item);
				itemClientBounds.Inflate(6, 6);
				base.Invalidate(itemClientBounds);
			}
		}

		public void InvalidateItem(int itemIndex)
		{
			if (itemIndex >= 0 && itemIndex < this.Items.Count)
			{
				this.InvalidateItem(this.Items[itemIndex]);
			}
		}

		public void RePaintItem(ASCardListViewItem item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			Rectangle itemClientBounds = this.GetItemClientBounds(item);
			if (item.Pushed)
			{
				itemClientBounds.Offset(2, 2);
			}
			if (base.ClientRectangle.IntersectsWith(itemClientBounds))
			{
				base.Invalidate(itemClientBounds);
			}
		}

		public void RePaintItems(ASCardListViewItemCollection items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			using (base.CreateGraphics())
			{
				foreach (ASCardListViewItem current in items)
				{
					Rectangle itemClientBounds = this.GetItemClientBounds(current);
					if (current.Pushed)
					{
						itemClientBounds.Offset(2, 2);
					}
					if (base.ClientRectangle.IntersectsWith(itemClientBounds))
					{
						base.Invalidate(itemClientBounds);
					}
				}
			}
		}

		private void method_0()
		{
			if (this.dictionary_0.Count > 0)
			{
				foreach (Image current in this.dictionary_0.Keys)
				{
					ASCardListViewControl.GClass20.smethod_1(current);
				}
				this.dictionary_0.Clear();
			}
			this.ASCardListViewItemCollection_0.Clear();
		}

		private void timer_1_Tick(object sender, EventArgs e)
		{
			if (this.dictionary_0.Count > 0 && this.ASCardListViewItemCollection_0.Count > 0)
			{
				foreach (Image current in this.dictionary_0.Keys)
				{
					if (this.dictionary_0[current])
					{
						ASCardListViewControl.GClass20.smethod_2(current);
					}
				}
				this.RePaintItems(this.ASCardListViewItemCollection_0);
			}
		}

		internal void method_1(Image image_1, ASCardListViewItem ASCardListViewItem_2)
		{
			if (image_1 != null)
			{
				bool flag;
				if (!this.dictionary_0.ContainsKey(image_1))
				{
					flag = ImageAnimator.CanAnimate(image_1);
					this.dictionary_0[image_1] = flag;
					if (flag)
					{
						ASCardListViewControl.GClass20.smethod_3(image_1);
					}
				}
				else
				{
					flag = this.dictionary_0[image_1];
				}
				if (flag && !this.ASCardListViewItemCollection_0.Contains(ASCardListViewItem_2))
				{
					this.ASCardListViewItemCollection_0.Add(ASCardListViewItem_2);
				}
			}
		}

        protected virtual void OnPaintCardItem(ASCardListViewPaintItemEventArgs args)
        {
            GraphicsPath graphicsPath = null;
            Rectangle itemBounds = args.ItemBounds;
            Rectangle rect = Rectangle.Intersect(itemBounds, args.ClipRectangle);
            if (CardRoundRadio > 0)
            {
                graphicsPath = ShapeDrawer.CreateRoundRectanglePath(new RectangleF((float)itemBounds.Left, (float)itemBounds.Top, (float)itemBounds.Width, (float)itemBounds.Height), (float)CardRoundRadio);
            }
            if (ShowCardShade && !args.Item.Pushed)
            {
                args.Graphics.TranslateTransform(3f, 3f);
                if (graphicsPath == null)
                {
                    args.Graphics.FillRectangle(Brushes.Silver, itemBounds);
                }
                else
                {
                    args.Graphics.FillPath(Brushes.Silver, graphicsPath);
                }
                args.Graphics.ResetTransform();
            }
            args.Highlight = args.Item.Highlight;
            if (!args.Highlight && args.Item.Blink)
            {
                args.Highlight = args.Item.bool_2;
            }
            if (args.Highlight)
            {
                if (graphicsPath == null)
                {
                    args.Graphics.FillRectangle(SystemBrushes.Highlight, rect);
                }
                else
                {
                    args.Graphics.FillPath(SystemBrushes.Highlight, graphicsPath);
                }
            }
            else
            {
                Color color = args.Item.BackColor;
                if (color.A == 0)
                {
                    color = CardBackColor;
                }
                if (color.A != 0 && CardBackgroundImage == null)
                {
                    SolidBrush solidBrush = GraphicsObjectBuffer.GetSolidBrush(color);
                    if (args.Item.Selected && SelectedCardBackColor.A != 0)
                    {
                        solidBrush = GraphicsObjectBuffer.GetSolidBrush(SelectedCardBackColor);
                    }
                    if (graphicsPath == null)
                    {
                        args.Graphics.FillRectangle(solidBrush, rect);
                    }
                    else
                    {
                        args.Graphics.FillPath(solidBrush, graphicsPath);
                    }
                }
                if (CardBackgroundImage != null)
                {
                    if (graphicsPath != null)
                    {
                        args.Graphics.SetClip(graphicsPath);
                    }
                    args.Graphics.DrawImage(CardBackgroundImage, itemBounds);
                    if (graphicsPath != null)
                    {
                        args.Graphics.ResetClip();
                    }
                }
            }
            foreach (ASCardContentItem item in CardTemplate)
            {
                ASCardContentItemPaintEventArgs ascardContentItemPaintEventArgs = new ASCardContentItemPaintEventArgs(this, args.Graphics, args.ClipRectangle, args.Item, item);
                ascardContentItemPaintEventArgs.Highlight = args.Highlight;
                ascardContentItemPaintEventArgs.ViewBounds = new Rectangle(item.Left + itemBounds.Left, item.Top + itemBounds.Top, item.Width, item.Height);
                if (args.Item.Values.ContainsKey(item))
                {
                    ascardContentItemPaintEventArgs.Value = args.Item.Values[item];
                }
                item.OnPaint(ascardContentItemPaintEventArgs);
            }
            bool flag;
            if (!(flag = args.Item.HighlightBorder) && args.Item == ASCardListViewItem_1)
            {
                flag = true;
            }
            if (flag)
            {
                using (Pen pen = new Pen(SystemColors.Highlight, 6f))
                {
                    Rectangle rect2 = itemBounds;
                    rect2.Inflate(-3, -3);
                    if (graphicsPath == null)
                    {
                        args.Graphics.DrawRectangle(pen, rect2);
                    }
                    else
                    {
                        using (GraphicsPath path = ShapeDrawer.CreateRoundRectanglePath(rect2, CardRoundRadio))
                        {
                            args.Graphics.DrawPath(pen, path);
                        }
                    }
                }
            }
            else
            {
                Color color = args.Item.BorderColor;
                if (color.A == 0)
                {
                    color = CardBorderColor;
                }
                float num = (float)args.Item.BorderWidth;
                if (num == 0f)
                {
                    num = (float)CardBorderWith;
                }
                if (color.A != 0 && num > 0f)
                {
                    using (Pen pen = new Pen(color, num))
                    {
                        if (graphicsPath == null)
                        {
                            args.Graphics.DrawRectangle(pen, itemBounds);
                        }
                        else
                        {
                            args.Graphics.DrawPath(pen, graphicsPath);
                        }
                    }
                }
            }
            if (ASCardListViewPaintItemEventHandler_0 != null)
            {
                ASCardListViewPaintItemEventHandler_0(this, args);
            }
        }

		protected override void OnPaint(PaintEventArgs pevent)
		{
			base.OnPaint(pevent);
			if (base.DesignMode)
			{
				using (StringFormat stringFormat = new StringFormat())
				{
					stringFormat.Alignment = StringAlignment.Center;
					stringFormat.LineAlignment = StringAlignment.Center;
					string s = string.Concat(new string[]
					{
						base.GetType().FullName,
						":",
						base.Name,
						Environment.NewLine,
						"ASCardList"
					});
					pevent.Graphics.DrawString(s, this.Font, Brushes.Black, new RectangleF(0f, 0f, (float)base.ClientSize.Width, (float)base.ClientSize.Height), stringFormat);
					return;
				}
			}
			pevent.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			int arg_EC_0 = base.AutoScrollPosition.X;
			int arg_FC_0 = base.AutoScrollPosition.Y;
			foreach (ASCardListViewItem current in this.Items)
			{
				Rectangle itemClientBounds = this.GetItemClientBounds(current);
				if (itemClientBounds.Top > pevent.ClipRectangle.Bottom)
				{
					break;
				}
				if (current.Pushed)
				{
					itemClientBounds.Offset(2, 2);
				}
				if (!Rectangle.Intersect(itemClientBounds, pevent.ClipRectangle).IsEmpty)
				{
					ASCardListViewPaintItemEventArgs args = new ASCardListViewPaintItemEventArgs(pevent.Graphics, pevent.ClipRectangle, current, itemClientBounds);
					this.OnPaintCardItem(args);
				}
			}
		}

		private void method_2(ASCardListViewItem ASCardListViewItem_2)
		{
			if (this.ASCardListViewItem_0 != ASCardListViewItem_2)
			{
				if (this.ASCardListViewItem_0 != null && this.ASCardListViewItem_0.Pushed)
				{
					this.ASCardListViewItem_0.Pushed = false;
					this.InvalidateItem(this.ASCardListViewItem_0);
				}
				this.ASCardListViewItem_0 = ASCardListViewItem_2;
				if (this.ASCardListViewItem_0 != null)
				{
					this.InvalidateItem(this.ASCardListViewItem_0);
				}
			}
		}

		protected override void OnMouseDown(MouseEventArgs mevent)
		{
			base.OnMouseDown(mevent);
			if (mevent.Button == MouseButtons.Left)
			{
				ASCardListViewItem itemAt = this.GetItemAt(mevent.X, mevent.Y);
				if (itemAt != null && !itemAt.Pushed)
				{
					itemAt.Pushed = true;
					this.InvalidateItem(itemAt);
				}
			}
		}

		protected override void OnMouseMove(MouseEventArgs mevent)
		{
			base.OnMouseMove(mevent);
			ASCardListViewItem itemAt = this.GetItemAt(mevent.X, mevent.Y);
			if (mevent.Button == MouseButtons.Left && itemAt != null)
			{
				itemAt.Pushed = true;
			}
			if (itemAt != null && this.ASCardListViewItem_0 != itemAt)
			{
				string text = itemAt.ToolTip;
				if (this.EnableSupperTooltip)
				{
					this.toolTip_0.Tag = itemAt;
					if (this.ASCardContentItemList_1 != null && this.ASCardContentItemList_1.Count > 0)
					{
						text = "   ";
					}
					Rectangle itemClientBounds = this.GetItemClientBounds(itemAt);
					int num = itemClientBounds.Right + 6;
					int num2 = itemClientBounds.Top + 3;
					Rectangle bounds = Screen.GetBounds(new Point(mevent.X, mevent.Y));
					bounds.Location = base.PointToClient(bounds.Location);
					if (num + this.TooltipWidth > bounds.Right)
					{
						num = itemClientBounds.Left - this.TooltipWidth - 6;
					}
					if (num2 + this.TooltipHeight > bounds.Bottom)
					{
						num2 = itemClientBounds.Top - this.TooltipHeight - 6;
						num = itemClientBounds.Left;
					}
					this.toolTip_0.Show(text, this, num, num2, 3000);
				}
				else
				{
					this.toolTip_0.SetToolTip(this, text);
				}
			}
			if (itemAt == null)
			{
				this.toolTip_0.Tag = null;
				this.toolTip_0.SetToolTip(this, null);
			}
			this.method_2(itemAt);
		}

		protected override void OnMouseLeave(EventArgs eventArgs_0)
		{
			base.OnMouseLeave(eventArgs_0);
			this.method_2(null);
		}

		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);
			ASCardListViewItem itemAt = this.GetItemAt(mevent.X, mevent.Y);
			if (itemAt != null && itemAt.Pushed)
			{
				itemAt.Pushed = false;
				this.InvalidateItem(itemAt);
			}
		}

		protected override void OnMouseClick(MouseEventArgs mouseEventArgs_0)
		{
			base.OnMouseClick(mouseEventArgs_0);
			if (mouseEventArgs_0.Button == MouseButtons.Left)
			{
				ASCardListViewItem itemAt = this.GetItemAt(mouseEventArgs_0.X, mouseEventArgs_0.Y);
				if (itemAt != null)
				{
					ASCardListViewEventArgs args = new ASCardListViewEventArgs(itemAt);
					this.OnItemClick(args);
				}
			}
		}

		protected override void OnMouseWheel(MouseEventArgs mouseEventArgs_0)
		{
			this.toolTip_0.Hide(this);
			base.OnMouseWheel(mouseEventArgs_0);
		}

		public virtual void OnItemClick(ASCardListViewEventArgs args)
		{
			if (this.ASCardListViewEventHandler_0 != null)
			{
				this.ASCardListViewEventHandler_0(this, args);
			}
		}

		protected override void OnMouseDoubleClick(MouseEventArgs mouseEventArgs_0)
		{
			base.OnMouseDoubleClick(mouseEventArgs_0);
			if (mouseEventArgs_0.Button == MouseButtons.Left)
			{
				ASCardListViewItem itemAt = this.GetItemAt(mouseEventArgs_0.X, mouseEventArgs_0.Y);
				if (itemAt != null)
				{
					ASCardListViewEventArgs args = new ASCardListViewEventArgs(itemAt);
					this.OnItemDoubleClick(args);
				}
			}
		}

		public virtual void OnItemDoubleClick(ASCardListViewEventArgs args)
		{
			if (this.ASCardListViewEventHandler_1 != null)
			{
				this.ASCardListViewEventHandler_1(this, args);
			}
		}

		private void method_3(ASCardListViewItem ASCardListViewItem_2)
		{
			if (this.ASCardListViewItem_1 != ASCardListViewItem_2)
			{
				ASCardListViewItem ASCardListViewItem = this.ASCardListViewItem_1;
				this.ASCardListViewItem_1 = ASCardListViewItem_2;
				if (ASCardListViewItem != null)
				{
					this.RePaintItem(ASCardListViewItem);
				}
				if (ASCardListViewItem_2 != null)
				{
					this.RePaintItem(ASCardListViewItem_2);
				}
			}
		}

		protected override void OnDragOver(DragEventArgs drgevent)
		{
			base.OnDragOver(drgevent);
			if (drgevent.Effect != DragDropEffects.None)
			{
				Point point = base.PointToClient(new Point(drgevent.X, drgevent.Y));
				ASCardListViewItem itemAt = this.GetItemAt(point.X, point.Y);
				if (itemAt == null)
				{
					drgevent.Effect = DragDropEffects.None;
				}
				else if (this.ASCardListViewDragEventHandler_0 != null)
				{
					ASCardListViewDragEventArgs e = new ASCardListViewDragEventArgs(this, drgevent, itemAt);
					this.ASCardListViewDragEventHandler_0(this, e);
				}
				if (drgevent.Effect == DragDropEffects.None)
				{
					this.method_3(null);
				}
				else
				{
					this.method_3(itemAt);
				}
			}
		}

		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			base.OnDragEnter(drgevent);
			if (drgevent.Effect != DragDropEffects.None)
			{
				Point point = base.PointToClient(new Point(drgevent.X, drgevent.Y));
				ASCardListViewItem itemAt = this.GetItemAt(point.X, point.Y);
				if (itemAt == null)
				{
					drgevent.Effect = DragDropEffects.None;
				}
				else if (this.ASCardListViewDragEventHandler_0 != null)
				{
					ASCardListViewDragEventArgs e = new ASCardListViewDragEventArgs(this, drgevent, itemAt);
					this.ASCardListViewDragEventHandler_0(this, e);
				}
				if (drgevent.Effect == DragDropEffects.None)
				{
					this.method_3(null);
				}
				else
				{
					this.method_3(itemAt);
				}
			}
		}

		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			base.OnDragDrop(drgevent);
			if (drgevent.Effect != DragDropEffects.None)
			{
				Point point = base.PointToClient(new Point(drgevent.X, drgevent.Y));
				ASCardListViewItem itemAt = this.GetItemAt(point.X, point.Y);
				if (itemAt == null)
				{
					drgevent.Effect = DragDropEffects.None;
				}
				else if (this.ASCardListViewDragEventHandler_1 != null)
				{
					ASCardListViewDragEventArgs e = new ASCardListViewDragEventArgs(this, drgevent, itemAt);
					this.ASCardListViewDragEventHandler_1(this, e);
				}
				this.method_3(null);
			}
		}

		private void method_4()
		{
			this.toolTip_0 = new ToolTip();
			this.toolTip_0.OwnerDraw = true;
			this.toolTip_0.Popup += new PopupEventHandler(this.toolTip_0_Popup);
			this.toolTip_0.Draw += new DrawToolTipEventHandler(this.toolTip_0_Draw);
		}

		private void toolTip_0_Draw(object sender, DrawToolTipEventArgs e)
		{
			e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			if (!this.EnableSupperTooltip)
			{
				e.DrawBackground();
				e.DrawText();
				e.DrawBorder();
			}
			else
			{
				ToolTip toolTip = (ToolTip)sender;
				Rectangle bounds = e.Bounds;
				bounds.Width--;
				bounds.Height--;
				GraphicsPath path = ShapeDrawer.CreateRoundRectanglePath(bounds, 6);
				using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(bounds, Color.FromArgb(254, 254, 254), Color.FromArgb(202, 217, 239), 90f))
				{
					e.Graphics.FillRectangle(linearGradientBrush, e.Bounds);
				}
				ASCardListViewControl arg_C1_0 = (ASCardListViewControl)e.AssociatedControl;
				ASCardListViewItem ASCardListViewItem = (ASCardListViewItem)toolTip.Tag;
				Rectangle bounds2 = e.Bounds;
				foreach (ASCardContentItem current in this.ASCardContentItemList_1)
				{
					ASCardContentItemPaintEventArgs ASCardContentItemPaintEventArgs = new ASCardContentItemPaintEventArgs(this, e.Graphics, bounds2, ASCardListViewItem, current);
					ASCardContentItemPaintEventArgs.Highlight = false;
					ASCardContentItemPaintEventArgs.ViewBounds = new Rectangle(current.Left + bounds2.Left, current.Top + bounds2.Top, current.Width, current.Height);
					if (ASCardListViewItem.TooltipValues.ContainsKey(current))
					{
						ASCardContentItemPaintEventArgs.Value = ASCardListViewItem.TooltipValues[current];
					}
					current.OnPaint(ASCardContentItemPaintEventArgs);
				}
				e.Graphics.DrawPath(Pens.Black, path);
			}
		}

		private void toolTip_0_Popup(object sender, PopupEventArgs e)
		{
			e.ToolTipSize = new Size(this.TooltipWidth, this.TooltipHeight);
		}
	}
}
