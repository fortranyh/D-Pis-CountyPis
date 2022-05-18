
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{
	
	public class ASCardListViewItem
	{
		internal ASCardListViewControl ASCardListViewControl_0 = null;

		private Color color_0 = Color.Empty;

		private int int_0 = 1;

		private Color color_1 = Color.Empty;

		private string string_0 = null;

		private bool bool_0 = false;

		private bool bool_1 = false;

		internal bool bool_2 = false;

		private bool bool_3 = false;

		private bool bool_4 = false;

		private bool bool_5 = false;

		private object object_0 = null;

		internal int int_1 = 0;

		internal int int_2 = 0;

		internal int int_3 = 0;

		[NonSerialized]
		private int int_4 = 0;

		[NonSerialized]
		internal int int_5 = 0;

		[NonSerialized]
		private int int_6 = 0;

		[NonSerialized]
		private int int_7 = 0;

		[NonSerialized]
		internal object object_1 = null;

		private Dictionary<ASCardContentItem, object> dictionary_0 = null;

		private Dictionary<ASCardContentItem, object> dictionary_1 = null;

		private bool bool_6 = false;

		[Browsable(false)]
		public ASCardListViewControl ListView
		{
			get
			{
				return this.ASCardListViewControl_0;
			}
		}

		[DefaultValue(typeof(Color), "Empty")]
		public Color BorderColor
		{
			get
			{
				return this.color_0;
			}
			set
			{
				this.color_0 = value;
			}
		}

		[DefaultValue(1)]
		public int BorderWidth
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

		[DefaultValue(typeof(Color), "Empty")]
		public Color BackColor
		{
			get
			{
				return this.color_1;
			}
			set
			{
				this.color_1 = value;
			}
		}

		[DefaultValue(null)]
		public string ToolTip
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

		[DefaultValue(false)]
		public bool HighlightBorder
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
					if (this.ASCardListViewControl_0 != null)
					{
						this.ASCardListViewControl_0.InvalidateItem(this);
					}
				}
			}
		}

		[DefaultValue(false)]
		public bool Highlight
		{
			get
			{
				return this.bool_1;
			}
			set
			{
				if (this.bool_1 != value)
				{
					this.bool_1 = value;
					if (this.ASCardListViewControl_0 != null)
					{
						this.ASCardListViewControl_0.InvalidateItem(this);
					}
				}
			}
		}

		[DefaultValue(false)]
		public bool Blink
		{
			get
			{
				return this.bool_3;
			}
			set
			{
				this.bool_3 = value;
			}
		}

		public bool Pushed
		{
			get
			{
				return this.bool_4;
			}
			set
			{
				if (this.bool_4 != value)
				{
					this.bool_4 = value;
					if (this.ASCardListViewControl_0 != null)
					{
						this.ASCardListViewControl_0.InvalidateItem(this);
					}
				}
			}
		}

		public bool Selected
		{
			get
			{
				return this.bool_5;
			}
			set
			{
				this.bool_5 = value;
			}
		}

		[Browsable(false)]
		public object Tag
		{
			get
			{
				return this.object_0;
			}
			set
			{
				this.object_0 = value;
			}
		}

		[Browsable(false), ReadOnly(true)]
		public int Index
		{
			get
			{
				return this.int_1;
			}
		}

		[Browsable(false)]
		public int RowIndex
		{
			get
			{
				return this.int_2;
			}
		}

		[Browsable(false)]
		public int ColumnIndex
		{
			get
			{
				return this.int_3;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Left
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

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Top
		{
			get
			{
				return this.int_5;
			}
			set
			{
				this.int_5 = value;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Width
		{
			get
			{
				return this.int_6;
			}
			set
			{
				this.int_6 = value;
			}
		}

		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Height
		{
			get
			{
				return this.int_7;
			}
			set
			{
				this.int_7 = value;
			}
		}

		public object DataBoundItem
		{
			get
			{
				return this.object_1;
			}
		}

		[Browsable(false), DefaultValue(null)]
		public Dictionary<ASCardContentItem, object> Values
		{
			get
			{
				return this.dictionary_0;
			}
			set
			{
				this.dictionary_0 = value;
			}
		}

		[Browsable(false), DefaultValue(null)]
		public Dictionary<ASCardContentItem, object> TooltipValues
		{
			get
			{
				return this.dictionary_1;
			}
			set
			{
				this.dictionary_1 = value;
			}
		}

		[DefaultValue(false)]
		public bool AutoLine
		{
			get
			{
				return this.bool_6;
			}
			set
			{
				this.bool_6 = value;
			}
		}

		public void Invalidate()
		{
			if (this.ListView != null)
			{
				this.ListView.InvalidateItem(this);
			}
		}

		public bool SetValue(string fieldName, object Value)
		{
			bool result;
			if (this.ASCardListViewControl_0 != null)
			{
				ASCardContentItem itemByFieldName = this.ASCardListViewControl_0.CardTemplate.GetItemByFieldName(fieldName);
				if (itemByFieldName != null)
				{
					this.dictionary_0[itemByFieldName] = Value;
					this.ASCardListViewControl_0.InvalidateItem(this);
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public object GetValue(string fieldName)
		{
			object result;
			foreach (ASCardContentItem current in this.dictionary_0.Keys)
			{
				if (string.Compare(current.DataField, fieldName, true) == 0)
				{
					result = this.dictionary_0[current];
					return result;
				}
			}
			result = null;
			return result;
		}
	}
}
