using System;
using System.Collections;
using System.Text;

namespace ASwartz.WinForms.Controls
{

    public class VariableString
    {
        private bool bool_0 = false;

        private string string_0 = "@";

        private string string_1 = "[%";

        private string string_2 = "%]";

        private IVariableProvider ivariableProvider_0 = new HashTableVariableProvider();

        private string string_3 = null;

        public bool NamedParameter
        {
            get
            {
                return this.bool_0;
            }
            set
            {
                this.bool_0 = value;
            }
        }

        public string ParameterNamePrefix
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

        public string VariablePrefix
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

        public string VariableEndfix
        {
            get
            {
                return this.string_2;
            }
            set
            {
                this.string_2 = value;
            }
        }

        public IVariableProvider Variables
        {
            get
            {
                return this.ivariableProvider_0;
            }
            set
            {
                this.ivariableProvider_0 = value;
            }
        }

        public string Text
        {
            get
            {
                return this.string_3;
            }
            set
            {
                this.string_3 = value;
            }
        }

        public VariableString()
        {
        }

        public VariableString(string string_4)
        {
            this.string_3 = string_4;
        }

        public VariableString(string string_4, string Prefix, string EndFix)
        {
            this.string_3 = string_4;
            this.string_1 = Prefix;
            this.string_2 = EndFix;
        }

        public void SetVariable(string strName, string strValue)
        {
            if (this.ivariableProvider_0 != null)
            {
                this.ivariableProvider_0.Set(strName, strValue);
            }
        }

        public string[] GetVariableNames()
        {
            string[] array = VariableString.AnalyseVariableString(this.string_3, this.string_1, this.string_2);
            string[] result;
            if (array != null)
            {
                ArrayList arrayList = new ArrayList();
                for (int i = 1; i < array.Length; i += 2)
                {
                    arrayList.Add(array[i]);
                }
                result = (string[])arrayList.ToArray(typeof(string));
            }
            else
            {
                result = null;
            }
            return result;
        }

        public string Execute()
        {
            return this.Execute(this.string_3, null);
        }

        public string Execute(ArrayList ParameterValues)
        {
            return this.Execute(this.string_3, ParameterValues);
        }

        public string Execute(string string_4, ArrayList ParameterValues)
        {
            if (this.ivariableProvider_0 == null)
            {
                throw new InvalidOperationException("未设置 Variables 属性");
            }
            string result;
            if (string_4 == null || string_4.Length == 0)
            {
                result = string_4;
            }
            else
            {
                string[] array = VariableString.AnalyseVariableString(string_4, this.string_1, this.string_2);
                if (array == null)
                {
                    result = null;
                }
                else
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    ArrayList arrayList = new ArrayList();
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            stringBuilder.Append(array[i]);
                        }
                        else
                        {
                            string text = array[i];
                            bool flag;
                            if (flag = text.StartsWith("@"))
                            {
                                text = text.Substring(1);
                            }
                            string value;
                            if (this.ivariableProvider_0.Exists(text))
                            {
                                value = this.ivariableProvider_0.Get(text);
                            }
                            else
                            {
                                value = "";
                            }
                            if (ParameterValues != null && flag)
                            {
                                if (this.bool_0)
                                {
                                    string text2 = text.Trim();
                                    if (this.method_0(arrayList, text2))
                                    {
                                        for (int j = arrayList.Count; j < 100; j++)
                                        {
                                            text2 = "Parameter" + j;
                                            if (!this.method_0(arrayList, text2))
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    arrayList.Add(text2);
                                    ParameterValues.Add(text2);
                                    ParameterValues.Add(value);
                                    stringBuilder.Append(" " + this.string_0 + text2 + " ");
                                }
                                else
                                {
                                    ParameterValues.Add(value);
                                    stringBuilder.Append(" ? ");
                                }
                            }
                            else
                            {
                                stringBuilder.Append(value);
                            }
                        }
                    }
                    result = stringBuilder.ToString();
                }
            }
            return result;
        }

        private bool method_0(ArrayList arrayList_0, string string_4)
        {
            string_4 = string_4.Trim();
            bool result;
            for (int i = 0; i < arrayList_0.Count; i++)
            {
                string text = (string)arrayList_0[i];
                text = text.Trim();
                if (string.Compare(string_4, text, true) == 0)
                {
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }

        public static string[] AnalyseVariableString(string strText, string strHead, string strEnd)
        {
            return VariableString.AnalyseVariableString(strText, strHead, strEnd, false);
        }

        public static string[] AnalyseVariableString(string strText, string strHead, string strEnd, bool EnableEmptyItem)
        {
            string[] result;
            if (strText == null || strHead == null || strEnd == null || strHead.Length == 0 || strEnd.Length == 0 || strText.Length == 0)
            {
                result = new string[]
                {
                    strText
                };
            }
            else
            {
                int num = strText.IndexOf(strHead);
                if (num < 0)
                {
                    result = new string[]
                    {
                        strText
                    };
                }
                else
                {
                    ArrayList arrayList = new ArrayList();
                    int num2 = 0;
                    do
                    {
                        int num3 = strText.IndexOf(strEnd, num + 1);
                        if (num3 <= num)
                        {
                            break;
                        }
                        num = VariableString.smethod_0(strText, strHead, num2, num3);
                        if (num == -1)
                        {
                            break;
                        }
                        int num4 = num3 - num - strHead.Length;
                        if (num4 == 0 && !EnableEmptyItem)
                        {
                            break;
                        }
                        string value = strText.Substring(num + strHead.Length, num4);
                        if (num2 < num)
                        {
                            string value2 = strText.Substring(num2, num - num2);
                            arrayList.Add(value2);
                        }
                        else
                        {
                            arrayList.Add("");
                        }
                        arrayList.Add(value);
                        num = num3 + strEnd.Length;
                        num2 = num;
                    }
                    while (num >= 0 && num < strText.Length);
                    if (num2 < strText.Length)
                    {
                        arrayList.Add(strText.Substring(num2));
                    }
                    result = (string[])arrayList.ToArray(typeof(string));
                }
            }
            return result;
        }

        private static int smethod_0(string string_4, string string_5, int int_0, int int_1)
        {
            if (string_4 == null)
            {
                throw new ArgumentNullException("strText");
            }
            if (string_5 == null)
            {
                throw new ArgumentNullException("strSearch");
            }
            int num = string_4.IndexOf(string_5, int_0);
            int result;
            if (num == -1)
            {
                result = -1;
            }
            else
            {
                int_0 += string_5.Length;
                while (true)
                {
                    int num2 = string_4.IndexOf(string_5, int_0);
                    if (num2 == -1 || num2 >= int_1)
                    {
                        break;
                    }
                    num = num2;
                    int_0 += string_5.Length;
                }
                result = num;
            }
            return result;
        }
    }

    public interface IVariableProvider
    {
        string[] AllNames
        {
            get;
        }

        bool Exists(string Name);

        string Get(string Name);

        void Set(string Name, string Value);
    }
}
