using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    public class myzh_report
    {
        public DataTable GetData(string study_no)
        {

            string sqlstr = "select study_no,rs_func,zh_md,content,report_doc,date_format(report_dt,'%Y-%m-%d %H:%i:%s') AS report_dt from myzh_report where study_no=@study_no";
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

        public Boolean SaveReport(string study_no, string rs_func, string zh_md, string content, string report_doc, string report_dt)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from myzh_report where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 0)
                {
                    //插入
                    sqlstr = "insert into myzh_report(study_no,rs_func,zh_md,content,report_doc,report_dt) values(@study_no,@rs_func,@zh_md,@content,@report_doc,@report_dt)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                    DBProcess._db.AddInParameter(cmd, "@rs_func", DbType.String, rs_func);
                    DBProcess._db.AddInParameter(cmd, "@zh_md", DbType.String, zh_md);
                    DBProcess._db.AddInParameter(cmd, "@content", DbType.String, content);
                    DBProcess._db.AddInParameter(cmd, "@report_doc", DbType.String, report_doc);
                    DBProcess._db.AddInParameter(cmd, "@report_dt", DbType.DateTime, report_dt);
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
                    sqlstr = "update myzh_report set rs_func=@rs_func,zh_md=@zh_md,content=@content,report_doc=@report_doc,report_dt=@report_dt where study_no=@study_no";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@rs_func", DbType.String, rs_func);
                    DBProcess._db.AddInParameter(cmd, "@zh_md", DbType.String, zh_md);
                    DBProcess._db.AddInParameter(cmd, "@content", DbType.String, content);
                    DBProcess._db.AddInParameter(cmd, "@report_doc", DbType.String, report_doc);
                    DBProcess._db.AddInParameter(cmd, "@report_dt", DbType.DateTime, report_dt);
                    DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
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



    }
}
