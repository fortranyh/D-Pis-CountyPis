using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
namespace DBHelper.BLL
{
    public class sys_user
    {
        public DataTable GetUserInfo(int flag)
        {

            string sqlstr = "select user_name,user_code from sys_user_dict ";
            if (flag == 0)
            {
                sqlstr = " select '全部' as user_name,'9999' as user_code union  all select user_name,user_code from sys_user_dict";
            }
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetUserInfo 执行语句异常：" + sqlstr);
            }
            return null;
        }

        //获取当前消息接收列表人员
        public DataTable GetMsgUserInfo()
        {
            string sqlstr = " select '全部' as user_name,'9999' as user_code union  all (select user_name,user_code from sys_user_dict where user_name<>'admin')";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetMsgUserInfo 执行语句异常：" + sqlstr);
            }
            return null;
        }



        //获取用户角色编码（级别）
        public int GetUserRoleCode(string user_code)
        {
            int result = 0;

            string sqlstr = "select user_role_code  from sys_user_dict where user_code='" + user_code + "'";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetUserRoleCode 执行语句异常：" + sqlstr);
            }

            return result;
        }
        //获取用户是否管理级别 
        public int GetUserGL(string user_code)
        {
            int result = 0;

            string sqlstr = "select user_qx from sys_user_dict where user_code='" + user_code + "'";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetUserGL 执行语句异常：" + sqlstr);
            }

            return result;
        }
        //获取用户名列表

        public List<string> GetUsersName()
        {
            List<string> lst = new List<string>();
            string sqlstr = "select user_name  from sys_user_dict where user_role_code>5 and user_name<>'admin' ";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(ds.Tables[0].Rows[i]["user_name"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetUsersName 执行语句：" + sqlstr);
            }
            return lst;
        }
        //系统登录
        public DataTable sysLoginCheck(string user_info)
        {
            DataTable dt = null;
            string sqlstr = "select  user_code,user_name,user_pwd,dept_code,user_role_code,dept_name,user_role,gb_flag  from  sys_user_view where user_status=1 and(user_name=@user_info or user_code=@user_info) ";
            try
            {

                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_info", DbType.String, user_info);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "sysLoginCheck 执行语句：" + sqlstr);
            }
            return dt;
        }
        //分中心用户登录 
        public int sysThirdLoginCheck(string user_code, string user_pwd, string hospital_code)
        {
            int RetInt = 0;
            string sqlstr = "select count(*) from third_path_user where user_enable=1 and hospital_code=@hospital_code and user_pwd=@user_pwd and user_code=@user_code";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@hospital_code", DbType.String, hospital_code);
                DBProcess._db.AddInParameter(cmd, "@user_pwd", DbType.String, user_pwd);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                RetInt = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "sysThirdLoginCheck 执行语句：" + sqlstr);
                RetInt = 0;
            }
            return RetInt;
        }



        //查询部门全部人员
        public DataTable SysAllUsers(string dept_code)
        {
            string sqlstr = "select user_code,user_name from sys_user_dict where dept_code=@dept_code and user_name<>'admin'";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@dept_code", DbType.String, dept_code);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "SysAllUsers 执行语句：" + sqlstr);
            }
            return dt;
        }
        public DataTable SysAllUsers()
        {
            string sqlstr = "select user_code,user_name,user_code from sys_user_dict where  user_name<>'admin' ";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "SysAllUsers 执行语句：" + sqlstr);
            }
            return dt;
        }
        //查询除去本人外的部门全部人员
        public DataTable SysTjAllUsers(string user_code, string dept_code)
        {
            string sqlstr = "select user_code,user_name from sys_user_dict where user_code<>@user_code and dept_code=@dept_code";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                DBProcess._db.AddInParameter(cmd, "@dept_code", DbType.String, dept_code);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "SysTjAllUsers 执行语句：" + sqlstr);
            }
            return dt;
        }
        //查询部门全部人员
        public DataTable SysShowAllUsers(string dept_code)
        {
            string sqlstr = "select user_code,user_name from sys_user_dict where  dept_code=@dept_code";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@dept_code", DbType.String, dept_code);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "SysShowAllUsers 执行语句：" + sqlstr);
            }
            return dt;
        }
        //获取全部菜单
        public DataTable GetMenus()
        {
            string sqlstr = "select menu_name,menu_tag from menus";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetMenus 执行语句：" + sqlstr);
            }
            return dt;
        }
        //获取指定用户的权限
        public DataTable GetUserQx(string user_code)
        {
            string sqlstr = "select menu_name,menu_tag from user_qx where user_code=@user_code";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetUserQx 执行语句：" + sqlstr);
            }
            return dt;
        }
        //删除用户全部权限
        public Boolean DelUserQx(string user_code)
        {
            string sqlstr = "delete from user_qx where user_code=@user_code";
            Boolean zxresult = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                DBProcess._db.ExecuteNonQuery(cmd);
                zxresult = true;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "DelUserQx 执行语句：" + sqlstr);
            }
            return zxresult;
        }
        //插入用户权限
        public Boolean InsertUserQx(string user_code, string menu_name, string menu_tag)
        {
            string sqlstr = "insert into user_qx(user_code,menu_name,menu_tag)values(@user_code,@menu_name,@menu_tag)";
            Boolean zxresult = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                DBProcess._db.AddInParameter(cmd, "@menu_name", DbType.String, menu_name);
                DBProcess._db.AddInParameter(cmd, "@menu_tag", DbType.String, menu_tag);
                DBProcess._db.ExecuteNonQuery(cmd);
                zxresult = true;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertUserQx 执行语句：" + sqlstr);
            }
            return zxresult;
        }
        //删除用户
        public Boolean DelUserInfo(string user_code)
        {
            string sqlstr = "delete from sys_user_dict where user_code=@user_code";
            Boolean zxresult = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                DBProcess._db.ExecuteNonQuery(cmd);
                zxresult = true;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "DelUserInfo 执行语句：" + sqlstr);
            }
            return zxresult;
        }
        public Boolean DelDoctorInfo(string doctor_code)
        {
            string sqlstr = "delete from doctor_dict where doctor_code=@doctor_code";
            Boolean zxresult = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@doctor_code", DbType.String, doctor_code);
                DBProcess._db.ExecuteNonQuery(cmd);
                zxresult = true;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "DelDoctorInfo 执行语句：" + sqlstr);
            }
            return zxresult;
        }
        //查询用户是否存在
        public int QueryUserCode(string user_code)
        {
            int result = 0;

            string sqlstr = "select count(*) as sl  from sys_user_dict where user_code=@user_code";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "QueryUserCode 执行语句异常：" + sqlstr);
            }
            return result;
        }
        //插入用户
        public Boolean InsertUser(string user_name, string user_code, string user_pwd, string dept_code, string user_role, int user_role_code, int gb_flag)
        {
            string sqlstr = " insert into sys_user_dict(user_code,user_name,user_pwd,user_role,user_role_code,dept_code,gb_flag)values(@user_code,@user_name,@user_pwd,@user_role,@user_role_code,@dept_code,@gb_flag)";
            Boolean zxresult = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                DBProcess._db.AddInParameter(cmd, "@user_name", DbType.String, user_name);
                DBProcess._db.AddInParameter(cmd, "@user_pwd", DbType.String, user_pwd);
                DBProcess._db.AddInParameter(cmd, "@user_role", DbType.String, user_role);
                DBProcess._db.AddInParameter(cmd, "@user_role_code", DbType.Int16, user_role_code);
                DBProcess._db.AddInParameter(cmd, "@dept_code", DbType.String, dept_code);
                DBProcess._db.AddInParameter(cmd, "@gb_flag", DbType.String, gb_flag);
                DBProcess._db.ExecuteNonQuery(cmd);
                zxresult = true;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertUser 执行语句：" + sqlstr);
            }
            return zxresult;
        }
        //插入doctor_dict
        public Boolean InsertDoctor(string doctor_name, string doctor_code, string dept_code, string doctor_role)
        {
            Boolean zxresult = false;
            string sqlstr = "select count(*) as sl from doctor_dict where doctor_code=@doctor_code and dept_code=@dept_code";
            DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
            DBProcess._db.AddInParameter(cmd, "@doctor_code", DbType.String, doctor_code);
            DBProcess._db.AddInParameter(cmd, "@dept_code", DbType.String, dept_code);
            int result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            if (result == 0)
            {
                sqlstr = " insert into doctor_dict(doctor_code,doctor_name,doctor_role,dept_code) values(@doctor_code,@doctor_name,@doctor_role,@dept_code)";
                try
                {
                    cmd.Parameters.Clear();
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@doctor_code", DbType.String, doctor_code);
                    DBProcess._db.AddInParameter(cmd, "@doctor_name", DbType.String, doctor_name);
                    DBProcess._db.AddInParameter(cmd, "@doctor_role", DbType.String, doctor_role);
                    DBProcess._db.AddInParameter(cmd, "@dept_code", DbType.String, dept_code);
                    DBProcess._db.ExecuteNonQuery(cmd);
                    zxresult = true;
                }
                catch (Exception ex)
                {
                    DBProcess.ShowException(ex, "InsertDoctor 执行语句：" + sqlstr);
                }
            }
            return zxresult;
        }

        //更新用户角色及密码
        public Boolean UpdateUser(string user_code, string user_pwd, string user_role, int user_role_code, int gb_flag)
        {
            string sqlstr = " update sys_user_dict set user_pwd=@user_pwd,user_role=@user_role,user_role_code=@user_role_code,gb_flag=@gb_flag where user_code=@user_code";
            Boolean zxresult = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_pwd", DbType.String, user_pwd);
                DBProcess._db.AddInParameter(cmd, "@user_role", DbType.String, user_role);
                DBProcess._db.AddInParameter(cmd, "@user_role_code", DbType.Int16, user_role_code);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                DBProcess._db.AddInParameter(cmd, "@gb_flag", DbType.String, gb_flag);
                DBProcess._db.ExecuteNonQuery(cmd);
                zxresult = true;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertUser 执行语句：" + sqlstr);
            }
            return zxresult;
        }
        //更新用户密码
        public Boolean UpdateUserPwd(string user_code, string user_pwd)
        {
            string sqlstr = " update sys_user_dict set user_pwd=@user_pwd where user_code=@user_code";
            Boolean zxresult = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_pwd", DbType.String, user_pwd);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                DBProcess._db.ExecuteNonQuery(cmd);
                zxresult = true;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateUserPwd 执行语句：" + sqlstr);
            }
            return zxresult;
        }
        public Boolean UpdateThirdUserPwd(string user_code, string user_pwd, string hospital_code)
        {
            string sqlstr = " update third_path_user set user_pwd=@user_pwd where user_code=@user_code and hospital_code=@hospital_code";
            Boolean zxresult = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@user_pwd", DbType.String, user_pwd);
                DBProcess._db.AddInParameter(cmd, "@user_code", DbType.String, user_code);
                DBProcess._db.AddInParameter(cmd, "@hospital_code", DbType.String, hospital_code);
                DBProcess._db.ExecuteNonQuery(cmd);
                zxresult = true;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateThirdUserPwd 执行语句：" + sqlstr);
            }
            return zxresult;
        }
    }
}
