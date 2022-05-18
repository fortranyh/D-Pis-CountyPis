using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace FastReportLib
{


    public partial class ReportViewer : UserControl
    {
        public static string ReplaceChiEng(string text)
        {
            const string s1 = "：；，？！";
            const string s2 = @":;,?!";
            char[] c = text.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                int n = s1.IndexOf(c[i]);
                if (n != -1) c[i] = s2[n];
            }
            return new string(c);
        }
        //应用程序执行路径
        public static string APPdirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        public ReportViewer()
        {
            if (System.IO.File.Exists(APPdirPath + @"\Chinese (Simplified).frl"))
            {
                FastReport.Utils.Res.LoadLocale(APPdirPath + @"\Chinese (Simplified).frl");
            }
            InitializeComponent();
            this.previewControl1.Clear();
            this.previewControl1.UIStyle = FastReport.Utils.UIStyle.Office2007Blue;
            //HPV指标初始化
            hpvInit();
        }
        //HPV指标初始化
        List<string> lstgw = new List<string>();
        List<string> lstdw = new List<string>();
        public void hpvInit()
        {
            //高
            lstgw.Add("16");
            lstgw.Add("18");
            lstgw.Add("31");
            lstgw.Add("33");
            lstgw.Add("35");
            lstgw.Add("39");
            lstgw.Add("45");
            lstgw.Add("51");
            lstgw.Add("52");
            lstgw.Add("53");
            lstgw.Add("56");
            lstgw.Add("58");
            lstgw.Add("59");
            lstgw.Add("66");
            lstgw.Add("68");
            //低
            lstdw.Add("6");
            lstdw.Add("11");
            lstdw.Add("42");
            lstdw.Add("43");
            lstdw.Add("44");
            lstdw.Add("81(CP8304)");
        }
        public void setToolBarVisible(Boolean viflag)
        {
            this.previewControl1.RefreshReport();
            this.previewControl1.ToolbarVisible = viflag;
        }
        //编辑
        public Boolean EditReport()
        {
            if (previewControl1.Report != null)
            {
                previewControl1.EditPage();
                return true;
            }
            else
            {
                return false;
            }
        }

        //报告预览时直接打印
        public Boolean PrintReport(string expFilePath)
        {
            if (previewControl1.Report != null)
            {
                //不弹出打印设置框
                previewControl1.Report.PrintSettings.ShowDialog = false;
                previewControl1.Report.Print();
                FastReport.Export.Image.ImageExport insExp = new FastReport.Export.Image.ImageExport();
                insExp.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
                previewControl1.Report.Export(insExp, @expFilePath);
                return true;
            }
            else
            {
                return false;
            }
        }
        //设置缩放
        public void setZoom(float zoomValue)
        {
            previewControl1.Zoom = zoomValue;
        }
        //组织学报告打印
        public Boolean DirectPrintBLReport(string ReportPrinter, int PrintReportNum, BLReportParas InsM, string ImagePath, string expFilePath, Font CurFont = null, Font CurzdFont = null)
        {

            FastReport.Utils.Config.ReportSettings.ShowProgress = false;
            FastReport.Utils.Config.ReportSettings.ShowPerformance = false;

            //创建fastreport报表实例
            FastReport.Report report = new FastReport.Report();
            //导入设计好的报表
            if (!InsM.fileImages.Equals(""))
            {

                string[] imgs = InsM.fileImages.Split('|');

                if (InsM.wtzd_flag.Equals("1"))
                {
                    if (!InsM.Txt_zdpz.Equals(""))
                    {

                        if (imgs.Length == 1)
                        {
                            report.Load(APPdirPath + @"\Report\WtgpBLpic1ReportA4.frx");
                            FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                            Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                        }
                        else if (imgs.Length == 2)
                        {
                            report.Load(APPdirPath + @"\Report\WtgpBLReportA4.frx");
                            FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                            Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                        }
                        else if (imgs.Length > 2)
                        {
                            report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                        }
                    }
                    else
                    {
                        if (imgs.Length == 1)
                        {
                            report.Load(APPdirPath + @"\Report\WtnogpBLpic1ReportA4.frx");
                        }
                        else if (imgs.Length == 2)
                        {
                            report.Load(APPdirPath + @"\Report\WtnogpBLReportA4.frx");
                        }
                        else if (imgs.Length > 2)
                        {
                            report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                        }
                    }
                }
                else
                {
                    if (!InsM.report_gb_doc.Equals(""))
                    {
                        if (!InsM.Txt_zdpz.Equals(""))
                        {

                            if (imgs.Length == 1)
                            {
                                report.Load(APPdirPath + @"\Report\gpBLpic1ReportA4.frx");
                                FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                                Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                            }
                            else if (imgs.Length == 2)
                            {
                                report.Load(APPdirPath + @"\Report\gpBLReportA4.frx");
                                FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                                Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                            }
                            else if (imgs.Length > 2)
                            {
                                report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                            }
                        }
                        else
                        {
                            if (imgs.Length == 1)
                            {
                                report.Load(APPdirPath + @"\Report\nogpBLpic1ReportA4.frx");
                            }
                            else if (imgs.Length == 2)
                            {
                                report.Load(APPdirPath + @"\Report\nogpBLReportA4.frx");
                            }
                            else if (imgs.Length > 2)
                            {
                                report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                            }
                        }
                    }
                    else
                    {
                        if (!InsM.Txt_zdpz.Equals(""))
                        {

                            if (imgs.Length == 1)
                            {
                                report.Load(APPdirPath + @"\Report\BLpic1ReportA4.frx");
                                FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                                Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                            }
                            else if (imgs.Length == 2)
                            {
                                report.Load(APPdirPath + @"\Report\BLReportA4.frx");
                                FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                                Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                            }
                            else if (imgs.Length > 2)
                            {
                                report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                            }
                        }
                        else
                        {
                            if (imgs.Length == 1)
                            {
                                report.Load(APPdirPath + @"\Report\noBLpic1ReportA4.frx");
                            }
                            else if (imgs.Length == 2)
                            {
                                report.Load(APPdirPath + @"\Report\noBLReportA4.frx");
                            }
                            else if (imgs.Length > 2)
                            {
                                report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                            }
                        }
                    }
                }
            }
            else
            {
                report.Load(APPdirPath + @"\Report\BLnopicReportA4.frx");
            }
            //设置默认打印机
            if (!ReportPrinter.Equals(""))
            {
                report.PrintSettings.Printer = ReportPrinter;
            }
            //打印份数
            report.PrintSettings.Copies = PrintReportNum;
            //参数设置
            report.SetParameterValue("ReportParaHospital", InsM.ReportParaHospital);
            if (report.GetParameter("Submit_UnitName") != null)
            {
                report.SetParameterValue("Submit_UnitName", InsM.Txt_Sjdw);
            }
            report.SetParameterValue("study_no", InsM.study_no);
            FastReport.TextObject Txt_Name = report.FindObject("Txt_Name") as FastReport.TextObject;
            Txt_Name.Text = InsM.Txt_Name;
            FastReport.TextObject Txt_Sex = report.FindObject("Txt_Sex") as FastReport.TextObject;
            Txt_Sex.Text = InsM.Txt_Sex;
            FastReport.TextObject Txt_Age = report.FindObject("Txt_Age") as FastReport.TextObject;
            Txt_Age.Text = InsM.Txt_Age;
            FastReport.TextObject Txt_ly = report.FindObject("Txt_ly") as FastReport.TextObject;
            Txt_ly.Text = InsM.Txt_ly;
            //门诊或者住院标签
            FastReport.TextObject mzorzyLab = report.FindObject("Text30") as FastReport.TextObject;
            //送检单位或者送检科室标签
            FastReport.TextObject dworks = report.FindObject("Text6") as FastReport.TextObject;
            if (InsM.Txt_ly.Equals("门诊"))
            {
                dworks.Text = "送检科室：";
                mzorzyLab.Text = "病人ID号：";
                FastReport.TextObject Txt_Pid = report.FindObject("Txt_Pid") as FastReport.TextObject;
                if (InsM.Txt_Pid.Contains("New") == false)
                {
                    Txt_Pid.Text = InsM.Txt_Pid;
                }
            }
            else if (InsM.Txt_ly.Equals("住院"))
            {
                dworks.Text = "送检科室：";
                mzorzyLab.Text = "住 院 号：";
                FastReport.TextObject Txt_Pid = report.FindObject("Txt_Pid") as FastReport.TextObject;
                Txt_Pid.Text = InsM.Txt_Zyh;
                if (!InsM.Txt_Ch.Equals(""))
                {
                    FastReport.TextObject Txt_BedNo = report.FindObject("Txt_BedNo") as FastReport.TextObject;
                    Txt_BedNo.Text = InsM.Txt_Ch + "床";
                }
            }
            else if (InsM.Txt_ly.Equals("外来"))
            {
                dworks.Text = "送检单位：";
                mzorzyLab.Text = "病人ID号：";
                FastReport.TextObject Txt_Pid = report.FindObject("Txt_Pid") as FastReport.TextObject;
                if (InsM.Txt_Pid.Contains("New") == false)
                {
                    Txt_Pid.Text = InsM.Txt_Pid;
                }
            }
            FastReport.TextObject Txt_Sjbw = report.FindObject("Txt_Sjbw") as FastReport.TextObject;
            Txt_Sjbw.Text = InsM.Txt_Sjbw;
            FastReport.TextObject Txt_Sjks = report.FindObject("Txt_Sjks") as FastReport.TextObject;
            Txt_Sjks.Text = InsM.Txt_Sjks;
            FastReport.TextObject Txt_Sjys = report.FindObject("Txt_Sjys") as FastReport.TextObject;
            Txt_Sjys.Text = InsM.Txt_Sjys;
            FastReport.TextObject Txt_Sqrq = report.FindObject("Txt_Sqrq") as FastReport.TextObject;
            Txt_Sqrq.Text = InsM.Txt_Sqrq;
            FastReport.TextObject Txt_Rysj = report.FindObject("Txt_Rysj") as FastReport.TextObject;
            if (CurFont != null)
            {
                Txt_Rysj.Font = CurFont;
            }
            Txt_Rysj.Text = ReplaceChiEng(InsM.Txt_Rysj);
            FastReport.TextObject Txt_Blzd = report.FindObject("Txt_Blzd") as FastReport.TextObject;
            if (CurzdFont != null)
            {
                Txt_Blzd.Font = CurzdFont;
            }
            if (!InsM.report_gb_doc.Equals(""))
            {
                FastReport.TextObject Txt_Gpys = report.FindObject("Txt_Gpys") as FastReport.TextObject;
                if (Txt_Gpys != null)
                {
                    Txt_Gpys.Text = InsM.report_gb_doc;
                }
            }
            else
            {
                FastReport.TextObject Text34 = report.FindObject("Text34") as FastReport.TextObject;
                if (Text34 != null)
                {
                    Text34.Visible = false;
                }
            }
            Txt_Blzd.Text = ReplaceChiEng(InsM.Txt_Blzd);
            FastReport.TextObject txt_Bgrq = report.FindObject("txt_Bgrq") as FastReport.TextObject;
            txt_Bgrq.Text = InsM.txt_Bgrq;
            FastReport.TextObject Txt_Bgys = report.FindObject("Txt_Bgys") as FastReport.TextObject;
            Txt_Bgys.Text = InsM.Txt_Bgys;
            FastReport.TextObject Txt_fyys = report.FindObject("Txt_fyys") as FastReport.TextObject;
            Txt_fyys.Text = InsM.Txt_fjys;
            if (!InsM.fileImages.Equals(""))
            {
                string[] imgs = InsM.fileImages.Split('|');
                if (imgs.Length == 1)
                {
                    FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                    Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);

                }
                else if (imgs.Length == 2)
                {

                    if (imgs[0].IndexOf("DT") != -1)
                    {
                        FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                        Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                        FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                        Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[1]);
                    }
                    else
                    {
                        FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                        Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[1]);
                        FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                        Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                    }

                }
                else if (imgs.Length > 2)
                {
                    FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                    Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                    FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                    Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[1]);
                    FastReport.PictureObject Picture3 = report.FindObject("Picture3") as FastReport.PictureObject;
                    Picture3.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[2]);
                }
            }
            //不弹出打印设置框
            report.PrintSettings.ShowDialog = false;
            report.Prepare();
            report.Print();
            FastReport.Export.Image.ImageExport insExp = new FastReport.Export.Image.ImageExport();
            insExp.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
            report.Export(insExp, @expFilePath);
            return true;
        }

        //组织学报告预览
        public void LoadBLPreviewer(string ReportPrinter, int PrintReportNum, BLReportParas InsM, string ImagePath, Font CurFont = null, Font CurzdFont = null)
        {
            FastReport.Utils.Config.ReportSettings.ShowProgress = false;
            FastReport.Utils.Config.ReportSettings.ShowPerformance = false;

            //创建fastreport报表实例
            FastReport.Report report = new FastReport.Report();
            //导入设计好的报表
            if (!InsM.fileImages.Equals(""))
            {
                string[] imgs = InsM.fileImages.Split('|');
                if (InsM.wtzd_flag.Equals("1"))
                {
                    if (!InsM.Txt_zdpz.Equals(""))
                    {

                        if (imgs.Length == 1)
                        {
                            report.Load(APPdirPath + @"\Report\WtgpBLpic1ReportA4.frx");
                            FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                            Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                        }
                        else if (imgs.Length == 2)
                        {
                            report.Load(APPdirPath + @"\Report\WtgpBLReportA4.frx");
                            FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                            Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                        }
                        else if (imgs.Length > 2)
                        {
                            report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                        }
                    }
                    else
                    {
                        if (imgs.Length == 1)
                        {
                            report.Load(APPdirPath + @"\Report\WtnogpBLpic1ReportA4.frx");
                        }
                        else if (imgs.Length == 2)
                        {
                            report.Load(APPdirPath + @"\Report\WtnogpBLReportA4.frx");
                        }
                        else if (imgs.Length > 2)
                        {
                            report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                        }
                    }
                }
                else
                {
                    if (!InsM.report_gb_doc.Equals(""))
                    {
                        if (!InsM.Txt_zdpz.Equals(""))
                        {

                            if (imgs.Length == 1)
                            {
                                report.Load(APPdirPath + @"\Report\gpBLpic1ReportA4.frx");
                                FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                                Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                            }
                            else if (imgs.Length == 2)
                            {
                                report.Load(APPdirPath + @"\Report\gpBLReportA4.frx");
                                FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                                Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                            }
                            else if (imgs.Length > 2)
                            {
                                report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                            }
                        }
                        else
                        {
                            if (imgs.Length == 1)
                            {
                                report.Load(APPdirPath + @"\Report\nogpBLpic1ReportA4.frx");
                            }
                            else if (imgs.Length == 2)
                            {
                                report.Load(APPdirPath + @"\Report\nogpBLReportA4.frx");
                            }
                            else if (imgs.Length > 2)
                            {
                                report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                            }
                        }
                    }
                    else
                    {
                        if (!InsM.Txt_zdpz.Equals(""))
                        {

                            if (imgs.Length == 1)
                            {
                                report.Load(APPdirPath + @"\Report\BLpic1ReportA4.frx");
                                FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                                Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                            }
                            else if (imgs.Length == 2)
                            {
                                report.Load(APPdirPath + @"\Report\BLReportA4.frx");
                                FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                                Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                            }
                            else if (imgs.Length > 2)
                            {
                                report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                            }
                        }
                        else
                        {
                            if (imgs.Length == 1)
                            {
                                report.Load(APPdirPath + @"\Report\noBLpic1ReportA4.frx");
                            }
                            else if (imgs.Length == 2)
                            {
                                report.Load(APPdirPath + @"\Report\noBLReportA4.frx");
                            }
                            else if (imgs.Length > 2)
                            {
                                report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                            }
                        }

                    }
                }
            }
            else
            {
                report.Load(APPdirPath + @"\Report\BLnopicReportA4.frx");
            }
            //设置默认打印机
            if (!ReportPrinter.Equals(""))
            {
                report.PrintSettings.Printer = ReportPrinter;
            }
            //打印份数
            report.PrintSettings.Copies = PrintReportNum;
            //参数设置
            report.SetParameterValue("ReportParaHospital", InsM.ReportParaHospital);
            if (report.GetParameter("Submit_UnitName") != null)
            {
                report.SetParameterValue("Submit_UnitName", InsM.Txt_Sjdw);
            }
            report.SetParameterValue("study_no", InsM.study_no);
            FastReport.TextObject Txt_Name = report.FindObject("Txt_Name") as FastReport.TextObject;
            Txt_Name.Text = InsM.Txt_Name;
            FastReport.TextObject Txt_Sex = report.FindObject("Txt_Sex") as FastReport.TextObject;
            Txt_Sex.Text = InsM.Txt_Sex;
            FastReport.TextObject Txt_Age = report.FindObject("Txt_Age") as FastReport.TextObject;
            Txt_Age.Text = InsM.Txt_Age;
            FastReport.TextObject Txt_ly = report.FindObject("Txt_ly") as FastReport.TextObject;
            Txt_ly.Text = InsM.Txt_ly;
            //门诊或者住院标签
            FastReport.TextObject mzorzyLab = report.FindObject("Text30") as FastReport.TextObject;
            //送检单位或者送检科室标签
            FastReport.TextObject dworks = report.FindObject("Text6") as FastReport.TextObject;
            if (InsM.Txt_ly.Equals("门诊"))
            {
                dworks.Text = "送检科室：";
                mzorzyLab.Text = "病人ID号：";
                FastReport.TextObject Txt_Pid = report.FindObject("Txt_Pid") as FastReport.TextObject;
                if (InsM.Txt_Pid.Contains("New") == false)
                {
                    Txt_Pid.Text = InsM.Txt_Pid;
                }
            }
            else if (InsM.Txt_ly.Equals("住院"))
            {
                dworks.Text = "送检科室：";
                mzorzyLab.Text = "住 院 号：";
                FastReport.TextObject Txt_Pid = report.FindObject("Txt_Pid") as FastReport.TextObject;
                Txt_Pid.Text = InsM.Txt_Zyh;
                if (!InsM.Txt_Ch.Equals(""))
                {
                    FastReport.TextObject Txt_BedNo = report.FindObject("Txt_BedNo") as FastReport.TextObject;
                    Txt_BedNo.Text = InsM.Txt_Ch + "床";
                }


            }
            else if (InsM.Txt_ly.Equals("外来"))
            {
                dworks.Text = "送检单位：";
                mzorzyLab.Text = "病人ID号：";
                FastReport.TextObject Txt_Pid = report.FindObject("Txt_Pid") as FastReport.TextObject;
                if (InsM.Txt_Pid.Contains("New") == false)
                {
                    Txt_Pid.Text = InsM.Txt_Pid;
                }
            }
            if (!InsM.report_gb_doc.Equals(""))
            {
                FastReport.TextObject Txt_Gpys = report.FindObject("Txt_Gpys") as FastReport.TextObject;
                if (Txt_Gpys != null)
                {
                    Txt_Gpys.Text = InsM.report_gb_doc;
                }
            }
            else
            {
                FastReport.TextObject Text34 = report.FindObject("Text34") as FastReport.TextObject;
                if (Text34 != null)
                {
                    Text34.Visible = false;
                }
            }
            FastReport.TextObject Txt_Sjbw = report.FindObject("Txt_Sjbw") as FastReport.TextObject;
            Txt_Sjbw.Text = InsM.Txt_Sjbw;
            FastReport.TextObject Txt_Sjks = report.FindObject("Txt_Sjks") as FastReport.TextObject;
            Txt_Sjks.Text = InsM.Txt_Sjks;
            FastReport.TextObject Txt_Sjys = report.FindObject("Txt_Sjys") as FastReport.TextObject;
            Txt_Sjys.Text = InsM.Txt_Sjys;
            FastReport.TextObject Txt_Sqrq = report.FindObject("Txt_Sqrq") as FastReport.TextObject;
            Txt_Sqrq.Text = InsM.Txt_Sqrq;
            FastReport.TextObject Txt_Rysj = report.FindObject("Txt_Rysj") as FastReport.TextObject;
            if (CurFont != null)
            {
                Txt_Rysj.Font = CurFont;
            }

            Txt_Rysj.Text = ReplaceChiEng(InsM.Txt_Rysj);
            FastReport.TextObject Txt_Blzd = report.FindObject("Txt_Blzd") as FastReport.TextObject;
            if (CurzdFont != null)
            {
                Txt_Blzd.Font = CurzdFont;
            }
            Txt_Blzd.Text = ReplaceChiEng(InsM.Txt_Blzd);
            FastReport.TextObject txt_Bgrq = report.FindObject("txt_Bgrq") as FastReport.TextObject;
            txt_Bgrq.Text = InsM.txt_Bgrq;
            FastReport.TextObject Txt_Bgys = report.FindObject("Txt_Bgys") as FastReport.TextObject;
            Txt_Bgys.Text = InsM.Txt_Bgys;
            FastReport.TextObject Txt_fyys = report.FindObject("Txt_fyys") as FastReport.TextObject;
            Txt_fyys.Text = InsM.Txt_fjys;
            if (!InsM.fileImages.Equals(""))
            {
                string[] imgs = InsM.fileImages.Split('|');
                if (imgs.Length == 1)
                {
                    FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                    Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);

                }
                else if (imgs.Length == 2)
                {
                    if (imgs[0].IndexOf("DT") != -1)
                    {
                        FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                        Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                        FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                        Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[1]);
                    }
                    else
                    {
                        FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                        Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[1]);
                        FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                        Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                    }

                }
                else if (imgs.Length > 2)
                {
                    FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                    Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                    FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                    Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[1]);
                    FastReport.PictureObject Picture3 = report.FindObject("Picture3") as FastReport.PictureObject;
                    Picture3.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[2]);
                }
            }
            //previewControl1.Zoom = 0.93f;
            report.Preview = this.previewControl1;
            if (!report.IsRunning)
            {
                report.Show();
            }

        }

        //胸腹水类型报告预览
        public void LoadXfsPreviewer(string ReportPrinter, int PrintReportNum, BbXfsReportParas InsM, string ImagePath)
        {
            FastReport.Utils.Config.ReportSettings.ShowProgress = false;
            FastReport.Utils.Config.ReportSettings.ShowPerformance = false;

            //创建fastreport报表实例
            FastReport.Report report = new FastReport.Report();
            Boolean ytflag = false;
            //导入设计好的报表
            if (!InsM.fileImages.Equals(""))
            {
                string[] imgs = InsM.fileImages.Split('|');
                if (imgs.Length > 0)
                {
                    if (!InsM.Txt_Zdpz.Equals(""))
                    {
                        report.Load(APPdirPath + @"\Report\BbXfsReportA4.frx");
                        FastReport.TextObject Text36 = report.FindObject("Text36") as FastReport.TextObject;
                        Text36.Text = ReplaceChiEng(InsM.Txt_Zdpz);
                    }
                    else
                    {
                        report.Load(APPdirPath + @"\Report\noBbXfsReportA4.frx");
                    }
                    ytflag = true;
                }
                else
                {
                    report.Load(APPdirPath + @"\Report\BbXfsReportA5.frx");
                }
            }
            else
            {
                report.Load(APPdirPath + @"\Report\BbXfsReportA5.frx");
            }


            //设置默认打印机
            if (!ReportPrinter.Equals(""))
            {
                report.PrintSettings.Printer = ReportPrinter;
            }
            //打印份数
            report.PrintSettings.Copies = PrintReportNum;
            //参数设置
            report.SetParameterValue("ReportParaHospital", InsM.ReportParaHospital);
            report.SetParameterValue("study_no", InsM.study_no);
            FastReport.TextObject Txt_Name = report.FindObject("Txt_Name") as FastReport.TextObject;
            Txt_Name.Text = InsM.Txt_Name;
            FastReport.TextObject Txt_Sex = report.FindObject("Txt_Sex") as FastReport.TextObject;
            Txt_Sex.Text = InsM.Txt_Sex;
            FastReport.TextObject Txt_Age = report.FindObject("Txt_Age") as FastReport.TextObject;
            Txt_Age.Text = InsM.Txt_Age;
            FastReport.TextObject Txt_ly = report.FindObject("Txt_ly") as FastReport.TextObject;
            Txt_ly.Text = InsM.Txt_ly;
            //门诊或者住院标签
            FastReport.TextObject mzorzyLab = report.FindObject("Text29") as FastReport.TextObject;
            //送检单位或者送检科室标签
            FastReport.TextObject dworks = report.FindObject("Text6") as FastReport.TextObject;
            FastReport.TextObject Txt_Zyh = report.FindObject("Txt_Zyh") as FastReport.TextObject;
            if (ytflag)
            {
                FastReport.TextObject Txt_Sqd = report.FindObject("Txt_Sqd") as FastReport.TextObject;
                Txt_Sqd.Text = InsM.Txt_Sqd;
                FastReport.TextObject Txt_Tel = report.FindObject("Txt_Tel") as FastReport.TextObject;
                Txt_Tel.Text = InsM.Txt_Tel;
                FastReport.TextObject Txt_Addr = report.FindObject("Txt_Addr") as FastReport.TextObject;
                Txt_Addr.Text = InsM.Txt_Addr;
            }
            if (InsM.Txt_ly.Equals("门诊"))
            {
                mzorzyLab.Text = "病人ID号：";
                if (InsM.Txt_Pid.Contains("New") == false)
                {
                    Txt_Zyh.Text = InsM.Txt_Pid;
                }
                dworks.Text = "送检科室：";

            }
            else if (InsM.Txt_ly.Equals("住院"))
            {
                mzorzyLab.Text = "住 院 号：";
                Txt_Zyh.Text = InsM.Txt_Zyh;
                dworks.Text = "送检科室：";
                if (!InsM.Txt_Bed.Equals(""))
                {
                    FastReport.TextObject Txt_BedNo = report.FindObject("Txt_BedNo") as FastReport.TextObject;
                    Txt_BedNo.Text = InsM.Txt_Bed + "床";
                }

            }
            else if (InsM.Txt_ly.Equals("外来"))
            {
                mzorzyLab.Text = "病人ID号：";
                if (InsM.Txt_Pid.Contains("New") == false)
                {
                    Txt_Zyh.Text = InsM.Txt_Pid;
                }
                dworks.Text = "送检单位：";
            }
            else if (InsM.Txt_ly.Equals("体检"))
            {
                mzorzyLab.Text = "体 检 号：";
                Txt_Zyh.Text = InsM.hospital_card;
                dworks.Text = "送检科室：";
            }
            FastReport.TextObject Txt_Sjbw = report.FindObject("Txt_Sjbw") as FastReport.TextObject;
            Txt_Sjbw.Text = InsM.Txt_Sjbw;
            FastReport.TextObject Txt_Sjks = report.FindObject("Txt_Sjks") as FastReport.TextObject;
            Txt_Sjks.Text = InsM.Txt_Sjks;
            FastReport.TextObject Txt_Sjys = report.FindObject("Txt_Sjys") as FastReport.TextObject;
            Txt_Sjys.Text = InsM.Txt_Sjys;
            FastReport.TextObject Txt_Sqrq = report.FindObject("Txt_Sqrq") as FastReport.TextObject;
            Txt_Sqrq.Text = InsM.Txt_Sqrq;
            FastReport.TextObject Txt_Blzd = report.FindObject("Txt_Blzd") as FastReport.TextObject;
            Txt_Blzd.Text = InsM.Txt_Blzd;
            FastReport.TextObject txt_Bgrq = report.FindObject("txt_Bgrq") as FastReport.TextObject;
            txt_Bgrq.Text = InsM.txt_Bgrq;
            FastReport.TextObject Txt_Bgys = report.FindObject("Txt_Bgys") as FastReport.TextObject;
            Txt_Bgys.Text = InsM.Txt_Bgys;
            FastReport.TextObject Txt_Fyys = report.FindObject("Txt_Fyys") as FastReport.TextObject;
            Txt_Fyys.Text = InsM.Txt_Fyys;



            if (!InsM.fileImages.Equals(""))
            {
                string[] imgs = InsM.fileImages.Split('|');
                if (imgs.Length > 0)
                {
                    FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                    Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);

                }
            }

            report.Preview = this.previewControl1;
            //previewControl1.Zoom = 0.93f;
            if (!report.IsRunning)
            {
                report.Show();
            }
        }

        //胸腹水类型报告打印
        public Boolean DirectPrintXfsReport(string ReportPrinter, int PrintReportNum, BbXfsReportParas InsM, string ImagePath, string expFilePath)
        {
            FastReport.Utils.Config.ReportSettings.ShowProgress = false;
            FastReport.Utils.Config.ReportSettings.ShowPerformance = false;

            //创建fastreport报表实例
            FastReport.Report report = new FastReport.Report();
            Boolean ytflag = false;
            //导入设计好的报表
            if (!InsM.fileImages.Equals(""))
            {
                string[] imgs = InsM.fileImages.Split('|');
                if (imgs.Length > 0)
                {
                    if (!InsM.Txt_Zdpz.Equals(""))
                    {
                        report.Load(APPdirPath + @"\Report\BbXfsReportA4.frx");
                        FastReport.TextObject Text36 = report.FindObject("Text36") as FastReport.TextObject;
                        Text36.Text = ReplaceChiEng(InsM.Txt_Zdpz);
                    }
                    else
                    {
                        report.Load(APPdirPath + @"\Report\noBbXfsReportA4.frx");
                    }
                    ytflag = true;
                }
                else
                {
                    report.Load(APPdirPath + @"\Report\BbXfsReportA5.frx");
                }
            }
            else
            {
                report.Load(APPdirPath + @"\Report\BbXfsReportA5.frx");
            }
            //设置默认打印机
            if (!ReportPrinter.Equals(""))
            {
                report.PrintSettings.Printer = ReportPrinter;
            }
            //打印份数
            report.PrintSettings.Copies = PrintReportNum;
            //参数设置
            report.SetParameterValue("ReportParaHospital", InsM.ReportParaHospital);
            report.SetParameterValue("study_no", InsM.study_no);
            FastReport.TextObject Txt_Name = report.FindObject("Txt_Name") as FastReport.TextObject;
            Txt_Name.Text = InsM.Txt_Name;
            FastReport.TextObject Txt_Sex = report.FindObject("Txt_Sex") as FastReport.TextObject;
            Txt_Sex.Text = InsM.Txt_Sex;
            FastReport.TextObject Txt_Age = report.FindObject("Txt_Age") as FastReport.TextObject;
            Txt_Age.Text = InsM.Txt_Age;
            FastReport.TextObject Txt_ly = report.FindObject("Txt_ly") as FastReport.TextObject;
            Txt_ly.Text = InsM.Txt_ly;
            //门诊或者住院标签
            FastReport.TextObject mzorzyLab = report.FindObject("Text29") as FastReport.TextObject;
            //送检单位或者送检科室标签
            FastReport.TextObject dworks = report.FindObject("Text6") as FastReport.TextObject;
            FastReport.TextObject Txt_Zyh = report.FindObject("Txt_Zyh") as FastReport.TextObject;
            if (InsM.Txt_ly.Equals("门诊"))
            {
                mzorzyLab.Text = "病人ID号：";
                if (InsM.Txt_Pid.Contains("New") == false)
                {
                    Txt_Zyh.Text = InsM.Txt_Pid;
                }
                dworks.Text = "送检科室：";

            }
            else if (InsM.Txt_ly.Equals("住院"))
            {
                mzorzyLab.Text = "住 院 号：";
                Txt_Zyh.Text = InsM.Txt_Zyh;
                dworks.Text = "送检科室：";
                if (!InsM.Txt_Bed.Equals(""))
                {
                    FastReport.TextObject Txt_BedNo = report.FindObject("Txt_BedNo") as FastReport.TextObject;
                    Txt_BedNo.Text = InsM.Txt_Bed + "床";
                }

            }
            else if (InsM.Txt_ly.Equals("外来"))
            {
                mzorzyLab.Text = "病人ID号：";
                if (InsM.Txt_Pid.Contains("New") == false)
                {
                    Txt_Zyh.Text = InsM.Txt_Pid;
                }
                dworks.Text = "送检单位：";
            }
            else if (InsM.Txt_ly.Equals("体检"))
            {
                mzorzyLab.Text = "体 检 号：";
                Txt_Zyh.Text = InsM.hospital_card;
                dworks.Text = "送检科室：";
            }

            FastReport.TextObject Txt_Sjbw = report.FindObject("Txt_Sjbw") as FastReport.TextObject;
            Txt_Sjbw.Text = InsM.Txt_Sjbw;
            FastReport.TextObject Txt_Sjks = report.FindObject("Txt_Sjks") as FastReport.TextObject;
            Txt_Sjks.Text = InsM.Txt_Sjks;
            FastReport.TextObject Txt_Sjys = report.FindObject("Txt_Sjys") as FastReport.TextObject;
            Txt_Sjys.Text = InsM.Txt_Sjys;
            FastReport.TextObject Txt_Sqrq = report.FindObject("Txt_Sqrq") as FastReport.TextObject;
            Txt_Sqrq.Text = InsM.Txt_Sqrq;
            FastReport.TextObject Txt_Blzd = report.FindObject("Txt_Blzd") as FastReport.TextObject;
            Txt_Blzd.Text = InsM.Txt_Blzd;
            FastReport.TextObject txt_Bgrq = report.FindObject("txt_Bgrq") as FastReport.TextObject;
            txt_Bgrq.Text = InsM.txt_Bgrq;
            FastReport.TextObject Txt_Bgys = report.FindObject("Txt_Bgys") as FastReport.TextObject;
            Txt_Bgys.Text = InsM.Txt_Bgys;
            FastReport.TextObject Txt_Fyys = report.FindObject("Txt_Fyys") as FastReport.TextObject;
            Txt_Fyys.Text = InsM.Txt_Fyys;
            if (ytflag)
            {
                FastReport.TextObject Txt_Sqd = report.FindObject("Txt_Sqd") as FastReport.TextObject;
                Txt_Sqd.Text = InsM.Txt_Sqd;
                FastReport.TextObject Txt_Tel = report.FindObject("Txt_Tel") as FastReport.TextObject;
                Txt_Tel.Text = InsM.Txt_Tel;
                FastReport.TextObject Txt_Addr = report.FindObject("Txt_Addr") as FastReport.TextObject;
                Txt_Addr.Text = InsM.Txt_Addr;
            }
            if (!InsM.fileImages.Equals(""))
            {
                string[] imgs = InsM.fileImages.Split('|');
                if (imgs.Length > 0)
                {
                    FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                    Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                }
            }

            //不弹出打印设置框
            report.PrintSettings.ShowDialog = false;
            report.Prepare();
            report.Print();
            FastReport.Export.Image.ImageExport insExp = new FastReport.Export.Image.ImageExport();
            insExp.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
            report.Export(insExp, @expFilePath);
            return true;
        }

        //妇科细胞学预览
        public void LoadXbxPreviewer(string ReportPrinter, int PrintReportNum, XbxReportParas InsM, string ImagePath)
        {

            FastReport.Utils.Config.ReportSettings.ShowProgress = false;
            FastReport.Utils.Config.ReportSettings.ShowPerformance = false;

            //创建fastreport报表实例
            FastReport.Report report = new FastReport.Report();

            //导入设计好的报表
            report.Load(APPdirPath + @"\Report\yjxbx.frx");
            //设置默认打印机
            if (!ReportPrinter.Equals(""))
            {
                report.PrintSettings.Printer = ReportPrinter;
            }
            //打印份数
            report.PrintSettings.Copies = PrintReportNum;
            //参数设置
            report.SetParameterValue("ReportParaHospital", InsM.ReportParaHospital);
            report.SetParameterValue("study_no", InsM.study_no);
            FastReport.TextObject Txt_Name = report.FindObject("Txt_Name") as FastReport.TextObject;
            Txt_Name.Text = InsM.Txt_Name;
            FastReport.TextObject Txt_Sex = report.FindObject("Txt_Sex") as FastReport.TextObject;
            Txt_Sex.Text = InsM.Txt_Sex;
            FastReport.TextObject Txt_Age = report.FindObject("Txt_Age") as FastReport.TextObject;
            Txt_Age.Text = InsM.Txt_Age;
            FastReport.TextObject Txt_ly = report.FindObject("Txt_ly") as FastReport.TextObject;
            Txt_ly.Text = InsM.Txt_ly;
            FastReport.TextObject Txt_Sjks = report.FindObject("Txt_Sjks") as FastReport.TextObject;
            Txt_Sjks.Text = InsM.Txt_Sjks;
            FastReport.TextObject Txt_Sjys = report.FindObject("Txt_Sjys") as FastReport.TextObject;
            Txt_Sjys.Text = InsM.Txt_Sjys;
            FastReport.TextObject Txt_Bblx = report.FindObject("Txt_Bblx") as FastReport.TextObject;
            Txt_Bblx.Text = InsM.Txt_bblx;
            FastReport.TextObject Text7 = report.FindObject("Text7") as FastReport.TextObject;
            FastReport.TextObject Txt_Mcyj = report.FindObject("Txt_Mcyj") as FastReport.TextObject;
            if (InsM.Txt_ly.Equals("体检"))
            {
                Text7.Text = "体 检 号：";
                Txt_Mcyj.Text = InsM.Txt_mcyj;
            }
            else if (InsM.Txt_ly.Equals("住院"))
            {
                Text7.Text = "住 院 号：";
                Txt_Mcyj.Text = InsM.Txt_Zyh;
            }
            else
            {
                Text7.Text = "医院卡号：";
                Txt_Mcyj.Text = InsM.Txt_mcyj;
            }
            FastReport.TextObject Txt_Sqrq = report.FindObject("Txt_Sqrq") as FastReport.TextObject;
            Txt_Sqrq.Text = InsM.Txt_Sqrq;

            DataSet ds = ConvertXMLToDataSet(InsM.Txt_Xbl);
            if (ds != null)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                // 
                FastReport.TextObject Text68 = report.FindObject("Text68") as FastReport.TextObject;
                Text68.Text = dr["myd"].ToString();
                //
                FastReport.TextObject Text69 = report.FindObject("Text69") as FastReport.TextObject;
                Text69.Text = dr["xbl"].ToString();

                //

                FastReport.CheckBoxObject CheckBox49 = report.FindObject("CheckBox49") as FastReport.CheckBoxObject;
                if (dr["e1"].ToString().Equals("True"))
                {
                    CheckBox49.Checked = true;
                    CheckBox49.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox49.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox17 = report.FindObject("CheckBox17") as FastReport.CheckBoxObject;
                if (dr["e2"].ToString().Equals("True"))
                {
                    CheckBox17.Checked = true;
                    CheckBox17.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox17.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox18 = report.FindObject("CheckBox18") as FastReport.CheckBoxObject;
                if (dr["e3"].ToString().Equals("True"))
                {
                    CheckBox18.Checked = true;
                    CheckBox18.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox18.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox19 = report.FindObject("CheckBox19") as FastReport.CheckBoxObject;
                if (dr["e4"].ToString().Equals("True"))
                {
                    CheckBox19.Checked = true;
                    CheckBox19.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox19.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox21 = report.FindObject("CheckBox21") as FastReport.CheckBoxObject;
                FastReport.CheckBoxObject CheckBox20 = report.FindObject("CheckBox20") as FastReport.CheckBoxObject;

                if (dr["e5"].ToString().Equals("True"))
                {
                    CheckBox21.Checked = true;
                    CheckBox21.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;

                }
                else
                {
                    CheckBox21.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox27 = report.FindObject("CheckBox27") as FastReport.CheckBoxObject;
                FastReport.CheckBoxObject CheckBox22 = report.FindObject("CheckBox22") as FastReport.CheckBoxObject;
                if (dr["e6"].ToString().Equals("True"))
                {
                    CheckBox27.Checked = true;
                    CheckBox22.Checked = true;
                    CheckBox27.Border.Color = Color.Red;
                    CheckBox22.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox27.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox28 = report.FindObject("CheckBox28") as FastReport.CheckBoxObject;
                if (dr["e7"].ToString().Equals("True"))
                {
                    CheckBox28.Checked = true;
                    CheckBox22.Checked = true;
                    CheckBox28.Border.Color = Color.Red;
                    CheckBox22.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox28.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox29 = report.FindObject("CheckBox29") as FastReport.CheckBoxObject;
                if (dr["e8"].ToString().Equals("True"))
                {
                    CheckBox29.Checked = true;
                    CheckBox22.Checked = true;
                    CheckBox29.Border.Color = Color.Red;
                    CheckBox22.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox29.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox23 = report.FindObject("CheckBox23") as FastReport.CheckBoxObject;
                if (dr["e9"].ToString().Equals("True"))
                {
                    CheckBox23.Checked = true;
                    CheckBox23.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox23.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox24 = report.FindObject("CheckBox24") as FastReport.CheckBoxObject;
                if (dr["e10"].ToString().Equals("True"))
                {
                    CheckBox24.Checked = true;
                    CheckBox24.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox24.Checked = false;
                }

                //
                FastReport.CheckBoxObject CheckBox25 = report.FindObject("CheckBox25") as FastReport.CheckBoxObject;
                if (dr["e11"].ToString().Equals("True"))
                {
                    CheckBox25.Checked = true;
                    CheckBox25.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox25.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox26 = report.FindObject("CheckBox26") as FastReport.CheckBoxObject;
                if (dr["e12"].ToString().Equals("True"))
                {
                    CheckBox26.Checked = true;
                    CheckBox26.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox26.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox33 = report.FindObject("CheckBox33") as FastReport.CheckBoxObject;
                if (dr["e13"].ToString().Equals("True"))
                {
                    CheckBox33.Checked = true;
                    CheckBox33.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox33.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox31 = report.FindObject("CheckBox31") as FastReport.CheckBoxObject;
                if (dr["e14"].ToString().Equals("True"))
                {
                    CheckBox31.Checked = true;
                    CheckBox31.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox31.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox32 = report.FindObject("CheckBox32") as FastReport.CheckBoxObject;
                if (dr["e15"].ToString().Equals("True"))
                {
                    CheckBox32.Checked = true;
                    CheckBox32.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox32.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox30 = report.FindObject("CheckBox30") as FastReport.CheckBoxObject;
                if (dr["e16"].ToString().Equals("True"))
                {
                    CheckBox30.Checked = true;
                    CheckBox30.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox30.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox34 = report.FindObject("CheckBox34") as FastReport.CheckBoxObject;
                if (dr["e17"].ToString().Equals("True"))
                {
                    CheckBox34.Checked = true;
                    CheckBox34.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox34.Checked = false;
                }
                //

                FastReport.CheckBoxObject CheckBox35 = report.FindObject("CheckBox35") as FastReport.CheckBoxObject;
                FastReport.CheckBoxObject CheckBox36 = report.FindObject("CheckBox36") as FastReport.CheckBoxObject;
                if (dr["e18"].ToString().Equals("True"))
                {
                    CheckBox36.Checked = true;
                    CheckBox36.Border.Color = Color.Red;
                    CheckBox35.Checked = true;
                    CheckBox35.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox36.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox37 = report.FindObject("CheckBox37") as FastReport.CheckBoxObject;
                if (dr["e19"].ToString().Equals("True"))
                {
                    CheckBox37.Checked = true;
                    CheckBox37.Border.Color = Color.Red;
                    CheckBox35.Checked = true;
                    CheckBox35.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox37.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox38 = report.FindObject("CheckBox38") as FastReport.CheckBoxObject;
                if (dr["e20"].ToString().Equals("True"))
                {
                    CheckBox38.Checked = true;
                    CheckBox38.Border.Color = Color.Red;
                    CheckBox35.Checked = true;
                    CheckBox35.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox38.Checked = false;
                }

                //
                FastReport.CheckBoxObject CheckBox39 = report.FindObject("CheckBox39") as FastReport.CheckBoxObject;
                if (dr["e21"].ToString().Equals("True"))
                {
                    CheckBox39.Checked = true;
                    CheckBox39.Border.Color = Color.Red;
                    CheckBox35.Checked = true;
                    CheckBox35.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox39.Checked = false;
                }
                // 
                FastReport.CheckBoxObject CheckBox42 = report.FindObject("CheckBox42") as FastReport.CheckBoxObject;
                if (dr["e22"].ToString().Equals("True"))
                {
                    CheckBox42.Checked = true;
                    CheckBox42.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox42.Checked = false;
                }
                //

                FastReport.CheckBoxObject CheckBox41 = report.FindObject("CheckBox41") as FastReport.CheckBoxObject;
                FastReport.CheckBoxObject CheckBox45 = report.FindObject("CheckBox45") as FastReport.CheckBoxObject;
                if (dr["e2"].ToString().Equals("True"))
                {
                    CheckBox45.Checked = true;
                    CheckBox45.Border.Color = Color.Red;
                    CheckBox41.Checked = true;
                    CheckBox41.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox45.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox44 = report.FindObject("CheckBox44") as FastReport.CheckBoxObject;
                if (dr["e24"].ToString().Equals("True"))
                {
                    CheckBox44.Checked = true;
                    CheckBox44.Border.Color = Color.Red;
                    CheckBox41.Checked = true;
                    CheckBox41.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox44.Checked = false;
                }
                //

                FastReport.CheckBoxObject CheckBox46 = report.FindObject("CheckBox46") as FastReport.CheckBoxObject;
                FastReport.CheckBoxObject CheckBox47 = report.FindObject("CheckBox47") as FastReport.CheckBoxObject;
                if (dr["e25"].ToString().Equals("True"))
                {
                    CheckBox47.Checked = true;
                    CheckBox47.Border.Color = Color.Red;
                    CheckBox46.Checked = true;
                    CheckBox46.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox47.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox48 = report.FindObject("CheckBox48") as FastReport.CheckBoxObject;
                if (dr["e26"].ToString().Equals("True"))
                {
                    CheckBox48.Checked = true;
                    CheckBox48.Border.Color = Color.Red;
                    CheckBox46.Checked = true;
                    CheckBox46.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox48.Checked = false;
                }
                //

                FastReport.CheckBoxObject CheckBox50 = report.FindObject("CheckBox50") as FastReport.CheckBoxObject;
                FastReport.CheckBoxObject CheckBox51 = report.FindObject("CheckBox51") as FastReport.CheckBoxObject;
                if (dr["e27"].ToString().Equals("True"))
                {
                    CheckBox51.Checked = true;
                    CheckBox51.Border.Color = Color.Red;
                    CheckBox50.Checked = true;
                    CheckBox50.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox51.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox52 = report.FindObject("CheckBox52") as FastReport.CheckBoxObject;
                if (dr["e28"].ToString().Equals("True"))
                {
                    CheckBox52.Checked = true;
                    CheckBox52.Border.Color = Color.Red;
                    CheckBox50.Checked = true;
                    CheckBox50.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox52.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox53 = report.FindObject("CheckBox53") as FastReport.CheckBoxObject;
                if (dr["e29"].ToString().Equals("True"))
                {
                    CheckBox53.Checked = true;
                    CheckBox53.Border.Color = Color.Red;
                    CheckBox50.Checked = true;
                    CheckBox50.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox53.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox54 = report.FindObject("CheckBox54") as FastReport.CheckBoxObject;
                if (dr["e30"].ToString().Equals("True"))
                {
                    CheckBox54.Checked = true;
                    CheckBox54.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox54.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox55 = report.FindObject("CheckBox55") as FastReport.CheckBoxObject;
                if (dr["e31"].ToString().Equals("True"))
                {
                    CheckBox55.Checked = true;
                    CheckBox55.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox55.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox57 = report.FindObject("CheckBox57") as FastReport.CheckBoxObject;
                if (dr["e32"].ToString().Equals("True"))
                {
                    CheckBox57.Checked = true;
                    CheckBox57.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox57.Checked = false;
                }
            }



            FastReport.TextObject txt_Zljy = report.FindObject("Txt_Zljy") as FastReport.TextObject;
            txt_Zljy.Text = InsM.Txt_Zljy;
            FastReport.TextObject Txt_Bgrq = report.FindObject("Txt_Bgrq") as FastReport.TextObject;
            Txt_Bgrq.Text = InsM.Txt_Bgrq;
            FastReport.TextObject Txt_Bgys = report.FindObject("Txt_Bgys") as FastReport.TextObject;
            Txt_Bgys.Text = InsM.Txt_Bgys;
            if (!InsM.fileImages.Equals(""))
            {
                string[] imgs = InsM.fileImages.Split('|');
                if (imgs.Length > 0)
                {
                    FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                    Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                }
            }

            report.Preview = this.previewControl1;
            //previewControl1.Zoom = 0.93f;
            if (!report.IsRunning)
            {
                report.Show();
            }
        }
        //妇科细胞学打印
        public Boolean DirectPrintXbxReport(string ReportPrinter, int PrintReportNum, XbxReportParas InsM, string ImagePath, string expFilePath)
        {
            FastReport.Utils.Config.ReportSettings.ShowProgress = false;
            FastReport.Utils.Config.ReportSettings.ShowPerformance = false;

            //创建fastreport报表实例
            FastReport.Report report = new FastReport.Report();

            //导入设计好的报表
            report.Load(APPdirPath + @"\Report\yjxbx.frx");
            //设置默认打印机
            if (!ReportPrinter.Equals(""))
            {
                report.PrintSettings.Printer = ReportPrinter;
            }
            //打印份数
            report.PrintSettings.Copies = PrintReportNum;
            //参数设置
            report.SetParameterValue("ReportParaHospital", InsM.ReportParaHospital);
            report.SetParameterValue("study_no", InsM.study_no);
            FastReport.TextObject Txt_Name = report.FindObject("Txt_Name") as FastReport.TextObject;
            Txt_Name.Text = InsM.Txt_Name;
            FastReport.TextObject Txt_Sex = report.FindObject("Txt_Sex") as FastReport.TextObject;
            Txt_Sex.Text = InsM.Txt_Sex;
            FastReport.TextObject Txt_Age = report.FindObject("Txt_Age") as FastReport.TextObject;
            Txt_Age.Text = InsM.Txt_Age;
            FastReport.TextObject Txt_ly = report.FindObject("Txt_ly") as FastReport.TextObject;
            Txt_ly.Text = InsM.Txt_ly;
            FastReport.TextObject Txt_Sjks = report.FindObject("Txt_Sjks") as FastReport.TextObject;
            Txt_Sjks.Text = InsM.Txt_Sjks;
            FastReport.TextObject Txt_Sjys = report.FindObject("Txt_Sjys") as FastReport.TextObject;
            Txt_Sjys.Text = InsM.Txt_Sjys;
            FastReport.TextObject Txt_Bblx = report.FindObject("Txt_Bblx") as FastReport.TextObject;
            Txt_Bblx.Text = InsM.Txt_bblx;

            FastReport.TextObject Text7 = report.FindObject("Text7") as FastReport.TextObject;
            FastReport.TextObject Txt_Mcyj = report.FindObject("Txt_Mcyj") as FastReport.TextObject;
            if (InsM.Txt_ly.Equals("体检"))
            {
                Text7.Text = "体 检 号：";
                Txt_Mcyj.Text = InsM.Txt_mcyj;
            }
            else if (InsM.Txt_ly.Equals("住院"))
            {
                Text7.Text = "住 院 号：";
                Txt_Mcyj.Text = InsM.Txt_Zyh;
            }
            else
            {
                Text7.Text = "医院卡号：";
                Txt_Mcyj.Text = InsM.Txt_mcyj;
            }

            FastReport.TextObject Txt_Sqrq = report.FindObject("Txt_Sqrq") as FastReport.TextObject;
            Txt_Sqrq.Text = InsM.Txt_Sqrq;

            DataSet ds = ConvertXMLToDataSet(InsM.Txt_Xbl);
            if (ds != null)
            {

                DataRow dr = ds.Tables[0].Rows[0];

                // 
                FastReport.TextObject Text68 = report.FindObject("Text68") as FastReport.TextObject;
                Text68.Text = dr["myd"].ToString();
                //
                FastReport.TextObject Text69 = report.FindObject("Text69") as FastReport.TextObject;
                Text69.Text = dr["xbl"].ToString();

                //

                FastReport.CheckBoxObject CheckBox49 = report.FindObject("CheckBox49") as FastReport.CheckBoxObject;
                if (dr["e1"].ToString().Equals("True"))
                {
                    CheckBox49.Checked = true;
                    CheckBox49.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox49.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox17 = report.FindObject("CheckBox17") as FastReport.CheckBoxObject;
                if (dr["e2"].ToString().Equals("True"))
                {
                    CheckBox17.Checked = true;
                    CheckBox17.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox17.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox18 = report.FindObject("CheckBox18") as FastReport.CheckBoxObject;
                if (dr["e3"].ToString().Equals("True"))
                {
                    CheckBox18.Checked = true;
                    CheckBox18.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox18.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox19 = report.FindObject("CheckBox19") as FastReport.CheckBoxObject;
                if (dr["e4"].ToString().Equals("True"))
                {
                    CheckBox19.Checked = true;
                    CheckBox19.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox19.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox21 = report.FindObject("CheckBox21") as FastReport.CheckBoxObject;
                FastReport.CheckBoxObject CheckBox20 = report.FindObject("CheckBox20") as FastReport.CheckBoxObject;

                if (dr["e5"].ToString().Equals("True"))
                {
                    CheckBox21.Checked = true;
                    CheckBox21.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;

                }
                else
                {
                    CheckBox21.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox27 = report.FindObject("CheckBox27") as FastReport.CheckBoxObject;
                FastReport.CheckBoxObject CheckBox22 = report.FindObject("CheckBox22") as FastReport.CheckBoxObject;
                if (dr["e6"].ToString().Equals("True"))
                {
                    CheckBox27.Checked = true;
                    CheckBox22.Checked = true;
                    CheckBox27.Border.Color = Color.Red;
                    CheckBox22.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox27.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox28 = report.FindObject("CheckBox28") as FastReport.CheckBoxObject;
                if (dr["e7"].ToString().Equals("True"))
                {
                    CheckBox28.Checked = true;
                    CheckBox22.Checked = true;
                    CheckBox28.Border.Color = Color.Red;
                    CheckBox22.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox28.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox29 = report.FindObject("CheckBox29") as FastReport.CheckBoxObject;
                if (dr["e8"].ToString().Equals("True"))
                {
                    CheckBox29.Checked = true;
                    CheckBox22.Checked = true;
                    CheckBox29.Border.Color = Color.Red;
                    CheckBox22.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox29.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox23 = report.FindObject("CheckBox23") as FastReport.CheckBoxObject;
                if (dr["e9"].ToString().Equals("True"))
                {
                    CheckBox23.Checked = true;
                    CheckBox23.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox23.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox24 = report.FindObject("CheckBox24") as FastReport.CheckBoxObject;
                if (dr["e10"].ToString().Equals("True"))
                {
                    CheckBox24.Checked = true;
                    CheckBox24.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox24.Checked = false;
                }

                //
                FastReport.CheckBoxObject CheckBox25 = report.FindObject("CheckBox25") as FastReport.CheckBoxObject;
                if (dr["e11"].ToString().Equals("True"))
                {
                    CheckBox25.Checked = true;
                    CheckBox25.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox25.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox26 = report.FindObject("CheckBox26") as FastReport.CheckBoxObject;
                if (dr["e12"].ToString().Equals("True"))
                {
                    CheckBox26.Checked = true;
                    CheckBox26.Border.Color = Color.Red;
                    CheckBox20.Checked = true;
                    CheckBox20.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox26.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox33 = report.FindObject("CheckBox33") as FastReport.CheckBoxObject;
                if (dr["e13"].ToString().Equals("True"))
                {
                    CheckBox33.Checked = true;
                    CheckBox33.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox33.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox31 = report.FindObject("CheckBox31") as FastReport.CheckBoxObject;
                if (dr["e14"].ToString().Equals("True"))
                {
                    CheckBox31.Checked = true;
                    CheckBox31.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox31.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox32 = report.FindObject("CheckBox32") as FastReport.CheckBoxObject;
                if (dr["e15"].ToString().Equals("True"))
                {
                    CheckBox32.Checked = true;
                    CheckBox32.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox32.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox30 = report.FindObject("CheckBox30") as FastReport.CheckBoxObject;
                if (dr["e16"].ToString().Equals("True"))
                {
                    CheckBox30.Checked = true;
                    CheckBox30.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox30.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox34 = report.FindObject("CheckBox34") as FastReport.CheckBoxObject;
                if (dr["e17"].ToString().Equals("True"))
                {
                    CheckBox34.Checked = true;
                    CheckBox34.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox34.Checked = false;
                }
                //

                FastReport.CheckBoxObject CheckBox35 = report.FindObject("CheckBox35") as FastReport.CheckBoxObject;
                FastReport.CheckBoxObject CheckBox36 = report.FindObject("CheckBox36") as FastReport.CheckBoxObject;
                if (dr["e18"].ToString().Equals("True"))
                {
                    CheckBox36.Checked = true;
                    CheckBox36.Border.Color = Color.Red;
                    CheckBox35.Checked = true;
                    CheckBox35.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox36.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox37 = report.FindObject("CheckBox37") as FastReport.CheckBoxObject;
                if (dr["e19"].ToString().Equals("True"))
                {
                    CheckBox37.Checked = true;
                    CheckBox37.Border.Color = Color.Red;
                    CheckBox35.Checked = true;
                    CheckBox35.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox37.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox38 = report.FindObject("CheckBox38") as FastReport.CheckBoxObject;
                if (dr["e20"].ToString().Equals("True"))
                {
                    CheckBox38.Checked = true;
                    CheckBox38.Border.Color = Color.Red;
                    CheckBox35.Checked = true;
                    CheckBox35.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox38.Checked = false;
                }

                //
                FastReport.CheckBoxObject CheckBox39 = report.FindObject("CheckBox39") as FastReport.CheckBoxObject;
                if (dr["e21"].ToString().Equals("True"))
                {
                    CheckBox39.Checked = true;
                    CheckBox39.Border.Color = Color.Red;
                    CheckBox35.Checked = true;
                    CheckBox35.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox39.Checked = false;
                }
                // 
                FastReport.CheckBoxObject CheckBox42 = report.FindObject("CheckBox42") as FastReport.CheckBoxObject;
                if (dr["e22"].ToString().Equals("True"))
                {
                    CheckBox42.Checked = true;
                    CheckBox42.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox42.Checked = false;
                }
                //

                FastReport.CheckBoxObject CheckBox41 = report.FindObject("CheckBox41") as FastReport.CheckBoxObject;
                FastReport.CheckBoxObject CheckBox45 = report.FindObject("CheckBox45") as FastReport.CheckBoxObject;
                if (dr["e2"].ToString().Equals("True"))
                {
                    CheckBox45.Checked = true;
                    CheckBox45.Border.Color = Color.Red;
                    CheckBox41.Checked = true;
                    CheckBox41.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox45.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox44 = report.FindObject("CheckBox44") as FastReport.CheckBoxObject;
                if (dr["e24"].ToString().Equals("True"))
                {
                    CheckBox44.Checked = true;
                    CheckBox44.Border.Color = Color.Red;
                    CheckBox41.Checked = true;
                    CheckBox41.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox44.Checked = false;
                }
                //

                FastReport.CheckBoxObject CheckBox46 = report.FindObject("CheckBox46") as FastReport.CheckBoxObject;
                FastReport.CheckBoxObject CheckBox47 = report.FindObject("CheckBox47") as FastReport.CheckBoxObject;
                if (dr["e25"].ToString().Equals("True"))
                {
                    CheckBox47.Checked = true;
                    CheckBox47.Border.Color = Color.Red;
                    CheckBox46.Checked = true;
                    CheckBox46.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox47.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox48 = report.FindObject("CheckBox48") as FastReport.CheckBoxObject;
                if (dr["e26"].ToString().Equals("True"))
                {
                    CheckBox48.Checked = true;
                    CheckBox48.Border.Color = Color.Red;
                    CheckBox46.Checked = true;
                    CheckBox46.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox48.Checked = false;
                }
                //

                FastReport.CheckBoxObject CheckBox50 = report.FindObject("CheckBox50") as FastReport.CheckBoxObject;
                FastReport.CheckBoxObject CheckBox51 = report.FindObject("CheckBox51") as FastReport.CheckBoxObject;
                if (dr["e27"].ToString().Equals("True"))
                {
                    CheckBox51.Checked = true;
                    CheckBox51.Border.Color = Color.Red;
                    CheckBox50.Checked = true;
                    CheckBox50.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox51.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox52 = report.FindObject("CheckBox52") as FastReport.CheckBoxObject;
                if (dr["e28"].ToString().Equals("True"))
                {
                    CheckBox52.Checked = true;
                    CheckBox52.Border.Color = Color.Red;
                    CheckBox50.Checked = true;
                    CheckBox50.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox52.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox53 = report.FindObject("CheckBox53") as FastReport.CheckBoxObject;
                if (dr["e29"].ToString().Equals("True"))
                {
                    CheckBox53.Checked = true;
                    CheckBox53.Border.Color = Color.Red;
                    CheckBox50.Checked = true;
                    CheckBox50.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox53.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox54 = report.FindObject("CheckBox54") as FastReport.CheckBoxObject;
                if (dr["e30"].ToString().Equals("True"))
                {
                    CheckBox54.Checked = true;
                    CheckBox54.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox54.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox55 = report.FindObject("CheckBox55") as FastReport.CheckBoxObject;
                if (dr["e31"].ToString().Equals("True"))
                {
                    CheckBox55.Checked = true;
                    CheckBox55.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox55.Checked = false;
                }
                //
                FastReport.CheckBoxObject CheckBox57 = report.FindObject("CheckBox57") as FastReport.CheckBoxObject;
                if (dr["e32"].ToString().Equals("True"))
                {
                    CheckBox57.Checked = true;
                    CheckBox57.Border.Color = Color.Red;
                }
                else
                {
                    CheckBox57.Checked = false;
                }
            }


            FastReport.TextObject txt_Zljy = report.FindObject("Txt_Zljy") as FastReport.TextObject;
            txt_Zljy.Text = InsM.Txt_Zljy;
            FastReport.TextObject Txt_Bgrq = report.FindObject("Txt_Bgrq") as FastReport.TextObject;
            Txt_Bgrq.Text = InsM.Txt_Bgrq;
            FastReport.TextObject Txt_Bgys = report.FindObject("Txt_Bgys") as FastReport.TextObject;
            Txt_Bgys.Text = InsM.Txt_Bgys;
            if (!InsM.fileImages.Equals(""))
            {
                string[] imgs = InsM.fileImages.Split('|');
                if (imgs.Length > 0)
                {
                    FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                    Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                }
            }

            //不弹出打印设置框
            report.PrintSettings.ShowDialog = false;
            report.Prepare();
            report.Print();
            FastReport.Export.Image.ImageExport insExp = new FastReport.Export.Image.ImageExport();
            insExp.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
            report.Export(insExp, @expFilePath);
            return true;
        }


        //HPV报告预览
        public void LoadHPVPreviewer(string ReportPrinter, int PrintReportNum, string exam_no, string study_no, string ImagePath, string CurImage, string hospitalName, string parts, string xbms, string Lczd, string report_date, string report_type)
        {

            //注册数据源
            DBHelper.BLL.exam_master Ins_Mas = new DBHelper.BLL.exam_master();
            DataSet Ds = Ins_Mas.GetDs(exam_no, study_no);
            if (Ds != null && Ds.Tables[0].Rows.Count == 1)
            {
                Ds.Tables[0].TableName = "exam_master_view";
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                //创建fastreport报表实例
                FastReport.Report report = new FastReport.Report();
                if (report_type == "0")
                {
                    report.Load(APPdirPath + @"\Report\HPV12-2.frx");
                }
                else if (report_type == "1")
                {
                    report.Load(APPdirPath + @"\Report\HPV-21.frx");
                }
                else if (report_type == "2")
                {
                    report.Load(APPdirPath + @"\Report\HPV.frx");
                }

                //设置默认打印机
                if (!ReportPrinter.Equals(""))
                {
                    report.PrintSettings.Printer = ReportPrinter;
                }
                //打印份数
                report.PrintSettings.Copies = PrintReportNum;
                //参数设置
                report.SetParameterValue("ReportParaHospital", hospitalName);
                //送检标本
                FastReport.TextObject Txt_Parts = report.FindObject("Text40") as FastReport.TextObject;
                Txt_Parts.Text = parts;
                //报告日期 
                FastReport.TextObject Txt_date = report.FindObject("Txt_Sjbw") as FastReport.TextObject;
                Txt_date.Text = report_date;
                //临床诊断
                FastReport.TextObject Txt_lczd = report.FindObject("Text42") as FastReport.TextObject;
                Txt_lczd.Text = Lczd;
                //注册数据源
                report.RegisterData(Ds);
                //赋值诊断
                string[] values = xbms.Split('|');
                if (report_type == "0")
                {
                    if (values[0].Equals(""))
                    {
                        FastReport.TextObject Txt_17 = report.FindObject("Text17") as FastReport.TextObject;
                        Txt_17.Text = "未检测到该类型的HPV感染";
                    }
                    else
                    {
                        FastReport.TextObject Txt_17 = report.FindObject("Text17") as FastReport.TextObject;
                        if (Convert.ToSingle(values[0]) <= 40)
                        {
                            Txt_17.Text = "阳性";
                            Txt_17.TextColor = Color.Red;
                        }
                        else if (Convert.ToSingle(values[0]) >= 40)
                        {
                            Txt_17.Text = "阴性";
                        }
                        else
                        {
                            Txt_17.Text = "未检测到该类型的HPV感染";
                        }
                        FastReport.TextObject Txt_47 = report.FindObject("Text47") as FastReport.TextObject;
                        Txt_47.Text = values[0];
                    }

                    if (values[1].Equals(""))
                    {
                        FastReport.TextObject Txt_45 = report.FindObject("Text45") as FastReport.TextObject;
                        Txt_45.Text = "未检测到该类型的HPV感染";
                    }
                    else
                    {
                        FastReport.TextObject Txt_45 = report.FindObject("Text45") as FastReport.TextObject;
                        if (Convert.ToSingle(values[1]) <= 40)
                        {
                            Txt_45.Text = "阳性";
                            Txt_45.TextColor = Color.Red;
                        }
                        else if (Convert.ToSingle(values[1]) >= 40)
                        {
                            Txt_45.Text = "阴性";
                        }
                        else
                        {
                            Txt_45.Text = "未检测到该类型的HPV感染";
                        }
                        FastReport.TextObject Txt_48 = report.FindObject("Text48") as FastReport.TextObject;
                        Txt_48.Text = values[1];
                    }
                    if (values[2].Equals(""))
                    {
                        FastReport.TextObject Txt_46 = report.FindObject("Text46") as FastReport.TextObject;
                        Txt_46.Text = "未检测到该类型的HPV感染";
                    }
                    else
                    {
                        FastReport.TextObject Txt_46 = report.FindObject("Text46") as FastReport.TextObject;
                        if (Convert.ToSingle(values[2]) <= 40)
                        {
                            Txt_46.Text = "阳性";
                            Txt_46.TextColor = Color.Red;
                        }
                        else if (Convert.ToSingle(values[2]) >= 40)
                        {
                            Txt_46.Text = "阴性";
                        }
                        else
                        {
                            Txt_46.Text = "未检测到该类型的HPV感染";
                        }
                        FastReport.TextObject Txt_49 = report.FindObject("Text49") as FastReport.TextObject;
                        Txt_49.Text = values[2];
                    }

                }
                else if (report_type == "2")
                {
                    FastReport.TextObject Txt_15 = report.FindObject("Text15") as FastReport.TextObject;
                    Txt_15.Text = values[0];
                    if (values[0].Equals("阳性(+)"))
                    {
                        Txt_15.TextColor = Color.Red;
                    }
                    FastReport.TextObject Txt_45 = report.FindObject("Text45") as FastReport.TextObject;
                    Txt_45.Text = values[1];
                    if (values[1].Equals("(+)"))
                    {
                        Txt_45.TextColor = Color.Red;
                    }
                    FastReport.TextObject Txt_46 = report.FindObject("Text46") as FastReport.TextObject;
                    Txt_46.Text = values[2];
                    if (values[2].Equals("(+)"))
                    {
                        Txt_46.TextColor = Color.Red;
                    }
                    FastReport.TextObject Txt_47 = report.FindObject("Text47") as FastReport.TextObject;
                    Txt_47.Text = values[3];
                    if (values[3].Equals("(+)"))
                    {
                        Txt_47.TextColor = Color.Red;
                    }
                }
                else if (report_type == "1")
                {
                    if (xbms.Equals(""))
                    {
                        FastReport.TextObject Txt_Blzd = report.FindObject("Txt_Blzd") as FastReport.TextObject;
                        Txt_Blzd.Text = "未发现人乳头状瘤病毒感染";
                        FastReport.TextObject Txt_g = report.FindObject("Text44") as FastReport.TextObject;
                        Txt_g.Text = "阴性";
                        FastReport.TextObject Txt_d = report.FindObject("Text46") as FastReport.TextObject;
                        Txt_d.Text = "阴性";
                    }
                    else
                    {
                        FastReport.TextObject Txt_Blzd = report.FindObject("Txt_Blzd") as FastReport.TextObject;
                        Txt_Blzd.Text = "发现人乳头状瘤病毒感染";
                        string gw = "";
                        string dw = "";

                        for (int i = 0; i < values.Length; i++)
                        {
                            if (lstgw.Contains(values[i]))
                            {
                                gw = gw.Equals("") ? values[i] : (gw + "," + values[i]);
                            }
                            else if (lstdw.Contains(values[i]))
                            {
                                dw = dw.Equals("") ? values[i] : (dw + "," + values[i]);
                            }
                        }

                        if (gw.Equals(""))
                        {
                            FastReport.TextObject Txt_g = report.FindObject("Text44") as FastReport.TextObject;
                            Txt_g.Text = "阴性";
                        }
                        else
                        {
                            FastReport.TextObject Txt_g = report.FindObject("Text44") as FastReport.TextObject;
                            Txt_g.Text = "阳性(" + gw + ")";
                            Txt_g.TextColor = Color.Red;
                        }
                        if (dw.Equals(""))
                        {
                            FastReport.TextObject Txt_d = report.FindObject("Text46") as FastReport.TextObject;
                            Txt_d.Text = "阴性";
                        }
                        else
                        {
                            FastReport.TextObject Txt_d = report.FindObject("Text46") as FastReport.TextObject;
                            Txt_d.Text = "阳性(" + dw + ")";
                            Txt_d.TextColor = Color.Red;
                        }
                    }

                }
                //previewControl1.Zoom = 0.93f;
                report.Preview = this.previewControl1;
                if (!report.IsRunning)
                {
                    report.Show();
                }
            }
        }

        //HPV类型报告打印
        public Boolean DirectPrintHPVReport(string ReportPrinter, int PrintReportNum, string exam_no, string study_no, string ImagePath, string CurImage, string hospitalName, string parts, string xbms, string Lczd, string report_date, string report_type, string expFilePath)
        {
            //注册数据源
            DBHelper.BLL.exam_master Ins_Mas = new DBHelper.BLL.exam_master();
            DataSet Ds = Ins_Mas.GetDs(exam_no, study_no);
            if (Ds != null && Ds.Tables[0].Rows.Count == 1)
            {
                Ds.Tables[0].TableName = "exam_master_view";
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                //创建fastreport报表实例
                FastReport.Report report = new FastReport.Report();
                if (report_type == "0")
                {
                    report.Load(APPdirPath + @"\Report\HPV12-2.frx");
                }
                else if (report_type == "1")
                {
                    report.Load(APPdirPath + @"\Report\HPV-21.frx");
                }
                else if (report_type == "2")
                {
                    report.Load(APPdirPath + @"\Report\HPV.frx");
                }

                //设置默认打印机
                if (!ReportPrinter.Equals(""))
                {
                    report.PrintSettings.Printer = ReportPrinter;
                }
                //打印份数
                report.PrintSettings.Copies = PrintReportNum;
                //参数设置
                report.SetParameterValue("ReportParaHospital", hospitalName);
                //送检标本
                FastReport.TextObject Txt_Parts = report.FindObject("Text40") as FastReport.TextObject;
                Txt_Parts.Text = parts;
                //报告日期 
                FastReport.TextObject Txt_date = report.FindObject("Txt_Sjbw") as FastReport.TextObject;
                Txt_date.Text = report_date;
                //临床诊断
                FastReport.TextObject Txt_lczd = report.FindObject("Text42") as FastReport.TextObject;
                Txt_lczd.Text = Lczd;
                //注册数据源
                report.RegisterData(Ds);
                //赋值诊断
                string[] values = xbms.Split('|');
                if (report_type == "0")
                {
                    if (values[0].Equals(""))
                    {
                        FastReport.TextObject Txt_17 = report.FindObject("Text17") as FastReport.TextObject;
                        Txt_17.Text = "未检测到该类型的HPV感染";
                    }
                    else
                    {
                        FastReport.TextObject Txt_17 = report.FindObject("Text17") as FastReport.TextObject;
                        if (Convert.ToSingle(values[0]) <= 40)
                        {
                            Txt_17.Text = "阳性";
                            Txt_17.TextColor = Color.Red;
                        }
                        else if (Convert.ToSingle(values[0]) >= 40)
                        {
                            Txt_17.Text = "阴性";
                        }
                        else
                        {
                            Txt_17.Text = "未检测到该类型的HPV感染";
                        }
                        FastReport.TextObject Txt_47 = report.FindObject("Text47") as FastReport.TextObject;
                        Txt_47.Text = values[0];
                    }

                    if (values[1].Equals(""))
                    {
                        FastReport.TextObject Txt_45 = report.FindObject("Text45") as FastReport.TextObject;
                        Txt_45.Text = "未检测到该类型的HPV感染";
                    }
                    else
                    {
                        FastReport.TextObject Txt_45 = report.FindObject("Text45") as FastReport.TextObject;
                        if (Convert.ToSingle(values[1]) <= 40)
                        {
                            Txt_45.Text = "阳性";
                            Txt_45.TextColor = Color.Red;
                        }
                        else if (Convert.ToSingle(values[1]) >= 40)
                        {
                            Txt_45.Text = "阴性";
                        }
                        else
                        {
                            Txt_45.Text = "未检测到该类型的HPV感染";
                        }
                        FastReport.TextObject Txt_48 = report.FindObject("Text48") as FastReport.TextObject;
                        Txt_48.Text = values[1];
                    }
                    if (values[2].Equals(""))
                    {
                        FastReport.TextObject Txt_46 = report.FindObject("Text46") as FastReport.TextObject;
                        Txt_46.Text = "未检测到该类型的HPV感染";
                    }
                    else
                    {
                        FastReport.TextObject Txt_46 = report.FindObject("Text46") as FastReport.TextObject;
                        if (Convert.ToSingle(values[2]) <= 40)
                        {
                            Txt_46.Text = "阳性";
                            Txt_46.TextColor = Color.Red;
                        }
                        else if (Convert.ToSingle(values[2]) >= 40)
                        {
                            Txt_46.Text = "阴性";
                        }
                        else
                        {
                            Txt_46.Text = "未检测到该类型的HPV感染";
                        }
                        FastReport.TextObject Txt_49 = report.FindObject("Text49") as FastReport.TextObject;
                        Txt_49.Text = values[2];
                    }

                }
                else if (report_type == "2")
                {
                    FastReport.TextObject Txt_15 = report.FindObject("Text15") as FastReport.TextObject;
                    Txt_15.Text = values[0];
                    if (values[0].Equals("阳性(+)"))
                    {
                        Txt_15.TextColor = Color.Red;
                    }
                    FastReport.TextObject Txt_45 = report.FindObject("Text45") as FastReport.TextObject;
                    Txt_45.Text = values[1];
                    if (values[1].Equals("(+)"))
                    {
                        Txt_45.TextColor = Color.Red;
                    }
                    FastReport.TextObject Txt_46 = report.FindObject("Text46") as FastReport.TextObject;
                    Txt_46.Text = values[2];
                    if (values[2].Equals("(+)"))
                    {
                        Txt_46.TextColor = Color.Red;
                    }
                    FastReport.TextObject Txt_47 = report.FindObject("Text47") as FastReport.TextObject;
                    Txt_47.Text = values[3];
                    if (values[3].Equals("(+)"))
                    {
                        Txt_47.TextColor = Color.Red;
                    }
                }
                else if (report_type == "1")
                {
                    if (xbms.Equals(""))
                    {
                        FastReport.TextObject Txt_Blzd = report.FindObject("Txt_Blzd") as FastReport.TextObject;
                        Txt_Blzd.Text = "未发现人乳头状瘤病毒感染";
                        FastReport.TextObject Txt_g = report.FindObject("Text44") as FastReport.TextObject;
                        Txt_g.Text = "阴性";
                        FastReport.TextObject Txt_d = report.FindObject("Text46") as FastReport.TextObject;
                        Txt_d.Text = "阴性";
                    }
                    else
                    {
                        FastReport.TextObject Txt_Blzd = report.FindObject("Txt_Blzd") as FastReport.TextObject;
                        Txt_Blzd.Text = "发现人乳头状瘤病毒感染";
                        string gw = "";
                        string dw = "";

                        for (int i = 0; i < values.Length; i++)
                        {
                            if (lstgw.Contains(values[i]))
                            {
                                gw = gw.Equals("") ? values[i] : (gw + "," + values[i]);
                            }
                            else if (lstdw.Contains(values[i]))
                            {
                                dw = dw.Equals("") ? values[i] : (dw + "," + values[i]);
                            }
                        }

                        if (gw.Equals(""))
                        {
                            FastReport.TextObject Txt_g = report.FindObject("Text44") as FastReport.TextObject;
                            Txt_g.Text = "阴性";
                        }
                        else
                        {
                            FastReport.TextObject Txt_g = report.FindObject("Text44") as FastReport.TextObject;
                            Txt_g.Text = "阳性(" + gw + ")";
                            Txt_g.TextColor = Color.Red;
                        }
                        if (dw.Equals(""))
                        {
                            FastReport.TextObject Txt_d = report.FindObject("Text46") as FastReport.TextObject;
                            Txt_d.Text = "阴性";
                        }
                        else
                        {
                            FastReport.TextObject Txt_d = report.FindObject("Text46") as FastReport.TextObject;
                            Txt_d.Text = "阳性(" + dw + ")";
                            Txt_d.TextColor = Color.Red;
                        }
                    }

                }
                //不弹出打印设置框
                report.PrintSettings.ShowDialog = false;
                report.Prepare();
                report.Print();
                FastReport.Export.Image.ImageExport insExp = new FastReport.Export.Image.ImageExport();
                insExp.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
                report.Export(insExp, @expFilePath);
                return true;
            }
            else
            {
                return false;
            }
        }
        private DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        //分子报告预览
        public void LoadFZPreviewer(string ReportPrinter, int PrintReportNum, string exam_no, string study_no, string ImagePath, string CurImage, string hospitalName, string parts, string xbms, string Lczd, string report_date, string report_type)
        {

            //注册数据源
            DBHelper.BLL.exam_master Ins_Mas = new DBHelper.BLL.exam_master();
            DataSet Ds = Ins_Mas.GetDs(exam_no, study_no);
            if (Ds != null && Ds.Tables[0].Rows.Count == 1)
            {
                Ds.Tables[0].TableName = "exam_master_view";
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                //创建fastreport报表实例
                FastReport.Report report = new FastReport.Report();
                if (report_type == "0")
                {
                    report.Load(APPdirPath + @"\Report\FZ0ReportA4.frx");
                }
                else if (report_type == "1")
                {
                    report.Load(APPdirPath + @"\Report\FZ1ReportA4.frx");
                }
                //设置默认打印机
                if (!ReportPrinter.Equals(""))
                {
                    report.PrintSettings.Printer = ReportPrinter;
                }
                //打印份数
                report.PrintSettings.Copies = PrintReportNum;
                //参数设置
                report.SetParameterValue("ReportParaHospital", hospitalName);
                //送检标本
                FastReport.TextObject Txt_Parts = report.FindObject("Txt_Sjbw") as FastReport.TextObject;
                Txt_Parts.Text = parts;
                //报告日期 
                FastReport.TextObject Txt_date = report.FindObject("txt_Bgrq") as FastReport.TextObject;
                Txt_date.Text = report_date.Substring(0, 10);
                //注册数据源
                report.RegisterData(Ds);
                //赋值诊断
                DataSet ds = ConvertXMLToDataSet(xbms);
                if (ds == null)
                {
                    return;
                }
                DataRow dr = ds.Tables[0].Rows[0];
                if (report_type == "0")
                {

                    //临床诊断
                    FastReport.TextObject txt_lczd = report.FindObject("txt_lczd") as FastReport.TextObject;
                    txt_lczd.Text = dr["lczd"].ToString();
                    //病理诊断
                    FastReport.TextObject txt_blzd = report.FindObject("txt_blzd") as FastReport.TextObject;
                    txt_blzd.Text = dr["blzd"].ToString();
                    //her2 
                    FastReport.TextObject Text15 = report.FindObject("Text15") as FastReport.TextObject;
                    Text15.Text = dr["her2"].ToString();
                    //肿瘤异性
                    FastReport.TextObject Text40 = report.FindObject("Text40") as FastReport.TextObject;
                    Text40.Text = dr["zlyx"].ToString();
                    //herfb1
                    if (dr["herfb1"].ToString().Equals("True"))
                    {
                        FastReport.CheckBoxObject CheckBox3 = report.FindObject("CheckBox3") as FastReport.CheckBoxObject;
                        CheckBox3.Border.Color = Color.Red;
                        CheckBox3.Checked = true;
                    }

                    //herfb2
                    if (dr["herfb2"].ToString().Equals("True"))
                    {
                        FastReport.CheckBoxObject CheckBox4 = report.FindObject("CheckBox4") as FastReport.CheckBoxObject;
                        CheckBox4.Border.Color = Color.Red;
                        CheckBox4.Checked = true;
                    }

                    //信号
                    FastReport.TextObject Text47 = report.FindObject("Text47") as FastReport.TextObject;
                    Text47.Text = dr["xh"].ToString();
                    //cep
                    FastReport.TextObject Text49 = report.FindObject("Text49") as FastReport.TextObject;
                    Text49.Text = dr["cep"].ToString();
                    //51
                    FastReport.TextObject Text51 = report.FindObject("Text51") as FastReport.TextObject;
                    if (!dr["xh"].ToString().Equals("") && !dr["cep"].ToString().Equals(""))
                    {
                        double valueCur = Convert.ToDouble(dr["xh"].ToString()) / Convert.ToDouble(dr["cep"].ToString());
                        Text51.Text = Math.Round(valueCur, 1).ToString();
                    }
                    //阳性扩
                    if (dr["yangk"].ToString().Equals("True"))
                    {
                        FastReport.CheckBoxObject CheckBox5 = report.FindObject("CheckBox5") as FastReport.CheckBoxObject;
                        CheckBox5.Border.Color = Color.Red;
                        CheckBox5.Checked = true;
                    }
                    //可疑
                    if (dr["ky"].ToString().Equals("True"))
                    {
                        FastReport.CheckBoxObject CheckBox6 = report.FindObject("CheckBox6") as FastReport.CheckBoxObject;
                        CheckBox6.Border.Color = Color.Red;
                        CheckBox6.Checked = true;
                    }
                    //阴扩
                    if (dr["yink"].ToString().Equals("True"))
                    {
                        FastReport.CheckBoxObject CheckBox7 = report.FindObject("CheckBox7") as FastReport.CheckBoxObject;
                        CheckBox7.Border.Color = Color.Red;
                        CheckBox7.Checked = true;
                    }
                    //无法判读
                    if (dr["wfpd"].ToString().Equals("True"))
                    {
                        FastReport.CheckBoxObject CheckBox8 = report.FindObject("CheckBox8") as FastReport.CheckBoxObject;
                        CheckBox8.Border.Color = Color.Red;
                        CheckBox8.Checked = true;
                    }

                }
                else if (report_type == "1")
                {
                    //组织病理号
                    FastReport.TextObject Text34 = report.FindObject("Text34") as FastReport.TextObject;
                    Text34.Text = dr["zzblh"].ToString();
                    //临床诊断
                    FastReport.TextObject txt_lczd = report.FindObject("txt_lczd") as FastReport.TextObject;
                    txt_lczd.Text = dr["lczd"].ToString();
                    //病理诊断
                    FastReport.TextObject txt_blzd = report.FindObject("txt_blzd") as FastReport.TextObject;
                    txt_blzd.Text = dr["blzd"].ToString();
                    //检测项目
                    FastReport.TextObject Text12 = report.FindObject("Text12") as FastReport.TextObject;
                    Text12.Text = dr["jcxm"].ToString();
                    //E18 
                    FastReport.Table.TableCell Cell7 = report.FindObject("Cell7") as FastReport.Table.TableCell;
                    Cell7.Text = dr["e18"].ToString();
                    //e19
                    FastReport.Table.TableCell Cell12 = report.FindObject("Cell12") as FastReport.Table.TableCell;
                    Cell12.Text = dr["e19"].ToString();
                    //e20
                    FastReport.Table.TableCell Cell17 = report.FindObject("Cell17") as FastReport.Table.TableCell;
                    Cell17.Text = dr["e20"].ToString();
                    //e21
                    FastReport.Table.TableCell Cell22 = report.FindObject("Cell22") as FastReport.Table.TableCell;
                    Cell22.Text = dr["e21"].ToString();
                    //ke2
                    FastReport.Table.TableCell Cell25 = report.FindObject("Cell25") as FastReport.Table.TableCell;
                    Cell25.Text = dr["ke2"].ToString();
                    //ke3
                    FastReport.Table.TableCell Cell28 = report.FindObject("Cell28") as FastReport.Table.TableCell;
                    Cell28.Text = dr["ke3"].ToString();

                    //bv6
                    FastReport.Table.TableCell Cell31 = report.FindObject("Cell31") as FastReport.Table.TableCell;
                    Cell31.Text = dr["bv6"].ToString();
                }
                //previewControl1.Zoom = 0.93f;
                report.Preview = this.previewControl1;
                if (!report.IsRunning)
                {
                    report.Show();
                }
            }
        }


        //分子病理类型报告打印
        public Boolean DirectPrintFZReport(string ReportPrinter, int PrintReportNum, string exam_no, string study_no, string ImagePath, string CurImage, string hospitalName, string parts, string xbms, string Lczd, string report_date, string report_type, string expFilePath)
        {
            //注册数据源
            DBHelper.BLL.exam_master Ins_Mas = new DBHelper.BLL.exam_master();
            DataSet Ds = Ins_Mas.GetDs(exam_no, study_no);
            if (Ds != null && Ds.Tables[0].Rows.Count == 1)
            {
                Ds.Tables[0].TableName = "exam_master_view";
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                //创建fastreport报表实例
                FastReport.Report report = new FastReport.Report();
                if (report_type == "0")
                {
                    report.Load(APPdirPath + @"\Report\FZ0ReportA4.frx");
                }
                else if (report_type == "1")
                {
                    report.Load(APPdirPath + @"\Report\FZ1ReportA4.frx");
                }
                //设置默认打印机
                if (!ReportPrinter.Equals(""))
                {
                    report.PrintSettings.Printer = ReportPrinter;
                }
                //打印份数
                report.PrintSettings.Copies = PrintReportNum;
                //参数设置
                report.SetParameterValue("ReportParaHospital", hospitalName);
                //送检标本
                FastReport.TextObject Txt_Parts = report.FindObject("Txt_Sjbw") as FastReport.TextObject;
                Txt_Parts.Text = parts;
                //报告日期 
                FastReport.TextObject Txt_date = report.FindObject("txt_Bgrq") as FastReport.TextObject;
                Txt_date.Text = report_date.Substring(0, 10);
                //注册数据源
                report.RegisterData(Ds);
                //赋值诊断
                DataSet ds = ConvertXMLToDataSet(xbms);
                if (ds == null)
                {
                    return false;
                }
                DataRow dr = ds.Tables[0].Rows[0];
                if (report_type == "0")
                {

                    //临床诊断
                    FastReport.TextObject txt_lczd = report.FindObject("txt_lczd") as FastReport.TextObject;
                    txt_lczd.Text = dr["lczd"].ToString();
                    //病理诊断
                    FastReport.TextObject txt_blzd = report.FindObject("txt_blzd") as FastReport.TextObject;
                    txt_blzd.Text = dr["blzd"].ToString();
                    //her2 
                    FastReport.TextObject Text15 = report.FindObject("Text15") as FastReport.TextObject;
                    Text15.Text = dr["her2"].ToString();
                    //肿瘤异性
                    FastReport.TextObject Text40 = report.FindObject("Text40") as FastReport.TextObject;
                    Text40.Text = dr["zlyx"].ToString();
                    //herfb1
                    if (dr["herfb1"].ToString().Equals("True"))
                    {
                        FastReport.CheckBoxObject CheckBox3 = report.FindObject("CheckBox3") as FastReport.CheckBoxObject;
                        CheckBox3.Border.Color = Color.Red;
                        CheckBox3.Checked = true;
                    }

                    //herfb2
                    if (dr["herfb2"].ToString().Equals("True"))
                    {
                        FastReport.CheckBoxObject CheckBox4 = report.FindObject("CheckBox4") as FastReport.CheckBoxObject;
                        CheckBox4.Border.Color = Color.Red;
                        CheckBox4.Checked = true;
                    }

                    //信号
                    FastReport.TextObject Text47 = report.FindObject("Text47") as FastReport.TextObject;
                    Text47.Text = dr["xh"].ToString();
                    //cep
                    FastReport.TextObject Text49 = report.FindObject("Text49") as FastReport.TextObject;
                    Text49.Text = dr["cep"].ToString();
                    //51
                    FastReport.TextObject Text51 = report.FindObject("Text51") as FastReport.TextObject;
                    if (!dr["xh"].ToString().Equals("") && !dr["cep"].ToString().Equals(""))
                    {
                        double valueCur = Convert.ToDouble(dr["xh"].ToString()) / Convert.ToDouble(dr["cep"].ToString());
                        Text51.Text = Math.Round(valueCur, 1).ToString();
                    }
                    //阳性扩
                    if (dr["yangk"].ToString().Equals("True"))
                    {
                        FastReport.CheckBoxObject CheckBox5 = report.FindObject("CheckBox5") as FastReport.CheckBoxObject;
                        CheckBox5.Border.Color = Color.Red;
                        CheckBox5.Checked = true;
                    }
                    //可疑
                    if (dr["ky"].ToString().Equals("True"))
                    {
                        FastReport.CheckBoxObject CheckBox6 = report.FindObject("CheckBox6") as FastReport.CheckBoxObject;
                        CheckBox6.Border.Color = Color.Red;
                        CheckBox6.Checked = true;
                    }
                    //阴扩
                    if (dr["yink"].ToString().Equals("True"))
                    {
                        FastReport.CheckBoxObject CheckBox7 = report.FindObject("CheckBox7") as FastReport.CheckBoxObject;
                        CheckBox7.Border.Color = Color.Red;
                        CheckBox7.Checked = true;
                    }
                    //无法判读
                    if (dr["wfpd"].ToString().Equals("True"))
                    {
                        FastReport.CheckBoxObject CheckBox8 = report.FindObject("CheckBox8") as FastReport.CheckBoxObject;
                        CheckBox8.Border.Color = Color.Red;
                        CheckBox8.Checked = true;
                    }

                }
                else if (report_type == "1")
                {
                    //组织病理号
                    FastReport.TextObject Text34 = report.FindObject("Text34") as FastReport.TextObject;
                    Text34.Text = dr["zzblh"].ToString();
                    //临床诊断
                    FastReport.TextObject txt_lczd = report.FindObject("txt_lczd") as FastReport.TextObject;
                    txt_lczd.Text = dr["lczd"].ToString();
                    //病理诊断
                    FastReport.TextObject txt_blzd = report.FindObject("txt_blzd") as FastReport.TextObject;
                    txt_blzd.Text = dr["blzd"].ToString();
                    //检测项目
                    FastReport.TextObject Text12 = report.FindObject("Text12") as FastReport.TextObject;
                    Text12.Text = dr["jcxm"].ToString();
                    //E18 
                    FastReport.Table.TableCell Cell7 = report.FindObject("Cell7") as FastReport.Table.TableCell;
                    Cell7.Text = dr["e18"].ToString();
                    //e19
                    FastReport.Table.TableCell Cell12 = report.FindObject("Cell12") as FastReport.Table.TableCell;
                    Cell12.Text = dr["e19"].ToString();
                    //e20
                    FastReport.Table.TableCell Cell17 = report.FindObject("Cell17") as FastReport.Table.TableCell;
                    Cell17.Text = dr["e20"].ToString();
                    //e21
                    FastReport.Table.TableCell Cell22 = report.FindObject("Cell22") as FastReport.Table.TableCell;
                    Cell22.Text = dr["e21"].ToString();
                    //ke2
                    FastReport.Table.TableCell Cell25 = report.FindObject("Cell25") as FastReport.Table.TableCell;
                    Cell25.Text = dr["ke2"].ToString();
                    //ke3
                    FastReport.Table.TableCell Cell28 = report.FindObject("Cell28") as FastReport.Table.TableCell;
                    Cell28.Text = dr["ke3"].ToString();

                    //bv6
                    FastReport.Table.TableCell Cell31 = report.FindObject("Cell31") as FastReport.Table.TableCell;
                    Cell31.Text = dr["bv6"].ToString();
                }
                //不弹出打印设置框
                report.PrintSettings.ShowDialog = false;
                report.Prepare();
                report.Print();
                FastReport.Export.Image.ImageExport insExp = new FastReport.Export.Image.ImageExport();
                insExp.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
                report.Export(insExp, @expFilePath);
                return true;
            }
            else
            {
                return false;
            }
        }






        //外院会诊报告打印
        public Boolean DirectPrintWyHzReport(string ReportPrinter, int PrintReportNum, string exam_no, string study_no, string ImagePath, string CurImage, string hospitalName, string parts, string zdyj, string wy_study_no, string sjdw, int lk_num, int bp_num, string report_date, string expFilePath)
        {
            //注册数据源
            DBHelper.BLL.exam_master Ins_Mas = new DBHelper.BLL.exam_master();
            DataSet Ds = Ins_Mas.GetDs(exam_no, study_no);
            if (Ds != null && Ds.Tables[0].Rows.Count == 1)
            {
                Ds.Tables[0].TableName = "exam_master_view";
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                //创建fastreport报表实例
                FastReport.Report report = new FastReport.Report();
                //导入设计好的报表
                if (!CurImage.Equals(""))
                {
                    string[] imgs = CurImage.Split('|');
                    //加载报告
                    if (imgs.Length == 1)
                    {
                        report.Load(APPdirPath + @"\Report\hzpic1ReportA4.frx");
                        FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                        Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                    }
                    else if (imgs.Length >= 2)
                    {
                        report.Load(APPdirPath + @"\Report\hzReportA4.frx");
                        if (imgs[0].IndexOf("DT") != -1)
                        {
                            FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                            Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                            FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                            Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[1]);
                        }
                        else
                        {
                            FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                            Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[1]);
                            FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                            Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                        }
                    }
                }
                else
                {
                    report.Load(APPdirPath + @"\Report\hznopicReportA4.frx");
                }
                //设置默认打印机
                if (!ReportPrinter.Equals(""))
                {
                    report.PrintSettings.Printer = ReportPrinter;
                }
                //打印份数
                report.PrintSettings.Copies = PrintReportNum;
                //参数设置
                report.SetParameterValue("ReportParaHospital", hospitalName);
                //送检标本
                FastReport.TextObject Txt_Parts = report.FindObject("Text40") as FastReport.TextObject;
                Txt_Parts.Text = parts;
                //报告日期 
                FastReport.TextObject Txt_date = report.FindObject("Txt_Date") as FastReport.TextObject;
                Txt_date.Text = report_date;
                //注册数据源
                report.RegisterData(Ds);

                //蜡块数目
                FastReport.TextObject Txt_lks = report.FindObject("Text36") as FastReport.TextObject;
                Txt_lks.Text = lk_num.ToString();
                //玻片数目
                FastReport.TextObject Txt_bps = report.FindObject("Text38") as FastReport.TextObject;
                Txt_bps.Text = bp_num.ToString();
                //原病理号
                FastReport.TextObject Txt_yblh = report.FindObject("Txt_yblh") as FastReport.TextObject;
                Txt_yblh.Text = wy_study_no.ToString();



                //不弹出打印设置框
                report.PrintSettings.ShowDialog = false;
                report.Prepare();
                report.Print();
                FastReport.Export.Image.ImageExport insExp = new FastReport.Export.Image.ImageExport();
                insExp.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
                report.Export(insExp, @expFilePath);
                return true;
            }
            return false;
        }

        //外院会诊报告预览
        public void LoadWyHzPreviewer(string ReportPrinter, int PrintReportNum, string exam_no, string study_no, string ImagePath, string CurImage, string hospitalName, string parts, string zdyj, string wy_study_no, string sjdw, int lk_num, int bp_num, string report_date)
        {
            //注册数据源
            DBHelper.BLL.exam_master Ins_Mas = new DBHelper.BLL.exam_master();
            DataSet Ds = Ins_Mas.GetDs(exam_no, study_no);
            if (Ds != null && Ds.Tables[0].Rows.Count == 1)
            {
                Ds.Tables[0].TableName = "exam_master_view";
                FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                //创建fastreport报表实例
                FastReport.Report report = new FastReport.Report();
                //导入设计好的报表
                if (!CurImage.Equals(""))
                {
                    string[] imgs = CurImage.Split('|');
                    //加载报告
                    if (imgs.Length == 1)
                    {
                        report.Load(APPdirPath + @"\Report\hzpic1ReportA4.frx");
                        FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                        Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                    }
                    else if (imgs.Length >= 2)
                    {
                        report.Load(APPdirPath + @"\Report\hzReportA4.frx");
                        if (imgs[0].IndexOf("DT") != -1)
                        {
                            FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                            Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                            FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                            Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[1]);
                        }
                        else
                        {
                            FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                            Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[1]);
                            FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                            Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                        }
                    }
                }
                else
                {
                    report.Load(APPdirPath + @"\Report\hznopicReportA4.frx");
                }
                //设置默认打印机
                if (!ReportPrinter.Equals(""))
                {
                    report.PrintSettings.Printer = ReportPrinter;
                }
                //打印份数
                report.PrintSettings.Copies = PrintReportNum;
                //参数设置
                report.SetParameterValue("ReportParaHospital", hospitalName);
                //送检标本
                FastReport.TextObject Txt_Parts = report.FindObject("Text40") as FastReport.TextObject;
                Txt_Parts.Text = parts;
                //报告日期 
                FastReport.TextObject Txt_date = report.FindObject("Txt_Date") as FastReport.TextObject;
                Txt_date.Text = report_date;
                //注册数据源
                report.RegisterData(Ds);

                //蜡块数目
                FastReport.TextObject Txt_lks = report.FindObject("Text36") as FastReport.TextObject;
                Txt_lks.Text = lk_num.ToString();
                //玻片数目
                FastReport.TextObject Txt_bps = report.FindObject("Text38") as FastReport.TextObject;
                Txt_bps.Text = bp_num.ToString();
                //原病理号
                FastReport.TextObject Txt_yblh = report.FindObject("Txt_yblh") as FastReport.TextObject;
                Txt_yblh.Text = wy_study_no.ToString();
                //病理诊断 
                FastReport.TextObject Txt_Blzd = report.FindObject("Txt_Blzd") as FastReport.TextObject;
                Txt_Blzd.Text = zdyj.ToString();
                //previewControl1.Zoom = 0.93f;
                report.Preview = this.previewControl1;
                if (!report.IsRunning)
                {
                    report.Show();
                }
            }
        }














        public void QpListPreview(DataSet ds1, DataSet ds2, string startTime, string endTime, string ReportPrinter)
        {

            FastReport.Utils.Config.ReportSettings.ShowProgress = false;
            FastReport.Utils.Config.ReportSettings.ShowPerformance = false;

            //创建fastreport报表实例
            FastReport.Report reportQpList = new FastReport.Report();
            //导入设计好的报表
            reportQpList.Load(APPdirPath + @"\Report\Qpddy.frx");
            //设置默认打印机
            if (!ReportPrinter.Equals(""))
            {
                reportQpList.PrintSettings.Printer = ReportPrinter;
            }
            //参数设置
            reportQpList.SetParameterValue("title", string.Format("{0}至{1}待切片工作列表", startTime, endTime));

            //绑定数据源
            ds1.Tables[0].TableName = "exam_blh";
            //注册数据源
            reportQpList.RegisterData(ds1);
            //把具体数据绑定到具体的DataBand上
            FastReport.DataBand db1 = reportQpList.FindObject("Data1") as FastReport.DataBand;
            db1.DataSource = reportQpList.GetDataSource("exam_blh");

            //绑定数据源
            ds2.Tables[0].TableName = "exam_draw_meterials";
            //注册数据源
            reportQpList.RegisterData(ds2);
            //把具体数据绑定到具体的DataBand上
            FastReport.DataBand db2 = reportQpList.FindObject("Data2") as FastReport.DataBand;
            db2.DataSource = reportQpList.GetDataSource("exam_draw_meterials");
            reportQpList.Preview = this.previewControl1;
            if (!reportQpList.IsRunning)
            {

                reportQpList.Show();
            }


        }
        public void LkYjbListPreview(DataSet ds1, string startTime, string endTime, string ReportPrinter)
        {
            FastReport.Utils.Config.ReportSettings.ShowProgress = false;
            FastReport.Utils.Config.ReportSettings.ShowPerformance = false;

            //创建fastreport报表实例
            FastReport.Report reportlkyjbList = new FastReport.Report();
            //导入设计好的报表
            reportlkyjbList.Load(APPdirPath + @"\Report\lkyjb.frx");
            //设置默认打印机
            if (!ReportPrinter.Equals(""))
            {
                reportlkyjbList.PrintSettings.Printer = ReportPrinter;
            }
            //参数设置
            reportlkyjbList.SetParameterValue("title", "标本材料移交表");

            FastReport.TextObject txt_rq = reportlkyjbList.FindObject("Text3") as FastReport.TextObject;
            txt_rq.Text = string.Format("{0}至{1}", startTime, endTime);
            FastReport.TextObject txt_count = reportlkyjbList.FindObject("Text5") as FastReport.TextObject;
            int lks = 0;
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                lks += Convert.ToInt32(ds1.Tables[0].Rows[i]["lks"]);
            }
            txt_count.Text = lks.ToString();

            //绑定数据源
            ds1.Tables[0].TableName = "exam_draw_meterials";
            //注册数据源
            reportlkyjbList.RegisterData(ds1);
            //把具体数据绑定到具体的DataBand上
            FastReport.DataBand db1 = reportlkyjbList.FindObject("Data2") as FastReport.DataBand;
            db1.DataSource = reportlkyjbList.GetDataSource("exam_draw_meterials");
            reportlkyjbList.Preview = this.previewControl1;
            if (!reportlkyjbList.IsRunning)
            {
                reportlkyjbList.Show();
            }

        }

        public void DjbPrintListPreview(DataSet ds1, string startTime, string endTime)
        {
            FastReport.Utils.Config.ReportSettings.ShowProgress = false;
            FastReport.Utils.Config.ReportSettings.ShowPerformance = false;

            //创建fastreport报表实例
            FastReport.Report reportdjbList = new FastReport.Report();
            //导入设计好的报表
            reportdjbList.Load(APPdirPath + @"\Report\djb.frx");
            //绑定数据源
            ds1.Tables[0].TableName = "exam_djb_view";
            //注册数据源
            reportdjbList.RegisterData(ds1);
            //把具体数据绑定到具体的DataBand上
            FastReport.DataBand db1 = reportdjbList.FindObject("Data1") as FastReport.DataBand;
            db1.DataSource = reportdjbList.GetDataSource("exam_djb_view");
            reportdjbList.Preview = this.previewControl1;
            if (!reportdjbList.IsRunning)
            {
                reportdjbList.Show();
            }
        }


        public void bgjsdListPreview(DataSet ds1, string dtTime, string ReportPrinter, string patient_source)
        {
            FastReport.Utils.Config.ReportSettings.ShowProgress = false;
            FastReport.Utils.Config.ReportSettings.ShowPerformance = false;

            //创建fastreport报表实例
            FastReport.Report reportbgjsd = new FastReport.Report();
            //导入设计好的报表
            reportbgjsd.Load(APPdirPath + @"\Report\bgjsd.frx");
            //设置默认打印机
            if (!ReportPrinter.Equals(""))
            {
                reportbgjsd.PrintSettings.Printer = ReportPrinter;
            }
            //参数设置
            reportbgjsd.SetParameterValue("title", string.Format("{0}病理科报告{1}签收单", dtTime, patient_source));
            //绑定数据源
            ds1.Tables[0].TableName = "exam_report_view";

            FastReport.TextObject Txt_Bgs = reportbgjsd.FindObject("Text3") as FastReport.TextObject;
            Txt_Bgs.Text = ds1.Tables[0].Rows.Count.ToString();

            //注册数据源
            reportbgjsd.RegisterData(ds1);
            //把具体数据绑定到具体的DataBand上
            FastReport.DataBand db1 = reportbgjsd.FindObject("Data2") as FastReport.DataBand;
            db1.DataSource = reportbgjsd.GetDataSource("exam_report_view");
            reportbgjsd.Preview = this.previewControl1;
            if (!reportbgjsd.IsRunning)
            {
                reportbgjsd.Show();
            }
        }

        public Boolean PrintQpListReport()
        {
            if (previewControl1.Report != null)
            {
                //不弹出打印设置框
                previewControl1.Report.PrintSettings.ShowDialog = false;
                previewControl1.Report.Print();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LoadBCBLPreviewer(string ReportPrinter, int PrintReportNum, BLReportParas InsM, string ImagePath, string Bc_Report_Str, string Bc_cbdoc, string Bc_shdoc, string bc_time, Font CurFont = null, Font CurzdFont = null)
        {
            FastReport.Utils.Config.ReportSettings.ShowProgress = false;
            FastReport.Utils.Config.ReportSettings.ShowPerformance = false;

            //创建fastreport报表实例
            FastReport.Report report = new FastReport.Report();
            //导入设计好的报表
            if (!InsM.fileImages.Equals(""))
            {
                string[] imgs = InsM.fileImages.Split('|');

                if (!InsM.Txt_zdpz.Equals(""))
                {

                    if (imgs.Length == 1)
                    {
                        report.Load(APPdirPath + @"\Report\BLpic1ReportA4.frx");
                        FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                        Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                    }
                    else if (imgs.Length == 2)
                    {
                        report.Load(APPdirPath + @"\Report\BLReportA4.frx");
                        FastReport.TextObject Txt_jxms = report.FindObject("Txt_jxms") as FastReport.TextObject;
                        Txt_jxms.Text = ReplaceChiEng(InsM.Txt_zdpz);
                    }
                    else if (imgs.Length > 2)
                    {
                        report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                    }
                }
                else
                {
                    if (imgs.Length == 1)
                    {
                        report.Load(APPdirPath + @"\Report\noBLpic1ReportA4.frx");
                    }
                    else if (imgs.Length == 2)
                    {
                        report.Load(APPdirPath + @"\Report\noBLReportA4.frx");
                    }
                    else if (imgs.Length > 2)
                    {
                        report.Load(APPdirPath + @"\Report\BLReport3PicA4.frx");
                    }
                }

            }
            else
            {
                report.Load(APPdirPath + @"\Report\BLnopicReportA4.frx");
            }
            //设置默认打印机
            if (!ReportPrinter.Equals(""))
            {
                report.PrintSettings.Printer = ReportPrinter;
            }
            //打印份数
            report.PrintSettings.Copies = PrintReportNum;
            //参数设置
            report.SetParameterValue("ReportParaHospital", InsM.ReportParaHospital);
            report.SetParameterValue("study_no", InsM.study_no);
            FastReport.TextObject Txt_Name = report.FindObject("Txt_Name") as FastReport.TextObject;
            Txt_Name.Text = InsM.Txt_Name;
            FastReport.TextObject Txt_Sex = report.FindObject("Txt_Sex") as FastReport.TextObject;
            Txt_Sex.Text = InsM.Txt_Sex;
            FastReport.TextObject Txt_Age = report.FindObject("Txt_Age") as FastReport.TextObject;
            Txt_Age.Text = InsM.Txt_Age;
            FastReport.TextObject Txt_ly = report.FindObject("Txt_ly") as FastReport.TextObject;
            Txt_ly.Text = InsM.Txt_ly;
            //门诊或者住院标签
            FastReport.TextObject mzorzyLab = report.FindObject("Text30") as FastReport.TextObject;
            //送检单位或者送检科室标签
            FastReport.TextObject dworks = report.FindObject("Text6") as FastReport.TextObject;
            if (InsM.Txt_ly.Equals("门诊"))
            {
                dworks.Text = "送检科室：";
                mzorzyLab.Text = "病人ID号：";
                FastReport.TextObject Txt_Pid = report.FindObject("Txt_Pid") as FastReport.TextObject;
                if (InsM.Txt_Pid.Contains("New") == false)
                {
                    Txt_Pid.Text = InsM.Txt_Pid;
                }
            }
            else if (InsM.Txt_ly.Equals("住院"))
            {
                dworks.Text = "送检科室：";
                mzorzyLab.Text = "住 院 号：";
                FastReport.TextObject Txt_Pid = report.FindObject("Txt_Pid") as FastReport.TextObject;
                Txt_Pid.Text = InsM.Txt_Zyh;
                if (!InsM.Txt_Ch.Equals(""))
                {
                    FastReport.TextObject Txt_BedNo = report.FindObject("Txt_BedNo") as FastReport.TextObject;
                    Txt_BedNo.Text = InsM.Txt_Ch + "床";
                }


            }
            else if (InsM.Txt_ly.Equals("外来"))
            {
                dworks.Text = "送检单位：";
                mzorzyLab.Text = "病人ID号：";
                FastReport.TextObject Txt_Pid = report.FindObject("Txt_Pid") as FastReport.TextObject;
                if (InsM.Txt_Pid.Contains("New") == false)
                {
                    Txt_Pid.Text = InsM.Txt_Pid;
                }
            }

            FastReport.TextObject Txt_Sjbw = report.FindObject("Txt_Sjbw") as FastReport.TextObject;
            Txt_Sjbw.Text = InsM.Txt_Sjbw;
            FastReport.TextObject Txt_Sjks = report.FindObject("Txt_Sjks") as FastReport.TextObject;
            Txt_Sjks.Text = InsM.Txt_Sjks;
            FastReport.TextObject Txt_Sjys = report.FindObject("Txt_Sjys") as FastReport.TextObject;
            Txt_Sjys.Text = InsM.Txt_Sjys;
            FastReport.TextObject Txt_Sqrq = report.FindObject("Txt_Sqrq") as FastReport.TextObject;
            Txt_Sqrq.Text = InsM.Txt_Sqrq;
            FastReport.TextObject Txt_Rysj = report.FindObject("Txt_Rysj") as FastReport.TextObject;
            if (CurFont != null)
            {
                Txt_Rysj.Font = CurFont;
            }

            Txt_Rysj.Text = ReplaceChiEng(InsM.Txt_Rysj);
            FastReport.TextObject Txt_Blzd = report.FindObject("Txt_Blzd") as FastReport.TextObject;
            if (CurzdFont != null)
            {
                Txt_Blzd.Font = CurzdFont;
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(InsM.Txt_Blzd);
            sb.AppendLine();
            sb.AppendLine(string.Format("报告医师：{0} 复验医师：{1} 报告日期：{2}", InsM.Txt_Bgys, InsM.Txt_fjys, InsM.txt_Bgrq));
            sb.AppendLine("补充报告诊断内容：");
            sb.AppendLine(Bc_Report_Str);
            Txt_Blzd.Text = ReplaceChiEng(sb.ToString());
            FastReport.TextObject txt_Bgrq = report.FindObject("txt_Bgrq") as FastReport.TextObject;
            txt_Bgrq.Text = bc_time;
            FastReport.TextObject Txt_Bgys = report.FindObject("Txt_Bgys") as FastReport.TextObject;
            Txt_Bgys.Text = Bc_cbdoc;
            FastReport.TextObject Txt_fyys = report.FindObject("Txt_fyys") as FastReport.TextObject;
            Txt_fyys.Text = Bc_shdoc;
            if (!InsM.fileImages.Equals(""))
            {
                string[] imgs = InsM.fileImages.Split('|');
                if (imgs.Length == 1)
                {
                    FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                    Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);

                }
                else if (imgs.Length == 2)
                {
                    if (imgs[0].IndexOf("DT") != -1)
                    {
                        FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                        Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                        FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                        Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[1]);
                    }
                    else
                    {
                        FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                        Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[1]);
                        FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                        Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                    }

                }
                else if (imgs.Length > 2)
                {
                    FastReport.PictureObject Picture1 = report.FindObject("Picture1") as FastReport.PictureObject;
                    Picture1.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[0]);
                    FastReport.PictureObject Picture2 = report.FindObject("Picture2") as FastReport.PictureObject;
                    Picture2.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[1]);
                    FastReport.PictureObject Picture3 = report.FindObject("Picture3") as FastReport.PictureObject;
                    Picture3.ImageLocation = string.Format(@"{0}{1}", ImagePath, imgs[2]);
                }
            }
            report.Preview = previewControl1;
            if (!report.IsRunning)
            {
                report.Show();
            }
        }
        //免疫组化报告
        public Boolean PreviewMyzhReport(myzhReportParas ins, string Printer_Name, int Print_Copys, int reportType = 0)
        {
            if (reportType == 4)
            {
                if (System.IO.File.Exists(APPdirPath + @"\Report\myzhReport.frx"))
                {
                    FastReport.Utils.Config.ReportSettings.ShowProgress = false;
                    FastReport.Utils.Config.ReportSettings.ShowPerformance = false;
                    FastReport.Report report = new FastReport.Report();
                    //导入设计好的报表
                    report.Load(APPdirPath + @"\Report\myzhReport.frx");
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

                    FastReport.TextObject Text44 = report.FindObject("Text44") as FastReport.TextObject;
                    Text44.Text = ins.lczd;

                    FastReport.TextObject Text46 = report.FindObject("Text46") as FastReport.TextObject;
                    Text46.Text = ins.blzd;

                    FastReport.TextObject Text34 = report.FindObject("Text34") as FastReport.TextObject;
                    Text34.Text = ins.submit_unit;


                    FastReport.TextObject Text36 = report.FindObject("Text36") as FastReport.TextObject;
                    Text36.Text = ins.input_id;

                    FastReport.TextObject Text38 = report.FindObject("Text38") as FastReport.TextObject;
                    Text38.Text = ins.bed_no;

                    FastReport.TextObject Text40 = report.FindObject("Text40") as FastReport.TextObject;
                    Text40.Text = ins.zh_md;

                    FastReport.TextObject Text42 = report.FindObject("Text42") as FastReport.TextObject;
                    Text42.Text = ins.rs_func;

                    //设置默认打印机
                    if (Printer_Name != "")
                    {
                        report.PrintSettings.Printer = Printer_Name;
                    }
                    report.Preview = previewControl1;
                    //previewControl1.Zoom = 0.93f;
                    if (!report.IsRunning)
                    {
                        report.Show();
                    }
                    return true;
                }
            }
            return false;
        }



        //预览延时、补充报告
        public Boolean PreviewDelayReport(ysReportParas ins, string Printer_Name, int Print_Copys, int reportType = 0)
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
                    report.Preview = previewControl1;
                    //previewControl1.Zoom = 0.93f;
                    if (!report.IsRunning)
                    {
                        report.Show();
                    }
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
                    report.Preview = previewControl1;
                    //previewControl1.Zoom = 0.93f;
                    if (!report.IsRunning)
                    {
                        report.Show();
                    }
                    return true;
                }
            }
            return false;
        }


        //预览冰冻报告
        public Boolean PreviewIceReport(iceReportParas ins, string Printer_Name, int Print_Copys)
        {
            setToolBarVisible(false);
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
                report.Preview = previewControl1;
                //previewControl1.Zoom = 0.93f;
                if (!report.IsRunning)
                {
                    report.Show();
                }
                return true;
            }
            return false;
        }
    }
}
