
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;

namespace ASwartz.WinForms.Controls
{
    [ComVisible(false)]
    public class ASSingleDataSource
    {
        private object object_0 = null;

        public object DataBoundItem
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

        public static ASSingleDataSource Package(object instance)
        {
            ASSingleDataSource result;
            if (instance == null)
            {
                result = null;
            }
            else if (instance is ASSingleDataSource)
            {
                result = (ASSingleDataSource)instance;
            }
            else
            {
                result = new ASSingleDataSource(instance);
            }
            return result;
        }

        public ASSingleDataSource()
        {
        }

        public ASSingleDataSource(object dataBoundItem)
        {
            this.object_0 = dataBoundItem;
        }

        public object ReadValue(string fieldName)
        {
            return ASSingleDataSource.ReadValue(this.object_0, fieldName);
        }

        public bool WriteValue(string fieldName, object fieldValue)
        {
            return ASSingleDataSource.WriteValue(this.object_0, fieldName, fieldValue);
        }

        public string[] GetFieldNames()
        {
            return ASSingleDataSource.GetFieldNames(this.object_0);
        }

        public static bool WriteValue(object dataBoundItem, string fieldName, object fieldValue)
        {
            bool result;
            if (dataBoundItem == null)
            {
                result = false;
            }
            else if (dataBoundItem is DataRow)
            {
                DataRow dataRow = (DataRow)dataBoundItem;
                dataRow[fieldName] = fieldValue;
                result = true;
            }
            else
            {
                if (dataBoundItem is DataRowView)
                {
                    DataRowView dataRowView = (DataRowView)dataBoundItem;
                    dataRowView[fieldName] = fieldValue;
                }
                if (dataBoundItem is DataTable)
                {
                    DataTable dataTable = (DataTable)dataBoundItem;
                    if (dataTable.Rows.Count > 0)
                    {
                        dataTable.Rows[0][fieldName] = fieldValue;
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else if (dataBoundItem is DataView)
                {
                    DataView dataView = (DataView)dataBoundItem;
                    if (dataView.Count > 0)
                    {
                        DataRowView dataRowView = dataView[0];
                        dataRowView[fieldName] = fieldValue;
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                else if (dataBoundItem is IDictionary)
                {
                    IDictionary dictionary = (IDictionary)dataBoundItem;
                    dictionary[fieldName] = fieldValue;
                    result = true;
                }
                else if (dataBoundItem is XmlNode)
                {
                    XmlNode rootNode = (XmlNode)dataBoundItem;
                    XmlNode xmlNode = XMLHelper.CreateXMLNodeByPath(rootNode, fieldName, 1, null);
                    if (xmlNode != null)
                    {
                        if (fieldValue == null || DBNull.Value.Equals(fieldValue))
                        {
                            xmlNode.Value = "";
                        }
                        else
                        {
                            xmlNode.Value = Convert.ToString(fieldValue);
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
                    if (dataBoundItem is XmlNodeList)
                    {
                        XmlNodeList xmlNodeList = (XmlNodeList)dataBoundItem;
                        if (xmlNodeList.Count > 0)
                        {
                            XmlNode rootNode = xmlNodeList[0];
                            XmlNode xmlNode = XMLHelper.CreateXMLNodeByPath(rootNode, fieldName, 1, null);
                            if (xmlNode == null)
                            {
                                result = false;
                                return result;
                            }
                            if (fieldValue == null || DBNull.Value.Equals(fieldValue))
                            {
                                xmlNode.Value = "";
                            }
                            else
                            {
                                xmlNode.Value = Convert.ToString(fieldValue);
                            }
                            result = true;
                            return result;
                        }
                    }
                    if (dataBoundItem.GetType().IsArray)
                    {
                        Array array = (Array)dataBoundItem;
                        if (array.GetLength(0) == 0)
                        {
                            result = false;
                        }
                        else
                        {
                            object value = array.GetValue(0);
                            result = ASSingleDataSource.WriteValue(value, fieldName, fieldValue);
                        }
                    }
                    else
                    {
                        result = ValueTypeHelper.SetPropertyValueMultiLayer(dataBoundItem, fieldName, fieldValue, false);
                    }
                }
            }
            return result;
        }

        public static object ReadValue(object dataBoundItem, string fieldName)
        {
            object result;
            if (dataBoundItem == null)
            {
                result = null;
            }
            else if (dataBoundItem is DataRow)
            {
                DataRow dataRow = (DataRow)dataBoundItem;
                result = dataRow[fieldName];
            }
            else if (dataBoundItem is DataRowView)
            {
                DataRowView dataRowView = (DataRowView)dataBoundItem;
                result = dataRowView[fieldName];
            }
            else if (dataBoundItem is DataTable)
            {
                DataTable dataTable = (DataTable)dataBoundItem;
                if (dataTable.Rows.Count > 0)
                {
                    result = dataTable.Rows[0][fieldName];
                }
                else
                {
                    result = null;
                }
            }
            else if (dataBoundItem is DataView)
            {
                DataView dataView = (DataView)dataBoundItem;
                if (dataView.Count > 0)
                {
                    DataRowView dataRowView = dataView[0];
                    result = dataRowView[fieldName];
                }
                else
                {
                    result = null;
                }
            }
            else if (dataBoundItem is IDictionary)
            {
                IDictionary dictionary = (IDictionary)dataBoundItem;
                if (dictionary.Contains(fieldName))
                {
                    result = dictionary[fieldName];
                }
                else
                {
                    result = null;
                }
            }
            else if (dataBoundItem is XmlNode)
            {
                XmlNode xmlNode = (XmlNode)dataBoundItem;
                XmlNode xmlNode2 = xmlNode.SelectSingleNode(fieldName);
                if (xmlNode2 == null)
                {
                    result = null;
                }
                else
                {
                    result = xmlNode2.Value;
                }
            }
            else
            {
                if (dataBoundItem is XmlNodeList)
                {
                    XmlNodeList xmlNodeList = (XmlNodeList)dataBoundItem;
                    if (xmlNodeList.Count > 0)
                    {
                        XmlNode xmlNode = xmlNodeList[0];
                        XmlNode xmlNode2 = xmlNode.SelectSingleNode(fieldName);
                        if (xmlNode2 == null)
                        {
                            result = null;
                            return result;
                        }
                        result = xmlNode2.Value;
                        return result;
                    }
                }
                if (dataBoundItem.GetType().IsArray)
                {
                    Array array = (Array)dataBoundItem;
                    if (array.GetLength(0) == 0)
                    {
                        result = null;
                    }
                    else
                    {
                        object value = array.GetValue(0);
                        result = ASSingleDataSource.ReadValue(value, fieldName);
                    }
                }
                else
                {
                    result = ValueTypeHelper.GetPropertyValue(dataBoundItem, fieldName, false);
                }
            }
            return result;
        }

        public static string[] GetFieldNames(object dataBoundItem)
        {
            if (dataBoundItem == null)
            {
                return null;
            }
            List<string> list = new List<string>();
            if (dataBoundItem is DataRow)
            {
                DataRow dataRow = (DataRow)dataBoundItem;
                DataTable table = dataRow.Table;
                foreach (DataColumn column in table.Columns)
                {
                    list.Add(column.ColumnName);
                }
            }
            else if (dataBoundItem is DataRowView)
            {
                DataRowView dataRowView = (DataRowView)dataBoundItem;
                foreach (DataColumn column2 in dataRowView.DataView.Table.Columns)
                {
                    list.Add(column2.ColumnName);
                }
            }
            else if (dataBoundItem is DataTable)
            {
                DataTable table = (DataTable)dataBoundItem;
                foreach (DataColumn column3 in table.Columns)
                {
                    list.Add(column3.ColumnName);
                }
            }
            else if (dataBoundItem is DataView)
            {
                DataView dataView = (DataView)dataBoundItem;
                foreach (DataColumn column4 in dataView.Table.Columns)
                {
                    list.Add(column4.ColumnName);
                }
            }
            else if (dataBoundItem is IDictionary)
            {
                IDictionary dictionary = (IDictionary)dataBoundItem;
                foreach (object key in dictionary.Keys)
                {
                    list.Add(Convert.ToString(key));
                }
            }
            else if (dataBoundItem is XmlNode)
            {
                XmlNode xmlNode = (XmlNode)dataBoundItem;
                foreach (XmlNode childNode in xmlNode.ChildNodes)
                {
                    if (childNode is XmlElement)
                    {
                        list.Add(childNode.Name);
                    }
                }
            }
            else if (dataBoundItem is XmlNodeList)
            {
                XmlNodeList xmlNodeList = (XmlNodeList)dataBoundItem;
                if (xmlNodeList.Count > 0)
                {
                    XmlNode xmlNode = xmlNodeList[0];
                    foreach (XmlNode childNode2 in xmlNode.ChildNodes)
                    {
                        if (childNode2 is XmlElement)
                        {
                            list.Add(childNode2.Name);
                        }
                    }
                }
            }
            else if (dataBoundItem.GetType().IsArray)
            {
                Type elementType = dataBoundItem.GetType().GetElementType();
                PropertyInfo[] properties = elementType.GetProperties();
                if (properties != null)
                {
                    PropertyInfo[] array = properties;
                    foreach (PropertyInfo propertyInfo in array)
                    {
                        list.Add(propertyInfo.Name);
                    }
                }
            }
            else
            {
                PropertyInfo[] properties = dataBoundItem.GetType().GetProperties();
                if (properties != null)
                {
                    PropertyInfo[] array = properties;
                    foreach (PropertyInfo propertyInfo in array)
                    {
                        list.Add(propertyInfo.Name);
                    }
                }
            }
            return list.ToArray();
        }
    }
}
