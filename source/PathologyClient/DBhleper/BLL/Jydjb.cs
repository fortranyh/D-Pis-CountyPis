using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    public class Jydjb
    {

        public int AddInfo(string study_no, string patient_name, string jcw_num, string byblzd, string yj, string jcr, string jctime, string hzhospital, string other, string memo_note)
        {
            string sqlstr = "insert into jydjb(study_no,patient_name,jcw_num,byblzd,yj,jcr,jctime,hzhospital,other,memo_note)values(@study_no,@patient_name,@jcw_num,@byblzd,@yj,@jcr,@jctime,@hzhospital,@other,@memo_note)";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@patient_name", DbType.String, patient_name);
                DBProcess._db.AddInParameter(cmd, "@jcw_num", DbType.String, jcw_num);
                DBProcess._db.AddInParameter(cmd, "@byblzd", DbType.String, byblzd);
                DBProcess._db.AddInParameter(cmd, "@yj", DbType.String, yj);
                DBProcess._db.AddInParameter(cmd, "@jcr", DbType.String, jcr);
                DBProcess._db.AddInParameter(cmd, "@jctime", DbType.DateTime, jctime);
                DBProcess._db.AddInParameter(cmd, "@hzhospital", DbType.String, hzhospital);
                DBProcess._db.AddInParameter(cmd, "@other", DbType.String, other);
                DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "AddInfo 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public DataTable GetInfo(string tj)
        {
            string sqlstr = "select id,study_no,patient_name,jcw_num,byblzd,yj,jcr, date_format(jctime,'%Y-%m-%d %H:%i:%s') AS jctime,hzhospital,wyblzd,date_format(ghtime,'%Y-%m-%d %H:%i:%s') AS ghtime,ghr,other,memo_note from jydjb where " + tj;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetInfo 执行语句异常：" + sqlstr);
            }
            return null;
        }


        public int UpdateInfo(string study_no, string patient_name, string jcw_num, string byblzd, string yj, string jcr, string jctime, string hzhospital, string other, string memo_note, string ghtime, string ghr, string wyblzd, string id)
        {
            string sqlstr = "update jydjb set study_no=@study_no, patient_name=@patient_name,jcw_num=@jcw_num,byblzd=@byblzd,yj=@yj,jcr=@jcr,jctime=@jctime,hzhospital=@hzhospital,other=@other,memo_note=@memo_note,ghtime=@ghtime,ghr=@ghr,wyblzd=@wyblzd where id=@id";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@patient_name", DbType.String, patient_name);
                DBProcess._db.AddInParameter(cmd, "@jcw_num", DbType.String, jcw_num);
                DBProcess._db.AddInParameter(cmd, "@byblzd", DbType.String, byblzd);
                DBProcess._db.AddInParameter(cmd, "@yj", DbType.String, yj);
                DBProcess._db.AddInParameter(cmd, "@jcr", DbType.String, jcr);
                DBProcess._db.AddInParameter(cmd, "@jctime", DbType.DateTime, jctime);
                DBProcess._db.AddInParameter(cmd, "@hzhospital", DbType.String, hzhospital);
                DBProcess._db.AddInParameter(cmd, "@other", DbType.String, other);
                DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
                DBProcess._db.AddInParameter(cmd, "@ghtime", DbType.DateTime, ghtime);
                DBProcess._db.AddInParameter(cmd, "@ghr", DbType.String, ghr);
                DBProcess._db.AddInParameter(cmd, "@wyblzd", DbType.String, wyblzd);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "patient_name 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

    }
}
