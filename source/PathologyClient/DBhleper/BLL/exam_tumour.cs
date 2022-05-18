using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    //肿瘤信息
    public class exam_tumour
    {

        public Boolean Process_exam_tumour(EntityModel.exam_tumour Tu_Ins, ref string Str_Result)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_tumour where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 0)
                {
                    //插入
                    sqlstr = "insert into exam_tumour(exam_no,find_date,parts,sizes,radiate_flag,chemotherapy,transfer_flag,trans_parts,memo)values(@exam_no,@find_date,@parts,@sizes,@radiate_flag,@chemotherapy,@transfer_flag,@trans_parts,@memo)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                    DBProcess._db.AddInParameter(cmd, "@find_date", DbType.Date, Tu_Ins.find_date);
                    DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, Tu_Ins.parts);
                    DBProcess._db.AddInParameter(cmd, "@sizes", DbType.String, Tu_Ins.sizes);
                    DBProcess._db.AddInParameter(cmd, "@radiate_flag", DbType.String, Tu_Ins.radiate_flag);
                    DBProcess._db.AddInParameter(cmd, "@chemotherapy", DbType.String, Tu_Ins.chemotherapy);
                    DBProcess._db.AddInParameter(cmd, "@transfer_flag", DbType.String, Tu_Ins.transfer_flag);
                    DBProcess._db.AddInParameter(cmd, "@trans_parts", DbType.String, Tu_Ins.trans_parts);
                    DBProcess._db.AddInParameter(cmd, "@memo", DbType.String, Tu_Ins.memo);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "插入肿瘤患者信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "插入肿瘤患者信息失败！";
                    }
                }
                else if (sl == 1)
                {
                    //更新
                    sqlstr = "update exam_tumour set find_date=@find_date,parts=@parts,sizes=@sizes,radiate_flag=@radiate_flag,chemotherapy=@chemotherapy,transfer_flag=@transfer_flag,trans_parts=@trans_parts,memo=@memo where exam_no=@exam_no";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@find_date", DbType.Date, Tu_Ins.find_date);
                    DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, Tu_Ins.parts);
                    DBProcess._db.AddInParameter(cmd, "@sizes", DbType.String, Tu_Ins.sizes);
                    DBProcess._db.AddInParameter(cmd, "@radiate_flag", DbType.String, Tu_Ins.radiate_flag);
                    DBProcess._db.AddInParameter(cmd, "@chemotherapy", DbType.String, Tu_Ins.chemotherapy);
                    DBProcess._db.AddInParameter(cmd, "@transfer_flag", DbType.String, Tu_Ins.transfer_flag);
                    DBProcess._db.AddInParameter(cmd, "@trans_parts", DbType.String, Tu_Ins.trans_parts);
                    DBProcess._db.AddInParameter(cmd, "@memo", DbType.String, Tu_Ins.memo);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "更新肿瘤患者信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "更新肿瘤患者信息失败！";
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Process_exam_tumour 执行语句异常：" + sqlstr);
                Zx_Result = false;
                Str_Result = "处理肿瘤患者信息异常：" + ex.ToString();
            }
            return Zx_Result;
        }


        public Boolean Process_exam_tumour(Model.exam_tumour Tu_Ins, ref string Str_Result)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_tumour where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 0)
                {
                    //插入
                    sqlstr = "insert into exam_tumour(exam_no,find_date,parts,sizes,radiate_flag,chemotherapy,transfer_flag,trans_parts,memo)values(@exam_no,@find_date,@parts,@sizes,@radiate_flag,@chemotherapy,@transfer_flag,@trans_parts,@memo)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                    DBProcess._db.AddInParameter(cmd, "@find_date", DbType.Date, Tu_Ins.find_date);
                    DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, Tu_Ins.parts);
                    DBProcess._db.AddInParameter(cmd, "@sizes", DbType.String, Tu_Ins.sizes);
                    DBProcess._db.AddInParameter(cmd, "@radiate_flag", DbType.String, Tu_Ins.radiate_flag);
                    DBProcess._db.AddInParameter(cmd, "@chemotherapy", DbType.String, Tu_Ins.chemotherapy);
                    DBProcess._db.AddInParameter(cmd, "@transfer_flag", DbType.String, Tu_Ins.transfer_flag);
                    DBProcess._db.AddInParameter(cmd, "@trans_parts", DbType.String, Tu_Ins.trans_parts);
                    DBProcess._db.AddInParameter(cmd, "@memo", DbType.String, Tu_Ins.memo);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "插入肿瘤患者信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "插入肿瘤患者信息失败！";
                    }
                }
                else if (sl == 1)
                {
                    //更新
                    sqlstr = "update exam_tumour set find_date=@find_date,parts=@parts,sizes=@sizes,radiate_flag=@radiate_flag,chemotherapy=@chemotherapy,transfer_flag=@transfer_flag,trans_parts=@trans_parts,memo=@memo where exam_no=@exam_no";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@find_date", DbType.Date, Tu_Ins.find_date);
                    DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, Tu_Ins.parts);
                    DBProcess._db.AddInParameter(cmd, "@sizes", DbType.String, Tu_Ins.sizes);
                    DBProcess._db.AddInParameter(cmd, "@radiate_flag", DbType.String, Tu_Ins.radiate_flag);
                    DBProcess._db.AddInParameter(cmd, "@chemotherapy", DbType.String, Tu_Ins.chemotherapy);
                    DBProcess._db.AddInParameter(cmd, "@transfer_flag", DbType.String, Tu_Ins.transfer_flag);
                    DBProcess._db.AddInParameter(cmd, "@trans_parts", DbType.String, Tu_Ins.trans_parts);
                    DBProcess._db.AddInParameter(cmd, "@memo", DbType.String, Tu_Ins.memo);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "更新肿瘤患者信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "更新肿瘤患者信息失败！";
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Process_exam_tumour 执行语句异常：" + sqlstr);
                Zx_Result = false;
                Str_Result = "处理肿瘤患者信息异常：" + ex.ToString();
            }
            return Zx_Result;
        }

        public Model.exam_tumour GetExam_tumourInfo(string exam_no)
        {
            string sqlstr = "select date_format(find_date,'%Y-%m-%d') as find_date,parts,sizes,radiate_flag,chemotherapy,transfer_flag,trans_parts,memo  from exam_tumour where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count == 1)
                {
                    Model.exam_tumour Pat_Ins = new Model.exam_tumour();
                    Pat_Ins.find_date = ds.Tables[0].Rows[0]["find_date"].ToString();
                    Pat_Ins.parts = ds.Tables[0].Rows[0]["parts"].ToString();
                    Pat_Ins.sizes = ds.Tables[0].Rows[0]["sizes"].ToString() ?? "";
                    Pat_Ins.radiate_flag = ds.Tables[0].Rows[0]["radiate_flag"].ToString();
                    Pat_Ins.chemotherapy = ds.Tables[0].Rows[0]["chemotherapy"].ToString() ?? "";
                    Pat_Ins.transfer_flag = ds.Tables[0].Rows[0]["transfer_flag"].ToString() ?? "";
                    Pat_Ins.trans_parts = ds.Tables[0].Rows[0]["trans_parts"].ToString() ?? "";
                    Pat_Ins.memo = ds.Tables[0].Rows[0]["memo"].ToString() ?? "";
                    return Pat_Ins;
                }

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetExam_tumourInfo 执行语句异常：" + sqlstr);

            }
            return null;

        }
    }
}
