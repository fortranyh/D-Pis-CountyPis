using System;
using System.Collections.Generic;
using System.Data;

namespace DBHelper.BLL
{
    public class group_dict
    {
        public DataTable Get_group_dict()
        {
            DataTable dt = null;
            string sqlstr = "select '00' as unite_code ,'' as unite_name union  all select unite_code,unite_name from group_dict order by unite_code asc";
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Get_group_dict 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }

        public List<string> Getunite_name()
        {
            List<string> lst = new List<string>();
            string sqlstr = "select  '' as unite_name union  all select unite_name from group_dict ";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(ds.Tables[0].Rows[i]["unite_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Getspecimens_type 执行语句：" + sqlstr);
            }
            return lst;
        }
    }
}
