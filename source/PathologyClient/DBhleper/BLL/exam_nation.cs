using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DBhleper.BLL
{
   public class exam_nation
    {
       public DataSet GetDsExam_Nation()
       {
           DataSet ds = null;
           string sqlstr = "select id,nation_name from exam_nation_dict order by order_num asc";
           try
           {
               ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetDsExam_Nation 执行语句：" + sqlstr);
           }
           return ds;
       }

      
    }
}
