using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PIS_Sys
{
    public partial class Frm_Ftp_Down : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Ftp_Down()
        {
            InitializeComponent();
        }
        public string ImgFolder = "";
        public string ImgQcFolder = "";
        public string ReportFolder = "";
        public string study_no = "";
        private void Frm_Ftp_Down_Load(object sender, EventArgs e)
        {
            labelX1.Visible = true;
            ftpDownload1.Visible = false;
            ftpDownload1.DownLoadError += new FtpDownloader.FtpDownload.DownLoadErrorEventHandler(OnDownLoadError);
            ftpDownload1.DownLoadComplete += new FtpDownloader.FtpDownload.DownLoadCompleteEventHandler(OnDownLoadComplete);
            timer1.Enabled = true;
        }
        // 定义调用方法的委托
        delegate void FunDelegateStart();
        void OnDownLoadComplete()
        {
            if (InvokeRequired)
            {
                // 要调用的方法的委托
                FunDelegateStart funDelegate = new FunDelegateStart(ftpDownload1_DownLoadComplete);
                this.BeginInvoke(funDelegate);
            }
        }
        private void ftpDownload1_DownLoadComplete()
        {
            ftpDownload1.Visible = false;
            this.Close();
        }
        //
        delegate void FunFtpError(string errorinfo);
        void OnDownLoadError(string errorinfo)
        {
            if (InvokeRequired)
            {
                // 要调用的方法的委托
                FunFtpError funDelegate = new FunFtpError(ftpDownload1_DownLoadError);
                this.BeginInvoke(funDelegate, new object[] { errorinfo });
            }
        }
        private void ftpDownload1_DownLoadError(string errorinfo)
        {
            Program.FileLog.Error(errorinfo);
            Application.DoEvents();
            ftpDownload1.SetOver();
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //多媒体报告资料下载
            DBHelper.BLL.multi_media_info insMulti = new DBHelper.BLL.multi_media_info();
            DataTable dtMulit = null;
            List<string> RemoteLst = new List<string>();
            List<string> LocalLst = new List<string>();

            if (!ImgFolder.Equals(""))
            {
                //报告采集图多媒体资料下载
                dtMulit = insMulti.GetData(study_no, 4);
                if (dtMulit != null && dtMulit.Rows.Count > 0)
                {
                    string PathImg = ImgFolder + @"\" + study_no;
                    if (Directory.Exists(PathImg) == false)
                    {
                        Directory.CreateDirectory(PathImg);
                        for (int i = 0; i < dtMulit.Rows.Count; i++)
                        {
                            RemoteLst.Add(dtMulit.Rows[i]["path"].ToString());
                            LocalLst.Add(string.Format(@"{0}\{1}", PathImg, dtMulit.Rows[i]["filename"].ToString()));
                        }

                    }
                    else
                    {
                        DirectoryInfo Dir = new DirectoryInfo(PathImg);
                        int fileCount = Dir.GetFiles().Count();
                        if (dtMulit.Rows.Count > fileCount)
                        {
                            Boolean downFlag = true;
                            for (int j = 0; j < dtMulit.Rows.Count; j++)
                            {
                                foreach (FileInfo FI in Dir.GetFiles())
                                {
                                    if (FI.Name.Equals(dtMulit.Rows[j]["filename"].ToString()))
                                    {
                                        downFlag = false;
                                        break;
                                    }
                                }
                                if (downFlag)
                                {
                                    RemoteLst.Add(dtMulit.Rows[j]["path"].ToString());
                                    LocalLst.Add(string.Format(@"{0}\{1}", PathImg, dtMulit.Rows[j]["filename"].ToString()));
                                }
                                downFlag = true;
                            }
                        }

                    }
                }
            }


            if (!ImgQcFolder.Equals(""))
            {
                //大体多媒体取材资料下载

                dtMulit = insMulti.GetData(study_no, 1);
                if (dtMulit != null && dtMulit.Rows.Count > 0)
                {
                    string PathImg = ImgQcFolder + @"\" + study_no;
                    if (Directory.Exists(PathImg) == false)
                    {
                        Directory.CreateDirectory(PathImg);
                        //开始下载

                        for (int i = 0; i < dtMulit.Rows.Count; i++)
                        {
                            RemoteLst.Add(dtMulit.Rows[i]["path"].ToString());
                            LocalLst.Add(string.Format(@"{0}\{1}", PathImg, dtMulit.Rows[i]["filename"].ToString()));
                        }

                    }
                    else
                    {
                        DirectoryInfo Dir = new DirectoryInfo(PathImg);
                        int fileCount = Dir.GetFiles().Count();
                        if (dtMulit.Rows.Count > fileCount)
                        {
                            //下载剩余的

                            Boolean downFlag = true;
                            for (int j = 0; j < dtMulit.Rows.Count; j++)
                            {
                                foreach (FileInfo FI in Dir.GetFiles())
                                {
                                    if (FI.Name.Equals(dtMulit.Rows[j]["filename"].ToString()))
                                    {
                                        downFlag = false;
                                        break;
                                    }
                                }
                                if (downFlag)
                                {
                                    RemoteLst.Add(dtMulit.Rows[j]["path"].ToString());
                                    LocalLst.Add(string.Format(@"{0}\{1}", PathImg, dtMulit.Rows[j]["filename"].ToString()));
                                }
                                downFlag = true;
                            }


                        }

                    }
                }
            }

            if (!ReportFolder.Equals(""))
            {
                //jpeg格式整体报告多媒体资料下载

                dtMulit = insMulti.GetData(study_no, 7);
                if (dtMulit != null && dtMulit.Rows.Count > 0)
                {
                    string PathImg = ReportFolder + @"\" + study_no;
                    if (Directory.Exists(PathImg) == false)
                    {
                        Directory.CreateDirectory(PathImg);
                        for (int i = 0; i < dtMulit.Rows.Count; i++)
                        {
                            RemoteLst.Add(dtMulit.Rows[i]["path"].ToString());
                            LocalLst.Add(string.Format(@"{0}\{1}", PathImg, dtMulit.Rows[i]["filename"].ToString()));
                        }

                    }
                    else
                    {
                        DirectoryInfo Dir = new DirectoryInfo(PathImg);
                        int fileCount = Dir.GetFiles().Count();
                        if (dtMulit.Rows.Count > fileCount)
                        {
                            //下载剩余的

                            Boolean downFlag = true;
                            for (int j = 0; j < dtMulit.Rows.Count; j++)
                            {
                                foreach (FileInfo FI in Dir.GetFiles())
                                {
                                    if (FI.Name.Equals(dtMulit.Rows[j]["filename"].ToString()))
                                    {
                                        downFlag = false;
                                        break;
                                    }
                                }
                                if (downFlag)
                                {
                                    RemoteLst.Add(dtMulit.Rows[j]["path"].ToString());
                                    LocalLst.Add(string.Format(@"{0}\{1}", PathImg, dtMulit.Rows[j]["filename"].ToString()));
                                }
                                downFlag = true;
                            }
                        }
                    }
                }
            }


            if (RemoteLst.Count > 0)
            {
                try
                {
                    ftpDownload1.ExecuteDownLoad(Program.FtpIP, Program.FtpPort, Program.FtpUser, Program.FtpPwd, RemoteLst, LocalLst, 50000, RemoteLst.Count);
                    ftpDownload1.Visible = true;
                    labelX1.Visible = false;
                    Application.DoEvents();
                }
                catch
                {
                    ftpDownload1.Visible = false;
                    this.Close();
                }
            }
            else
            {
                this.Close();
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

        private void Frm_Ftp_Down_FormClosed(object sender, FormClosedEventArgs e)
        {
            //结束下载
            ftpDownload1.SetOver();
        }




    }
}
