using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DBhleper.BLL
{
    public  class exam_report
    {

        //报告ZK时间
        public DataTable GetReportTjZk(string tj)
        {
            string sqlstr = "select exam_type,modality,b.user_name as user_name, date_format(received_datetime,'%Y-%m-%d %H:%i:%s') AS received_datetime, zk_time from exam_report_view a left join sys_user_dict b on a.cbreport_doc_code =b.user_code where user_name<>'admin' and exam_status=55  " + tj;
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetReportTjZk 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }


        public Model.exam_report GetExam_Report(string study_no)
        {
            string sqlstr = "select study_no,zdyj,rysj,xbms,image,cbreport_doc_code,cbreport_doc_name,date_format(cbreport_datetime,'%Y-%m-%d %H:%i:%s') as cbreport_datetime,date_format(zzreport_datetime,'%Y-%m-%d %H:%i:%s') as  zzreport_datetime,date_format(shreport_datetime,'%Y-%m-%d %H:%i:%s') as shreport_datetime,zzreport_doc_code,zzreport_doc_name,shreport_doc_code,shreport_doc_name,lock_flag,zdpz,sfyx,zdbm,zdkey,lcfh,date_format(report_print_datetime,'%Y-%m-%d %H:%i:%s') as report_print_datetime,tmplet_index,report_childtmp_index,lk_num,bp_num,wy_study_no,report_gb_doc from exam_report where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DataTable dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
                cmd.Parameters.Clear();
                if (dt != null && dt.Rows.Count == 1)
                {
                    Model.exam_report Ins = new Model.exam_report();
                    Ins.study_no = study_no;
                    Ins.zdyj = dt.Rows[0]["zdyj"].ToString();
                    Ins.rysj = dt.Rows[0]["rysj"].ToString();
                    Ins.xbms = dt.Rows[0]["xbms"].ToString();
                    Ins.image = dt.Rows[0]["image"].ToString();
                    Ins.cbreport_doc_code = dt.Rows[0]["cbreport_doc_code"].ToString();
                    Ins.cbreport_doc_name = dt.Rows[0]["cbreport_doc_name"].ToString();
                    Ins.cbreport_datetime = dt.Rows[0]["cbreport_datetime"].ToString();
                    Ins.zzreport_datetime = dt.Rows[0]["zzreport_datetime"].ToString();
                    Ins.shreport_datetime = dt.Rows[0]["shreport_datetime"].ToString();
                    Ins.zzreport_doc_code = dt.Rows[0]["zzreport_doc_code"].ToString();
                    Ins.zzreport_doc_name = dt.Rows[0]["zzreport_doc_name"].ToString();
                    Ins.shreport_doc_code = dt.Rows[0]["shreport_doc_code"].ToString();
                    Ins.shreport_doc_name = dt.Rows[0]["shreport_doc_name"].ToString();
                    Ins.lock_flag = dt.Rows[0]["lock_flag"].ToString();
                    Ins.zdpz = dt.Rows[0]["zdpz"].ToString();
                    Ins.sfyx = dt.Rows[0]["sfyx"].ToString();
                    Ins.zdbm = dt.Rows[0]["zdbm"].ToString();
                    Ins.zdkey = dt.Rows[0]["zdkey"].ToString();
                    Ins.lcfh = dt.Rows[0]["lcfh"].ToString();
                    Ins.report_print_datetime = dt.Rows[0]["report_print_datetime"].ToString();
                    Ins.tmplet_index = dt.Rows[0]["tmplet_index"].ToString();
                    Ins.report_childtmp_index = dt.Rows[0]["report_childtmp_index"].ToString();
                    Ins.lk_num =Convert.ToInt32( dt.Rows[0]["lk_num"].ToString());
                    Ins.bp_num = Convert.ToInt32(dt.Rows[0]["bp_num"].ToString());
                    Ins.wy_study_no = dt.Rows[0]["wy_study_no"].ToString();
                    Ins.report_gb_doc = dt.Rows[0]["report_gb_doc"].ToString();
                    return Ins;
                }

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetExam_Report 执行语句异常：" + sqlstr);
            }
            return null;

        }
        //插入报告痕迹表 
        public Boolean SaveHis_Report(Model.exam_report InsM, string strDocCode, string StrDocName)
        {

            Boolean Zx_Result = false;
            string sqlstr = "INSERT INTO EXAM_HIS_REPORT(study_no,zdyj,rysj,xbms,save_doc_code,save_doc_name,zdpz,save_datetime)values(@study_no,@zdyj,@rysj,@xbms,@save_doc_code,@save_doc_name,@zdpz,@save_datetime)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, InsM.study_no);
                DBProcess._db.AddInParameter(cmd, "@zdyj", DbType.String, InsM.zdyj);
                DBProcess._db.AddInParameter(cmd, "@rysj", DbType.String, InsM.rysj);
                DBProcess._db.AddInParameter(cmd, "@xbms", DbType.String, InsM.xbms);
                DBProcess._db.AddInParameter(cmd, "@save_doc_code", DbType.String, strDocCode);
                DBProcess._db.AddInParameter(cmd, "@save_doc_name", DbType.String, StrDocName);
                DBProcess._db.AddInParameter(cmd, "@zdpz", DbType.String, InsM.zdpz);
                DBProcess._db.AddInParameter(cmd, "@save_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
                DBProcess.ShowException(ex, "SaveHis_Report 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }
        //报告痕迹列表  
        public DataTable GetReportHjInfo(string study_no)
        {
            string sqlstr = "select zdyj,rysj,xbms,save_doc_name,zdpz,save_datetime as save_datetime_dt,date_format(save_datetime,'%Y-%m-%d %H:%i:%s') as save_datetime from exam_his_report where study_no=@study_no order by save_datetime_dt asc";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DataTable dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
                cmd.Parameters.Clear();
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetReportHjInfo 执行语句异常：" + sqlstr);
            }
            return null;
        }
        //查询报告单 select * from exam_report_view
        public DataTable GetSqReportInfo(string tj)
        {
            string sqlstr = "select req_physician,patient_source,exam_type,cbreport_datetime,tmplet_index,req_dept from exam_report_view where " + tj;
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetSqReportInfo 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }


        //获取报告模板索引
        public int GetReportTemletIndex(string study_no)
        {
            int Report_Index = 0;
            string sqlstr = "select tmplet_index from exam_report  where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Report_Index = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetReportTemletIndex 执行语句异常：" + sqlstr);
            }
            return Report_Index;
        }


        //获取报告是否已经存在
        public int GetReportCount(string study_no)
        {
            int Report_Count = 0;
            string sqlstr = "select count(*)  from exam_report  where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Report_Count = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetReportCount 执行语句异常：" + sqlstr);
            }
            return Report_Count;
        }

        public Boolean SaveExam_history_report(string study_no,string xgreport_doc_code,string xgreport_doc_name)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_report where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 1)
                {
                    Model.exam_report InsM = GetExam_Report(study_no);
                    sqlstr = "insert into exam_history_report(study_no,zdyj,rysj,xbms,image,cbreport_doc_code,cbreport_doc_name,cbreport_datetime,lock_flag,zdpz,sfyx,zdbm,zdkey,lcfh,tmplet_index,xgreport_doc_code,xgreport_doc_name,xgreport_print_datetime) values(@study_no,@zdyj,@rysj,@xbms,@image,@cbreport_doc_code,@cbreport_doc_name,@cbreport_datetime,@lock_flag,@zdpz,@sfyx,@zdbm,@zdkey,@lcfh,@tmplet_index,@xgreport_doc_code,@xgreport_doc_name,@xgreport_print_datetime)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, InsM.study_no);
                    DBProcess._db.AddInParameter(cmd, "@zdyj", DbType.String, InsM.zdyj);
                    DBProcess._db.AddInParameter(cmd, "@rysj", DbType.String, InsM.rysj);
                    DBProcess._db.AddInParameter(cmd, "@xbms", DbType.String, InsM.xbms);
                    DBProcess._db.AddInParameter(cmd, "@image", DbType.String, InsM.image);
                    DBProcess._db.AddInParameter(cmd, "@cbreport_doc_code", DbType.String, InsM.cbreport_doc_code);
                    DBProcess._db.AddInParameter(cmd, "@cbreport_doc_name", DbType.String, InsM.cbreport_doc_name);
                    DBProcess._db.AddInParameter(cmd, "@cbreport_datetime", DbType.DateTime, InsM.cbreport_datetime);
                    DBProcess._db.AddInParameter(cmd, "@lock_flag", DbType.Int16, InsM.lock_flag);
                    DBProcess._db.AddInParameter(cmd, "@zdpz", DbType.String, InsM.zdpz);
                    DBProcess._db.AddInParameter(cmd, "@sfyx", DbType.String, InsM.sfyx);
                    DBProcess._db.AddInParameter(cmd, "@zdbm", DbType.String, InsM.zdbm);
                    DBProcess._db.AddInParameter(cmd, "@zdkey", DbType.String, InsM.zdkey);
                    DBProcess._db.AddInParameter(cmd, "@lcfh", DbType.String, InsM.lcfh);
                    DBProcess._db.AddInParameter(cmd, "@tmplet_index", DbType.String, InsM.tmplet_index);
                    DBProcess._db.AddInParameter(cmd, "@xgreport_doc_code", DbType.String, xgreport_doc_code);
                    DBProcess._db.AddInParameter(cmd, "@xgreport_doc_name", DbType.String, xgreport_doc_name);
                    DBProcess._db.AddInParameter(cmd, "@xgreport_print_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                    }
                    else
                    {
                        Zx_Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "SaveExam_history_report 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }

        public int GetExam_history_report_Count(string study_no)
        {
            int Zx_Result = 0;
            string sqlstr = "select count(*) as sl from exam_history_report where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Zx_Result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetExam_history_report_Count 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }

        public Boolean SaveExam_Report(Model.exam_report InsM, int tmplet_index)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_report where study_no=@study_no";
            try
            {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, InsM.study_no);
               int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
               cmd.Parameters.Clear();
               if (sl == 0)
               {
                   sqlstr = "insert into exam_report(study_no,zdyj,rysj,xbms,image,cbreport_doc_code,cbreport_doc_name,zzreport_doc_code,zzreport_doc_name,cbreport_datetime,lock_flag,zdpz,sfyx,zdbm,zdkey,lcfh,tmplet_index,report_childtmp_index,lk_num,bp_num,wy_study_no,zzreport_datetime,report_gb_doc) values(@study_no,@zdyj,@rysj,@xbms,@image,@cbreport_doc_code,@cbreport_doc_name,@zzreport_doc_code,@zzreport_doc_name,@cbreport_datetime,@lock_flag,@zdpz,@sfyx,@zdbm,@zdkey,@lcfh,@tmplet_index,@report_childtmp_index,@lk_num,@bp_num,@wy_study_no,@zzreport_datetime,@report_gb_doc)";
                   cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                   DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, InsM.study_no);
                   DBProcess._db.AddInParameter(cmd, "@zdyj", DbType.String, InsM.zdyj);
                   DBProcess._db.AddInParameter(cmd, "@rysj", DbType.String, InsM.rysj);
                   DBProcess._db.AddInParameter(cmd, "@xbms", DbType.String, InsM.xbms);
                   DBProcess._db.AddInParameter(cmd, "@image", DbType.String, InsM.image);
                   DBProcess._db.AddInParameter(cmd, "@cbreport_doc_code", DbType.String, InsM.cbreport_doc_code);
                   DBProcess._db.AddInParameter(cmd, "@cbreport_doc_name", DbType.String, InsM.cbreport_doc_name);
                   DBProcess._db.AddInParameter(cmd, "@zzreport_doc_code", DbType.String, InsM.zzreport_doc_code);
                   DBProcess._db.AddInParameter(cmd, "@zzreport_doc_name", DbType.String, InsM.zzreport_doc_name);
                   DBProcess._db.AddInParameter(cmd, "@cbreport_datetime", DbType.DateTime, InsM.cbreport_datetime);
                   DBProcess._db.AddInParameter(cmd, "@lock_flag", DbType.Int16, InsM.lock_flag);
                   DBProcess._db.AddInParameter(cmd, "@zdpz", DbType.String, InsM.zdpz);
                   DBProcess._db.AddInParameter(cmd, "@sfyx", DbType.String, InsM.sfyx);
                   DBProcess._db.AddInParameter(cmd, "@zdbm", DbType.String, InsM.zdbm);
                   DBProcess._db.AddInParameter(cmd, "@zdkey", DbType.String, InsM.zdkey);
                   DBProcess._db.AddInParameter(cmd, "@lcfh", DbType.String, InsM.lcfh);
                   DBProcess._db.AddInParameter(cmd, "@tmplet_index", DbType.String, tmplet_index);
                   DBProcess._db.AddInParameter(cmd, "@report_childtmp_index", DbType.String,InsM.report_childtmp_index);
                   DBProcess._db.AddInParameter(cmd, "@lk_num", DbType.Int32, InsM.lk_num);
                   DBProcess._db.AddInParameter(cmd, "@bp_num", DbType.Int32, InsM.bp_num);
                   DBProcess._db.AddInParameter(cmd, "@wy_study_no", DbType.String, InsM.wy_study_no);
                   DBProcess._db.AddInParameter(cmd, "@zzreport_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                   DBProcess._db.AddInParameter(cmd, "@report_gb_doc", DbType.String, InsM.report_gb_doc);
                   if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                   {
                       Zx_Result = true;
                   }
                   else
                   {
                       Zx_Result = false;
                   }
               }
               else if (sl == 1)
               {
                   sqlstr = "update exam_report set zdyj=@zdyj,rysj=@rysj,xbms=@xbms,image=@image,cbreport_doc_code=@cbreport_doc_code,cbreport_doc_name=@cbreport_doc_name,cbreport_datetime=@cbreport_datetime,lock_flag=@lock_flag,zdpz=@zdpz,sfyx=@sfyx,zdbm=@zdbm,zdkey=@zdkey,lcfh=@lcfh,report_childtmp_index=@report_childtmp_index,lk_num=@lk_num,bp_num=@bp_num,wy_study_no=@wy_study_no,zzreport_doc_code=@zzreport_doc_code,zzreport_doc_name=@zzreport_doc_name,zzreport_datetime=@zzreport_datetime,report_gb_doc=@report_gb_doc where study_no=@study_no";
                   cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                  
                   DBProcess._db.AddInParameter(cmd, "@zdyj", DbType.String, InsM.zdyj);
                   DBProcess._db.AddInParameter(cmd, "@rysj", DbType.String, InsM.rysj);
                   DBProcess._db.AddInParameter(cmd, "@xbms", DbType.String, InsM.xbms);
                   DBProcess._db.AddInParameter(cmd, "@image", DbType.String, InsM.image);
                   DBProcess._db.AddInParameter(cmd, "@cbreport_doc_code", DbType.String, InsM.cbreport_doc_code);
                   DBProcess._db.AddInParameter(cmd, "@cbreport_doc_name", DbType.String, InsM.cbreport_doc_name);
                   DBProcess._db.AddInParameter(cmd, "@cbreport_datetime", DbType.DateTime, InsM.cbreport_datetime);
                   DBProcess._db.AddInParameter(cmd, "@lock_flag", DbType.Int16, InsM.lock_flag);
                   DBProcess._db.AddInParameter(cmd, "@zdpz", DbType.String, InsM.zdpz);
                   DBProcess._db.AddInParameter(cmd, "@sfyx", DbType.String, InsM.sfyx);
                   DBProcess._db.AddInParameter(cmd, "@zdbm", DbType.String, InsM.zdbm);
                   DBProcess._db.AddInParameter(cmd, "@zdkey", DbType.String, InsM.zdkey);
                   DBProcess._db.AddInParameter(cmd, "@lcfh", DbType.String, InsM.lcfh);
                   DBProcess._db.AddInParameter(cmd, "@report_childtmp_index", DbType.String, InsM.report_childtmp_index);
                   DBProcess._db.AddInParameter(cmd, "@lk_num", DbType.Int32, InsM.lk_num);
                   DBProcess._db.AddInParameter(cmd, "@bp_num", DbType.Int32, InsM.bp_num);
                   DBProcess._db.AddInParameter(cmd, "@wy_study_no", DbType.String, InsM.wy_study_no);
                   DBProcess._db.AddInParameter(cmd, "@zzreport_doc_code", DbType.String, InsM.zzreport_doc_code);
                   DBProcess._db.AddInParameter(cmd, "@zzreport_doc_name", DbType.String, InsM.zzreport_doc_name);
                   DBProcess._db.AddInParameter(cmd, "@zzreport_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                   DBProcess._db.AddInParameter(cmd, "@report_gb_doc", DbType.String, InsM.report_gb_doc);
                   DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, InsM.study_no);
                   if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                   {
                       Zx_Result = true;
                   }
                   else
                   {
                       Zx_Result = false;
                   }

               }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "SaveExam_Report 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }

        public Boolean UpdateReport_Image(string study_no,string ImageStr)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_report where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 1)
                {
                    sqlstr = "update exam_report set image=@image where study_no=@study_no";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@image", DbType.String, ImageStr);
                    DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                    }
                    else
                    {
                        Zx_Result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateReport_Image 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }

        //发送报告给主治医师
        public Boolean SendReportToZzys(string study_no, string user_code, string user_name)
        {
            Boolean Zx_Result = false;
            string sqlstr = "update exam_report set zzreport_datetime=@zzreport_datetime,zzreport_doc_code=@zzreport_doc_code,zzreport_doc_name=@zzreport_doc_name,lock_flag=0 where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@zzreport_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                DBProcess._db.AddInParameter(cmd, "@zzreport_doc_code", DbType.String, user_code);
                DBProcess._db.AddInParameter(cmd, "@zzreport_doc_name", DbType.String, user_name);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
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
                DBProcess.ShowException(ex, "SendReportToZzys 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }
        //审核报告
        public Boolean ShReport(string study_no, string user_code, string user_name)
        {
            Boolean Zx_Result = false;
            string sqlstr = "update exam_report set shreport_datetime=@shreport_datetime,shreport_doc_code=@shreport_doc_code,shreport_doc_name=@shreport_doc_name,lock_flag=0 where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@shreport_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                DBProcess._db.AddInParameter(cmd, "@shreport_doc_code", DbType.String, user_code);
                DBProcess._db.AddInParameter(cmd, "@shreport_doc_name", DbType.String, user_name);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    Zx_Result = true;
                }
                else
                {
                    Zx_Result = false;
                }
            }catch(Exception ex){
                DBProcess.ShowException(ex, "ShReport 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }
        //打印报告
        public Boolean PrintReport(string study_no)
        {
            Boolean Zx_Result = false;
            string sqlstr = "update exam_report set report_print_datetime=@report_print_datetime where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@report_print_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
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
                DBProcess.ShowException(ex, "PrintReport 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }
        //获取报告医师信息
        public DataTable GetReportYsInfo(string study_no)
        {
            string sqlstr = "select cbreport_doc_code,zzreport_doc_code,shreport_doc_code from exam_report where study_no='" + study_no + "'";

            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
              
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetReportYsInfo 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
        //获取档案报告
        public DataTable GetReportList(string tj)
        {
            string sqlstr = "select study_no,modality_cn,status_name,patient_name,sex,age,patient_source,zdyj,zdpz,rysj,req_physician,req_dept,submit_unit,cbreport_doc_name,zzreport_doc_name,shreport_doc_name,received_datetime,cbreport_datetime,zzreport_datetime,shreport_datetime,report_print_datetime, zk_time as sj,sfyx,lcfh,exam_no,exam_type,modality,exam_status,parts,qucai_doctor_name,qucai_datetime,patient_id,input_id,hospital_card,delay_reason from exam_report_view where 1=1   " + tj;
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetReportList 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
        public DataTable GetThirdReportList(string tj)
        {
            string sqlstr = "select req_date_time,study_no,modality_cn,status_name,patient_name,sex,age,patient_source,zdyj,zdpz,rysj,req_physician,req_dept,submit_unit,cbreport_doc_name,zzreport_doc_name,shreport_doc_name,received_datetime,cbreport_datetime,zzreport_datetime,shreport_datetime,report_print_datetime, zk_time as sj,sfyx,lcfh,exam_no,exam_type,modality,exam_status,parts,qucai_doctor_name,qucai_datetime,patient_id,input_id,hospital_card,delay_reason,ice_flag,ks_flag from exam_third_report_view where 1=1   " + tj;
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetReportList 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
        //获取历史报告
        public DataTable GetHisReportList(string tj)
        {
            string sqlstr = "select exam_no,exam_type,study_no,modality_cn,patient_name,sex,age,patient_source,req_dept,req_physician,submit_unit,status_name,modality,zdyj,rysj,zdpz,cbreport_doc_name,cbreport_datetime,report_print_datetime,received_datetime,sfyx,lcfh from djb_view where 1=1  and " + tj;
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetHisReportList 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
        //获取报告列表
        public DataTable GetPrintReportList(string tj)
        {
            string sqlstr = "select exam_no,exam_type,study_no,modality_cn,patient_name,sex,age,patient_source,req_dept,req_physician,submit_unit,status_name,modality,cbreport_doc_name,cbreport_datetime,zzreport_datetime,shreport_datetime,zzreport_doc_name,shreport_doc_name,report_print_datetime,received_datetime from exam_report_list where  " + tj;
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetReportList 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
        //获取报告修改痕迹列表
        public DataTable GetHistoryReportList(string study_no)
        {
            string sqlstr = "select exam_no,exam_type,study_no,modality_cn,patient_name,sex,age,patient_source,req_dept,req_physician,submit_unit,status_name,modality,zdyj,rysj,cbreport_doc_name,cbreport_datetime,zzreport_datetime,shreport_datetime,zzreport_doc_name,shreport_doc_name,zdpz,report_print_datetime,received_datetime, zk_time as sj,sfyx,lcfh,xgreport_doc_name,xgreport_print_datetime from exam_history_report_view where  study_no='" + study_no + "'";
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetHistoryReportList 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }



        public DataTable GetReportLcfhList(string tj)
        {
            string sqlstr = "select lcfh from exam_report_view  where  " + tj;
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetReportLcfhList 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
        //收藏报告
        public DataTable GetScReportList(string tj)
        {
            string sqlstr = "select exam_no,exam_type,a.study_no as study_no,modality_cn,patient_name,sex,age,patient_source,req_dept,req_physician,submit_unit,status_name,modality,zdyj,rysj,cbreport_doc_name,cbreport_datetime,zzreport_datetime,shreport_datetime,zzreport_doc_name,shreport_doc_name,zdpz,report_print_datetime,zdbm,gjc,memo_note,doc_code,doc_name,date_format(create_datetime,'%Y-%m-%d %H:%i:%s') AS create_datetime,exam_status from sc_report b  left join exam_report_view a  on a.study_no=b.study_no where 1=1  " + tj;
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetScReportList 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }


        //获取修改病人列表
        public DataTable GetModityList(string tj)
        {
            string sqlstr = "";
            if (tj.Trim() != "")
            {
                tj = " and study_no='" + tj.Trim().Replace("'", "''") + "'";
                sqlstr = "select exam_no,study_no,modality_cn,patient_name,sex,age,patient_source,status_name,a.modality as modality,exam_type, b.status_code as curstatus,date_format(req_date_time,'%Y-%m-%d %H:%i:%s') AS req_date_time from exam_master a inner join exam_status_dict b on a.exam_status=b.status_code inner join exam_type_dict c on a.modality=c.modality   inner join exam_pat_mi d on a.patient_id=d.patient_id where status_code>=20 and status_code<55   " + tj;
                
            }
            else
            {
                sqlstr = "select exam_no,study_no,modality_cn,patient_name,sex,age,patient_source,status_name,a.modality as modality,exam_type, b.status_code as curstatus,date_format(req_date_time,'%Y-%m-%d %H:%i:%s') AS req_date_time from exam_master a inner join exam_status_dict b on a.exam_status=b.status_code inner join exam_type_dict c on a.modality=c.modality   inner join exam_pat_mi d on a.patient_id=d.patient_id where status_code>=20 and status_code<55  and date_sub(curdate(),interval 30 day)<=date(req_date_time)  ";
            }

            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetModityList 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
        //获取报告模板
        public DataTable GetReportTemp(string exam_type)
        {
            string tj = "";
           
            tj = " and big_type in ("+exam_type+ ")";
           
            string sqlstr = "select temp_name,temp_enable,temp_index,temp_txt,sort_index from report_temp_dict where temp_enable=1 "+tj+" order by sort_index asc";
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetReportTemp 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
        //更新报告模板
        public Boolean UpdateReportIndex(string study_no, int tmplet_index, string exam_status)
        {

            string sqlstr = "update exam_master set exam_status=@exam_status where study_no=@study_no";
            Boolean ZxResult = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_status", DbType.String, exam_status);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
                sqlstr = "update exam_report set tmplet_index=@tmplet_index where study_no=@study_no";
                cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@tmplet_index", DbType.Int16, tmplet_index);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.ExecuteNonQuery(cmd);
                ZxResult = true;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateReportIndex 执行语句：" + sqlstr);
            }
            return ZxResult;
        }
        //统计阳性率
        public DataTable GetYxl(string tj)
        {
            string sqlstr = "select sfyx,count(sfyx) as sl from exam_report where " +  tj  + " group by sfyx";
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetYxl 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }

        //报告接收单查询
        public DataSet GetBgJsd(string startTime, string endTime, string tj)
        {
            string sqlstr = string.Format("select study_no,req_dept,patient_name from  exam_report_view where report_print_datetime>='{0}' and report_print_datetime<='{1}' {2} order by study_no asc", startTime, endTime,  tj);
            DataSet ds = null;
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetBgJsd：" + sqlstr);
                ds = null;
            }
            return ds;
        }

        //登记薄打印列表
        public DataSet GetDsDjbPrintList(string tj)
        {
            string sqlstr = "select  * from  exam_djb_view where  "+tj+"  order by study_no asc";
            DataSet ds = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                ds = DBProcess._db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsDjbPrintList 执行语句异常：" + sqlstr);
            }
            return ds;
        }
        //获取报告质控列表 
        public DataTable GetReportLimitList()
        {
            string sqlstr = "select study_no,modality_cn,status_name,patient_name,patient_source,req_physician,req_dept,submit_unit,received_datetime,TIMESTAMPDIFF(minute,received_datetime,now()) as sj,exam_no,exam_type,modality,ks_flag,ice_flag from exam_master_view where exam_status>=20 and exam_status<55 order by sj desc";
            DataSet ds = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                ds = DBProcess._db.ExecuteDataSet(cmd);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetReportLimitList 执行语句异常：" + sqlstr);
            }
            return null;
        }
        //获取诊断批注 
        public string Getzdpz(string study_no)
        {
            string ret = "";
            string sqlstr = "select zdpz from exam_report where  study_no='" + study_no + "'";
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    ret = dt.Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Getzdpz 执行语句：" + sqlstr);
                dt = null;
            }
            return ret;
        }
        //病理整体报告
        public string GetReportImgPath(string exam_no, int img_type)
        {
            string ReportPath="";
            //定义数据库操作对象
            Microsoft.Practices.EnterpriseLibrary.Data.Database _db = Microsoft.Practices.EnterpriseLibrary.Common.Configuration.EnterpriseLibraryContainer.Current.GetInstance<Microsoft.Practices.EnterpriseLibrary.Data.Database>("mysql");
            string sqlstr = "select count(*) from exam_master where  exam_status>=55 and exam_no=@exam_no ";
            System.Data.Common.DbCommand cmd = _db.GetSqlStringCommand(sqlstr);
            cmd = _db.GetSqlStringCommand(sqlstr);
            _db.AddInParameter(cmd, "@exam_no", System.Data.DbType.String, exam_no);
            int icount = Convert.ToInt32(_db.ExecuteScalar(cmd));
            cmd.Parameters.Clear();
            if (icount > 0)
            {
                sqlstr = "select study_no from exam_master where exam_status>=55 and exam_no=@exam_no ";
                cmd = _db.GetSqlStringCommand(sqlstr);
                _db.AddInParameter(cmd, "@exam_no", System.Data.DbType.String, exam_no);
                string study_no = Convert.ToString(_db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                sqlstr = "select path,filename from multi_media_info where  del_flag=0  and media_type=" + img_type + " and study_no=@study_no order by id asc";
                cmd = _db.GetSqlStringCommand(sqlstr);
                _db.AddInParameter(cmd, "@study_no", System.Data.DbType.String, study_no);
                System.Data.DataSet ds = _db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count == 1)
                {
                    ReportPath = ds.Tables[0].Rows[0]["path"].ToString().Replace("/", @"\");
                }
                else
                {
                    ReportPath = "";
                }
            }
            else
            {
                ReportPath = "";
            }
            return ReportPath;
        }

    }
}
