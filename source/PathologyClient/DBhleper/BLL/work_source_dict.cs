using System;
using System.Collections.Generic;
using System.Data;
namespace DBHelper.BLL
{
    public class work_source_dict
    {
        public DataTable Get_work_source_dict()
        {
            DataTable dt = null;
            string sqlstr = "select '000' as work_code,'' as work_name union all select work_code,work_name from work_source_dict order by work_code asc";
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Get_work_source_dict 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }


        public List<string> Getwork_source()
        {
            List<string> lst = new List<string>();
            string sqlstr = "select work_name from work_source_dict  order by work_code asc";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(ds.Tables[0].Rows[i]["work_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Getwork_source 执行语句：" + sqlstr);
            }
            return lst;
        }
    }


}
