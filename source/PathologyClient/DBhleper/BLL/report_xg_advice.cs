using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DBhleper.BLL
{
  public  class report_xg_advice
    {
      public DataTable GetXgAdvice(string study_no)
      {
          DataTable dt = null;
          string sqlstr = "select id,advice,doc_code,doc_name, date_format(create_dt,'%Y-%m-%d %H:%i:%s') AS create_dt from report_xg_advice where study_no=@study_no order by id asc";
          try
          {
              DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
              DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
              dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
          }
          catch (Exception ex)
          {
              DBProcess.ShowException(ex, "GetXgAdvice 执行语句：" + sqlstr);
          }
          return dt;
      }

      public int GetXgAdviceCount(string study_no, DateTime create_dt)
      {
          
          string sqlstr = "select id,advice,doc_code,doc_name, date_format(create_dt,'%Y-%m-%d %H:%i:%s') AS create_dt from report_xg_advice where study_no=@study_no and create_dt=@create_dt order by id asc";
          try
          {
              DataTable dt = null;
              DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
              DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
              DBProcess._db.AddInParameter(cmd, "@create_dt", DbType.String, create_dt);
              dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
              return dt.Rows.Count;
          }
          catch (Exception ex)
          {
              DBProcess.ShowException(ex, "GetXgAdviceCount 执行语句：" + sqlstr);
          }
          return 0;
      }
      public Boolean InsertYAdvice(string study_no, string advice, string doc_code, string doc_name, DateTime create_dt)
      {
          Boolean Zx_Result = false;
          string sqlstr = "insert into report_xg_advice(study_no,advice,doc_code,doc_name,create_dt) values(@study_no,@advice,@doc_code,@doc_name,@create_dt)";
          try
          {
              DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
              DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
              DBProcess._db.AddInParameter(cmd, "@advice", DbType.String, advice);
              DBProcess._db.AddInParameter(cmd, "@doc_code", DbType.String, doc_code);
              DBProcess._db.AddInParameter(cmd, "@doc_name", DbType.String, doc_name);
              DBProcess._db.AddInParameter(cmd, "@create_dt", DbType.DateTime, create_dt);
              if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
              {
                  Zx_Result = true;
              }
              else
              {
                  Zx_Result = false;
              }
          }
          catch (Exception ex)
          {
              DBProcess.ShowException(ex, "InsertAdvice 执行语句异常：" + sqlstr);
          }
          return Zx_Result;
      }

      public Boolean InsertAdvice(string study_no, string advice, string doc_code, string doc_name)
      {
          Boolean Zx_Result = false;
          string sqlstr = "insert into report_xg_advice(study_no,advice,doc_code,doc_name,create_dt) values(@study_no,@advice,@doc_code,@doc_name,@create_dt)";
          try
          {
                  DbCommand  cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                  DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                  DBProcess._db.AddInParameter(cmd, "@advice", DbType.String, advice);
                  DBProcess._db.AddInParameter(cmd, "@doc_code", DbType.String, doc_code);
                  DBProcess._db.AddInParameter(cmd, "@doc_name", DbType.String, doc_name);
                  DBProcess._db.AddInParameter(cmd, "@create_dt", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                  if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                  {
                      Zx_Result = true;
                  }
                  else
                  {
                      Zx_Result = false;
                  }
          }
          catch (Exception ex)
          {
              DBProcess.ShowException(ex, "InsertAdvice 执行语句异常：" + sqlstr);
          }
          return Zx_Result;
      }

      public Boolean modityAdvice(int id, string advice)
      {
          Boolean Zx_Result = false;
          string sqlstr = "update report_xg_advice set advice=@advice where id=@id";
          try
          {
              DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
              DBProcess._db.AddInParameter(cmd, "@advice", DbType.String, advice);
              DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
              if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
              {
                  Zx_Result = true;
              }
              else
              {
                  Zx_Result = false;
              }
          }
          catch (Exception ex)
          {
              DBProcess.ShowException(ex, "modityAdvice 执行语句异常：" + sqlstr);
          }
          return Zx_Result;

      }
      public DataTable GetdtAdvice(string tj)
      {
          DataTable dt = null;
          string sqlstr = "select study_no,advice,doc_name, date_format(create_dt,'%Y-%m-%d %H:%i:%s') AS create_dt from report_xg_advice where " + tj + " order by id asc";
          try
          {
              DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
              dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
          }
          catch (Exception ex)
          {
              DBProcess.ShowException(ex, "GetdtAdvice 执行语句：" + sqlstr);
          }
          return dt;
      }
      public Boolean DelAdvice(int id)
      {
          Boolean Zx_Result = false;
          string sqlstr = "delete from  report_xg_advice where id=@id";
          try
          {
              DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
              DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
              if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
              {
                  Zx_Result = true;
              }
              else
              {
                  Zx_Result = false;
              }
          }
          catch (Exception ex)
          {
              DBProcess.ShowException(ex, "DelAdvice 执行语句异常：" + sqlstr);
          }
          return Zx_Result;

      }

    }
}
