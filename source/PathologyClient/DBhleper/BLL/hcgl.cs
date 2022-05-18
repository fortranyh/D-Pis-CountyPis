using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    public class hcgl
    {
        public int AddInfoTsyjl(string wp, string jl, string ghrq, string qm, string bz)
        {
            string sqlstr = "insert into hcgl_tsyjl(wp,jl,ghrq,qm,bz)values(@wp,@jl,@ghrq,@qm,@bz)";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@wp", DbType.String, wp);
                DBProcess._db.AddInParameter(cmd, "@jl", DbType.String, jl);
                DBProcess._db.AddInParameter(cmd, "@ghrq", DbType.String, ghrq);
                DBProcess._db.AddInParameter(cmd, "@qm", DbType.String, qm);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "AddInfoTsyjl 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public DataTable GetInfoTsyjl(string tj)
        {
            string sqlstr = "select id,wp,jl,date_format(ghrq,'%Y-%m-%d %H:%i:%s') AS ghrq,qm,bz from hcgl_tsyjl where " + tj;
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
                DBProcess.ShowException(ex, "GetInfoTsyjl 执行语句异常：" + sqlstr);
            }
            return null;
        }


        public int UpdateInfoTsyjl(string wp, string jl, string ghrq, string qm, string bz, string id)
        {
            string sqlstr = "update hcgl_tsyjl set wp=@wp, jl=@jl,ghrq=@ghrq,qm=@qm,bz=@bz where id=@id";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@wp", DbType.String, wp);
                DBProcess._db.AddInParameter(cmd, "@jl", DbType.String, jl);
                DBProcess._db.AddInParameter(cmd, "@ghrq", DbType.String, ghrq);
                DBProcess._db.AddInParameter(cmd, "@qm", DbType.String, qm);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateInfoTsyjl 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public int AddInfoRygh(string wp, string jl, string ghrq, string qm, string bz)
        {
            string sqlstr = "insert into hcgl_rygh(wp,jl,ghrq,qm,bz)values(@wp,@jl,@ghrq,@qm,@bz)";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@wp", DbType.String, wp);
                DBProcess._db.AddInParameter(cmd, "@jl", DbType.String, jl);
                DBProcess._db.AddInParameter(cmd, "@ghrq", DbType.String, ghrq);
                DBProcess._db.AddInParameter(cmd, "@qm", DbType.String, qm);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "AddInfoRygh 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public DataTable GetInfoRygh(string tj)
        {
            string sqlstr = "select id,wp,jl,date_format(ghrq,'%Y-%m-%d %H:%i:%s') AS ghrq,qm,bz from hcgl_rygh where " + tj + " order by ghrq desc";
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
                DBProcess.ShowException(ex, "GetInfoRygh 执行语句异常：" + sqlstr);
            }
            return null;
        }


        public int UpdateInfoRygh(string wp, string jl, string ghrq, string qm, string bz, string id)
        {
            string sqlstr = "update hcgl_rygh set wp=@wp, jl=@jl,ghrq=@ghrq,qm=@qm,bz=@bz where id=@id";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@wp", DbType.String, wp);
                DBProcess._db.AddInParameter(cmd, "@jl", DbType.String, jl);
                DBProcess._db.AddInParameter(cmd, "@ghrq", DbType.String, ghrq);
                DBProcess._db.AddInParameter(cmd, "@qm", DbType.String, qm);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateInfoRygh 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public int AddInfoFwfy(string ylwp, string zl, string sl, string clrq, string clff, string zxz, string hsz, string bz)
        {
            string sqlstr = "insert into hcgl_fwfy(ylwp,zl,sl,clrq,clff,zxz,hsz,bz)values(@ylwp,@zl,@sl,@clrq,@clff,@zxz,@hsz,@bz)";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@ylwp", DbType.String, ylwp);
                DBProcess._db.AddInParameter(cmd, "@zl", DbType.String, zl);
                DBProcess._db.AddInParameter(cmd, "@sl", DbType.String, sl);
                DBProcess._db.AddInParameter(cmd, "@clrq", DbType.String, clrq);
                DBProcess._db.AddInParameter(cmd, "@clff", DbType.String, clff);
                DBProcess._db.AddInParameter(cmd, "@zxz", DbType.String, zxz);
                DBProcess._db.AddInParameter(cmd, "@hsz", DbType.String, hsz);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "AddInfoFwfy 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public DataTable GetInfoFwfy(string tj)
        {
            string sqlstr = "select id,ylwp,zl,sl,date_format(clrq,'%Y-%m-%d %H:%i:%s') AS clrq,clff,zxz,hsz,bz from hcgl_fwfy where " + tj + " order by clrq desc";
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
                DBProcess.ShowException(ex, "GetInfoFwfy 执行语句异常：" + sqlstr);
            }
            return null;
        }


        public int UpdateInfoFwfy(string ylwp, string zl, string sl, string clrq, string clff, string zxz, string hsz, string bz, string id)
        {
            string sqlstr = "update hcgl_fwfy set ylwp=@ylwp, zl=@zl,sl=@sl,clrq=@clrq,clff=@clff,zxz=@zxz,hsz=@hsz,bz=@bz where id=@id";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@ylwp", DbType.String, ylwp);
                DBProcess._db.AddInParameter(cmd, "@zl", DbType.String, zl);
                DBProcess._db.AddInParameter(cmd, "@sl", DbType.String, sl);
                DBProcess._db.AddInParameter(cmd, "@clrq", DbType.String, clrq);
                DBProcess._db.AddInParameter(cmd, "@clff", DbType.String, clff);
                DBProcess._db.AddInParameter(cmd, "@zxz", DbType.String, zxz);
                DBProcess._db.AddInParameter(cmd, "@hsz", DbType.String, hsz);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateInfoFwfy 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }


        public int AddInfoYdsj(string rq, string wpmc, string kc, string yxq, string ckl, string jy, string syz, string glz)
        {
            string sqlstr = "insert into hcgl_ydsj(rq,wpmc,kc,yxq,ckl,jy,syz,glz)values(@rq,@wpmc,@kc,@yxq,@ckl,@jy,@syz,@glz)";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@rq", DbType.String, rq);
                DBProcess._db.AddInParameter(cmd, "@wpmc", DbType.String, wpmc);
                DBProcess._db.AddInParameter(cmd, "@kc", DbType.String, kc);
                DBProcess._db.AddInParameter(cmd, "@yxq", DbType.String, yxq);
                DBProcess._db.AddInParameter(cmd, "@ckl", DbType.String, ckl);
                DBProcess._db.AddInParameter(cmd, "@jy", DbType.String, jy);
                DBProcess._db.AddInParameter(cmd, "@syz", DbType.String, syz);
                DBProcess._db.AddInParameter(cmd, "@glz", DbType.String, glz);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "AddInfoYdsj 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public DataTable GetInfoYdsj(string tj)
        {
            string sqlstr = "select id,date_format(rq,'%Y-%m-%d %H:%i:%s') AS rq,wpmc,kc,date_format(yxq,'%Y-%m-%d %H:%i:%s') AS yxq,ckl,jy,syz,glz from hcgl_ydsj where " + tj;
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
                DBProcess.ShowException(ex, "GetInfoYdsj 执行语句异常：" + sqlstr);
            }
            return null;
        }


        public int UpdateInfoYdsj(string rq, string wpmc, string kc, string yxq, string ckl, string jy, string syz, string glz, string id)
        {
            string sqlstr = "update hcgl_ydsj set rq=@rq, wpmc=@wpmc,kc=@kc,yxq=@yxq,ckl=@ckl,jy=@jy,syz=@syz,glz=@glz where id=@id";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@rq", DbType.String, rq);
                DBProcess._db.AddInParameter(cmd, "@wpmc", DbType.String, wpmc);
                DBProcess._db.AddInParameter(cmd, "@kc", DbType.String, kc);
                DBProcess._db.AddInParameter(cmd, "@yxq", DbType.String, yxq);
                DBProcess._db.AddInParameter(cmd, "@ckl", DbType.String, ckl);
                DBProcess._db.AddInParameter(cmd, "@jy", DbType.String, jy);
                DBProcess._db.AddInParameter(cmd, "@syz", DbType.String, syz);
                DBProcess._db.AddInParameter(cmd, "@glz", DbType.String, glz);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateInfoYdsj 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public int AddInfoYryb(string rq, string wpmc, string kc, string yxq, string ckl, string jy, string qm, string bz)
        {
            string sqlstr = "insert into hcgl_yryb(rq,wpmc,kc,yxq,ckl,jy,qm,bz)values(@rq,@wpmc,@kc,@yxq,@ckl,@jy,@qm,@bz)";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@rq", DbType.String, rq);
                DBProcess._db.AddInParameter(cmd, "@wpmc", DbType.String, wpmc);
                DBProcess._db.AddInParameter(cmd, "@kc", DbType.String, kc);
                DBProcess._db.AddInParameter(cmd, "@yxq", DbType.String, yxq);
                DBProcess._db.AddInParameter(cmd, "@ckl", DbType.String, ckl);
                DBProcess._db.AddInParameter(cmd, "@jy", DbType.String, jy);
                DBProcess._db.AddInParameter(cmd, "@qm", DbType.String, qm);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "AddInfoYryb 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public DataTable GetInfoYryb(string tj)
        {
            string sqlstr = "select id,date_format(rq,'%Y-%m-%d %H:%i:%s') AS rq,wpmc,kc,date_format(yxq,'%Y-%m-%d %H:%i:%s') AS yxq,ckl,jy,qm,bz from hcgl_yryb where " + tj;
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
                DBProcess.ShowException(ex, "GetInfoYryb 执行语句异常：" + sqlstr);
            }
            return null;
        }


        public int UpdateInfoYryb(string rq, string wpmc, string kc, string yxq, string ckl, string jy, string qm, string bz, string id)
        {
            string sqlstr = "update hcgl_yryb set rq=@rq, wpmc=@wpmc,kc=@kc,yxq=@yxq,ckl=@ckl,jy=@jy,qm=@qm,bz=@bz where id=@id";
            int Result = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@rq", DbType.String, rq);
                DBProcess._db.AddInParameter(cmd, "@wpmc", DbType.String, wpmc);
                DBProcess._db.AddInParameter(cmd, "@kc", DbType.String, kc);
                DBProcess._db.AddInParameter(cmd, "@yxq", DbType.String, yxq);
                DBProcess._db.AddInParameter(cmd, "@ckl", DbType.String, ckl);
                DBProcess._db.AddInParameter(cmd, "@jy", DbType.String, jy);
                DBProcess._db.AddInParameter(cmd, "@qm", DbType.String, qm);
                DBProcess._db.AddInParameter(cmd, "@bz", DbType.String, bz);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateInfoYryb 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
    }
}
