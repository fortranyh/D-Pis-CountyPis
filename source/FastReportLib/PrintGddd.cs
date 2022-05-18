using System;
using System.Data;
using System.Text;

namespace FastReportLib
{

    public static class PrintGddd
    {
        //应用程序执行路径
        public static string APPdirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        //打印归档底单
        public static Boolean DirectPrintGddd(String study_no, String rysj, string blzd, string bgys, string bgsj, string Printer_Name, Boolean ChkPrintDtms)
        {
            if (System.IO.File.Exists(APPdirPath + @"\Report\GdddReportA4.frx"))
            {
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                FastReport.Report report = new FastReport.Report();
                //导入设计好的报表
                report.Load(APPdirPath + @"\Report\GdddReportA4.frx");
                //注册数据源
                DBHelper.BLL.exam_master Ins_Mas = new DBHelper.BLL.exam_master();
                DataSet Ds = Ins_Mas.GetDs("select meterial_no,parts,draw_doctor_name,bm_doc_name,date_format(draw_datetime,'%Y-%m-%d %H:%i:%s') as draw_datetime  from exam_draw_meterials where study_no='" + study_no + "'");
                string strlkinfo = "";
                string qcsj = "";
                string qcys = "";
                if (Ds != null)
                {
                    Ds.Tables[0].TableName = "exam_draw_meterials";
                    //注册数据源
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine();
                        for (int i = 0; i <= Ds.Tables[0].Rows.Count - 1; i++)
                        {
                            if (qcsj.Equals(""))
                            {
                                if (Ds.Tables[0].Rows[i]["draw_datetime"].ToString().Length >= 10)
                                {
                                    qcsj = Ds.Tables[0].Rows[i]["draw_datetime"].ToString().Substring(0, 10);
                                    qcys = Ds.Tables[0].Rows[i]["draw_doctor_name"].ToString();
                                }
                            }
                            sb.AppendFormat("({0}),{1} || ", Ds.Tables[0].Rows[i]["meterial_no"].ToString(), Ds.Tables[0].Rows[i]["parts"].ToString());
                        }
                        sb.AppendLine();
                        //sb.AppendFormat("病理编号:{0} 取材医师:{1} 录入人:{2} 蜡块总数:{3} ", study_no, qcys, lrr, Ds.Tables[0].Rows.Count.ToString());
                        sb.AppendLine();
                        strlkinfo = sb.ToString();
                        sb.Clear();
                    }
                }
                //
                if (!ChkPrintDtms)
                {
                    //
                    FastReport.TextObject Txtstudy_no = report.FindObject("Text1") as FastReport.TextObject;
                    Txtstudy_no.Text = study_no;
                    //
                    FastReport.TextObject TxtQcsj = report.FindObject("TxtQcsj") as FastReport.TextObject;
                    TxtQcsj.Text = qcsj;
                    //
                    FastReport.TextObject TxtQcys = report.FindObject("TxtQcys") as FastReport.TextObject;
                    TxtQcys.Text = qcys;
                    //
                    FastReport.TextObject Txt_Rysj = report.FindObject("Txt_Rysj") as FastReport.TextObject;
                    Txt_Rysj.Text = rysj + strlkinfo;
                }
                else
                {
                    FastReport.TextObject Text30 = report.FindObject("Text30") as FastReport.TextObject;
                    Text30.Visible = false;
                    FastReport.TextObject Text31 = report.FindObject("Text31") as FastReport.TextObject;
                    Text31.Visible = false;
                    FastReport.TextObject Text33 = report.FindObject("Text33") as FastReport.TextObject;
                    Text33.Visible = false;
                }
                //
                FastReport.TextObject Txt_Blzd = report.FindObject("Txt_Blzd") as FastReport.TextObject;
                Txt_Blzd.Text = blzd;
                //
                FastReport.TextObject txt_Bgrq = report.FindObject("txt_Bgrq") as FastReport.TextObject;
                txt_Bgrq.Text = bgsj;
                //
                FastReport.TextObject Txt_Bgys = report.FindObject("Txt_Bgys") as FastReport.TextObject;
                Txt_Bgys.Text = bgys;
                //设置默认打印机
                if (Printer_Name != "")
                {
                    report.PrintSettings.Printer = Printer_Name;
                }
                //打印份数
                report.PrintSettings.Copies = 1;
                //不弹出打印设置框
                report.PrintSettings.ShowDialog = false;
                report.Print();
                //释放资源
                report.Dispose();
                return true;
            }
            return false;
        }

        //打印取材归档底单
        public static Boolean DirectPrintQcGddd(String study_no, String rysj, string qcys, string lrr, string Printer_Name)
        {
            if (System.IO.File.Exists(APPdirPath + @"\Report\qcgdReportA4.frx"))
            {
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                FastReport.Report report = new FastReport.Report();
                //导入设计好的报表
                report.Load(APPdirPath + @"\Report\qcgdReportA4.frx");
                //注册数据源
                DBHelper.BLL.exam_master Ins_Mas = new DBHelper.BLL.exam_master();
                DataSet Ds = Ins_Mas.GetDs("select meterial_no,parts,draw_doctor_name,bm_doc_name,date_format(draw_datetime,'%Y-%m-%d %H:%i:%s') as draw_datetime  from exam_draw_meterials where study_no='" + study_no + "'");
                string strlkinfo = "";
                string qcsj = "";
                if (Ds != null)
                {
                    Ds.Tables[0].TableName = "exam_draw_meterials";
                    //注册数据源
                    if (Ds.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine();
                        for (int i = 0; i <= Ds.Tables[0].Rows.Count - 1; i++)
                        {
                            if (qcsj.Equals(""))
                            {
                                if (Ds.Tables[0].Rows[i]["draw_datetime"].ToString().Length >= 10)
                                {
                                    qcsj = Ds.Tables[0].Rows[i]["draw_datetime"].ToString().Substring(0, 10);
                                }
                            }
                            sb.AppendFormat("({0}),{1} || ", Ds.Tables[0].Rows[i]["meterial_no"].ToString(), Ds.Tables[0].Rows[i]["parts"].ToString());
                        }
                        sb.AppendLine();
                        //sb.AppendFormat("病理编号:{0} 取材医师:{1} 录入人:{2} 蜡块总数:{3} ", study_no, qcys, lrr, Ds.Tables[0].Rows.Count.ToString());
                        sb.AppendLine();
                        strlkinfo = sb.ToString();
                        sb.Clear();
                    }
                }
                //
                FastReport.TextObject Txtstudy_no = report.FindObject("Text1") as FastReport.TextObject;
                Txtstudy_no.Text = study_no;
                //
                FastReport.TextObject Txt_Rysj = report.FindObject("Txt_Rysj") as FastReport.TextObject;
                Txt_Rysj.Text = rysj + strlkinfo;
                //
                FastReport.TextObject TxtQcys = report.FindObject("TxtQcys") as FastReport.TextObject;
                TxtQcys.Text = qcys;
                //
                FastReport.TextObject TxtQcsj = report.FindObject("TxtQcsj") as FastReport.TextObject;
                TxtQcsj.Text = qcsj;
                //设置默认打印机
                if (Printer_Name != "")
                {
                    report.PrintSettings.Printer = Printer_Name;
                }
                //打印份数
                report.PrintSettings.Copies = 1;
                //不弹出打印设置框
                report.PrintSettings.ShowDialog = false;
                report.Print();
                //释放资源
                report.Dispose();
                return true;
            }
            return false;
        }




    }
}
