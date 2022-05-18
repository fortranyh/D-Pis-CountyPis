using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
namespace DBhleper.BLL
{
    public class exam_specimens
    {
        public Boolean Process_exam_specimens(string sjbbStr,int Icount, string exam_no,string doctor_code,string doctor_name,Boolean Merge_flag,ref string Str_Result)
        {
            Boolean Zx_Result = false;
            string sqlstr = "";
            //正常申请单情况下
            try
            {
                sqlstr = "select count(*) from exam_specimens where exam_no=@exam_no and parts=@parts and pack_order=@pack_order";
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, sjbbStr);
                DBProcess._db.AddInParameter(cmd, "@pack_order", DbType.String, Icount);
                int icount = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                if (icount == 0)
                {
                    cmd.Parameters.Clear();
                    sqlstr = "delete from exam_specimens where exam_no=@exam_no";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                    DBProcess._db.ExecuteNonQuery(cmd);
                    //插入
                    cmd.Parameters.Clear();
                    sqlstr = "insert into exam_specimens(exam_no,parts,pack_order,specimens_class,sfhg,memo_note,receive_doctor_code,receive_doctor_name)values(@exam_no,@parts,@pack_order,@specimens_class,@sfhg,@memo_note,@receive_doctor_code,@receive_doctor_name)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                    DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, sjbbStr);
                    DBProcess._db.AddInParameter(cmd, "@pack_order", DbType.String, Icount);
                    DBProcess._db.AddInParameter(cmd, "@specimens_class", DbType.String, "");
                    DBProcess._db.AddInParameter(cmd, "@sfhg", DbType.Int16, "1");
                    DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, "");
                    DBProcess._db.AddInParameter(cmd, "@receive_doctor_code", DbType.String, doctor_code);
                    DBProcess._db.AddInParameter(cmd, "@receive_doctor_name", DbType.String, doctor_name);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "插入标本信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "插入标本信息失败！";
                    }
                }
                else
                {
                    Zx_Result = true;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Process_exam_specimens 执行语句异常" + sqlstr);
                Zx_Result = false;
                Str_Result = "处理标本信息异常：" + ex.ToString();
            }
            return Zx_Result;
        }
        //插入标本信息
        public Boolean InsertSpecimensInfo(string exam_no,  string parts, string specimens_class,string memo_note, string pack_order, string doctor_code, string doctor_name)
        {
            Boolean ZxResult = false;
            string sqlstr = "insert into exam_specimens(exam_no,parts,pack_order,specimens_class,sfhg,memo_note,receive_doctor_code,receive_doctor_name)values(@exam_no,@parts,@pack_order,@specimens_class,@sfhg,@memo_note,@receive_doctor_code,@receive_doctor_name)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, parts);
                DBProcess._db.AddInParameter(cmd, "@pack_order", DbType.String, pack_order);
                DBProcess._db.AddInParameter(cmd, "@specimens_class", DbType.String, specimens_class);
                DBProcess._db.AddInParameter(cmd, "@sfhg", DbType.Int16, 1);
                DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
                DBProcess._db.AddInParameter(cmd, "@receive_doctor_code", DbType.String, doctor_code);
                DBProcess._db.AddInParameter(cmd, "@receive_doctor_name", DbType.String, doctor_name);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    ZxResult = true;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertSpecimensInfo 执行语句异常" + sqlstr);
            }
            return ZxResult;
        }

        //标本接收模块标本展示
        public DataTable GetSpecimensInfo(string exam_no)
        {
           string sqlstr = "select id,parts,pack_order,specimens_class,sfhg,memo_note from exam_specimens where sfhg=1 and exam_no=@exam_no order by id asc";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               cmd.Parameters.Clear();
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return ds.Tables[0];
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetSpecimensInfo 执行语句异常：" + sqlstr);
           }
           return null;

        }
        //取材模块标本展示
        public DataTable GetQCSpecimensInfo(string exam_no)
        {
            string sqlstr = "select id,parts,pack_order,specimens_class,child_type,memo_note,date_format(receive_datetime,'%Y-%m-%d %H:%i:%s') as  receive_datetime from exam_specimens where sfhg=1 and exam_no=@exam_no order by id asc";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetQCSpecimensInfo 执行语句异常：" + sqlstr);
            }
            return null;

        }
        //更新大体描述 
        public void Update_See_memo(string see_memo, string exam_no)
        {
            string sqlstr = "update exam_specimens set see_memo=@see_memo where see_memo is not null and exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@see_memo", DbType.String, see_memo);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Update_See_memo 执行语句：" + sqlstr);
            }
        }
        //更新材块存放位置 
        public void Update_Specimens_Location(string specimens_location, string exam_no)
        {
            string sqlstr = "update exam_specimens set specimens_location=@specimens_location where  exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@specimens_location", DbType.String, specimens_location);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Update_Specimens_Location 执行语句：" + sqlstr);
            }
        }
        //获取原始大体描述信息
        public DataTable GetDtmsInfo(string exam_no)
        {
            string sqlstr = "select parts,see_memo from exam_specimens where see_memo is not null and exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDtmsInfo 执行语句异常：" + sqlstr);
            }
            return null;
        }

        //删除指定申请单下的指定的标本信息
        public void DelSpecimens(Int32 id)
        {
            string sqlstr = "delete from exam_specimens where  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "DelSpecimens 执行语句：" + sqlstr);
            }
        }

        //更新标本类型
        public void UpdateSpecimensClass(Int32 id, string specimens_class)
        {
            string sqlstr = "update exam_specimens set specimens_class=@specimens_class where  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@specimens_class", DbType.String, specimens_class);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                DBProcess._db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateSpecimensClass 执行语句：" + sqlstr);
            }
        }
        //更新标本子类型 
        public void UpdateSpecimensChildClass(Int32 id, string child_type)
        {
            string sqlstr = "update exam_specimens set child_type=@child_type where  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@child_type", DbType.String, child_type);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateSpecimensChildClass 执行语句：" + sqlstr);
            }
        }
        public void UpdateSpecimensRqh(Int32 id, string pack_order)
        {
            string sqlstr = "update exam_specimens set pack_order=@pack_order where  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@pack_order", DbType.String, pack_order);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                DBProcess._db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateSpecimensRqh 执行语句：" + sqlstr);
            }
        }
        //更新大体取材信息
        public Boolean UpdateSpecimensDTQC(int id, string see_memo, string record_doctor_code, string record_doctor_name, string draw_datetime, string specimens_process, string specimens_location, int lk_count, int ck_count, ref string result_str)
        {
            Boolean zxResult = false;
            string sqlstr = "update exam_specimens set see_memo=@see_memo,record_doctor_code=@record_doctor_code,record_doctor_name=@record_doctor_name,draw_datetime=@draw_datetime,specimens_process=@specimens_process,specimens_location=@specimens_location,lk_count=@lk_count,ck_count=@ck_count   where  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@see_memo", DbType.String, see_memo);
                DBProcess._db.AddInParameter(cmd, "@record_doctor_code", DbType.String, record_doctor_code);
                DBProcess._db.AddInParameter(cmd, "@record_doctor_name", DbType.String, record_doctor_name);
                DBProcess._db.AddInParameter(cmd, "@draw_datetime", DbType.DateTime, draw_datetime);
                DBProcess._db.AddInParameter(cmd, "@specimens_process", DbType.String, specimens_process);
                DBProcess._db.AddInParameter(cmd, "@specimens_location", DbType.String, specimens_location);
                DBProcess._db.AddInParameter(cmd, "@lk_count", DbType.Int16, lk_count);
                DBProcess._db.AddInParameter(cmd, "@ck_count", DbType.Int16, ck_count);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    zxResult = true;
                    result_str = "大体取材保存成功！";
                }
                else
                {
                    result_str = "大体取材保存失败!";
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateSpecimensDTQC 执行语句：" + sqlstr);
                result_str = "大体取材保存失败:" + ex.ToString();
            }
            return zxResult;
        }
        //获取大体取材统计及描述信息
        public DataTable GetSpecimensDTQC(int id)
        {
            DataTable dt = null;
            string sqlstr = "select see_memo,record_doctor_code,record_doctor_name,draw_datetime,specimens_process,specimens_location,lk_count,ck_count from exam_specimens where id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
                if (dt != null && dt.Rows.Count == 1)
                {
                    return dt;
                }
                else
                {
                    dt = null; 
                }
                
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetSpecimensDTQC 执行语句：" + sqlstr);
            }
            return dt;
        }
        //报告根据申请单号获取取材信息
        public DataTable GetBGSpecimensDTQC(string exam_no)
        {
            DataTable dt = null;
            string sqlstr = "select see_memo,parts,date_format(draw_datetime,'%Y-%m-%d %H:%i:%s') as draw_datetime from exam_specimens where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
                if (dt != null )
                {
                    return dt;
                }
                else
                {
                    dt = null;
                }

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetBGSpecimensDTQC 执行语句：" + sqlstr);
            }
            return dt;
        }
        public void UpdateSpecimensNote(Int32 id, string memo_note)
        {
            string sqlstr = "update exam_specimens set memo_note=@memo_note where  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                DBProcess._db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateSpecimensNote 执行语句：" + sqlstr);
            }
        }
        public void UpdateSpecimensParts(Int32 id, string parts)
        {
            string sqlstr = "update exam_specimens set parts=@parts where  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, parts);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                DBProcess._db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateSpecimensParts 执行语句：" + sqlstr);
            }
        }
        //更新标本不规范化
        public int UpdateBbGfh(int id, int bbgfh_flag)
        {
            int result = 0;
            string sqlstr = "update exam_specimens set bbgfh_flag=@bbgfh_flag where  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@bbgfh_flag", DbType.Int16, bbgfh_flag);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                result= DBProcess._db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateBbGfh 执行语句：" + sqlstr);
                result = 0;
            }
            return result;
        }
        //获取标本是否规范化固定标记
        public int GetBbBuGfhFlag(int id)
        {
            int result = 0;
            string sqlstr = "select count(*) from exam_specimens  where bbgfh_flag=0 and  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                result =Convert.ToInt16(DBProcess._db.ExecuteScalar(cmd));

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetBbBuGfhFlag 执行语句：" + sqlstr);
                result = 0;
            }
            return result;
        }
        //获取标本类型
        public string GetSpecimensClass(string exam_no)
        {
            string Result = "";
            string sqlstr = " select specimens_class from exam_specimens where exam_no='" + exam_no + "' limit 1";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                Result=Convert.ToString( DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetSpecimensClass 执行语句：" + sqlstr);
            }
            return Result;
        }
        //获取录入人姓名  
        public string GetSpecimensRecord_doctor_name(string exam_no)
        {
            string Result = "";
            string sqlstr = " select record_doctor_name from exam_specimens where record_doctor_name<>'' and exam_no='" + exam_no + "' limit 1";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                Result = Convert.ToString(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetSpecimensRecord_doctor_name 执行语句：" + sqlstr);
            }
            return Result;
        }



        //查询标本信息
        public DataTable QuerySepcimensInfo(string tj)
        {

            string sqlstr = "select a.id as id, a.exam_no as exam_no,b.study_no as study_no,a.parts as parts,pack_order,specimens_class,date_format(b.received_datetime,'%Y-%m-%d %H:%i:%s') as receive_datetime,date_format(b.qucai_datetime,'%Y-%m-%d %H:%i:%s') as qucai_datetime,b.receive_doctor_name as receive_doctor_name,specimens_process,specimens_location,lk_count,ck_count,a.record_doctor_name as record_doctor_name,see_memo,memo_note,bbgfh_flag from exam_specimens a left join exam_master b on a.exam_no=b.exam_no and a.sfhg=1 where b.exam_status>1   " + tj + " order by b.study_no desc";
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
                DBProcess.ShowException(ex, "GetSepcimensInfo 执行语句异常：" + sqlstr);
            }
            return null;
        }

    }
}
