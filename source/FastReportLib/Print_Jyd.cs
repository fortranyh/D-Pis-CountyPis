using System;

namespace FastReportLib
{
    public class Print_Jyd
    {
        public string BLH { get; set; }
        public string name { get; set; }
        public string count { get; set; }
        public string byzd { get; set; }
        public string hzyy { get; set; }
        public string yj { get; set; }
        public string jcr { get; set; }
        public string qt { get; set; }
        public string bz { get; set; }
        public string datetime { get; set; }
    }

    public class Print_Jydjd
    {
        public static string APPdirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);

        //打印借阅登记单
        public static Boolean PrintJydjd(Print_Jyd ins, string Printer_Name)
        {
            if (System.IO.File.Exists(APPdirPath + @"\Report\PrintJyd.frx"))
            {
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                FastReport.Report report = new FastReport.Report();
                //导入设计好的报表
                report.Load(APPdirPath + @"\Report\PrintJyd.frx");
                FastReport.TextObject Text4 = report.FindObject("Text4") as FastReport.TextObject;
                Text4.Text = ins.BLH;

                FastReport.TextObject Text6 = report.FindObject("Text6") as FastReport.TextObject;
                Text6.Text = ins.name;

                FastReport.TextObject Text8 = report.FindObject("Text8") as FastReport.TextObject;
                Text8.Text = ins.jcr;

                FastReport.TextObject Text10 = report.FindObject("Text10") as FastReport.TextObject;
                Text10.Text = ins.count;

                FastReport.TextObject Text12 = report.FindObject("Text12") as FastReport.TextObject;
                Text12.Text = ins.yj;

                FastReport.TextObject Text14 = report.FindObject("Text14") as FastReport.TextObject;
                Text14.Text = ins.byzd;

                FastReport.TextObject Text20 = report.FindObject("Text20") as FastReport.TextObject;
                Text20.Text = ins.qt;

                FastReport.TextObject Text16 = report.FindObject("Text16") as FastReport.TextObject;
                Text16.Text = ins.hzyy;

                FastReport.TextObject Text18 = report.FindObject("Text18") as FastReport.TextObject;
                Text18.Text = ins.bz;

                FastReport.TextObject Text22 = report.FindObject("Text22") as FastReport.TextObject;
                Text22.Text = ins.datetime;

                FastReport.TextObject Text24 = report.FindObject("Text24") as FastReport.TextObject;
                Text24.Text = ins.BLH;

                FastReport.TextObject Text26 = report.FindObject("Text26") as FastReport.TextObject;
                Text26.Text = ins.name;

                FastReport.TextObject Text28 = report.FindObject("Text28") as FastReport.TextObject;
                Text28.Text = ins.jcr;

                FastReport.TextObject Text30 = report.FindObject("Text30") as FastReport.TextObject;
                Text30.Text = ins.count;

                FastReport.TextObject Text32 = report.FindObject("Text32") as FastReport.TextObject;
                Text32.Text = ins.yj;

                FastReport.TextObject Text34 = report.FindObject("Text34") as FastReport.TextObject;
                Text34.Text = ins.byzd;

                FastReport.TextObject Text40 = report.FindObject("Text40") as FastReport.TextObject;
                Text40.Text = ins.qt;

                FastReport.TextObject Text36 = report.FindObject("Text36") as FastReport.TextObject;
                Text36.Text = ins.hzyy;

                FastReport.TextObject Text38 = report.FindObject("Text38") as FastReport.TextObject;
                Text38.Text = ins.bz;

                FastReport.TextObject Text42 = report.FindObject("Text42") as FastReport.TextObject;
                Text42.Text = ins.datetime;

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
