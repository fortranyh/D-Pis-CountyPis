using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    public class exam_bgzd_dict
    {

        public DataTable GetTreeExam_bgzd_dict(int type_dict)
        {
            string sqlstr = "select id,part_name,parent_code,order_no from  exam_lcfh_dict order by parent_code asc";
            if (type_dict == 1)
            {
                sqlstr = "select id,part_name,parent_code,order_no from exam_zdbmm_dict order by parent_code asc";
            }
            else if (type_dict == 2)
            {
                sqlstr = "select id,part_name,parent_code,order_no from exam_zdbms_dict order by parent_code asc";
            }
            else if (type_dict == 3)
            {
                sqlstr = "select id,part_name,parent_code,order_no from exam_zdpz_dict order by parent_code asc";
            }
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetTreeExam_bgzd_dict 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }

        public DataTable GetChildParts(int type_dict, string Pid)
        {
            string sqlstr = "select id from  exam_lcfh_dict where parent_code='" + Pid + "'";
            if (type_dict == 1)
            {
                sqlstr = "select id,part_name,parent_code,order_no from exam_zdbmm_dict where parent_code='" + Pid + "'";
            }
            else if (type_dict == 2)
            {
                sqlstr = "select id,part_name,parent_code,order_no from exam_zdbms_dict where parent_code='" + Pid + "'";
            }
            else if (type_dict == 3)
            {
                sqlstr = "select id,part_name,parent_code,order_no from exam_zdpz_dict where parent_code='" + Pid + "'";
            }
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetChildParts 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }

        public DataTable GetDsExam_bgzd_dict(int type_dict)
        {
            DataTable dt = null;
            string sqlstr = "select id,part_name,parent_code,order_no from  exam_lcfh_dict order by parent_code asc";
            if (type_dict == 1)
            {
                sqlstr = "select id,part_name,parent_code,order_no from exam_zdbmm_dict order by parent_code asc";
            }
            else if (type_dict == 2)
            {
                sqlstr = "select id,part_name,parent_code,order_no from exam_zdbms_dict order by parent_code asc";
            }
            else if (type_dict == 3)
            {
                sqlstr = "select id,part_name,parent_code,order_no from exam_zdpz_dict order by parent_code,order_no asc";
            }
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsExam_bgzd_dict 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
        public string GetExam_bgzd_Name(int type_dict, string id)
        {
            string result = "";
            string sqlstr = "select part_name from  exam_lcfh_dict where id='" + id + "'";
            if (type_dict == 1)
            {
                sqlstr = "select part_name from exam_zdbmm_dict where id='" + id + "'";
            }
            else if (type_dict == 2)
            {
                sqlstr = "select part_name from exam_zdbms_dict  where id='" + id + "'";
            }
            else if (type_dict == 3)
            {
                sqlstr = "select part_name from exam_zdpz_dict  where id='" + id + "'";
            }
            try
            {
                DataTable dt = DBProcess._db.ExecuteDataSet(sqlstr).Tables[0];
                if (dt != null && dt.Rows.Count == 1)
                {
                    result = dt.Rows[0]["part_name"].ToString();
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetExam_bgzd_Name 执行语句：" + sqlstr);
            }
            return result;
        }
        //插入
        public Boolean InsertParts(int type_dict, string id, string part_name, string parent_code)
        {
            Boolean zxResult = false;
            try
            {
                //插入
                string sqlstr = "insert into exam_lcfh_dict(id,part_name,parent_code) values(@id,@part_name,@parent_code)";
                if (type_dict == 1)
                {
                    sqlstr = "  insert into exam_zdbmm_dict(id,part_name,parent_code) values(@id,@part_name,@parent_code)";
                }
                else if (type_dict == 2)
                {
                    sqlstr = " insert into exam_zdbms_dict(id,part_name,parent_code) values(@id,@part_name,@parent_code)";
                }
                else if (type_dict == 3)
                {
                    sqlstr = " insert into exam_zdpz_dict(id,part_name,parent_code) values(@id,@part_name,@parent_code)";
                }
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.String, id);
                DBProcess._db.AddInParameter(cmd, "@part_name", DbType.String, part_name);
                DBProcess._db.AddInParameter(cmd, "@parent_code", DbType.String, parent_code);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    zxResult = true;
                }
                else
                {
                    zxResult = false;
                }
            }
            catch
            {

            }

            return zxResult;
        }

        //删除
        public Boolean DelParts(int type_dict, string id)
        {
            Boolean zxResult = false;
            string sqlstr = "delete from  exam_lcfh_dict where  id='" + id + "'";
            if (type_dict == 1)
            {
                sqlstr = "delete from  exam_zdbmm_dict where  id='" + id + "'";
            }
            else if (type_dict == 2)
            {
                sqlstr = "delete from  exam_zdbms_dict where  id='" + id + "'";
            }
            else if (type_dict == 3)
            {
                sqlstr = "delete from  exam_zdpz_dict where  id='" + id + "'";
            }
            try
            {
                DBProcess._db.ExecuteNonQuery(CommandType.Text, sqlstr);
                return true;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "DelParts 执行语句：" + sqlstr);

            }
            return zxResult;
        }
        //更新
        public int updatePartText(int type_dict, string part_name, string id)
        {
            string sqlstr = "update exam_lcfh_dict set part_name=@part_name where  id=@id";
            if (type_dict == 1)
            {
                sqlstr = "update exam_zdbmm_dict set part_name=@part_name where  id=@id";
            }
            else if (type_dict == 2)
            {
                sqlstr = "update exam_zdbms_dict set part_name=@part_name where  id=@id";
            }
            else if (type_dict == 3)
            {
                sqlstr = "update exam_zdpz_dict set part_name=@part_name where  id=@id";
            }
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@part_name", DbType.String, part_name);
                DBProcess._db.AddInParameter(cmd, "@id", DbType.String, id);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    return 1;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "updatePartText 执行语句异常：" + sqlstr);
            }
            return 0;
        }



    }
}
