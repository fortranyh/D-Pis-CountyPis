using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DBHelper.BLL
{
    public class exam_master
    {

        public DataSet GetDs(string strSql)
        {
            DataSet ds = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(strSql);
                ds = DBProcess._db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDs 执行语句异常：" + strSql);
                ds = null;
            }
            return ds;
        }
        //获取取材医生
        public string GetQCYS(string study_no)
        {
            string Result = "";
            string sqlstr = "select qucai_doctor_name from exam_master where study_no=@study_no and exam_status>20";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Result = DBProcess._db.ExecuteScalar(cmd).ToString();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetQCYS 执行语句：" + sqlstr);
                Result = "";
            }
            return Result;
        }
        //删除申请状态超出60天的记录 
        public int DelteExamMaster60Days()
        {
            int Result = 0;
            string sqlstr = "delete from exam_master where exam_status=15 and date(req_date_time) <= date_sub(curdate(),interval 60 day)";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "DelteExamMaster60Days 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public DataSet GetDs(string exam_no, string study_no)
        {
            string strSql = "select   receive_doctor_name,new_flag,exam_no,study_no,exam_type,modality_cn,patient_name,sex,age,patient_id,patient_source,req_dept,req_physician,submit_unit,req_date_time,received_datetime,status_name,exam_status,si_card,hospital_card,output_id,input_id,ice_flag,modality,ward,bed_no,phone_number,visit_id,cbreport_doc_name from exam_master_view where exam_no='" + exam_no + "' and study_no='" + study_no + "'";
            DataSet ds = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(strSql);
                ds = DBProcess._db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDs 执行语句异常：" + strSql);
                ds = null;
            }
            return ds;
        }

        public DataTable GetDt(string sqlstr)
        {
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDt 执行语句异常：" + sqlstr);
            }
            return dt;
        }

        public DataTable GetPatInfo(int tj, string value, string exam_type)
        {
            DataTable dt = null;
            string sqlstr = "select exam_no,patient_name,sex,patient_id,input_id,output_id,patient_source from exam_master_view where ";
            try
            {
                if (tj == 0)
                {
                    sqlstr += "study_no=@value";
                }
                else if (tj == 1)
                {
                    sqlstr += "exam_no=@value";
                }
                else if (tj == 2)
                {
                    sqlstr += "output_id=@value";
                }
                else if (tj == 3)
                {
                    sqlstr += "input_id=@value";
                }


                sqlstr += " and exam_type in (" + exam_type + ") ";

                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@value", DbType.String, value);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetPatInfo 执行语句异常：" + sqlstr);
            }
            return dt;
        }
        public string GetModality(string study_no)
        {
            string sqlstr = "select modality as sl from exam_master where exam_status>0 and study_no=@study_no";
            string modality = "";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                modality = Convert.ToString(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetModality 执行语句异常：" + sqlstr);
            }
            return modality;
        }
        //查询检查是否存在
        public int GetExamNoCount(string exam_no)
        {
            try
            {
                string sqlstr = "select count(*) as sl from exam_master where exam_status>1 and exam_no=@exam_no";
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                return Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch
            {
                return 0;
            }
        }
        public Boolean Process_ExamMaster(Model.exam_master examMaster_Ins, ref string Str_Result)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_master where exam_no=@exam_no";
            try
            {
                examMaster_Ins.costs = examMaster_Ins.costs ?? "0";
                if (examMaster_Ins.costs.Equals(""))
                {
                    examMaster_Ins.costs = "0";
                }
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, examMaster_Ins.exam_no);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 0)
                {
                    //插入
                    sqlstr = "insert into exam_master(exam_no,modality,exam_type,examItems,study_no,patient_id,age,ageUint,new_flag,inout_type,input_id,output_id,ward,bed_no,patient_source,submit_unit,req_dept,req_dept_code,req_physician,req_physician_code,req_date_time,ice_flag,ks_flag,zl_flag,fk_flag,exam_status,received_doctor_code,receive_doctor_name,received_datetime,costs,wtzd_flag) values(@exam_no,@modality,@exam_type,@examItems,@study_no,@patient_id,@age,@ageUint,@new_flag,@inout_type,@input_id,@output_id,@ward,@bed_no,@patient_source,@submit_unit,@req_dept,@req_dept_code,@req_physician,@req_physician_code,@req_date_time,@ice_flag,@ks_flag,@zl_flag,@fk_flag,@exam_status,@received_doctor_code,@receive_doctor_name,@received_datetime,@costs,@wtzd_flag)";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, examMaster_Ins.exam_no);
                    DBProcess._db.AddInParameter(cmd, "@modality", DbType.String, examMaster_Ins.modality);
                    DBProcess._db.AddInParameter(cmd, "@exam_type", DbType.String, examMaster_Ins.exam_type);
                    DBProcess._db.AddInParameter(cmd, "@examItems", DbType.String, examMaster_Ins.examItems);
                    DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, examMaster_Ins.study_no);
                    DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, examMaster_Ins.patient_id);
                    DBProcess._db.AddInParameter(cmd, "@age", DbType.String, examMaster_Ins.age);
                    DBProcess._db.AddInParameter(cmd, "@ageUint", DbType.String, examMaster_Ins.ageUint);
                    DBProcess._db.AddInParameter(cmd, "@new_flag", DbType.Int16, examMaster_Ins.new_flag);
                    DBProcess._db.AddInParameter(cmd, "@inout_type", DbType.Int16, examMaster_Ins.inout_type);
                    DBProcess._db.AddInParameter(cmd, "@input_id", DbType.String, examMaster_Ins.input_id);
                    DBProcess._db.AddInParameter(cmd, "@output_id", DbType.String, examMaster_Ins.output_id);
                    DBProcess._db.AddInParameter(cmd, "@ward", DbType.String, examMaster_Ins.ward);
                    DBProcess._db.AddInParameter(cmd, "@bed_no", DbType.String, examMaster_Ins.bed_no);
                    DBProcess._db.AddInParameter(cmd, "@patient_source", DbType.String, examMaster_Ins.patient_source);
                    DBProcess._db.AddInParameter(cmd, "@submit_unit", DbType.String, examMaster_Ins.submit_unit);
                    DBProcess._db.AddInParameter(cmd, "@req_dept", DbType.String, examMaster_Ins.req_dept);
                    DBProcess._db.AddInParameter(cmd, "@req_dept_code", DbType.String, examMaster_Ins.req_dept_code);
                    DBProcess._db.AddInParameter(cmd, "@req_physician_code", DbType.String, examMaster_Ins.req_physician_code);
                    DBProcess._db.AddInParameter(cmd, "@req_physician", DbType.String, examMaster_Ins.req_physician);
                    DBProcess._db.AddInParameter(cmd, "@req_date_time", DbType.DateTime, examMaster_Ins.req_date_time);
                    DBProcess._db.AddInParameter(cmd, "@ice_flag", DbType.Int16, examMaster_Ins.ice_flag);
                    DBProcess._db.AddInParameter(cmd, "@ks_flag", DbType.Int16, examMaster_Ins.ks_flag);
                    DBProcess._db.AddInParameter(cmd, "@zl_flag", DbType.Int16, examMaster_Ins.zl_flag);
                    DBProcess._db.AddInParameter(cmd, "@fk_flag", DbType.Int16, examMaster_Ins.fk_flag);
                    DBProcess._db.AddInParameter(cmd, "@exam_status", DbType.String, examMaster_Ins.exam_status);
                    DBProcess._db.AddInParameter(cmd, "@received_doctor_code", DbType.String, examMaster_Ins.received_doctor_code);
                    DBProcess._db.AddInParameter(cmd, "@receive_doctor_name", DbType.String, examMaster_Ins.receive_doctor_name);
                    DBProcess._db.AddInParameter(cmd, "@received_datetime", DbType.DateTime, examMaster_Ins.received_datetime);
                    DBProcess._db.AddInParameter(cmd, "@costs", DbType.Single, Convert.ToSingle(examMaster_Ins.costs));
                    DBProcess._db.AddInParameter(cmd, "@wtzd_flag", DbType.Int16, examMaster_Ins.wtzd_flag);

                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "插入申请单信息成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "插入申请单信息失败！";
                    }
                }
                else if (sl == 1)
                {
                    sqlstr = "select exam_status from exam_master where exam_no='" + examMaster_Ins.exam_no + "'";
                    int Cur_status = Convert.ToInt32(DBProcess._db.ExecuteScalar(CommandType.Text, sqlstr));
                    if (Cur_status > 1 && Cur_status < 20)
                    {
                        //更新
                        sqlstr = "update exam_master set exam_type=@exam_type,bed_no=@bed_no,modality=@modality,examItems=@examItems,study_no=@study_no,age=@age,ageUint=@ageUint,inout_type=@inout_type,ice_flag=@ice_flag,ks_flag=@ks_flag,zl_flag=@zl_flag,fk_flag=@fk_flag,exam_status=@exam_status,patient_source=@patient_source,submit_unit=@submit_unit,req_dept=@req_dept,req_dept_code=@req_dept_code,req_physician_code=@req_physician_code,req_physician=@req_physician,received_doctor_code=@received_doctor_code,receive_doctor_name=@receive_doctor_name,received_datetime=@received_datetime,input_id=@input_id,costs=@costs,patient_id=@patient_id,wtzd_flag=@wtzd_flag where exam_no=@exam_no";
                        cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                        DBProcess._db.AddInParameter(cmd, "@exam_type", DbType.String, examMaster_Ins.exam_type);
                        DBProcess._db.AddInParameter(cmd, "@bed_no", DbType.String, examMaster_Ins.bed_no);
                        DBProcess._db.AddInParameter(cmd, "@modality", DbType.String, examMaster_Ins.modality);
                        DBProcess._db.AddInParameter(cmd, "@examItems", DbType.String, examMaster_Ins.examItems);
                        DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, examMaster_Ins.study_no);
                        DBProcess._db.AddInParameter(cmd, "@age", DbType.String, examMaster_Ins.age);
                        DBProcess._db.AddInParameter(cmd, "@ageUint", DbType.String, examMaster_Ins.ageUint);
                        DBProcess._db.AddInParameter(cmd, "@inout_type", DbType.Int16, examMaster_Ins.inout_type);
                        DBProcess._db.AddInParameter(cmd, "@ice_flag", DbType.Int16, examMaster_Ins.ice_flag);
                        DBProcess._db.AddInParameter(cmd, "@ks_flag", DbType.Int16, examMaster_Ins.ks_flag);
                        DBProcess._db.AddInParameter(cmd, "@zl_flag", DbType.Int16, examMaster_Ins.zl_flag);
                        DBProcess._db.AddInParameter(cmd, "@fk_flag", DbType.Int16, examMaster_Ins.fk_flag);
                        DBProcess._db.AddInParameter(cmd, "@exam_status", DbType.String, "20");
                        DBProcess._db.AddInParameter(cmd, "@patient_source", DbType.String, examMaster_Ins.patient_source);
                        DBProcess._db.AddInParameter(cmd, "@submit_unit", DbType.String, examMaster_Ins.submit_unit);
                        DBProcess._db.AddInParameter(cmd, "@req_dept", DbType.String, examMaster_Ins.req_dept);
                        DBProcess._db.AddInParameter(cmd, "@req_dept_code", DbType.String, examMaster_Ins.req_dept_code);
                        DBProcess._db.AddInParameter(cmd, "@req_physician_code", DbType.String, examMaster_Ins.req_physician_code);
                        DBProcess._db.AddInParameter(cmd, "@req_physician", DbType.String, examMaster_Ins.req_physician);
                        DBProcess._db.AddInParameter(cmd, "@received_doctor_code", DbType.String, examMaster_Ins.received_doctor_code);
                        DBProcess._db.AddInParameter(cmd, "@receive_doctor_name", DbType.String, examMaster_Ins.receive_doctor_name);
                        DBProcess._db.AddInParameter(cmd, "@received_datetime", DbType.DateTime, examMaster_Ins.received_datetime);
                        DBProcess._db.AddInParameter(cmd, "@input_id", DbType.String, examMaster_Ins.input_id);
                        DBProcess._db.AddInParameter(cmd, "@costs", DbType.Single, Convert.ToSingle(examMaster_Ins.costs));
                        DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, examMaster_Ins.patient_id);
                        DBProcess._db.AddInParameter(cmd, "@wtzd_flag", DbType.Int16, examMaster_Ins.wtzd_flag);
                        DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, examMaster_Ins.exam_no);
                        if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                        {
                            Zx_Result = true;
                            Str_Result = "更新申请单信息成功！";
                        }
                        else
                        {
                            Zx_Result = false;
                            Str_Result = "更新申请单信息失败！";
                        }
                    }
                    else if (Cur_status >= 20 && Cur_status < 55)
                    {
                        //更新
                        sqlstr = "update exam_master set age=@age,bed_no=@bed_no,ageUint=@ageUint,ice_flag=@ice_flag,ks_flag=@ks_flag,merge_exam_no=@merge_exam_no,patient_source=@patient_source,submit_unit=@submit_unit,req_dept=@req_dept,req_dept_code=@req_dept_code,req_physician_code=@req_physician_code,req_physician=@req_physician,input_id=@input_id,costs=@costs,patient_id=@patient_id,wtzd_flag=@wtzd_flag  where exam_no=@exam_no";
                        cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                        DBProcess._db.AddInParameter(cmd, "@age", DbType.String, examMaster_Ins.age);
                        DBProcess._db.AddInParameter(cmd, "@bed_no", DbType.String, examMaster_Ins.bed_no);
                        DBProcess._db.AddInParameter(cmd, "@ageUint", DbType.String, examMaster_Ins.ageUint);
                        DBProcess._db.AddInParameter(cmd, "@ice_flag", DbType.Int16, examMaster_Ins.ice_flag);
                        DBProcess._db.AddInParameter(cmd, "@ks_flag", DbType.Int16, examMaster_Ins.ks_flag);
                        DBProcess._db.AddInParameter(cmd, "@merge_exam_no", DbType.String, examMaster_Ins.merge_exam_no);
                        DBProcess._db.AddInParameter(cmd, "@patient_source", DbType.String, examMaster_Ins.patient_source);
                        DBProcess._db.AddInParameter(cmd, "@submit_unit", DbType.String, examMaster_Ins.submit_unit);
                        DBProcess._db.AddInParameter(cmd, "@req_dept", DbType.String, examMaster_Ins.req_dept);
                        DBProcess._db.AddInParameter(cmd, "@req_dept_code", DbType.String, examMaster_Ins.req_dept_code);
                        DBProcess._db.AddInParameter(cmd, "@req_physician_code", DbType.String, examMaster_Ins.req_physician_code);
                        DBProcess._db.AddInParameter(cmd, "@req_physician", DbType.String, examMaster_Ins.req_physician);
                        DBProcess._db.AddInParameter(cmd, "@input_id", DbType.String, examMaster_Ins.input_id);
                        DBProcess._db.AddInParameter(cmd, "@costs", DbType.Single, Convert.ToSingle(examMaster_Ins.costs));
                        DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, examMaster_Ins.patient_id);
                        DBProcess._db.AddInParameter(cmd, "@wtzd_flag", DbType.Int16, examMaster_Ins.wtzd_flag);
                        DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, examMaster_Ins.exam_no);
                        if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                        {
                            Zx_Result = true;
                            Str_Result = "更新申请单信息成功！";
                        }
                        else
                        {
                            Zx_Result = false;
                            Str_Result = "更新申请单信息失败！";
                        }

                        cmd.Parameters.Clear();
                        if (examMaster_Ins.merge_exam_no.ToString() != "")
                        {
                            //更新为合并状态
                            sqlstr = "update exam_master set exam_status='-1' where exam_no='" + examMaster_Ins.merge_exam_no + "'";
                            cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                            if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                            {
                                Zx_Result = true;
                                Str_Result = "更新为合并状态信息成功！";
                            }
                            else
                            {
                                Zx_Result = false;
                                Str_Result = "更新为合并状态信息失败！";
                            }
                        }
                    }
                    else
                    {
                        Zx_Result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Process_ExamMaster 执行语句异常：" + sqlstr);
                Zx_Result = false;
                Str_Result = "处理病人申请单信息异常：" + ex.ToString();
            }
            return Zx_Result;
        }


        public DataSet GetDsExam_master(string sj, string zt, string lasttj = "")
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from exam_master_view where 1=1 ");
            switch (sj)
            {
                case "今天":
                    sb.Append(" and to_days(req_date_time)=to_days(now())  ");
                    break;
                case "昨天":
                    sb.Append(" and to_days(now())-to_days(req_date_time)<=1  ");
                    break;
                case "三天":
                    sb.Append(" and date_sub(curdate(),interval 3 day)<=date(req_date_time)  ");
                    break;
                case "四天":
                    sb.Append(" and date_sub(curdate(),interval 4 day)<=date(req_date_time)  ");
                    break;
                case "五天":
                    sb.Append(" and date_sub(curdate(),interval 5 day)<=date(req_date_time)  ");
                    break;
                case "六天":
                    sb.Append(" and date_sub(curdate(),interval 6 day)<=date(req_date_time)  ");
                    break;
                case "一周":
                    sb.Append(" and date_sub(curdate(),interval 7 day)<=date(req_date_time)  ");
                    break;
                case "两周":
                    sb.Append(" and date_sub(curdate(),interval 14 day)<=date(req_date_time)  ");
                    break;
                case "三周":
                    sb.Append(" and date_sub(curdate(),interval 21 day)<=date(req_date_time)  ");
                    break;
                case "一月":
                    sb.Append(" and date_sub(curdate(),interval 30 day)<=date(req_date_time)  ");
                    break;
                default:
                    sb.Append(" and date_sub(curdate(),interval 14 day)<=date(req_date_time)  ");
                    break;
            }
            switch (zt)
            {
                case "-1":
                    sb.Append(" and  exam_status>1 and exam_status<60 ");
                    break;
                case "0":
                    sb.Append(" and  exam_status='0' ");
                    break;
                case "1":
                    sb.Append(" and exam_status='1' ");
                    break;
                case "10":
                    sb.Append(" and exam_status='10' ");
                    break;
                case "15":
                    sb.Append(" and exam_status='15' ");
                    break;
                case "20":
                    sb.Append(" and exam_status='20' ");
                    break;
                case "25":
                    sb.Append(" and exam_status='25' ");
                    break;
                case "27":
                    sb.Append(" and exam_status='27' ");
                    break;
                case "30":
                    sb.Append(" and exam_status='30' ");
                    break;
                case "35":
                    sb.Append(" and exam_status='35' ");
                    break;
                case "36":
                    sb.Append(" and exam_status='36' ");
                    break;
                case "40":
                    sb.Append(" and exam_status='40' ");
                    break;
                case "50":
                    sb.Append(" and exam_status='50' ");
                    break;
                case "55":
                    sb.Append(" and  exam_status='55' ");
                    break;
                case "60":
                    sb.Append(" and  exam_status='60' ");
                    break;
                default:
                    sb.Append(" and  exam_status>1 and exam_status<60 ");
                    break;
            }
            try
            {
                sb.Append(lasttj);

                sb.Append(" order by study_no asc");
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsExam_master 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }
        //获取登记信息
        public DataSet GetDJDsExam_master(string sj, Boolean QcTypeFlag, string zt, string exam_type)
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from exam_master_view where 1=1 ");
            if (QcTypeFlag)
            {

                sb.Append(" and exam_type in (" + exam_type + ")");

            }
            switch (zt)
            {

                case "10":
                    sb.Append(" and exam_status='10' ");
                    switch (sj)
                    {
                        case "今天":
                            sb.Append(" and to_days(req_date_time)=to_days(now())  ");
                            break;
                        case "昨天":
                            sb.Append(" and to_days(now())-to_days(req_date_time)<=1  ");
                            break;
                        case "三天":
                            sb.Append(" and date_sub(curdate(),interval 3 day)<=date(req_date_time)  ");
                            break;
                        case "四天":
                            sb.Append(" and date_sub(curdate(),interval 4 day)<=date(req_date_time)  ");
                            break;
                        case "五天":
                            sb.Append(" and date_sub(curdate(),interval 5 day)<=date(req_date_time)  ");
                            break;
                        case "六天":
                            sb.Append(" and date_sub(curdate(),interval 6 day)<=date(req_date_time)  ");
                            break;
                        case "一周":
                            sb.Append(" and date_sub(curdate(),interval 7 day)<=date(req_date_time)  ");
                            break;
                        case "两周":
                            sb.Append(" and date_sub(curdate(),interval 14 day)<=date(req_date_time)  ");
                            break;
                        case "三周":
                            sb.Append(" and date_sub(curdate(),interval 21 day)<=date(req_date_time)  ");
                            break;
                        case "一月":
                            sb.Append(" and date_sub(curdate(),interval 30 day)<=date(req_date_time)  ");
                            break;
                        default:
                            sb.Append(" and date_sub(curdate(),interval 14 day)<=date(req_date_time)  ");
                            break;
                    }
                    sb.Append(" order by req_date_time desc");
                    break;
                case "15":
                    sb.Append(" and exam_status='15' ");
                    switch (sj)
                    {
                        case "今天":
                            sb.Append(" and to_days(req_date_time)=to_days(now())  ");
                            break;
                        case "昨天":
                            sb.Append(" and to_days(now())-to_days(req_date_time)<=1  ");
                            break;
                        case "三天":
                            sb.Append(" and date_sub(curdate(),interval 3 day)<=date(req_date_time)  ");
                            break;
                        case "四天":
                            sb.Append(" and date_sub(curdate(),interval 4 day)<=date(req_date_time)  ");
                            break;
                        case "五天":
                            sb.Append(" and date_sub(curdate(),interval 5 day)<=date(req_date_time)  ");
                            break;
                        case "六天":
                            sb.Append(" and date_sub(curdate(),interval 6 day)<=date(req_date_time)  ");
                            break;
                        case "一周":
                            sb.Append(" and date_sub(curdate(),interval 7 day)<=date(req_date_time)  ");
                            break;
                        case "两周":
                            sb.Append(" and date_sub(curdate(),interval 14 day)<=date(req_date_time)  ");
                            break;
                        case "三周":
                            sb.Append(" and date_sub(curdate(),interval 21 day)<=date(req_date_time)  ");
                            break;
                        case "一月":
                            sb.Append(" and date_sub(curdate(),interval 30 day)<=date(req_date_time)  ");
                            break;
                        default:
                            sb.Append(" and date_sub(curdate(),interval 14 day)<=date(req_date_time)  ");
                            break;
                    }
                    sb.Append(" order by req_date_time desc");
                    break;
                case "20":
                    sb.Append(" and exam_status='20' ");
                    switch (sj)
                    {
                        case "今天":
                            sb.Append(" and to_days(received_datetime)=to_days(now())  ");
                            break;
                        case "昨天":
                            sb.Append(" and to_days(now())-to_days(received_datetime)<=1  ");
                            break;
                        case "三天":
                            sb.Append(" and date_sub(curdate(),interval 3 day)<=date(received_datetime)  ");
                            break;
                        case "四天":
                            sb.Append(" and date_sub(curdate(),interval 4 day)<=date(received_datetime)  ");
                            break;
                        case "五天":
                            sb.Append(" and date_sub(curdate(),interval 5 day)<=date(received_datetime)  ");
                            break;
                        case "六天":
                            sb.Append(" and date_sub(curdate(),interval 6 day)<=date(received_datetime)  ");
                            break;
                        case "一周":
                            sb.Append(" and date_sub(curdate(),interval 7 day)<=date(received_datetime)  ");
                            break;
                        case "两周":
                            sb.Append(" and date_sub(curdate(),interval 14 day)<=date(received_datetime)  ");
                            break;
                        case "三周":
                            sb.Append(" and date_sub(curdate(),interval 21 day)<=date(received_datetime)  ");
                            break;
                        case "一月":
                            sb.Append(" and date_sub(curdate(),interval 30 day)<=date(received_datetime)  ");
                            break;
                        default:
                            sb.Append(" and date_sub(curdate(),interval 14 day)<=date(received_datetime)  ");
                            break;
                    }
                    sb.Append(" order by study_no  desc");
                    break;
                default:
                    sb.Append(" and  exam_status>1 and exam_status<=20 ");
                    switch (sj)
                    {
                        case "今天":
                            sb.Append(" and to_days(req_date_time)=to_days(now())  ");
                            break;
                        case "昨天":
                            sb.Append(" and to_days(now())-to_days(req_date_time)<=1  ");
                            break;
                        case "三天":
                            sb.Append(" and date_sub(curdate(),interval 3 day)<=date(req_date_time)  ");
                            break;
                        case "四天":
                            sb.Append(" and date_sub(curdate(),interval 4 day)<=date(req_date_time)  ");
                            break;
                        case "五天":
                            sb.Append(" and date_sub(curdate(),interval 5 day)<=date(req_date_time)  ");
                            break;
                        case "六天":
                            sb.Append(" and date_sub(curdate(),interval 6 day)<=date(req_date_time)  ");
                            break;
                        case "一周":
                            sb.Append(" and date_sub(curdate(),interval 7 day)<=date(req_date_time)  ");
                            break;
                        case "两周":
                            sb.Append(" and date_sub(curdate(),interval 14 day)<=date(req_date_time)  ");
                            break;
                        case "三周":
                            sb.Append(" and date_sub(curdate(),interval 21 day)<=date(req_date_time)  ");
                            break;
                        case "一月":
                            sb.Append(" and date_sub(curdate(),interval 30 day)<=date(req_date_time)  ");
                            break;
                        default:
                            sb.Append(" and date_sub(curdate(),interval 14 day)<=date(req_date_time)  ");
                            break;
                    }
                    sb.Append(" order by study_no  desc,req_date_time desc");
                    break;
            }
            try
            {
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDJDsExam_master 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }


        //获取取材管理
        public DataSet GetQCDsExam_master(string sj, string zt, string exam_type, string lasttj = "")
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from exam_master_view where 1=1 ");
            switch (sj)
            {
                case "今天":
                    sb.Append(" and to_days(received_datetime)=to_days(now())  ");
                    break;
                case "昨天":
                    sb.Append(" and to_days(now())-to_days(received_datetime)<=1  ");
                    break;
                case "三天":
                    sb.Append(" and date_sub(curdate(),interval 3 day)<=date(received_datetime)  ");
                    break;
                case "四天":
                    sb.Append(" and date_sub(curdate(),interval 4 day)<=date(received_datetime)  ");
                    break;
                case "五天":
                    sb.Append(" and date_sub(curdate(),interval 5 day)<=date(received_datetime)  ");
                    break;
                case "六天":
                    sb.Append(" and date_sub(curdate(),interval 6 day)<=date(received_datetime)  ");
                    break;
                case "一周":
                    sb.Append(" and date_sub(curdate(),interval 7 day)<=date(received_datetime)  ");
                    break;
                case "两周":
                    sb.Append(" and date_sub(curdate(),interval 14 day)<=date(received_datetime)  ");
                    break;
                case "三周":
                    sb.Append(" and date_sub(curdate(),interval 21 day)<=date(received_datetime)  ");
                    break;
                case "一月":
                    sb.Append(" and date_sub(curdate(),interval 30 day)<=date(received_datetime)  ");
                    break;
                default:
                    sb.Append(" and date_sub(curdate(),interval 14 day)<=date(received_datetime)  ");
                    break;
            }
            switch (zt)
            {

                case "20":
                    sb.Append(" and exam_status='20' ");
                    break;
                case "25":
                    sb.Append(" and exam_status='25' ");
                    break;
                default:
                    sb.Append(" and  exam_status>=20 and exam_status<=25 ");
                    break;
            }
            try
            {
                sb.Append(lasttj);

                sb.Append(" and exam_type in (" + exam_type + ")");

                sb.Append(" order by study_no asc");

                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetQCDsExam_master 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }


        //获取技术制片
        public DataSet GetZPDsExam_master(string sj, string zt, int exam_type, string lasttj = "")
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from exam_master_view where 1=1 ");
            switch (sj)
            {
                case "今天":
                    sb.Append(" and to_days(received_datetime)=to_days(now())  ");
                    break;
                case "昨天":
                    sb.Append(" and to_days(now())-to_days(received_datetime)<=1  ");
                    break;
                case "三天":
                    sb.Append(" and date_sub(curdate(),interval 3 day)<=date(received_datetime)  ");
                    break;
                case "四天":
                    sb.Append(" and date_sub(curdate(),interval 4 day)<=date(received_datetime)  ");
                    break;
                case "五天":
                    sb.Append(" and date_sub(curdate(),interval 5 day)<=date(received_datetime)  ");
                    break;
                case "六天":
                    sb.Append(" and date_sub(curdate(),interval 6 day)<=date(received_datetime)  ");
                    break;
                case "一周":
                    sb.Append(" and date_sub(curdate(),interval 7 day)<=date(received_datetime)  ");
                    break;
                case "两周":
                    sb.Append(" and date_sub(curdate(),interval 14 day)<=date(received_datetime)  ");
                    break;
                case "三周":
                    sb.Append(" and date_sub(curdate(),interval 21 day)<=date(received_datetime)  ");
                    break;
                case "一月":
                    sb.Append(" and date_sub(curdate(),interval 30 day)<=date(received_datetime)  ");
                    break;
                default:
                    sb.Append(" and date_sub(curdate(),interval 14 day)<=date(received_datetime)  ");
                    break;
            }
            switch (zt)
            {

                case "25":
                    sb.Append(" and exam_status='25' ");
                    break;
                case "27":
                    sb.Append(" and exam_status='27' ");
                    break;
                case "30":
                    sb.Append(" and exam_status='30' ");
                    break;
                default:
                    sb.Append(" and  exam_status>=25 and exam_status<=30 ");
                    break;
            }
            try
            {
                sb.Append(lasttj);
                sb.Append(" and exam_type in (" + exam_type + ")");
                sb.Append(" order by study_no asc");
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetZPDsExam_master 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }


        //报告工作站刷新（归档类型不允许出现）
        public DataSet GetDsBGExam_master(string sj, string zt, string exam_type, string cbreport_doc_code, string lb_tj, int ksice = 0, Boolean qcbg_flag = false)
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from exam_master_view  where 1=1 ");
            switch (sj)
            {
                case "今天":
                    sb.Append(" and to_days(received_datetime)=to_days(now())  ");
                    break;
                case "昨天":
                    sb.Append(" and to_days(now())-to_days(received_datetime)<=1  ");
                    break;
                case "三天":
                    sb.Append(" and date_sub(curdate(),interval 3 day)<=date(received_datetime)  ");
                    break;
                case "四天":
                    sb.Append(" and date_sub(curdate(),interval 4 day)<=date(received_datetime)  ");
                    break;
                case "五天":
                    sb.Append(" and date_sub(curdate(),interval 5 day)<=date(received_datetime)  ");
                    break;
                case "六天":
                    sb.Append(" and date_sub(curdate(),interval 6 day)<=date(received_datetime)  ");
                    break;
                case "一周":
                    sb.Append(" and date_sub(curdate(),interval 7 day)<=date(received_datetime)  ");
                    break;
                case "两周":
                    sb.Append(" and date_sub(curdate(),interval 14 day)<=date(received_datetime)  ");
                    break;
                case "三周":
                    sb.Append(" and date_sub(curdate(),interval 21 day)<=date(received_datetime)  ");
                    break;
                case "一月":
                    sb.Append(" and date_sub(curdate(),interval 30 day)<=date(received_datetime)  ");
                    break;
                default:
                    sb.Append(" and date_sub(curdate(),interval 14 day)<=date(received_datetime)  ");
                    break;
            }
            switch (zt)
            {
                case "30":
                    sb.Append(" and exam_status='30' ");
                    break;
                case "35":
                    sb.Append(" and exam_status='35' ");
                    break;
                case "36":
                    sb.Append(" and exam_status='36' ");
                    break;
                case "40":
                    sb.Append(" and exam_status='40' ");
                    break;

                case "50":
                    sb.Append(" and exam_status='50' ");
                    break;
                case "55":
                    sb.Append(" and exam_status='55' ");
                    break;
                case "90":
                    sb.Append(" and exam_status>='40' and exam_status<'55' ");
                    sb.Append(" and cbreport_doc_code='" + cbreport_doc_code + "'");
                    break;
                default:

                    sb.Append(" and exam_status>=20 and exam_status<55   ");
                    break;
            }
            try
            {
                if (ksice == 1)
                {
                    sb.Append(" and ice_flag=1 ");
                }
                else if (ksice == 2)
                {

                    sb.Append(" and ks_flag=1 ");
                }
                if (lb_tj.Equals("all"))
                {
                    sb.Append(" and exam_type in (" + exam_type + ")");
                }
                else
                {
                    sb.Append(" and exam_type =" + lb_tj);
                }
                if (qcbg_flag)
                {
                    sb.Append(" and (cbreport_doc_code='" + cbreport_doc_code + "' || qucai_doctor_code='" + cbreport_doc_code + "' || zzreport_doc_code='" + cbreport_doc_code + "' || shreprt_doc_code='" + cbreport_doc_code + "' || qucai_doctor_code='') ");
                }

                sb.Append("  order by study_no asc limit 800");
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDsBGExam_master 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }



        //条件查询（归档类型不允许出现）
        public DataSet QueryDsExam_master(string key_str, string str_tj, string exam_type)
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from exam_master_view where exam_status>1 and exam_status<60 ");
            switch (key_str)
            {
                case "申请单号":
                    sb.Append(" and exam_no='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "病人编号":
                    sb.Append(" and patient_id='");
                    sb.Append(str_tj.ToUpper());
                    sb.Append("'");
                    break;
                case "病理号":
                    sb.Append(" and study_no='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "门诊号":
                    sb.Append(" and output_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "住院号":
                    sb.Append(" and input_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "姓名":
                    sb.Append(" and patient_name like '%");
                    sb.Append(str_tj);
                    sb.Append("%'");
                    break;
                case "医院卡号":
                    sb.Append(" and hospital_card='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "医保卡号":
                    sb.Append(" and si_card='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                default:
                    sb.Append(" ");
                    break;
            }
            try
            {
                sb.Append(" and exam_type in (" + exam_type + ")");
                sb.Append(" order by study_no desc,req_date_time desc");
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "QueryDsExam_master 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }

        //标本登记
        public DataSet QueryDJDsExam_master(string key_str, Boolean QcTypeFlag, string str_tj, string exam_type)
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from exam_master_view where exam_status>1 and exam_status<60  ");
            switch (key_str)
            {
                case "申请单号":
                    sb.Append(" and exam_no='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "病人编号":
                    sb.Append(" and patient_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "病理号":
                    sb.Append(" and study_no='");
                    sb.Append(str_tj.ToUpper());
                    sb.Append("'");
                    break;
                case "门诊号":
                    sb.Append(" and output_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "住院号":
                    sb.Append(" and input_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "姓名":
                    sb.Append(" and patient_name like '%");
                    sb.Append(str_tj);
                    sb.Append("%'");
                    break;
                case "医院卡号":
                    sb.Append(" and hospital_card='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "医保卡号":
                    sb.Append(" and si_card='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                default:
                    sb.Append(" ");
                    break;
            }
            try
            {
                if (QcTypeFlag)
                {
                    sb.Append(" and exam_type in (" + exam_type + ")");
                }
                sb.Append(" order by req_date_time asc");
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "QueryDJDsExam_master 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }


        //取材管理
        public DataSet QueryQCDsExam_master(string key_str, string str_tj, string exam_type)
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from exam_master_view where exam_status>=20 and exam_status<=25 ");
            switch (key_str)
            {
                case "申请单号":
                    sb.Append(" and exam_no='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "病人编号":
                    sb.Append(" and patient_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "病理号":
                    sb.Append(" and study_no='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "门诊号":
                    sb.Append(" and output_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "住院号":
                    sb.Append(" and input_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "姓名":
                    sb.Append(" and patient_name like '%");
                    sb.Append(str_tj);
                    sb.Append("%'");
                    break;
                case "医院卡号":
                    sb.Append(" and hospital_card='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "医保卡号":
                    sb.Append(" and si_card='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                default:
                    sb.Append(" ");
                    break;
            }
            try
            {
                sb.Append(" and exam_type in (" + exam_type + ")");
                sb.Append(" order by study_no desc");
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "QueryQCDsExam_master 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }


        //制片工作站条件查询
        public DataSet QueryDsZPExam_master(string key_str, string str_tj, string exam_type)
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from exam_master_view where exam_status>1 and exam_status<60 ");
            switch (key_str)
            {
                case "申请单号":
                    sb.Append(" and exam_no='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "病人编号":
                    sb.Append(" and patient_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "病理号":
                    sb.Append(" and study_no='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "门诊号":
                    sb.Append(" and output_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "住院号":
                    sb.Append(" and input_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "姓名":
                    sb.Append(" and patient_name like '%");
                    sb.Append(str_tj);
                    sb.Append("%'");
                    break;
                case "医院卡号":
                    sb.Append(" and hospital_card='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "医保卡号":
                    sb.Append(" and si_card='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                default:
                    sb.Append(" ");
                    break;
            }
            try
            {
                sb.Append(" and exam_type in (" + exam_type + ")");
                sb.Append(" order by study_no desc");
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "QueryDsZPExam_master 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }



        //报告工作站条件查询（归档类型不允许出现）
        public DataSet QueryDsBGExam_master(string key_str, string str_tj, string exam_type)
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from exam_master_view where exam_status>1 and exam_status<60 ");
            switch (key_str)
            {
                case "申请单号":
                    sb.Append(" and exam_no='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "病人编号":
                    sb.Append(" and patient_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "病理号":
                    sb.Append(" and study_no='");
                    sb.Append(str_tj.ToUpper());
                    sb.Append("'");
                    break;
                case "门诊号":
                    sb.Append(" and output_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "住院号":
                    sb.Append(" and input_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "姓名":
                    sb.Append(" and patient_name like '%");
                    sb.Append(str_tj);
                    sb.Append("%'");
                    break;
                case "医院卡号":
                    sb.Append(" and hospital_card='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "医保卡号":
                    sb.Append(" and si_card='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "诊断意见":
                    sb.Append(" and zdyj like '%");
                    sb.Append(str_tj);
                    sb.Append("%'");
                    break;
                case "综合诊断意见":
                    string[] values = str_tj.Split('|');
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (!values[i].Equals(""))
                        {
                            sb.Append(" and zdyj like '%");
                            sb.Append(values[i]);
                            sb.Append("%'");
                        }
                    }
                    break;
                default:
                    sb.Append(" ");
                    break;
            }
            try
            {
                sb.Append(" and exam_type in (" + exam_type + ")");
                sb.Append(" order by study_no desc limit 100");
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "QueryDsBGExam_master 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }
        //扫描查询(扫描只能显示预约和申请两种状态)
        public DataSet QuerySMDsExam_master(string key_str, Boolean QcTypeFlag, string str_tj, string exam_type)
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from exam_master_view where  exam_status>1 and exam_status<20 ");
            switch (key_str)
            {
                case "申请单号":
                    sb.Append(" and exam_no='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "病人编号":
                    sb.Append(" and patient_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "病理号":
                    sb.Append(" and study_no='");
                    sb.Append(str_tj.ToUpper());
                    sb.Append("'");
                    break;
                case "门诊号":
                    sb.Append(" and output_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "住院号":
                    sb.Append(" and input_id='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "姓名":
                    sb.Append(" and patient_name like '%");
                    sb.Append(str_tj);
                    sb.Append("%'");
                    break;
                case "医院卡号":
                    sb.Append(" and hospital_card='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "医保卡号":
                    sb.Append(" and si_card='");
                    sb.Append(str_tj);
                    sb.Append("'");
                    break;
                case "混合":
                    sb.AppendFormat(" and (input_id='{0}' ||  hospital_card='{0}' || exam_no='{0}' || patient_id='{0}'|| si_card='{0}') ", str_tj);
                    break;
                default:
                    sb.Append(" ");
                    break;
            }
            try
            {
                if (QcTypeFlag)
                {
                    sb.Append(" and exam_type in (" + exam_type + ")");
                }
                sb.Append(" order by req_date_time desc");
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "QuerySMDsExam_master 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }





        //延期报告查询
        public DataSet QueryDelayDsExam_master(string str_tj)
        {
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from exam_master_view where exam_status=36 and delay_reason like '%" + str_tj + "%'");
            try
            {
                sb.Append(" order by study_no desc");
                ds = DBProcess._db.ExecuteDataSet(CommandType.Text, sb.ToString());
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "QueryDelayDsExam_master 执行语句：" + sb.ToString());
            }
            sb.Clear();
            return ds;
        }



        public Boolean ZF_ExamMaster(string Sqd, string zf_doc_code, ref string Str_Result)
        {
            Boolean Zx_Result = false;
            string sqlstr = "select count(*) as sl from exam_master where exam_status<'55' and exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Sqd);
                int sl = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                if (sl == 0)
                {
                    Str_Result = string.Format("作废申请失败：\n报告打印不能退!");
                    Zx_Result = false;
                }
                else
                {
                    sqlstr = "update exam_master set study_no=NULL, exam_status='1',zf_doc_code=@zf_doc_code where exam_no=@exam_no";
                    cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, Sqd);
                    DBProcess._db.AddInParameter(cmd, "@zf_doc_code", DbType.String, zf_doc_code);
                    if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                    {
                        Zx_Result = true;
                        Str_Result = "作废申请单成功！";
                    }
                    else
                    {
                        Zx_Result = false;
                        Str_Result = "作废申请单失败！";
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "ZF_ExamMaster 执行语句：" + sqlstr);
                Str_Result = string.Format("作废申请单异常:\n{0}", ex.ToString());
            }
            return Zx_Result;
        }

        //历次检查数目
        public int GetHistoryExamCount(string exam_no, string patient_name)
        {
            int Zx_Result = 0;
            //'已经打印'作为是否是历史检查的结点
            string sqlstr = "select count(*) from exam_master_view where exam_no!=@exam_no and patient_name=@patient_name and exam_status>=40";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DBProcess._db.AddInParameter(cmd, "@patient_name", DbType.String, patient_name);
                Zx_Result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetHistoryExamCount 执行语句：" + sqlstr);
            }
            return Zx_Result;
        }
        //历次检查数目
        public int GetExamCount(string exam_no, string patient_name)
        {
            int Zx_Result = 0;
            //'已经打印'作为是否是历史检查的结点
            string sqlstr = "select count(*) from exam_master_view where exam_no!=@exam_no and patient_name=@patient_name and exam_status>=20";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DBProcess._db.AddInParameter(cmd, "@patient_name", DbType.String, patient_name);
                Zx_Result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetHistoryExamCount 执行语句：" + sqlstr);
            }
            return Zx_Result;
        }
        //针对冷冻后送情况，查询是否已经存在此人的病理检查
        public int GetSameExamCount(string exam_no, string patient_id)
        {
            int Zx_Result = 0;
            //'已经打印'作为是否是历史检查的结点,其他状态都认为是当前检查
            string sqlstr = "select count(*) from exam_master where exam_no!=@exam_no and patient_id=@patient_id and exam_status>=20 and exam_status<55";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, patient_id);
                Zx_Result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetSameExamCount 执行语句：" + sqlstr);
            }
            return Zx_Result;
        }
        //获取相同检查
        public DataTable GetDtSameExam(string exam_no, string patient_id)
        {
            DataTable dt = null;
            string sqlstr = "select study_no,received_datetime,exam_no,patient_id as pat_id,patient_name as pat_name,status_name,ice_flag from exam_master_view where exam_no!=@exam_no and patient_id=@patient_id and exam_status>=20 and exam_status<55";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DBProcess._db.AddInParameter(cmd, "@patient_id", DbType.String, patient_id);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDtSameExam 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
        //获取委托诊断标记
        public int GetWtzdFlag(string exam_no)
        {
            int RetInt = 0;
            string sqlstr = "select wtzd_flag from exam_master where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count == 1)
                {
                    RetInt = Convert.ToInt32(ds.Tables[0].Rows[0]["wtzd_flag"]);
                }
            }
            catch (Exception ex)
            {
                RetInt = 0;
                DBProcess.ShowException(ex, "GetWtzdFlag 执行语句：" + sqlstr);
            }
            return RetInt;
        }
        //获取当前申请单信息
        public Model.exam_master GetExam_MasterInfo(string exam_no)
        {
            string sqlstr = "select exam_no,modality,exam_type,study_no,age,ageUint,patient_id,new_flag,inout_type,input_id,output_id,ward,bed_no,patient_source,submit_unit,req_dept,req_physician,date_format(req_date_time,'%Y-%m-%d %H:%i:%s') as req_date_time,ice_flag,ks_flag,zl_flag,fk_flag,exam_status,costs,examItems,wtzd_flag from exam_master where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count == 1)
                {
                    Model.exam_master MasIns = new Model.exam_master();
                    MasIns.exam_no = exam_no;
                    MasIns.age = ds.Tables[0].Rows[0]["age"].ToString();
                    MasIns.ageUint = ds.Tables[0].Rows[0]["ageUint"].ToString();
                    MasIns.modality = ds.Tables[0].Rows[0]["modality"].ToString();
                    MasIns.exam_type = ds.Tables[0].Rows[0]["exam_type"].ToString();
                    MasIns.study_no = ds.Tables[0].Rows[0]["study_no"].ToString() ?? "";
                    MasIns.patient_id = ds.Tables[0].Rows[0]["patient_id"].ToString();
                    MasIns.new_flag = Convert.ToInt32(ds.Tables[0].Rows[0]["new_flag"]);
                    MasIns.inout_type = Convert.ToInt32(ds.Tables[0].Rows[0]["inout_type"]);
                    MasIns.input_id = ds.Tables[0].Rows[0]["input_id"].ToString() ?? "";
                    MasIns.output_id = ds.Tables[0].Rows[0]["output_id"].ToString() ?? "";
                    MasIns.ward = ds.Tables[0].Rows[0]["ward"].ToString() ?? "";
                    MasIns.bed_no = ds.Tables[0].Rows[0]["bed_no"].ToString() ?? "";
                    MasIns.patient_source = ds.Tables[0].Rows[0]["patient_source"].ToString();
                    MasIns.submit_unit = ds.Tables[0].Rows[0]["submit_unit"].ToString() ?? "";
                    MasIns.req_dept = ds.Tables[0].Rows[0]["req_dept"].ToString() ?? "";
                    MasIns.req_physician = ds.Tables[0].Rows[0]["req_physician"].ToString() ?? "";
                    MasIns.req_date_time = ds.Tables[0].Rows[0]["req_date_time"].ToString();
                    MasIns.ice_flag = Convert.ToInt32(ds.Tables[0].Rows[0]["ice_flag"]);
                    MasIns.ks_flag = Convert.ToInt32(ds.Tables[0].Rows[0]["ks_flag"]);
                    MasIns.zl_flag = Convert.ToInt32(ds.Tables[0].Rows[0]["zl_flag"]);
                    MasIns.fk_flag = Convert.ToInt32(ds.Tables[0].Rows[0]["fk_flag"]);
                    MasIns.exam_status = ds.Tables[0].Rows[0]["exam_status"].ToString();
                    MasIns.costs = ds.Tables[0].Rows[0]["costs"].ToString();
                    MasIns.examItems = ds.Tables[0].Rows[0]["examItems"].ToString();
                    MasIns.wtzd_flag = Convert.ToInt32(ds.Tables[0].Rows[0]["wtzd_flag"]);
                    return MasIns;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetExam_MasterInfo 执行语句：" + sqlstr);
            }
            return null;
        }
        //取材获取基本信息
        public DataTable GetQcPatInfo(string exam_no)
        {
            string sqlstr = "select study_no,patient_name,sex,age,req_dept,exam_no,req_physician,patient_source,submit_unit from exam_master_view where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DataSet ds = DBProcess._db.ExecuteDataSet(cmd);
                cmd.Parameters.Clear();
                if (ds != null && ds.Tables[0].Rows.Count == 1)
                {
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetQcPatInfo 执行语句：" + sqlstr);
            }
            return null;
        }
        //根据病理号获取申请单号
        public string GetExam_No(string study_no)
        {
            string sqlstr = "select exam_no from exam_master where  study_no=@study_no and exam_status>1";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                string value_status = Convert.ToString(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                return value_status;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetExam_No 执行语句：" + sqlstr);
            }
            return "";
        }
        //获取病理诊断和临床诊断
        public void GetLczdBlzd(string study_no, ref string lczd, ref string blzd)
        {
            string str1 = "select zdyj from exam_report where study_no=@study_no";
            string str2 = "select clinical_diag from exam_requisition where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(str1);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                blzd = Convert.ToString(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetLczdBlzd 执行语句：" + str1);
            }
            string exam_no = GetExam_No(study_no);
            if (exam_no != "")
            {
                try
                {
                    DbCommand cmd = DBProcess._db.GetSqlStringCommand(str2);
                    DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                    lczd = Convert.ToString(DBProcess._db.ExecuteScalar(cmd));
                    cmd.Parameters.Clear();
                }
                catch (Exception ex)
                {
                    DBProcess.ShowException(ex, "GetLczdBlzd 执行语句：" + str2);
                }
            }
        }


        //根据申请单号获取检查状态
        public int GetExam_Status(string exam_no)
        {
            string sqlstr = "select exam_status from exam_master where  exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                int value_status = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                return value_status;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetExam_Status 执行语句：" + sqlstr);
            }
            return -1;
        }
        //根据病理号获取检查状态
        public int GetStudyExam_Status(string study_no)
        {
            string sqlstr = "select exam_status from exam_master where exam_status>1 and study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                int value_status = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
                return value_status;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetStudyExam_Status 执行语句：" + sqlstr);
            }
            return -1;
        }

        //更新冰冻状态
        public int UpdateIce_Flag(string exam_no, string ice_flag)
        {
            int Result = 0;
            string sqlstr = "update exam_master set ice_flag=@ice_flag where exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                DBProcess._db.AddInParameter(cmd, "@ice_flag", DbType.Int32, Convert.ToInt32(ice_flag));
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDtSameExam 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
        //更新已经取材结束状态
        public int UpdateQC_Flag(string study_no, string qucai_doctor_code, string qucai_doctor_name)
        {
            int Result = 0;
            string sqlstr = "update exam_master set exam_status=25,qucai_datetime=@qucai_datetime,qucai_doctor_code=@qucai_doctor_code,qucai_doctor_name=@qucai_doctor_name where study_no=@study_no and exam_status>=20 and exam_status<25";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@qucai_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                DBProcess._db.AddInParameter(cmd, "@qucai_doctor_code", DbType.String, qucai_doctor_code);
                DBProcess._db.AddInParameter(cmd, "@qucai_doctor_name", DbType.String, qucai_doctor_name);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateQC_Flag 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
        //更新已经包埋结束状态
        public int UpdateBM_Flag(string study_no, string baomai_doctor_code, string baomai_doctor_name)
        {
            int Result = 0;
            string sqlstr = "update exam_master set exam_status=27,baomai_datetime=@baomai_datetime,baomai_doctor_code=@baomai_doctor_code,baomai_doctor_name=@baomai_doctor_name where study_no=@study_no and exam_status>=25 and exam_status<27";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@baomai_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                DBProcess._db.AddInParameter(cmd, "@baomai_doctor_code", DbType.String, baomai_doctor_code);
                DBProcess._db.AddInParameter(cmd, "@baomai_doctor_name", DbType.String, baomai_doctor_name);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateBM_Flag 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
        //查询包埋相关信息
        public Boolean GetBaoMaiInfo(ref string doctorname, ref string baomaiDateTime, string study_no)
        {
            Boolean zxResult = false;
            string sqlstr = " select date_format(baomai_datetime,'%Y-%m-%d %H:%i:%s') as baomai_datetime,baomai_doctor_name from exam_master where exam_status>=27 and study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                using (IDataReader dataReader = DBProcess._db.ExecuteReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        doctorname = dataReader["baomai_doctor_name"].ToString();
                        baomaiDateTime = dataReader["baomai_datetime"].ToString();
                        zxResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetBaoMaiInfo 执行语句：" + sqlstr);
            }
            return zxResult;
        }
        //查询取材相关信息
        public Boolean GetQuCaiInfo(ref string doctorname, ref string QCDateTime, string study_no)
        {
            Boolean zxResult = false;
            string sqlstr = " select date_format(qucai_datetime,'%Y-%m-%d %H:%i:%s') as qucai_datetime,qucai_doctor_name from exam_master where exam_status>=25 and study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                using (IDataReader dataReader = DBProcess._db.ExecuteReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        doctorname = dataReader["qucai_doctor_name"].ToString();
                        QCDateTime = dataReader["qucai_datetime"].ToString();
                        zxResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetQuCaiInfo 执行语句：" + sqlstr);
            }
            return zxResult;
        }

        //查询制片相关信息
        public Boolean GetFilmInfo(ref string doctorname, ref string zpDateTime, string study_no)
        {
            Boolean zxResult = false;
            string sqlstr = " select date_format(zhipian_datetime,'%Y-%m-%d %H:%i:%s') as zhipian_datetime,zhipian_doctor_name from exam_master where exam_status>=27 and study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                using (IDataReader dataReader = DBProcess._db.ExecuteReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        doctorname = dataReader["zhipian_doctor_name"].ToString();
                        zpDateTime = dataReader["zhipian_datetime"].ToString();
                        zxResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetFilmInfo 执行语句：" + sqlstr);
            }
            return zxResult;
        }

        //更新已经制片结束状态
        public int UpdateZP_Flag(string study_no, string zhipian_doctor_code, string zhipian_doctor_name)
        {
            int Result = 0;
            string sqlstr = "update exam_master set exam_status=30,zhipian_datetime=@zhipian_datetime,zhipian_doctor_code=@zhipian_doctor_code,zhipian_doctor_name=@zhipian_doctor_name where study_no=@study_no and exam_status>=27 and exam_status<30";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@zhipian_datetime", DbType.DateTime, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                DBProcess._db.AddInParameter(cmd, "@zhipian_doctor_code", DbType.String, zhipian_doctor_code);
                DBProcess._db.AddInParameter(cmd, "@zhipian_doctor_name", DbType.String, zhipian_doctor_name);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateZP_Flag 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
        //根据病理号更新检查状态
        public int UpdateExam_Status(string study_no, string statusValue)
        {
            int Result = 0;
            string sqlstr = "update exam_master set exam_status=@exam_status  where exam_status>1 and study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_status", DbType.String, statusValue);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateExam_Status 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
        //更新质控用总时间
        public int UpdateZkTime(string exam_no)
        {
            string sqlstr1 = "select TIMESTAMPDIFF(minute,received_datetime,now())  as total_sj from exam_master where exam_status>=55 and  exam_no='{0}'";
            string sqlstr2 = "update  exam_master set zk_time={0}  where exam_status>=55 and  exam_no='{1}'";
            int Zx_Result = 0;
            try
            {
                int TotalSj = GetOneValue(string.Format(sqlstr1, exam_no));
                ExcuteSql(string.Format(sqlstr2, TotalSj, exam_no));
                Zx_Result = TotalSj;
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateZkTime 执行语句异常.");
            }
            return Zx_Result;
        }

        //获取质控时间
        public int GetZkTime(string exam_no)
        {
            string sqlstr1 = "select TIMESTAMPDIFF(minute,received_datetime,now())  as total_sj from exam_master where exam_status>=55 and  exam_no='{0}'";
            int TotalSj = 0;
            try
            {
                TotalSj = GetOneValue(string.Format(sqlstr1, exam_no));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetZkTime 执行语句异常.");
            }
            return TotalSj;
        }
        //更新送检单位
        public int UpdateSubmitDw(string submit_unit, string study_no)
        {
            int Result = 0;
            string sqlstr = "update exam_master set submit_unit=@submit_unit where exam_status>1 and study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@submit_unit", DbType.String, submit_unit);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateSubmitDw 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        //更新报告内容和取材标记
        public int UpdateMasBgnrFlag(string bgnr_gs_flag, string bbqc_info, string study_no)
        {
            int Result = 0;
            string sqlstr = "update exam_master set bgnr_gs_flag=@bgnr_gs_flag,bbqc_info=@bbqc_info where exam_status>1 and study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@bgnr_gs_flag", DbType.String, bgnr_gs_flag);
                DBProcess._db.AddInParameter(cmd, "@bbqc_info", DbType.String, bbqc_info);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateMasBgnrFlag 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }

        public DataSet GetMasBgnrFlag(string study_no)
        {
            DataSet ds = null;
            string sqlstr = "select bgnr_gs_flag,bbqc_info from  exam_master where exam_status>1 and study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                ds = DBProcess._db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetMasBgnrFlag 执行语句：" + sqlstr);
                ds = null;
            }
            return ds;
        }

        //保存报告
        public int UpdateSaveReport_Status(string study_no, string statusValue, string cbreport_doc_code)
        {
            int Result = 0;
            string sqlstr = "update exam_master set exam_status=@exam_status,cbreport_doc_code=@cbreport_doc_code  where exam_status>1 and study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_status", DbType.String, statusValue);
                DBProcess._db.AddInParameter(cmd, "@cbreport_doc_code", DbType.String, cbreport_doc_code);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateSaveReport_Status 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
        //  
        public DataTable GetData(string study_no)
        {
            string sqlstr = "select  modality, study_no,exam_no,patient_name,patient_id from exam_master_view where exam_status>0 and study_no=@study_no ";
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

        //病人基本信息
        public DataTable GetPatientInfo(string study_no)
        {
            string sqlstr = "select patient_name,sex,age,received_datetime,req_dept,input_id,bed_no,patient_source,submit_unit  from  exam_master_view where study_no=@study_no ";
            DataTable dt = null;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetPatientInfo 执行语句异常：" + sqlstr);
            }
            return dt;
        }
        //更新延时原因到主表
        public int UpdateDelay_reason(string study_no, string delay_reason)
        {
            int Result = 0;
            string sqlstr = "update exam_master set delay_reason=@delay_reason  where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@delay_reason", DbType.String, delay_reason);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateDelay_reason 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
        //更新送检部位
        public int UpdateParts(string study_no, string parts)
        {
            int Result = 0;
            string sqlstr = "update exam_master set parts=@parts  where exam_status>15 and study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@parts", DbType.String, parts);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Result = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateParts 执行语句：" + sqlstr);
                Result = 0;
            }
            return Result;
        }
        //获取送检部位
        public DataTable SelectParts(string study_no)
        {
            DataTable dt = null;
            string sqlstr = "select parts from exam_master  where exam_status>15 and study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "SelectParts 执行语句：" + sqlstr);
            }
            return dt;
        }
        //病理号查询当前检查状态
        public Boolean GetCurStudyno_Status(string study_no, ref string status_code, ref int lock_flag, ref string lock_doc_name, ref string lock_doc_code)
        {
            Boolean zxResult = false;
            string sqlstr = " select exam_status,lock_flag,lock_doc_name,lock_doc_code from exam_master where exam_status>1 and study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                using (IDataReader dataReader = DBProcess._db.ExecuteReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        status_code = dataReader["exam_status"].ToString();
                        lock_flag = Convert.ToInt16(dataReader["lock_flag"]);
                        lock_doc_name = dataReader["lock_doc_name"].ToString();
                        lock_doc_code = dataReader["lock_doc_code"].ToString();
                        zxResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetCurStudyno_Status 执行语句：" + sqlstr);
            }
            return zxResult;
        }
        //申请单号查询当前检查状态
        public Boolean GetCurExamno_Status(string exam_no, ref string status_code, ref int lock_flag, ref string lock_doc_name, ref string lock_doc_code, ref int wtzd_flag)
        {
            Boolean zxResult = false;
            string sqlstr = " select exam_status,lock_flag,lock_doc_name,lock_doc_code,wtzd_flag from exam_master where exam_status>1 and exam_no=@exam_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                using (IDataReader dataReader = DBProcess._db.ExecuteReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        status_code = dataReader["exam_status"].ToString();
                        lock_flag = Convert.ToInt16(dataReader["lock_flag"]);
                        lock_doc_name = dataReader["lock_doc_name"].ToString();
                        lock_doc_code = dataReader["lock_doc_code"].ToString();
                        wtzd_flag = Convert.ToInt16(dataReader["wtzd_flag"]);
                        zxResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetCurExamno_Status 执行语句：" + sqlstr);
            }
            return zxResult;
        }

        //锁定报告
        public Boolean LockReport(string study_no, string lock_doc_name, string lock_doc_code)
        {

            Boolean Result = false;
            string sqlstr = "update exam_master set lock_flag=@lock_flag,lock_doc_name=@lock_doc_name,lock_doc_code=@lock_doc_code where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@lock_flag", DbType.Int16, 1);
                DBProcess._db.AddInParameter(cmd, "@lock_doc_name", DbType.String, lock_doc_name);
                DBProcess._db.AddInParameter(cmd, "@lock_doc_code", DbType.String, lock_doc_code);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "LockReport 执行语句：" + sqlstr);
                Result = false;
            }
            return Result;

        }


        //解锁报告
        public Boolean unLockReport(string study_no, string lock_doc_name, string lock_doc_code)
        {

            Boolean Result = false;
            string sqlstr = "update exam_master set lock_flag=@lock_flag,lock_doc_name=@lock_doc_name,lock_doc_code=@lock_doc_code where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@lock_flag", DbType.Int16, 0);
                DBProcess._db.AddInParameter(cmd, "@lock_doc_name", DbType.String, lock_doc_name);
                DBProcess._db.AddInParameter(cmd, "@lock_doc_code", DbType.String, lock_doc_code);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                if (DBProcess._db.ExecuteNonQuery(cmd) == 1)
                {
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "unLockReport 执行语句：" + sqlstr);
                Result = false;
            }
            return Result;
        }
        //病理登记薄
        public DataTable GetDjbInfo(string tj)
        {
            DataTable dt = null;
            string sqlstr = "select exam_no,study_no,modality_cn,costs,patient_name,sex,age,patient_id,patient_source,req_dept,req_physician,submit_unit,req_date_time,received_datetime,output_id,input_id,ward,bed_no,status_name,si_card,hospital_card,zdyj,rysj,cbreport_doc_name,cbreport_datetime,zdpz,report_print_datetime,sfyx,lcfh,new_flag,exam_type,ice_flag from djb_view where " + tj + " order by study_no asc";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                dt = DBProcess._db.ExecuteDataSet(cmd).Tables[0];

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDjbInfo 执行语句：" + sqlstr);
            }
            return dt;
        }


        //病理号是否存在
        public int GetStudyNoCount(string study_no)
        {
            string sqlstr = "select count(*) as sl from  exam_master where study_no=@study_no and exam_status>1 ";
            int count = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                count = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetStudyNoCount 执行语句异常：" + sqlstr);
            }
            return count;
        }
        //更新病理号
        public int UpdateStudyNo(string study_no, string exam_no)
        {
            string sqlstr = "update  exam_master set  study_no=@study_no where exam_no=@exam_no ";
            int count = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                count = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateStudyNo 执行语句异常：" + sqlstr);
            }
            return count;
        }
        //更新检查类型
        public int UpdateModalityType(string modality, string exam_type, string exam_no)
        {
            string sqlstr = "update  exam_master set  modality=@modality,exam_type=@exam_type where exam_no=@exam_no ";
            int count = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@modality", DbType.String, modality);
                DBProcess._db.AddInParameter(cmd, "@exam_type", DbType.String, exam_type);
                DBProcess._db.AddInParameter(cmd, "@exam_no", DbType.String, exam_no);
                count = DBProcess._db.ExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateModalityType 执行语句异常：" + sqlstr);
            }
            return count;
        }
        //统计冰冻
        public DataTable GetBdtj(string tj)
        {
            string sqlstr = "select ice_flag,count(ice_flag) as sl from exam_master where exam_status>=20 and exam_type='PL' and " + tj + "  group by ice_flag";
            DataTable dt = null;
            try
            {
                dt = DBProcess._db.ExecuteDataSet(CommandType.Text, sqlstr).Tables[0];
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetBdtj 执行语句：" + sqlstr);
                dt = null;
            }
            return dt;
        }
        //获取登记条码打印状态
        public int GetDjPrintFlag(string study_no)
        {
            int Zx_Result = 0;
            string sqlstr = "select dj_print_flag from exam_master where study_no=@study_no and exam_status>=10";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Zx_Result = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetDjPrintFlag 执行语句：" + sqlstr);
            }
            return Zx_Result;
        }
        //更新登记条码打印状态
        public int UpdateDjPrintFlag(string study_no)
        {
            int Zx_Result = 0;
            string sqlstr = "update exam_master set dj_print_flag=1   where study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Zx_Result = DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateDjPrintFlag 执行语句：" + sqlstr);
            }
            return Zx_Result;
        }
        //更新蜡块上脱水机时间 
        public int UpdateLkTs_DateTime(string study_no)
        {
            int Zx_Result = 0;
            string sqlstr = "update exam_master set tuoshui_datetime=now() where exam_status>1 and study_no=@study_no";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                Zx_Result = DBProcess._db.ExecuteNonQuery(cmd);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "UpdateLkTs_DateTime 执行语句：" + sqlstr);
            }
            return Zx_Result;
        }

        //更新胃镜六点标记
        public int Updatewj_liud(string study_no, int wj_liud)
        {
            int result = 0;
            string sqlstr = "update exam_master set wj_liud=@wj_liud where  study_no=@study_no and exam_status>=20";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@wj_liud", DbType.Int16, wj_liud);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                result = DBProcess._db.ExecuteNonQuery(cmd);

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Updatewj_liud 执行语句：" + sqlstr);
                result = 0;
            }
            return result;
        }
        //获取胃镜六点标记
        public int Getwj_liud(string study_no)
        {
            int result = 0;
            string sqlstr = "select count(*) from exam_master  where wj_liud=1 and   study_no=@study_no and exam_status>=20";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                DBProcess._db.AddInParameter(cmd, "@study_no", DbType.String, study_no);
                result = Convert.ToInt16(DBProcess._db.ExecuteScalar(cmd));

            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "Getwj_liud 执行语句：" + sqlstr);
                result = 0;
            }
            return result;
        }
        public string GetOneStrValue(string sqlstr)
        {
            string totalSj = "";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                totalSj = Convert.ToString(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetOneValue 执行语句异常：" + sqlstr);
            }
            return totalSj;
        }

        public int GetOneValue(string sqlstr)
        {
            int totalSj = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                totalSj = Convert.ToInt32(DBProcess._db.ExecuteScalar(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetOneValue 执行语句异常：" + sqlstr);
                totalSj = 0;
            }
            return totalSj;
        }

        public int ExcuteSql(string sqlstr)
        {
            int ret = 0;
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                ret = Convert.ToInt32(DBProcess._db.ExecuteNonQuery(cmd));
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "ExcuteSql 执行语句异常：" + sqlstr);
            }
            return ret;
        }

    }
}
