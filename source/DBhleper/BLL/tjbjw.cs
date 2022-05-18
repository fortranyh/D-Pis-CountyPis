using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    public class tjbjw
    {

        //获取套餐类型列表 
        public DataSet GetDstaocan_type()
        {
            DataSet ds = null;
            string sqlstr = "select id,taocan_type_name from taocan_type_dict";
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDstaocan_type 执行语句：" + sqlstr);
            }
            return ds;
        }

        public DataTable GetAllDTBjw()
        {
            DataTable dt = null;
            string sqlstr = "select * from tj_bjw_dict order by bjw_name asc";
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetAllDTBjw 执行语句：" + sqlstr);
            }
            return dt;
        }

        public DataTable GetAllTaocanDTBjw(int taocan_id)
        {
            DataTable dt = null;
            string sqlstr = "select c.id as bjw_id, bjw_name,taocan_name,taocan_type from tj_bjw_taocan a inner join tj_taocan_dict b on  a.taocan_id=b.id inner join tj_bjw_dict  c on a.bjw_id =c.id where a.taocan_id=" + taocan_id;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetAllTaocanDTBjw 执行语句：" + sqlstr);
            }
            return dt;
        }

        public int GetBjwCount(string bjw_name)
        {
            int result = 0;
            string sqlstr = "select count(*) as sl from tj_bjw_dict where bjw_name=@bjw_name";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@bjw_name", DbType.String, bjw_name);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                result = sl;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetBjwCount 执行语句异常：" + sqlstr);
                result = 0;
            }

            return result;
        }
        public int InsertBjw(string bjw_name, string bjw_type, string rs_name, string rs_code)
        {
            int result = 0;
            string sqlstr = "insert tj_bjw_dict(bjw_name,bjw_type,rs_name,rs_code)values(@bjw_name,@bjw_type,@rs_name,@rs_code)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@bjw_name", DbType.String, bjw_name);
                DBProcess._db.AddInParameter(cmd, "@bjw_type", DbType.String, bjw_type);
                DBProcess._db.AddInParameter(cmd, "@rs_name", DbType.String, rs_name);
                DBProcess._db.AddInParameter(cmd, "@rs_code", DbType.String, rs_code);
                int sl = DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
                result = sl;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertBjw 执行语句异常：" + sqlstr);
                result = 0;
            }

            return result;
        }

        public DataTable GetAllDTTaocan()
        {
            DataTable dt = null;
            string sqlstr = "select * from tj_taocan_dict order by id asc";
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetAllDTTaocan 执行语句：" + sqlstr);
            }
            return dt;
        }



        public DataTable GetAllDTTaocan1()
        {
            DataTable dt = null;
            string sqlstr = "select '-1' as id,'' as taocan_name union  all (select id,taocan_name from tj_taocan_dict order by id asc)";
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetAllDTTaocan1 执行语句：" + sqlstr);
            }
            return dt;
        }



        public int GetTaoCanCount(string taocan_name)
        {
            int result = 0;
            string sqlstr = "select count(*) as sl from tj_taocan_dict where taocan_name=@taocan_name";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@taocan_name", DbType.String, taocan_name);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                result = sl;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetTaoCanCount 执行语句异常：" + sqlstr);
                result = 0;
            }

            return result;
        }

        public int InsertTaoCan(string taocan_name, string taocan_type)
        {
            int result = 0;
            string sqlstr = "insert into tj_taocan_dict(taocan_name,taocan_type)values(@taocan_name,@taocan_type)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@taocan_name", DbType.String, taocan_name);
                DBProcess._db.AddInParameter(cmd, "@taocan_type", DbType.String, taocan_type);
                int sl = DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
                result = sl;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertTaoCan 执行语句异常：" + sqlstr);
                result = 0;
            }

            return result;
        }

        public int delBjw(int id)
        {
            int result = 0;
            string sqlstr = "delete from tj_bjw_dict where id=@id ";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                int sl = DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
                result = sl;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "delBjw 执行语句异常：" + sqlstr);
                result = 0;
            }

            return result;
        }
        public int delTaocan(int id)
        {
            int result = 0;
            string sqlstr = "delete from tj_taocan_dict where id=@id ";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                int sl = DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
                result = sl;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "delTaocan 执行语句异常：" + sqlstr);
                result = 0;
            }

            return result;
        }


        public int Deltj_bjw(int bjw_id)
        {
            int result = 0;
            string sqlstr = "delete from tj_bjw_taocan where bjw_id=@bjw_id ";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@bjw_id", DbType.Int32, bjw_id);
                int sl = DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
                result = sl;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Deltj_bjw 执行语句异常：" + sqlstr);
                result = 0;
            }

            return result;
        }

        public int Deltj_Taocan(int Taocan_id)
        {
            int result = 0;
            string sqlstr = "delete from tj_bjw_taocan where taocan_id=@taocan_id ";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@taocan_id", DbType.Int32, Taocan_id);
                int sl = DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
                result = sl;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Deltj_Taocan 执行语句异常：" + sqlstr);
                result = 0;
            }

            return result;
        }

        public int GetBjwtaocanCount(int bjw_id, int taocan_id)
        {
            int result = 0;
            string sqlstr = "select count(*) as sl from tj_bjw_taocan where bjw_id=@bjw_id and taocan_id=@taocan_id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@bjw_id", DbType.Int32, bjw_id);
                DBProcess._db.AddInParameter(cmd, "@taocan_id", DbType.Int32, taocan_id);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                result = sl;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetBjwtaocanCount 执行语句异常：" + sqlstr);
                result = 0;
            }

            return result;
        }
        public int InsertBjwtaocan(int bjw_id, int taocan_id)
        {
            int result = 0;
            string sqlstr = "insert into tj_bjw_taocan(taocan_id,bjw_id)values(@taocan_id,@bjw_id)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@bjw_id", DbType.Int32, bjw_id);
                DBProcess._db.AddInParameter(cmd, "@taocan_id", DbType.Int32, taocan_id);
                int sl = DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
                result = sl;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertBjwtaocan 执行语句异常：" + sqlstr);
                result = 0;
            }

            return result;
        }
        //删除指定套餐中的指定标记物
        public int DelOneBjwtaocan(int bjw_id, int taocan_id)
        {
            int result = 0;
            string sqlstr = "delete from tj_bjw_taocan where bjw_id=@bjw_id and taocan_id=@taocan_id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@bjw_id", DbType.Int32, bjw_id);
                DBProcess._db.AddInParameter(cmd, "@taocan_id", DbType.Int32, taocan_id);
                result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "DelOneBjwtaocan 执行语句异常：" + sqlstr);
                result = 0;
            }
            return result;
        }

        //根据标记物获取标记物类型
        public string GetbjwType(string bjw_name)
        {
            string result = "";
            string sqlstr = "select bjw_type from tj_bjw_dict where bjw_name=@bjw_name";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@bjw_name", DbType.String, bjw_name);
                result = Convert.ToString(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetbjwType 执行语句异常：" + sqlstr);
                result = "";
            }
            return result;
        }

        public DataSet GetBjwInfo(string sqlstr)
        {
            DataSet ds = null;
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetBjwInfo 执行语句：" + sqlstr);
            }
            return ds;
        }

        //更新标记物对应的设备染色编码和名称
        public int UpdateBjwInfo(string rs_code, string rs_name, int id)
        {
            int result = 0;
            string sqlstr = "update tj_bjw_dict set rs_code=@rs_code,rs_name=@rs_name  where id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@rs_code", DbType.String, rs_code);
                DBProcess._db.AddInParameter(cmd, "@rs_name", DbType.String, rs_name);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateBjwInfo 执行语句异常：" + sqlstr);
                result = 0;
            }
            return result;

        }

    }
}
