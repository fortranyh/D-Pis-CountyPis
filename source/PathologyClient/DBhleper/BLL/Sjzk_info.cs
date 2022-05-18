using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DBhleper.BLL
{
    public class Sjzk_info
    {
        public int AddInfoMyzhsj(string name, string result, string date, string bz)
        {
            string sqlstr = "insert into tj_myzh_hgl(name,result,date,bz)values(@name,@result,@date,@bz)";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@name", DbType.String, name);
                DBProcess._db.AddInParameter(cmd, "@result", DbType.String, result);
                DBProcess._db.AddInParameter(cmd, "@date", DbType.String, date);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "AddInfoMyzhsj 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public DataTable GetInfoMyzhsj(string tj)
        {
            string sqlstr = "select id,name,result,date_format(date,'%Y-%m-%d %H:%i:%s') AS date,bz from tj_myzh_hgl where " + tj;
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
                DBProcess.ShowException(ex, "GetInfoMyzhsj 执行语句异常：" + sqlstr);
            }
            return null;
        }

        public int AddInfoFzblsj(string name, string result, string date, string bz)
        {
            string sqlstr = "insert into tj_fzbl_hgl(name,result,date,bz)values(@name,@result,@date,@bz)";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@name", DbType.String, name);
                DBProcess._db.AddInParameter(cmd, "@result", DbType.String, result);
                DBProcess._db.AddInParameter(cmd, "@date", DbType.String, date);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "AddInfoFzblsj 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public DataTable GetInfoFzblsj(string tj)
        {
            string sqlstr = "select id,name,result,date_format(date,'%Y-%m-%d %H:%i:%s') AS date,bz from tj_fzbl_hgl where " + tj;
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
                DBProcess.ShowException(ex, "GetInfoFzblsj 执行语句异常：" + sqlstr);
            }
            return null;
        }
        public int AddInfoXbzk(string num, string byyyx, string date, string bz,string byzd,string zkzd,string zkyyx,string qsh,string zzh )
        {
            string sqlstr = "insert into tj_xbzk_fhl(num,byyyx,date,bz,zkyyx,byzd,zkzd,qsh,zzh)values(@num,@byyyx,@date,@bz,@zkyyx,@byzd,@zkzd,@qsh,@zzh)";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@num", DbType.String, num);
                DBProcess._db.AddInParameter(cmd, "@byyyx", DbType.String, byyyx);
                DBProcess._db.AddInParameter(cmd, "@date", DbType.String, date);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
                DBProcess._db.AddInParameter(cmd, "@zkyyx", DbType.String, zkyyx);
                DBProcess._db.AddInParameter(cmd, "@byzd", DbType.String, byzd);
                DBProcess._db.AddInParameter(cmd, "@zkzd", DbType.String, zkzd);
                DBProcess._db.AddInParameter(cmd, "@qsh", DbType.String, qsh);
                DBProcess._db.AddInParameter(cmd, "@zzh", DbType.String, zzh);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "AddInfoXbzk 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public DataTable GetInfoXbzk(string tj)
        {
            string sqlstr = "select id,num,byyyx,zkyyx,date_format(date,'%Y-%m-%d %H:%i:%s') AS date,byzd,zkzd,qsh,zzh,bz from tj_xbzk_fhl where " + tj;
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
                DBProcess.ShowException(ex, "GetInfoXbzk 执行语句异常：" + sqlstr);
            }
            return null;
        }
        //YH修改
        public int  GetInfoXbzkint(string tj)
        {   
            int aa =0;
            string sqlstr = "select count(*) as sl from tj_xbzk_fhl where " + tj;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    aa = Convert.ToInt16(ds.Tables[0].Rows[0]["sl"]);
                    return aa;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetInfoXbzk 执行语句异常：" + sqlstr);
            }
            return aa;
        }


        public int UpdateInfo(string num, string byyyx, string bz, string byzd, string zkzd, string zkyyx, string qsh, string zzh, string id)
        {
            string sqlstr = "update tj_xbzk_fhl set num=@num,byyyx=@byyyx,zkyyx=@zkyyx,byzd=@byzd,zkzd=@zkzd,qsh=@qsh,zzh=@zzh,bz=@bz where id=@id";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@num", DbType.String, num);
                DBProcess._db.AddInParameter(cmd, "@byyyx", DbType.String, byyyx);
                DBProcess._db.AddInParameter(cmd, "@zkyyx", DbType.String, zkyyx);
                DBProcess._db.AddInParameter(cmd, "@byzd", DbType.String, byzd);
                DBProcess._db.AddInParameter(cmd, "@zkzd", DbType.String, zkzd);
                DBProcess._db.AddInParameter(cmd, "@qsh", DbType.String, qsh);
                DBProcess._db.AddInParameter(cmd, "@zzh", DbType.String, zzh);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
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
