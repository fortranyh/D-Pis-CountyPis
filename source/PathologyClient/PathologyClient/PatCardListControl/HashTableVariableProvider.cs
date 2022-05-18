using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{
	[ComVisible(false)]
	public class HashTableVariableProvider : IVariableProvider
	{
		private Hashtable hashtable_0 = null;

		public Hashtable Values
		{
			get
			{
				return this.hashtable_0;
			}
		}

		public string[] AllNames
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object current in this.hashtable_0.Keys)
				{
					arrayList.Add(Convert.ToString(current));
				}
				return (string[])arrayList.ToArray(typeof(string));
			}
		}

		public HashTableVariableProvider()
		{
			this.hashtable_0 = new Hashtable();
		}

		public HashTableVariableProvider(Hashtable vars)
		{
			this.hashtable_0 = vars;
		}

		public void Set(string Name, string Value)
		{
			this.hashtable_0[Name] = Value;
		}

		public bool Exists(string Name)
		{
			bool result;
			foreach (object current in this.hashtable_0.Keys)
			{
				if (Convert.ToString(current) == Name)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}

		public string Get(string Name)
		{
			string result;
			foreach (object current in this.hashtable_0.Keys)
			{
				if (Convert.ToString(current) == Name)
				{
					result = Convert.ToString(this.hashtable_0[current]);
					return result;
				}
			}
			result = null;
			return result;
		}
	}
}
