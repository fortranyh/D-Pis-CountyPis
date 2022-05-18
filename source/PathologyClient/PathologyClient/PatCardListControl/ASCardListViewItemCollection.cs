using System.Collections.Generic;

namespace ASwartz.WinForms.Controls
{

    public class ASCardListViewItemCollection : List<ASCardListViewItem>
    {
        public ASCardListViewItem GetItemByDataBoundItem(object dataBoundItem)
        {
            ASCardListViewItem result;
            foreach (ASCardListViewItem current in this)
            {
                if (current.DataBoundItem == dataBoundItem)
                {
                    result = current;
                    return result;
                }
            }
            result = null;
            return result;
        }
    }
}
