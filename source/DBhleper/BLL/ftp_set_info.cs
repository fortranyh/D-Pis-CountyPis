using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    public class ftp_set_info
    {
        //查询配置信息
        public DataTable GetData()
        {
            string sqlstr = "select ftpip,ftpport,ftpuser,ftppwd from ftp_set_info";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0].Rows.Count == 1)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetData 执行语句：" + sqlstr);
            }
            return null;
        }

        public void UpdateFtp(string ftpuser, string ftppwd, string ftpip, int ftpport)
        {
            string sqlstr = "update ftp_set_info set ftpip=@ftpip,ftpport=@ftpport,ftpuser=@ftpuser,ftppwd=@ftppwd";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@ftpip", DbType.String, ftpip);
                DBProcess._db.AddInParameter(cmd, "@ftpport", DbType.Int16, ftpport);
                DBProcess._db.AddInParameter(cmd, "@ftpuser", DbType.String, ftpuser);
                DBProcess._db.AddInParameter(cmd, "@ftppwd", DbType.String, ftppwd);
                DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateFtp 执行语句：" + sqlstr);
            }
        }
    }
}
