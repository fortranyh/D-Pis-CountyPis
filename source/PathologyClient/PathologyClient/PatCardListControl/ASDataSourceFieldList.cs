
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{

    public class ASDataSourceFieldList : List<ASDataSourceField>
	{
		public ASDataSourceField this[string fieldName]
		{
			get
			{
				ASDataSourceField result;
				foreach (ASDataSourceField current in this)
				{
					if (string.Compare(current.FieldName, fieldName, true) == 0)
					{
						result = current;
						return result;
					}
				}
				result = null;
				return result;
			}
		}

		public ASDataSourceField AddField(string fieldName)
		{
			ASDataSourceField ASDataSourceField = new ASDataSourceField();
			ASDataSourceField.FieldName = fieldName;
			base.Add(ASDataSourceField);
			return ASDataSourceField;
		}

		public ASDataSourceField AddField(string fieldName, string bindingPath)
		{
			ASDataSourceField ASDataSourceField = new ASDataSourceField();
			ASDataSourceField.FieldName = fieldName;
			ASDataSourceField.BindingPath = bindingPath;
			base.Add(ASDataSourceField);
			return ASDataSourceField;
		}
	}
}
