
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ASwartz.WinForms.Controls
{
	
	public class ASCardContentItemList : List<ASCardContentItem>
	{
		public ASCardContentItem GetItemByFieldName(string fieldName)
		{
			ASCardContentItem result;
			foreach (ASCardContentItem current in this)
			{
				if (string.Compare(current.DataField, fieldName, true) == 0)
				{
					result = current;
					return result;
				}
			}
			result = null;
			return result;
		}

		public ASCardImageListKeyItem AddImageListKey(string fieldName, string string_0, int left, int int_0, int width, int height)
		{
			ASCardImageListKeyItem ASCardImageListKeyItem = new ASCardImageListKeyItem();
			ASCardImageListKeyItem.DataField = fieldName;
			ASCardImageListKeyItem.ImageKey = string_0;
			ASCardImageListKeyItem.Left = left;
			ASCardImageListKeyItem.Top = int_0;
			ASCardImageListKeyItem.Width = width;
			ASCardImageListKeyItem.Height = height;
			base.Add(ASCardImageListKeyItem);
			return ASCardImageListKeyItem;
		}

		public ASCardStringItem AddString(string fieldName, string text, int left, int int_0, int width, int height)
		{
			ASCardStringItem ASCardStringItem = new ASCardStringItem();
			ASCardStringItem.DataField = fieldName;
			ASCardStringItem.Text = text;
			ASCardStringItem.Left = left;
			ASCardStringItem.Top = int_0;
			ASCardStringItem.Width = width;
			ASCardStringItem.Height = height;
			base.Add(ASCardStringItem);
			return ASCardStringItem;
		}

		public ASCardRectangleItem AddRectangle(Color borderColor, Color backColor, int left, int int_0, int width, int height)
		{
			ASCardRectangleItem ASCardRectangleItem = new ASCardRectangleItem();
			ASCardRectangleItem.BorderColor = borderColor;
			ASCardRectangleItem.BackColor = backColor;
			ASCardRectangleItem.Left = left;
			ASCardRectangleItem.Top = int_0;
			ASCardRectangleItem.Width = width;
			ASCardRectangleItem.Height = height;
			base.Add(ASCardRectangleItem);
			return ASCardRectangleItem;
		}

		public ASCardImageItem AddImage(string fieldName, Image image_0, int left, int int_0, int width, int height)
		{
			ASCardImageItem ASCardImageItem = new ASCardImageItem();
			ASCardImageItem.Image = image_0;
			ASCardImageItem.DataField = fieldName;
			ASCardImageItem.Left = left;
			ASCardImageItem.Top = int_0;
			ASCardImageItem.Width = width;
			ASCardImageItem.Height = height;
			base.Add(ASCardImageItem);
			return ASCardImageItem;
		}
	}
}
