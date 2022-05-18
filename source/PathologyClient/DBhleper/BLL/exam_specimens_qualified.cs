using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
namespace DBhleper.BLL
{
    public class exam_specimens_qualified
    {
        public Boolean Process_Specimens_qualified(Model.exam_specimens_qualified ins, ref string Str_Result)
        {
            Boolean Zx_Result = false;
            string sqlstr = "insert into exam_specimens_qualified(exam_no,specimens_name,qualified_info,doctor_code,note_doctor,doctor_name,dept_name,study_no,patient_id,input_id)values(@exam_no,@specimens_name,@qualified_info,@doctor_code,@note_doctor,@doctor_name,@dept_name,@study_no,@patient_id,@input_id)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                //插入
                cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, ins.exam_no);
                DBProcess._db.AddInParameter(cmd, "@specimens_name", DbType.String, ins.specimens_name);
                DBProcess._db.AddInParameter(cmd, "@qualified_info", DbType.String, ins.qualified_info);
                DBProcess._db.AddInParameter(cmd, "@doctor_code", DbType.String, ins.doctor_code);
                DBProcess._db.AddInParameter(cmd, "@note_doctor", DbType.String, ins.note_doctor);
                DBProcess._db.AddInParameter(cmd, "@doctor_name", DbType.String, ins.doctor_name);
                DBProcess._db.AddInParameter(cmd, "@dept_name", DbType.String, ins.dept_name);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, ins.study_no);
                DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, ins.patient_id);
                DBProcess._db.AddInParameter(cmd, "@input_id", DbType.String, ins.input_id);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    Zx_Result = true;
                    Str_Result = "不合格信息登记成功！";
                }
                else
                {
                    Zx_Result = false;
                    Str_Result = "不合格信息登记失败！";
                }

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Process_Specimens_qualified 执行语句异常" + sqlstr);
                Zx_Result = false;
                Str_Result = "不合格信息登记异常：" + ex.ToString();
            }
            return Zx_Result;
        }
        //获取不合格标本
        public DataSet GetDsSpecimens_qualified(string exam_no)
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select id,specimens_name,qualified_info,date_format(note_datetime,'%Y-%m-%d %H:%i:%s') as  note_datetime,note_doctor,doctor_name,dept_name,study_no,patient_id,input_id from exam_specimens_qualified where exam_no='" + exam_no + "'");
            try
            {
                sb.Append(" order by note_datetime asc");
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsSpecimens_qualified 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }

        //获取不合格标本
        public DataSet GetDsSpecimens_qualifiedInfo(string tj)
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select specimens_name,qualified_info,date_format(note_datetime,'%Y-%m-%d %H:%i:%s') as  note_datetime,note_doctor,doctor_name,dept_name,study_no,input_id,patient_id from exam_specimens_qualified where " + tj);
            try
            {
                sb.Append(" order by note_datetime asc");
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsSpecimens_qualifiedInfo 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }

    }
}
