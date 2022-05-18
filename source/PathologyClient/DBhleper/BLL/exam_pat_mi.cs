using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    public class exam_pat_mi
    {
        public Boolean Process_Patmi(EntityModel.exam_pat_mi Patmi_Ins, ref string Str_Result)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_pat_mi where patient_id=@patient_id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, Patmi_Ins.patient_id);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 0)
                {
                    //插入
                    sqlstr = "insert into exam_pat_mi(patient_id,patient_name,name_phonetic,sex,date_of_birth,nation,identity,current_place,si_card,hospital_card,phone_number) values(@patient_id,@patient_name,@name_phonetic,@sex,@date_of_birth,@nation,@identity,@current_place,@si_card,@hospital_card,@phone_number)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, Patmi_Ins.patient_id);
                    DBProcess._db.AddInParameter(cmd, "@patient_name", DbType.String, Patmi_Ins.patient_name);
                    DBProcess._db.AddInParameter(cmd, "@name_phonetic", DbType.String, Patmi_Ins.name_phonetic);
                    DBProcess._db.AddInParameter(cmd, "@sex", DbType.String, Patmi_Ins.sex);
                    DBProcess._db.AddInParameter(cmd, "@date_of_birth", DbType.String, Patmi_Ins.date_of_birth);
                    DBProcess._db.AddInParameter(cmd, "@nation", DbType.String, Patmi_Ins.nation);
                    DBProcess._db.AddInParameter(cmd, "@identity", DbType.String, Patmi_Ins.identity);
                    DBProcess._db.AddInParameter(cmd, "@current_place", DbType.String, Patmi_Ins.current_place);
                    DBProcess._db.AddInParameter(cmd, "@si_card", DbType.String, Patmi_Ins.si_card);
                    DBProcess._db.AddInParameter(cmd, "@hospital_card", DbType.String, Patmi_Ins.hospital_card);
                    DBProcess._db.AddInParameter(cmd, "@phone_number", DbType.String, Patmi_Ins.phone_number);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "插入病人基本信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "插入病人基本信息失败！";
                    }
                }
                else if (sl == 1)
                {
                    //更新
                    sqlstr = "update exam_pat_mi set patient_name=@patient_name,name_phonetic=@name_phonetic,sex=@sex,date_of_birth=@date_of_birth,nation=@nation,identity=@identity,current_place=@current_place,si_card=@si_card,hospital_card=@hospital_card,phone_number=@phone_number where patient_id=@patient_id";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);

                    DBProcess._db.AddInParameter(cmd, "@patient_name", DbType.String, Patmi_Ins.patient_name);
                    DBProcess._db.AddInParameter(cmd, "@name_phonetic", DbType.String, Patmi_Ins.name_phonetic);
                    DBProcess._db.AddInParameter(cmd, "@sex", DbType.String, Patmi_Ins.sex);
                    DBProcess._db.AddInParameter(cmd, "@date_of_birth", DbType.String, Patmi_Ins.date_of_birth);
                    DBProcess._db.AddInParameter(cmd, "@nation", DbType.String, Patmi_Ins.nation);
                    DBProcess._db.AddInParameter(cmd, "@identity", DbType.String, Patmi_Ins.identity);
                    DBProcess._db.AddInParameter(cmd, "@current_place", DbType.String, Patmi_Ins.current_place);
                    DBProcess._db.AddInParameter(cmd, "@si_card", DbType.String, Patmi_Ins.si_card);
                    DBProcess._db.AddInParameter(cmd, "@hospital_card", DbType.String, Patmi_Ins.hospital_card);
                    DBProcess._db.AddInParameter(cmd, "@phone_number", DbType.String, Patmi_Ins.phone_number);
                    DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, Patmi_Ins.patient_id);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "更新病人基本信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "更新病人基本信息失败！";
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Process_Patmi 执行语句异常：" + sqlstr);
                Zx_Result = false;
                Str_Result = "处理病人基本信息异常：" + ex.ToString();
            }
            return Zx_Result;
        }

        public Boolean Process_Patmi(Model.exam_pat_mi Patmi_Ins, ref string Str_Result)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_pat_mi where patient_id=@patient_id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, Patmi_Ins.patient_id);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 0)
                {
                    //插入
                    sqlstr = "insert into exam_pat_mi(patient_id,patient_name,name_phonetic,sex,date_of_birth,nation,identity,current_place,si_card,hospital_card,phone_number) values(@patient_id,@patient_name,@name_phonetic,@sex,@date_of_birth,@nation,@identity,@current_place,@si_card,@hospital_card,@phone_number)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, Patmi_Ins.patient_id);
                    DBProcess._db.AddInParameter(cmd, "@patient_name", DbType.String, Patmi_Ins.patient_name);
                    DBProcess._db.AddInParameter(cmd, "@name_phonetic", DbType.String, Patmi_Ins.name_phonetic);
                    DBProcess._db.AddInParameter(cmd, "@sex", DbType.String, Patmi_Ins.sex);
                    DBProcess._db.AddInParameter(cmd, "@date_of_birth", DbType.String, Patmi_Ins.date_of_birth);
                    DBProcess._db.AddInParameter(cmd, "@nation", DbType.String, Patmi_Ins.nation);
                    DBProcess._db.AddInParameter(cmd, "@identity", DbType.String, Patmi_Ins.identity);
                    DBProcess._db.AddInParameter(cmd, "@current_place", DbType.String, Patmi_Ins.current_place);
                    DBProcess._db.AddInParameter(cmd, "@si_card", DbType.String, Patmi_Ins.si_card);
                    DBProcess._db.AddInParameter(cmd, "@hospital_card", DbType.String, Patmi_Ins.hospital_card);
                    DBProcess._db.AddInParameter(cmd, "@phone_number", DbType.String, Patmi_Ins.phone_number);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "插入病人基本信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "插入病人基本信息失败！";
                    }
                }
                else if (sl == 1)
                {
                    //更新
                    sqlstr = "update exam_pat_mi set patient_name=@patient_name,name_phonetic=@name_phonetic,sex=@sex,date_of_birth=@date_of_birth,nation=@nation,identity=@identity,current_place=@current_place,si_card=@si_card,hospital_card=@hospital_card,phone_number=@phone_number where patient_id=@patient_id";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);

                    DBProcess._db.AddInParameter(cmd, "@patient_name", DbType.String, Patmi_Ins.patient_name);
                    DBProcess._db.AddInParameter(cmd, "@name_phonetic", DbType.String, Patmi_Ins.name_phonetic);
                    DBProcess._db.AddInParameter(cmd, "@sex", DbType.String, Patmi_Ins.sex);
                    DBProcess._db.AddInParameter(cmd, "@date_of_birth", DbType.String, Patmi_Ins.date_of_birth);
                    DBProcess._db.AddInParameter(cmd, "@nation", DbType.String, Patmi_Ins.nation);
                    DBProcess._db.AddInParameter(cmd, "@identity", DbType.String, Patmi_Ins.identity);
                    DBProcess._db.AddInParameter(cmd, "@current_place", DbType.String, Patmi_Ins.current_place);
                    DBProcess._db.AddInParameter(cmd, "@si_card", DbType.String, Patmi_Ins.si_card);
                    DBProcess._db.AddInParameter(cmd, "@hospital_card", DbType.String, Patmi_Ins.hospital_card);
                    DBProcess._db.AddInParameter(cmd, "@phone_number", DbType.String, Patmi_Ins.phone_number);
                    DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, Patmi_Ins.patient_id);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "更新病人基本信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "更新病人基本信息失败！";
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Process_Patmi 执行语句异常：" + sqlstr);
                Zx_Result = false;
                Str_Result = "处理病人基本信息异常：" + ex.ToString();
            }
            return Zx_Result;
        }
        //查询病人ID是否存在
        public int GetExamPatCount(string patient_id)
        {
            try
            {
                string sqlstr = "select count(*) as sl from exam_pat_mi where patient_id=@patient_id";
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, patient_id);
                return Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch
            {
                return 0;
            }
        }
        public Model.exam_pat_mi GetPatientInfo(string patient_id)
        {
            string sqlstr = "select patient_id,patient_name,name_phonetic,sex,date_format(date_of_birth,'%Y-%m-%d') as date_of_birth,nation,identity,current_place,si_card,hospital_card,phone_number from exam_pat_mi where patient_id=@patient_id";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, patient_id);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count == 1)
                {
                    Model.exam_pat_mi Pat_Ins = new Model.exam_pat_mi();
                    Pat_Ins.patient_id = patient_id;
                    Pat_Ins.patient_name = ds.Tables[0].Rows[0]["patient_name"].ToString();
                    Pat_Ins.name_phonetic = ds.Tables[0].Rows[0]["name_phonetic"].ToString();
                    Pat_Ins.phone_number = ds.Tables[0].Rows[0]["phone_number"].ToString() ?? "";
                    Pat_Ins.sex = ds.Tables[0].Rows[0]["sex"].ToString();
                    Pat_Ins.si_card = ds.Tables[0].Rows[0]["si_card"].ToString() ?? "";
                    Pat_Ins.nation = ds.Tables[0].Rows[0]["nation"].ToString() ?? "";
                    Pat_Ins.identity = ds.Tables[0].Rows[0]["identity"].ToString() ?? "";
                    Pat_Ins.hospital_card = ds.Tables[0].Rows[0]["hospital_card"].ToString() ?? "";
                    Pat_Ins.date_of_birth = ds.Tables[0].Rows[0]["date_of_birth"].ToString();
                    Pat_Ins.current_place = ds.Tables[0].Rows[0]["current_place"].ToString();
                    return Pat_Ins;
                }

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetPatientInfo 执行语句异常：" + sqlstr);

            }
            return null;
        }

    }
}
