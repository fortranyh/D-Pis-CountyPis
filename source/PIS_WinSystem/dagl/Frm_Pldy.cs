using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_Pldy : DevComponents.DotNetBar.Office2007Form
    {
        FastReportLib.ReportViewer reportViewer1 = new FastReportLib.ReportViewer();
        public Frm_Pldy()
        {
            InitializeComponent();
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();
        }

        private void Frm_Pldy_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            panel1.MinRowHeight = 30;
            //锁定前四列
            panel1.FrozenColumnCount = 4;
            panel1.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].ReadOnly = true;
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel1.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
            //绑定医生
            DBHelper.BLL.doctor_dict doctor_dict_ins = new DBHelper.BLL.doctor_dict();
            DataSet ds = doctor_dict_ins.GetDsExam_User(Program.Dept_Code);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                comboBoxEx1.DataSource = ds.Tables[0];
                comboBoxEx1.DisplayMember = "user_name";
                comboBoxEx1.ValueMember = "user_code";
                comboBoxEx1.Text = Program.User_Name;
            }
            else
            {
                comboBoxEx1.DataSource = null;
            }


        }
        //提示窗体
        public void Frm_TJInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(this);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = true;
            Frm_AlertIns.AutoCloseTimeOut = 1;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dtStart.Value > dtEnd.Value)
            {
                Frm_TJInfo("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" cbreport_datetime>='{0} 00:00:00' and cbreport_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));



            sb.AppendFormat(" and exam_type in ({0}) ", ConfigurationManager.AppSettings["w_big_type_db"]);



            sb.AppendFormat(" and cbreport_doc_name='{0}'", comboBoxEx1.Text.Trim());

            sb.Append(" and exam_status<55 ");

            DataTable dt = new DataTable();
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            dt = ins.GetPrintReportList(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                superGridControl1.PrimaryGrid.DataSource = null;
                Frm_TJInfo("提示", "当前条件下报告已经打印完毕！");
            }
        }
        //病理号
        string study_no = "";
        string exam_no = "";
        string ImgFolder;
        string ImgQcFolder;
        private void buttonX2_Click(object sender, EventArgs e)
        {
            buttonX2.Enabled = false;
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
            {
                SelectedElementCollection col = superGridControl1.PrimaryGrid.GetSelectedRows();
                if (col.Count > 0)
                {
                    for (int i = 0; i < col.Count; i++)
                    {
                        GridRow Row = col[i] as GridRow;
                        //病理号
                        study_no = Row.Cells["study_no"].Value.ToString();
                        //申请单号
                        exam_no = Row.Cells["exam_no"].Value.ToString();
                        //报告图像路径
                        ImgFolder = Program.APPdirPath + @"\Pis_Cap_Image";
                        if (Directory.Exists(ImgFolder) == false)
                        {
                            Directory.CreateDirectory(ImgFolder);
                        }
                        //取材图像路径
                        ImgQcFolder = Program.APPdirPath + @"\Pis_Image";
                        if (Directory.Exists(ImgQcFolder) == false)
                        {
                            Directory.CreateDirectory(ImgQcFolder);
                        }
                        //下载
                        Frm_Ftp_Down ins = new Frm_Ftp_Down();
                        ins.ImgFolder = ImgFolder;
                        ins.ImgQcFolder = ImgQcFolder;
                        ins.study_no = study_no;
                        ins.ShowDialog();
                        //
                        RefreshPreviewReport();
                    }
                    buttonX1.PerformClick();
                }
            }
            buttonX2.Enabled = true;
        }

        //报告图像串
        string ReportImageStr = "";
        string PathImg = Program.APPdirPath;
        //预览
        public void RefreshPreviewReport()
        {
            //查询报告模板
            DBHelper.BLL.exam_report insReport = new DBHelper.BLL.exam_report();
            int report_type = insReport.GetReportTemletIndex(study_no);
            //报告图片路径
            string ReportFolder = Program.APPdirPath + @"\Pis_Report_Image";
            if (Directory.Exists(ReportFolder) == false)
            {
                Directory.CreateDirectory(ReportFolder);
            }
            string filename = string.Format("Report_{0}", study_no);
            string PathReportImg = ReportFolder + @"\" + study_no;
            if (Directory.Exists(PathReportImg) == false)
            {
                Directory.CreateDirectory(PathReportImg);
            }
            string strFileName = string.Format(@"{0}\{1}.jpg", PathReportImg, filename);
            DataSet _DataSet = new DataSet();
            DBHelper.Model.exam_report InsMReport;
            DBHelper.BLL.exam_master exam_mas_Ins = new DBHelper.BLL.exam_master();
            _DataSet = exam_mas_Ins.QueryDsBGExam_master("病理号", study_no, Program.workstation_type_db);
            if (_DataSet != null && _DataSet.Tables[0].Rows.Count == 1)
            {
                //检查部位
                DataTable dt = exam_mas_Ins.SelectParts(study_no);
                string partsStr = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    partsStr = dt.Rows[0]["parts"].ToString();
                }
                //临床诊断
                string lczdStr = "";
                DBHelper.BLL.exam_requisition Rins = new DBHelper.BLL.exam_requisition();
                DBHelper.Model.exam_requisition RMins = Rins.GetRequisitionInfo(exam_no);
                if (RMins != null)
                {
                    lczdStr = RMins.clinical_diag;
                }
                //报告信息
                DBHelper.BLL.exam_report InsBllReport = new DBHelper.BLL.exam_report();
                InsMReport = InsBllReport.GetExam_Report(study_no);
                if (InsMReport != null)
                {
                    ReportImageStr = InsMReport.image ?? "";
                }
                //打印成功是否标志
                Boolean Print_Flag = false;

                if (report_type == 0)
                {
                    try
                    {
                        //直接打印
                        FastReportLib.BLReportParas InsBLReport = new FastReportLib.BLReportParas();
                        InsBLReport.InitParasDate();
                        InsBLReport.ReportParaHospital = Program.HospitalName;
                        InsBLReport.study_no = InsMReport.study_no;
                        InsBLReport.Txt_Name = _DataSet.Tables[0].Rows[0]["patient_name"].ToString();
                        InsBLReport.Txt_Sex = _DataSet.Tables[0].Rows[0]["sex"].ToString();
                        InsBLReport.Txt_Age = _DataSet.Tables[0].Rows[0]["age"].ToString();
                        InsBLReport.Txt_ly = _DataSet.Tables[0].Rows[0]["patient_source"].ToString();
                        if (InsBLReport.Txt_ly.Equals("外来"))
                        {
                            InsBLReport.Txt_Sjks = _DataSet.Tables[0].Rows[0]["submit_unit"].ToString();
                        }
                        else
                        {
                            InsBLReport.Txt_Sjks = _DataSet.Tables[0].Rows[0]["req_dept"].ToString();
                        }
                        InsBLReport.Txt_Bf = _DataSet.Tables[0].Rows[0]["ward"].ToString();
                        InsBLReport.Txt_Ch = _DataSet.Tables[0].Rows[0]["bed_no"].ToString();
                        InsBLReport.Txt_Zyh = _DataSet.Tables[0].Rows[0]["input_id"].ToString();
                        InsBLReport.Txt_Sjys = _DataSet.Tables[0].Rows[0]["req_physician"].ToString();
                        InsBLReport.Txt_Sqrq = _DataSet.Tables[0].Rows[0]["received_datetime"].ToString().Substring(0, 10);
                        if (_DataSet.Tables[0].Rows[0]["patient_id"].ToString().IndexOf("New") == -1)
                        {
                            InsBLReport.Txt_Pid = _DataSet.Tables[0].Rows[0]["patient_id"].ToString();
                        }
                        else
                        {
                            InsBLReport.Txt_Pid = "";
                        }
                        InsBLReport.Txt_mzh = _DataSet.Tables[0].Rows[0]["output_id"].ToString();
                        InsBLReport.Txt_Rysj = InsMReport.rysj;
                        InsBLReport.Txt_Blzd = InsMReport.zdyj;
                        InsBLReport.txt_Bgrq = InsMReport.cbreport_datetime.Substring(0, 10);
                        InsBLReport.Txt_Bgys = InsMReport.cbreport_doc_name;
                        InsBLReport.Txt_fjys = InsMReport.zzreport_doc_name;
                        InsBLReport.fileImages = ReportImageStr;
                        InsBLReport.Txt_zdpz = InsMReport.zdpz;
                        InsBLReport.report_gb_doc = InsMReport.report_gb_doc;
                        InsBLReport.wtzd_flag = _DataSet.Tables[0].Rows[0]["wtzd_flag"].ToString();
                        InsBLReport.Txt_Sjdw = _DataSet.Tables[0].Rows[0]["submit_unit"].ToString();
                        //送检部位
                        InsBLReport.Txt_Sjbw = partsStr;
                        //PId
                        if (_DataSet.Tables[0].Rows[0]["patient_id"].ToString().IndexOf("New") == -1)
                        {
                            InsBLReport.Txt_Pid = _DataSet.Tables[0].Rows[0]["patient_id"].ToString();
                        }
                        else
                        {
                            InsBLReport.Txt_Pid = "";
                        }

                        string PathImg = Program.APPdirPath;
                        if (this.reportViewer1.DirectPrintBLReport(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, InsBLReport, PathImg, strFileName))
                        {
                            Print_Flag = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("组织学报告打印失败：" + ex.ToString());
                        return;
                    }
                }

                else if (report_type == 1)
                {
                    try
                    {
                        //直接打印
                        FastReportLib.XbxReportParas InsXbxReport = new FastReportLib.XbxReportParas();
                        InsXbxReport.InitParasDate();
                        InsXbxReport.ReportParaHospital = Program.HospitalName;
                        InsXbxReport.study_no = _DataSet.Tables[0].Rows[0]["study_no"].ToString();
                        InsXbxReport.Txt_Name = _DataSet.Tables[0].Rows[0]["patient_name"].ToString();
                        InsXbxReport.Txt_Sex = _DataSet.Tables[0].Rows[0]["sex"].ToString();
                        InsXbxReport.Txt_Age = _DataSet.Tables[0].Rows[0]["age"].ToString();
                        InsXbxReport.Txt_ly = _DataSet.Tables[0].Rows[0]["patient_source"].ToString();
                        if (InsXbxReport.Txt_ly.Equals("外来"))
                        {
                            InsXbxReport.Txt_Sjks = _DataSet.Tables[0].Rows[0]["submit_unit"].ToString();
                        }
                        else
                        {
                            InsXbxReport.Txt_Sjks = _DataSet.Tables[0].Rows[0]["req_dept"].ToString();
                        }
                        InsXbxReport.Txt_Zyh = _DataSet.Tables[0].Rows[0]["input_id"].ToString();
                        InsXbxReport.Txt_Sjys = _DataSet.Tables[0].Rows[0]["req_physician"].ToString();
                        InsXbxReport.Txt_Sqrq = _DataSet.Tables[0].Rows[0]["received_datetime"].ToString().Substring(0, 10);
                        InsXbxReport.Txt_mzh = _DataSet.Tables[0].Rows[0]["output_id"].ToString();

                        InsXbxReport.fileImages = ReportImageStr;

                        InsXbxReport.Txt_Xbl = InsMReport.xbms;
                        InsXbxReport.Txt_Xbxjg = InsMReport.zdyj;
                        InsXbxReport.Txt_Zljy = InsMReport.zdpz;
                        InsXbxReport.Txt_Bgrq = InsMReport.cbreport_datetime.Substring(0, 10);
                        InsXbxReport.Txt_Bgys = InsMReport.cbreport_doc_name;

                        //送检标本
                        InsXbxReport.Txt_bblx = partsStr;
                        //病人信息
                        if (InsXbxReport.Txt_ly.Equals("体检") || InsXbxReport.Txt_ly.Equals("门诊"))
                        {
                            InsXbxReport.Txt_mcyj = _DataSet.Tables[0].Rows[0]["hospital_card"].ToString();
                        }

                        string PathImg = Program.APPdirPath;
                        if (this.reportViewer1.DirectPrintXbxReport(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, InsXbxReport, PathImg, strFileName))
                        {
                            Print_Flag = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("打印报告失败：" + ex.ToString());
                        return;
                    }
                }

                else if (report_type == 3)
                {
                    try
                    {
                        //直接打印
                        FastReportLib.BbXfsReportParas InsBbXfsReport = new FastReportLib.BbXfsReportParas();
                        InsBbXfsReport.InitParasDate();
                        InsBbXfsReport.ReportParaHospital = Program.HospitalName;
                        InsBbXfsReport.study_no = _DataSet.Tables[0].Rows[0]["study_no"].ToString();
                        InsBbXfsReport.Txt_Name = _DataSet.Tables[0].Rows[0]["patient_name"].ToString();
                        InsBbXfsReport.Txt_Sex = _DataSet.Tables[0].Rows[0]["sex"].ToString();
                        InsBbXfsReport.Txt_Age = _DataSet.Tables[0].Rows[0]["age"].ToString();
                        InsBbXfsReport.Txt_Bed = _DataSet.Tables[0].Rows[0]["bed_no"].ToString();
                        InsBbXfsReport.Txt_ly = _DataSet.Tables[0].Rows[0]["patient_source"].ToString();
                        InsBbXfsReport.hospital_card = _DataSet.Tables[0].Rows[0]["hospital_card"].ToString();

                        if (_DataSet.Tables[0].Rows[0]["patient_id"].ToString().IndexOf("New") == -1)
                        {
                            InsBbXfsReport.Txt_Pid = _DataSet.Tables[0].Rows[0]["patient_id"].ToString();
                        }
                        else
                        {
                            InsBbXfsReport.Txt_Pid = "";
                        }
                        if (InsBbXfsReport.Txt_ly.Equals("外来"))
                        {
                            InsBbXfsReport.Txt_Sjks = _DataSet.Tables[0].Rows[0]["submit_unit"].ToString();
                        }
                        else
                        {
                            InsBbXfsReport.Txt_Sjks = _DataSet.Tables[0].Rows[0]["req_dept"].ToString();
                        }

                        InsBbXfsReport.Txt_Zyh = _DataSet.Tables[0].Rows[0]["input_id"].ToString();
                        InsBbXfsReport.Txt_Sjys = _DataSet.Tables[0].Rows[0]["req_physician"].ToString();
                        InsBbXfsReport.Txt_Sqrq = _DataSet.Tables[0].Rows[0]["received_datetime"].ToString().Substring(0, 10);
                        InsBbXfsReport.Txt_mzh = _DataSet.Tables[0].Rows[0]["output_id"].ToString();
                        InsBbXfsReport.Txt_Blzd = InsMReport.zdyj;
                        InsBbXfsReport.txt_Bgrq = InsMReport.cbreport_datetime.Substring(0, 10);
                        InsBbXfsReport.Txt_Bgys = InsMReport.cbreport_doc_name;
                        InsBbXfsReport.Txt_Fyys = InsMReport.zzreport_doc_name;
                        InsBbXfsReport.Txt_Zdpz = InsMReport.zdpz;
                        InsBbXfsReport.fileImages = ReportImageStr;
                        //送检部位
                        InsBbXfsReport.Txt_Sjbw = partsStr;
                        if (partsStr.Equals(""))
                        {
                            //送检部位
                            string strBw = "";
                            DBHelper.BLL.exam_specimens InsSpec = new DBHelper.BLL.exam_specimens();
                            DataTable dtSpec = InsSpec.GetBGSpecimensDTQC(_DataSet.Tables[0].Rows[0]["exam_no"].ToString());
                            if (dtSpec != null)
                            {
                                for (int i = 0; i <= dtSpec.Rows.Count - 1; i++)
                                {
                                    strBw = strBw + dtSpec.Rows[i]["parts"].ToString() + " ";
                                }
                                InsBbXfsReport.Txt_Sjbw = strBw.Trim();
                            }
                        }
                        string PathImg = Program.APPdirPath;
                        if (this.reportViewer1.DirectPrintXfsReport(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, InsBbXfsReport, PathImg, strFileName))
                        {
                            Print_Flag = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("打印报告失败：" + ex.ToString());
                        return;
                    }
                }
                else if (report_type == 4)
                {
                    try
                    {
                        //直接打印
                        Print_Flag = this.reportViewer1.DirectPrintHPVReport(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, exam_no, study_no, PathImg, ReportImageStr, Program.HospitalName, partsStr, InsMReport.xbms, lczdStr, InsMReport.cbreport_datetime, InsMReport.report_childtmp_index, @strFileName);
                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("打印报告失败：" + ex.ToString());
                        return;
                    }
                }
                else if (report_type == 5)
                {
                    try
                    {
                        //直接打印
                        Print_Flag = this.reportViewer1.DirectPrintWyHzReport(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, exam_no, study_no, PathImg, ReportImageStr, Program.HospitalName, partsStr, InsMReport.zdyj, InsMReport.wy_study_no, "", InsMReport.lk_num, InsMReport.bp_num, InsMReport.cbreport_datetime, @strFileName);
                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("打印报告失败：" + ex.ToString());
                        return;
                    }
                }
                else if (report_type == 6)
                {
                    try
                    {
                        //直接打印
                        Print_Flag = this.reportViewer1.DirectPrintFZReport(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, exam_no, study_no, PathImg, ReportImageStr, Program.HospitalName, partsStr, InsMReport.xbms, lczdStr, InsMReport.cbreport_datetime, InsMReport.report_childtmp_index, @strFileName);
                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("打印报告失败：" + ex.ToString());
                        return;
                    }
                }

                if (Print_Flag)
                {
                    //更新状态
                    DBHelper.BLL.exam_report insBll = new DBHelper.BLL.exam_report();
                    Boolean ZXResult = insBll.PrintReport(study_no);
                    if (ZXResult == true)
                    {
                        //更新报告状态
                        DBHelper.BLL.exam_master insMas = new DBHelper.BLL.exam_master();
                        int status = insMas.GetStudyExam_Status(study_no);
                        if (status < 55)
                        {
                            //更新检查状态
                            insMas.UpdateExam_Status(study_no, "55");
                            //更新质控时间
                            int ZkTotalTime = insMas.UpdateZkTime(exam_no);
                            //获取质控时间限制 
                            DBHelper.BLL.exam_type insType = new DBHelper.BLL.exam_type();
                            string modality = _DataSet.Tables[0].Rows[0]["modality"].ToString();
                            DataTable dtYs = insType.GetReportLimitFromModality(modality);
                            if (dtYs.Rows.Count > 0)
                            {
                                int curZkTime = Convert.ToInt32(dtYs.Rows[0]["report_limit"]);
                                if (ZkTotalTime > curZkTime)
                                {
                                    blzd.FrmYsRegister insYsFrm = new blzd.FrmYsRegister();
                                    insYsFrm.exam_no = exam_no;
                                    insYsFrm.bgys = InsMReport.cbreport_doc_name;
                                    insYsFrm.shys = InsMReport.zzreport_doc_name;
                                    insYsFrm.patient_name = _DataSet.Tables[0].Rows[0]["patient_name"].ToString();
                                    insYsFrm.study_no = study_no;
                                    insYsFrm.ShowDialog();
                                }
                            }
                        }
                    }
                    Frm_TJInfo("提示", "打印成功！");
                    //上传报告图片格式到服务器
                    if (System.IO.File.Exists(@strFileName))
                    {
                        string remoteFile = string.Format(@"{0}/{1}/{2}/{3}/{4}.jpg", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, study_no, filename);
                        //上传
                        if (FtpUpload(@remoteFile, strFileName))
                        {
                            DBHelper.BLL.multi_media_info insMulti = new DBHelper.BLL.multi_media_info();
                            if (insMulti.InsertData(strFileName, remoteFile, string.Format("{0}.jpg", filename), 7) == 1)
                            {

                            }
                        }
                    }
                    //是否启用打印上报接口服务
                    if (Program.Interface_SetInfo != null)
                    {
                        if (Program.Interface_SetInfo.enable_flag == 1)
                        {
                            //已经打印是否调用接口服务
                            if (Program.Interface_SetInfo.print_flag == 1)
                            {
                                ClientSCS.SynchronizedClient("dy|" + study_no);
                            }
                        }
                    }

                }
            }
        }

        //判断文件的目录是否存,不存则创建
        public static void FtpCheckDirectoryExist(string destFilePath)
        {
            string fullDir = FtpParseDirectory(destFilePath);
            string[] dirs = fullDir.Split('/');
            string curDir = "/";
            for (int i = 0; i < dirs.Length; i++)
            {
                string dir = dirs[i];
                //如果是以/开始的路径,第一个为空  
                if (dir != null && dir.Length > 0)
                {
                    try
                    {
                        curDir += dir + "/";
                        FtpMakeDir(curDir);
                    }
                    catch (Exception)
                    { }
                }
            }
        }

        public static string FtpParseDirectory(string destFilePath)
        {
            return destFilePath.Substring(0, destFilePath.LastIndexOf("/"));
        }
        public static string FtpIp = Program.FtpIP;
        public static int FtpPort = Program.FtpPort;
        public static string FtpUser = Program.FtpUser;
        public static string FtpPwd = Program.FtpPwd;
        //创建目录
        public static Boolean FtpMakeDir(string curDir)
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(string.Format(@"ftp://{0}:{1}/", FtpIp, FtpPort) + curDir);
            req.Credentials = new NetworkCredential(FtpUser, FtpPwd);
            req.Method = WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                response.Close();
            }
            catch (Exception)
            {
                req.Abort();
                return false;
            }
            req.Abort();
            return true;
        }
        //上传文件
        public static Boolean FtpUpload(string ftpPath, string localFile)
        {
            //检查目录是否存在，不存在创建
            FtpCheckDirectoryExist(ftpPath);
            FileInfo fi = new FileInfo(localFile);
            FileStream fs = fi.OpenRead();
            long length = fs.Length;
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(string.Format(@"ftp://{0}:{1}/", FtpIp, FtpPort) + ftpPath);
            req.Credentials = new NetworkCredential(FtpUser, FtpPwd);
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.ContentLength = length;
            req.Timeout = 10 * 1000;
            try
            {
                Stream stream = req.GetRequestStream();
                int BufferLength = 2048; //2K   
                byte[] b = new byte[BufferLength];
                int i;
                while ((i = fs.Read(b, 0, BufferLength)) > 0)
                {
                    stream.Write(b, 0, i);
                }
                stream.Close();
                stream.Dispose();
            }
            catch
            {
                return false;
            }
            finally
            {
                fs.Close();
                req.Abort();
            }
            req.Abort();
            return true;
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            Frm_bghc ins = new Frm_bghc();
            ins.ShowDialog();
        }
    }
}
