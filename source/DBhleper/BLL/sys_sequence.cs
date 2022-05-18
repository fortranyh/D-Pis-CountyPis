using System;
using System.Data;
using System.Data.Common;
namespace DBHelper.BLL
{
    //序号发生器
    public class sys_sequence
    {
        //当前流水号列表
        public DataSet GetDsSequence()
        {
            DataSet ds = new DataSet();
            string sqlstr = "select * from sys_sequence where show_flag=1";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                cmd.Connection = DBProcess._db.CreateConnection();
                DbDataAdapter da = DBProcess._db.GetDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsSequence 执行语句：" + sqlstr);
                ds = null;
            }
            return ds;
        }
        public Boolean UpdateDsSequence(DataTable data)
        {
            Boolean result = false;
            try
            {
                DataTable changes = data.GetChanges();
                if (changes != null)
                {
                    for (int i = 0; i < changes.Rows.Count; i++)
                    {
                        string sqlstr = "select `pissetval`('" + changes.Rows[i]["name"].ToString() + "'," + changes.Rows[i]["CURRENT_VALUE"].ToString() + ")";
                        DBProcess._db.ExecuteScalar(CommandType.Text, sqlstr).ToString();
                    }
                    data.AcceptChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateDsSequence 执行出错！");
            }
            return result;
        }


        //新建病人PID生成
        public string GetPID_Sequence()
        {
            string result = string.Empty;
            string sqlstr = "select `pisnextval`('PID')";
            try
            {
                result = DBProcess._db.ExecuteScalar(CommandType.Text, sqlstr).ToString();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetPID_Sequence 执行语句：" + sqlstr);
                result = string.Empty;
            }
            return result;
        }
        //新建病人申请单号码生成
        public string GetSQD_Sequence()
        {
            string result = string.Empty;
            string sqlstr = "select `pisnextval`('SQD')";
            try
            {
                result = DBProcess._db.ExecuteScalar(CommandType.Text, sqlstr).ToString();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetSQD_Sequence 执行语句：" + sqlstr);
                result = string.Empty;
            }
            return result;
        }
        //查询手动输入的病理号是否可用
        public int CheckBLH_Enable(string Cur_BLH, string big_type)
        {
            int result = 0;
            string sqlstr = "select count(*) as sl from exam_master where exam_status>1 and study_no='" + Cur_BLH + "' and exam_type='" + big_type + "'";
            try
            {
                result = Convert.ToInt32(DBProcess._db.ExecuteScalar(CommandType.Text, sqlstr));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetSQD_Sequence 执行语句：" + sqlstr);
                result = 0;
            }
            return result;
        }
        //生成病理号
        public string Create_BLH(string modality, string exam_type, int inout_type)
        {
            string result = "";
            string sqlstr = "select sequence_name,pre_char from exam_type_dict where exam_flag='" + inout_type + "' and big_type='" + exam_type + "' and modality='" + modality + "'";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count == 1)
                {
                    string pre_char = ds.Tables[0].Rows[0]["pre_char"].ToString() ?? "";
                    string sequence_name = ds.Tables[0].Rows[0]["sequence_name"].ToString() ?? "";
                    sqlstr = "select `pisnextval`('" + sequence_name + "')";
                    result = pre_char + DBProcess._db.ExecuteScalar(CommandType.Text, sqlstr).ToString();
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Create_BLH 执行语句：" + sqlstr);
                result = "";
            }
            return result;
        }
        //查询是否冰冻单独编号
        public Boolean Query_BdSet()
        {
            Boolean result = false;
            string sqlstr = "select pre_char,sequence_name from bingdong_setinfo where cur_enable=1";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count == 1)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Query_BdSet 执行语句：" + sqlstr);
                result = false;
            }
            return result;
        }


        //生成冰冻号
        public string Create_BDBLH()
        {
            string result = "";
            string sqlstr = "select pre_char,sequence_name from bingdong_setinfo where cur_enable=1";
            try
            {
                DataSet ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr);
                if (ds != null && ds.Tables[0].Rows.Count == 1)
                {
                    string pre_char = ds.Tables[0].Rows[0]["pre_char"].ToString() ?? "";
                    string sequence_name = ds.Tables[0].Rows[0]["sequence_name"].ToString() ?? "";
                    sqlstr = "select `pisnextval`('" + sequence_name + "')";
                    result = pre_char + DBProcess._db.ExecuteScalar(CommandType.Text, sqlstr).ToString();
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Create_BDBLH 执行语句：" + sqlstr);
                result = "";
            }
            return result;
        }

        //医嘱号生成
        public string GetYZID_Sequence()
        {
            string result = string.Empty;
            string sqlstr = "select `pisnextval`('YZ')";
            try
            {
                result = DBProcess._db.ExecuteScalar(CommandType.Text, sqlstr).ToString();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetYZID_Sequence 执行语句：" + sqlstr);
                result = string.Empty;
            }
            return result;
        }
        //当前病理号
        public string GetBLH_Sequence(string exam_type)
        {
            string result = string.Empty;
            string sqlstr = "select `piscurrval`('" + exam_type + "')";
            try
            {
                result = DBProcess._db.ExecuteScalar(CommandType.Text, sqlstr).ToString();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetBLH_Sequence 执行语句：" + sqlstr);
                result = string.Empty;
            }
            return result;
        }
        //设置病理号
        public string SetBLH_Sequence(string exam_type, string BLH)
        {
            string curBlh = "0";
            if (CheckBLH_Enable(BLH, exam_type) > 0)
            {
                return "-1";
            }
            string sqlstr = "select `pissetval`('" + exam_type + "'," + BLH + ")";
            try
            {
                curBlh = DBProcess._db.ExecuteScalar(CommandType.Text, sqlstr).ToString();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "SetBLH_Sequence 执行语句：" + sqlstr);
                curBlh = "0";
            }
            return curBlh;
        }


    }
}
