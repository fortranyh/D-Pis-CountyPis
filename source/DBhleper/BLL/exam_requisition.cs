using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    //申请单描述信息
    public class exam_requisition
    {
        public Boolean Process_exam_requisition(Model.exam_requisition Tu_Ins, ref string Str_Result)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_requisition where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 0)
                {
                    //插入
                    sqlstr = "insert into exam_requisition(exam_no,history_note,ops_note,clinical_diag,infectious_note)values(@exam_no,@history_note,@ops_note,@clinical_diag,@infectious_note)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                    DBProcess._db.AddInParameter(cmd, "@history_note", DbType.String, Tu_Ins.history_note);
                    DBProcess._db.AddInParameter(cmd, "@ops_note", DbType.String, Tu_Ins.ops_note);
                    DBProcess._db.AddInParameter(cmd, "@clinical_diag", DbType.String, Tu_Ins.clinical_diag);
                    DBProcess._db.AddInParameter(cmd, "@infectious_note", DbType.String, Tu_Ins.infectious_note);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "插入申请单描述信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "插入申请单描述信息失败！";
                    }
                }
                else if (sl == 1)
                {
                    //更新
                    sqlstr = "update exam_requisition set history_note=@history_note,ops_note=@ops_note,clinical_diag=@clinical_diag,infectious_note=@infectious_note where exam_no=@exam_no";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@history_note", DbType.String, Tu_Ins.history_note);
                    DBProcess._db.AddInParameter(cmd, "@ops_note", DbType.String, Tu_Ins.ops_note);
                    DBProcess._db.AddInParameter(cmd, "@clinical_diag", DbType.String, Tu_Ins.clinical_diag);
                    DBProcess._db.AddInParameter(cmd, "@infectious_note", DbType.String, Tu_Ins.infectious_note);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "更新申请单描述信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "更新申请单描述信息失败！";
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Process_exam_requisition 执行语句异常：" + sqlstr);
                Zx_Result = false;
                Str_Result = "处理申请单描述信息异常：" + ex.ToString();
            }
            return Zx_Result;
        }

        public Model.exam_requisition GetRequisitionInfo(string exam_no)
        {
            string sqlstr = "select history_note,ops_note,clinical_diag,infectious_note from exam_requisition where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count == 1)
                {
                    Model.exam_requisition MasIns = new Model.exam_requisition();
                    MasIns.exam_no = exam_no;
                    MasIns.history_note = ds.Tables[0].Rows[0]["history_note"].ToString();
                    MasIns.ops_note = ds.Tables[0].Rows[0]["ops_note"].ToString();
                    MasIns.clinical_diag = ds.Tables[0].Rows[0]["clinical_diag"].ToString();
                    MasIns.infectious_note = ds.Tables[0].Rows[0]["infectious_note"].ToString();
                    return MasIns;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetRequisitionInfo 执行语句：" + sqlstr);
            }
            return null;
        }
        //获取临床诊断
        public string Getclinical_diag(string exam_no)
        {
            string lczd = "";
            string sqlstr = "select clinical_diag from exam_requisition  where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                lczd = Convert.ToString(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Getclinical_diag 执行语句异常：" + sqlstr);
            }
            return lczd;
        }
        //获取传染病信息
        public string GetInfectious_note(string exam_no)
        {
            string infectious_note = "";
            string sqlstr = "select infectious_note from exam_requisition  where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                infectious_note = Convert.ToString(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetInfectious_note 执行语句异常：" + sqlstr);
            }
            return infectious_note;
        }
    }
}
