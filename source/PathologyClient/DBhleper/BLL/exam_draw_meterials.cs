using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DBhleper.BLL
{
   public class exam_draw_meterials
    {
        //更新取材备注
       public void UpdateDraw_Meterials(Int32 id, string memo_note)
        {
            string sqlstr = "update exam_draw_meterials set memo_note=@memo_note where  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                DBProcess._db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateDraw_Meterials 执行语句：" + sqlstr);
            }
        }

       //更新蜡块上脱水机时间 
       public int UpdateLkTs_DateTime(Int32 id)
       {
           int Zx_Result = 0;
           string sqlstr = "update exam_draw_meterials set tuoshui_datetime=now() where id=@id and tuoshui_datetime is null";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               Zx_Result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateLkTs_DateTime 执行语句：" + sqlstr);
           }
           return Zx_Result;
       }
       //更新取材材块号
       public void UpdateDraw_Meterials_N0(Int32 id, string meterial_no,string study_no)
       {
           string sqlstr = "update exam_draw_meterials set meterial_no=@meterial_no,barcode=@barcode where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@meterial_no", DbType.String, meterial_no);
               DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String, study_no + "-" + meterial_no);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateDraw_Meterials_N0 执行语句：" + sqlstr);
           }
       }
       //更新材块数目
       public void UpdateDraw_Group_num(Int32 id, int group_num)
       {
           string sqlstr = "update exam_draw_meterials set group_num=@group_num where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@group_num", DbType.String, group_num);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateDraw_Group_num 执行语句：" + sqlstr);
           }
       }
       //更新材块单位
       public void UpdateDraw_group_unite(Int32 id, string group_unite)
       {
           string sqlstr = "update exam_draw_meterials set group_unite=@group_unite where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@group_unite", DbType.String, group_unite);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateDraw_group_unite 执行语句：" + sqlstr);
           }
       }
       //更新材块部位
       public void UpdateParts(Int32 id, string parts)
       {
           string sqlstr = "update exam_draw_meterials set parts=@parts where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, parts);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateParts 执行语句：" + sqlstr);
           }
       }
       //更新备注信息
       public void UpdateMemo_note(Int32 id, string memo_note)
       {
           string sqlstr = "update exam_draw_meterials set memo_note=@memo_note where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateMemo_note 执行语句：" + sqlstr);
           }
       }
       //更新取材医师
       public void UpdateDraw_doctor_name(Int32 id, string draw_doctor_name)
       {
           string sqlstr = "update exam_draw_meterials set draw_doctor_name=@draw_doctor_name where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@draw_doctor_name", DbType.String, draw_doctor_name);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateDraw_doctor_name 执行语句：" + sqlstr);
           }
       }
       //更新任务来源 
       public void UpdateWork_source(Int32 id, string work_source)
       {
           string sqlstr = "update exam_draw_meterials set work_source=@work_source where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@work_source", DbType.String, work_source);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateWork_source 执行语句：" + sqlstr);
           }
       }
       //删除指定id下的取材信息
       public void DelMeterials(Int32 id)
       {
           string sqlstr = "delete from exam_draw_meterials where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "DelMeterials 执行语句：" + sqlstr);
           }
       }
       //插入一条取材记录
       public Boolean Insert_draw_meterials(string study_no,int specimens_id,string parts,string draw_doctor_name)
       {
           Boolean Zx_Result = false;
           string sqlstr = "";
           try
           {
                       //插入
                       sqlstr = "insert into exam_draw_meterials(study_no,specimens_id,meterial_no,barcode,work_source,parts,group_num,group_unite,memo_note,draw_doctor_name,draw_datetime,hd_flag)values(@study_no,@specimens_id,@meterial_no,@barcode,@work_source,@parts,@group_num,@group_unite,@memo_note,@draw_doctor_name,@draw_datetime,1)";
                       DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                       DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                       DBProcess._db.AddInParameter(cmd, "@specimens_id", DbType.Int32, specimens_id);
                       DBProcess._db.AddInParameter(cmd, "@meterial_no", DbType.String, "1");
                       DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String, study_no + "-1");
                       DBProcess._db.AddInParameter(cmd, "@work_source", DbType.String, "常规");
                       DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, parts);
                       DBProcess._db.AddInParameter(cmd, "@group_num", DbType.Int16,1);
                       DBProcess._db.AddInParameter(cmd, "@group_unite", DbType.String, "");
                       DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, "Auto");
                       DBProcess._db.AddInParameter(cmd, "@draw_doctor_name", DbType.String, draw_doctor_name);
                       DBProcess._db.AddInParameter(cmd, "@draw_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                       if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                       {
                           Zx_Result = true;
                       }
                       else
                       {
                           Zx_Result = false;
                       }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "Insert_draw_meterials 执行语句异常" + sqlstr);
               Zx_Result = false;
           }
           return Zx_Result;
       }




       public Boolean Process_draw_meterials(DataTable dtBB,string BLH, ref string Str_Result)
       {
           Boolean Zx_Result = false;
           string sqlstr = "";
           try
           {
               for (int i = 0; i < dtBB.Rows.Count; i++)
               {
                   string id = dtBB.Rows[i]["id"].ToString();
                   string new_str = dtBB.Rows[i]["new_flag"].ToString();
                   object rqh_str = dtBB.Rows[i]["meterial_no"] ?? i.ToString();
                   if (new_str.Equals("1") && id.Equals("0"))
                   {
                       //插入
                       sqlstr = "insert into exam_draw_meterials(study_no,specimens_id,meterial_no,barcode,work_source,parts,group_num,group_unite,memo_note,draw_doctor_name,draw_datetime,tuoshui_datetime,lktsdt_flag)values(@study_no,@specimens_id,@meterial_no,@barcode,@work_source,@parts,@group_num,@group_unite,@memo_note,@draw_doctor_name,@draw_datetime,@tuoshui_datetime,@lktsdt_flag)";
                       DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                       DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, BLH);
                       DBProcess._db.AddInParameter(cmd, "@specimens_id", DbType.Int32, Convert.ToInt32(dtBB.Rows[i]["specimens_id"]));
                       DBProcess._db.AddInParameter(cmd, "@meterial_no", DbType.String, rqh_str);
                       if (!rqh_str.Equals(""))
                       {
                           DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String, BLH + "-" + rqh_str);
                       }
                       else
                       {
                           DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String, BLH);
                       }
                       DBProcess._db.AddInParameter(cmd, "@work_source", DbType.String, dtBB.Rows[i]["work_source"].ToString());
                       DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, dtBB.Rows[i]["parts"].ToString());
                       DBProcess._db.AddInParameter(cmd, "@group_num", DbType.Int16, Convert.ToInt32(dtBB.Rows[i]["group_num"]));
                       DBProcess._db.AddInParameter(cmd, "@group_unite", DbType.String, dtBB.Rows[i]["group_unite"].ToString());
                       DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, dtBB.Rows[i]["memo_note"].ToString());
                       DBProcess._db.AddInParameter(cmd, "@draw_doctor_name", DbType.String, dtBB.Rows[i]["draw_doctor_name"].ToString());
                       DBProcess._db.AddInParameter(cmd, "@draw_datetime", DbType.DateTime, dtBB.Rows[i]["draw_datetime"].ToString());
                       if (dtBB.Rows[i]["lktsdt_flag"].ToString().Equals("快"))
                       {
                           DBProcess._db.AddInParameter(cmd, "@tuoshui_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                           DBProcess._db.AddInParameter(cmd, "@lktsdt_flag", DbType.String, "快");
                       }
                       else
                       {
                           DBProcess._db.AddInParameter(cmd, "@tuoshui_datetime", DbType.DateTime, DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
                           DBProcess._db.AddInParameter(cmd, "@lktsdt_flag", DbType.String, "慢");
                       }
                       if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                       {
                           Zx_Result = true;
                           Str_Result = "插入取材材块信息成功！";
                       }
                       else
                       {
                           Zx_Result = false;
                           Str_Result = "插入取材材块信息失败！";
                       }
                   }
                   else
                   {
                       Zx_Result = true;
                       Str_Result = "更新取材材块信息成功！";
                   }
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "Process_draw_meterials 执行语句异常" + sqlstr);
               Zx_Result = false;
               Str_Result = "处理取材材块信息异常：" + ex.ToString();
           }
           return Zx_Result;
       }

       //更新上脱水机时间
       public void UpdateTsjDt(Int32 id, string lktsdt_flag)
       {
           string sqlstr = "update exam_draw_meterials set tuoshui_datetime=@tuoshui_datetime,lktsdt_flag=@lktsdt_flag where  id=@id";
           try
           {
               
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               if (lktsdt_flag.Equals("快"))
               {
                   DBProcess._db.AddInParameter(cmd, "@tuoshui_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                   DBProcess._db.AddInParameter(cmd, "@lktsdt_flag", DbType.String, "快");
               }
               else
               {
                   DBProcess._db.AddInParameter(cmd, "@tuoshui_datetime", DbType.DateTime, DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss"));
                   DBProcess._db.AddInParameter(cmd, "@lktsdt_flag", DbType.String, "慢");
               }
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               DBProcess._db.ExecuteNonQuery(cmd);

           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateWork_source 执行语句：" + sqlstr);
           }
       }

       //标本接收模块标本展示
       public DataTable GetMeterialsInfo(string study_no, int specimens_id)
       {
           string sqlstr = "select id,study_no,specimens_id,meterial_no,work_source,parts,group_num,group_unite,memo_note,draw_doctor_name,date_format(draw_datetime,'%Y-%m-%d %H:%i:%s') as draw_datetime,lktsdt_flag from exam_draw_meterials where study_no=@study_no and specimens_id=@specimens_id  order by id asc";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               DBProcess._db.AddInParameter(cmd, "@specimens_id", DbType.String, specimens_id);
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               cmd.Parameters.Clear();
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return ds.Tables[0];
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetMeterialsInfo 执行语句异常：" + sqlstr);
           }
           return null;

       }

       //获取指定病理号的取材数目
       public int GetMeterialsCount(string study_no){
           int result = 0;
           string sqlstr = "select count(*) as sl  from  exam_draw_meterials where study_no=@study_no";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               result =Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetMeterialsCount 执行语句异常：" + sqlstr);
           }
           return result;
       }
        //查询指定标本id下的取材数目 
       public int GetSpecimens_MeterialsCount(int specimens_id)
       {
           int result = 0;
           string sqlstr = "select count(*) as sl  from  exam_draw_meterials where specimens_id=@specimens_id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@specimens_id", DbType.Int32, specimens_id);
               result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetSpecimens_MeterialsCount 执行语句异常：" + sqlstr);
           }
           return result;
       }
       //核对材块的数据
       public DataTable GetDtHdMeterials(string study_no)
       {
           string sqlstr = "select a.id as id, a.study_no as study_no,meterial_no,a.parts as parts,group_num,group_unite,draw_doctor_name,a.memo_note as memo_note,hd_flag,hd_flag as sfys,b.specimens_class as  specimens_class,work_source,barcode,a.bm_doc_name as bm_doc_name,date_format(a.tuoshui_datetime,'%Y-%m-%d %H:%i:%s')  as tuoshui_datetime from exam_draw_meterials a left join exam_specimens b on a.specimens_id =b.id where study_no=@study_no order by a.draw_datetime asc ";
           DataTable dt = null;
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetDtHdMeterials 执行语句异常：" + sqlstr);
           }
           return dt;
       }

       

       //更新核对信息
       public int UpdateHDDrawInfo(Int32 id)
       {
           int result = 0;
           string sqlstr = "update exam_draw_meterials set hd_flag=1 where  id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               result= DBProcess._db.ExecuteNonQuery(cmd);
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateHDDrawInfo 执行语句：" + sqlstr);
               result = 0;
           }
           return result;
       }

       //对取材信息全部核对
       public int UpdateAllHDDrawInfo(string  study_no)
       {
           int result = 0;
           string sqlstr = "update exam_draw_meterials set hd_flag=1 where  study_no=@study_no";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               result = DBProcess._db.ExecuteNonQuery(cmd);
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateAllHDDrawInfo 执行语句：" + sqlstr);
               result = 0;
           }
           return result;
       }
       //蜡块移交表
       public DataSet GetDsYjb(string startTime, string endTime)
       {
           string sqlstr = "select  study_no,count(*) as lks,draw_doctor_name,'' as hdr,'' as bz from  exam_draw_meterials where  tuoshui_datetime>='" + startTime + "' and tuoshui_datetime<='" + endTime + " 23:59:59' group by study_no order by study_no asc";
           DataSet ds = null;
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               ds = DBProcess._db.ExecuteDataSet(cmd);
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetDsYjb 执行语句异常：" + sqlstr);
           }
           return ds;
       }
       //获取待制片列表
       public DataSet GetDsStudyno(string startTime,string endTime)
       {
           string sqlstr = "select study_no from exam_wait_filmmaking exam_blh where draw_datetime>='" + startTime + "' and draw_datetime<='" + endTime + " 23:59:59' group by study_no  order by study_no asc";
           DataSet ds = null;
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               ds = DBProcess._db.ExecuteDataSet(cmd);
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetDsStudyno 执行语句异常：" + sqlstr);
           }
           return ds;
       }
       //获取待制片列表
       public DataSet GetDsDrawList(string startTime, string endTime)
       {
           string sqlstr = "select study_no, meterial_no,work_source,parts,group_num,memo_note from exam_wait_filmmaking exam_draw_meterials where draw_datetime>='" + startTime + "' and draw_datetime<='" + endTime + " 23:59:59'   order by meterial_no asc";
           DataSet ds = null;
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               ds = DBProcess._db.ExecuteDataSet(cmd);
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetDsDrawList 执行语句异常：" + sqlstr);
           }
           return ds;
       }
       //更新包埋医师
       public Boolean UpdateBmDocName(int id, string bm_doc_name)
       {
           Boolean ZxResult = true;
           string sqlstr = " update exam_draw_meterials set bm_doc_name=@bm_doc_name,bm_datetime=@bm_datetime where id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@bm_doc_name", DbType.String, bm_doc_name);
               DBProcess._db.AddInParameter(cmd, "@bm_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               if (result == 1)
               {
                  ZxResult=true;
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateBmDocName 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }
       public Boolean UpdateBmDocName(string study_no, string bm_doc_name)
       {
           Boolean ZxResult = true;
           string sqlstr = " update exam_draw_meterials set bm_doc_name=@bm_doc_name,bm_datetime=@bm_datetime where study_no=@study_no and bm_doc_name='' ";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@bm_doc_name", DbType.String, bm_doc_name);
               DBProcess._db.AddInParameter(cmd, "@bm_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               ZxResult = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateBmDocName 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }
       //查询指定的材块号是否存在
       public int CheckMeterialNo(string study_no, string meterial_no)
       {
           int Result = 0;
           string sqlstr = "select count(*) as sl from exam_draw_meterials where study_no=@study_no and  meterial_no=@meterial_no ";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               DBProcess._db.AddInParameter(cmd, "@meterial_no", DbType.String, meterial_no);

               Result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "CheckMeterialNo 执行语句异常：" + sqlstr);
               Result = 0;
           }
           return Result;
       }
       //获取材块取材部位
        public string GetMeterialPart( int id)
       {
           string Result = "";
           string sqlstr = "select parts from exam_draw_meterials where id=@id ";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               Result = Convert.ToString(DBProcess._db.ExecuteScalar(cmd));
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetMeterialPart 执行语句异常：" + sqlstr);
               Result = "";
           }
           return Result;
       }
       //插入一条包埋信息
       public int InsertBmInfo(string study_no, int specimens_id, string meterial_no, string work_source,string parts, int group_num,string memo_note, string bm_doc_name)
       {
           int Result = 0;
           string sqlstr = "insert into exam_draw_meterials(study_no,specimens_id,meterial_no,barcode,work_source,parts,group_num,memo_note,bm_datetime,bm_doc_name) values(@study_no,@specimens_id,@meterial_no,@barcode,@work_source,@parts,@group_num,@memo_note,@bm_datetime,@bm_doc_name)";
           try
           {
                       DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                       DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                       DBProcess._db.AddInParameter(cmd, "@specimens_id", DbType.Int32, specimens_id);
                       DBProcess._db.AddInParameter(cmd, "@meterial_no", DbType.String, meterial_no);
                       if (!meterial_no.Equals(""))
                       {
                           DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String, study_no + "-" + meterial_no);
                       }
                       else
                       {
                           DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String, study_no);
                       }
                       DBProcess._db.AddInParameter(cmd, "@work_source", DbType.String, work_source);
                       DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, parts);
                       DBProcess._db.AddInParameter(cmd, "@group_num", DbType.Int16, group_num);
                       DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
                       DBProcess._db.AddInParameter(cmd, "@bm_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                       DBProcess._db.AddInParameter(cmd, "@bm_doc_name", DbType.String, bm_doc_name);
                       if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                       {
                           Result = 1;
                       }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "InsertBmInfo 执行语句异常：" + sqlstr);
               Result = 0;
           }
           return Result;
       }
       //制片模块材块号列表
       public DataTable GetZpMeterialNo(string study_no)
       {
           string sqlstr = "select id,meterial_no from  exam_draw_meterials where study_no=@study_no order by meterial_no asc ";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               cmd.Parameters.Clear();
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return ds.Tables[0];
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetZpMeterialNo 执行语句异常：" + sqlstr);
           }
           return null;
       }
       //档案查询蜡块
       public DataTable GetLkInfo(string tj)
       {
           string sqlstr = "select id,study_no,meterial_no,parts,group_num,group_unite,draw_doctor_name,draw_datetime,memo_note,hd_flag,sfys,specimens_class,work_source,barcode,bm_doc_name,gd_flag,gd_doctor,gd_datetime,gd_location,bm_xh,tuoshui_datetime from exam_lk_view where 1=1 " + tj;
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
               DBProcess.ShowException(ex, "GetLkInfo 执行语句异常：" + sqlstr);
           }
           return null;
       }

       //获取材料块号字典
       public DataTable GetCkhDict()
       {
           string sqlstr = "select ckh,value from qc_ckh_dict order by order_xh asc";
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
               DBProcess.ShowException(ex, "GetCkhDict 执行语句异常：" + sqlstr);
           }
           return null;
       }

       //包埋信息
       public DataTable GetBminfo(string study_no, string draw_doctor_name, string draw_datetime,Boolean print_flag,Boolean sort=true)
       {
          
           StringBuilder sb = new StringBuilder();
           sb.Append("select id,study_no,work_source,meterial_no,parts,group_num,draw_doctor_name,date_format(draw_datetime,'%Y-%m-%d') AS draw_datetime,bm_doc_name,bm_qr,print_flag,memo_note,barcode,qp_barcode,bm_xh,date_format(tuoshui_datetime,'%Y-%m-%d %H:%i:%s') as tuoshui_datetime from exam_draw_meterials where hd_flag=1 and bm_qr=0 ");
           if(study_no.Equals("")){
               if (!draw_doctor_name.Equals(""))
               {
                   sb.AppendFormat(" and draw_doctor_name='{0}'",draw_doctor_name);
               }
               sb.AppendFormat(" and draw_datetime>='{0} 00:00:00' and draw_datetime<='{1} 23:59:59'", draw_datetime, draw_datetime);
           }else{
               sb.AppendFormat(" and study_no='{0}'", study_no);
           }
           if (print_flag)
           {
               sb.Append(" and print_flag=0 ");
           }
           if (sort)
           {
               sb.Append("  order by study_no asc,id asc");
           }
           else
           {
               sb.Append("  order by study_no desc,id asc");
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
               DBProcess.ShowException(ex, "GetBminfo 执行语句异常：" + sb.ToString());
           }
           return null;
       }
       //非脱钙包埋
       public DataTable GetFtgBminfo(string study_no, string draw_doctor_name, string draw_datetime, Boolean print_flag, Boolean sort = true)
       {

           StringBuilder sb = new StringBuilder();
           sb.Append("select id,study_no,work_source,meterial_no,parts,group_num,draw_doctor_name,date_format(draw_datetime,'%Y-%m-%d') AS draw_datetime,bm_doc_name,bm_qr,print_flag,memo_note,barcode,qp_barcode from exam_draw_meterials where hd_flag=1 and bm_qr=0 and  work_source<>'脱钙' ");
           if (study_no.Equals(""))
           {
               if (!draw_doctor_name.Equals(""))
               {
                   sb.AppendFormat(" and draw_doctor_name='{0}'", draw_doctor_name);
               }
               sb.AppendFormat(" and draw_datetime>='{0} 00:00:00' and draw_datetime<='{1} 23:59:59'", draw_datetime, draw_datetime);
           }
           else
           {
               sb.AppendFormat(" and study_no='{0}'", study_no);
           }
           if (print_flag)
           {
               sb.Append(" and print_flag=0 ");
           }
           if (sort)
           {
               sb.Append("  order by study_no asc,id asc");
           }
           else
           {
               sb.Append("  order by study_no desc,id asc");
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
               DBProcess.ShowException(ex, "GetBminfo 执行语句异常：" + sb.ToString());
           }
           return null;
       }
       //脱钙包埋信息
       public DataTable GetTgBminfo()
       {

           StringBuilder sb = new StringBuilder();
           sb.Append("select id,study_no,work_source,meterial_no,parts,group_num,draw_doctor_name,date_format(draw_datetime,'%Y-%m-%d') AS draw_datetime,bm_doc_name,bm_qr,print_flag,memo_note,barcode,qp_barcode,bm_xh from exam_draw_meterials where hd_flag=1 and bm_qr=0 and work_source='脱钙' ");
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
               DBProcess.ShowException(ex, "GettgBminfo 执行语句异常：" + sb.ToString());
           }
           return null;
       }



       //包埋切片号生成
       public string GetQPBarcode(string study_no, string pre_char)
       {
           string ResultStr = "";
           string sqlstr = "select max(qp_barcode) as qp_barcode from  exam_draw_meterials where study_no=@study_no";
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
                   int Result = Convert.ToInt32(str);
                   ResultStr = pre_char + (Result + 1).ToString();
               }
               else
               {
                   ResultStr = study_no + "01";
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetQPBarcode 执行语句异常：" + sqlstr);
               ResultStr = "";
           }
           return ResultStr;
       }
       //更新切片条码
       public Boolean UpdateQpBarcode(int id, string qp_barcode)
       {
           Boolean ZxResult = true;
           string sqlstr = " update exam_draw_meterials set qp_barcode=@qp_barcode,bm_xh=@bm_xh where id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@qp_barcode", DbType.String, qp_barcode);
               DBProcess._db.AddInParameter(cmd, "@bm_xh", DbType.String, qp_barcode.Substring(qp_barcode.Length - 2, 2));
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
       //更新打印状态
       public Boolean UpdatePrintFlag(int id)
       {
           Boolean ZxResult = true;
           string sqlstr = " update exam_draw_meterials set print_flag=1 where id=@id";
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
       //确认包埋完毕
       public Boolean UpdateBmQr(int id, string bm_doc_name)
       {
           Boolean ZxResult = true;
           string sqlstr = " update exam_draw_meterials set bm_qr=1, bm_doc_name=@bm_doc_name,bm_datetime=@bm_datetime where id=@id  ";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@bm_doc_name", DbType.String, bm_doc_name);
               DBProcess._db.AddInParameter(cmd, "@bm_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               ZxResult = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateBmQr 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }

       //更新蜡块归档
       public Boolean UpdateLkGd(int id, string gd_location, string gd_doctor)
       {
           Boolean ZxResult = true;
           string sqlstr = " update exam_draw_meterials set gd_flag=1,gd_doctor=@gd_doctor,gd_datetime=@gd_datetime,gd_location=@gd_location where id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@gd_doctor", DbType.String, gd_doctor);
               DBProcess._db.AddInParameter(cmd, "@gd_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
               DBProcess._db.AddInParameter(cmd, "@gd_location", DbType.String, gd_location);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               ZxResult = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateLkGd 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }
       public Boolean CancelLkGd(int id)
       {
           Boolean ZxResult = true;
           string sqlstr = " update exam_draw_meterials set gd_flag=0,gd_doctor=@gd_doctor,gd_datetime=@gd_datetime,gd_location=@gd_location where id=@id";
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@gd_doctor", DbType.String, "");
               DBProcess._db.AddInParameter(cmd, "@gd_datetime", DbType.DateTime, null);
               DBProcess._db.AddInParameter(cmd, "@gd_location", DbType.String, "");
               DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
               int result = DBProcess._db.ExecuteNonQuery(cmd);
               cmd.Parameters.Clear();
               ZxResult = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "CancelLkGd 执行语句异常：" + sqlstr);
           }
           return ZxResult;
       }
       //获取取材ID 
       public int GetDrawId(string study_no, string meterial_no)
       {
           StringBuilder sb = new StringBuilder();
           sb.Append("select id from exam_draw_meterials where study_no=@study_no and meterial_no=@meterial_no");
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sb.ToString());
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               DBProcess._db.AddInParameter(cmd, "@meterial_no", DbType.String, meterial_no);
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetDrawId 执行语句异常：" + sb.ToString());
           }
           return -1;

       }

    }
}
