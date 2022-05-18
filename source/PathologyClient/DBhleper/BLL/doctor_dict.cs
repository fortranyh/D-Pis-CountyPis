using System;
using System.Data;
namespace DBHelper.BLL
{
    public class doctor_dict
    {
        //获取技术医师列表 
        public DataSet GetDsJSExam_User(string dept_code)
        {
            DataSet ds = null;
            string sqlstr = "select '' as user_code, '' as user_name  union  all (select user_code,user_name from sys_user_dict where user_name<>'admin' and user_role_code<=5 and dept_code='" + dept_code + "' order by sort_order asc)";
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
        public DataSet GetDsBgys_User(string dept_code)
        {
            DataSet ds = null;
            string sqlstr = "select user_code,user_name from sys_user_dict where user_name<>'admin' and user_role_code>5 and dept_code='" + dept_code + "'";
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
        public DataSet GetDsExam_Dcotor(string dept_code)
        {
            DataSet ds = null;
            string sqlstr = "select doctor_code,doctor_name from doctor_dict where dept_code='" + dept_code + "' order by doctor_code asc";
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsExam_Dcotor 执行语句：" + sqlstr);
            }
            return ds;
        }
        //获取报告医师列表 
        public DataSet GetDsExam_User(string dept_code)
        {
            DataSet ds = null;
            string sqlstr = "(select '-1' as  user_code,'' as user_name) union  all    (select user_code,user_name from sys_user_dict where user_name<>'admin' and user_role_code>5 and dept_code='" + dept_code + "' order by sort_order asc)";
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
        //获取规培医师列表
        public DataSet GetDsGB_User(string dept_code)
        {
            DataSet ds = null;
            string sqlstr = "select user_code,user_name from sys_user_dict where user_name<>'admin' and gb_flag=1 and dept_code='" + dept_code + "'";
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


        //获取报告医师列表 
        public DataSet GetDsAllExam_User(string dept_code)
        {
            DataSet ds = null;
            string sqlstr = "select '9999' as user_code, '全部' as user_name union  all (select user_code,user_name from sys_user_dict where user_name<>'admin' and user_role_code>5 and dept_code='" + dept_code + "' order by sort_order asc)";
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsAllExam_User 执行语句：" + sqlstr);
            }
            return ds;
        }
        public DataSet GetDsExam_DcotorPy(string dept_code)
        {
            DataSet ds = null;
            string sqlstr = "select doctor_code as doc_code,doctor_name as doc_name,input_code as doc_py from doctor_dict where dept_code='" + dept_code + "' order by doctor_code asc";
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsExam_DcotorPy 执行语句：" + sqlstr);
            }
            return ds;
        }

        public DataTable GetSjdwList()
        {
            string sqlstr = "select * from hospital_list";
            DataSet ds = null;
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetSjdwList 执行语句：" + sqlstr);
            }
            return null;
        }

    }
}
