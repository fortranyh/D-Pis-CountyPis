using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DBhleper.BLL
{
   public class exam_bc_report
    {
       public Boolean SaveReport(Model.exam_bc_report insM)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_bc_report where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, insM.study_no);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 0)
                {
                    //插入
                    sqlstr = "insert into exam_bc_report(study_no,content,report_doc_name,report_doc_code,report_datetime) values(@study_no,@content,@report_doc_name,@report_doc_code,@report_datetime)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, insM.study_no);
                    DBProcess._db.AddInParameter(cmd, "@content", DbType.String, insM.content);
                    DBProcess._db.AddInParameter(cmd, "@report_doc_name", DbType.String, insM.report_doc_name);
                    DBProcess._db.AddInParameter(cmd, "@report_doc_code", DbType.String, insM.report_doc_code);
                    DBProcess._db.AddInParameter(cmd, "@report_datetime", DbType.DateTime, insM.report_datetime);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                    }
                    else
                    {
                        Zx_Result = false;
                    }
                }
                else if (sl == 1)
                {

                    //更新
                    sqlstr = "update exam_bc_report set  content=@content,report_doc_name=@report_doc_name,report_doc_code=@report_doc_code,report_datetime=@report_datetime where study_no=@study_no";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, insM.study_no);
                    DBProcess._db.AddInParameter(cmd, "@content", DbType.String, insM.content);
                    DBProcess._db.AddInParameter(cmd, "@report_doc_name", DbType.String, insM.report_doc_name);
                    DBProcess._db.AddInParameter(cmd, "@report_doc_code", DbType.String, insM.report_doc_code);
                    DBProcess._db.AddInParameter(cmd, "@report_datetime", DbType.DateTime, insM.report_datetime);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;

                    }
                    else
                    {
                        Zx_Result = false;

                    }
                }

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "SaveReport 执行语句异常：" + sqlstr);
                Zx_Result = false;
            }
            return Zx_Result;
        }

        public DataTable GetData(string study_no)
        {

            string sqlstr = "select study_no,content,report_doc_name,report_doc_code,sh_doc_name,sh_doc_code,sh_flag,date_format(sh_datetime,'%Y-%m-%d %H:%i:%s') as sh_datetime,date_format(report_datetime,'%Y-%m-%d %H:%i:%s') as report_datetime from exam_bc_report where study_no=@study_no";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetData 执行语句异常：" + sqlstr);
            }
            return dt;

        }
        //获取审核的延时报告列表
        public DataTable GetShReportList(string tj)
        {
            string sqlstr = "select study_no,content,report_doc_name,sh_doc_name, date_format(sh_datetime,'%Y-%m-%d %H:%i:%s') AS    sh_datetime, date_format(report_datetime,'%Y-%m-%d %H:%i:%s') AS report_datetime from exam_bc_report where 1=1 " + tj;
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetShReportList 执行语句异常：" + sqlstr);
            }
            return dt;

        }

        public Boolean ShReport(string study_no, string sh_datetime, int sh_flag, string sh_doc_code, string sh_doc_name)
        {

            string sqlstr = "update exam_bc_report set  sh_doc_name=@sh_doc_name,sh_doc_code=@sh_doc_code,sh_flag=@sh_flag,sh_datetime=@sh_datetime where study_no=@study_no";
            Boolean result = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@sh_doc_name", DbType.String, sh_doc_name);
                DBProcess._db.AddInParameter(cmd, "@sh_doc_code", DbType.String, sh_doc_code);
                DBProcess._db.AddInParameter(cmd, "@sh_flag", DbType.Int16, sh_flag);
                DBProcess._db.AddInParameter(cmd, "@sh_datetime", DbType.DateTime, sh_datetime);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                int icount = DBProcess._db.ExecuteNonQuery(cmd);
                if (icount == 1)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetData 执行语句异常：" + sqlstr);
            }
            return result;
        }
    }
}
