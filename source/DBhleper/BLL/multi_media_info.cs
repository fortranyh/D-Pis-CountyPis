using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    public class multi_media_info
    {
        //插入多媒体数据
        public int InsertData(string study_no, string path, string filename, int media_type)
        {
            int Result = 0;
            if (media_type == 7 || media_type == 11)
            {
                string sqlstr1 = "select count(*) as sl from multi_media_info where study_no=@study_no and media_type=@media_type";
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr1);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@media_type", DbType.Int16, media_type);
                Result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (Result > 0)
                {
                    sqlstr1 = "update multi_media_info set del_flag=1 where study_no=@study_no and media_type=@media_type and del_flag=0";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr1);
                    DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                    DBProcess._db.AddInParameter(cmd, "@media_type", DbType.Int16, media_type);
                    Result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                    cmd.Parameters.Clear();
                }
            }
            string sqlstr = "insert multi_media_info(study_no,media_type,path,filename)values(@study_no,@media_type,@path,@filename)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@path", DbType.String, path);
                DBProcess._db.AddInParameter(cmd, "@filename", DbType.String, filename);
                DBProcess._db.AddInParameter(cmd, "@media_type", DbType.Int16, media_type);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "InsertData 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
        //查询未删除多媒体数据
        public DataTable GetData(string study_no, int media_type)
        {
            string sqlstr = "select path,filename from multi_media_info where  del_flag=0  and study_no=@study_no and media_type=@media_type  order by id asc";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@media_type", DbType.Int16, media_type);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetData 执行语句：" + sqlstr);
            }
            return null;
        }
        //查询全部多媒体数据
        public DataTable GetAllData(string study_no, int media_type)
        {
            string sqlstr = "select path,filename from multi_media_info where (del_flag=0 or del_flag=1) and study_no=@study_no and media_type=@media_type  order by id asc";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@media_type", DbType.Int16, media_type);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetAllData 执行语句：" + sqlstr);
            }
            return null;
        }
        //删除多媒体数据
        public int DelData(string study_no, string filename, int media_type)
        {
            int Result = 0;
            string sqlstr = "update multi_media_info set del_flag=1 where study_no=@study_no and media_type=@media_type and filename=@filename";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@filename", DbType.String, filename);
                DBProcess._db.AddInParameter(cmd, "@media_type", DbType.Int16, media_type);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "DelData 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
        //找回删除的图像
        public int ZhDeledData(string study_no, string filename, int media_type)
        {
            int Result = 0;
            string sqlstr = "update multi_media_info set del_flag=0 where study_no=@study_no and media_type=@media_type and filename=@filename";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@filename", DbType.String, filename);
                DBProcess._db.AddInParameter(cmd, "@media_type", DbType.Int16, media_type);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "ZhDeledData 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
        //修正图像
        public int XzData(string study_no, string filename, int media_type, string Curstudy_no)
        {
            int Result = 0;
            string sqlstr = "update multi_media_info set study_no=@Curstudy_no,path=@path,del_flag=0 where study_no=@study_no and filename=@filename and media_type=@media_type";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@Curstudy_no", DbType.String, Curstudy_no);
                DBProcess._db.AddInParameter(cmd, "@path", DbType.String, Curstudy_no + @"/" + filename);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@filename", DbType.String, filename);
                DBProcess._db.AddInParameter(cmd, "@media_type", DbType.Int16, media_type);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "XzData 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        //更新多媒体备注
        public int UpdateMemo_note(string study_no, string filename, int media_type, string memo_note)
        {
            int Result = 0;
            string sqlstr = "update multi_media_info set memo_note=@memo_note where study_no=@study_no and filename=@filename and media_type=@media_type";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@memo_note", DbType.String, memo_note);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@filename", DbType.String, filename);
                DBProcess._db.AddInParameter(cmd, "@media_type", DbType.Int16, media_type);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateMemo_note 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
        //取备注
        public string GetMemo_note(string study_no, string filename, int media_type)
        {
            string Result = "";
            string sqlstr = "select memo_note from  multi_media_info  where study_no=@study_no and filename=@filename and media_type=@media_type";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@filename", DbType.String, filename);
                DBProcess._db.AddInParameter(cmd, "@media_type", DbType.Int16, media_type);
                Result = DBProcess._db.ExecuteScalar(cmd).ToString();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetMemo_note 执行语句：" + sqlstr);
                Result = "";
            }
            return Result;
        }


    }
}
