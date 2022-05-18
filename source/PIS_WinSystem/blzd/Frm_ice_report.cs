using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_ice_report : DevComponents.DotNetBar.Office2007Form
    {
        public string BLH = "";
        public string exam_no = "";
        private string patient_name, sex, age, req_dept;

        public Frm_ice_report()
        {
            InitializeComponent();
        }

        private void Frm_ice_report_Load(object sender, EventArgs e)
        {

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

            DataSet ds1 = doctor_dict_ins.GetDsExam_User(Program.Dept_Code);
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {

                comboBoxEx2.DataSource = ds1.Tables[0];
                comboBoxEx2.DisplayMember = "user_name";
                comboBoxEx2.ValueMember = "user_code";
                comboBoxEx2.Text = Program.User_Name;
            }
            else
            {
                comboBoxEx2.DataSource = null;
            }
            //
            report_bl_datetime.Value = DateTime.Now;
            //
            DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
            DataTable dtPat = insM.GetPatientInfo(BLH);
            if (dtPat != null && dtPat.Rows.Count == 1)
            {
                req_dept = dtPat.Rows[0]["req_dept"].ToString();
                patient_name = dtPat.Rows[0]["patient_name"].ToString();
                sex = dtPat.Rows[0]["sex"].ToString();
                age = dtPat.Rows[0]["age"].ToString();
                lbl_receive_time.Text = dtPat.Rows[0]["received_datetime"].ToString();
                textBox1.Text = dtPat.Rows[0]["input_id"].ToString();
                if (!dtPat.Rows[0]["bed_no"].ToString().Equals(""))
                {
                    textBox2.Text = dtPat.Rows[0]["bed_no"].ToString() + "床";
                }
            }

            //报告内容
            DBHelper.BLL.exam_ice_report ins = new DBHelper.BLL.exam_ice_report();
            DataTable dt = ins.GetData(BLH);
            if (dt != null && dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["sh_flag"].ToString().Equals("1"))
                {
                    richTextBoxEx1.Text = dt.Rows[0]["content"].ToString();
                    comboBoxEx1.Text = dt.Rows[0]["report_doc_name"].ToString();
                    comboBoxEx2.Text = dt.Rows[0]["sh_doc_name"].ToString();
                    comboBoxEx3.Text = dt.Rows[0]["slfh"].ToString();
                    report_bl_datetime.Text = dt.Rows[0]["report_datetime"].ToString();
                    buttonX3.TextColor = Color.Blue;
                    buttonX1.Enabled = true;
                    buttonX5.Enabled = true;
                }
                else
                {
                    richTextBoxEx1.Text = dt.Rows[0]["content"].ToString();
                    comboBoxEx1.Text = dt.Rows[0]["report_doc_name"].ToString();
                    comboBoxEx2.Text = dt.Rows[0]["sh_doc_name"].ToString();
                    comboBoxEx3.Text = dt.Rows[0]["slfh"].ToString();
                    report_bl_datetime.Text = dt.Rows[0]["report_datetime"].ToString();
                    buttonX1.Enabled = false;
                    buttonX5.Enabled = false;
                }
                buttonX3.Enabled = true;
            }
            else
            {
                buttonX3.Enabled = false;
                buttonX5.Enabled = false;
                buttonX1.Enabled = false;
                comboBoxEx3.Text = "符合";
            }
        }
        //保存
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (richTextBoxEx1.Text.Trim() != "")
            {
                DBHelper.BLL.exam_ice_report ins = new DBHelper.BLL.exam_ice_report();
                DBHelper.Model.exam_ice_report insM = new DBHelper.Model.exam_ice_report();
                insM.content = richTextBoxEx1.Text.Trim();
                insM.study_no = BLH;
                insM.report_datetime = report_bl_datetime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                insM.report_doc_code = comboBoxEx1.SelectedValue.ToString();
                insM.report_doc_name = comboBoxEx1.Text;
                if (comboBoxEx3.Text.Trim().Equals(""))
                {
                    insM.lcfh = "符合";
                }
                else
                {
                    insM.lcfh = comboBoxEx3.Text.Trim();
                }
                if (ins.SaveReport(insM) == true)
                {
                    Frm_TJInfo("提示", "保存成功!");
                    buttonX3.Enabled = true;
                }
            }
        }
        //提示窗体
        public void Frm_TJInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(this);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = true;
            Frm_AlertIns.AutoCloseTimeOut = 3;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }
        //审核
        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (richTextBoxEx1.Text.Trim() != "")
            {

                DBHelper.BLL.exam_ice_report ins = new DBHelper.BLL.exam_ice_report();
                DataTable dt = ins.GetData(BLH);
                if (dt != null && dt.Rows.Count == 1)
                {
                    if (dt.Rows[0]["sh_flag"].ToString().Equals("1"))
                    {

                        if (ins.ShReport(BLH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1, comboBoxEx2.SelectedValue.ToString(), comboBoxEx2.Text.Trim(), comboBoxEx3.Text.Trim()))
                        {
                            Frm_TJInfo("提示", "审核成功!");
                            buttonX3.TextColor = Color.Blue;
                            buttonX1.Enabled = true;
                            buttonX5.Enabled = true;
                        }
                    }
                    else
                    {
                        if (ins.ShReport(BLH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1, comboBoxEx2.SelectedValue.ToString(), comboBoxEx2.Text.Trim(), comboBoxEx3.Text.Trim()))
                        {
                            Frm_TJInfo("提示", "审核成功!");
                            buttonX3.TextColor = Color.Blue;
                            buttonX1.Enabled = true;
                            buttonX5.Enabled = true;
                        }
                    }
                }

            }
        }


        //打印成功后图像格式报告上传和报告状态修改
        public void ReportUploadStatus(string strFileName, string exam_no, string study_no)
        {
            string filenameStr = string.Format("BDReport_{0}", study_no);
            DBHelper.BLL.exam_master insMas = new DBHelper.BLL.exam_master();
            //上传报告图片格式到服务器
            if (System.IO.File.Exists(@strFileName))
            {
                string remoteFile = string.Format(@"{0}/{1}/{2}/{3}/{4}.jpg", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, study_no, filenameStr);
                //上传
                if (FtpUpload(@remoteFile, strFileName))
                {
                    DBHelper.BLL.multi_media_info insMulti = new DBHelper.BLL.multi_media_info();
                    if (insMulti.InsertData(study_no, remoteFile, string.Format("{0}.jpg", filenameStr), 11) == 1)
                    {

                    }
                }
            }
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


        //打印
        private void buttonX5_Click(object sender, EventArgs e)
        {
            buttonX2_Click(null, null);
            if (buttonX3.TextColor != Color.Blue)
            {
                buttonX3_Click(null, null);
            }
            if (!richTextBoxEx1.Text.Trim().Equals(""))
            {
                FastReportLib.iceReportParas insM = new FastReportLib.iceReportParas();
                insM.study_no = BLH;
                insM.ReportParaHospital = Program.HospitalName;
                if (lbl_receive_time.Text.Trim().Length >= 10)
                {
                    insM.Txt_Jsrq = lbl_receive_time.Text.Trim().Substring(0, 10);
                }
                else
                {
                    insM.Txt_Jsrq = lbl_receive_time.Text.Trim();
                }
                insM.Txt_Name = patient_name;
                insM.exam_no = exam_no;
                insM.Txt_Sex = sex;
                insM.Txt_Age = age;
                insM.req_dept = req_dept;
                insM.Txt_Content = richTextBoxEx1.Text.Trim();
                insM.Txt_BgDate = report_bl_datetime.Text;
                insM.Txt_report_Doctor = comboBoxEx1.Text;
                insM.shys = comboBoxEx2.Text;
                insM.zyh = textBox1.Text.Trim();
                insM.ch = textBox2.Text.Trim();

                //报告图片格式图像路径
                string ReportFolder = Program.APPdirPath + @"\Pis_Report_Image";
                if (Directory.Exists(ReportFolder) == false)
                {
                    Directory.CreateDirectory(ReportFolder);
                }
                string PathImg = Program.APPdirPath;
                string filename = string.Format("Report_{0}", BLH);
                string PathReportImg = ReportFolder + @"\" + BLH;
                if (Directory.Exists(PathReportImg) == false)
                {
                    Directory.CreateDirectory(PathReportImg);
                }
                string strFileName = string.Format(@"{0}\{1}.jpg", PathReportImg, filename);
                if (FastReportLib.IceReport.PrintIceReport(insM, PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, @strFileName))
                {
                    Frm_TJInfo("提示", "报告打印成功！");
                    if (File.Exists(@strFileName))
                    {
                        ReportUploadStatus(@strFileName, exam_no, BLH);
                    }
                }
            }
            else
            {
                Frm_TJInfo("提示", "报告内容不能为空！");
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.richTextBoxEx1.SelectedText);
            richTextBoxEx1.SelectedText = String.Empty;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBoxEx1.SelectedText);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            richTextBoxEx1.SelectedText = Clipboard.GetText();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            buttonX2_Click(null, null);
            if (!richTextBoxEx1.Text.Trim().Equals(""))
            {
                FastReportLib.iceReportParas insM = new FastReportLib.iceReportParas();
                insM.study_no = BLH;
                insM.ReportParaHospital = Program.HospitalName;
                if (lbl_receive_time.Text.Trim().Length >= 10)
                {
                    insM.Txt_Jsrq = lbl_receive_time.Text.Trim().Substring(0, 10);
                }
                else
                {
                    insM.Txt_Jsrq = lbl_receive_time.Text.Trim();
                }
                insM.Txt_Name = patient_name;
                insM.exam_no = exam_no;
                insM.Txt_Sex = sex;
                insM.Txt_Age = age;
                insM.req_dept = req_dept;
                insM.Txt_Content = richTextBoxEx1.Text.Trim();
                insM.Txt_BgDate = report_bl_datetime.Text;
                insM.Txt_report_Doctor = comboBoxEx1.Text;
                insM.shys = comboBoxEx2.Text;
                insM.zyh = textBox1.Text.Trim();
                insM.ch = textBox2.Text.Trim();

                Frm_BgPre insF = new Frm_BgPre();
                insF.insIce = insM;
                insF.reportType = 2;
                insF.Printer_Name = PIS_Sys.Properties.Settings.Default.ReportPrinter;
                insF.Print_Copys = PIS_Sys.Properties.Settings.Default.ReportPrintNum;
                insF.ShowDialog();
            }
            else
            {
                Frm_TJInfo("提示", "报告内容不能为空！");
            }
        }
    }
}
