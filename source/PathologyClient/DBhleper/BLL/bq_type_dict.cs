using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBhleper.BLL
{
  public  class bq_type_dict
    {
      public DataTable GetAllData()
      {
          string sqlstr = "select '0' as bq_code,'全部' as bq_name union  all  (select bq_code,bq_name from bq_type_dict  order by bq_index asc)";

          DataTable dt = null;
          try
          {
              dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];

          }
          catch (Exception ex)
          {
              DBProcess.ShowException(ex, "GetData 执行语句：" + sqlstr);
              dt = null;
          }
          return dt;
      }
      public DataTable GetData()
      {
          string sqlstr = "select bq_code,bq_name from bq_type_dict  order by bq_index asc";

          DataTable dt = null;
          try
          {
              dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];

          }
          catch (Exception ex)
          {
              DBProcess.ShowException(ex, "GetData 执行语句：" + sqlstr);
              dt = null;
          }
          return dt;
      }

      public DataTable GetDataJsyz()
      {
          string sqlstr = "select bq_code,bq_name from bq_type_dict where bq_enable=1 order by bq_index asc";

          DataTable dt = null;
          try
          {
              dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];

          }
          catch (Exception ex)
          {
              DBProcess.ShowException(ex, "GetDataJsyz 执行语句：" + sqlstr);
              dt = null;
          }
          return dt;
      }

    }
}
