using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DBhleper.BLL
{
   public  class exam_jsyz
    {
        //数据
        public DataTable GetData(string study_no)
        {
            string sqlstr = "select id,exam_no,study_no,lk_no,work_source,parts,group_num,sq_doctor_name,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') as sq_datetime,yz_flag,zx_doc_name,date_format(zx_datetime,'%Y-%m-%d %H:%i:%s') as zx_datetime,barcode,memo_note,zc_flag,qc_id from exam_jsyz where study_no=@study_no";
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
        public Boolean DelData(int id)
        {
            string sqlstr = "delete  from exam_jsyz where id=@id";
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
       public Boolean InsertData(string exam_no, string study_no, string lk_no, string work_source, string parts, int group_num, string sq_doctor_name, string sq_datetime, string yz_flag, string barcode, string memo_note, int zc_flag, int qc_id)
       {
           Boolean Zx_Result = false;
           string sqlstr = "insert into exam_jsyz(exam_no,study_no,lk_no,work_source,parts,group_num,sq_doctor_name,sq_datetime,yz_flag,barcode,memo_note,zc_flag,qc_id) values(@exam_no,@study_no,@lk_no,@work_source,@parts,@group_num,@sq_doctor_name,@sq_datetime,@yz_flag,@barcode,@memo_note,@zc_flag,@qc_id)";
           try
           {
               //插入
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               DBProcess._db.AddInParameter(cmd, "@lk_no", DbType.String, lk_no);
               DBProcess._db.AddInParameter(cmd, "@work_source", DbType.String, work_source);
               DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, parts);
               DBProcess._db.AddInParameter(cmd, "@group_num", DbType.Int32, group_num);
               DBProcess._db.AddInParameter(cmd, "@sq_doctor_name", DbType.String, sq_doctor_name);
               DBProcess._db.AddInParameter(cmd, "@sq_datetime", DbType.DateTime, sq_datetime);
               DBProcess._db.AddInParameter(cmd, "@yz_flag", DbType.String, yz_flag);
               DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String, barcode);
               DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
               DBProcess._db.AddInParameter(cmd, "@zc_flag", DbType.Int16, zc_flag);
               DBProcess._db.AddInParameter(cmd, "@qc_id", DbType.Int16, qc_id);
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
           string sqlstr = "update exam_jsyz set zc_flag=0 where zc_flag=1 and study_no=@study_no";
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

       //取材管理展示未执行技术医嘱
       public DataTable GetQcJsyzData()
       {
           DateTime dtime = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
           string sqlstr = "select id,exam_no,study_no,lk_no,work_source,parts,group_num,sq_doctor_name,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') as sq_datetime,yz_flag,zx_doc_name,date_format(zx_datetime,'%Y-%m-%d %H:%i:%s') as zx_datetime,barcode,memo_note,zc_flag,qc_id  from exam_jsyz where zc_flag=0 and yz_flag='申请' and  work_source='补取' and sq_datetime >='" + dtime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
           DataTable dt = null;
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetQcJsyzData 执行语句异常：" + sqlstr);
           }
           return dt;
       }
       //取材管理展示已执行技术医嘱
       public DataTable GetQcYzxJsyzData( string tj)
       {
           string sqlstr = "select id,exam_no,study_no,lk_no,work_source,parts,group_num,sq_doctor_name,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') as sq_datetime,yz_flag,zx_doc_name,date_format(zx_datetime,'%Y-%m-%d %H:%i:%s') as zx_datetime,barcode,memo_note,zc_flag,qc_id  from exam_jsyz where zc_flag=0 and yz_flag='已执行' and  work_source='补取' " + tj;
           DataTable dt = null;
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetQcYzxJsyzData 执行语句异常：" + sqlstr);
           }
           return dt;
       }


       //切片管理展示未执行技术医嘱
       public DataTable GetQpJsyzData()
       {
           string sqlstr = "select id,exam_no,study_no,lk_no,work_source,parts,group_num,sq_doctor_name,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') as sq_datetime,yz_flag,zx_doc_name,date_format(zx_datetime,'%Y-%m-%d %H:%i:%s') as zx_datetime,barcode,memo_note,zc_flag,qc_id,0 as chk from exam_jsyz where zc_flag=0 and yz_flag='申请' and work_source<>'常规' and work_source<>'补取' ";
           DataTable dt = null;
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetQpJsyzData 执行语句异常：" + sqlstr);
           }
           return dt;
       }
       //切片管理展示已经执行技术医嘱
       public DataTable GetQpYzxJsyzData(string tj)
       {
           string sqlstr = "select id,exam_no,study_no,lk_no,work_source,parts,group_num,sq_doctor_name,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') as sq_datetime,yz_flag,zx_doc_name,date_format(zx_datetime,'%Y-%m-%d %H:%i:%s') as zx_datetime,barcode,memo_note,zc_flag,qc_id from exam_jsyz where zc_flag=0 and yz_flag='已执行' and work_source<>'常规' and work_source<>'补取' " + tj;
           DataTable dt = null;
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetQpYzxJsyzData 执行语句异常：" + sqlstr);
           }
           return dt;
       }
       //更新技术医嘱状态
       public Boolean UpdateJsyzStatus(int id, string zx_doc_name)
       {
           string sqlstr = "update exam_jsyz set zx_datetime=@zx_datetime,zx_doc_name=@zx_doc_name,yz_flag=@yz_flag where id=@id";
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
               DBProcess.ShowException(ex, "UpdateJsyzStatus 执行语句异常：" + sqlstr);
           }
           return Zx_Result;
       }

    }
}
