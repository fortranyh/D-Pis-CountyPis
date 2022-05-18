using DevComponents.DotNetBar;
using Manina.Windows.Forms;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;
namespace PIS_Sys
{
    public partial class Frm_ZhImage : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_ZhImage()
        {
            InitializeComponent();
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();
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
        string ImgFolder;
        string ImgQcFolder;
        public string study_no = "";
        private void Frm_ZhImage_Load(object sender, EventArgs e)
        {
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
            Frm_Ftp_Down1 ins = new Frm_Ftp_Down1();
            ins.ImgFolder = ImgFolder;
            ins.ImgQcFolder = ImgQcFolder;
            ins.study_no = study_no;
            ins.ShowDialog();
            //缩略图大小设置
            SetThumsSize();
            //加载多媒体信息
            RefreshMultiList();
        }
        //缩略图大小
        int thumsWidth = 0;
        int thumsHeight = 0;
        public void SetThumsSize()
        {
            thumsWidth = PIS_Sys.Properties.Settings.Default.BGthumsWidth;
            thumsHeight = PIS_Sys.Properties.Settings.Default.BGthumsHeight;
            if (thumsWidth < PIS_Sys.Properties.Settings.Default.DTthumsWidth)
            {
                thumsWidth = PIS_Sys.Properties.Settings.Default.DTthumsWidth;
            }
            if (thumsHeight < PIS_Sys.Properties.Settings.Default.DTthumsHeight)
            {
                thumsHeight = PIS_Sys.Properties.Settings.Default.DTthumsHeight;
            }

        }
        public Image GetImage(string path)
        {
            FileStream fs = new System.IO.FileStream(path, FileMode.Open);
            Image result = System.Drawing.Image.FromStream(fs);
            fs.Close();
            return result;
        }
        //刷新本地视频音频和图像
        public void RefreshMultiList()
        {
            listViewEx1.Items.Clear();
            listViewEx1.ThumbnailSize = new Size(thumsWidth * 3, thumsHeight * 3);
            listViewEx1.SuspendLayout();
            //报告图像
            if (Directory.Exists(ImgFolder + @"\" + study_no) == true)
            {

                foreach (var file in Directory.GetFiles(ImgFolder + @"\" + study_no, "*.jpg"))
                {
                    ImageListViewItem ins = new ImageListViewItem();
                    ins.Tag = file;
                    ins.Text = "BG_" + listViewEx1.Items.Count.ToString();
                    ins.FileName = file;
                    listViewEx1.Items.Add(ins);
                }

            }
            //取材图像
            if (Directory.Exists(ImgQcFolder + @"\" + study_no) == true)
            {
                foreach (var file in Directory.GetFiles(ImgQcFolder + @"\" + study_no, "*.jpg"))
                {
                    ImageListViewItem ins = new ImageListViewItem();
                    ins.Tag = file;
                    ins.Text = "QC_" + listViewEx1.Items.Count.ToString();
                    ins.FileName = file;
                    listViewEx1.Items.Add(ins);
                }
            }
            listViewEx1.ResumeLayout();
        }

        //找回删除的图像
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (listViewEx1.Items.Count > 0)
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;
                if (TaskDialog.Show("找回图像", "确认", "确定要找回选中的图像么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    for (int i = 0; i < listViewEx1.Items.Count; i++)
                    {
                        Boolean zhflag = false;
                        if (listViewEx1.Items[i].Checked == true)
                        {
                            string filePath = listViewEx1.Items[i].FileName;
                            string filename = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                            DBHelper.BLL.multi_media_info ins = new DBHelper.BLL.multi_media_info();
                            int type_dmt = 1;
                            if (listViewEx1.Items[i].Text.IndexOf("BG_") != -1)
                            {
                                type_dmt = 4;
                            }
                            else if (listViewEx1.Items[i].Text.IndexOf("QC_") != -1)
                            {
                                type_dmt = 1;
                            }
                            ins.ZhDeledData(study_no, filename, type_dmt);
                            zhflag = true;
                        }
                        if (zhflag)
                        {
                            Frm_TJInfo("提示", "找回成功！");
                        }
                    }
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (!textBoxX1.Text.Trim().Equals("") && study_no != textBoxX1.Text.Trim())
            {
                //查询存在此病理号
                DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                if (ins.GetStudyNoCount(textBoxX1.Text.Trim()) == 1)
                {
                    if (listViewEx1.Items.Count > 0)
                    {
                        eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                        TaskDialog.EnableGlass = false;
                        if (TaskDialog.Show("修正图像", "确认", "确定要修正选中的图像么？", Curbutton) == eTaskDialogResult.Ok)
                        {
                            Boolean zhflag = false;
                            for (int i = 0; i < listViewEx1.Items.Count; i++)
                            {
                                if (listViewEx1.Items[i].Checked == true)
                                {
                                    string filePath = listViewEx1.Items[i].FileName;
                                    string filename = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                                    DBHelper.BLL.multi_media_info insM = new DBHelper.BLL.multi_media_info();
                                    int type_dmt = 1;
                                    if (listViewEx1.Items[i].Text.IndexOf("BG_") != -1)
                                    {
                                        type_dmt = 4;
                                    }
                                    else if (listViewEx1.Items[i].Text.IndexOf("QC_") != -1)
                                    {
                                        type_dmt = 1;
                                    }
                                    if (insM.XzData(study_no, filename, type_dmt, textBoxX1.Text.Trim()) == 1)
                                    {

                                        string remoteFile = string.Format(@"{0}/{1}/{2}/{3}/{4}.jpg", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, textBoxX1.Text.Trim(), filename);
                                        //2.上传 VBProcess.ftpUpload(strFileName, remoteFile,"127.0.0.1", "21", "peerct", "125353Ct")
                                        if (FtpUpload(@remoteFile, filePath))
                                        {
                                            if (System.IO.File.Exists(filePath))
                                            {
                                                System.IO.File.Delete(filePath);
                                            }
                                            zhflag = true;
                                        }
                                    }
                                }

                            }
                            if (zhflag)
                            {
                                Frm_TJInfo("提示", "修正成功！");
                                this.RefreshMultiList();
                            }
                        }
                    }

                }
                else
                {

                    Frm_TJInfo("提示", "清重新输入要修正的病理号！\n修正病理号不存在！");
                }


            }
            else
            {
                Frm_TJInfo("提示", "清先输入要修正的病理号！");
            }
        }


        public static string FtpIp = Program.FtpIP;
        public static int FtpPort = Program.FtpPort;
        public static string FtpUser = Program.FtpUser;
        public static string FtpPwd = Program.FtpPwd;
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




    }
}
