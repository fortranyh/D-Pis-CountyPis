using System;
using System.Data;
using System.Data.Common;
namespace DBHelper.BLL
{
    public class dept_message
    {
        //总消息数
        public int GetMessageTotalCount(string toUser_Code)
        {
            int Zx_Result = 0;
            string sqlstr = "select count(*) from dept_message where toUser_Code=@toUser_Code";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@toUser_Code", DbType.String, toUser_Code);
                Zx_Result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetMessageTotalCount 执行语句：" + sqlstr);
            }
            return Zx_Result;
        }
        //未读取消息
        public int GetMessageCount(string toUser_Code)
        {
            int Zx_Result = 0;
            string sqlstr = "select count(*) from dept_message where toUser_Code=@toUser_Code  and see_flag=0";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@toUser_Code", DbType.String, toUser_Code);
                Zx_Result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetMessageCount 执行语句：" + sqlstr);
            }
            return Zx_Result;
        }
        //获取未读消息
        public DataTable GetMessageInfo(string user_code)
        {
            string sqlstr = "select id,fromUser_Code,fromUser_Name,message,date_format(create_datetime,'%Y-%m-%d %H:%i:%s') as create_datetime from dept_message where see_flag=0 and toUser_Code=@user_code";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetMessageInfo 执行语句异常：" + sqlstr);
            }
            return null;
        }
        //获取消息类型字典
        public DataTable GetMsgType()
        {
            string sqlstr = "select id,msg_type from msgtype_dict";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetMsgType 执行语句异常：" + sqlstr);
            }
            return null;
        }
        //添加消息类型
        public string InsertMsgType(string msg_type)
        {
            string sqlstr = "select count(*) from msgtype_dict where msg_type=@msg_type";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@msg_type", DbType.String, msg_type);
                int result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (result == 0)
                {
                    sqlstr = "insert into msgtype_dict(msg_type)values(@msg_type)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@msg_type", DbType.String, msg_type);
                    result = DBProcess._db.ExecuteNonQuery(cmd);
                    cmd.Parameters.Clear();
                    if (result == 1)
                    {
                        return "true";
                    }
                }
                else
                {
                    return "当前消息类型已经存在！";
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertMsgType 执行语句异常：" + sqlstr);
            }
            return "false";
        }

        //更新消息类型
        public string UpdateMsgType(int id, string msg_type)
        {
            string sqlstr = "select count(*) from msgtype_dict where msg_type=@msg_type";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@msg_type", DbType.String, msg_type);
                int result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (result == 0)
                {
                    sqlstr = "update msgtype_dict set msg_type=@msg_type where id=@id";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@msg_type", DbType.String, msg_type);
                    DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                    result = DBProcess._db.ExecuteNonQuery(cmd);
                    cmd.Parameters.Clear();
                    if (result == 1)
                    {
                        return "true";
                    }
                }
                else
                {
                    return "当前消息类型已经存在！";
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateMsgType 执行语句异常：" + sqlstr);
            }
            return "false";
        }
        //删除消息类型
        public string DelMsgType(int id)
        {
            string sqlstr = "delete from msgtype_dict where id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                int result = DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
                if (result == 1)
                {
                    return "true";
                }

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "DelMsgType 执行语句异常：" + sqlstr);
            }
            return "false";
        }
        //获取所有消息
        public DataTable GetAllMessageInfo(string user_code)
        {
            string sqlstr = "select id,fromUser_Code,fromUser_Name,message,date_format(create_datetime,'%Y-%m-%d %H:%i:%s') as create_datetime,msg_type from dept_message where  (TO_DAYS(NOW()) - TO_DAYS(create_datetime) < 3) and toUser_Code=@user_code order by id desc limit 50";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetAllMessageInfo 执行语句异常：" + sqlstr);
            }
            return null;
        }

        //当天已经读取的消息
        public DataTable GetYDMessageInfo(string user_code)
        {
            string sqlstr = "select id,fromUser_Code,fromUser_Name,message,date_format(create_datetime,'%Y-%m-%d %H:%i:%s') as create_datetime from dept_message where see_flag=1 and (TO_DAYS(NOW()) - TO_DAYS(create_datetime) < 3) and toUser_Code=@user_code";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetYDMessageInfo 执行语句异常：" + sqlstr);
            }
            return null;
        }
        //更新消息列表状态
        public Boolean UpdateMessageInfo(string user_code, int id)
        {
            string sqlstr = "update dept_message set see_flag=1 where see_flag=0 and toUser_Code=@user_code and id<=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                int result = DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
                if (result > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateMessageInfo 执行语句异常：" + sqlstr);
            }
            return false;
        }
        //插入新消息
        public string InsertNewMessage(string fromUser_Code, string fromUser_Name, string toUser_Code, string toUser_Name, string message, string msg_type)
        {
            string sqlstr = "insert into dept_message(fromUser_Code,fromUser_Name,toUser_Code,toUser_Name,message,msg_type)values(@fromUser_Code,@fromUser_Name,@toUser_Code,@toUser_Name,@message,@msg_type)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@fromUser_Code", DbType.String, fromUser_Code);
                DBProcess._db.AddInParameter(cmd, "@fromUser_Name", DbType.String, fromUser_Name);
                DBProcess._db.AddInParameter(cmd, "@toUser_Code", DbType.String, toUser_Code);
                DBProcess._db.AddInParameter(cmd, "@toUser_Name", DbType.String, toUser_Name);
                DBProcess._db.AddInParameter(cmd, "@message", DbType.String, message);
                DBProcess._db.AddInParameter(cmd, "@msg_type", DbType.String, msg_type);
                int result = DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
                if (result == 1)
                {
                    return "true";
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertNewMessage 执行语句异常：" + sqlstr);
            }
            return "false";
        }
        //插入新消息
        public Boolean InsertMessage(Model.dept_message InsM)
        {
            string sqlstr = "insert into dept_message(fromUser_Code,fromUser_Name,toUser_Code,toUser_Name,message)values(@fromUser_Code,@fromUser_Name,@toUser_Code,@toUser_Name,@message)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@fromUser_Code", DbType.String, InsM.fromUser_Code);
                DBProcess._db.AddInParameter(cmd, "@fromUser_Name", DbType.String, InsM.fromUser_Name);
                DBProcess._db.AddInParameter(cmd, "@toUser_Code", DbType.String, InsM.toUser_Code);
                DBProcess._db.AddInParameter(cmd, "@toUser_Name", DbType.String, InsM.toUser_Name);
                DBProcess._db.AddInParameter(cmd, "@message", DbType.String, InsM.message);
                int result = DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
                if (result == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertMessage 执行语句异常：" + sqlstr);
            }
            return false;
        }
        //删除历史消息  
        public Boolean DeleteMessageInfo()
        {
            string sqlstr = "delete from dept_message WHERE TO_DAYS(NOW()) - TO_DAYS(create_datetime) >= 3";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                int result = DBProcess._db.ExecuteNonQuery(cmd);
                return true;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "DeleteMessageInfo 执行语句异常：" + sqlstr);
            }
            return false;
        }
    }
}
