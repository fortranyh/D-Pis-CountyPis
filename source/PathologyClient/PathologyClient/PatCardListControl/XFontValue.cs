
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ASwartz.WinForms.Controls
{
    [Serializable]
    public class XFontValue : ICloneable, IComponent, IXFontValue
    {
        public static string SuggestBaseName;

        [NonSerialized]
        public static Font DefaultFont;

        [NonSerialized]
        public static string DefaultFontName;

        [NonSerialized]
        public static float DefaultFontSize;

        private string _Name = XFontValue.DefaultFontName;

        private float _Size = XFontValue.DefaultFontSize;

        private GraphicsUnit _Unit = GraphicsUnit.Point;

        private bool bolBold = false;

        private bool bolItalic = false;

        private bool bolUnderline = false;

        private bool bolStrikeout = false;

        [NonSerialized]
        private static List<Font> myBuffer;

        [NonSerialized]
        private static List<string> BadFontNames;

        [NonSerialized]
        private int _RawFontIndex = -1;

        [NonSerialized]
        private Font myValue = null;

        private ISite mySite = null;

        public static event EventHandler BufferCleared;

        public event EventHandler Disposed = null;

        [Browsable(false)]
        public bool IsDefault
        {
            get
            {
                return this._Name == XFontValue.DefaultFontName && this._Size == XFontValue.DefaultFontSize && !this.bolItalic && !this.bolUnderline && !this.bolBold && !this.bolStrikeout;
            }
        }

        [Browsable(false)]
        public bool IsDefaultName
        {
            get
            {
                return this._Name == XFontValue.DefaultFontName;
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    if (this._Name == null || this._Name.Length == 0)
                    {
                        this._Name = XFontValue.DefaultFontName;
                    }
                    this.myValue = null;
                }
            }
        }

        [Browsable(false)]
        public bool IsDefaultSize
        {
            get
            {
                return this._Size == XFontValue.DefaultFontSize;
            }
        }

        [DefaultValue(9f), Editor("ASwartz.Editor.FontSizeEditor", typeof(UITypeEditor))]
        public float Size
        {
            get
            {
                return this._Size;
            }
            set
            {
                if (this._Size != value)
                {
                    this._Size = value;
                    if (this._Size <= 0f)
                    {
                        this._Size = XFontValue.DefaultFontSize;
                    }
                    this.myValue = null;
                }
            }
        }

        [DefaultValue(GraphicsUnit.Point)]
        public GraphicsUnit Unit
        {
            get
            {
                return this._Unit;
            }
            set
            {
                this._Unit = value;
            }
        }

        [DefaultValue(false)]
        public bool Bold
        {
            get
            {
                return this.bolBold;
            }
            set
            {
                if (this.bolBold != value)
                {
                    this.bolBold = value;
                    this.myValue = null;
                }
            }
        }

        [DefaultValue(false)]
        public bool Italic
        {
            get
            {
                return this.bolItalic;
            }
            set
            {
                if (this.bolItalic != value)
                {
                    this.bolItalic = value;
                    this.myValue = null;
                }
            }
        }

        [DefaultValue(false)]
        public bool Underline
        {
            get
            {
                return this.bolUnderline;
            }
            set
            {
                if (this.bolUnderline != value)
                {
                    this.bolUnderline = value;
                    this.myValue = null;
                }
            }
        }

        [DefaultValue(false)]
        public bool Strikeout
        {
            get
            {
                return this.bolStrikeout;
            }
            set
            {
                if (this.bolStrikeout != value)
                {
                    this.bolStrikeout = value;
                    this.myValue = null;
                }
            }
        }

        [Browsable(false), DefaultValue(FontStyle.Regular), XmlIgnore]
        public FontStyle Style
        {
            get
            {
                FontStyle fontStyle = FontStyle.Regular;
                if (this.bolBold)
                {
                    fontStyle = FontStyle.Bold;
                }
                if (this.bolItalic)
                {
                    fontStyle |= FontStyle.Italic;
                }
                if (this.bolUnderline)
                {
                    fontStyle |= FontStyle.Underline;
                }
                if (this.bolStrikeout)
                {
                    fontStyle |= FontStyle.Strikeout;
                }
                return fontStyle;
            }
            set
            {
                if (this.Style != value)
                {
                    this.bolBold = this.GetStyle(value, FontStyle.Bold);
                    this.bolItalic = this.GetStyle(value, FontStyle.Italic);
                    this.bolUnderline = this.GetStyle(value, FontStyle.Underline);
                    this.bolStrikeout = this.GetStyle(value, FontStyle.Strikeout);
                    this.myValue = null;
                }
            }
        }

        [Browsable(false)]
        public int CellAscent
        {
            get
            {
                return this.Value.FontFamily.GetCellAscent(this.Style);
            }
        }

        [Browsable(false)]
        public int CellDescent
        {
            get
            {
                return this.Value.FontFamily.GetCellDescent(this.Style);
            }
        }

        [Browsable(false)]
        public int LineSpacing
        {
            get
            {
                return this.Value.FontFamily.GetLineSpacing(this.Style);
            }
        }

        [Browsable(false)]
        public int EmHeight
        {
            get
            {
                return this.Value.FontFamily.GetEmHeight(this.Style);
            }
        }

        [Browsable(false)]
        public static List<Font> Buffer
        {
            get
            {
                return XFontValue.myBuffer;
            }
        }

        [Browsable(false), XmlIgnore]
        public int RawFontIndex
        {
            get
            {
                if (this._RawFontIndex < 0 || this.myValue == null)
                {
                    Font value = this.Value;
                    this._RawFontIndex = XFontValue.myBuffer.IndexOf(value);
                }
                return this._RawFontIndex;
            }
        }

        [Browsable(false), XmlIgnore]
        public Font Value
        {
            get
            {
                Font defaultFont;
                if (this.myValue == null)
                {
                    if (XFontValue.myBuffer.Count > 200)
                    {
                        defaultFont = XFontValue.DefaultFont;
                        return defaultFont;
                    }
                    string text = this._Name;
                    float size = this._Size;
                    FontStyle style = this.Style;
                    if (XFontValue.BadFontNames.Count > 0)
                    {
                        foreach (string current in XFontValue.BadFontNames)
                        {
                            if (string.Compare(current, text, true) == 0)
                            {
                                text = XFontValue.DefaultFontName;
                                break;
                            }
                        }
                    }
                    if (text == XFontValue.DefaultFontName && size == XFontValue.DefaultFontSize && style == FontStyle.Regular)
                    {
                        this.myValue = XFontValue.DefaultFont;
                        if (!XFontValue.myBuffer.Contains(XFontValue.DefaultFont))
                        {
                            XFontValue.myBuffer.Add(XFontValue.DefaultFont);
                        }
                    }
                    else
                    {
                        foreach (Font current2 in XFontValue.myBuffer)
                        {
                            if (text == current2.Name && size == current2.Size && style == current2.Style && this._Unit == current2.Unit)
                            {
                                this.myValue = current2;
                                break;
                            }
                        }
                    }
                    if (this.myValue == null)
                    {
                        FontFamily fontFamily = null;
                        try
                        {
                            fontFamily = new FontFamily(text);
                            if (fontFamily.Name != text)
                            {
                                fontFamily = new FontFamily(XFontValue.DefaultFontName);
                                bool flag = false;
                                foreach (string current in XFontValue.BadFontNames)
                                {
                                    if (string.Compare(current, this._Name, true) == 0)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                                if (!flag)
                                {
                                    XFontValue.BadFontNames.Add(this._Name);
                                }
                            }
                        }
                        catch (Exception)
                        {
                            fontFamily = new FontFamily(XFontValue.DefaultFontName);
                            bool flag = false;
                            foreach (string current in XFontValue.BadFontNames)
                            {
                                if (string.Compare(current, this._Name, true) == 0)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                XFontValue.BadFontNames.Add(this._Name);
                            }
                        }
                        try
                        {
                            if (!fontFamily.IsStyleAvailable(this.Style))
                            {
                                FontStyle[] array = new FontStyle[]
                                {
                                    FontStyle.Regular,
                                    FontStyle.Bold,
                                    FontStyle.Italic,
                                    FontStyle.Underline,
                                    FontStyle.Strikeout,
                                    FontStyle.Bold | FontStyle.Italic
                                };
                                for (int i = 0; i < array.Length; i++)
                                {
                                    FontStyle style2 = array[i];
                                    if (fontFamily.IsStyleAvailable(style2))
                                    {
                                        this.Style = style2;
                                        break;
                                    }
                                }
                            }
                            this.myValue = new Font(fontFamily, this._Size, this.Style, this.Unit);
                            XFontValue.myBuffer.Add(this.myValue);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            this.myValue = XFontValue.DefaultFont;
                        }
                    }
                }
                defaultFont = this.myValue;
                return defaultFont;
            }
            set
            {
                if (value == null)
                {
                    value = XFontValue.DefaultFont;
                }
                if (!this.EqualsValue(value))
                {
                    this._Name = value.Name;
                    this._Size = value.Size;
                    this.bolBold = value.Bold;
                    this.bolItalic = value.Italic;
                    this.bolUnderline = value.Underline;
                    this.bolStrikeout = value.Strikeout;
                    this._Unit = value.Unit;
                    this.myValue = value;
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), XmlIgnore]
        public ISite Site
        {
            get
            {
                return this.mySite;
            }
            set
            {
                this.mySite = value;
            }
        }

        static XFontValue()
        {
            XFontValue.SuggestBaseName = "Font";
            XFontValue.DefaultFont = null;
            XFontValue.DefaultFontName = null;
            XFontValue.DefaultFontSize = 9f;
            XFontValue.myBuffer = new List<Font>();
            XFontValue.BadFontNames = new List<string>();
            XFontValue.BufferCleared = null;
            XFontValue.DefaultFont = Control.DefaultFont;
            XFontValue.DefaultFontName = XFontValue.DefaultFont.Name;
            XFontValue.DefaultFontSize = XFontValue.DefaultFont.Size;
        }

        public XFontValue()
        {
        }

        public XFontValue(string name, float size)
        {
            this._Name = name;
            this._Size = size;
        }

        public XFontValue(string name, float size, FontStyle style)
        {
            this._Name = name;
            this._Size = size;
            this.Style = style;
        }

        public XFontValue(string name, float size, FontStyle style, GraphicsUnit unit)
        {
            this._Name = name;
            this._Size = size;
            this.Style = style;
            this.Unit = unit;
        }

        public XFontValue(Font font_0)
        {
            this.Value = font_0;
        }

        private bool GetStyle(FontStyle intValue, FontStyle MaskFlag)
        {
            return (intValue & MaskFlag) == MaskFlag;
        }

        public bool FixFontName()
        {
            string text = XFontValue.FixFontName(this._Name);
            bool result;
            if (text != this._Name)
            {
                this._Name = text;
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static void ClearBuffer()
        {
            XFontValue.myBuffer.Clear();
            XFontValue.BadFontNames.Clear();
            GC.Collect();
            if (XFontValue.BufferCleared != null)
            {
                XFontValue.BufferCleared(null, null);
            }
        }

        public static string FixFontName(string name)
        {
            if (name == null || name.Trim().Length == 0)
            {
                return DefaultFontName;
            }
            while (true)
            {
                name = name.Trim();
                if (BadFontNames.Count > 0)
                {
                    foreach (string badFontName in BadFontNames)
                    {
                        if (string.Compare(name, badFontName, true) == 0)
                        {
                            return DefaultFontName;
                        }
                    }
                }
                try
                {
                    FontFamily fontFamily = new FontFamily(name);
                    return fontFamily.Name;
                }
                catch (Exception)
                {
                    int num = name.IndexOf("(");
                    if (num <= 0)
                    {
                        BadFontNames.Add(name);
                        break;
                    }
                    string text = name.Substring(0, num).Trim();
                    if (text.Length <= 0)
                    {
                        BadFontNames.Add(name);
                        break;
                    }
                    name = text;
                }
            }
            return DefaultFontName;
        }
        public float GetHeight()
        {
            Font value = this.Value;
            float result;
            if (value == null)
            {
                result = 0f;
            }
            else
            {
                result = value.GetHeight();
            }
            return result;
        }

        public float GetHeight(Graphics graphics_0)
        {
            Font value = this.Value;
            float result;
            if (value == null)
            {
                result = 0f;
            }
            else
            {
                result = value.GetHeight(graphics_0);
            }
            return result;
        }

        public float GetHeight(float float_0)
        {
            Font value = this.Value;
            float result;
            if (value == null)
            {
                result = 0f;
            }
            else
            {
                result = value.GetHeight(float_0);
            }
            return result;
        }

        public float GetHeight(GraphicsUnit unit)
        {
            return GraphicsUnitConvert.Convert(this.Value.SizeInPoints, GraphicsUnit.Point, unit);
        }

        public void CopySettings(XFontValue SourceFont)
        {
            this._Name = SourceFont._Name;
            this._Size = SourceFont._Size;
            this.bolBold = SourceFont.bolBold;
            this.bolItalic = SourceFont.bolItalic;
            this.bolUnderline = SourceFont.bolUnderline;
            this.bolStrikeout = SourceFont.bolStrikeout;
            this._Unit = SourceFont._Unit;
        }

        public bool EqualsValue(Font font_0)
        {
            return font_0 != null && !(this._Name != font_0.Name) && this._Size == font_0.Size && this.bolBold == font_0.Bold && this.bolItalic == font_0.Italic && this.bolUnderline == font_0.Underline && this.bolStrikeout == font_0.Strikeout && this._Unit == font_0.Unit;
        }

        public bool EqualsValue(XFontValue xfontValue_0)
        {
            return xfontValue_0 != null && (this == xfontValue_0 || (!(this._Name != xfontValue_0._Name) && this._Size == xfontValue_0._Size && this.bolBold == xfontValue_0.bolBold && this.bolItalic == xfontValue_0.bolItalic && this.bolUnderline == xfontValue_0.bolUnderline && this.bolStrikeout == xfontValue_0.bolStrikeout && this._Unit == xfontValue_0._Unit));
        }

        public XFontValue Clone()
        {
            XFontValue xFontValue = new XFontValue();
            xFontValue.CopySettings(this);
            return xFontValue;
        }

        object ICloneable.Clone()
        {
            XFontValue xFontValue = new XFontValue();
            xFontValue.CopySettings(this);
            return xFontValue;
        }

        public override bool Equals(object object_0)
        {
            bool result;
            if (object_0 == this)
            {
                result = true;
            }
            else if (!(object_0 is XFontValue))
            {
                result = false;
            }
            else
            {
                XFontValue xFontValue = (XFontValue)object_0;
                result = (xFontValue.bolBold == this.bolBold && xFontValue.bolItalic == this.bolItalic && xFontValue.bolStrikeout == this.bolStrikeout && xFontValue.bolUnderline == this.bolUnderline && xFontValue._Size == this._Size && xFontValue._Name == this._Name && xFontValue._Unit == this._Unit);
            }
            return result;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override string ToString()
        {
            ArrayList arrayList = new ArrayList();
            arrayList.Add(this.Name);
            arrayList.Add(this.Size.ToString());
            if (this.Style != FontStyle.Regular)
            {
                arrayList.Add("style=" + this.Style.ToString("G"));
            }
            if (this._Unit != GraphicsUnit.Point)
            {
                arrayList.Add(this._Unit.ToString("G"));
            }
            return string.Join(", ", (string[])arrayList.ToArray(typeof(string)));
        }

        public void Parse(string Text)
        {
            if (Text != null)
            {
                string[] array = Text.Split(new char[]
                {
                    ','
                });
                if (array.Length < 1)
                {
                    throw new ArgumentException("必须符合 name,size,style=Bold,Italic,Underline,Strikeout 样式");
                }
                string name = array[0];
                float size = 9f;
                if (array.Length >= 2)
                {
                    size = float.Parse(array[1].Trim());
                }
                FontStyle fontStyle = FontStyle.Regular;
                bool flag = false;
                for (int i = 2; i < array.Length; i++)
                {
                    string text = array[i].Trim().ToLower();
                    if (!flag && text.StartsWith("style"))
                    {
                        int num = text.IndexOf("=");
                        if (num > 0)
                        {
                            flag = true;
                            text = text.Substring(num + 1);
                        }
                    }
                    if (flag)
                    {
                        if (Enum.IsDefined(typeof(FontStyle), text.Trim()))
                        {
                            FontStyle fontStyle2 = (FontStyle)Enum.Parse(typeof(FontStyle), text.Trim(), true);
                            fontStyle |= fontStyle2;
                        }
                        else if (Enum.IsDefined(typeof(GraphicsUnit), text.Trim()))
                        {
                            this._Unit = (GraphicsUnit)Enum.Parse(typeof(GraphicsUnit), text.Trim(), true);
                        }
                    }
                }
                this.Name = name;
                this.Size = size;
                this.Style = fontStyle;
            }
        }

        public void Dispose()
        {
            if (this.Disposed != null)
            {
                this.Disposed(this, new EventArgs());
            }
        }
    }
}
