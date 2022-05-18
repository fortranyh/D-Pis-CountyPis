using FastReport.Barcode;
using System;
namespace FastReportLib
{
    public class PrintBarCode
    {
        //应用程序执行路径
        public static string APPdirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        //打印病理号条码
        public static void PrintBarcode(String BLH, String Pat_Name, string Printer_Name, int Print_Copys)
        {
            if (System.IO.File.Exists(APPdirPath + @"\Report\DjBarcode.frx"))
            {
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                FastReport.Report report = new FastReport.Report();
                //导入设计好的报表
                report.Load(APPdirPath + @"\Report\DjBarcode.frx");
                //条码赋值
                BarcodeObject BarCodeIns = report.FindObject("Barcode1") as FastReport.Barcode.BarcodeObject;
                BarCodeIns.Text = BLH;
                BarcodeObject BarCodeIns2 = report.FindObject("Barcode2") as FastReport.Barcode.BarcodeObject;
                BarCodeIns2.Text = BLH;
                FastReport.TextObject s5 = report.FindObject("Text5") as FastReport.TextObject;
                FastReport.TextObject s6 = report.FindObject("Text6") as FastReport.TextObject;
                s5.Text = BLH;
                s6.Text = BLH;
                //设置参数
                report.SetParameterValue("Pat_Name", Pat_Name);
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
            }
        }
        //单联打印玻片号条码
        public static void PrintQPBarcode(String Ckh1, String Pkh1, string bjw1, string hospi1, string Printer_Name, int Print_Copys)
        {
            if (System.IO.File.Exists(APPdirPath + @"\Report\QPBarcode.frx"))
            {
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                FastReport.Report report = new FastReport.Report();
                //导入设计好的报表
                report.Load(APPdirPath + @"\Report\QPBarcode.frx");
                //条码赋值
                if (Pkh1.Length > 0)
                {
                    BarcodeObject BarCodeIns = report.FindObject("Barcode1") as FastReport.Barcode.BarcodeObject;
                    BarCodeIns.Text = Pkh1;
                    FastReport.TextObject Text9 = report.FindObject("Text9") as FastReport.TextObject;
                    Text9.Text = Pkh1.Substring(Pkh1.Length - 2, 2);
                }

                string[] Ckh1s = Ckh1.Split('-');
                if (Ckh1s.Length == 2)
                {
                    FastReport.TextObject Text1 = report.FindObject("Text1") as FastReport.TextObject;
                    Text1.Text = Ckh1s[0];
                    //
                    FastReport.TextObject Text7 = report.FindObject("Text7") as FastReport.TextObject;
                    Text7.Text = Ckh1s[1];
                }
                else
                {
                    FastReport.TextObject Text1 = report.FindObject("Text1") as FastReport.TextObject;
                    Text1.Text = Ckh1s[0];
                    //
                    FastReport.TextObject Text7 = report.FindObject("Text7") as FastReport.TextObject;
                    Text7.Text = "";
                }
                //标记物或者标本部位
                FastReport.TextObject Text5 = report.FindObject("Text5") as FastReport.TextObject;
                Text5.Text = bjw1;
                //医院名称
                FastReport.TextObject Text3 = report.FindObject("Text3") as FastReport.TextObject;
                Text3.Text = hospi1;

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
            }
        }
        //双联打印玻片号条码
        public static void PrintQPBarcode2(String Ckh1, String Pkh1, string bjw1, string hospi1, String Ckh2, String Pkh2, string bjw2, string hospi2, string Printer_Name, int Print_Copys)
        {
            if (System.IO.File.Exists(APPdirPath + @"\Report\QPBarcode2.frx"))
            {
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                FastReport.Report report = new FastReport.Report();
                //导入设计好的报表
                report.Load(APPdirPath + @"\Report\QPBarcode2.frx");



                //标记物或者标本部位
                FastReport.TextObject Text5 = report.FindObject("Text5") as FastReport.TextObject;
                Text5.Text = bjw2;
                FastReport.TextObject Text6 = report.FindObject("Text6") as FastReport.TextObject;
                Text6.Text = bjw1;
                //条码赋值
                if (Pkh2.Length > 0)
                {
                    BarcodeObject BarCodeIns = report.FindObject("Barcode1") as FastReport.Barcode.BarcodeObject;
                    BarCodeIns.Text = Pkh2;
                    FastReport.TextObject Text9 = report.FindObject("Text9") as FastReport.TextObject;
                    Text9.Text = Pkh2.Substring(Pkh2.Length - 2, 2);
                }
                if (Pkh1.Length > 0)
                {
                    BarcodeObject BarCodeIns2 = report.FindObject("Barcode2") as FastReport.Barcode.BarcodeObject;
                    BarCodeIns2.Text = Pkh1;
                    FastReport.TextObject Text10 = report.FindObject("Text10") as FastReport.TextObject;
                    Text10.Text = Pkh1.Substring(Pkh1.Length - 2, 2);
                }

                string[] Ckh1s = Ckh1.Split('-');
                if (Ckh1s.Length == 2)
                {
                    FastReport.TextObject Text2 = report.FindObject("Text2") as FastReport.TextObject;
                    Text2.Text = Ckh1s[0];
                    //
                    FastReport.TextObject Text8 = report.FindObject("Text8") as FastReport.TextObject;
                    Text8.Text = Ckh1s[1];
                }
                else
                {
                    FastReport.TextObject Text2 = report.FindObject("Text2") as FastReport.TextObject;
                    Text2.Text = Ckh1s[0];
                    //
                    FastReport.TextObject Text8 = report.FindObject("Text8") as FastReport.TextObject;
                    Text8.Text = "";
                }
                string[] Ckh2s = Ckh2.Split('-');
                if (Ckh2s.Length == 2)
                {
                    //蜡块号
                    FastReport.TextObject Text1 = report.FindObject("Text1") as FastReport.TextObject;
                    Text1.Text = Ckh2s[0];
                    FastReport.TextObject Text7 = report.FindObject("Text7") as FastReport.TextObject;
                    Text7.Text = Ckh2s[1];
                }
                else
                {
                    //蜡块号
                    FastReport.TextObject Text1 = report.FindObject("Text1") as FastReport.TextObject;
                    Text1.Text = Ckh2s[0];
                    FastReport.TextObject Text7 = report.FindObject("Text7") as FastReport.TextObject;
                    Text7.Text = "";
                }


                //医院名称
                FastReport.TextObject Text3 = report.FindObject("Text3") as FastReport.TextObject;
                Text3.Text = hospi2;
                FastReport.TextObject Text4 = report.FindObject("Text4") as FastReport.TextObject;
                Text4.Text = hospi1;
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
                //FastReport.Export.Image.ImageExport insExp = new FastReport.Export.Image.ImageExport();
                //insExp.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
                //report.Export(insExp, @"C:\bpbarcode.jpg");
                //释放资源
                report.Dispose();
            }
        }
    }
}
