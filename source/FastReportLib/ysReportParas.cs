using System;

namespace FastReportLib
{
    public class ysReportParas
    {
        public string ReportParaHospital { get; set; }
        public string study_no { get; set; }
        public string Txt_Name { get; set; }
        public string Txt_Sex { get; set; }
        public string Txt_Age { get; set; }
        public string Txt_Jsrq { get; set; }
        public string Txt_report_Doctor { get; set; }
        public string Txt_shreport_Doctor { get; set; }
        public string Txt_Content { get; set; }
        public string Txt_BgDate { get; set; }
        public string req_dept { get; set; }
        public string patient_source { get; set; }
        public string exam_no { get; set; }
    }

    public class myzhReportParas
    {
        public string ReportParaHospital { get; set; }
        public string study_no { get; set; }
        public string Txt_Name { get; set; }
        public string Txt_Sex { get; set; }
        public string Txt_Age { get; set; }
        public string Txt_Jsrq { get; set; }
        public string Txt_report_Doctor { get; set; }
        public string Txt_shreport_Doctor { get; set; }
        public string Txt_Content { get; set; }
        public string Txt_BgDate { get; set; }
        public string req_dept { get; set; }
        public string patient_source { get; set; }
        public string exam_no { get; set; }
        public string lczd { get; set; }
        public string blzd { get; set; }
        public string rs_func { get; set; }
        public string zh_md { get; set; }
        public string submit_unit { get; set; }
        public string input_id { get; set; }
        public string bed_no { get; set; }
    }



    public class DelayReport
    {
        public static string APPdirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);

        //打印延时报告
        public static Boolean PrintDelayReport(ysReportParas ins, string Printer_Name, int Print_Copys, int reportType = 0)
        {
            if (reportType == 0)
            {
                if (System.IO.File.Exists(APPdirPath + @"\Report\ysReport.frx"))
                {
                    FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                    FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                    FastReport.Report report = new FastReport.Report();
                    //导入设计好的报表
                    report.Load(APPdirPath + @"\Report\ysReport.frx");
                    FastReport.TextObject Text1 = report.FindObject("Text1") as FastReport.TextObject;
                    Text1.Text = ins.ReportParaHospital;

                    FastReport.TextObject Text4 = report.FindObject("Text4") as FastReport.TextObject;
                    Text4.Text = ins.patient_source;

                    FastReport.TextObject Text5 = report.FindObject("Text5") as FastReport.TextObject;
                    Text5.Text = ins.study_no;

                    FastReport.TextObject Text7 = report.FindObject("Text7") as FastReport.TextObject;
                    Text7.Text = ins.Txt_Name;

                    FastReport.TextObject Text13 = report.FindObject("Text13") as FastReport.TextObject;
                    Text13.Text = ins.Txt_Sex;

                    FastReport.TextObject Text15 = report.FindObject("Text15") as FastReport.TextObject;
                    Text15.Text = ins.Txt_Age;

                    FastReport.TextObject Text32 = report.FindObject("Text32") as FastReport.TextObject;
                    Text32.Text = ins.req_dept;

                    FastReport.TextObject Text9 = report.FindObject("Text9") as FastReport.TextObject;
                    Text9.Text = ins.Txt_Jsrq;


                    FastReport.TextObject Text22 = report.FindObject("Text22") as FastReport.TextObject;
                    Text22.Text = ins.Txt_Content;

                    FastReport.TextObject Text25 = report.FindObject("Text25") as FastReport.TextObject;
                    Text25.Text = ins.Txt_report_Doctor;

                    FastReport.TextObject Text29 = report.FindObject("Text29") as FastReport.TextObject;
                    Text29.Text = ins.Txt_BgDate;

                    //设置默认打印机
                    if (Printer_Name != "")
                    {
                        report.PrintSettings.Printer = Printer_Name;
                    }
                    //打印份数
                    report.PrintSettings.Copies = Print_Copys;
                    //不弹出打印设置框
                    report.PrintSettings.ShowDialog = false;
                    report.Print();
                    //释放资源
                    report.Dispose();
                    return true;
                }
            }
            else if (reportType == 1)
            {

                if (System.IO.File.Exists(APPdirPath + @"\Report\BCreport.frx"))
                {
                    FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                    FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                    FastReport.Report report = new FastReport.Report();
                    //导入设计好的报表
                    report.Load(APPdirPath + @"\Report\BCreport.frx");
                    FastReport.TextObject Text1 = report.FindObject("Text1") as FastReport.TextObject;
                    Text1.Text = ins.ReportParaHospital;

                    FastReport.TextObject Text4 = report.FindObject("Text4") as FastReport.TextObject;
                    Text4.Text = ins.patient_source;

                    FastReport.TextObject Text5 = report.FindObject("Text5") as FastReport.TextObject;
                    Text5.Text = ins.study_no;

                    FastReport.TextObject Text7 = report.FindObject("Text7") as FastReport.TextObject;
                    Text7.Text = ins.Txt_Name;

                    FastReport.TextObject Text13 = report.FindObject("Text13") as FastReport.TextObject;
                    Text13.Text = ins.Txt_Sex;

                    FastReport.TextObject Text15 = report.FindObject("Text15") as FastReport.TextObject;
                    Text15.Text = ins.Txt_Age;

                    FastReport.TextObject Text32 = report.FindObject("Text32") as FastReport.TextObject;
                    Text32.Text = ins.req_dept;

                    FastReport.TextObject Text9 = report.FindObject("Text9") as FastReport.TextObject;
                    Text9.Text = ins.Txt_Jsrq;


                    FastReport.TextObject Text22 = report.FindObject("Text22") as FastReport.TextObject;
                    Text22.Text = ins.Txt_Content;

                    FastReport.TextObject Text25 = report.FindObject("Text25") as FastReport.TextObject;
                    Text25.Text = ins.Txt_report_Doctor;

                    FastReport.TextObject Text29 = report.FindObject("Text29") as FastReport.TextObject;
                    Text29.Text = ins.Txt_BgDate;

                    //设置默认打印机
                    if (Printer_Name != "")
                    {
                        report.PrintSettings.Printer = Printer_Name;
                    }
                    //打印份数
                    report.PrintSettings.Copies = Print_Copys;
                    //不弹出打印设置框
                    report.PrintSettings.ShowDialog = false;
                    report.Print();
                    //释放资源
                    report.Dispose();
                    return true;
                }
            }
            return false;
        }

    }
}
