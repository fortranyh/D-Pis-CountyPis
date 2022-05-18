using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    public class huizhen_info
    {
        public DataTable GetHzinfo(string study_no)
        {
            DataTable dt = null;
            string sqlstr = "select id,study_no,doc_no,doc_name,chry,zjyj, date_format(create_dt,'%Y-%m-%d %H:%i:%s') AS create_dt from huizhen_info where study_no=@study_no order by id asc";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetHzinfo 执行语句：" + sqlstr);
            }
            return dt;
        }

        public int GetHzCount(string study_no)
        {
            int zxResult = 0;
            string sqlstr = "select count(*) from huizhen_info where study_no=@study_no order by id asc";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                zxResult = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetHzCount 执行语句：" + sqlstr);
                zxResult = 0;
            }
            return zxResult;
        }

        public Boolean InsertHzInfo(string study_no, string chry, string zjyj, string doc_no, string doc_name)
        {
            Boolean Zx_Result = false;
            string sqlstr = "insert into huizhen_info(study_no,chry,zjyj,doc_no,doc_name,create_dt) values(@study_no,@chry,@zjyj,@doc_no,@doc_name,@create_dt)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@chry", DbType.String, chry);
                DBProcess._db.AddInParameter(cmd, "@zjyj", DbType.String, zjyj);
                DBProcess._db.AddInParameter(cmd, "@doc_no", DbType.String, doc_no);
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
                DBProcess.ShowException(ex, "InsertHzInfo 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }

        public Boolean modityZjyj(string study_no, string zjyj)
        {
            Boolean Zx_Result = false;
            string sqlstr = "update huizhen_info set zjyj=@zjyj where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@zjyj", DbType.String, zjyj);
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
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "modityZjyj 执行语句异常：" + sqlstr);
            }
            return Zx_Result;

        }
    }
}
