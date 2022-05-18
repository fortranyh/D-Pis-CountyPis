using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
namespace DBhleper.BLL
{
   public class exam_yz
    {
       public DataTable GetYzinfo(string study_no,string work_source)
       {

           string sqlstr = "(select study_no,lk_no,work_source,1 as yzlx  from exam_tjyz where zc_flag=0 and yz_flag='申请' group by study_no,lk_no,work_source,yzlx) union  all (select study_no,lk_no,work_source,0 as yzlx from exam_jsyz where zc_flag=0 and yz_flag='申请' and  work_source<>'补取' group by study_no,lk_no,work_source,yzlx) order by study_no asc";

           string tj = "";
           if (!study_no.Equals(""))
           {
               tj = " and study_no='" + study_no + "'";
           }
           if (work_source.Equals("全部"))
           {
               sqlstr = "(select study_no,lk_no,work_source,1 as yzlx  from exam_tjyz where zc_flag=0 and yz_flag='申请' " +tj+ " group by study_no,lk_no,work_source,yzlx) union  all (select study_no,lk_no,work_source,0 as yzlx from exam_jsyz where zc_flag=0 and yz_flag='申请' and  work_source<>'补取' "+tj+" group by study_no,lk_no,work_source,yzlx) order by study_no asc";
           }
           else
           {
               if (work_source.Equals("免疫组化") || work_source.Equals("分子病理") || work_source.Equals("特殊染色") || work_source.Equals("电镜"))
               {
                   tj += " and taocan_type='" + work_source + "'";
                   sqlstr = "(select study_no,lk_no,work_source,1 as yzlx  from exam_tjyz where zc_flag=0 and yz_flag='申请' "+tj+" group by study_no,lk_no,work_source,yzlx) order by study_no asc";
               }
               else
               {
                   tj += " and work_source='" + work_source + "'";
                   sqlstr = "(select study_no,lk_no,work_source,0 as yzlx from exam_jsyz where zc_flag=0 and yz_flag='申请' and  work_source<>'补取' " + tj + " group by study_no,lk_no,work_source,yzlx) order by study_no asc";
               }
           }
           
           
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
               DBProcess.ShowException(ex, "GetYzinfo 执行语句异常：" + sqlstr);
           }
           return null;
       }

       public DataTable GetSpeYzinfo(string study_no,string lk_no,string work_source,string yzlx)
       {
           string sqlstr = "";
           try
           {
               
                if (yzlx.Equals("0"))
                {
                    sqlstr = "select id,study_no,lk_no,work_source,bj_name,group_num,sq_doctor_name,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,qc_id,memo_note,0 as yzlx from exam_jsyz where zc_flag=0 and yz_flag='申请' and work_source<>'补取' and study_no=@study_no and lk_no=@lk_no and work_source=@work_source";
                }
                else if (yzlx.Equals("1"))
                {
                    sqlstr = "select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note, 1 as yzlx from exam_tjyz where zc_flag=0 and yz_flag='申请' and study_no=@study_no and lk_no=@lk_no and work_source=@work_source";
                }
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
               DBProcess._db.AddInParameter(cmd, "@lk_no", DbType.String, lk_no);
               DBProcess._db.AddInParameter(cmd, "@work_source", DbType.String, work_source);
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return ds.Tables[0];
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "GetSpeYzinfo 执行语句异常：" + sqlstr);
           }
           return null;
       }
       //查询特检管理中的技术医嘱
       public DataTable GetjsYzinfo(string study_no)
       {

           string tj = "";
           if (!study_no.Equals(""))
           {
               tj = " and study_no='" + study_no + "'";
           }
           string sqlstr = "(select study_no,lk_no,work_source,0 as yzlx from exam_jsyz where zc_flag=0 and yz_flag='申请' and  work_source<>'补取' " + tj + " group by study_no,lk_no,work_source,yzlx) order by study_no asc";
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
               DBProcess.ShowException(ex, "GetjsYzinfo 执行语句异常：" + sqlstr);
           }
           return null;
       }

       public DataTable QueryjsYzinfo(string study_no)
       {
           string sqlstr = "";
           try
           {
               string tj = "";
               sqlstr = "(select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note,0 as yzlx from exam_jsyz where zc_flag=0 and yz_flag='申请'  and work_source<>'补取') union  all (select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note, 1 as yzlx from exam_tjyz where zc_flag=0 and yz_flag='申请') order by study_no asc";
               if (!study_no.Equals(""))
               {
                   tj = " and study_no='" + study_no + "'";
               }
               sqlstr = "select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note,0 as yzlx from exam_jsyz where zc_flag=0 and yz_flag='申请'  and work_source<>'补取' " + tj + " order by study_no asc";
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return ds.Tables[0];
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "QueryYzinfo 执行语句异常：" + sqlstr);
           }
           return null;
       }

       //查询特检管理中的特检医嘱
       public DataTable GettjYzinfo(string study_no)
       {

           string tj = "";
           if (!study_no.Equals(""))
           {
               tj = " and study_no='" + study_no + "'";
           }
           tj += " and (taocan_type='免疫组化' or taocan_type='分子病理' or taocan_type='特殊染色' or taocan_type='电镜') ";
           string sqlstr = "(select study_no,lk_no,work_source,1 as yzlx  from exam_tjyz where zc_flag=0 and yz_flag='申请' " + tj + " group by study_no,lk_no,work_source,yzlx) order by study_no asc";

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
               DBProcess.ShowException(ex, "GettjYzinfo 执行语句异常：" + sqlstr);
           }
           return null;
       }

       public DataTable QuerytjYzinfo(string study_no)
       {
           string sqlstr = "";
           try
           {
               string tj = "";
               sqlstr = "(select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note,0 as yzlx from exam_jsyz where zc_flag=0 and yz_flag='申请'  and work_source<>'补取') union  all (select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note, 1 as yzlx from exam_tjyz where zc_flag=0 and yz_flag='申请') order by study_no asc";
               if (!study_no.Equals(""))
               {
                   tj = " and study_no='" + study_no + "'";
               }
               
               tj += " and (taocan_type='免疫组化' or taocan_type='分子病理' or taocan_type='特殊染色' or taocan_type='电镜') ";
               sqlstr = "select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note, 1 as yzlx from exam_tjyz where zc_flag=0 and yz_flag='申请' " + tj + " order by study_no asc";
               

               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return ds.Tables[0];
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "Querytjinfo 执行语句异常：" + sqlstr);
           }
           return null;
       }



       public DataTable QueryYzinfo(string study_no,string work_source)
       {
            string sqlstr = "";
           try
           {
               string tj = "";
               sqlstr = "(select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note,0 as yzlx from exam_jsyz where zc_flag=0 and yz_flag='申请'  and work_source<>'补取') union  all (select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note, 1 as yzlx from exam_tjyz where zc_flag=0 and yz_flag='申请') order by study_no asc";

               if (!study_no.Equals(""))
               {
                   tj = " and study_no='" + study_no + "'";
               }
               if (work_source.Equals("全部"))
               {
                   sqlstr = "(select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note,0 as yzlx from exam_jsyz where zc_flag=0 and yz_flag='申请'  and work_source<>'补取' " + tj + ") union  all (select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note, 1 as yzlx from exam_tjyz where zc_flag=0 and yz_flag='申请' " + tj + ") order by study_no asc";
               }
               else
               {
                   if (work_source.Equals("免疫组化") || work_source.Equals("分子病理") || work_source.Equals("特殊染色") || work_source.Equals("电镜"))
                   {
                       tj += " and taocan_type='" + work_source + "'";
                       sqlstr = "select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note, 1 as yzlx from exam_tjyz where zc_flag=0 and yz_flag='申请' " + tj + " order by study_no asc";
                   }
                   else
                   {
                       tj += " and work_source='" + work_source + "'";
                       sqlstr = "select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note,0 as yzlx from exam_jsyz where zc_flag=0 and yz_flag='申请'  and work_source<>'补取' " + tj + " order by study_no asc";
                   }
               }
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return ds.Tables[0];
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "QueryYzinfo 执行语句异常：" + sqlstr);
           }
           return null;
       }

       public DataTable QueryYzxYzinfo(string study_no, string dtstart,string dtend)
       {
           string sqlstr = "";
           try
           {
               StringBuilder sb = new StringBuilder();
               if (study_no.Equals(""))
               {
                   sb.AppendFormat(" and zx_datetime>='{0} 00:00:00' and zx_datetime<='{1} 23:59:59'", dtstart, dtend);
               }
               else
               {
                   sb.AppendFormat(" and study_no='{0}'", study_no);
               }
               string tj = sb.ToString();
               sqlstr = "(select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note,0 as yzlx,zx_doc_name,date_format(zx_datetime,'%Y-%m-%d %H:%i:%s') AS zx_datetime,barcode from exam_jsyz where zc_flag=0 and yz_flag='已执行'  and work_source<>'补取' " + tj + ") union  all (select id,study_no,lk_no,work_source,bj_name,group_num,date_format(sq_datetime,'%Y-%m-%d %H:%i:%s') AS sq_datetime,sq_doctor_name,qc_id,memo_note, 1 as yzlx,zx_doc_name,date_format(zx_datetime,'%Y-%m-%d %H:%i:%s') AS zx_datetime,barcode from exam_tjyz where zc_flag=0 and yz_flag='已执行' " + tj + ") order by study_no asc";

               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
               if (ds != null && ds.Tables[0].Rows.Count > 0)
               {
                   return ds.Tables[0];
               }
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "QueryYzxYzinfo 执行语句异常：" + sqlstr);
           }
           return null;
       }

       //执行科内医嘱
       public Boolean UpdateyzStatus(int id, string zx_doc_name,string barcode,string yzlx)
       {
           string sqlstr = "";
           if (yzlx.Equals("1"))
           {
               sqlstr = "update exam_tjyz set zx_datetime=@zx_datetime,zx_doc_name=@zx_doc_name,yz_flag=@yz_flag,barcode=@barcode where id=@id"; 
           }
           else
           {
               sqlstr = "update exam_jsyz set zx_datetime=@zx_datetime,zx_doc_name=@zx_doc_name,yz_flag=@yz_flag,barcode=@barcode where id=@id";
           }
           Boolean Zx_Result = false;
           try
           {
               DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
               DBProcess._db.AddInParameter(cmd, "@zx_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
               DBProcess._db.AddInParameter(cmd, "@zx_doc_name", DbType.String, zx_doc_name);
               DBProcess._db.AddInParameter(cmd, "@yz_flag", DbType.String, "已执行");
               DBProcess._db.AddInParameter(cmd, "@barcode", DbType.String, barcode);
               DBProcess._db.AddInParameter(cmd, "@id", DbType.String, id);
               DBProcess._db.ExecuteNonQuery(cmd);
               Zx_Result = true;
           }
           catch (Exception ex)
           {
               DBProcess.ShowException(ex, "UpdateyzStatus 执行语句异常：" + sqlstr);
           }
           return Zx_Result;
       }
    }
}
