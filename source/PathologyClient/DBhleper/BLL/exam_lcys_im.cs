using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DBhleper.BLL
{
   public  class exam_lcys_im
    {
        public Boolean SaveLcgt(Model.exam_lcgt_model insM)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_lcys_im where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, insM.study_no);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 0)
                {
                    //插入
                    sqlstr = "insert into exam_lcys_im(study_no,im_time,im_lc_dept,im_lc_doc,im_bl_doc,im_info) values(@study_no,@im_time,@im_lc_dept,@im_lc_doc,@im_bl_doc,@im_info)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, insM.study_no);
                    DBProcess._db.AddInParameter(cmd, "@im_time", DbType.DateTime, insM.im_time);
                    DBProcess._db.AddInParameter(cmd, "@im_lc_dept", DbType.String, insM.im_lc_dept);
                    DBProcess._db.AddInParameter(cmd, "@im_lc_doc", DbType.String, insM.im_lc_doc);
                    DBProcess._db.AddInParameter(cmd, "@im_bl_doc", DbType.String, insM.im_bl_doc);
                    DBProcess._db.AddInParameter(cmd, "@im_info", DbType.String, insM.im_info);
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

                    //更新
                    sqlstr = "update exam_lcys_im set  im_time=@im_time,im_lc_dept=@im_lc_dept,im_lc_doc=@im_lc_doc,im_bl_doc=@im_bl_doc,im_info=@im_info where study_no=@study_no";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@im_time", DbType.DateTime, insM.im_time);
                    DBProcess._db.AddInParameter(cmd, "@im_lc_dept", DbType.String, insM.im_lc_dept);
                    DBProcess._db.AddInParameter(cmd, "@im_lc_doc", DbType.String, insM.im_lc_doc);
                    DBProcess._db.AddInParameter(cmd, "@im_bl_doc", DbType.String, insM.im_bl_doc);
                    DBProcess._db.AddInParameter(cmd, "@im_info", DbType.String, insM.im_info);
                    DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, insM.study_no);
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
                DBProcess.ShowException(ex, "SaveLcgt 执行语句异常：" + sqlstr);
                Zx_Result = false;
            }
            return Zx_Result;
        }

        public DataTable GetData(string study_no)
        {

            string sqlstr = "select study_no,im_lc_dept,im_lc_doc,im_bl_doc,im_info,date_format(im_time,'%Y-%m-%d %H:%i:%s') as im_time  from exam_lcys_im where study_no=@study_no";
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
        //获取临床沟通列表
        public DataSet GetlcgtList(string tj)
        {
            string sqlstr = "select id,study_no,date_format(im_time,'%Y-%m-%d %H:%i:%s') as im_time,im_lc_dept,im_lc_doc,im_bl_doc,im_info from exam_lcys_im where 1=1  " + tj;
            DataSet ds = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                ds = DBProcess._db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetlcgtList 执行语句异常：" + sqlstr);
            }
            return ds;

        }

    }
}
