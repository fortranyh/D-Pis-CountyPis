using DevComponents.DotNetBar;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
namespace PIS_Sys.dagl
{
    public partial class Frm_ReportViewer : DevComponents.DotNetBar.Office2007Form
    {

        //申请单号
        public string exam_no = "";
        //病理号
        public string study_no = "";
        //检查类型
        public string modality = "";

        public Frm_ReportViewer()
        {
            InitializeComponent();
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

            this.CenterToScreen();
        }

        private void Frm_ReportViewer_Load(object sender, EventArgs e)
        {
            //报告图片路径
            string ReportFolder = Program.APPdirPath + @"\Pis_Report_Image";
            if (Directory.Exists(ReportFolder) == false)
            {
                Directory.CreateDirectory(ReportFolder);
            }
            //查询报告状态
            DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
            int status = insM.GetStudyExam_Status(study_no);

            if (status >= 55)
            {
                //下载
                Frm_Ftp_Down ins = new Frm_Ftp_Down();
                ins.ReportFolder = ReportFolder;
                ins.study_no = study_no;
                ins.ShowDialog();
                string PathImg = ReportFolder + @"\" + study_no;
                this.pictureBox1.Image = null;
                if (Directory.Exists(PathImg))
                {
                    DirectoryInfo Dir = new DirectoryInfo(PathImg);
                    foreach (FileInfo FI in Dir.GetFiles())
                    {
                        Image img = GetImage(FI.FullName);
                        this.pictureBox1.Image = img;

                        this.pictureBox1.Location = new Point((panelEx1.Width - img.Width) / 2, 0);
                        pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

                    }
                }
                else
                {
                    Frm_TJInfo("提示", "整体报告不存在！");
                }
                this.panelEx1.AutoScroll = true;
            }
            else
            {
                Frm_TJInfo("提示", "报告未打印，不能预览！");
            }

        }
        public Image GetImage(string path)
        {
            FileStream fs = new System.IO.FileStream(path, FileMode.Open);
            Image result = System.Drawing.Image.FromStream(fs);
            fs.Close();
            return result;
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

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            panelEx1.Select();
            panelEx1.Focus();
        }
        //打印
        private void buttonX1_Click(object sender, EventArgs e)
        {
            Print();
        }


        public void Print()
        {
            try
            {

                var printerSettings = new PrinterSettings
                {
                    PrinterName = PIS_Sys.Properties.Settings.Default.ReportPrinter,
                };
                PrintDocument document = new PrintDocument
                {
                    PrinterSettings = printerSettings,
                    DocumentName = Thread.CurrentThread.Name,
                    PrintController = new StandardPrintController()
                };

                document.PrinterSettings.Collate = true;
                document.PrinterSettings.Copies = (short)PIS_Sys.Properties.Settings.Default.ReportPrintNum;
                document.PrintPage += OnPrintPage;
                document.Print();
                Frm_TJInfo("提示", "打印成功！");
            }
            catch (Exception ex)
            {
                Program.FileLog.Error("打印失败: " + ex.ToString());
            }

        }
        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {

            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    Application.DoEvents();
                    e.Graphics.DrawImage(pictureBox1.Image, e.Graphics.VisibleClipBounds);
                    e.HasMorePages = false;
                    pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                    Application.DoEvents();
                }

            }
            catch (Exception ex)
            {
                Program.FileLog.Error("打印失败: " + ex.ToString());
            }
        }




    }
}
