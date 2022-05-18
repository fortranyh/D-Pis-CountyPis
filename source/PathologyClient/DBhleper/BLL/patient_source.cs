using System;
using System.Collections.Generic;
using System.Data;

namespace DBHelper.BLL
{
    public class patient_source
    {
        public List<string> GetPatient_source()
        {
            List<string> lst = new List<string>();
            string sqlstr = "select source_name from patient_source_dict order by order_no asc";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(ds.Tables[0].Rows[i]["source_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetPatient_source 执行语句：" + sqlstr);
            }
            return lst;
        }


        public DataTable GetTjPatient_source()
        {
            DataTable dt = null;
            string sqlstr = "select '-1' as  id,'全部' as source_name union  all (select id, source_name from patient_source_dict order by order_no asc)";
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetTjPatient_source 执行语句：" + sqlstr);
            }
            return dt;
        }

    }
}
