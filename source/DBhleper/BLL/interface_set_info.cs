using System;
using System.Data;
using System.Data.Common;

namespace DBHelper.BLL
{
    public class interface_set_info
    {
        public string GetThirdUrlinfo(string type_str)
        {
            string sqlstr = "select url from thirdinterface_info where type_str='" + type_str + "'";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                using (IDataReader dataReader = DBProcess._db.ExecuteReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        return dataReader["url"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetThirdUrlinfo 执行语句：" + sqlstr);
            }
            return "";
        }


        public Model.interface_set_info GetInterfaceSetInfo()
        {
            string sqlstr = "select * from interface_set_info limit 1";
            try
            {
                DbCommand cmd = DBProcess._db.GetSqlStringCommand(sqlstr);
                using (IDataReader dataReader = DBProcess._db.ExecuteReader(cmd))
                {
                    if (dataReader.Read())
                    {
                        Model.interface_set_info insM = new Model.interface_set_info();
                        insM.tcp_ip = dataReader["tcp_ip"].ToString();
                        insM.tcp_port = Convert.ToInt32(dataReader["tcp_port"]);
                        insM.enable_flag = Convert.ToInt32(dataReader["enable_flag"]);
                        insM.receive_flag = Convert.ToInt32(dataReader["receive_flag"]);   //'标本接收标记',
                        insM.draw_flag = Convert.ToInt32(dataReader["draw_flag"]);   // '取材结束标记',
                        insM.baomai_flag = Convert.ToInt32(dataReader["baomai_flag"]);   // '包埋结束标记',
                        insM.slide_flag = Convert.ToInt32(dataReader["slide_flag"]);   // 制片结束标记',
                        insM.special_flag = Convert.ToInt32(dataReader["special_flag"]);   // '特检结束标记',
                        insM.init_report_flag = Convert.ToInt32(dataReader["init_report_flag"]);  //  '初步报告标记',
                        insM.verify_flag = Convert.ToInt32(dataReader["verify_flag"]);  // '审核报告标记',
                        insM.print_flag = Convert.ToInt32(dataReader["print_flag"]);  // '打印报告标记',
                        insM.appointment_flag = Convert.ToInt32(dataReader["appointment_flag"]);   // '预约标记',
                        insM.application_flag = Convert.ToInt32(dataReader["application_flag"]);  //  '申请标记',
                        insM.frozen_flag = Convert.ToInt32(dataReader["frozen_flag"]);  //  '冰冻标记',
                        insM.pis_cancel_flag = Convert.ToInt32(dataReader["pis_cancel_flag"]);   //  'pis作废标记',
                        insM.archives_flag = Convert.ToInt32(dataReader["archives_flag"]);   // '归档标记',
                        insM.his_cancel_flag = Convert.ToInt32(dataReader["his_cancel_flag"]);   //  'his作废标记',
                        insM.consultation_flag = Convert.ToInt32(dataReader["consultation_flag"]);   //  '会诊标记',
                        insM.consultation_finish_flag = Convert.ToInt32(dataReader["consultation_finish_flag"]);   //  '会诊结束标记',
                        insM.bhg_flag = Convert.ToInt32(dataReader["bhg_flag"]);   //  '不合格标本登记标记',
                        return insM;
                    }
                }
            }
            catch (Exception ex)
            {
                DBProcess.ShowException(ex, "GetInterfaceSetInfo 执行语句：" + sqlstr);
            }
            return null;
        }
    }
}
