using System;
using System.Data;

namespace FastReportLib
{
    public class iceReportParas
    {
        public string ReportParaHospital { get; set; }
        public string study_no { get; set; }
        public string Txt_Name { get; set; }
        public string Txt_Sex { get; set; }
        public string Txt_Age { get; set; }
        public string Txt_Jsrq { get; set; }
        public string Txt_report_Doctor { get; set; }
        public string Txt_Content { get; set; }
        public string Txt_BgDate { get; set; }
        public string req_dept { get; set; }
        public string zyh { get; set; }
        public string ch { get; set; }
        public string shys { get; set; }
        public string exam_no { get; set; }
    }

    public class IceReport
    {
        public static string APPdirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);

        //打印冰冻报告
        public static Boolean PrintIceReport(iceReportParas ins, string Printer_Name, int Print_Copys, string strFileName = "")
        {
            if (System.IO.File.Exists(APPdirPath + @"\Report\iceReport.frx"))
            {
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                FastReport.Report report = new FastReport.Report();
                //导入设计好的报表
                report.Load(APPdirPath + @"\Report\iceReport.frx");
                FastReport.TextObject Text1 = report.FindObject("Text1") as FastReport.TextObject;
                Text1.Text = ins.ReportParaHospital;

                FastReport.TextObject Text5 = report.FindObject("Text5") as FastReport.TextObject;
                Text5.Text = ins.study_no;

                FastReport.TextObject Text7 = report.FindObject("Text7") as FastReport.TextObject;
                Text7.Text = ins.Txt_Name;

                FastReport.TextObject Text13 = report.FindObject("Text13") as FastReport.TextObject;
                Text13.Text = ins.Txt_Sex;

                FastReport.TextObject Text15 = report.FindObject("Text15") as FastReport.TextObject;
                Text15.Text = ins.Txt_Age;

                DBHelper.BLL.exam_master insMas = new DBHelper.BLL.exam_master();
                DataTable dtMas = insMas.GetDt("select patient_source,submit_unit from exam_master  where exam_no='" + ins.exam_no + "'");
                if (dtMas.Rows.Count > 0 && dtMas.Rows[0]["patient_source"].ToString().Equals("外来"))
                {
                    FastReport.TextObject Textsjdw = report.FindObject("Text32") as FastReport.TextObject;
                    Textsjdw.Text = dtMas.Rows[0]["submit_unit"].ToString();
                    FastReport.TextObject Text31 = report.FindObject("Text31") as FastReport.TextObject;
                    Text31.Text = "送检单位:";
                }
                else
                {

                    FastReport.TextObject Text32 = report.FindObject("Text32") as FastReport.TextObject;
                    Text32.Text = ins.req_dept;
                }

                FastReport.TextObject Text9 = report.FindObject("Text9") as FastReport.TextObject;
                Text9.Text = ins.Txt_Jsrq;


                FastReport.TextObject Text22 = report.FindObject("Text22") as FastReport.TextObject;
                Text22.Text = ins.Txt_Content;

                FastReport.TextObject Text25 = report.FindObject("Text25") as FastReport.TextObject;
                Text25.Text = ins.Txt_report_Doctor;

                FastReport.TextObject Text29 = report.FindObject("Text29") as FastReport.TextObject;
                Text29.Text = ins.Txt_BgDate;

                FastReport.TextObject Text42 = report.FindObject("Text42") as FastReport.TextObject;
                Text42.Text = ins.shys;

                FastReport.TextObject Text40 = report.FindObject("Text40") as FastReport.TextObject;
                Text40.Text = ins.zyh;

                FastReport.TextObject Text36 = report.FindObject("Text36") as FastReport.TextObject;
                Text36.Text = ins.ch;

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

                if (!strFileName.Equals(""))
                {
                    FastReport.Export.Image.ImageExport insExp = new FastReport.Export.Image.ImageExport();
                    insExp.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
                    report.Export(insExp, @strFileName);
                }


                //释放资源
                report.Dispose();
                return true;
            }
            return false;
        }
    }
}
