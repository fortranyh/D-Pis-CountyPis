using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DBhleper.BLL
{
    public class exam_tjyz
    {
        //免疫组化设备参数  

        public DataSet GetInfo(string sqlstr)
        {
            DataSet ds = null;
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetInfo 执行语句：" + sqlstr);
            }
            return ds;
        }


        //数据
        public DataTable GetData(string study_no)
        {
            string sqlstr = "select  id,exam_no,study_no,lk_no,work_source,bj_name,group_num,sq_doctor_name,yz_flag,fx_result,zx_doc_name,barcode,memo_note,zc_flag, date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') as sq_datetime,date_format(zx_datetime,'%Y-%m-%d %H:%i:%s') as zx_datetime,qc_id from exam_tjyz where study_no=@study_no";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetData 执行语句异常：" + sqlstr);
            }
            return dt;
        }

        public DataTable GetDataTjyz(string study_no)
        {
            string sqlstr = "select  id,exam_no,study_no,lk_no,work_source,bj_name,group_num,sq_doctor_name,yz_flag,fx_result,zx_doc_name,barcode,memo_note,zc_flag, date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') as sq_datetime,date_format(zx_datetime,'%Y-%m-%d %H:%i:%s') as zx_datetime,qc_id,myzh_yl,myzh_bz,fz_hg,fz_bz,taocan_type from exam_tjyz where study_no=@study_no";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetData 执行语句异常：" + sqlstr);
            }
            return dt;
        }
        //数据
        public DataTable GetData(string study_no,string work_source,string lk_no)
        {
            string sqlstr = "select  id,exam_no,study_no,lk_no,work_source,bj_name,group_num,sq_doctor_name,yz_flag,fx_result,zx_doc_name,barcode,memo_note,zc_flag, date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') as sq_datetime,date_format(zx_datetime,'%Y-%m-%d %H:%i:%s') as zx_datetime,qc_id,1 as chk from exam_tjyz where study_no=@study_no and lk_no=@lk_no and work_source=@work_source and zc_flag=0 and yz_flag='申请' ";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@lk_no", DbType.String, lk_no);
                DBProcess._db.AddInParameter(cmd, "@work_source", DbType.String, work_source);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetData 执行语句异常：" + sqlstr);
            }
            return dt;
        }

        //已经执行特检医嘱数据
        public DataTable GetTjYzxData(string tj)
        {
            string sqlstr = "select  id,exam_no,study_no,lk_no,work_source,bj_name,group_num,sq_doctor_name,yz_flag,fx_result,zx_doc_name,barcode,memo_note,zc_flag, date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') as sq_datetime,date_format(zx_datetime,'%Y-%m-%d %H:%i:%s') as zx_datetime,qc_id,0 as chk,dy_flag from exam_tjyz where  zc_flag=0 and yz_flag='已执行' " + tj;
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetTjYzxData 执行语句异常：" + sqlstr);
            }
            return dt;
        }
        //特检切片已执行列表
        public DataTable GetTjlbData(string tj)
        {
            string sqlstr = "select  id,exam_no,study_no,lk_no,work_source,bj_name,group_num,sq_doctor_name,yz_flag,fx_result,zx_doc_name,barcode,memo_note,zc_flag, date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') as sq_datetime,date_format(zx_datetime,'%Y-%m-%d %H:%i:%s') as zx_datetime,qc_id,fz_hg,myzh_yl from exam_tjyz where yz_flag='已执行' and "+ tj + " order by zx_datetime desc";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetTjlbData 执行语句异常：" + sqlstr);
            }
            return dt;
        }
        //分子病理各项指标
        public DataTable GetTjFzData(string tj)
        {
            string sqlstr = "select DISTINCT bj_name from exam_tjyz where yz_flag='已执行'  and taocan_type='分子病理' and " + tj + " group by bj_name desc"; ;
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetTjFzData 执行语句异常：" + sqlstr);
            }
            return dt;
        }

        //更新免疫组化染色切片优良
        public int UpdateMyzh_Yl(Int32 id, string myzh_yl)
        {
            int result = 0;
            string sqlstr = "update exam_tjyz set myzh_yl=@myzh_yl where  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@myzh_yl", DbType.String, myzh_yl);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                result = DBProcess._db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateMyzh_Yl 执行语句：" + sqlstr);
                result = 0;
            }
            return result;
        }

        

       //更新特检医嘱已经打印
        public int UpdatePrintFlag(Int32 id)
        {
            int result = 0;
            string sqlstr = "update exam_tjyz set dy_flag='已打印' where  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdatePrintFlag 执行语句：" + sqlstr);
                result = 0;
            }
            return result;
        }
        public int UpdateMyzh_jyb(Int32 id, string myzh_yl,string myzh_bz)
        {
            int result = 0;
            string sqlstr = "update exam_tjyz set myzh_yl=@myzh_yl,myzh_bz=@myzh_bz where  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@myzh_yl", DbType.String, myzh_yl);
                DBProcess._db.AddInParameter(cmd, "@myzh_bz", DbType.String, myzh_bz);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                result = DBProcess._db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateMyzh_jyb 执行语句：" + sqlstr);
                result = 0;
            }
            return result;
        }


        //更新分子病理室内质控
        public int UpdateFzhg(Int32 id, string fz_hg,string fz_bz)
        {
            int result = 0;
            string sqlstr = "update exam_tjyz set fz_hg=@fz_hg,fz_bz=@fz_bz where  id=@id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@fz_hg", DbType.String, fz_hg);
                DBProcess._db.AddInParameter(cmd, "@fz_bz", DbType.String, fz_bz);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                result = DBProcess._db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateFzhg 执行语句：" + sqlstr);
                result = 0;
            }
            return result;
        }
        public DataTable GetTjyzData()
        {
            string sqlstr = "select study_no,lk_no,work_source  from exam_tjyz where zc_flag=0 and yz_flag='申请' group by study_no,lk_no,work_source";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetTjyzData 执行语句异常：" + sqlstr);
            }
            return dt;
        }

        
        //数据
        public Boolean DelData(int id)
        {
            string sqlstr = "delete  from exam_tjyz where id=@id";
            Boolean Zx_Result = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
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
                DBProcess.ShowException(ex, "DelData 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }
        public Boolean InsertData(string exam_no, string study_no, string lk_no, string work_source, string bj_name, int group_num, string sq_doctor_name, string sq_datetime, string yz_flag, string barcode, string memo_note, int zc_flag, int qc_id, string taocan_type = "", string myzh_yl = "", string fz_hg="")
        {
            Boolean Zx_Result = false;
            string sqlstr = "insert into exam_tjyz(exam_no,study_no,lk_no,work_source,bj_name,group_num,sq_doctor_name,sq_datetime,yz_flag,barcode,memo_note,zc_flag,qc_id,taocan_type,myzh_yl,fz_hg) values(@exam_no,@study_no,@lk_no,@work_source,@bj_name,@group_num,@sq_doctor_name,@sq_datetime,@yz_flag,@barcode,@memo_note,@zc_flag,@qc_id,@taocan_type,@myzh_yl,@fz_hg)";
            try
            {
                //插入
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@lk_no", DbType.String, lk_no);
                DBProcess._db.AddInParameter(cmd, "@work_source", DbType.String, work_source);
                DBProcess._db.AddInParameter(cmd, "@bj_name", DbType.String, bj_name);
                DBProcess._db.AddInParameter(cmd, "@group_num", DbType.Int32, group_num);
                DBProcess._db.AddInParameter(cmd, "@sq_doctor_name", DbType.String, sq_doctor_name);
                DBProcess._db.AddInParameter(cmd, "@sq_datetime", DbType.DateTime, sq_datetime);
                DBProcess._db.AddInParameter(cmd, "@yz_flag", DbType.String, yz_flag);
                DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String, barcode);
                DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
                DBProcess._db.AddInParameter(cmd, "@zc_flag", DbType.Int16, zc_flag);
                DBProcess._db.AddInParameter(cmd, "@qc_id", DbType.Int16, qc_id);
                DBProcess._db.AddInParameter(cmd, "@taocan_type", DbType.String, taocan_type);
                DBProcess._db.AddInParameter(cmd, "@myzh_yl", DbType.String, myzh_yl);
                DBProcess._db.AddInParameter(cmd, "@fz_hg", DbType.String, fz_hg);
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
                DBProcess.ShowException(ex, "InsertData 执行语句异常：" + sqlstr);
                Zx_Result = false;
            }
            return Zx_Result;
        }

        public Boolean UpdateZCData(string study_no)
        {
            string sqlstr = "update exam_tjyz set zc_flag=0 where zc_flag=1 and study_no=@study_no";
            Boolean Zx_Result = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.ExecuteNonQuery(cmd);

                Zx_Result = true;

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateZCData 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }

        public Boolean UpdateResultData(int id, string fx_result)
        {
            string sqlstr = "update exam_tjyz set fx_result=@fx_result where  id=@id";
            Boolean Zx_Result = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@fx_result", DbType.String, fx_result);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                DBProcess._db.ExecuteNonQuery(cmd);
                Zx_Result = true;

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateResultData 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }
        public Boolean Updatememo_note(int id, string memo_note)
        {
            string sqlstr = "update exam_tjyz set memo_note=@memo_note where  id=@id";
            Boolean Zx_Result = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                DBProcess._db.ExecuteNonQuery(cmd);
                Zx_Result = true;

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Updatememo_note 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }
        public Boolean Updatemyzh_bz(int id, string myzh_bz)
        {
            string sqlstr = "update exam_tjyz set myzh_bz=@myzh_bz where  id=@id";
            Boolean Zx_Result = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@myzh_bz", DbType.String, myzh_bz);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                DBProcess._db.ExecuteNonQuery(cmd);
                Zx_Result = true;

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Updatemyzh_bz 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }
        public Boolean Updatefz_bz(int id, string fz_bz)
        {
            string sqlstr = "update exam_tjyz set fz_bz=@fz_bz where  id=@id";
            Boolean Zx_Result = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@myzh_bz", DbType.String, fz_bz);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.Int32, id);
                DBProcess._db.ExecuteNonQuery(cmd);
                Zx_Result = true;

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Updatefz_bz 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }
        //更新特检医嘱状态
        public Boolean UpdatetjyzStatus(int id, string zx_doc_name)
        {
            string sqlstr = "update exam_tjyz set zx_datetime=@zx_datetime,zx_doc_name=@zx_doc_name,yz_flag=@yz_flag where id=@id";
            Boolean Zx_Result = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@zx_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                DBProcess._db.AddInParameter(cmd, "@zx_doc_name", DbType.String, zx_doc_name);
                DBProcess._db.AddInParameter(cmd, "@yz_flag", DbType.String, "已执行");
                DBProcess._db.AddInParameter(cmd, "@id", DbType.String, id);
                DBProcess._db.ExecuteNonQuery(cmd);

                Zx_Result = true;

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdatetjyzStatus 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }

        //更新特检医嘱条码
        public Boolean UpdatetjyzBarcode(int id, string barcode)
        {
            string sqlstr = "update exam_tjyz set barcode=@barcode where id=@id";
            Boolean Zx_Result = false;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String, barcode);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.String, id);
                DBProcess._db.ExecuteNonQuery(cmd);
                Zx_Result = true;

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdatetjyzBarcode 执行语句异常：" + sqlstr);
            }
            return Zx_Result;
        }
        //
        public List<string> GetTj_Results()
        {
            List<string> lst = new List<string>();
            string sqlstr = "select  '' as result union  all (select result from exam_tj_result order by order_index asc) ";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        lst.Add(ds.Tables[0].Rows[i]["result"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetTj_Results 执行语句：" + sqlstr);
            }
            return lst;
        }
        //获取特检医嘱切片号
        public string GetBarcode(string study_no, string pre_char)
        {
            string ResultStr = "";
            string sqlstr = "select max(barcode) as barcode from  exam_tjyz where study_no=@study_no";
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
                DBProcess.ShowException(ex, "GetBarcode 执行语句异常：" + sqlstr);
                ResultStr = "";
            }
            return ResultStr;
        }
    }
   
}
