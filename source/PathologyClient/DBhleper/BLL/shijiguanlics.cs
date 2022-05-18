using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DBhleper.BLL
{
    public class shijiguanlics
    {
        public int AddInfoShijiruku(string sjmc, double rkl, string rksj, string rkr, string bz, double sjdkc,string sjgq)
        {
            string sqlstr = "insert into shiji_ruku(sjmc,rkl,cursl,rksj,rkr,bz,sjdkc,sjgq)values(@sjmc,@rkl,@cursl,@rksj,@rkr,@bz,@sjdkc,@sjgq)";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@sjmc", DbType.String, sjmc);
                DBProcess._db.AddInParameter(cmd, "@rkl", DbType.Double, rkl);
                DBProcess._db.AddInParameter(cmd, "@cursl", DbType.Double, rkl);
                DBProcess._db.AddInParameter(cmd, "@rksj", DbType.String, rksj);
                DBProcess._db.AddInParameter(cmd, "@rkr", DbType.String, rkr);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
                DBProcess._db.AddInParameter(cmd, "@sjdkc", DbType.Double, sjdkc);
                DBProcess._db.AddInParameter(cmd, "@sjgq", DbType.String, sjgq);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "AddInfoShijiruku 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public int AddInfoShijichuku(string sjmc, double ckl, string cksj, string ckr, string bz)
        {
            string sqlstr = "insert into shiji_chuku(sjmc,ckl,cksj,ckr,bz)values(@sjmc,@ckl,@cksj,@ckr,@bz)";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@sjmc", DbType.String, sjmc);
                DBProcess._db.AddInParameter(cmd, "@ckl", DbType.Double, ckl);
                DBProcess._db.AddInParameter(cmd, "@cksj", DbType.String, cksj);
                DBProcess._db.AddInParameter(cmd, "@ckr", DbType.String, ckr);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "AddInfoShijichuku 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }




        public DataTable GetInfoShijiruku(string tj)
        {
            string sqlstr = "select id,sjmc,rkl,cursl,date_format(rksj,'%Y-%m-%d %H:%i:%s') AS rksj,rkr,bz,yw_flag,sjdkc,date_format(sjgq,'%Y-%m-%d') AS sjgq from shiji_ruku where " + tj;
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
                DBProcess.ShowException(ex, "GetInfoShijiruku 执行语句异常：" + sqlstr);
            }
            return null;
        }

     


        //出库查询
        public DataTable GetInfoShijichuku(string tj)
        {
            string sqlstr = "select id,sjmc,ckl,date_format(cksj,'%Y-%m-%d %H:%i:%s') as cksj,ckr,bz from shiji_chuku  where " + tj;
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
                DBProcess.ShowException(ex, "GetInfoShijiruku 执行语句异常：" + sqlstr);
            }
            return null;
        }


        public DataSet GetSjInfo()
        {
            string sqlstr = "select sjmc,cursl from shiji_ruku where yw_flag='未用完' order by rksj desc";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetSjInfo 执行语句异常：" + sqlstr);
            }
            return null;
        }



        public int DelInfoShijiruku(string id)
        {
            string sqlstr = "update  shiji_ruku set yw_flag='用完' where id=@id";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "DelInfoShijiruku 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        //
        public int UpdateInfoShijiruku(string sjmc,double sl)
        {
            string sqlstr = "update  shiji_ruku set cursl=@cursl where sjmc=@sjmc and yw_flag='未用完'";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@cursl", DbType.Double, sl);
                DBProcess._db.AddInParameter(cmd, "@sjmc", DbType.String, sjmc);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateInfoShijiruku 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
        //
        public int  SelectedSjmc(string sjmc)
        {
            string sqlstr = "select count(*) as sl from shiji_ruku where sjmc=@sjmc and yw_flag='未用完'";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@sjmc", DbType.String, sjmc);
                Result =Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "SelectedSjmc 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        // 
        public int SelectedSjmcTjCount()
        {
            string sqlstr = "select count(*) as sl from shiji_ruku where cursl<=sjdkc and yw_flag='未用完'";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                Result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "SelectedSjmc 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }


    }
}
