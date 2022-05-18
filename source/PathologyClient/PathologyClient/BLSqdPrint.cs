using System;
using System.Collections.Generic;
using System.Text;
using FastReport.Barcode;

namespace PathologyClient
{
   public class BLSqdPrint
    {
        //打印病理申请单
       public static Boolean PrintBlSQD(EntityModel.Exam_BlSqd ins, string Printer_Name, int Print_Copys)
        {
            try
            {
                if (System.IO.File.Exists(Program.APPdirPath + @"\Report\BLSQDA4.frx"))
                {
                    FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                    FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                    FastReport.Report report = new FastReport.Report();
                    //导入设计好的报表
                    report.Load(Program.APPdirPath + @"\Report\BLSQDA4.frx");

                    FastReport.TextObject hospital_name = report.FindObject("Text34") as FastReport.TextObject;
                    hospital_name.Text = ins.hospital_name ?? "";

                    FastReport.Table.TableCell Txt_Name = report.FindObject("Cell2") as FastReport.Table.TableCell;
                    Txt_Name.Text = ins.pat_name ?? "";

                    FastReport.Table.TableCell Txt_Sex = report.FindObject("Cell4") as FastReport.Table.TableCell;
                    Txt_Sex.Text = ins.sex ?? "";

                    FastReport.Table.TableCell Txt_Age = report.FindObject("Cell26") as FastReport.Table.TableCell;
                    Txt_Age.Text = ins.age ?? "";

                    FastReport.Table.TableCell Txt_Phone = report.FindObject("Cell28") as FastReport.Table.TableCell;
                    Txt_Phone.Text = ins.pho ?? "";

                    FastReport.Table.TableCell Text36 = report.FindObject("Cell7") as FastReport.Table.TableCell;
                    Text36.Text = ins.sqks ?? "";

                    FastReport.Table.TableCell Txt_Jsrq = report.FindObject("Cell30") as FastReport.Table.TableCell;
                    Txt_Jsrq.Text = ins.sqys ?? "";

                    FastReport.Table.TableCell Txt_Sjbw = report.FindObject("Cell40") as FastReport.Table.TableCell;
                    Txt_Sjbw.Text = ins.parts ?? "";

                    //条码赋值
                    BarcodeObject BarCodeIns = report.FindObject("Barcode1") as FastReport.Barcode.BarcodeObject;
                    BarCodeIns.Text = ins.exam_no ?? "";

                    FastReport.Table.TableCell Txt_Lczd = report.FindObject("Cell12") as FastReport.Table.TableCell;
                    Txt_Lczd.Text = ins.lczd ?? "";

                    FastReport.Table.TableCell Text47 = report.FindObject("Cell59") as FastReport.Table.TableCell;
                    Text47.Text = ins.bs ?? "";

                    FastReport.Table.TableCell Cell67 = report.FindObject("Cell67") as FastReport.Table.TableCell;
                    Cell67.Text = ins.szsj ?? "";

                    FastReport.Table.TableCell Text50 = report.FindObject("Cell75") as FastReport.Table.TableCell;
                    Text50.Text = ins.rysj ?? "";

                    FastReport.Table.TableCell Text52 = report.FindObject("Cell98") as FastReport.Table.TableCell;
                    Text52.Text = ins.sqrq.ToString().Substring(0, 10);

                    FastReport.Table.TableCell Text54 = report.FindObject("Cell90") as FastReport.Table.TableCell;
                    Text54.Text = ins.sqys ?? "";

                    //设置默认打印机
                    if (Printer_Name != "")
                    {
                        report.PrintSettings.Printer = Printer_Name;
                    }
                    //打印份数
                    report.PrintSettings.Copies = Print_Copys;
                    //不弹出打印设置框
                    report.PrintSettings.ShowDialog = false;
                    report.Prepare();
                    report.Print();
                    return true;
                }
            }
            catch
            {

            }
            return false;
        }
    }
}
