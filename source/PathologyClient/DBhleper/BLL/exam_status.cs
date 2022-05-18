using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
namespace DBHelper.BLL
{
    public class exam_status
    {
        public DataTable GetExamStatusDic()
        {
            DataTable dt = null;
            string sqlstr = "select status_code,status_name,status_color from exam_status_dict where enable_show=1 and status_code>=10 and status_code<60 order by order_index asc";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dt = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetExamStatusDic 执行语句：" + sqlstr);
            }
            return dt;
        }
        public List<Model.exam_status> GetModelExam_Status()
        {
            List<Model.exam_status> lst = new List<Model.exam_status>();
            string sqlstr = " select '-9' as status_code,'全部' as status_name,'-65536' as status_color union  all  ( select status_code,status_name,status_color from exam_status_dict where enable_show=1 order by order_index asc)";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Model.exam_status exam_status_ins = new Model.exam_status();
                        exam_status_ins.status_code = ds.Tables[0].Rows[i]["status_code"].ToString();
                        exam_status_ins.status_name = ds.Tables[0].Rows[i]["status_name"].ToString();
                        exam_status_ins.status_color = ds.Tables[0].Rows[i]["status_color"].ToString();
                        lst.Add(exam_status_ins);
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetModelExam_Status 执行语句：" + sqlstr);
            }
            return lst;
        }

        public List<Model.exam_status> GetDJModelExam_Status()
        {
            List<Model.exam_status> lst = new List<Model.exam_status>();
            string sqlstr = " select '-9' as status_code,'全部' as status_name,'-65536' as status_color union  all  ( select status_code,status_name,status_color from exam_status_dict where enable_show=1 and status_code>=10 and status_code<=20 order by order_index asc)";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Model.exam_status exam_status_ins = new Model.exam_status();
                        exam_status_ins.status_code = ds.Tables[0].Rows[i]["status_code"].ToString();
                        exam_status_ins.status_name = ds.Tables[0].Rows[i]["status_name"].ToString();
                        exam_status_ins.status_color = ds.Tables[0].Rows[i]["status_color"].ToString();
                        lst.Add(exam_status_ins);
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDJModelExam_Status 执行语句：" + sqlstr);
            }
            return lst;
        }


        public List<Model.exam_status> GetQCModelExam_Status()
        {
            List<Model.exam_status> lst = new List<Model.exam_status>();
            string sqlstr = " select '-9' as status_code,'全部' as status_name,'-65536' as status_color union  all  ( select status_code,status_name,status_color from exam_status_dict where enable_show=1 and status_code>=20 and status_code<=25  order by order_index asc)";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Model.exam_status exam_status_ins = new Model.exam_status();
                        exam_status_ins.status_code = ds.Tables[0].Rows[i]["status_code"].ToString();
                        exam_status_ins.status_name = ds.Tables[0].Rows[i]["status_name"].ToString();
                        exam_status_ins.status_color = ds.Tables[0].Rows[i]["status_color"].ToString();
                        lst.Add(exam_status_ins);
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetQCModelExam_Status 执行语句：" + sqlstr);
            }
            return lst;
        }



        public List<Model.exam_status> GetZPModelExam_Status()
        {
            List<Model.exam_status> lst = new List<Model.exam_status>();
            string sqlstr = " select '-9' as status_code,'全部' as status_name,'-65536' as status_color union  all  ( select status_code,status_name,status_color from exam_status_dict where enable_show=1 and status_code>=25 and status_code<=30 order by order_index asc)";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Model.exam_status exam_status_ins = new Model.exam_status();
                        exam_status_ins.status_code = ds.Tables[0].Rows[i]["status_code"].ToString();
                        exam_status_ins.status_name = ds.Tables[0].Rows[i]["status_name"].ToString();
                        exam_status_ins.status_color = ds.Tables[0].Rows[i]["status_color"].ToString();
                        lst.Add(exam_status_ins);
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetZPModelExam_Status 执行语句：" + sqlstr);
            }
            return lst;
        }

        public List<Model.exam_status> GetModelBGExam_Status()
        {
            List<Model.exam_status> lst = new List<Model.exam_status>();
            string sqlstr = " select '-9' as status_code,'全部' as status_name,'-65536' as status_color union  all  ( select status_code,status_name,status_color from exam_status_dict where enable_show=1 and status_code>=20 and status_code<60 order by order_index asc)";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        Model.exam_status exam_status_ins = new Model.exam_status();
                        exam_status_ins.status_code = ds.Tables[0].Rows[i]["status_code"].ToString();
                        exam_status_ins.status_name = ds.Tables[0].Rows[i]["status_name"].ToString();
                        exam_status_ins.status_color = ds.Tables[0].Rows[i]["status_color"].ToString();
                        lst.Add(exam_status_ins);
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetModelBGExam_Status 执行语句：" + sqlstr);
            }
            return lst;
        }




        public DataSet GetDsExam_Status()
        {
            DataSet ds = null;
            string sqlstr = "select status_code,status_name,status_color from exam_status_dict where enable_show=1 order by order_index asc";
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsExam_Status 执行语句：" + sqlstr);
            }
            return ds;
        }

        public DataTable GetDsExam_status()
        {
            DataTable dt = null;
            string sqlstr = "select status_code,status_name,status_color from exam_status_dict where enable_show=1 and status_code>1 order by status_code asc";
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsExam_Status 执行语句：" + sqlstr);
            }
            return dt;
        }

        //更新颜色
        public void UpdateColor(string status_code, string status_color)
        {
            string sqlstr = "update exam_status_dict set status_color=@status_color where  status_code=@status_code";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@status_color", DbType.String, status_color);
                DBProcess._db.AddInParameter(cmd, "@status_code", DbType.String, status_code);
                DBProcess._db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateColor 执行语句：" + sqlstr);
            }
        }

    }
}
