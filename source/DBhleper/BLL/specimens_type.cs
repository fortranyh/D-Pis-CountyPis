using System;
using System.Collections.Generic;
using System.Data;
namespace DBHelper.BLL
{
    public class specimens_type
    {

        public List<string> Getspecimens_type(int Stype)
        {
            List<string> lst = new List<string>();
            string sqlstr = "select specimens_name from specimens_type_dict where big_or_small=" + Stype + " order by id asc";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(ds.Tables[0].Rows[i]["specimens_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetMospecimens_type 执行语句：" + sqlstr);
            }
            return lst;
        }


    }
}
