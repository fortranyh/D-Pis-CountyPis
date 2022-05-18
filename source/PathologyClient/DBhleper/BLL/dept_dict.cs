using System;
using System.Data;
namespace DBHelper.BLL
{
    public class dept_dict
    {
        public DataSet GetDsExam_Dept()
        {
            DataSet ds = null;
            string sqlstr = "select dept_code,dept_name from dept_dict where dept_status=1 order by sort_order asc";
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsExam_Dept 执行语句：" + sqlstr);
            }
            return ds;
        }

        public DataSet GetDsExam_Dept_Py(string py)
        {
            DataSet ds = null;

            string sqlstr;
            if (py == "")
            {
                sqlstr = "select dept_code,dept_name,input_code as dept_py from dept_dict where dept_status=1 order by sort_order asc";
            }
            else
            {
                sqlstr = "select dept_code,dept_name,input_code as dept_py from dept_dict where dept_status=1 and input_code like '" + py + "%' order by sort_order asc";
            }
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsExam_Dept_Py 执行语句：" + sqlstr);
            }
            return ds;
        }
    }
}
