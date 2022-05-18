using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBhleper.BLL
{
    public class charge_dict
    {
        public DataSet GetDsCharge_dict()
        {
            DataSet ds = null;
            string sqlstr = "select charge_code,charge_name,costs,py_code,bz from charge_dict where enable=1  order by id asc";
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsCharge_dict 执行语句：" + sqlstr);
            }
            return ds;
        }
       
    }
}
