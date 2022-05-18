using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ASwartz.WinForms.Controls
{
	[ComVisible(false)]
	public sealed class ValueTypeHelper
	{
		private static Dictionary<Type, Dictionary<string, PropertyInfo>> dictionary_0 = new Dictionary<Type, Dictionary<string, PropertyInfo>>();

		private static Hashtable hashtable_0 = null;

		public static object CallMethodByName(object instance, string methodName, object[] parameters, bool throwException)
		{
			object result;
			if (instance == null)
			{
				if (throwException)
				{
					throw new ArgumentNullException("instance");
				}
				result = null;
			}
			else if (string.IsNullOrEmpty(methodName))
			{
				if (throwException)
				{
					throw new ArgumentNullException("methodName");
				}
				result = null;
			}
			else
			{
				MethodInfo[] methods = instance.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public);
				for (int i = 0; i < methods.Length; i++)
				{
					MethodInfo methodInfo = methods[i];
					if (string.Compare(methodInfo.Name, methodName, true) == 0)
					{
						ParameterInfo[] parameters2 = methodInfo.GetParameters();
						int num = (parameters2 == null) ? 0 : parameters2.Length;
						int num2 = (parameters == null) ? 0 : parameters.Length;
						if (num == num2)
						{
							if (throwException)
							{
								object[] array = null;
								if (num > 0)
								{
									array = new object[num];
									for (int j = 0; j < parameters.Length; j++)
									{
										if (parameters[j] != null)
										{
											if (parameters2[j].ParameterType.IsInstanceOfType(parameters[j]))
											{
												array[j] = parameters[j];
											}
											else
											{
												array[j] = ValueTypeHelper.ConvertTo(parameters[j], parameters2[j].ParameterType);
											}
										}
										else
										{
											array[j] = ValueTypeHelper.GetDefaultValue(parameters2[j].ParameterType);
										}
									}
								}
								object obj = methodInfo.Invoke(instance, array);
								result = obj;
								return result;
							}
							try
							{
								object[] array = null;
								if (num > 0)
								{
									array = new object[num];
									for (int j = 0; j < parameters.Length; j++)
									{
										if (parameters[j] != null)
										{
											if (parameters2[j].ParameterType.IsInstanceOfType(parameters[j]))
											{
												array[j] = parameters[j];
											}
											else
											{
												array[j] = ValueTypeHelper.ConvertTo(parameters[j], parameters2[j].ParameterType);
											}
										}
										else
										{
											array[j] = ValueTypeHelper.GetDefaultValue(parameters2[j].ParameterType);
										}
									}
								}
								object obj = methodInfo.Invoke(instance, array);
								result = obj;
							}
							catch (Exception ex)
							{
								Debug.WriteLine(ex.Message);
								result = null;
							}
							return result;
						}
					}
				}
				if (throwException)
				{
					throw new ArgumentException(instance.GetType().FullName + "." + methodName);
				}
				result = null;
			}
			return result;
		}

        public static bool SetPropertyValueMultiLayer(object instance, string propertyName, object Value, bool throwExecption)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentNullException("propertyName");
            }
            string[] array = propertyName.Split('.');
            object obj = instance;
            int num = 0;
            while (true)
            {
                if (num >= array.Length)
                {
                    return false;
                }
                string text = array[num].Trim();
                if (!string.IsNullOrEmpty(text))
                {
                    if (num == array.Length - 1)
                    {
                        return SetPropertyValue(obj, text, Value, throwExecption);
                    }
                    PropertyInfo property = obj.GetType().GetProperty(text, BindingFlags.Instance | BindingFlags.Public);
                    if (property == null && throwExecption)
                    {
                        throw new Exception("未找到属性" + obj.GetType().FullName + "." + text);
                    }
                    object value = property.GetValue(obj, null);
                    if (value == null)
                    {
                        break;
                    }
                    obj = value;
                }
                num++;
            }
            return false;
        }
		public static bool SetPropertyValue(object instance, string propertyName, object Value, bool throwException)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (string.IsNullOrEmpty(propertyName))
			{
				throw new ArgumentNullException("propertyName");
			}
			bool result;
			foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(instance))
			{
				if (!propertyDescriptor.IsReadOnly && string.Compare(propertyDescriptor.Name, propertyName, true) == 0)
				{
					object obj = Value;
					if (Value != null && !propertyDescriptor.PropertyType.IsInstanceOfType(obj))
					{
						if (throwException)
						{
							if (propertyDescriptor.Converter != null)
							{
								obj = propertyDescriptor.Converter.ConvertFrom(obj);
							}
							else
							{
								obj = Convert.ChangeType(obj, propertyDescriptor.PropertyType);
							}
						}
						else
						{
							try
							{
								if (propertyDescriptor.Converter != null)
								{
									obj = propertyDescriptor.Converter.ConvertFrom(obj);
								}
								else
								{
									if (propertyDescriptor.PropertyType.IsEnum)
									{
										if (obj is string)
										{
											obj = Enum.Parse(propertyDescriptor.PropertyType, (string)obj);
										}
										else
										{
											obj = Enum.ToObject(propertyDescriptor.PropertyType, obj);
										}
									}
									obj = Convert.ChangeType(obj, propertyDescriptor.PropertyType);
								}
							}
							catch (Exception)
							{
								result = false;
								return result;
							}
						}
					}
					if (!throwException)
					{
						try
						{
							propertyDescriptor.SetValue(instance, obj);
							result = true;
							return result;
						}
						catch (Exception)
						{
							result = false;
							return result;
						}
					}
					propertyDescriptor.SetValue(instance, obj);
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public static string FormStringWithPropertyValue(string formatString, object instance, bool throwException)
		{
			string result;
			if (string.IsNullOrEmpty(formatString))
			{
				result = formatString;
			}
			else
			{
				string[] array = VariableString.AnalyseVariableString(formatString, "{", "}");
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					if (i % 2 == 0)
					{
						stringBuilder.Append(array[i]);
					}
					else
					{
						string text = array[i];
						string text2 = null;
						int num = text.IndexOf(':');
						if (num > 0)
						{
							text2 = text.Substring(num + 1);
							text = text.Substring(0, num);
						}
						object propertyValue = ValueTypeHelper.GetPropertyValue(instance, text, throwException);
						if (propertyValue != null)
						{
							if (!string.IsNullOrEmpty(text2) && propertyValue is IFormattable)
							{
								string value = ((IFormattable)propertyValue).ToString(text2, null);
								stringBuilder.Append(value);
							}
							else
							{
								stringBuilder.Append(Convert.ToString(propertyValue));
							}
						}
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		public static object GetPropertyValue(object instance, string propertyName, bool throwException)
		{
			object result;
			if (instance == null)
			{
				if (throwException)
				{
					throw new ArgumentNullException("instance");
				}
				result = null;
			}
			else if (string.IsNullOrEmpty(propertyName))
			{
				if (throwException)
				{
					throw new ArgumentNullException("propertyName");
				}
				result = null;
			}
			else
			{
				Type type = instance.GetType();
				Dictionary<string, PropertyInfo> dictionary;
				if (ValueTypeHelper.dictionary_0.ContainsKey(type))
				{
					dictionary = ValueTypeHelper.dictionary_0[type];
				}
				else
				{
					dictionary = new Dictionary<string, PropertyInfo>();
					ValueTypeHelper.dictionary_0[type] = dictionary;
				}
				PropertyInfo propertyInfo;
				if (dictionary.ContainsKey(propertyName))
				{
					propertyInfo = dictionary[propertyName];
				}
				else
				{
					dictionary[propertyName] = null;
					propertyInfo = instance.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
					if (propertyInfo == null)
					{
						if (throwException)
						{
							throw new Exception("未找到属性" + propertyName);
						}
						result = null;
						return result;
					}
					else if (!propertyInfo.CanRead)
					{
						if (throwException)
						{
							throw new Exception("属性" + propertyName + "无法读取数据");
						}
						result = null;
						return result;
					}
					else
					{
						ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
						if (indexParameters != null && indexParameters.Length > 0)
						{
							if (throwException)
							{
								throw new Exception("属性" + propertyName + "不得有参数");
							}
							result = null;
							return result;
						}
						else
						{
							dictionary[propertyName] = propertyInfo;
						}
					}
				}
				if (propertyInfo == null)
				{
					if (throwException)
					{
						throw new Exception("没有合适的属性" + propertyName);
					}
					result = null;
				}
				else if (throwException)
				{
					result = propertyInfo.GetValue(instance, null);
				}
				else
				{
					try
					{
						result = propertyInfo.GetValue(instance, null);
					}
					catch
					{
						result = null;
					}
				}
			}
			return result;
		}

		public static double ObjectToDouble(object object_0, double DefaultValue)
		{
			double result;
			if (object_0 == null || DBNull.Value.Equals(object_0))
			{
				result = DefaultValue;
			}
			else if (object_0 is byte || object_0 is short || object_0 is int || object_0 is long || object_0 is decimal || object_0 is double)
			{
				result = (double)object_0;
			}
			else
			{
				result = ValueTypeHelper.StringToDouble(Convert.ToString(object_0), DefaultValue);
			}
			return result;
		}

		public static double StringToDouble(string string_0, double DefaultValue)
		{
			double result;
			if (string_0 == null)
			{
				result = DefaultValue;
			}
			else
			{
				string_0 = string_0.Trim();
				if (string_0.Length == 0)
				{
					result = DefaultValue;
				}
				else
				{
					double num = 0.0;
					if (double.TryParse(string_0, NumberStyles.Any, null, out num))
					{
						result = num;
					}
					else
					{
						result = DefaultValue;
					}
				}
			}
			return result;
		}

		public static object GetDefaultValue(Type ValueType)
		{
			if (ValueTypeHelper.hashtable_0 == null)
			{
				ValueTypeHelper.hashtable_0 = new Hashtable();
				ValueTypeHelper.hashtable_0[typeof(object)] = null;
				ValueTypeHelper.hashtable_0[typeof(byte)] = 0;
				ValueTypeHelper.hashtable_0[typeof(sbyte)] = 0;
				ValueTypeHelper.hashtable_0[typeof(short)] = 0;
				ValueTypeHelper.hashtable_0[typeof(ushort)] = 0;
				ValueTypeHelper.hashtable_0[typeof(int)] = 0;
				ValueTypeHelper.hashtable_0[typeof(uint)] = 0u;
				ValueTypeHelper.hashtable_0[typeof(long)] = 0L;
				ValueTypeHelper.hashtable_0[typeof(ulong)] = 0uL;
				ValueTypeHelper.hashtable_0[typeof(char)] = '\0';
				ValueTypeHelper.hashtable_0[typeof(float)] = 0f;
				ValueTypeHelper.hashtable_0[typeof(double)] = 0.0;
				ValueTypeHelper.hashtable_0[typeof(decimal)] = 0m;
				ValueTypeHelper.hashtable_0[typeof(bool)] = false;
				ValueTypeHelper.hashtable_0[typeof(string)] = null;
				ValueTypeHelper.hashtable_0[typeof(DateTime)] = DateTime.MinValue;
				ValueTypeHelper.hashtable_0[typeof(Point)] = Point.Empty;
				ValueTypeHelper.hashtable_0[typeof(PointF)] = PointF.Empty;
				ValueTypeHelper.hashtable_0[typeof(Size)] = Size.Empty;
				ValueTypeHelper.hashtable_0[typeof(SizeF)] = SizeF.Empty;
				ValueTypeHelper.hashtable_0[typeof(Rectangle)] = Rectangle.Empty;
				ValueTypeHelper.hashtable_0[typeof(RectangleF)] = RectangleF.Empty;
				ValueTypeHelper.hashtable_0[typeof(Color)] = Color.Transparent;
			}
			if (ValueType == null)
			{
				throw new ArgumentNullException("ValueType");
			}
			object result;
			if (ValueTypeHelper.hashtable_0.ContainsKey(ValueType))
			{
				result = ValueTypeHelper.hashtable_0[ValueType];
			}
			else if (ValueType.IsValueType)
			{
				result = Activator.CreateInstance(ValueType);
			}
			else
			{
				result = null;
			}
			return result;
		}

		public static bool TryConvertTo(object Value, Type NewType, ref object Result)
		{
			if (NewType == null)
			{
				throw new ArgumentNullException("NewType");
			}
			bool result;
			if (Value == null || DBNull.Value.Equals(Value))
			{
				if (NewType.IsClass)
				{
					Result = null;
					result = true;
				}
				else
				{
					result = false;
				}
			}
			else
			{
				Type type = Value.GetType();
				if (type.Equals(NewType) || type.IsSubclassOf(NewType))
				{
					Result = Value;
					result = true;
				}
				else
				{
					try
					{
						bool flag = type.Equals(typeof(string));
						if (NewType.Equals(typeof(string)))
						{
							if (flag)
							{
								Result = (string)Value;
							}
							else
							{
								Result = Convert.ToString(Value);
							}
							result = true;
						}
						else if (NewType.Equals(typeof(bool)))
						{
							if (flag)
							{
								bool flag2 = false;
								if (ValueTypeHelper.TryParseBoolean((string)Value, out flag2))
								{
									Result = flag2;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToBoolean(Value);
								result = true;
							}
						}
						else if (NewType.Equals(typeof(char)))
						{
							if (flag)
							{
								char c = '\0';
								if (ValueTypeHelper.TryParseChar((string)Value, out c))
								{
									Result = c;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToChar(Value);
								result = true;
							}
						}
						else if (NewType.Equals(typeof(byte)))
						{
							if (flag)
							{
								byte b = 0;
								if (ValueTypeHelper.TryParseByte((string)Value, out b))
								{
									Result = b;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToByte(Value);
								result = true;
							}
						}
						else if (NewType.Equals(typeof(sbyte)))
						{
							if (flag)
							{
								sbyte b2 = 0;
								if (ValueTypeHelper.TryParseSByte((string)Value, out b2))
								{
									Result = b2;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToSByte(Value);
								result = true;
							}
						}
						else if (NewType.Equals(typeof(short)))
						{
							if (flag)
							{
								short num = 0;
								if (ValueTypeHelper.TryParseInt16((string)Value, out num))
								{
									Result = num;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToInt16(Value);
								result = true;
							}
						}
						else if (NewType.Equals(typeof(ushort)))
						{
							if (flag)
							{
								ushort num2 = 0;
								if (ValueTypeHelper.TryParseUInt16((string)Value, out num2))
								{
									Result = num2;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToUInt16(Value);
								result = true;
							}
						}
						else if (NewType.Equals(typeof(int)))
						{
							if (flag)
							{
								int num3 = 0;
								if (ValueTypeHelper.TryParseInt32((string)Value, out num3))
								{
									Result = num3;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToInt32(Value);
								result = true;
							}
						}
						else if (NewType.Equals(typeof(uint)))
						{
							if (flag)
							{
								uint num4 = 0u;
								if (ValueTypeHelper.TryParseUInt32((string)Value, out num4))
								{
									Result = num4;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToUInt32(Value);
								result = true;
							}
						}
						else if (NewType.Equals(typeof(long)))
						{
							if (flag)
							{
								long num5 = 0L;
								if (ValueTypeHelper.TryParseInt64((string)Value, out num5))
								{
									Result = num5;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToInt64(Value);
								result = true;
							}
						}
						else if (NewType.Equals(typeof(ulong)))
						{
							if (flag)
							{
								ulong num6 = 0uL;
								if (ValueTypeHelper.TryParseUInt64((string)Value, out num6))
								{
									Result = num6;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToUInt64(Value);
								result = true;
							}
						}
						else if (NewType.Equals(typeof(float)))
						{
							if (flag)
							{
								float num7 = 0f;
								if (ValueTypeHelper.TryParseSingle((string)Value, out num7))
								{
									Result = num7;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToSingle(Value);
								result = true;
							}
						}
						else if (NewType.Equals(typeof(double)))
						{
							if (flag)
							{
								double num8 = 0.0;
								if (ValueTypeHelper.TryParseDouble((string)Value, out num8))
								{
									Result = num8;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToDouble(Value);
								result = true;
							}
						}
						else if (NewType.Equals(typeof(decimal)))
						{
							if (flag)
							{
								decimal num9 = 0m;
								if (ValueTypeHelper.TryParseDecimal((string)Value, out num9))
								{
									Result = num9;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToDecimal(Convert.ToSingle(Value));
								result = true;
							}
						}
						else if (NewType.Equals(typeof(DateTime)))
						{
							if (flag)
							{
								DateTime minValue = DateTime.MinValue;
								if (ValueTypeHelper.TryParseDateTime((string)Value, out minValue))
								{
									Result = minValue;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = Convert.ToDateTime(Value);
								result = true;
							}
						}
						else if (NewType.Equals(typeof(TimeSpan)))
						{
							if (flag)
							{
								TimeSpan zero = TimeSpan.Zero;
								if (ValueTypeHelper.TryParseTimeSpan((string)Value, out zero))
								{
									Result = zero;
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else
							{
								Result = new TimeSpan(Convert.ToInt64(Value));
								result = true;
							}
						}
						else if (NewType.Equals(typeof(byte[])))
						{
							if (flag)
							{
								try
								{
									byte[] array = Convert.FromBase64String((string)Value);
									Result = array;
									result = true;
									return result;
								}
								catch
								{
									result = false;
									return result;
								}
							}
							result = false;
						}
						else if (NewType.IsEnum)
						{
							if (Enum.IsDefined(type, Value))
							{
								if (flag)
								{
									Result = Enum.Parse(NewType, (string)Value, true);
								}
								else
								{
									Result = Enum.ToObject(NewType, Value);
								}
								result = true;
							}
							else
							{
								result = false;
							}
						}
						else
						{
							TypeConverter converter = TypeDescriptor.GetConverter(NewType);
							if (converter != null)
							{
								if (converter.CanConvertFrom(Value.GetType()))
								{
									Result = converter.ConvertFrom(Value);
									result = true;
								}
								else
								{
									result = false;
								}
							}
							else if (Value is IConvertible)
							{
								Result = ((IConvertible)Value).ToType(NewType, null);
								result = true;
							}
							else
							{
								Result = Convert.ChangeType(Value, NewType);
								result = true;
							}
						}
					}
					catch
					{
						result = false;
					}
				}
			}
			return result;
		}

		public static object ConvertTo(object Value, Type NewType, object DefaultValue)
		{
			if (NewType == null)
			{
				throw new ArgumentNullException("NewType");
			}
			object result;
			if (Value == null || DBNull.Value.Equals(Value))
			{
				result = DefaultValue;
			}
			else
			{
				Type type = Value.GetType();
				if (type.Equals(NewType) || type.IsSubclassOf(NewType))
				{
					result = Value;
				}
				else if (NewType.Equals(typeof(string)))
				{
					result = Convert.ToString(Value);
				}
				else if (NewType.Equals(typeof(bool)))
				{
					if (Value is string)
					{
						result = bool.Parse((string)Value);
					}
					else
					{
						result = Convert.ToBoolean(Value);
					}
				}
				else
				{
					try
					{
						if (NewType.Equals(typeof(char)))
						{
							result = Convert.ToChar(Value);
						}
						else if (NewType.Equals(typeof(byte)))
						{
							result = Convert.ToByte(Value);
						}
						else if (NewType.Equals(typeof(sbyte)))
						{
							result = Convert.ToSByte(Value);
						}
						else if (NewType.Equals(typeof(short)))
						{
							result = Convert.ToInt16(Value);
						}
						else if (NewType.Equals(typeof(ushort)))
						{
							result = Convert.ToUInt16(Value);
						}
						else if (NewType.Equals(typeof(int)))
						{
							result = Convert.ToInt32(Value);
						}
						else if (NewType.Equals(typeof(uint)))
						{
							result = Convert.ToUInt32(Value);
						}
						else if (NewType.Equals(typeof(long)))
						{
							result = Convert.ToInt64(Value);
						}
						else if (NewType.Equals(typeof(ulong)))
						{
							result = Convert.ToUInt64(Value);
						}
						else if (NewType.Equals(typeof(float)))
						{
							result = Convert.ToSingle(Value);
						}
						else if (NewType.Equals(typeof(double)))
						{
							result = Convert.ToDouble(Value);
						}
						else if (NewType.Equals(typeof(decimal)))
						{
							decimal num = Convert.ToDecimal(Convert.ToSingle(Value));
							result = num;
						}
						else if (NewType.Equals(typeof(DateTime)))
						{
							DateTime dateTime = DateTime.MinValue;
							if (type.Equals(typeof(string)))
							{
								dateTime = DateTime.Parse((string)Value);
							}
							else
							{
								dateTime = Convert.ToDateTime(Value);
							}
							result = dateTime;
						}
						else if (NewType.Equals(typeof(TimeSpan)))
						{
							TimeSpan timeSpan = TimeSpan.Zero;
							if (type.Equals(typeof(string)))
							{
								timeSpan = TimeSpan.Parse((string)Value);
							}
							else
							{
								timeSpan = TimeSpan.Parse(Convert.ToString(Value));
							}
							result = timeSpan;
						}
						else if (NewType.Equals(typeof(byte[])))
						{
							if (type.Equals(typeof(string)))
							{
								byte[] array = Convert.FromBase64String((string)Value);
								result = array;
							}
							else
							{
								result = null;
							}
						}
						else if (NewType.IsEnum)
						{
							if (Value is string)
							{
								result = Enum.Parse(NewType, (string)Value);
							}
							else
							{
								result = Enum.ToObject(NewType, Value);
							}
						}
						else
						{
							TypeConverter converter = TypeDescriptor.GetConverter(NewType);
							if (converter != null)
							{
								if (converter.CanConvertFrom(Value.GetType()))
								{
									result = converter.ConvertFrom(Value);
								}
								else
								{
									result = DefaultValue;
								}
							}
							else if (Value is IConvertible)
							{
								result = ((IConvertible)Value).ToType(NewType, null);
							}
							else
							{
								result = Convert.ChangeType(Value, NewType);
							}
						}
					}
					catch
					{
						result = DefaultValue;
					}
				}
			}
			return result;
		}

		public static object ConvertTo(object Value, Type NewType)
		{
			if (NewType == null)
			{
				throw new ArgumentNullException("NewType");
			}
			object result;
			if (NewType.IsInstanceOfType(Value))
			{
				result = Value;
			}
			else
			{
				bool flag = false;
				if (Value is string)
				{
					string text = (string)Value;
					if (text == null || text.Trim().Length == 0)
					{
						flag = true;
					}
				}
				if (Value == null || DBNull.Value.Equals(Value))
				{
					if (NewType.IsClass)
					{
						result = null;
					}
					else
					{
						result = ValueTypeHelper.GetDefaultValue(NewType);
					}
				}
				else
				{
					Type type = Value.GetType();
					if (type.Equals(NewType) || type.IsSubclassOf(NewType))
					{
						result = Value;
					}
					else if (NewType.Equals(typeof(string)))
					{
						result = Convert.ToString(Value);
					}
					else if (NewType.Equals(typeof(bool)))
					{
						if (Value is string)
						{
							result = bool.Parse((string)Value);
						}
						else if (flag)
						{
							result = false;
						}
						else
						{
							result = Convert.ToBoolean(Value);
						}
					}
					else if (NewType.Equals(typeof(char)))
					{
						result = Convert.ToChar(Value);
					}
					else if (NewType.Equals(typeof(byte)))
					{
						if (flag)
						{
							result = 0;
						}
						else
						{
							result = Convert.ToByte(Value);
						}
					}
					else if (NewType.Equals(typeof(sbyte)))
					{
						if (flag)
						{
							result = 0;
						}
						else
						{
							result = Convert.ToSByte(Value);
						}
					}
					else if (NewType.Equals(typeof(short)))
					{
						if (flag)
						{
							result = 0;
						}
						else
						{
							result = Convert.ToInt16(Value);
						}
					}
					else if (NewType.Equals(typeof(ushort)))
					{
						if (flag)
						{
							result = 0;
						}
						else
						{
							result = Convert.ToUInt16(Value);
						}
					}
					else if (NewType.Equals(typeof(int)))
					{
						if (flag)
						{
							result = 0;
						}
						else
						{
							result = Convert.ToInt32(Value);
						}
					}
					else if (NewType.Equals(typeof(uint)))
					{
						if (flag)
						{
							result = 0u;
						}
						else
						{
							result = Convert.ToUInt32(Value);
						}
					}
					else if (NewType.Equals(typeof(long)))
					{
						if (flag)
						{
							result = 0L;
						}
						else
						{
							result = Convert.ToInt64(Value);
						}
					}
					else if (NewType.Equals(typeof(ulong)))
					{
						if (flag)
						{
							result = 0uL;
						}
						else
						{
							result = Convert.ToUInt64(Value);
						}
					}
					else if (NewType.Equals(typeof(float)))
					{
						if (flag)
						{
							result = 0f;
						}
						else
						{
							result = Convert.ToSingle(Value);
						}
					}
					else if (NewType.Equals(typeof(double)))
					{
						if (flag)
						{
							result = 0.0;
						}
						else
						{
							result = Convert.ToDouble(Value);
						}
					}
					else if (NewType.Equals(typeof(decimal)))
					{
						if (flag)
						{
							result = 0m;
						}
						else
						{
							decimal num = Convert.ToDecimal(Convert.ToSingle(Value));
							result = num;
						}
					}
					else if (NewType.Equals(typeof(DateTime)))
					{
						if (flag)
						{
							result = DateTime.MinValue;
						}
						else
						{
							DateTime dateTime = DateTime.MinValue;
							if (type.Equals(typeof(string)))
							{
								dateTime = DateTime.Parse((string)Value);
							}
							else
							{
								dateTime = Convert.ToDateTime(Value);
							}
							result = dateTime;
						}
					}
					else if (NewType.Equals(typeof(TimeSpan)))
					{
						if (flag)
						{
							result = TimeSpan.Zero;
						}
						else
						{
							TimeSpan timeSpan = TimeSpan.Zero;
							if (type.Equals(typeof(string)))
							{
								timeSpan = TimeSpan.Parse((string)Value);
							}
							else
							{
								timeSpan = TimeSpan.Parse(Convert.ToString(Value));
							}
							result = timeSpan;
						}
					}
					else if (NewType.IsEnum)
					{
						if (Value is string)
						{
							result = Enum.Parse(NewType, (string)Value);
						}
						else
						{
							result = Enum.ToObject(NewType, Convert.ToInt32(Value));
						}
					}
					else
					{
						TypeConverter converter = TypeDescriptor.GetConverter(NewType);
						if (converter != null)
						{
							if (!converter.CanConvertFrom(Value.GetType()))
							{
								throw new ArgumentException("Value");
							}
							result = converter.ConvertFrom(Value);
						}
						else if (Value is IConvertible)
						{
							result = ((IConvertible)Value).ToType(NewType, null);
						}
						else
						{
							result = Convert.ChangeType(Value, NewType);
						}
					}
				}
			}
			return result;
		}

		public static Enum ParseEnum(string Value, Enum DefaultValue)
		{
			string[] names = Enum.GetNames(DefaultValue.GetType());
			string[] array = names;
			Enum result;
			for (int i = 0; i < array.Length; i++)
			{
				string strA = array[i];
				if (string.Compare(strA, Value, true) == 0)
				{
					result = (Enum)Enum.Parse(DefaultValue.GetType(), Value);
					return result;
				}
			}
			result = DefaultValue;
			return result;
		}

		public static bool TryParseTimeSpan(string Value, out TimeSpan Result)
		{
			return TimeSpan.TryParse(Value, out Result);
		}

		public static bool TryParseDateTime(string Value, out DateTime Result)
		{
			Result = DateTime.MinValue;
			bool result;
			if (Value == null || Value.Trim().Length == 0)
			{
				result = false;
			}
			else
			{
				Value = Value.Trim();
				result = DateTime.TryParse(Value, out Result);
			}
			return result;
		}

		public static bool TryParseDateTimeExt(string Value, out DateTime Result)
		{
            Result = DateTime.MinValue;
            if (Value == null || Value.Trim().Length == 0)
            {
                return false;
            }
            Value = Value.Trim();
            bool flag = true;
            string text = Value;
            foreach (char value in text)
            {
                if ("0123456789".IndexOf(value) < 0)
                {
                    flag = false;
                    break;
                }
            }
            if (flag)
            {
                if (Value.Length < 4)
                {
                    return false;
                }
                int num = 1;
                int num2 = 1;
                int num3 = 0;
                int num4 = 0;
                int num5 = 0;
                int year = Convert.ToInt32(Value.Substring(0, 4));
                if (Value.Length >= 6)
                {
                    num = Convert.ToInt32(Value.Substring(4, 2));
                    if (num <= 0 || num > 12)
                    {
                        return false;
                    }
                }
                if (Value.Length >= 8)
                {
                    num2 = Convert.ToInt32(Value.Substring(6, 2));
                    if (num2 <= 0 || num2 > DateTime.DaysInMonth(year, num))
                    {
                        return false;
                    }
                }
                if (Value.Length >= 10)
                {
                    num3 = Convert.ToInt32(Value.Substring(8, 2));
                    if (num3 > 24)
                    {
                        return false;
                    }
                }
                if (Value.Length >= 12)
                {
                    num4 = Convert.ToInt32(Value.Substring(10, 2));
                    if (num4 > 60)
                    {
                        return false;
                    }
                }
                if (Value.Length >= 14)
                {
                    num5 = Convert.ToInt32(Value.Substring(12, 2));
                    if (num5 > 60)
                    {
                        return false;
                    }
                }
                Result = new DateTime(year, num, num2, num3, num4, num5);
                return true;
            }
            return TryParseDateTime(Value, out Result);
		}

		public static bool TryParseDecimal(string Value, out decimal Result)
		{
			return decimal.TryParse(Value, out Result);
		}

		public static bool TryParseDouble(string Value, out double Result)
		{
			return double.TryParse(Value, out Result);
		}

		public static bool TryParseSingle(string Value, out float Result)
		{
			return float.TryParse(Value, out Result);
		}

		public static bool TryParseChar(string Value, out char Result)
		{
			Result = '\0';
			bool result;
			if (Value != null && Value.Length == 1)
			{
				Result = Value[0];
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public static bool TryParseUInt64(string Value, out ulong Result)
		{
			return ulong.TryParse(Value, out Result);
		}

		public static bool TryParseInt64(string Value, out long Result)
		{
			return long.TryParse(Value, out Result);
		}

		public static bool TryParseUInt32(string Value, out uint Result)
		{
			return uint.TryParse(Value, out Result);
		}

		public static bool TryParseInt32(string Value, out int Result)
		{
			return int.TryParse(Value, out Result);
		}

		public static bool TryParseUInt16(string Value, out ushort Result)
		{
			return ushort.TryParse(Value, out Result);
		}

		public static bool TryParseInt16(string Value, out short Result)
		{
			return short.TryParse(Value, out Result);
		}

		public static bool TryParseByte(string Value, out byte Result)
		{
			return byte.TryParse(Value, out Result);
		}

		public static bool TryParseSByte(string Value, out sbyte Result)
		{
			return sbyte.TryParse(Value, out Result);
		}

		private static bool smethod_0(string string_0, string string_1)
		{
			bool result;
			if (string_0 != null && string_0.Length > 0)
			{
				for (int i = 0; i < string_0.Length; i++)
				{
					char value = string_0[i];
					if (string_1.IndexOf(value) < 0)
					{
						result = false;
						return result;
					}
				}
			}
			result = true;
			return result;
		}

		public static bool TryParseBoolean(string Value, out bool Result)
		{
			Result = false;
			bool result;
			if (Value != null)
			{
				Value = Value.Trim();
				if (Value == "0")
				{
					Result = false;
					result = true;
					return result;
				}
				if (Value == "1")
				{
					Result = true;
					result = true;
					return result;
				}
				if (string.Compare("True", Value, true) == 0)
				{
					Result = true;
					result = true;
					return result;
				}
				if (string.Compare("False", Value, true) == 0)
				{
					Result = false;
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		private ValueTypeHelper()
		{
		}
	}
}
