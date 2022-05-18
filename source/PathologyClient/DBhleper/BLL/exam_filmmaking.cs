using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DBhleper.BLL
{
   public class exam_filmmaking
    {
        //
        public DataTable GetDataTable(string sqlstr)
        {
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDataTable 执行语句异常：" + sqlstr);
            }
            return dt;
        }
        //核对制片的数据
       public DataTable GetDtFilmMaking(string study_no)
        {
            string sqlstr = "select 1 as xz, id,study_no,draw_id,draw_barcode,film_child_barcode,barcode,work_source,film_num,memo_note,zp_flag,print_flag,make_doc_name,level,level_memo from exam_filmmaking where study_no=@study_no";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDtFilmMaking 执行语句异常：" + sqlstr);
            }
            return dt;
        }
       //获取包埋医生
       public string GetBmDocName(int draw_id)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("select bm_doc_name from exam_filmmaking where draw_id=@draw_id");
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sb.ToString());
               DBProcess._db.AddInParameter(cmd, "@draw_id", DbType.Int32, draw_id);
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return ds.Tables[0].Rows[0][0].ToString();
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetBmDocName 执行语句异常：" + sb.ToString());
           }
           return "";

       }



       //制片的评级
       public DataTable GetDtFilmMakingPj(string study_no)
       {
           string sqlstr = "select id,study_no,draw_id,draw_barcode,film_child_barcode,barcode,work_source,film_num,memo_note,zp_flag,print_flag,make_doc_name,level,level_memo from exam_filmmaking where study_no=@study_no";
           DataTable dt = null;
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetDtFilmMaking 执行语句异常：" + sqlstr);
           }
           return dt;
       }
       //档案查询切片信息
       public DataTable GetQpInfo(string tj)
       {
           string sqlstr = "select * from exam_qp_view  where 1=1 " + tj + "order by id asc,qp_xh asc";
           DataTable dt = null;
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetQpInfo 执行语句异常：" + sqlstr);
           }
           return dt;
       }
       //包埋完成初始插入切片信息
       public Boolean Process_filmmaking(string study_no,int draw_id,string draw_barcode,string barcode,string work_source,string memo_note,string make_doc_code,string make_doc_name, int print_flag,ref string Str_Result)
       {
           Boolean Zx_Result = false;
           string sqlstr = "";
           try
           {
                       //插入
               sqlstr = "insert into exam_filmmaking(study_no,draw_id,draw_barcode,film_child_barcode,barcode,work_source,memo_note,bm_doc_code,bm_doc_name,bm_datetime,print_flag,qp_xh)values(@study_no,@draw_id,@draw_barcode,@film_child_barcode,@barcode,@work_source,@memo_note,@bm_doc_code,@bm_doc_name,@bm_datetime,@print_flag,@qp_xh)";
                       DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                       DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                       DBProcess._db.AddInParameter(cmd, "@draw_id", DbType.Int32, draw_id);
                       DBProcess._db.AddInParameter(cmd, "@draw_barcode", DbType.String, barcode);
                       DBProcess._db.AddInParameter(cmd, "@film_child_barcode", DbType.String, draw_barcode);
                       DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String,draw_barcode );
                       DBProcess._db.AddInParameter(cmd, "@work_source", DbType.String, work_source);
                       DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
                       DBProcess._db.AddInParameter(cmd, "@bm_doc_code", DbType.String, make_doc_code);
                       DBProcess._db.AddInParameter(cmd, "@bm_doc_name", DbType.String, make_doc_name);
                       DBProcess._db.AddInParameter(cmd, "@bm_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                       DBProcess._db.AddInParameter(cmd, "@print_flag", DbType.Int16, print_flag);
                       DBProcess._db.AddInParameter(cmd, "@qp_xh", DbType.String,draw_barcode.Substring(draw_barcode.Length - 2, 2));
                       if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                       {
                           Zx_Result = true;
                           Str_Result = "初始插入切片信息成功！";
                       }
                       else
                       {
                           Zx_Result = false;
                           Str_Result = "初始插入切片信息失败！";
                       }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "Process_filmmaking 执行语句异常" + sqlstr);
               Zx_Result = false;
               Str_Result = "初始插入切片信息异常：" + ex.ToString();
           }
           return Zx_Result;
       }
       //切片确认
       public Boolean Qr_film(int id, string barcode, string make_doc_code, string make_doc_name)
       {
           Boolean Zx_Result = false;
           string sqlstr = "update exam_filmmaking set make_doc_code=@make_doc_code,make_doc_name=@make_doc_name,make_datetime=@make_datetime,zp_flag=1 where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@make_doc_code", DbType.String, make_doc_code);
               DBProcess._db.AddInParameter(cmd, "@make_doc_name", DbType.String, make_doc_name);
               DBProcess._db.AddInParameter(cmd, "@make_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);
               Zx_Result = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "Qr_film 执行语句：" + sqlstr);
           }
           return Zx_Result;
       }
       //切片合并 
       public Boolean Hb_film(int id)
       {
           Boolean Zx_Result = false;
           string sqlstr = "update exam_filmmaking set hb_flag=1 where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);
               Zx_Result = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "Hb_film 执行语句：" + sqlstr);
           }
           return Zx_Result;
       }


       //更新玻片备注
       public void UpdateFilmmaking_Note(Int32 id, string memo_note)
       {
           string sqlstr = "update exam_filmmaking set memo_note=@memo_note where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateFilmmaking_Note 执行语句：" + sqlstr);
           }
       }
       //更新玻片备注
       public void UpdateFilmmaking_Num(Int32 id, int film_num)
       {
           string sqlstr = "update exam_filmmaking set film_num=@film_num where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@film_num", DbType.Int32, film_num);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateFilmmaking_Num 执行语句：" + sqlstr);
           }
       }

       //更新切片状态
       public void UpdateFilmmakingZp_flag(Int32 id, int zp_flag)
       {
           string sqlstr = "update exam_filmmaking set zp_flag=@zp_flag where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@zp_flag", DbType.Int32, zp_flag);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateFilmmakingZp_flag 执行语句：" + sqlstr);
           }
       }
       //更新切片级别
       public void UpdateFilmmakingLevel(Int32 id, string Level, string level_memo)
       {
           string sqlstr = "update exam_filmmaking set Level=@Level,level_memo=@level_memo where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@Level", DbType.String, Level);
               DBProcess._db.AddInParameter(cmd, "@level_memo", DbType.String, level_memo);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateFilmmakingLevel 执行语句：" + sqlstr);
           }
       }
       //更新打印状态
       public void UpdateFilmmakingPrint_flag(Int32 id, int print_flag)
       {
           string sqlstr = "update exam_filmmaking set print_flag=@print_flag where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@print_flag", DbType.Int32, print_flag);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateFilmmakingPrint_flag 执行语句：" + sqlstr);
           }
       }
       //更新HE染色切片
       public int UpdateFilmmakingHE_flag(Int32 id, string he_level)
       {
           int result = 0;
           string sqlstr = "update exam_filmmaking set he_level=@he_level,he_flag=@he_flag where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@he_level", DbType.String, he_level);
               DBProcess._db.AddInParameter(cmd, "@he_flag", DbType.Int16, 1);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               result= DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateFilmmakingPrint_flag 执行语句：" + sqlstr);
               result = 0;
           }
           return result;
       }
       //删除指定id下的取材信息
       public void DelFilmmaking(Int32 id)
       {
           string sqlstr = "delete from exam_filmmaking where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "DelFilmmaking 执行语句：" + sqlstr);
           }
       }
       //更新制片医师
       public Boolean UpdateZPDocName(int id, string make_doc_name)
       {
           Boolean ZxResult = true;
           string sqlstr = " update  exam_filmmaking set make_doc_name=@make_doc_name where id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@make_doc_name", DbType.String, make_doc_name);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               if (result == 1)
               {
                   ZxResult = true;
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateZPDocName 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }
       //更新制片医师
       public Boolean UpdateZPDocName(string study_no, string make_doc_name)
       {
           Boolean ZxResult = true;
           string sqlstr = " update  exam_filmmaking set make_doc_name=@make_doc_name where id=@id and make_doc_name=''";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@make_doc_name", DbType.String, make_doc_name);
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               if (result == 1)
               {
                   ZxResult = true;
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateZPDocName 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }
       //添加切片信息
       public int InsertQpInfo(string study_no, int draw_id, string draw_barcode, string barcode, string work_source, string zp_info, int film_num, string make_doc_name, string mkdate, string bm_doc_name)
       {
           int ZxResult = 0;
           string sqlstr = "insert into exam_filmmaking(study_no,draw_id,draw_barcode,film_child_barcode,barcode,work_source,zp_info,film_num,make_doc_name,make_datetime,zp_flag,qp_xh,bm_doc_name)values(@study_no,@draw_id,@draw_barcode,@film_child_barcode,@barcode,@work_source,@zp_info,@film_num,@make_doc_name,@make_datetime,@zp_flag,@qp_xh,@bm_doc_name)";
           try
           {
               //插入
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               DBProcess._db.AddInParameter(cmd, "@draw_id", DbType.Int32, draw_id);
               DBProcess._db.AddInParameter(cmd, "@draw_barcode", DbType.String, draw_barcode);
               DBProcess._db.AddInParameter(cmd, "@film_child_barcode", DbType.String, draw_barcode);
               DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String, barcode);
               DBProcess._db.AddInParameter(cmd, "@work_source", DbType.String, work_source);
               DBProcess._db.AddInParameter(cmd, "@zp_info", DbType.String, zp_info);
               DBProcess._db.AddInParameter(cmd, "@film_num", DbType.Int32, film_num);
               DBProcess._db.AddInParameter(cmd, "@make_doc_name", DbType.String, make_doc_name);
               DBProcess._db.AddInParameter(cmd, "@make_datetime", DbType.DateTime, mkdate);
               DBProcess._db.AddInParameter(cmd, "@zp_flag", DbType.Int16, 2);
               DBProcess._db.AddInParameter(cmd, "@qp_xh", DbType.String, barcode.Substring(barcode.Length - 2, 2));
               DBProcess._db.AddInParameter(cmd, "@bm_doc_name", DbType.String, bm_doc_name);
               if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
               {
                   ZxResult = 1;
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "InsertQpInfo 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }
       //获取切片号
       public string GetBarcode(string study_no,string pre_char)
       {
           string ResultStr = "";
           string sqlstr = "select max(barcode) as barcode from  exam_filmmaking where study_no=@study_no";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               string str = DBProcess._db.ExecuteScalar(cmd).ToString();
               if (str != "")
               {
                 
                   if (pre_char != "")
                   {
                       str = str.Replace(pre_char, "");
                   }
                   else
                   {
                       pre_char = study_no.Substring(0, 1);
                       if (Microsoft.VisualBasic.Information.IsNumeric(pre_char) == true)
                       {
                           pre_char = "";
                       }
                       else
                       {
                           str = str.Replace(pre_char, "");
                       }
                   }
                   Int64 Result = Convert.ToInt64(str);
                   ResultStr = pre_char + (Result + 1).ToString();
               }
               else
               {
                   ResultStr = study_no + "01";
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetBarcode 执行语句异常：" + sqlstr);
               ResultStr = "";
           }
           return ResultStr;
       }

     


       //待切片信息
       public DataTable GetWaitQpinfo(string study_no, string bm_datetime, Boolean sort = true)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("select id,study_no,work_source,draw_barcode,bm_doc_name,date_format(bm_datetime,'%Y-%m-%d') AS bm_datetime,zp_flag,print_flag,film_num,memo_note,barcode,qp_xh as dzpqp_xh from exam_filmmaking where zp_flag=0 and hb_flag=0 ");
           if (study_no.Equals(""))
           {
               sb.AppendFormat(" and bm_datetime>='{0} 00:00:00' and bm_datetime<='{1} 23:59:59'", bm_datetime, bm_datetime);
           }
           else
           {
               sb.AppendFormat(" and study_no='{0}'", study_no);
           }
           if (sort)
           {
               sb.Append("  order by study_no asc,draw_id asc");
           }
           else
           {
               sb.Append("  order by study_no desc,draw_id asc");
           }
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sb.ToString());
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return ds.Tables[0];
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetWaitQpinfo 执行语句异常：" + sb.ToString());
           }
           return null;
       }
       //已经制片列表
       public DataTable GetOverQpinfo(string wks,string study_no, string make_datetime, Boolean print_flag, Boolean zp_flag, Boolean sort = true)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("select a.id as id,a.study_no as study_no,a.work_source as work_source,draw_barcode,zp_info,a.barcode as barcode,a.bm_doc_name as bm_doc_name,make_doc_name,date_format(make_datetime,'%Y-%m-%d') AS make_datetime,zp_flag,a.print_flag as print_flag,film_num,a.memo_note as memo_note,level,level_memo,draw_id,parts,a.qp_xh as qp_xh  from exam_filmmaking a join exam_draw_meterials b on a.draw_id=b.id  where zp_flag>0 and hb_flag=0 and a.bm_doc_name<>'' ");
           if (study_no.Equals(""))
           {
               sb.AppendFormat(" and make_datetime>='{0} 00:00:00' and make_datetime<='{1} 23:59:59'", make_datetime, make_datetime);
           }
           else
           {
               sb.AppendFormat(" and a.study_no='{0}'", study_no);
           }
           if (print_flag)
           {
               sb.Append("  and a.print_flag=0");
           }
           if (!wks.Equals(""))
           {
               wks = wks.Replace("'", "");
               if (wks.Equals("冰冻和冰余"))
               {
                   sb.Append(" and (a.work_source='冰冻' || a.work_source='冰余') ");
               }
               else
               {
                   sb.Append(" and (a.work_source='" + wks + "' || a.memo_note='" + wks + "' ||  zp_info='" + wks + "') ");
               }
           }
           if (zp_flag)
           {
               sb.Append("  and a.zp_flag<2");
           }
           if (sort)
           {
               sb.Append("  order by a.study_no asc,draw_id asc");
           }
           else
           {
               sb.Append("  order by a.study_no desc,draw_id asc");
           }
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sb.ToString());
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return ds.Tables[0];
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetOverQpinfo 执行语句异常：" + sb.ToString());
           }
           return null;
       }
       //扫描核对制片列表
       public DataTable SmHdQpinfo(string barcode, string study_no, string make_datetime, Boolean print_flag, Boolean zp_flag, Boolean sort = true)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("select id,study_no,work_source,draw_barcode,zp_info,barcode,bm_doc_name,make_doc_name,date_format(make_datetime,'%Y-%m-%d') AS make_datetime,zp_flag,print_flag,film_num,memo_note,level,level_memo from exam_filmmaking where zp_flag>0 and hb_flag=0 ");
           sb.AppendFormat(" and barcode='{0}'", barcode);
           if (!study_no.Equals(""))
           {
               sb.AppendFormat(" and study_no='{0}'", study_no);
           }
           if (print_flag)
           {
               sb.Append("  and print_flag=0");
           }
           if (zp_flag)
           {
               sb.Append("  and zp_flag<2");
           }
           if (sort)
           {
               sb.Append("  order by study_no asc,draw_barcode asc");
           }
           else
           {
               sb.Append("  order by study_no desc,draw_barcode asc");
           }
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sb.ToString());
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return ds.Tables[0];
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "SmHdQpinfo 执行语句异常：" + sb.ToString());
           }
           return null;
       }

       //更新切片条码
       public Boolean UpdateQpBarcode(int id, string qp_barcode)
       {
           Boolean ZxResult = true;
           string sqlstr = " update exam_filmmaking set barcode=@qp_barcode where id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@qp_barcode", DbType.String, qp_barcode);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               ZxResult = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateQpBarcode 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }
       //核对玻片
       public Boolean UpdateQpHdflag(int id)
       {
           Boolean ZxResult = true;
           string sqlstr = " update exam_filmmaking set zp_flag=2 where id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               ZxResult = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateQpHdflag 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }
       //更新打印状态
       public Boolean UpdatePrintFlag(int id)
       {
           Boolean ZxResult = true;
           string sqlstr = " update exam_filmmaking set print_flag=1 where id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               ZxResult = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdatePrintFlag 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }
       //根据条码号更新打印状态
       public Boolean UpdateBarcodePrintFlag(string barcode)
       {
           Boolean ZxResult = true;
           string sqlstr = " update exam_filmmaking set print_flag=1 where barcode=@barcode";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String, barcode);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               ZxResult = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateBarcodePrintFlag 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }
       //删除切片
       public Boolean DelQpInfo(int id)
       {
           Boolean ZxResult = true;
           string sqlstr = " delete from exam_filmmaking where id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               ZxResult = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "DelQpInfo 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }
       //删除切片
       public Boolean DelQpBarcode(string  barcode)
       {
           Boolean ZxResult = true;
           string sqlstr = " delete from exam_filmmaking where barcode=@barcode";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String, barcode);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               ZxResult = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "DelQpBarcode 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }

       //更新切片归档
       public Boolean UpdateQpGd(int id, string qpgd_location, string qpgd_doctor)
       {
           Boolean ZxResult = true;
           string sqlstr = " update exam_filmmaking set qpgd_flag=1,qpgd_doctor=@qpgd_doctor,qpgd_datetime=@qpgd_datetime,qpgd_location=@qpgd_location where id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@qpgd_doctor", DbType.String, qpgd_doctor);
               DBProcess._db.AddInParameter(cmd, "@qpgd_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
               DBProcess._db.AddInParameter(cmd, "@qpgd_location", DbType.String, qpgd_location);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               ZxResult = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateQpGd 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }
       public Boolean CancelQpGd(int id)
       {
           Boolean ZxResult = true;
           string sqlstr = " update exam_filmmaking set qpgd_flag=0,qpgd_doctor=@qpgd_doctor,qpgd_datetime=@qpgd_datetime,qpgd_location=@qpgd_location where id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@qpgd_doctor", DbType.String, "");
               DBProcess._db.AddInParameter(cmd, "@qpgd_datetime", DbType.DateTime, null);
               DBProcess._db.AddInParameter(cmd, "@qpgd_location", DbType.String, "");
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               ZxResult = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "CancelQpGd 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }

       //获取切片评级原因字典表
       public DataTable GetQpPj_Data()
       {
           string sqlstr = "select id,pj_info from qp_pjinfo_dict order by id asc";
           DataTable dt = null;
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetQpPj_Data 执行语句异常：" + sqlstr);
           }
           return dt;
       }
    }
}
