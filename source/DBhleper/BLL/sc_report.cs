using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    public class sc_report
    {

        public DataTable getScDatetime(string study_no)
        {
            DataTable dt = null;
            string sqlstr = "select zdbm,gjc,memo_note,date_format(create_datetime,'%Y-%m-%d %H:%i:%s') as create_datetime from sc_report where study_no='" + study_no + "'";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "getScDatetime 执行语句异常：" + sqlstr);
            }
            return dt;


        }
        public Boolean InsertReportInfo(string study_no, string zdbm, string gjc, string memo_note, string doc_code, string doc_name)
        {
            Boolean Zx_Result = false;
            string sqlstr = "insert into sc_report(study_no,zdbm,gjc,memo_note,doc_code,doc_name)values(@study_no,@zdbm,@gjc,@memo_note,@doc_code,@doc_name)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@zdbm", DbType.String, zdbm);
                DBProcess._db.AddInParameter(cmd, "@gjc", DbType.String, gjc);
                DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
                DBProcess._db.AddInParameter(cmd, "@doc_code", DbType.String, doc_code);
                DBProcess._db.AddInParameter(cmd, "@doc_name", DbType.String, doc_name);
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
                DBProcess.ShowException(ex, "InsertReportInfo 执行语句异常：" + sqlstr);
            }
            return Zx_Result;

        }

    }
}
