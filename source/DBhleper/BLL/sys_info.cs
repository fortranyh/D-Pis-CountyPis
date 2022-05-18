using System;
using System.Data;

namespace DBHelper.BLL
{
    //获取系统相关信息(系统编号 医院名称)
    public class sys_info
    {
        public DataSet GetDsAllExam_User(string dept_code)
        {
            DataSet ds = null;
            string sqlstr = "select '11111' as user_code,'' as user_name  union  all (select user_code,user_name from sys_user_dict where user_name<>'admin' and user_role_code>5 and dept_code='" + dept_code + "' order by sort_order asc)";
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsExam_User 执行语句：" + sqlstr);
            }
            return ds;
        }

        public DataTable GetData()
        {
            string sqlstr = "select * from sys_info";

            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetData 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
    }
}
