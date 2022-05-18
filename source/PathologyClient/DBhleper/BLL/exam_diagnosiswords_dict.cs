using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DBhleper.BLL
{
    //诊断常用词模版
   public class exam_diagnosiswords_dict
    {
       public DataTable GetTreeExam_diagnosiswords_dict()
        {
            string sqlstr = "select id,part_name,parent_code,order_no from  exam_diagnosiswords_dict";

            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetTreeExam_diagnosiswords_dict 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
       public DataTable GetChildParts(string Pid)
       {
           string sqlstr = "select id from  exam_diagnosiswords_dict where parent_code='" + Pid + "'";
           DataTable dt = null;
           try
           {
               dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetChildParts 执行语句：" + sqlstr);
               dt = null;
           }
           return dt;
       }
       public DataTable GetDsExam_diagnosiswords_dict()
        {
            DataTable dt = null;
            string sqlstr = "select id,part_name,parent_code,order_no from  exam_diagnosiswords_dict";
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsExam_diagnosiswords_dict 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
       public string GetExam_diagnosiswords_Name(string id)
        {
            string result = "";
            string sqlstr = "select part_name from  exam_diagnosiswords_dict where id='" + id + "'";
            try
            {
                DataTable dt = DBProcess._db.ExecuteDataSet(sqlstr).Tables[0];
                if (dt != null && dt.Rows.Count == 1)
                {
                    result = dt.Rows[0]["part_name"].ToString();
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetExam_diagnosiswords_Name 执行语句：" + sqlstr);
            }
            return result;
        }

       //插入关键词
       public Boolean InsertParts(string id, string part_name, string parent_code)
       {
           Boolean zxResult = false;
           try
           {
               //插入
               string sqlstr = "insert into exam_diagnosiswords_dict(id,part_name,parent_code) values(@id,@part_name,@parent_code)";
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.String, id);
               DBProcess._db.AddInParameter(cmd, "@part_name", DbType.String, part_name);
               DBProcess._db.AddInParameter(cmd, "@parent_code", DbType.String, parent_code);
               if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
               {
                   zxResult = true;
               }
               else
               {
                   zxResult = false;
               }
           }
           catch
           {

           }

           return zxResult;
       }

       //删除关键词
       public Boolean DelParts(string id)
       {
           Boolean zxResult = false;
           string sqlstr = "delete from  exam_diagnosiswords_dict where  id='" + id + "'";

           try
           {
               DBProcess._db.ExecuteNonQuery(CommandType.Text, sqlstr);
               return true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "DelParts 执行语句：" + sqlstr);

           }
           return zxResult;
       }
       //更新关键词
       public int updatePartText(string part_name, string id)
       {
           string sqlstr = "update exam_diagnosiswords_dict set part_name=@part_name where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@part_name", DbType.String, part_name);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.String, id);
               if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
               {
                   return 1;
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "updatePartText 执行语句异常：" + sqlstr);
           }
           return 0;
       }




    }
}
