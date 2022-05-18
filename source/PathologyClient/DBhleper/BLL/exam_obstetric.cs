using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    //妇科信息
    public class exam_obstetric
    {
        public Boolean Process_exam_obstetric(EntityModel.exam_obstetric Tu_Ins, ref string Str_Result)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_obstetric where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 0)
                {
                    //插入
                    sqlstr = "insert into exam_obstetric(exam_no,pre_date,last_date,ops_flag,ops_date,ops_unit,foetus,production,absolute_flag,memo)values(@exam_no,@pre_date,@last_date,@ops_flag,@ops_date,@ops_unit,@foetus,@production,@absolute_flag,@memo)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                    DBProcess._db.AddInParameter(cmd, "@pre_date", DbType.Date, Tu_Ins.pre_date);
                    DBProcess._db.AddInParameter(cmd, "@last_date", DbType.Date, Tu_Ins.last_date);
                    DBProcess._db.AddInParameter(cmd, "@ops_flag", DbType.String, Tu_Ins.ops_flag);
                    DBProcess._db.AddInParameter(cmd, "@ops_date", DbType.Date, Tu_Ins.ops_date);
                    DBProcess._db.AddInParameter(cmd, "@ops_unit", DbType.String, Tu_Ins.ops_unit);
                    DBProcess._db.AddInParameter(cmd, "@foetus", DbType.String, Tu_Ins.foetus);
                    DBProcess._db.AddInParameter(cmd, "@production", DbType.String, Tu_Ins.production);
                    DBProcess._db.AddInParameter(cmd, "@absolute_flag", DbType.String, Tu_Ins.absolute_flag);
                    DBProcess._db.AddInParameter(cmd, "@memo", DbType.String, Tu_Ins.memo);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "插入妇科患者信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "插入妇科患者信息失败！";
                    }
                }
                else if (sl == 1)
                {
                    //更新
                    sqlstr = "update exam_obstetric set pre_date=@pre_date,last_date=@last_date,ops_flag=@ops_flag,ops_date=@ops_date,ops_unit=@ops_unit,foetus=@foetus,production=@production,absolute_flag=@absolute_flag,memo=@memo where exam_no=@exam_no";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);

                    DBProcess._db.AddInParameter(cmd, "@pre_date", DbType.Date, Tu_Ins.pre_date);
                    DBProcess._db.AddInParameter(cmd, "@last_date", DbType.Date, Tu_Ins.last_date);
                    DBProcess._db.AddInParameter(cmd, "@ops_flag", DbType.String, Tu_Ins.ops_flag);
                    DBProcess._db.AddInParameter(cmd, "@ops_date", DbType.Date, Tu_Ins.ops_date);
                    DBProcess._db.AddInParameter(cmd, "@ops_unit", DbType.String, Tu_Ins.ops_unit);
                    DBProcess._db.AddInParameter(cmd, "@foetus", DbType.String, Tu_Ins.foetus);
                    DBProcess._db.AddInParameter(cmd, "@production", DbType.String, Tu_Ins.production);
                    DBProcess._db.AddInParameter(cmd, "@absolute_flag", DbType.String, Tu_Ins.absolute_flag);
                    DBProcess._db.AddInParameter(cmd, "@memo", DbType.String, Tu_Ins.memo);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "更新妇科患者信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "更新妇科患者信息失败！";
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Process_exam_obstetric 执行语句异常：" + sqlstr);
                Zx_Result = false;
                Str_Result = "处理妇科患者信息异常：" + ex.ToString();
            }
            return Zx_Result;
        }

        public Boolean Process_exam_obstetric(Model.exam_obstetric Tu_Ins, ref string Str_Result)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_obstetric where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 0)
                {
                    //插入
                    sqlstr = "insert into exam_obstetric(exam_no,pre_date,last_date,ops_flag,ops_date,ops_unit,foetus,production,absolute_flag,memo)values(@exam_no,@pre_date,@last_date,@ops_flag,@ops_date,@ops_unit,@foetus,@production,@absolute_flag,@memo)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                    DBProcess._db.AddInParameter(cmd, "@pre_date", DbType.Date, Tu_Ins.pre_date);
                    DBProcess._db.AddInParameter(cmd, "@last_date", DbType.Date, Tu_Ins.last_date);
                    DBProcess._db.AddInParameter(cmd, "@ops_flag", DbType.String, Tu_Ins.ops_flag);
                    DBProcess._db.AddInParameter(cmd, "@ops_date", DbType.Date, Tu_Ins.ops_date);
                    DBProcess._db.AddInParameter(cmd, "@ops_unit", DbType.String, Tu_Ins.ops_unit);
                    DBProcess._db.AddInParameter(cmd, "@foetus", DbType.String, Tu_Ins.foetus);
                    DBProcess._db.AddInParameter(cmd, "@production", DbType.String, Tu_Ins.production);
                    DBProcess._db.AddInParameter(cmd, "@absolute_flag", DbType.String, Tu_Ins.absolute_flag);
                    DBProcess._db.AddInParameter(cmd, "@memo", DbType.String, Tu_Ins.memo);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "插入妇科患者信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "插入妇科患者信息失败！";
                    }
                }
                else if (sl == 1)
                {
                    //更新
                    sqlstr = "update exam_obstetric set pre_date=@pre_date,last_date=@last_date,ops_flag=@ops_flag,ops_date=@ops_date,ops_unit=@ops_unit,foetus=@foetus,production=@production,absolute_flag=@absolute_flag,memo=@memo where exam_no=@exam_no";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);

                    DBProcess._db.AddInParameter(cmd, "@pre_date", DbType.Date, Tu_Ins.pre_date);
                    DBProcess._db.AddInParameter(cmd, "@last_date", DbType.Date, Tu_Ins.last_date);
                    DBProcess._db.AddInParameter(cmd, "@ops_flag", DbType.String, Tu_Ins.ops_flag);
                    DBProcess._db.AddInParameter(cmd, "@ops_date", DbType.Date, Tu_Ins.ops_date);
                    DBProcess._db.AddInParameter(cmd, "@ops_unit", DbType.String, Tu_Ins.ops_unit);
                    DBProcess._db.AddInParameter(cmd, "@foetus", DbType.String, Tu_Ins.foetus);
                    DBProcess._db.AddInParameter(cmd, "@production", DbType.String, Tu_Ins.production);
                    DBProcess._db.AddInParameter(cmd, "@absolute_flag", DbType.String, Tu_Ins.absolute_flag);
                    DBProcess._db.AddInParameter(cmd, "@memo", DbType.String, Tu_Ins.memo);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Tu_Ins.exam_no);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "更新妇科患者信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "更新妇科患者信息失败！";
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Process_exam_obstetric 执行语句异常：" + sqlstr);
                Zx_Result = false;
                Str_Result = "处理妇科患者信息异常：" + ex.ToString();
            }
            return Zx_Result;
        }

        public Model.exam_obstetric GetExam_obstetricInfo(string exam_no)
        {
            string sqlstr = "select date_format(pre_date,'%Y-%m-%d') as pre_date,date_format(last_date,'%Y-%m-%d') as last_date,ops_flag,date_format(ops_date,'%Y-%m-%d') as ops_date,ops_unit,foetus,production,absolute_flag,memo  from exam_obstetric where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count == 1)
                {
                    Model.exam_obstetric Pat_Ins = new Model.exam_obstetric();
                    Pat_Ins.pre_date = ds.Tables[0].Rows[0]["pre_date"].ToString();
                    Pat_Ins.last_date = ds.Tables[0].Rows[0]["last_date"].ToString();
                    Pat_Ins.ops_flag = ds.Tables[0].Rows[0]["ops_flag"].ToString() ?? "";
                    Pat_Ins.ops_date = ds.Tables[0].Rows[0]["ops_date"].ToString();
                    Pat_Ins.ops_unit = ds.Tables[0].Rows[0]["ops_unit"].ToString() ?? "";
                    Pat_Ins.foetus = ds.Tables[0].Rows[0]["foetus"].ToString() ?? "";
                    Pat_Ins.production = ds.Tables[0].Rows[0]["production"].ToString() ?? "";
                    Pat_Ins.absolute_flag = ds.Tables[0].Rows[0]["absolute_flag"].ToString() ?? "";
                    Pat_Ins.memo = ds.Tables[0].Rows[0]["memo"].ToString();
                    return Pat_Ins;
                }

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetExam_obstetricInfo 执行语句异常：" + sqlstr);

            }
            return null;
        }
        //获取末次月经日期
        public string GetLastYjInfo(string exam_no)
        {
            string Result = "";
            string sqlstr = " select  date_format(last_date,'%Y-%m-%d') as  last_date from exam_obstetric where exam_no='" + exam_no + "' limit 1";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                Result = Convert.ToString(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetLastYjInfo 执行语句：" + sqlstr);
            }
            return Result;
        }
        //获取是否绝经
        public string Getabsolute_flag(string exam_no)
        {
            string Result = "";
            string sqlstr = " select  absolute_flag from exam_obstetric where exam_no='" + exam_no + "' limit 1";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                Result = Convert.ToString(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Getabsolute_flag 执行语句：" + sqlstr);
            }
            return Result;
        }
    }
}
