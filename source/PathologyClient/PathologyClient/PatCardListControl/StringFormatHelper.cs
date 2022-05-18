using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ASwartz.WinForms.Controls
{
    [ComVisible(false)]
    public sealed class StringFormatHelper
    {
        public static string FixMultiLineTextForCr(string text)
        {
            string result;
            if (string.IsNullOrEmpty(text))
            {
                result = text;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder(text.Length);
                int length = text.Length;
                for (int i = 0; i < length; i++)
                {
                    char c = text[i];
                    if (c == '\n' && i > 0 && text[i] != '\r')
                    {
                        stringBuilder.Append('\r');
                    }
                    stringBuilder.Append(c);
                }
                result = stringBuilder.ToString();
            }
            return result;
        }

        public static string FormatString(string format, IDataRecord record)
        {
            string result;
            if (string.IsNullOrEmpty(format))
            {
                result = format;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                StringBuilder stringBuilder2 = null;
                for (int i = 0; i < format.Length; i++)
                {
                    char c = format[i];
                    if (c == '{')
                    {
                        stringBuilder2 = new StringBuilder();
                    }
                    else if (c == '}')
                    {
                        string text = null;
                        string text2 = stringBuilder2.ToString();
                        stringBuilder2 = null;
                        int num = text2.IndexOf(':');
                        if (num > 0)
                        {
                            text = text2.Substring(num + 1);
                            text2 = text2.Substring(0, num);
                        }
                        int num2 = record.GetOrdinal(text2);
                        if (num2 < 0 && !int.TryParse(text2, out num2))
                        {
                            num2 = -1;
                        }
                        if (num2 >= 0 && !record.IsDBNull(num2))
                        {
                            object value = record.GetValue(num2);
                            if (string.IsNullOrEmpty(text))
                            {
                                stringBuilder.Append(Convert.ToString(value));
                            }
                            else if (value is IFormattable)
                            {
                                stringBuilder.Append(((IFormattable)value).ToString(text, null));
                            }
                            else
                            {
                                stringBuilder.Append(Convert.ToString(value));
                            }
                        }
                    }
                    else if (stringBuilder2 == null)
                    {
                        stringBuilder.Append(c);
                    }
                    else
                    {
                        stringBuilder2.Append(c);
                    }
                }
                if (stringBuilder2 != null)
                {
                    stringBuilder.Append(stringBuilder2.ToString());
                }
                result = stringBuilder.ToString();
            }
            return result;
        }

        public static string AddLineCount(string MultilineText, int StartLineIndex)
        {
            string result;
            if (MultilineText == null)
            {
                result = null;
            }
            else
            {
                StringReader stringReader = new StringReader(MultilineText);
                ArrayList arrayList = new ArrayList();
                for (string text = stringReader.ReadLine(); text != null; text = stringReader.ReadLine())
                {
                    arrayList.Add(text);
                }
                stringReader.Close();
                StringBuilder stringBuilder = new StringBuilder();
                string format = new string('0', (int)Math.Ceiling(Math.Log10((double)(arrayList.Count + StartLineIndex))));
                string newLine = Environment.NewLine;
                for (int i = 0; i < arrayList.Count; i++)
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append(newLine);
                    }
                    stringBuilder.Append((i + StartLineIndex).ToString(format));
                    stringBuilder.Append(":");
                    stringBuilder.Append((string)arrayList[i]);
                }
                result = stringBuilder.ToString();
            }
            return result;
        }

        public static string[] GetLines(string strText)
        {
            string[] result;
            if (strText == null || strText.Length == 0)
            {
                result = null;
            }
            else
            {
                StringReader stringReader = new StringReader(strText);
                ArrayList arrayList = new ArrayList();
                for (string text = stringReader.ReadLine(); text != null; text = stringReader.ReadLine())
                {
                    arrayList.Add(text);
                }
                stringReader.Close();
                result = (string[])arrayList.ToArray(typeof(string));
            }
            return result;
        }

        public static string ToSingleLine(string strText)
        {
            string result;
            if (strText == null || strText.Length == 0)
            {
                result = strText;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < strText.Length; i++)
                {
                    char c = strText[i];
                    if (c != '\r' && c != '\n')
                    {
                        stringBuilder.Append(c);
                    }
                }
                result = stringBuilder.ToString();
            }
            return result;
        }

        public static string RemoveChar(string strText, char vChar)
        {
            string result;
            if (strText == null || strText.Length == 0)
            {
                result = strText;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < strText.Length; i++)
                {
                    char c = strText[i];
                    if (c != vChar)
                    {
                        stringBuilder.Append(c);
                    }
                }
                result = stringBuilder.ToString();
            }
            return result;
        }

        public static string RemovePrefix(string strText, string strPrefix)
        {
            string result;
            if (strText == null)
            {
                result = strText;
            }
            else if (strText.StartsWith(strPrefix))
            {
                result = strText.Substring(strPrefix.Length);
            }
            else
            {
                result = strText;
            }
            return result;
        }

        public static string RemoveEndfix(string strText, string strEndfix)
        {
            string result;
            if (strText == null || !strText.EndsWith(strEndfix))
            {
                result = strText;
            }
            else
            {
                result = strText.Substring(0, strText.Length - strEndfix.Length);
            }
            return result;
        }

        public static string LinesAddfix(string strText, string strPrefix, string strEndfix)
        {
            string result;
            if (strText == null || strText.Length == 0)
            {
                result = strText;
            }
            else
            {
                StringReader stringReader = new StringReader(strText);
                StringBuilder stringBuilder = new StringBuilder();
                string text = stringReader.ReadLine();
                while (text != null)
                {
                    if (text.Length > 0 && strPrefix != null)
                    {
                        stringBuilder.Append(strPrefix);
                    }
                    stringBuilder.Append(text);
                    if (text.Length > 0 && strEndfix != null)
                    {
                        stringBuilder.Append(strEndfix);
                    }
                    text = stringReader.ReadLine();
                    if (text == null)
                    {
                        break;
                    }
                    stringBuilder.Append(Environment.NewLine);
                }
                stringReader.Close();
                result = stringBuilder.ToString();
            }
            return result;
        }

        public static int FixStringByteLength(ArrayList myList, int FixStyle, char FillChar)
        {
            Encoding encoding = Encoding.GetEncoding(936);
            int num = 0;
            foreach (string txt in myList)
            {
                if (txt != null)
                {
                    int byteCount = encoding.GetByteCount(txt);
                    if (num < byteCount)
                    {
                        num = byteCount;
                    }
                }
            }
            if (FixStyle == 1 || FixStyle == 2)
            {
                for (int i = 0; i < myList.Count; i++)
                {
                    string txt = (string)myList[i];
                    if (txt == null)
                    {
                        txt = "";
                    }
                    int byteCount = encoding.GetByteCount(txt);
                    if (byteCount != num)
                    {
                        if (FixStyle == 1)
                        {
                            txt = new string(FillChar, num - byteCount) + txt;
                        }
                        else
                        {
                            txt += new string(FillChar, num - byteCount);
                        }
                        myList[i] = txt;
                    }
                }
            }
            return num;
        }

        public static string ClearWhiteSpace(string strText, int intMaxLength, bool bolHtml)
        {
            string result;
            if (strText == null)
            {
                result = null;
            }
            else
            {
                bool flag = false;
                StringBuilder stringBuilder = new StringBuilder();
                int num = 0;
                for (int i = 0; i < strText.Length; i++)
                {
                    char c = strText[i];
                    if (char.IsWhiteSpace(c))
                    {
                        flag = true;
                    }
                    else
                    {
                        if (bolHtml && flag)
                        {
                            stringBuilder.Append(" ");
                        }
                        stringBuilder.Append(c);
                        if (intMaxLength > 0)
                        {
                            num++;
                            if (num > intMaxLength)
                            {
                                break;
                            }
                        }
                        flag = false;
                    }
                }
                result = stringBuilder.ToString();
            }
            return result;
        }

        public static string ClearBlankLine(string strData)
        {
            if (strData == null)
            {
                return null;
            }
            StringReader stringReader = new StringReader(strData);
            StringBuilder stringBuilder = new StringBuilder();
            string text = stringReader.ReadLine();
            bool flag = true;
            while (text != null)
            {
                string text2 = text;
                foreach (char c in text2)
                {
                    if (!char.IsWhiteSpace(c))
                    {
                        if (!flag)
                        {
                            stringBuilder.Append("\r\n");
                        }
                        stringBuilder.Append(text);
                        flag = false;
                        break;
                    }
                }
                text = stringReader.ReadLine();
            }
            stringReader.Close();
            return stringBuilder.ToString();
        }

        public static string RemoveBlank(string strText)
        {
            string result;
            if (strText == null)
            {
                result = strText;
            }
            else if (strText.Length == 0)
            {
                result = strText;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < strText.Length; i++)
                {
                    char c = strText[i];
                    if (!char.IsWhiteSpace(c))
                    {
                        stringBuilder.Append(c);
                    }
                }
                result = stringBuilder.ToString();
            }
            return result;
        }

        public static string NormalizeSpace(string strData)
        {
            string result;
            if (strData == null)
            {
                result = null;
            }
            else
            {
                char[] array = new char[strData.Length];
                int num = 0;
                bool flag = false;
                for (int i = 0; i < strData.Length; i++)
                {
                    if (char.IsWhiteSpace(strData[i]))
                    {
                        if (!flag)
                        {
                            flag = true;
                            array[num] = ' ';
                            num++;
                        }
                    }
                    else
                    {
                        flag = false;
                        array[num] = strData[i];
                        num++;
                    }
                }
                if (num == 0)
                {
                    result = "";
                }
                else
                {
                    result = new string(array, 0, num);
                }
            }
            return result;
        }

        public static bool IsBlankString(string strData)
        {
            bool result;
            if (strData == null)
            {
                result = true;
            }
            else
            {
                for (int i = 0; i < strData.Length; i++)
                {
                    if (!char.IsWhiteSpace(strData[i]))
                    {
                        result = false;
                        return result;
                    }
                }
                result = true;
            }
            return result;
        }

        public static bool HasBlank(string strData)
        {
            bool result;
            if (strData == null)
            {
                result = false;
            }
            else
            {
                for (int i = 0; i < strData.Length; i++)
                {
                    if (char.IsWhiteSpace(strData[i]))
                    {
                        result = true;
                        return result;
                    }
                }
                result = false;
            }
            return result;
        }

        public static bool HasContent(string strData)
        {
            bool result;
            if (strData != null && strData.Length > 0)
            {
                for (int i = 0; i < strData.Length; i++)
                {
                    char c = strData[i];
                    if (!char.IsWhiteSpace(c))
                    {
                        result = true;
                        return result;
                    }
                }
            }
            result = false;
            return result;
        }

        public static bool IsEmpty(string strData)
        {
            return strData == null || strData.Length == 0;
        }

        public static bool IsLetterOrDigit(string strData)
        {
            bool result;
            if (strData != null)
            {
                for (int i = 0; i < strData.Length; i++)
                {
                    if (!char.IsLetterOrDigit(strData, i))
                    {
                        result = false;
                        return result;
                    }
                }
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public static string GroupFormatString(string strData, int GroupSize, int LineGroupCount)
        {
            string result;
            if (strData == null || strData.Length == 0 || (GroupSize <= 0 && LineGroupCount <= 0))
            {
                result = strData;
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                int length = strData.Length;
                int num = 0;
                LineGroupCount *= GroupSize;
                while (true)
                {
                    stringBuilder.Append(" ");
                    if (num + GroupSize >= length)
                    {
                        break;
                    }
                    stringBuilder.Append(strData.Substring(num, GroupSize));
                    num += GroupSize;
                    if (num % LineGroupCount == 0)
                    {
                        stringBuilder.Append("\r\n");
                    }
                }
                stringBuilder.Append(strData.Substring(num));
                result = stringBuilder.ToString();
            }
            return result;
        }

        private StringFormatHelper()
        {
        }
    }
}
