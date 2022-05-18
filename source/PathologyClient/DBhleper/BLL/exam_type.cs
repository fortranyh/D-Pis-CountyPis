using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
namespace DBhleper.BLL
{
   public class exam_type
    {
       public DataTable GetBigTypeDt()
       {
           DataTable dt = null ;
           string sqlstr = "select modality,default_templet_index from exam_type_dict ";
           try
           {
               dt =  DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetBigTypeDt 执行语句：" + sqlstr);
           }
           return dt;
       }

       //获取报告超时限制值 
       public DataTable GetReportLimit(string w_big_type)
       {
           DataTable dt = null;
           string sqlstr = " select modality,report_limit from exam_type_dict where big_type in (" + w_big_type + ")";
           try
           {
               dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetReportLimit 执行语句：" + sqlstr);
           }
           return dt;
       }

       //获取报告超时限制值 
       public DataTable GetReportLimitFromModality(string modality)
       {
           DataTable dt = null;
           string sqlstr = " select modality,report_limit from exam_type_dict where modality ='" + modality + "'";
           try
           {
               dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetReportLimitFromModality 执行语句：" + sqlstr);
           }
           return dt;
       }

       public DataSet GetDsExam_Type(int flag, Boolean QcTypeFlag, string w_big_type)
       {
           DataSet ds = null;
           string sqlstr = "select modality,modality_cn,pre_char,sequence_name from exam_type_dict where enable_flag=1 and exam_flag=" + flag.ToString() + " order by id asc";
           if (QcTypeFlag)
           {

               sqlstr = "select modality,modality_cn,pre_char,sequence_name from exam_type_dict where enable_flag=1 and big_type in (" + w_big_type + ") and exam_flag=" + flag.ToString() + " order by id asc";
               
           }

           try
           {
               ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetDsExam_Type 执行语句：" + sqlstr);
           }
           return ds;
       }

       //所有检查大类
       public DataTable GetAllDTExam_Big_Type()
       {
           DataTable dt = null;
           string sqlstr = "select * from exam_big_type_dict where big_type_enable=1 order by id asc";
           try
           {
               dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetAllDTExam_Big_Type 执行语句：" + sqlstr);
           }
           return dt;
       }

       //所有检查小类
       public DataTable GetAllDTExam_Type()
       {
           DataTable dt = null;
           string sqlstr = "select * from exam_type_dict   order by id asc";
           try
           {
               dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetAllDTExam_Type 执行语句：" + sqlstr);
           }
           return dt;
       }

       public DataTable GetTjAllDTExam_Type(string w_big_type)
       {
           DataTable dt = null;
           string sqlstr = "select '-1' as  modality,'全部' as modality_cn union  all (select modality,modality_cn from exam_type_dict where big_type in("+w_big_type+")  order by id asc)";
           try
           {
               dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetTjAllDTExam_Type 执行语句：" + sqlstr);
           }
           return dt;
       }

       public DataTable GetTjAllDTBigExam_Type(string w_big_type)
       {
           DataTable dt = null;
           string sqlstr = "select '-1' as  big_type_code,'全部' as big_type_name union  all (select big_type_code,big_type_name from exam_big_type_dict where big_type_enable=1 and big_type_code in(" + w_big_type + ")  order by id asc)";
           try
           {
               dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetTjAllDTBigExam_Type 执行语句：" + sqlstr);
           }
           return dt;
       }
       

       public string GetPrechar(string modality)
       {
           string ResultStr = "";
           string sqlstr = "select pre_char from exam_type_dict where modality=@modality";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@modality", DbType.String, modality);
               ResultStr= DBProcess._db.ExecuteScalar(cmd).ToString();
              
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetPrechar 执行语句异常：" + sqlstr);
               ResultStr = "";
           }
           return ResultStr;
       }

       //获取大类
       public string GetBigType(string modality)
       {
           string ResultStr = "";
           string sqlstr = "select big_type from exam_type_dict where modality=@modality";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@modality", DbType.String, modality);
               ResultStr = DBProcess._db.ExecuteScalar(cmd).ToString();

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetBigType 执行语句异常：" + sqlstr);
               ResultStr = "";
           }
           return ResultStr;
       }

    }
}
