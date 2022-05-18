using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DBhleper.BLL
{
   public class specimen_process_dict
    {
       public DataTable Get_specimen_process_dict()
       {
           DataTable dt = null;
           string sqlstr = "select '000' as process_code ,'' as process_name union  all select process_code,process_name from specimen_process_dict order by process_code asc";
           try
           {
               dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "Get_specimen_process_dict 执行语句：" + sqlstr);
               dt = null;
           }
           return dt;
       }

    }
}
