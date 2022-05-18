using System.Collections;
using System.Data;
using System.Xml;

namespace ASwartz.WinForms.Controls
{
    public class ASDataSource
    {
        public enum Enum17
        {
            const_0,
            const_1,
            const_2,
            const_3
        }

        private object object_0 = null;

        private string string_0 = null;

        private ASDataSourceFieldList ASDataSourceFieldList_0 = new ASDataSourceFieldList();

        private bool bool_0 = false;

        private int _int_0 = 0;
        public int int_0
        {
            get
            {
                return _int_0;
            }
            set
            {
                this._int_0 = value;
            }
        }
        private ASDataSource.Enum17 _enum17_0 = ASDataSource.Enum17.const_2;
        public ASDataSource.Enum17 enum17_0
        {
            get
            {
                return _enum17_0;
            }
            set
            {
                this._enum17_0 = value;
            }
        }
        private IEnumerator ienumerator_0 = null;

        public object DataSource
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

        public string RootXPath
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

        public ASDataSourceFieldList Fields
        {
            get
            {
                return this.ASDataSourceFieldList_0;
            }
            set
            {
                this.ASDataSourceFieldList_0 = value;
            }
        }

        public object Current
        {
            get
            {
                object result;
                if (this.ienumerator_0 == null)
                {
                    result = null;
                }
                else
                {
                    result = this.ienumerator_0.Current;
                }
                return result;
            }
        }

        private void method_0()
        {
            if (!this.bool_0)
            {
                this.bool_0 = true;
                this.Start();
            }
        }

        public void Start()
        {
            int_0 = 0;
            int num = 0;
            foreach (ASDataSourceField field in Fields)
            {
                field.int_1 = num++;
            }
            if (object_0 == null)
            {
                foreach (ASDataSourceField field2 in Fields)
                {
                    field2.bool_0 = true;
                }
            }
            else
            {
                if (object_0 is DataTable)
                {
                    DataTable dataTable = (DataTable)object_0;
                    enum17_0 = Enum17.const_0;
                    ienumerator_0 = dataTable.Rows.GetEnumerator();
                    foreach (ASDataSourceField field3 in Fields)
                    {
                        field3.int_0 = dataTable.Columns.IndexOf(field3.FieldName);
                        if (field3.int_0 >= 0)
                        {
                            field3.enum17_0 = Enum17.const_0;
                            field3.bool_0 = false;
                        }
                        else
                        {
                            field3.bool_0 = true;
                        }
                    }
                }
                else if (object_0 is DataView)
                {
                    DataView dataView = (DataView)object_0;
                    enum17_0 = Enum17.const_0;
                    ienumerator_0 = dataView.GetEnumerator();
                    foreach (ASDataSourceField field4 in Fields)
                    {
                        field4.int_0 = dataView.Table.Columns.IndexOf(field4.FieldName);
                        if (field4.int_0 >= 0)
                        {
                            field4.enum17_0 = Enum17.const_0;
                            field4.bool_0 = false;
                        }
                        else
                        {
                            field4.bool_0 = true;
                        }
                    }
                }
                else if (object_0 is XmlNode)
                {
                    XmlNode xmlNode = (XmlNode)object_0;
                    if (!string.IsNullOrEmpty(RootXPath))
                    {
                        XmlNodeList xmlNodeList = xmlNode.SelectNodes(RootXPath);
                        ienumerator_0 = xmlNodeList.GetEnumerator();
                    }
                    else
                    {
                        ienumerator_0 = xmlNode.ChildNodes.GetEnumerator();
                    }
                    enum17_0 = Enum17.const_3;
                    foreach (ASDataSourceField field5 in Fields)
                    {
                        field5.bool_0 = false;
                        field5.enum17_0 = Enum17.const_3;
                    }
                }
                else if (object_0 is XmlNodeList)
                {
                    XmlNodeList xmlNodeList = (XmlNodeList)object_0;
                    enum17_0 = Enum17.const_3;
                    ienumerator_0 = xmlNodeList.GetEnumerator();
                    foreach (ASDataSourceField field6 in Fields)
                    {
                        field6.bool_0 = false;
                        field6.enum17_0 = Enum17.const_3;
                    }
                }
                else if (object_0 is IEnumerable)
                {
                    IEnumerable enumerable = (IEnumerable)object_0;
                    ienumerator_0 = enumerable.GetEnumerator();
                    enum17_0 = Enum17.const_2;
                    foreach (ASDataSourceField field7 in Fields)
                    {
                        field7.bool_0 = false;
                        field7.enum17_0 = Enum17.const_2;
                    }
                }
                foreach (ASDataSourceField field8 in Fields)
                {
                    if (string.IsNullOrEmpty(field8.FieldName))
                    {
                        field8.bool_0 = true;
                    }
                }
            }
        }
        public void Reset()
        {
            this.Start();
            if (this.ienumerator_0 != null)
            {
                this.ienumerator_0.Reset();
            }
        }

        public bool MoveNext()
        {
            return this.ienumerator_0 != null && this.ienumerator_0.MoveNext();
        }

        public object ReadValue(string fieldName)
        {
            ASSingleDataSource ASSingleDataSource = ASSingleDataSource.Package(this.Current);
            return ASSingleDataSource.ReadValue(fieldName);
        }
    }
}
