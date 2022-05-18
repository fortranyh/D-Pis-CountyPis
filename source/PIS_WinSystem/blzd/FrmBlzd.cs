using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Video.FFMPEG;
using AForge.Vision.Motion;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar.SuperGrid.Style;
using DVPCameraType;
using Greenshot;
using Greenshot.Drawing;
using Greenshot.IniFile;
using Greenshot.Plugin;
using GreenshotPlugin.Core;
using Manina.Windows.Forms;
using PIS_Sys.qcgl;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class FrmBlzd
    {

        #region "初始化逻辑"


        //提示窗体
        public void Frm_TJInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(this);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = true;
            Frm_AlertIns.AutoCloseTimeOut = 2;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }
        Color CurBtnColor;
        string VideoFolder;
        string ImgFolder;
        string AudioFolder;
        string ReportFolder;
        string DTImgFolder;
        Boolean thumsFlag = false;
        Dictionary<string, Color> exam_status_dic = new Dictionary<string, Color>();
        Dictionary<string, Color> exam_status_name_dic = new Dictionary<string, Color>();
        Dictionary<string, string> examStatus_dic = new Dictionary<string, string>();
        //设置报告书写可见性(modality和报告书写界面tab索引对应关系字典)
        Dictionary<string, int> exam_type_dict = new Dictionary<string, int>();
        //窗体显示设置
        private void FrmBlzd_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
        //窗体加载时初始化
        private void FrmBlzd_Load(object sender, EventArgs e)
        {
            cmbBgnr.Items.Add("是");
            cmbBgnr.Items.Add("否");
            cmbBgnr.SelectedIndex = 0;
            cmbBbqc.Items.Add("");
            cmbBbqc.Items.Add("标本过小");
            cmbBbqc.Items.Add("标本挤压");
            cmbBbqc.Items.Add("取材代表性不够");
            cmbBbqc.SelectedIndex = 0;
            //电子病历接口
            if (Program.EmrUrlStr.Equals(""))
            {
                buttonX8.Visible = false;
            }
            //PACS接口
            if (Program.PACSUrlStr.Equals(""))
            {
                buttonX12.Visible = false;
            }
            //启动质控
            if (PIS_Sys.Properties.Settings.Default.zkapp_flag)
            {
                ZhiKongApp();
            }
            //
            if (Program.workstation_type_db.Contains("XBX") && !Program.workstation_type_db.Contains("PL"))
            {
                btn_BD.Visible = false;
                btn_QC.Visible = false;
                btn_jsyz.Visible = false;
            }
            //系统字体大小
            this.ComboBox2.Items.Add("8");
            this.ComboBox2.Items.Add("9");
            this.ComboBox2.Items.Add("10");
            this.ComboBox2.Items.Add("11");
            this.ComboBox2.Items.Add("12");
            this.ComboBox2.Items.Add("14");
            this.ComboBox2.Items.Add("16");
            //
            this.comboBox1.Items.Add("8");
            this.comboBox1.Items.Add("9");
            this.comboBox1.Items.Add("10");
            this.comboBox1.Items.Add("11");
            this.comboBox1.Items.Add("12");
            this.comboBox1.Items.Add("14");
            this.comboBox1.Items.Add("16");
            ComboBox2.SelectedIndex = 4;
            comboBox1.SelectedIndex = 4;
            //绑定初诊医生
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

            //规培医师 
            cmb_gpys.Items.Add("");
            DataSet dsgb = doctor_dict_ins.GetDsGB_User(Program.Dept_Code);
            if (dsgb != null && dsgb.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsgb.Tables[0].Rows.Count; i++)
                {
                    cmb_gpys.Items.Add(dsgb.Tables[0].Rows[i]["user_name"].ToString());
                }
            }
            //主诊医生
            DataSet dszz = doctor_dict_ins.GetDsExam_User(Program.Dept_Code);
            if (dszz != null && dszz.Tables[0].Rows.Count > 0)
            {
                comboBoxEx2.DataSource = dszz.Tables[0];
                comboBoxEx2.DisplayMember = "user_name";
                comboBoxEx2.ValueMember = "user_code";
                comboBoxEx2.Text = Program.User_Name;
            }
            else
            {
                comboBoxEx2.DataSource = null;
            }
            //
            superTabControl2.AllowDrop = false;
            superTabControl1.AllowDrop = false;
            superTabControl2.ReorderTabsEnabled = false;
            superTabControl1.ReorderTabsEnabled = false;
            //加载检查类型默认模板
            DBHelper.BLL.exam_type InsType = new DBHelper.BLL.exam_type();
            DataTable dtType = InsType.GetBigTypeDt();
            if (dtType != null && dtType.Rows.Count > 0)
            {
                for (int i = 0; i < dtType.Rows.Count; i++)
                {
                    exam_type_dict[dtType.Rows[i]["modality"].ToString()] = Convert.ToInt32(dtType.Rows[i]["default_templet_index"]);
                }
            }
            //加载检查状态
            DBHelper.BLL.exam_status exam_status_ins = new DBHelper.BLL.exam_status();
            List<DBHelper.Model.exam_status> lst = exam_status_ins.GetModelBGExam_Status();
            if (lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    //添加颜色表
                    exam_status_dic[lst[i].status_code] = Color.FromArgb(Convert.ToInt32(lst[i].status_color));
                    exam_status_name_dic[lst[i].status_name] = Color.FromArgb(Convert.ToInt32(lst[i].status_color));
                    examStatus_dic[lst[i].status_code] = lst[i].status_name;
                    if (lst[i].status_code.Equals("-9") || (Convert.ToInt32(lst[i].status_code) > 29 && Convert.ToInt32(lst[i].status_code) <= 55))
                    {
                        DevComponents.Editors.ComboItem cmbitem = new DevComponents.Editors.ComboItem();
                        cmbitem.Text = lst[i].status_name;
                        cmbitem.Value = lst[i].status_code;
                        cmb_ExamStatus.Items.Add(cmbitem);
                    }
                }
                lst.Clear();
                //添加我的报告
                DevComponents.Editors.ComboItem cmbitemBG = new DevComponents.Editors.ComboItem();
                cmbitemBG.Text = "我的报告";
                cmbitemBG.Value = "90";
                cmb_ExamStatus.Items.Add(cmbitemBG);
            }
            cmb_tj.SelectedIndex = 0;
            cmb_ExamStatus.SelectedIndex = 0;
            chK_status.Checked = true;
            //添加检查类别绑定
            if (!Program.workstation_type_name.Equals(""))
            {

                string[] txts = Program.workstation_type_name.Split(',');
                string[] txts_tag = Program.workstation_type_db.Split(',');
                if (txts.Length > 1)
                {
                    BtnJclb.Visible = true;
                    DevComponents.DotNetBar.ButtonItem buttonItemAll = new DevComponents.DotNetBar.ButtonItem();
                    buttonItemAll.Text = "全部";
                    buttonItemAll.Tag = "all";
                    BtnJclb.SubItems.Add(buttonItemAll);
                    for (int j = 0; j < txts.Length; j++)
                    {
                        DevComponents.DotNetBar.ButtonItem buttonItemins = new DevComponents.DotNetBar.ButtonItem();
                        buttonItemins.Text = txts[j].ToString();
                        buttonItemins.Tag = txts_tag[j].ToString();
                        BtnJclb.SubItems.Add(buttonItemins);
                    }
                    //绑定事件处理
                    for (int i = 0; i < this.BtnJclb.SubItems.Count; i++)
                    {
                        ButtonItem BtnItem = ((ButtonItem)this.BtnJclb.SubItems[i]);
                        if (i == 0)
                        {
                            BtnItem.Checked = true;
                        }
                        else
                        {
                            BtnItem.Checked = false;
                        }
                        BtnItem.Click += new System.EventHandler(Chk_BtnJclb);
                    }
                }
                else
                {
                    BtnJclb.Visible = false;
                }
            }
            else
            {
                BtnJclb.Visible = false;
            }

            //绑定事件处理
            for (int i = 0; i < this.QueryBtn.SubItems.Count; i++)
            {
                ButtonItem BtnItem = ((ButtonItem)this.QueryBtn.SubItems[i]);
                if (BtnItem.Text.Equals(PIS_Sys.Properties.Settings.Default.Refresh_Date_BG))
                {
                    BtnItem.Checked = true;
                }
                else
                {
                    BtnItem.Checked = false;
                }
                BtnItem.Click += new System.EventHandler(Chk_BtnRq);
            }
            //列表样式
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.AllCells;
            GridPanel panel2 = superGridControl1.PrimaryGrid;
            panel2.Name = "exam_master_view";
            panel2.MinRowHeight = 30;
            //锁定1列
            panel2.FrozenColumnCount = 1;
            panel2.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ReadOnly = true;
                panel2.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //是否存在多媒体文件
            //1视频路径
            VideoFolder = Program.APPdirPath + @"\Pis_Cap_Video";
            if (Directory.Exists(VideoFolder) == false)
            {
                Directory.CreateDirectory(VideoFolder);
            }
            //2图像路径
            ImgFolder = Program.APPdirPath + @"\Pis_Cap_Image";
            if (Directory.Exists(ImgFolder) == false)
            {
                Directory.CreateDirectory(ImgFolder);
            }
            //3录音路径
            AudioFolder = Program.APPdirPath + @"\Pis_Cap_Audio";
            if (Directory.Exists(AudioFolder) == false)
            {
                Directory.CreateDirectory(AudioFolder);
            }
            //4报告图片路径
            ReportFolder = Program.APPdirPath + @"\Pis_Report_Image";
            if (Directory.Exists(ReportFolder) == false)
            {
                Directory.CreateDirectory(ReportFolder);
            }
            //5.大体图像路径
            DTImgFolder = Program.APPdirPath + @"\Pis_Image";
            if (Directory.Exists(DTImgFolder) == false)
            {
                Directory.CreateDirectory(DTImgFolder);
            }
            //加载图像列表
            Type colorType = typeof(ImageListViewColor);
            int K = 0;
            foreach (PropertyInfo field in colorType.GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                colorToolStripComboBox.Items.Add(new ColorComboBoxItem(field));
                if (field.Name == "Default")
                    colorToolStripComboBox.SelectedIndex = K;
                K++;
            }
            //诊断意见模版
            BuildBgTree();
            //缩略图大小
            if (PIS_Sys.Properties.Settings.Default.BGthumsWidth == 0 || PIS_Sys.Properties.Settings.Default.BGthumsHeight == 0)
            {
                thumsFlag = true;
            }
            else
            {
                thumsFlag = false;
                imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.BGthumsWidth, PIS_Sys.Properties.Settings.Default.BGthumsHeight);
            }
            //光标位置记录
            txt_zdyj.Enter += new EventHandler(txt_zdyj_Enter);
            txt_xbxXfs.Enter += new EventHandler(txt_xbxXfs_Enter);
            txt_Wy_zdyj.Enter += new EventHandler(richTextBoxEx1_Enter);

            //创建常用词目录
            BuildDiagnosisWordsTree();
            //图像采集
            btn_closecj.Enabled = false;
            btn_PZ.Enabled = false;
            btn_LX.Enabled = false;
            btn_lxstop.Enabled = false;
            //缩略图不显示滚动条
            imageListView1.ScrollBars = false;
            //加载摄像头
            LoadVidoSource();
            if (Program.UsbCameraType == 0 && videoDevices != null)
            {
                foreach (FilterInfo device in videoDevices)
                {
                    if (device.Name.Equals(PIS_Sys.Properties.Settings.Default.BGDevice))
                    {
                        devicesCombo.Text = PIS_Sys.Properties.Settings.Default.BGDevice;
                        break;
                    }
                }
            }
            else if (Program.UsbCameraType == 1)
            {
                if (devicesCombo.Items.Contains(PIS_Sys.Properties.Settings.Default.BGDevice))
                {
                    devicesCombo.Text = PIS_Sys.Properties.Settings.Default.BGDevice;
                }
            }
            ftpDownload1.Visible = false;
            ftpDownload1.DownLoadComplete += new FtpDownloader.FtpDownload.DownLoadCompleteEventHandler(OnDownLoadComplete);
            ftpDownload1.DownLoadError += new FtpDownloader.FtpDownload.DownLoadErrorEventHandler(OnDownLoadError);
            //临床符合 诊断编码字典
            this.MenuItemClick += new MenuEventHandler(bbxx_MenuItemClick);
            CreatePopUpJcbwMenu(0);
            CreatePopUpJcbwMenu(3);
            //加载报告模板 
            DBHelper.BLL.exam_report insTemp = new DBHelper.BLL.exam_report();
            DataTable dtTemp = insTemp.GetReportTemp(Program.workstation_type_db);
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                for (int i = 0; i < dtTemp.Rows.Count; i++)
                {
                    DevComponents.Editors.ComboItem cmbitem = new DevComponents.Editors.ComboItem();
                    cmbitem.Text = dtTemp.Rows[i]["temp_name"].ToString();
                    cmbitem.Value = dtTemp.Rows[i]["temp_index"].ToString();
                    comboBoxItem1.Items.Add(cmbitem);
                }
                comboBoxItem1.SelectedIndex = 0;
            }
            else
            {
                comboBoxItem1.Visible = false;
            }
        }
        //窗体激活
        private void FrmBlzd_Activated(object sender, EventArgs e)
        {
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //开启焦点并刷新数据
            if (timer1.Enabled == false)
            {
                timer1.Enabled = true;
            }
        }
        //窗体关闭
        private void FrmBlzd_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //结束下载
                ftpDownload1.SetOver();
                // 关闭之前的视频源
                if (Program.UsbCameraType == 0)
                {
                    this.videoSourcePlayer1.NewFrame -= new AForge.Controls.VideoSourcePlayer.NewFrameHandler(this.videoSourcePlayer1_NewFrame);
                    this.videoSourcePlayer1.DoubleClick -= new System.EventHandler(this.videoSourcePlayer1_DoubleClick);
                    CloseVideoSource();
                    StopSaving();
                }
                else if (Program.UsbCameraType == 1)
                {
                    this.digitsCamUserCtl2.MshotCaptureImageEvent -= new MshotDigitsCamControl.DigitsCamUserCtl.CaptureSingleImgHandle(this.digitsCamUserCtl2_MshotCaptureImageEvent);
                    digitsCamUserCtl2.DigitsCamClosed();
                }
            }
            catch (Exception ex)
            {
                Program.FileLog.Error(ex.ToString());
            }
        }

        //通用版本
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        //官方版本
        private MshotDigitsCamControl.DigitsCamUserCtl digitsCamUserCtl2;
        public FrmBlzd()
        {
            InitializeComponent();
            if (Program.UsbCameraType == 0)
            {
                this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
                this.superTabControlPanel4.SuspendLayout();
                this.superTabControlPanel4.Controls.Add(this.videoSourcePlayer1);
                this.superTabControlPanel4.Controls.SetChildIndex(this.videoSourcePlayer1, 0); //设定不被其他控件覆盖，新加
                this.videoSourcePlayer1.BackColor = System.Drawing.Color.Black;
                this.videoSourcePlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.videoSourcePlayer1.KeepAspectRatio = true;
                this.videoSourcePlayer1.Location = new System.Drawing.Point(0, 35);
                this.videoSourcePlayer1.Name = "videoSourcePlayer1";
                this.videoSourcePlayer1.Size = new System.Drawing.Size(760, 577);
                this.videoSourcePlayer1.TabIndex = 47;
                this.videoSourcePlayer1.Text = "videoSourcePlayer1";
                this.videoSourcePlayer1.KeepAspectRatio = true;
                this.videoSourcePlayer1.VideoSource = null;
                this.superTabControlPanel4.ResumeLayout(false);
                this.videoSourcePlayer1.NewFrame += new AForge.Controls.VideoSourcePlayer.NewFrameHandler(this.videoSourcePlayer1_NewFrame);
                this.videoSourcePlayer1.DoubleClick += new System.EventHandler(this.videoSourcePlayer1_DoubleClick);
            }
            else if (Program.UsbCameraType == 1)
            {
                this.digitsCamUserCtl2 = new MshotDigitsCamControl.DigitsCamUserCtl();
                this.superTabControlPanel4.SuspendLayout();
                this.superTabControlPanel4.Controls.Add(this.digitsCamUserCtl2);
                this.superTabControlPanel4.Controls.SetChildIndex(this.digitsCamUserCtl2, 0); //设定不被其他控件覆盖，新加
                this.digitsCamUserCtl2.Dock = System.Windows.Forms.DockStyle.Fill;
                this.digitsCamUserCtl2.Location = new System.Drawing.Point(0, 35);
                this.digitsCamUserCtl2.Name = "digitsCamUserCtl2";
                this.digitsCamUserCtl2.Size = new System.Drawing.Size(760, 577);
                this.digitsCamUserCtl2.TabIndex = 47;
                this.superTabControlPanel4.ResumeLayout(false);
                this.digitsCamUserCtl2.MshotCaptureImageEvent += new MshotDigitsCamControl.DigitsCamUserCtl.CaptureSingleImgHandle(this.digitsCamUserCtl2_MshotCaptureImageEvent);
            }
            //热键操作
            HotkeyRepeatLimit = 1000;
            repeatLimitTimer = Stopwatch.StartNew();
            //
            this.HandleCreated += new EventHandler(FrmBlzd_HandleCreated);
            this.HotkeyPress += new HotkeyEventHandler(Frm_Dtcj_HotkeyPress);
            //
            CurBtnColor = btn_ZP.TextColor;
            //
            btn_up.Enabled = false;
            btn_down.Enabled = false;
            //加载打印机
            foreach (String iprt in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                DevComponents.Editors.ComboItem cmbitem = new DevComponents.Editors.ComboItem();
                cmbitem.Text = iprt;
                cmb_printers.Items.Add(cmbitem);
            }
            //报告打印机
            Boolean xzPrinters = false;
            for (int i = 0; i < cmb_printers.Items.Count; i++)
            {
                if (((DevComponents.Editors.ComboItem)cmb_printers.Items[i]).Text.Equals(PIS_Sys.Properties.Settings.Default.ReportPrinter))
                {
                    cmb_printers.SelectedIndex = i;
                    xzPrinters = true;
                    break;
                }
            }
            if (!xzPrinters)
            {
                if (cmb_printers.Items.Count > 0)
                {
                    cmb_printers.SelectedIndex = 0;
                }
            }
        }
        private void FrmBlzd_HandleCreated(object sender, EventArgs e)
        {
            //注册热键
            hotkey_bbxx = new HotkeyInfo(Keys.F4);
            if (hotkey_bbxx.Status == HotkeyStatus.Registered)
            {
                UnregisterHotkey(hotkey_bbxx);
            }
            if (hotkey_bbxx.Status != HotkeyStatus.Registered && hotkey_bbxx.IsValidHotkey)
            {
                RegisterHotkey(hotkey_bbxx);

                if (hotkey_bbxx.Status == HotkeyStatus.Registered)
                {

                }
                else if (hotkey_bbxx.Status == HotkeyStatus.Failed)
                {

                }
            }
        }
        //获取进程句柄
        public IntPtr GetProcessHandler(string processName)
        {
            Process[] Processes = Process.GetProcessesByName(processName);
            IntPtr hWnd = IntPtr.Zero;
            foreach (Process p in Processes)
            {
                hWnd = p.Handle;
                break;
            }
            return hWnd;
        }
        private void ZhiKongApp()
        {
            //启动即时消息
            IntPtr hWnd = GetProcessHandler("ZhiKongIM");
            if (hWnd == IntPtr.Zero)
            {
                string filePath = Program.APPdirPath + @"\ZhiKongIM";
                if (Directory.Exists(filePath) == true)
                {
                    filePath = Program.APPdirPath + @"\ZhiKongIM\ZhiKongIM.exe";
                    if (!File.Exists(filePath))
                    {
                        return;
                    }
                    else
                    {
                        InvokeExe(Program.APPdirPath, @"ZhiKongIM\ZhiKongIM.exe", "");
                    }
                }
            }
        }
        //调用独立EXE(exe全路径；参数串)
        public void InvokeExe(string path, string exename, string Paras)
        {
            System.Diagnostics.ProcessStartInfo objExecuteFile = new System.Diagnostics.ProcessStartInfo();
            System.Diagnostics.Process objExecute = new System.Diagnostics.Process();
            try
            {
                if (System.IO.File.Exists(path + @"\" + exename) == true)
                {
                    //应用程序路径
                    objExecuteFile.FileName = path + @"\" + exename;
                    //应用程序参数
                    objExecuteFile.Arguments = Paras;
                    objExecuteFile.UseShellExecute = false;
                    objExecute.StartInfo = objExecuteFile;
                    objExecute.Start();
                    objExecute.EnableRaisingEvents = true;
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region "影像下载上传"
        void OnDownLoadComplete()
        {
            try
            {
                if (InvokeRequired)
                {
                    // 要调用的方法的委托
                    FunDelegateStart funDelegate = new FunDelegateStart(ftpDownload1_DownLoadComplete);
                    this.BeginInvoke(funDelegate);
                }
            }
            catch (Exception ex)
            {
                Program.FileLog.Error(ex.ToString());
            }
        }
        private void ftpDownload1_DownLoadComplete()
        {
            try
            {
                ftpDownload1.Visible = false;
                RefreshMultiList();
                if (superTabControl1.SelectedTabIndex == 3)
                {
                    this.reportViewer1.Visible = true;
                    RefreshPreviewReport();
                }
            }
            catch (Exception ex)
            {
                Program.FileLog.Error(ex.ToString());
            }

        }
        delegate void FunFtpError(string errorinfo);
        void OnDownLoadError(string errorinfo)
        {
            try
            {
                if (InvokeRequired)
                {
                    // 要调用的方法的委托
                    FunFtpError funDelegate = new FunFtpError(ftpDownload1_DownLoadError);
                    this.BeginInvoke(funDelegate, new object[] { errorinfo });
                }
            }
            catch (Exception ex)
            {
                Program.FileLog.Error(ex.ToString());
            }
        }
        private void ftpDownload1_DownLoadError(string errorinfo)
        {
            try
            {
                Program.FileLog.Error(errorinfo);
                ftpDownload1.Visible = false;
                ftpDownload1.SetOver();
                if (superTabControl1.SelectedTabIndex == 3)
                {
                    this.reportViewer1.Visible = true;
                    RefreshPreviewReport();
                }
            }
            catch (Exception ex)
            {
                Program.FileLog.Error(ex.ToString());
            }
        }
        #endregion

        #region  "模版相关"

        //关键词字典维护
        private void advTree1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (advTree1.SelectedNode != null)
                {
                    添加ToolStripMenuItem.Visible = true;
                    删除ToolStripMenuItem.Visible = true;
                    编辑ToolStripMenuItem.Visible = true;

                    if (advTree1.SelectedNode.Parent == null)
                    {
                        删除ToolStripMenuItem.Visible = false;
                        编辑ToolStripMenuItem.Visible = false;
                        Point p = advTree1.PointToScreen(new Point(e.X, e.Y));
                        this.contextMenuStrip1.Show(p);
                        return;
                    }

                    if (advTree1.SelectedNode.Parent.Parent == null)
                    {
                        Point p = advTree1.PointToScreen(new Point(e.X, e.Y));
                        this.contextMenuStrip1.Show(p);
                    }
                    else
                    {
                        if (advTree1.SelectedNode.TagString != "-1")
                        {
                            添加ToolStripMenuItem.Visible = false;
                            Point p = advTree1.PointToScreen(new Point(e.X, e.Y));
                            this.contextMenuStrip1.Show(p);
                        }

                    }
                }
            }
        }
        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNode();
        }
        //添加节点
        private void AddNode()
        {
            //添加树形节点
            if (advTree1.SelectedNode != null)
            {
                //----------------------------
                string uid = Helper.GetUidStr();
                //
                string puid = advTree1.SelectedNode.TagString;
                DBHelper.BLL.exam_diagnosiswords_dict ins = new DBHelper.BLL.exam_diagnosiswords_dict();
                Boolean zxResult = false;
                if (advTree1.SelectedNode.TagString == "-1")
                {
                    zxResult = ins.InsertParts(uid, "新节点", "0");
                }
                else
                {

                    zxResult = ins.InsertParts(uid, "新节点", puid);
                }
                if (zxResult == true)
                {
                    //----------------------------
                    DevComponents.AdvTree.Node lvm;
                    lvm = new DevComponents.AdvTree.Node();
                    lvm.Tag = uid;
                    lvm.Text = "新节点";
                    advTree1.SelectedNode.Nodes.Add(lvm);
                    advTree1.SelectedNode.Expand();
                    advTree1.SelectedNode = lvm;
                    advTree1.CellEdit = true;
                    advTree1.SelectedNode.BeginEdit();
                }
            }
        }
        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modifyNode();
        }
        private void modifyNode()
        {
            //更新树形节点
            if (advTree1.SelectedNode != null && advTree1.SelectedNode.Parent != null)
            {
                advTree1.CellEdit = true;
                if (!advTree1.SelectedNode.IsEditing)
                {
                    advTree1.SelectedNode.BeginEdit();
                }
            }

        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelNode();
        }
        //删除节点
        public void DelNode()
        {
            //删除树形节点
            if (advTree1.SelectedNode != null && advTree1.SelectedNode.Parent != null)
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;

                if (TaskDialog.Show("询问", "确认", "确定要删除此部位么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    DevComponents.AdvTree.Node curPTreeNode;
                    curPTreeNode = advTree1.SelectedNode.Parent;

                    //遍历删除库
                    delTreeDb(advTree1.SelectedNode.Tag.ToString());
                    //遍历删除节点
                    DelNodeCache(advTree1.SelectedNode);
                }

            }
        }
        public void DelNodeCache(DevComponents.AdvTree.Node node)
        {
            if (node.Nodes.Count > 0)
            {
                for (int i = node.Nodes.Count - 1; i >= 0; i--)
                {
                    DelNodeCache(node.Nodes[i]);
                }
            }
            node.Remove();
        }
        private void delTreeDb(string id)
        {
            //删库
            DBHelper.BLL.exam_diagnosiswords_dict ins = new DBHelper.BLL.exam_diagnosiswords_dict();
            DataTable dt = ins.GetChildParts(id);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    delTreeDb(dt.Rows[i][0].ToString());
                }
            }
            ins.DelParts(id);
        }
        private void advTree1_AfterCellEditComplete(object sender, CellEditEventArgs e)
        {
            if (e.Cell != null)
            {
                if (e.NewText.Length > 0)
                {

                    advTree1.SelectedNode.EndEdit(false);

                    //更新标题
                    DBHelper.BLL.exam_diagnosiswords_dict ins = new DBHelper.BLL.exam_diagnosiswords_dict();
                    ins.updatePartText(e.NewText, Convert.ToString(e.Cell.Tag));
                }
                else
                {
                    advTree1.SelectedNode.EndEdit(true);
                    e.Cell.Editable = true;
                    advTree1.SelectedNode.BeginEdit();
                }
            }
        }


        private void BuildBgTree()
        {
            //移除单击事件处理程序
            this.advTree2.NodeClick -= new TreeNodeMouseEventHandler(advTree2_NodeClick);
            this.advTree2.NodeDoubleClick -= new TreeNodeMouseEventHandler(advTree2_NodeDoubleClick);

            string RootID;
            string strRootText;
            advTree2.Nodes[0].Nodes.Clear();
            DBHelper.BLL.blzd_templet ins = new DBHelper.BLL.blzd_templet();
            DataTable dt = ins.GetTreeBlzd_Templet(Program.User_Code);
            if (dt != null && dt.Rows.Count == 0)
            {
                //插入根节点
                string uid = Helper.GetUidStr();
                ins.InsertBLtempletRoot(Program.User_Code, uid);
                dt = ins.GetTreeBlzd_Templet(Program.User_Code);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                string fliter = "parentid='-1'";
                DataRow[] drArr = dt.Select(fliter);
                advTree2.BeginUpdate();
                int i = 0;
                foreach (DataRow dr in drArr)
                {
                    RootID = dr["id"].ToString();
                    strRootText = dr["title"].ToString();
                    Node node = new Node();
                    node.Tag = RootID;
                    node.Text = strRootText;
                    node.ImageExpanded = global::PIS_Sys.Properties.Resources.NoteHS;
                    advTree2.Nodes[0].Nodes.Add(node);

                    CreateTreeViewRecursive2(advTree2.Nodes[0].Nodes[i].Nodes, dt, RootID);

                    if (node.Nodes.Count > 0)
                    {
                        node.ExpandVisibility = eNodeExpandVisibility.Visible;
                        node.Image = global::PIS_Sys.Properties.Resources.cc;
                    }
                    else
                    {
                        node.ExpandVisibility = eNodeExpandVisibility.Hidden;
                        node.Image = global::PIS_Sys.Properties.Resources.ShowRulelinesHS;
                    }
                    //移除已添加行，提高性能
                    dt.Rows.Remove(dr);
                    i += 1;
                }
                advTree2.EndUpdate();
                if (advTree2.Nodes[0].Nodes.Count > 0)
                {
                    advTree2.Nodes[0].Nodes[0].Expand();
                }
                //添加单击事件处理程序
                this.advTree2.NodeClick += new TreeNodeMouseEventHandler(advTree2_NodeClick);
                this.advTree2.NodeDoubleClick += new TreeNodeMouseEventHandler(advTree2_NodeDoubleClick);
            }
        }

        private void advTree2_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {

            if (e.Node.Nodes.Count == 0)
            {
                string id = Convert.ToString(e.Node.Tag);
                DBHelper.BLL.blzd_templet ins = new DBHelper.BLL.blzd_templet();
                DBHelper.Model.bgnr_templet Inscontent = ins.GetMContent(id, Program.User_Code);
                richTextBoxEx2.Focus();
                richTextBoxEx2.Text = Inscontent.content;
            }
        }
        private void advTree2_NodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            if (btn_save1.Enabled == false)
            {
                return;
            }
            if (e.Node.Nodes.Count == 0)
            {
                string id = Convert.ToString(e.Node.Tag);
                DBHelper.BLL.blzd_templet ins = new DBHelper.BLL.blzd_templet();
                DBHelper.Model.bgnr_templet Inscontent = ins.GetMContent(id, Program.User_Code);
                if (report_type == 0)
                {
                    if (!Inscontent.content.Equals("") && !txt_zdyj.Text.Trim().Equals(""))
                    {
                        eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                        TaskDialog.EnableGlass = false;

                        if (TaskDialog.Show("询问", "确认", "确定替换现有的诊断意见么？", Curbutton) == eTaskDialogResult.Ok)
                        {
                            txt_zdyj.Focus();
                            txt_zdyj.Text = Inscontent.content;
                            this.txt_zdyj.SelectionStart = Inscontent.content.Length;
                        }
                    }
                    else
                    {
                        if (!Inscontent.content.Equals(""))
                        {
                            txt_zdyj.Text = Inscontent.content;
                            txt_zdyj.Focus();
                            txt_zdyj.SelectionStart = Inscontent.content.Length;
                        }
                    }
                }
                else if (report_type == 1)
                {

                }
                else if (report_type == 3)
                {
                    if (!Inscontent.content.Equals("") && !txt_xbxXfs.Text.Trim().Equals(""))
                    {
                        eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                        TaskDialog.EnableGlass = false;

                        if (TaskDialog.Show("询问", "确认", "确定替换现有的诊断意见么？", Curbutton) == eTaskDialogResult.Ok)
                        {
                            txt_xbxXfs.Focus();
                            txt_xbxXfs.Text = Inscontent.content;
                            txt_xbxXfs.SelectionStart = Inscontent.content.Length;
                        }
                    }
                    else
                    {
                        if (!Inscontent.content.Equals(""))
                        {
                            txt_xbxXfs.Text = Inscontent.content;
                            txt_xbxXfs.Focus();
                            txt_xbxXfs.SelectionStart = Inscontent.content.Length;
                        }
                    }
                }
                else if (report_type == 5)
                {
                    if (!Inscontent.content.Equals("") && !txt_Wy_zdyj.Text.Trim().Equals(""))
                    {
                        eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                        TaskDialog.EnableGlass = false;

                        if (TaskDialog.Show("询问", "确认", "确定替换现有的诊断意见么？", Curbutton) == eTaskDialogResult.Ok)
                        {
                            txt_Wy_zdyj.Focus();
                            txt_Wy_zdyj.Text = Inscontent.content;
                            this.txt_Wy_zdyj.SelectionStart = Inscontent.content.Length;
                        }
                    }
                    else
                    {
                        if (!Inscontent.content.Equals(""))
                        {
                            txt_Wy_zdyj.Text = Inscontent.content;
                            txt_Wy_zdyj.Focus();
                            txt_Wy_zdyj.SelectionStart = Inscontent.content.Length;
                        }
                    }
                }

            }
        }

        private void CreateTreeViewRecursive2(NodeCollection nodes, DataTable dataSource, string parentId)
        {
            dataSource.DefaultView.Sort = "  TreeLevel asc,autoid asc";
            string fliter = "parentid='" + parentId + "'";
            //查询子节点
            DataRow[] drArr = dataSource.Select(fliter);
            Node node;
            foreach (DataRow dr in drArr)
            {
                node = new Node();
                node.Tag = dr["id"].ToString();
                node.Text = dr["title"].ToString();
                node.ImageExpanded = global::PIS_Sys.Properties.Resources.NoteHS;
                nodes.Add(node);
                //递归创建子节点
                CreateTreeViewRecursive2(node.Nodes, dataSource, Convert.ToString(dr["id"]));

                if (node.Nodes.Count > 0)
                {
                    node.ExpandVisibility = eNodeExpandVisibility.Visible;
                    node.Image = global::PIS_Sys.Properties.Resources.cc;
                }
                else
                {
                    node.ExpandVisibility = eNodeExpandVisibility.Hidden;
                    node.Image = global::PIS_Sys.Properties.Resources.ShowRulelinesHS;
                }
                //移除已添加行，提高性能
                dataSource.Rows.Remove(dr);
            }
        }

        //细胞胸腹水（诊断意见）
        public void txt_xbxXfs_Enter(object sender, EventArgs e)
        {
            if (btn_save1.Enabled == true)
            {
                report_type = 3;
                advTree2.Tag = report_type;
                superTabControl2.SelectedTabIndex = 1;
                txt_xbxXfs.Focus();
            }
        }
        //外院会诊（诊断意见）
        public void richTextBoxEx1_Enter(object sender, EventArgs e)
        {
            if (btn_save1.Enabled == true)
            {
                report_type = 5;
                advTree2.Tag = report_type;
                superTabControl2.SelectedTabIndex = 1;
                txt_Wy_zdyj.Focus();
            }
        }


        //病理诊断(诊断意见)
        public void txt_zdyj_Enter(object sender, EventArgs e)
        {
            if (btn_save1.Enabled == true)
            {
                report_type = 0;
                advTree2.Tag = report_type;
                superTabControl2.SelectedTabIndex = 1;
                txt_zdyj.Focus();
            }
        }

        //创建常用词目录
        private void BuildDiagnosisWordsTree()
        {
            DBHelper.BLL.exam_diagnosiswords_dict ins = new DBHelper.BLL.exam_diagnosiswords_dict();
            DataTable dt = ins.GetTreeExam_diagnosiswords_dict();
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.DefaultView.Sort = " order_no asc";
                string fliter = "parent_code='0'";
                DataRow[] drArr = dt.Select(fliter);
                advTree1.BeginUpdate();
                foreach (DataRow dr in drArr)
                {
                    Node node = new Node();
                    node.Tag = dr["id"].ToString();
                    node.TagString = dr["id"].ToString();
                    node.Text = dr["part_name"].ToString();
                    node.ImageExpanded = global::PIS_Sys.Properties.Resources.NoteHS;
                    CreateTreeViewRecursive(node.Nodes, dt, dr["id"].ToString());
                    advTree1.Nodes[0].Nodes.Add(node);
                    if (node.Nodes.Count > 0)
                    {
                        node.ExpandVisibility = eNodeExpandVisibility.Visible;
                        node.Image = global::PIS_Sys.Properties.Resources.cc;
                    }
                    else
                    {
                        node.ExpandVisibility = eNodeExpandVisibility.Hidden;
                        node.Image = global::PIS_Sys.Properties.Resources.ShowRulelinesHS;
                    }
                    //移除已添加行，提高性能
                    dt.Rows.Remove(dr);
                }
                advTree1.EndUpdate();
                //添加单击事件处理程序
                this.advTree1.NodeClick += new TreeNodeMouseEventHandler(advTree1_NodeClick);
            }
        }
        private void CreateTreeViewRecursive(NodeCollection nodes, DataTable dataSource, string parentId)
        {
            dataSource.DefaultView.Sort = " order_no asc";
            string fliter = "parent_code='" + parentId + "'";
            //查询子节点
            DataRow[] drArr = dataSource.Select(fliter);
            foreach (DataRow dr in drArr)
            {
                Node node = new Node();
                node.Tag = dr["id"].ToString();
                node.TagString = dr["id"].ToString();
                node.Text = dr["part_name"].ToString();
                node.Image = global::PIS_Sys.Properties.Resources.ShowRulelinesHS;
                nodes.Add(node);
                //移除已添加行，提高性能
                dataSource.Rows.Remove(dr);
            }
        }
        //赋值选中的关键词
        private void advTree1_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            if (btn_save1.Enabled == false)
            {
                return;
            }
            if (e.Node.Nodes.Count == 0)
            {
                if (report_type == 0)
                {
                    int oldlength = txt_zdyj.Text.Length;
                    txt_zdyj.Text += e.Node.Text;
                    txt_zdyj.Focus();
                    txt_zdyj.SelectionStart = oldlength + e.Node.Text.Length;
                }
                else if (report_type == 1)
                {

                }
                else if (report_type == 3)
                {
                    int oldlength = txt_xbxXfs.Text.Length;
                    txt_xbxXfs.Text += e.Node.Text;
                    txt_xbxXfs.Focus();
                    txt_xbxXfs.SelectionStart = oldlength + e.Node.Text.Length;
                }
                else if (report_type == 5)
                {
                    int oldlength = txt_Wy_zdyj.Text.Length;
                    txt_Wy_zdyj.Text += e.Node.Text;
                    txt_Wy_zdyj.Focus();
                    txt_Wy_zdyj.SelectionStart = oldlength + e.Node.Text.Length;
                }

            }
        }
        #endregion

        #region "采集图像相关"
        FilterInfoCollection videoDevices = null;
        //加载摄像头列表
        private void LoadVidoSource()
        {
            try
            {
                if (Program.UsbCameraType == 0)
                {
                    videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                    if (videoDevices.Count == 0)
                        throw new ApplicationException();
                    foreach (FilterInfo device in videoDevices)
                    {
                        devicesCombo.Items.Add(device.Name);
                    }
                }
                else if (Program.UsbCameraType == 1)
                {
                    dvpStatus status;
                    uint i, n = 0;
                    dvpCameraInfo dev_info = new dvpCameraInfo();

                    // 此时，n为成功枚举到的相机数量，将添加到下拉列表中，下拉列表中的内容为每个相机的FriendlyName
                    // 获得当前能连接的相机数量
                    status = DVPCamera.dvpRefresh(ref n);

                    if (status == dvpStatus.DVP_STATUS_OK)
                    {
                        for (i = 0; i < n; i++)
                        {
                            // 逐个枚举出每个相机的信息
                            status = DVPCamera.dvpEnum(i, ref dev_info);

                            if (status == dvpStatus.DVP_STATUS_OK)
                            {
                                // 界面使用的是UNICODE，枚举的设备信息为ANSI字符串，需要将ANSI转UNICODE
                                int item = devicesCombo.Items.Add(dev_info.FriendlyName);
                                if (item == 0)
                                {
                                    devicesCombo.SelectedIndex = item;
                                }
                            }
                        }
                    }
                    if (n == 0)
                    {
                        devicesCombo.Items.Add("不存在视频设备");
                        devicesCombo.Enabled = false;
                    }
                }
            }
            catch (ApplicationException)
            {
                devicesCombo.Items.Add("不存在视频设备");
                devicesCombo.Enabled = false;
            }

            devicesCombo.SelectedIndex = 0;

        }
        private void btn_LX_Click(object sender, EventArgs e)
        {
            try
            {
                if (videoSourcePlayer1.IsRunning)
                {
                    StartSaving();
                }
            }
            catch
            {

            }
        }
        private void btn_closecj_Click(object sender, EventArgs e)
        {
            if (Program.UsbCameraType == 0)
            {
                StopSaving();
                // 关闭之前的视频源
                CloseVideoSource();
            }
            else if (Program.UsbCameraType == 1)
            {
                digitsCamUserCtl2.CloseCamera();
            }
            btn_closecj.Enabled = false;
            btn_PZ.Enabled = false;
            btn_LX.Enabled = false;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            imageListView1.ScrollBars = !imageListView1.ScrollBars;
        }
        private struct ColorComboBoxItem
        {
            public string Name;
            public PropertyInfo Field;

            public override string ToString()
            {
                return Name;
            }

            public ColorComboBoxItem(PropertyInfo field)
            {
                Name = field.Name;
                Field = field;
            }
        }
        private void x96ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.BGthumsWidth, PIS_Sys.Properties.Settings.Default.BGthumsHeight);
        }

        private void x120ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.BGthumsWidth * 2, PIS_Sys.Properties.Settings.Default.BGthumsHeight * 2);
        }

        private void x200ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.BGthumsWidth * 3, PIS_Sys.Properties.Settings.Default.BGthumsHeight * 3);
        }

        private void 重置缩略图大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            qcgl.FrmSetThums ins = new qcgl.FrmSetThums();
            ins.BringToFront();
            ins.Owner = this;
            if (ins.ShowDialog() == DialogResult.OK)
            {
                PIS_Sys.Properties.Settings.Default.BGthumsWidth = ins.ThWidth;
                PIS_Sys.Properties.Settings.Default.BGthumsHeight = ins.ThHeigth;
                PIS_Sys.Properties.Settings.Default.Save();
                imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.BGthumsWidth, PIS_Sys.Properties.Settings.Default.BGthumsHeight);
            }
        }

        private void clearThumbsToolStripButton_Click(object sender, EventArgs e)
        {
            imageListView1.ClearThumbnailCache();
        }

        private void paneToolStripButton_Click(object sender, EventArgs e)
        {
            imageListView1.View = Manina.Windows.Forms.View.Pane;
        }

        private void galleryToolStripButton_Click(object sender, EventArgs e)
        {
            imageListView1.View = Manina.Windows.Forms.View.Gallery;
        }

        private void thumbnailsToolStripButton_Click(object sender, EventArgs e)
        {
            imageListView1.View = Manina.Windows.Forms.View.Thumbnails;
        }

        private void colorToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyInfo field = ((ColorComboBoxItem)colorToolStripComboBox.SelectedItem).Field;
            ImageListViewColor color = (ImageListViewColor)field.GetValue(null, null);
            imageListView1.Colors = color;
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
            if (!this.txt_pz_blh.Text.Trim().Equals(""))
            {
                listViewEx1.Items.Clear();
                //视频
                string OutputFolder = Path.Combine(Program.APPdirPath, @"Pis_Cap_Video\" + txt_pz_blh.Text.Trim());
                if (Directory.Exists(OutputFolder))
                {
                    foreach (var file in Directory.GetFiles(OutputFolder, "*.avi"))
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = "视频";
                        int index = file.LastIndexOf(@"\");
                        string filename = file.Substring(index + 1);
                        lvi.SubItems.Add(filename);
                        string filepath = file.Substring(0, index);
                        lvi.SubItems.Add(filepath);
                        this.listViewEx1.Items.Add(lvi);

                    }
                }
                //图像
                imageListView1.Items.Clear();
                string BgOutputFolder = Path.Combine(ImgFolder, txt_pz_blh.Text.Trim());
                imageListView1.SuspendLayout();
                //报告
                if (Directory.Exists(BgOutputFolder))
                {
                    string[] files = ReportImageStr.Split('|');
                    foreach (var file in Directory.GetFiles(BgOutputFolder, "*.jpg"))
                    {
                        ImageListViewItem ins = new ImageListViewItem();
                        string filename = file.Replace(Program.APPdirPath, "");
                        ins.Tag = filename;
                        if (toolStripLabel3.Text.Equals(""))
                        {
                            Image img = GetImage(file);
                            toolStripLabel3.Text = string.Format("({0}:{1})", img.Width.ToString(), img.Height.ToString());
                            Application.DoEvents();
                            if (thumsFlag == true)
                            {
                                thumsFlag = false;
                                PIS_Sys.Properties.Settings.Default.BGthumsWidth = img.Width / 10;
                                PIS_Sys.Properties.Settings.Default.BGthumsHeight = img.Height / 10;
                                PIS_Sys.Properties.Settings.Default.Save();
                                imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.BGthumsWidth, PIS_Sys.Properties.Settings.Default.BGthumsHeight);
                            }
                        }
                        if (PIS_Sys.Properties.Settings.Default.EditPicName)
                        {
                            string filename1 = file.Substring(file.LastIndexOf(@"\") + 1);
                            DBHelper.BLL.multi_media_info insInfo = new DBHelper.BLL.multi_media_info();
                            string result = insInfo.GetMemo_note(this.txt_pz_blh.Text.Trim(), filename1, 4);
                            if (!result.Equals(""))
                            {
                                ins.Text = result;
                            }
                            else
                            {
                                ins.Text = "BG_" + imageListView1.Items.Count.ToString();
                            }
                        }
                        else
                        {
                            ins.Text = "BG_" + imageListView1.Items.Count.ToString();
                        }

                        ins.FileName = file;
                        foreach (string rfile in files)
                        {
                            if (filename.Equals(rfile))
                            {
                                ins.Checked = true;
                                break;
                            }
                        }
                        imageListView1.Items.Add(ins);
                    }
                }
                //大体取材图片
                if (PIS_Sys.Properties.Settings.Default.Load_DT_IMG)
                {
                    string DtOutputFolder = Path.Combine(DTImgFolder, txt_pz_blh.Text.Trim());
                    if (Directory.Exists(DtOutputFolder))
                    {
                        string[] files = ReportImageStr.Split('|');
                        foreach (var file in Directory.GetFiles(DtOutputFolder, "*.jpg"))
                        {
                            ImageListViewItem ins = new ImageListViewItem();
                            string filename = file.Replace(Program.APPdirPath, "");
                            ins.Tag = filename;
                            if (PIS_Sys.Properties.Settings.Default.EditPicName)
                            {
                                string filename1 = file.Substring(file.LastIndexOf(@"\") + 1);
                                DBHelper.BLL.multi_media_info insInfo = new DBHelper.BLL.multi_media_info();
                                string result = insInfo.GetMemo_note(this.txt_pz_blh.Text.Trim(), filename1, 1);
                                if (!result.Equals(""))
                                {
                                    ins.Text = result;
                                }
                                else
                                {
                                    ins.Text = "QC_" + imageListView1.Items.Count.ToString();
                                }
                            }
                            else
                            {
                                ins.Text = "QC_" + imageListView1.Items.Count.ToString();
                            }
                            ins.FileName = file;
                            foreach (string rfile in files)
                            {
                                if (filename.Equals(rfile))
                                {
                                    ins.Checked = true;
                                    break;
                                }
                            }
                            imageListView1.Items.Add(ins);
                        }
                    }
                }

                imageListView1.ResumeLayout();
            }
        }

        private void imageListView1_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            //if (System.IO.File.Exists(e.Item.FileName))
            //{
            //    AnnotateImage(e.Item.FileName, e.Item.FilePath);
            //}
            if (PIS_Sys.Properties.Settings.Default.EditPicName)
            {
                string filePath = e.Item.FileName;
                string filename = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                Frm_Memo insFrm = new Frm_Memo();
                if (insFrm.ShowDialog() == DialogResult.OK)
                {
                    DBHelper.BLL.multi_media_info ins = new DBHelper.BLL.multi_media_info();
                    if (filename.IndexOf("DT") != -1)
                    {
                        if (ins.UpdateMemo_note(this.txt_pz_blh.Text.Trim(), filename, 1, insFrm.Memo_note) == 1)
                        {
                            e.Item.Text = insFrm.Memo_note;
                        }
                    }
                    else if (filename.IndexOf("BG") != -1)
                    {

                        if (ins.UpdateMemo_note(this.txt_pz_blh.Text.Trim(), filename, 4, insFrm.Memo_note) == 1)
                        {
                            e.Item.Text = insFrm.Memo_note;
                        }
                    }
                }
            }
        }

        public static Image LoadImage(string filePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath) && IsImageFile(filePath) && File.Exists(filePath))
                {
                    return Image.FromStream(new MemoryStream(File.ReadAllBytes(filePath)));
                }
            }
            catch
            {

            }

            return null;
        }
        public static bool IsImageFile(string filePath)
        {
            return IsValidFile(filePath, typeof(ImageFileExtensions));
        }
        private static bool IsValidFile(string filePath, Type enumType)
        {
            string ext = GetFilenameExtension(filePath);

            if (!string.IsNullOrEmpty(ext))
            {
                return Enum.GetNames(enumType).Any(x => ext.Equals(x, StringComparison.InvariantCultureIgnoreCase));
            }

            return false;
        }
        public static string GetFilenameExtension(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                int pos = filePath.LastIndexOf('.');

                if (pos >= 0)
                {
                    return filePath.Substring(pos + 1).ToLowerInvariant();
                }
            }

            return null;
        }

        public static void AnnotateImage(string imgPath, string pathImage)
        {
            if (!IniConfig.isInitialized)
            {
                IniConfig.AllowSave = true;
                IniConfig.Init(@pathImage);
            }

            using (Image cloneImage = LoadImage(imgPath))
            using (ICapture capture = new Capture { Image = cloneImage })
            using (Surface surface = new Surface(capture))
            using (ImageEditorForm editor = new ImageEditorForm(surface, true))
            {
                editor.IsTaskWork = false;
                editor.SetImagePath(imgPath);
                DialogResult result = editor.ShowDialog();

                if (result == DialogResult.OK && editor.IsTaskWork)
                {

                }


            }
        }
        private void btn_playVideo_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listViewEx1.SelectedIndices.Count > 0)
                {
                    if (listViewEx1.SelectedIndices != null && listViewEx1.SelectedIndices.Count > 0)
                    {
                        ListView.SelectedIndexCollection c = listViewEx1.SelectedIndices;
                        string filename = listViewEx1.Items[c[0]].SubItems[1].Text;// 表示选中行的第二列
                        string filepath = listViewEx1.Items[c[0]].SubItems[2].Text;// 表示选中行的第二列
                        string filePathstr = Path.Combine(filepath, filename);
                        if (System.IO.File.Exists(filePathstr))
                        {
                            Process.Start(filePathstr);
                        }
                        else
                        {
                            Frm_TJInfo("提示", "文件不存在！");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Frm_TJInfo("播放错误", ex.ToString());
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
        //双击拍照
        private void videoSourcePlayer1_DoubleClick(object sender, EventArgs e)
        {
            btn_PZ_Click(null, null);
        }
        Boolean boolCaptureing = false;
        private void btn_PZ_Click(object sender, EventArgs e)
        {
            if (boolCaptureing)
            {
                return;
            }
            try
            {
                boolCaptureing = true;
                if (Program.UsbCameraType == 0)
                {
                    if (videoSourcePlayer1.IsRunning)
                    {

                        //1.拍照
                        Bitmap bmps1 = videoSourcePlayer1.GetCurrentVideoFrame();
                        //2.压缩上传并刷新显示
                        CapImage(bmps1);
                        //3.资源是否,否则内存泄漏
                        if (bmps1 != null)
                        {
                            bmps1.Dispose();
                        }
                    }
                    else
                    {
                        Frm_TJInfo("当前不能执行此操作", "请先打开摄像头！");
                    }
                }
                else if (Program.UsbCameraType == 1)
                {
                    if (!txt_pz_blh.Text.Trim().Equals(""))
                    {
                        string capText = digitsCamUserCtl2.ExeCaptureSingleImg(txt_pz_blh.Text.Trim());
                        char[] separator = { '|' };
                        string[] strs = capText.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        if (strs.Length == 2)
                        {
                            string study_no = strs[0];
                            string file_path = strs[1];
                            if (System.IO.File.Exists(file_path))
                            {
                                //1.拍照
                                Image img = GetImage(file_path);
                                Bitmap bmps1 = new Bitmap(img);
                                img.Dispose();
                                File.Delete(file_path);
                                //2.压缩上传并刷新显示
                                CapImage(bmps1);
                                //3.资源是否,否则内存泄漏
                                if (bmps1 != null)
                                {
                                    bmps1.Dispose();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.FileLogIns.Error(ex.ToString());
            }
            finally
            {
                boolCaptureing = false;
            }
        }
        private void CapImage(Bitmap bmps1)
        {
            if (bmps1 != null)
            {
                if (thumsFlag == true)
                {
                    thumsFlag = false;
                    PIS_Sys.Properties.Settings.Default.BGthumsWidth = bmps1.Width / 10;
                    PIS_Sys.Properties.Settings.Default.BGthumsHeight = bmps1.Height / 10;
                    PIS_Sys.Properties.Settings.Default.Save();
                    imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.BGthumsWidth, PIS_Sys.Properties.Settings.Default.BGthumsHeight);
                }
                string filename = string.Format("BG_{0}", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                string PathImg = ImgFolder + @"\" + txt_pz_blh.Text.Trim();
                if (Directory.Exists(PathImg) == false)
                {
                    Directory.CreateDirectory(PathImg);
                }
                //只有当有病理号时，才可以进行上传及保存和展示
                if (!txt_pz_blh.Text.Trim().Equals(""))
                {
                    string strFileName = string.Format(@"{0}\{1}.jpg", PathImg, filename);
                    string remoteFile = string.Format(@"{0}/{1}/{2}/{3}/{4}.jpg", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, txt_pz_blh.Text.Trim(), filename);
                    bmps1.Save(strFileName);
                    //上传
                    if (FtpUpload(@remoteFile, strFileName))
                    {
                        DBHelper.BLL.multi_media_info insMulti = new DBHelper.BLL.multi_media_info();
                        if (insMulti.InsertData(txt_pz_blh.Text.Trim(), remoteFile, string.Format("{0}.jpg", filename), 4) == 1)
                        {
                            //3.展示
                            ImageListViewItem ins = new ImageListViewItem();
                            ins.Tag = strFileName.Replace(Program.APPdirPath, "");
                            ins.Text = string.Format("BG_{0}", imageListView1.Items.Count.ToString());
                            if (toolStripLabel3.Text.Equals(""))
                            {
                                toolStripLabel3.Text = string.Format("({0}:{1})", bmps1.Width.ToString(), bmps1.Height.ToString());
                                Application.DoEvents();
                            }
                            ins.FileName = strFileName;
                            imageListView1.Items.Insert(0, ins);
                        }
                    }
                }
                else
                {
                    Frm_TJInfo("当前不能执行此操作", "请先选择一个病人！");
                }

            }
            if (bmps1 != null)
            {
                bmps1.Dispose();
            }
            //更新报告图像
            string strImage = GetBgImage();
            DBHelper.BLL.exam_report insImg = new DBHelper.BLL.exam_report();
            insImg.UpdateReport_Image(txt_pz_blh.Text.Trim(), strImage);
        }
        private void digitsCamUserCtl2_MshotCaptureImageEvent(object sender, MshotDigitsCamControl.CapImageEventArgs e)
        {
            if (boolCaptureing)
            {
                return;
            }
            try
            {
                boolCaptureing = true;
                if (!txt_pz_blh.Text.Trim().Equals(""))
                {
                    string capText = digitsCamUserCtl2.ExeCaptureSingleImg(txt_pz_blh.Text.Trim());
                    char[] separator = { '|' };
                    string[] strs = capText.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    if (strs.Length == 2)
                    {
                        string study_no = strs[0];
                        string file_path = strs[1];
                        if (System.IO.File.Exists(file_path))
                        {
                            //1.拍照
                            Image img = GetImage(file_path);
                            Bitmap bmps1 = new Bitmap(img);
                            img.Dispose();
                            File.Delete(file_path);
                            //2.压缩上传并刷新显示
                            CapImage(bmps1);
                            //3.资源是否,否则内存泄漏
                            if (bmps1 != null)
                            {
                                bmps1.Dispose();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.FileLogIns.Error(ex.ToString());
            }
            finally
            {
                boolCaptureing = false;
            }
        }
        // 定义调用方法的委托
        delegate void FunDelegateStart();
        // 真正需要执行的方法
        private void FunStart()
        {

            btn_lxstop.Enabled = true;
            btn_LX.Enabled = false;
        }
        void OnRecordingStart()
        {
            if (InvokeRequired)
            {
                // 要调用的方法的委托
                FunDelegateStart funDelegate = new FunDelegateStart(FunStart);
                this.BeginInvoke(funDelegate);
            }
        }
        Stopwatch StopwatchTimer = new Stopwatch();//录像计算时间
        // 定义调用方法的委托
        delegate void FunRecordStop(string VideoPath);
        // 真正需要执行的方法
        private void FunStop(string videopath)
        {
            StopwatchTimer.Stop();
            btn_lxstop.Enabled = false;
            btn_LX.Enabled = true;
            VideoTimeProgress.Text = "";
            //加载视频列表
            ListViewItem lvi = new ListViewItem();
            lvi.Text = "视频";
            int index = videopath.LastIndexOf(@"\");
            string filename = videopath.Substring(index + 1);
            lvi.SubItems.Add(filename);
            string filepath = videopath.Substring(0, index);
            lvi.SubItems.Add(filepath);
            this.listViewEx1.Items.Add(lvi);

        }
        void OnRecordStop(string videopath)
        {
            if (InvokeRequired)
            {
                // 要调用的方法的委托
                FunRecordStop funDelegate = new FunRecordStop(FunStop);
                this.BeginInvoke(funDelegate, new object[] { videopath });
            }
        }

        //结束录像
        private void btn_lxstop_Click(object sender, EventArgs e)
        {
            try
            {
                if (videoSourcePlayer1.IsRunning)
                {
                    StopSaving();
                }
            }
            catch
            {

            }
        }

        private string device;
        // opened video source
        private IVideoSource videoSource = null;
        // motion detector
        MotionDetector detector = new MotionDetector(new TwoFramesDifferenceDetector(), null);
        public VideoCodec Codec = VideoCodec.MPEG4;
        public ConcurrentQueue<Helper.FrameAction> Buffer = new ConcurrentQueue<Helper.FrameAction>();
        private bool _firstFrame = true;
        private Thread _recordingThread;
        private readonly object _lockobject = new object();
        private DateTime _recordingStartTime;
        private readonly ManualResetEvent _stopWrite = new ManualResetEvent(false);
        public string VideoFileName = "";
        private VideoFileWriter _writer;
        private int _videoWidth, _videoHeight;
        //是否正在录像
        public bool Recording
        {
            get
            {
                try
                {
                    return _recordingThread != null && !_recordingThread.Join(TimeSpan.Zero);
                }
                catch
                {

                }
                return false;

            }
        }

        //开始录像
        public void StartSaving()
        {

            if (Recording)
                return;

            lock (_lockobject)
            {
                if (Recording)
                    return;

                if (!Recording)
                {
                    while (Buffer.Count > 0)
                    {
                        Helper.FrameAction fa;
                        if (Buffer.TryPeek(out fa))
                        {
                            if (Buffer.TryDequeue(out fa))
                                fa.Nullify();
                        }
                    }
                }
                _recordingStartTime = DateTime.UtcNow;
                _recordingThread = new Thread(Record)
                {
                    Name = "RecordingThread0",
                    IsBackground = false,
                    Priority = ThreadPriority.Normal
                };
                _recordingThread.Start();
            }
        }

        private unsafe void WriteFrame(Helper.FrameAction fa, DateTime recordingStart, ref long lastvideopts, ref double maxAlarm,
          ref Helper.FrameAction? peakFrame, ref long lastaudiopts)
        {
            switch (fa.FrameType)
            {
                case Enums.FrameType.Video:
                    using (var ms = new MemoryStream(fa.Content))
                    {
                        using (var bmp = (Bitmap)Image.FromStream(ms))
                        {
                            var pts = (long)(fa.TimeStamp - recordingStart).TotalMilliseconds;
                            if (pts >= lastvideopts)
                            {
                                _writer.WriteVideoFrame(ResizeBitmap(bmp));
                                lastvideopts = pts;
                            }
                        }
                        if (fa.Level > maxAlarm || peakFrame == null)
                        {
                            maxAlarm = fa.Level;
                            peakFrame = fa;
                        }
                        ms.Close();
                    }
                    break;
            }
            fa.Nullify();
        }

        private Bitmap ResizeBitmap(Bitmap frame)
        {

            if (frame.Width == _videoWidth && frame.Height == _videoHeight)
                return frame;

            var b = new Bitmap(_videoWidth, _videoHeight);
            var r = new Rectangle(0, 0, _videoWidth, _videoHeight);
            using (var g = Graphics.FromImage(b))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighSpeed;
                g.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                g.SmoothingMode = SmoothingMode.None;
                g.InterpolationMode = InterpolationMode.Default;
                g.DrawImage(frame, r);
            }

            frame.Dispose();
            frame = null;
            return b;
        }
        private static int CalcBitRate(int q)
        {
            return 8000 * (Convert.ToInt32(Math.Pow(2, (q - 1))));
        }


        [HandleProcessCorruptedStateExceptions]
        private void Record()
        {
            _stopWrite.Reset();

            VideoFileName = "Video_" + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string PathVideo = VideoFolder + @"\" + txt_pz_blh.Text.Trim();
            if (Directory.Exists(PathVideo) == false)
            {
                Directory.CreateDirectory(PathVideo);
            }
            string videopath = PathVideo + @"\" + VideoFileName + ".avi";
            string thumbspath = PathVideo + @"\" + VideoFileName + ".jpg";

            bool error = false;
            double maxAlarm = 0;
            long lastvideopts = -1, lastaudiopts = -1;
            DateTime recordingStart = Helper.Now;
            try
            {
                try
                {
                    Program.FFMPEGMutex.WaitOne();
                    _writer = new VideoFileWriter();
                    _writer.Open(videopath, _videoWidth, _videoHeight, 20, VideoCodec.MPEG4);
                    OnRecordingStart();
                }
                finally
                {
                    try
                    {
                        Program.FFMPEGMutex.ReleaseMutex();

                    }
                    catch (ObjectDisposedException)
                    {
                        //can happen on shutdown
                    }
                }


                Helper.FrameAction? peakFrame = null;
                bool first = true;

                while (!_stopWrite.WaitOne(5))
                {
                    Helper.FrameAction fa;
                    if (Buffer.TryDequeue(out fa))
                    {
                        if (first)
                        {
                            StopwatchTimer.Reset();
                            StopwatchTimer.Start();
                            recordingStart = fa.TimeStamp;
                            first = false;
                        }

                        WriteFrame(fa, recordingStart, ref lastvideopts, ref maxAlarm, ref peakFrame,
                            ref lastaudiopts);
                        //计算时间显示
                        OnRecordingOneFrame();
                    }

                }
                //录像结束！加载视频列表
                OnRecordStop(videopath);


            }
            catch
            {
                error = true;

            }
            finally
            {
                if (_writer != null && _writer.IsOpen)
                {
                    try
                    {
                        Program.FFMPEGMutex.WaitOne();
                        _writer.Dispose();
                    }
                    catch
                    {

                    }
                    finally
                    {
                        try
                        {
                            Program.FFMPEGMutex.ReleaseMutex();
                        }
                        catch (ObjectDisposedException)
                        {
                            //can happen on shutdown
                        }
                    }

                    _writer = null;
                }

            }
            if (_firstFrame)
            {
                error = true;
            }
            if (error)
            {
                try
                {
                    if (File.Exists(videopath))
                        FileOperations.Delete(videopath);
                }
                catch
                {
                }

                ClearBuffer();


            }
        }
        // 定义调用方法的委托
        delegate void FunDelegate();
        // 真正需要执行的方法
        private void Fun()
        {
            VideoTimeProgress.Text = string.Format("已录像:{0}秒", (StopwatchTimer.ElapsedMilliseconds / 1000).ToString());
        }
        void OnRecordingOneFrame()
        {
            if (InvokeRequired)
            {
                // 要调用的方法的委托
                FunDelegate funDelegate = new FunDelegate(Fun);
                // this.BeginInvoke(被调用的方法的委托，要传递的参数[Object数组])
                this.BeginInvoke(funDelegate);
            }

        }


        private void ClearBuffer()
        {
            lock (_lockobject)
            {
                Helper.FrameAction fa;
                while (Buffer.TryDequeue(out fa))
                {
                    fa.Nullify();
                }
            }

        }
        private void StopSaving()
        {
            if (Recording)
            {
                _stopWrite.Set();
                try
                {
                    if (!_recordingThread.Join(TimeSpan.Zero))
                        _recordingThread.Join();
                }
                catch { }
                _firstFrame = true;
                btn_LX.Text = "开始录像";
            }
        }


        private static bool ThumbnailCallback()
        {
            return false;
        }
        public static ImageCodecInfo GetFEncoder
        {
            get
            {
                ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

                foreach (ImageCodecInfo codec in codecs)
                {
                    if (codec.FormatID == ImageFormat.Jpeg.Guid)
                    {
                        return codec;
                    }
                }
                return null;
            }
        }

        public static EncoderParameters GetEncoderParameters()
        {
            EncoderParameters EncoderParams;
            EncoderParams = new EncoderParameters(1);
            EncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 50L);
            return EncoderParams;
        }
        private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
            if (Recording)
            {
                if (_firstFrame)
                {
                    _videoWidth = videoSourcePlayer1.Width;
                    _videoHeight = videoSourcePlayer1.Height;
                    if ((_videoWidth & 1) != 0)
                    {
                        _videoWidth = _videoWidth - 1;
                    }
                    if ((_videoHeight & 1) != 0)
                    {
                        _videoHeight = _videoHeight - 1;
                    }

                    _firstFrame = false;
                }

                lock (this)
                {
                    float motionLevel = 0;

                    Buffer.Enqueue(new Helper.FrameAction(image, motionLevel, Helper.Now));
                }
            }
        }
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (devicesCombo.Enabled == false)
            {
                return;
            }
            try
            {
                if (Program.UsbCameraType == 0)
                {
                    device = videoDevices[devicesCombo.SelectedIndex].MonikerString;
                    //创建视频源
                    VideoCaptureDevice videoSource = new VideoCaptureDevice(device);
                    OpenVideoSource(videoSource);
                }
                else if (Program.UsbCameraType == 1)
                {
                    digitsCamUserCtl2.ConnCamera(devicesCombo.Text);//新
                }
                btn_closecj.Enabled = true;
                btn_PZ.Enabled = true;
                btn_LX.Enabled = true;
            }
            catch { }
        }
        private void OpenVideoSource(IVideoSource source)
        {
            // 设置忙
            this.Cursor = Cursors.WaitCursor;

            // 关闭之前的视频源
            CloseVideoSource();
            //保持视频缩放比例
            if (Program.KeepAspectRatioFlag == 1)
            {
                videoSourcePlayer1.KeepAspectRatio = true;
            }
            // 开始新的视频源
            videoSourcePlayer1.VideoSource = new AsyncVideoSource(source);
            videoSourcePlayer1.Start();
            videoSource = source;
            this.Cursor = Cursors.Default;
        }
        // 关闭先前的视频源
        private void CloseVideoSource()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                // 停止当前视频源
                videoSourcePlayer1.SignalToStop();
                // 等待视频源关闭
                for (int i = 0; (i < 50) && (videoSourcePlayer1.IsRunning); i++)
                {
                    Thread.Sleep(100);
                }
                if (videoSourcePlayer1.IsRunning)
                    videoSourcePlayer1.Stop();
                this.Cursor = Cursors.Default;
            }
            catch
            {

            }
        }
        private void buttonItem24_Click(object sender, EventArgs e)
        {

            try
            {
                if (Program.UsbCameraType == 0)
                {
                    if ((videoSource != null) && (videoSource is VideoCaptureDevice))
                    {
                        ((VideoCaptureDevice)videoSource).DisplayPropertyPage(this.Handle);
                    }
                }
                else if (Program.UsbCameraType == 1)
                {
                    digitsCamUserCtl2.OpenPropertyForm();
                }
            }
            catch (NotSupportedException ex)
            {
                Frm_TJInfo("错误", ex.Message);
            }
        }
        private void buttonItem25_Click(object sender, EventArgs e)
        {
            if ((videoSource != null) && (videoSource is VideoCaptureDevice) && (videoSource.IsRunning))
            {
                try
                {
                    ((VideoCaptureDevice)videoSource).DisplayCrossbarPropertyPage(this.Handle);
                }
                catch (NotSupportedException ex)
                {
                    Frm_TJInfo("错误", ex.Message);
                }
            }
        }
        #endregion

        #region  "左侧列表"

        private void Chk_BtnRq(object sender, EventArgs e)
        {
            ButtonItem BtnItem = (ButtonItem)sender;
            for (int i = 0; i < this.QueryBtn.SubItems.Count; i++)
            {
                ((ButtonItem)this.QueryBtn.SubItems[i]).Checked = false;
            }
            BtnItem.Checked = true;
            QueryBtn_Click(null, null);
        }
        //检查类别事件处理
        private void Chk_BtnJclb(object sender, EventArgs e)
        {
            ButtonItem BtnItem = (ButtonItem)sender;
            for (int i = 0; i < this.BtnJclb.SubItems.Count; i++)
            {
                ((ButtonItem)this.BtnJclb.SubItems[i]).Checked = false;
            }
            BtnItem.Checked = true;
            QueryBtn_Click(null, null);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            superTabControl2.SelectedTabIndex = 0;
            //刷新列表
            QueryBtn_Click(null, null);
            WindowState = FormWindowState.Maximized;
        }
        //刷新列表
        private void QueryBtn_Click(object sender, EventArgs e)
        {
            DataSet _DataSet = null;
            string dt_tj = "两周";
            for (int i = 0; i < this.QueryBtn.SubItems.Count; i++)
            {
                ButtonItem BtnItem = (ButtonItem)this.QueryBtn.SubItems[i];
                if (BtnItem.Checked == true)
                {
                    dt_tj = BtnItem.Text;
                    break;
                }
            }
            string lb_tj = "all";
            for (int i = 0; i < this.BtnJclb.SubItems.Count; i++)
            {
                ButtonItem BtnItem = (ButtonItem)this.BtnJclb.SubItems[i];
                if (BtnItem.Checked == true)
                {
                    lb_tj = BtnItem.Tag.ToString();
                    break;
                }
            }
            string zt_tj = "";
            if (chK_status.Checked)
            {
                zt_tj = ((DevComponents.Editors.ComboItem)(cmb_ExamStatus.SelectedItem)).Value.ToString();
            }
            //初始统计计数
            Init_Js();
            //执行数据刷新
            if (_DataSet != null)
            {
                _DataSet.Clear();
            }
            _DataSet = new DataSet();
            DBHelper.BLL.exam_master exam_mas_Ins = new DBHelper.BLL.exam_master();
            _DataSet = exam_mas_Ins.GetDsBGExam_master(dt_tj, zt_tj, Program.workstation_type_db, Program.User_Code, lb_tj, 0, PIS_Sys.Properties.Settings.Default.QcBg_flag);
            if (_DataSet != null && _DataSet.Tables[0].Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.ClearSelectedCells();
                superGridControl1.PrimaryGrid.ClearSelectedRows();
                superGridControl1.PrimaryGrid.DataSource = _DataSet;
                //计数
                tj_func(_DataSet.Tables[0]);
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
                btn_up.Enabled = false;
                btn_down.Enabled = false;
                ClearData();
            }
        }
        //清空界面数据
        private void ClearData()
        {
            if (report_type == 0 || report_type == 3)
            {
                labelX1.Text = "光镜所见：";
            }
            else
            {
                labelX1.Text = "治疗建议：";
            }
            InitReportIns();
            labelX10.Text = "";
            ReportImageStr = "";
            txt_sqd.Text = "";
            txt_pz_blh.Text = "";
            txt_pz_xm.Text = "";
            txt_bl_shys.Text = "";
            txt_bl_zdpz.Text = "";
            txt_lcfh.Text = "";
            cmb_bl_sfyx.Text = "否";
            report_bl_datetime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            txt_rysj.Text = "";
            txt_zdyj.Text = "";
            txt_xbxXfs.Text = "";
            textBoxX8.Text = "";
            txt_ICD10.Text = "";
            txt_Wy_zdyj.Text = "";
            txt_sjdw.Text = "";
            txt_wyblh.Text = "";
            integerInput1.Value = 1;
            integerInput2.Value = 1;
            cmbBbqc.SelectedIndex = 0;
            cmbBgnr.SelectedIndex = 0;
            cmb_gpys.SelectedIndex = 0;
        }
        //计数
        public void tj_func(DataTable dt)
        {
            int rows = dt.Rows.Count;
            for (int i = 0; i < rows; i++)
            {
                //是否冰冻
                string ice_flag = dt.Rows[i]["ice_flag"].ToString();
                //状态值
                string status = dt.Rows[i]["exam_status"].ToString();
                //会诊标记
                string huizhen_flag = dt.Rows[i]["huizhen_flag"].ToString();
                //随访标记
                string suifang_flag = dt.Rows[i]["suifang_flag"].ToString();
                //随访完成标记
                string suifang_wc_flag = dt.Rows[i]["suifang_wc_flag"].ToString();

                if (ice_flag.Equals("1"))
                {
                    bd_int += 1;
                }
                switch (status)
                {
                    case "30":
                        zp_int += 1;
                        break;
                    case "36":
                        ys_int += 1;
                        break;
                    case "40":
                        zd_int += 1;
                        break;
                    case "50":
                        sh_int += 1;
                        break;
                    case "55":
                        dy_int += 1;
                        break;
                    default:
                        break;
                }
                //最后一行开始赋值
                if (i == rows - 1)
                {
                    if (superGridControl1.PrimaryGrid.Footer == null)
                    {
                        superGridControl1.PrimaryGrid.Footer = new GridFooter();
                    }
                    superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='blue'><b>{0}</b></font> 条记录:片<font color='blue'><b>{1}</b></font>,延<font color='blue'><b>{2}</b></font>,诊<font color='blue'><b>{3}</b></font>,审<font color='blue'><b>{4}</b></font>,印<font color='blue'><b>{5}</b></font>", rows, zp_int, ys_int, zd_int, sh_int, dy_int);
                    lbl_bdBL.Text = string.Format(bdbL_str, bd_int);
                }
            }


        }
        //计数
        string bdbL_str = "冰冻病例:<font color='blue'><b>{0}</b></font> 例";
        //冰冻特别标识（重要类别计数）
        int bd_int = 0;
        int zp_int = 0;
        int ys_int = 0;
        int zd_int = 0;
        int sh_int = 0;
        int dy_int = 0;
        private void Init_Js()
        {
            bd_int = 0;
            zp_int = 0;
            ys_int = 0;
            zd_int = 0;
            sh_int = 0;
            dy_int = 0;

            if (superGridControl1.PrimaryGrid.Footer == null)
            {
                superGridControl1.PrimaryGrid.Footer = new GridFooter();
            }
            superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Red'><b>{0}</b></font> 条记录", 0);
            lbl_bdBL.Text = string.Format(bdbL_str, bd_int);
        }

        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            GridPanel panel = e.GridPanel;
            panel.DefaultVisualStyles.CaptionStyles.Default.Alignment = Alignment.MiddleCenter;
            panel.DefaultVisualStyles.CellStyles.Default.Alignment = Alignment.MiddleCenter;
            if (panel.Footer == null)
            {
                panel.Footer = new GridFooter();
            }
            //选中先前的那条
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                SelectedOldStudyNO(txt_pz_blh.Text.Trim());
            }
        }
        //选中指定病理号的记录
        private void SelectedOldStudyNO(string study_no)
        {
            if (superGridControl1.PrimaryGrid.DataSource == null)
            {
                return;
            }
            for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count; i++)
            {
                string cur_studyno = ((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["study_no"].Value.ToString();
                if (cur_studyno.Equals(study_no))
                {
                    //先清空以前选中的所有行
                    this.superGridControl1.PrimaryGrid.ClearSelectedCells();
                    this.superGridControl1.PrimaryGrid.ClearSelectedRows();
                    //最前面的黑色三角号跟着移动
                    this.superGridControl1.PrimaryGrid.SetActiveRow((GridContainer)superGridControl1.PrimaryGrid.Rows[i]);
                    //选中当前行
                    superGridControl1.PrimaryGrid.SetSelectedRows(i, 1, true);
                    superGridControl1.PrimaryGrid.SetActiveRow((GridRow)superGridControl1.PrimaryGrid.Rows[i]);
                    superGridControl1.PrimaryGrid.Rows[i].EnsureVisible();
                    break;
                }
            }
        }

        private void superGridControl1_GetCellStyle(object sender, GridGetCellStyleEventArgs e)
        {
            if (e.GridCell.GridColumn.Name.Equals("status_name") == true)
            {
                string strStatus = (string)e.GridCell.Value;
                if (exam_status_name_dic.ContainsKey(strStatus))
                {
                    e.Style.TextColor = exam_status_name_dic[strStatus];
                }
            }
        }
        private void superGridControl1_GetRowHeaderStyle(object sender, GridGetRowHeaderStyleEventArgs e)
        {
            try
            {
                if (this.superGridControl1.PrimaryGrid.Rows.Count > 0)
                {
                    GridRow Row = (GridRow)e.GridRow;
                    string str_info = "";
                    string ice_flag = ((GridRow)e.GridRow).Cells["ice_flag"].Value.ToString();
                    if (ice_flag.Equals("1"))
                    {
                        str_info = "冰";
                        e.Style.TextColor = Color.Red;
                    }

                    string ks_flag = ((GridRow)e.GridRow).Cells["ks_flag"].Value.ToString();
                    if (ks_flag.Equals("1"))
                    {
                        str_info = "快";
                        e.Style.TextColor = Color.Red;
                    }
                    e.GridRow.RowHeaderText = str_info;

                }
            }
            catch
            {

            }
        }
        private int report_type = 0;
        //设置报告书写可见性
        public void SetTabVisible(string modality)
        {
            comboBoxItem1.Enabled = true;
            report_type = exam_type_dict[modality];
            for (int i = 0; i < comboBoxItem1.Items.Count; i++)
            {
                int tab_index = Convert.ToInt32(((DevComponents.Editors.ComboItem)(comboBoxItem1.Items[i])).Value);
                if (tab_index == report_type)
                {
                    comboBoxItem1.SelectedIndex = i;
                    break;
                }
            }
        }
        public void SetTabVisible(int type_value)
        {
            report_type = type_value;
            for (int i = 0; i < comboBoxItem1.Items.Count; i++)
            {
                int tab_index = Convert.ToInt32(((DevComponents.Editors.ComboItem)(comboBoxItem1.Items[i])).Value);
                if (tab_index == report_type)
                {
                    comboBoxItem1.SelectedIndex = i;
                    comboBoxItem1.Enabled = false;
                    break;
                }
            }
        }
        string lcjc_str = "历次检查:<font color='blue'><b>{0}</b></font>例";
        //报告图像串
        string ReportImageStr = "";
        private void ShowJcInfo(string exam_no, string patient_id, string study_no, string patient_name, string sex, string age, string patient_source)
        {
            try
            {

                //取信息并赋值给界面
                ClearData();
                //病人基本信息
                txt_pz_blh.Text = study_no;
                txt_sqd.Text = exam_no;
                txt_pz_xm.Text = patient_name;
                labelX10.Text = string.Format("病理号:{0}  病人姓名:{1}  性别:{2}  年龄:{3}  病人来源:{4}", study_no, patient_name, sex, age, patient_source);
                //展示当前病理号下的所有制片信息
                DBHelper.BLL.exam_filmmaking insZp = new DBHelper.BLL.exam_filmmaking();
                DataTable dtzp = insZp.GetDtFilmMakingPj(study_no);
                if (dtzp != null && dtzp.Rows.Count > 0)
                {
                    btn_ZP.TextColor = Color.Blue;
                }
                else
                {
                    btn_ZP.TextColor = CurBtnColor;
                }
                //展示当前病理号下的所有取材信息
                DBHelper.BLL.exam_draw_meterials insQc = new DBHelper.BLL.exam_draw_meterials();
                DataTable dtqc = insQc.GetDtHdMeterials(study_no);
                if (dtqc != null && dtqc.Rows.Count > 0)
                {
                    btn_QC.TextColor = Color.Blue;
                }
                else
                {
                    btn_QC.TextColor = CurBtnColor;
                }
                //展示特检医嘱信息
                DBHelper.BLL.exam_tjyz instjyz = new DBHelper.BLL.exam_tjyz();
                DataTable dttjyz = instjyz.GetDataTjyz(study_no);
                if (dttjyz != null && dttjyz.Rows.Count > 0)
                {
                    btn_tjyz.TextColor = Color.Blue;
                }
                else
                {
                    btn_tjyz.TextColor = CurBtnColor;
                }
                //展示技术医嘱信息
                DBHelper.BLL.exam_jsyz insjsyz = new DBHelper.BLL.exam_jsyz();
                DataTable dtjsyz = insjsyz.GetData(study_no);
                if (dtjsyz != null && dtjsyz.Rows.Count > 0)
                {
                    btn_jsyz.TextColor = Color.Blue;
                }
                else
                {
                    btn_jsyz.TextColor = CurBtnColor;
                }
                //与临床医师沟通内容
                DBHelper.BLL.exam_lcys_im insFrm = new DBHelper.BLL.exam_lcys_im();
                DataTable dtlcgt = insFrm.GetData(study_no);
                if (dtlcgt != null && dtlcgt.Rows.Count > 0)
                {
                    buttonX19.TextColor = Color.Blue;
                }
                else
                {
                    buttonX19.TextColor = CurBtnColor;
                }
                //冰冻报告
                DBHelper.BLL.exam_ice_report insBd = new DBHelper.BLL.exam_ice_report();
                DataTable dtBd = insBd.GetData(study_no);
                if (dtBd != null && dtBd.Rows.Count == 1)
                {
                    btn_BD.TextColor = Color.Blue;
                }
                else
                {
                    btn_BD.TextColor = CurBtnColor;
                }
                //免疫组化报告
                DBHelper.BLL.myzh_report insmyzh = new DBHelper.BLL.myzh_report();
                DataTable dtmyzh = insmyzh.GetData(study_no);
                if (dtmyzh != null && dtmyzh.Rows.Count == 1)
                {
                    this.buttonX18.TextColor = Color.Blue;
                }
                else
                {
                    buttonX18.TextColor = CurBtnColor;
                }
                //修改意见
                DBHelper.BLL.report_xg_advice insxgyj = new DBHelper.BLL.report_xg_advice();
                DataTable dtxgyj = insxgyj.GetXgAdvice(study_no);
                if (dtxgyj != null && dtxgyj.Rows.Count > 0)
                {
                    buttonX4.TextColor = Color.Blue;
                }
                else
                {
                    buttonX4.TextColor = CurBtnColor;
                }
                //会诊意见
                DBHelper.BLL.huizhen_info inshz = new DBHelper.BLL.huizhen_info();
                DataTable dthz = inshz.GetHzinfo(study_no);
                if (dthz != null && dthz.Rows.Count > 0)
                {
                    buttonX5.TextColor = Color.Blue;
                }
                else
                {
                    buttonX5.TextColor = CurBtnColor;
                }
                //追踪随访
                DBHelper.BLL.pat_zzsf_advice inszzsf = new DBHelper.BLL.pat_zzsf_advice();
                DataTable dtzzsf = inszzsf.GetZzsfAdvice(study_no);
                if (dtzzsf != null && dtzzsf.Rows.Count > 0)
                {
                    buttonX6.TextColor = Color.Blue;
                }
                else
                {
                    buttonX6.TextColor = CurBtnColor;
                }
                //延时报告
                DBHelper.BLL.exam_delay_report insys = new DBHelper.BLL.exam_delay_report();
                DataTable dtys = insys.GetData(study_no);
                if (dtys != null && dtys.Rows.Count == 1)
                {
                    buttonX1.TextColor = Color.Blue;
                }
                else
                {
                    buttonX1.TextColor = CurBtnColor;
                }
                //补充报告
                DBHelper.BLL.exam_bc_report insbc = new DBHelper.BLL.exam_bc_report();
                DataTable dtbc = insbc.GetData(study_no);
                if (dtbc != null && dtbc.Rows.Count == 1)
                {
                    buttonX3.TextColor = Color.Blue;
                }
                else
                {
                    buttonX3.TextColor = CurBtnColor;
                }
                //获取检查状态(展示报告信息)
                DBHelper.BLL.exam_master insStatus = new DBHelper.BLL.exam_master();
                int status = insStatus.GetStudyExam_Status(txt_pz_blh.Text.Trim());
                if (status < 40)
                {
                    comboBoxEx1.Text = Program.User_Name;
                    comboBoxEx2.Text = Program.User_Name;
                }
                DataSet dsbgflag = insStatus.GetMasBgnrFlag(txt_pz_blh.Text.Trim());
                if (dsbgflag != null && dsbgflag.Tables[0].Rows.Count > 0)
                {
                    cmbBgnr.Text = dsbgflag.Tables[0].Rows[0]["bgnr_gs_flag"].ToString();
                    cmbBbqc.Text = dsbgflag.Tables[0].Rows[0]["bbqc_info"].ToString();
                }

                //检查部位
                if (status < 40)
                {
                    DBHelper.BLL.exam_specimens InsSpec = new DBHelper.BLL.exam_specimens();
                    DataTable dtSpec = InsSpec.GetBGSpecimensDTQC(exam_no);
                    if (dtSpec != null && dtSpec.Rows.Count > 0)
                    {
                        string StrParts = "";
                        for (int i = 0; i <= dtSpec.Rows.Count - 1; i++)
                        {
                            if (StrParts.Equals(""))
                            {
                                StrParts = dtSpec.Rows[i]["parts"].ToString();
                            }
                            else
                            {
                                StrParts += "," + dtSpec.Rows[i]["parts"].ToString();
                            }
                        }
                        textBoxX8.Text = StrParts;
                    }
                }
                else if (status >= 40)
                {
                    DataTable dt = insStatus.SelectParts(txt_pz_blh.Text.Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        textBoxX8.Text = dt.Rows[0]["parts"].ToString();
                    }
                }
                //（1）还没有开始写报告的大体所见
                if (report_type == 0)
                {
                    if (status < 40)
                    {
                        DBHelper.BLL.exam_specimens InsSpec = new DBHelper.BLL.exam_specimens();
                        DataTable dtSpec = InsSpec.GetBGSpecimensDTQC(exam_no);
                        if (dtSpec != null)
                        {
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i <= dtSpec.Rows.Count - 1; i++)
                            {
                                sb.AppendFormat("{0}", dtSpec.Rows[i]["see_memo"].ToString());
                                //sb.AppendLine();
                                //sb.Append(dtSpec.Rows[i]["draw_datetime"].ToString());
                                if (i != dtSpec.Rows.Count - 1)
                                {
                                    sb.AppendLine();
                                }
                            }
                            txt_rysj.Text = sb.ToString();
                            sb.Clear();
                            sb = null;
                        }
                    }
                }
                else if (report_type == 5)
                {
                    txt_sjdw.Text = this.patientInfo1.GetSjdw();
                }

                //（2）已经写了报告的报告内容
                if (status >= 40)
                {
                    DBHelper.BLL.exam_report InsBllReport = new DBHelper.BLL.exam_report();
                    DBHelper.Model.exam_report InsMReport = InsBllReport.GetExam_Report(study_no);
                    if (InsMReport != null)
                    {
                        cmb_gpys.Text = InsMReport.report_gb_doc;

                        if (report_type == 0)
                        {
                            txt_rysj.Text = InsMReport.rysj;
                            txt_zdyj.Text = InsMReport.zdyj;
                        }
                        else if (report_type == 3)
                        {
                            txt_xbxXfs.Text = InsMReport.zdyj;
                        }
                        else if (report_type == 5)
                        {
                            integerInput1.Value = InsMReport.lk_num;
                            integerInput2.Value = InsMReport.bp_num;
                            txt_wyblh.Text = InsMReport.wy_study_no;
                            txt_Wy_zdyj.Text = InsMReport.zdyj;
                        }

                        ReportImageStr = InsMReport.image ?? "";
                        if (status >= 55)
                        {
                            report_bl_datetime.Text = InsMReport.cbreport_datetime;
                        }
                        else
                        {
                            report_bl_datetime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        cmb_bl_sfyx.Text = InsMReport.sfyx;
                        if (((DevComponents.Editors.ComboItem)(cmb_bl_sfyx.SelectedItem)).Text.Equals(""))
                        {
                            cmb_bl_sfyx.Text = "否";
                        }
                        txt_lcfh.Text = InsMReport.lcfh;
                        if (txt_lcfh.Text.Trim().Equals(""))
                        {
                            txt_lcfh.Text = "符合";
                        }
                        txt_bl_zdpz.Text = InsMReport.zdpz;
                        comboBoxEx1.Text = InsMReport.cbreport_doc_name;
                        comboBoxEx2.Text = InsMReport.zzreport_doc_name;
                        txt_bl_shys.Text = InsMReport.shreport_doc_name;
                        txt_ICD10.Text = InsMReport.zdbm;
                    }
                }
                //刷新报表
                if (superTabControl1.SelectedTabIndex == 3)
                {
                    this.reportViewer1.Visible = false;
                }
                //3.多媒体资料下载
                DBHelper.BLL.multi_media_info insMulti = new DBHelper.BLL.multi_media_info();
                List<string> RemoteLst = new List<string>();
                List<string> LocalLst = new List<string>();
                DataTable dtMulit = null;
                //大体取材图片
                if (PIS_Sys.Properties.Settings.Default.Load_DT_IMG)
                {
                    dtMulit = insMulti.GetData(txt_pz_blh.Text.Trim(), 1);
                    if (dtMulit != null && dtMulit.Rows.Count > 0)
                    {
                        string PathImg = DTImgFolder + @"\" + txt_pz_blh.Text.Trim();
                        if (Directory.Exists(PathImg) == false)
                        {
                            Directory.CreateDirectory(PathImg);
                            //全部
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
                                //剩余的
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
                    if (dtMulit != null)
                    {
                        dtMulit.Clear();
                        dtMulit = null;
                    }
                }
                dtMulit = insMulti.GetData(txt_pz_blh.Text.Trim(), 4);
                if (dtMulit != null && dtMulit.Rows.Count > 0)
                {
                    string PathImg = ImgFolder + @"\" + txt_pz_blh.Text.Trim();
                    if (Directory.Exists(PathImg) == false)
                    {
                        Directory.CreateDirectory(PathImg);
                        //全部
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
                            //剩余的
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
                    if (dtMulit != null)
                    {
                        dtMulit.Clear();
                        dtMulit = null;
                    }
                }
                //开始下载
                if (RemoteLst.Count > 0)
                {
                    try
                    {
                        ftpDownload1.ExecuteDownLoad(Program.FtpIP, Program.FtpPort, Program.FtpUser, Program.FtpPwd, RemoteLst, LocalLst, 50000, RemoteLst.Count);
                        ftpDownload1.Visible = true;
                    }
                    catch
                    {
                        ftpDownload1.Visible = false;
                    }
                }
                else
                {
                    //刷新多媒体列表
                    RefreshMultiList();
                    if (superTabControl1.SelectedTabIndex == 3)
                    {
                        this.reportViewer1.Visible = true;
                        RefreshPreviewReport();
                    }
                }

                //4.按钮控制
                ControlBtn(status, study_no);
            }
            catch (Exception ex)
            {
                this.reportViewer1.Visible = true;
                Program.FileLog.ErrorFormat("报告展示异常，请确认FTP服务器正常设置:{0}\n", ex.ToString());
            }
        }
        //操作控制
        private void ControlBtn(int status, string study_no)
        {
            //假如取材状态
            if (status < 40)
            {
                btn_cap.Enabled = true;
                btn_save1.Enabled = true;
                btn_jsyz.Enabled = true;
                btn_tjyz.Enabled = true;
                btn_preview.Enabled = false;
                btn_send.Enabled = false;
                btn_sc.Enabled = false;
                btn_check.Enabled = false;
                btn_print1.Enabled = false;
                btn_print.Enabled = false;
                btn_modify.Enabled = false;
                buttonX5.Enabled = false;
                buttonX4.Enabled = false;
                if (report_type == 0)
                {
                    tabControlPanel1.Enabled = true;
                }
                else if (report_type == 3)
                {
                    tabControlPanel4.Enabled = true;
                }
                else if (report_type == 5)
                {
                    tabControlPanel5.Enabled = true;
                }
                txt_bl_zdpz.ReadOnly = false;
            }
            //假如报告已经打印
            if (status >= 55)
            {
                btn_cap.Enabled = false;
                btn_save1.Enabled = false;
                btn_check.Enabled = false;
                btn_send.Enabled = false;
                btn_preview.Enabled = true;
                btn_sc.Enabled = true;
                btn_print1.Enabled = true;
                btn_print.Enabled = true;
                btn_modify.Enabled = true;
                buttonX5.Enabled = true;
                buttonX4.Enabled = true;
                if (report_type == 0)
                {
                    tabControlPanel1.Enabled = false;
                }
                else if (report_type == 3)
                {
                    tabControlPanel4.Enabled = false;
                }
                else if (report_type == 5)
                {
                    tabControlPanel5.Enabled = false;
                }
                txt_bl_zdpz.ReadOnly = true;
            }
            //报告正在书写过程中
            if (status < 55 && status >= 40)
            {
                //获取报告医师编码及级别
                DBHelper.BLL.exam_report InsR = new DBHelper.BLL.exam_report();
                DataTable dt = InsR.GetReportYsInfo(study_no);
                buttonX5.Enabled = true;
                buttonX4.Enabled = true;
                if (dt != null && dt.Rows.Count == 1)
                {
                    DBHelper.BLL.sys_user InsU = new DBHelper.BLL.sys_user();
                    int CBReportRoleCode = InsU.GetUserRoleCode(dt.Rows[0]["cbreport_doc_code"].ToString());
                    if (Program.User_Code.Equals(dt.Rows[0]["cbreport_doc_code"].ToString()) || Program.User_Code.Equals(dt.Rows[0]["zzreport_doc_code"].ToString()) || Program.User_Code.Equals(dt.Rows[0]["shreport_doc_code"].ToString()) || CBReportRoleCode < Program.user_role_code)
                    {
                        //同一组写报告的人 //级别高的人
                        btn_cap.Enabled = true;
                        btn_save1.Enabled = true;
                        btn_jsyz.Enabled = true;
                        btn_tjyz.Enabled = true;
                        btn_send.Enabled = true;
                        btn_modify.Enabled = false;
                        if (status == 50)
                        {
                            btn_check.Enabled = false;
                            btn_send.Enabled = false;
                        }
                        else
                        {
                            btn_check.Enabled = true;
                            btn_send.Enabled = true;
                        }
                        btn_preview.Enabled = true;
                        btn_sc.Enabled = true;
                        btn_print1.Enabled = true;
                        btn_print.Enabled = true;
                        if (report_type == 0)
                        {
                            tabControlPanel1.Enabled = true;
                        }
                        else if (report_type == 3)
                        {
                            tabControlPanel4.Enabled = true;
                        }
                        else if (report_type == 5)
                        {
                            tabControlPanel5.Enabled = true;
                        }
                        txt_bl_zdpz.ReadOnly = false;
                    }
                    else
                    {
                        btn_cap.Enabled = false;
                        btn_save1.Enabled = false;
                        btn_jsyz.Enabled = false;
                        btn_tjyz.Enabled = false;
                        btn_check.Enabled = false;
                        btn_send.Enabled = false;
                        btn_modify.Enabled = false;
                        btn_preview.Enabled = true;
                        btn_sc.Enabled = true;
                        btn_print1.Enabled = true;
                        btn_print.Enabled = true;
                        if (report_type == 0)
                        {
                            tabControlPanel1.Enabled = false;
                        }
                        else if (report_type == 3)
                        {
                            tabControlPanel4.Enabled = false;
                        }
                        else if (report_type == 5)
                        {
                            tabControlPanel5.Enabled = false;
                        }
                        txt_bl_zdpz.ReadOnly = true;
                    }
                }
            }
        }

        private void superGridControl1_SelectionChanged(object sender, GridEventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (e.GridPanel.ActiveRow == null)
                {
                    superGridControl1.PrimaryGrid.Rows.Clear();
                    superGridControl1.PrimaryGrid.InvalidateLayout();
                    return;
                }
                int index = e.GridPanel.ActiveRow.Index;
                if (index == -1)
                {
                    btn_up.Enabled = false;
                    btn_down.Enabled = false;
                    return;
                }

                RefreshData(index);
            }
            superGridControl1.Select();
            superGridControl1.Focus();
        }

        public void RefreshData(int index)
        {

            if (index == 0)
            {
                btn_up.Enabled = false;
                if (superGridControl1.PrimaryGrid.Rows.Count > 1)
                {
                    btn_down.Enabled = true;
                }
                else
                {
                    btn_down.Enabled = false;
                }
            }
            else if (index == superGridControl1.PrimaryGrid.Rows.Count - 1)
            {
                btn_down.Enabled = false;
                if (superGridControl1.PrimaryGrid.Rows.Count > 1)
                {
                    btn_up.Enabled = true;
                }
                else
                {
                    btn_up.Enabled = false;
                }
            }
            else
            {
                btn_up.Enabled = true;
                btn_down.Enabled = true;
            }

            GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
            //申请单号
            string exam_no = Row.Cells["exam_no"].Value.ToString();
            //病理号
            string study_no = Row.Cells["study_no"].Value.ToString();
            //病人姓名
            string patient_name = Row.Cells["patient_name"].Value.ToString();
            //病人id号
            string patient_id = Row.Cells["patient_id"].Value.ToString();
            // 
            string patient_source = Row.Cells["patient_source"].Value.ToString();
            string sex = Row.Cells["sex"].Value.ToString();
            string age = Row.Cells["age"].Value.ToString();
            //取当前已经采集的报告图像数目
            DBHelper.BLL.multi_media_info insMulti = new DBHelper.BLL.multi_media_info();
            DataTable dtMulit = insMulti.GetData(study_no, 4);
            string imgnum = "0";
            string strinfo = "";
            if (dtMulit != null && dtMulit.Rows.Count > 0)
            {
                imgnum = dtMulit.Rows.Count.ToString();
            }
            string ice_flag = Row.Cells["ice_flag"].Value.ToString();
            if (ice_flag.Equals("1"))
            {
                strinfo = "冰";
            }
            string ks_flag = Row.Cells["ks_flag"].Value.ToString();
            if (ks_flag.Equals("1"))
            {
                strinfo = "快";
            }
            Row.RowHeaderText = imgnum + strinfo;

            //取历次检查数目
            DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
            int total = ins.GetExamCount(exam_no, patient_name);
            lbl_history.Text = string.Format(lcjc_str, total);
            // 实时查询状态信息
            string status_code = "";
            int lock_flag = 0;
            string lock_doc_name = "";
            string lock_doc_code = "";
            int wtzd_flag = 0;
            if (ins.GetCurExamno_Status(exam_no, ref status_code, ref lock_flag, ref lock_doc_name, ref lock_doc_code, ref wtzd_flag))
            {
                //if (lock_flag == 1 && !lock_doc_code.Equals(Program.User_Code))
                //{
                //    Frm_TJInfo("报告被锁定", string.Format("当前检查被{0}医生锁定\n当前不能进行报告书写操作！", lock_doc_name));
                //    return;
                //}
                if (!status_code.Equals(Row.Cells["exam_status"].Value.ToString()))
                {
                    if (examStatus_dic.ContainsKey(status_code))
                    {
                        if (exam_status_dic.ContainsKey(status_code))
                        {
                            Row.Cells["status_name"].CellStyles.Default.TextColor = exam_status_dic[status_code];
                            Row.Cells["exam_status"].Value = status_code;
                        }
                        Row.Cells["status_name"].Value = examStatus_dic[status_code];
                    }
                    else
                    {
                        //刷新列表
                        Frm_TJInfo("提示", "此病人报告已经发布，正在刷新病人列表！");
                        superTabControl2.SelectedTabIndex = 0;
                        QueryBtn_Click(null, null);
                        return;
                    }
                }
            }
            //委托诊断标记
            if (wtzd_flag == 1)
            {
                Lab_WtzdFlag.Visible = true;
                Lab_WtzdFlag.Tag = "1";
            }
            else
            {
                Lab_WtzdFlag.Visible = false;
                Lab_WtzdFlag.Tag = "0";
            }

            //检查类型
            string modality = Row.Cells["modality"].Value.ToString();
            //根据当前检查状态来确定模板
            if (Convert.ToInt32(status_code) >= 40)
            {
                //查询报告模板
                DBHelper.BLL.exam_report insReport = new DBHelper.BLL.exam_report();
                int Report_Index = insReport.GetReportTemletIndex(study_no);
                SetTabVisible(Report_Index);
            }
            else
            {
                SetTabVisible(modality);
            }
            //冰冻类型
            string Ice_flag = Row.Cells["ice_flag"].Value.ToString();
            if (Ice_flag.Equals("1"))
            {
                btn_BD.Enabled = true;
            }
            else
            {
                btn_BD.Enabled = false;
            }
            //基本信息
            this.patientInfo1.SetPatinfo(patient_id, exam_no);
            //数据展示
            ShowJcInfo(exam_no, patient_id, study_no, patient_name, sex, age, patient_source);

        }
        private void buttonX31_Click(object sender, EventArgs e)
        {
            string str_tj = txt_tj.Text.Trim().Replace("'", "");
            if (!str_tj.Equals(""))
            {
                DataSet _DataSet = null;
                string key_str = ((DevComponents.Editors.ComboItem)(cmb_tj.SelectedItem)).Text.Trim();
                //初始统计计数
                Init_Js();
                //执行查询
                if (_DataSet != null)
                {
                    _DataSet.Clear();
                }
                _DataSet = new DataSet();
                DBHelper.BLL.exam_master exam_mas_Ins = new DBHelper.BLL.exam_master();
                _DataSet = exam_mas_Ins.QueryDsBGExam_master(key_str, str_tj, Program.workstation_type_db);
                if (_DataSet != null && _DataSet.Tables[0].Rows.Count > 0)
                {
                    superGridControl1.PrimaryGrid.ClearSelectedCells();
                    superGridControl1.PrimaryGrid.ClearSelectedRows();
                    superGridControl1.PrimaryGrid.DataSource = _DataSet;
                    superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Red'><b>{0}</b></font> 条记录", _DataSet.Tables[0].Rows.Count);

                }
                else
                {
                    superGridControl1.PrimaryGrid.DataSource = null;
                    Frm_TJInfo("病人不存在", string.Format("{0}为{1}的查询为空！", key_str, str_tj));
                    ClearData();

                }
            }
        }

        private void buttonX30_Click(object sender, EventArgs e)
        {
            txt_tj.Text = "";
            txt_tj.Focus();
        }

        private void txt_tj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                string str_tj = txt_tj.Text.Trim();
                if (!str_tj.Equals(""))
                {
                    buttonX31_Click(null, null);
                }
            }
        }
        private void lbl_knly_Click(object sender, EventArgs e)
        {
            Form Frm_mesIns = Application.OpenForms["FrmMessage"];
            if (Frm_mesIns == null)
            {
                Frm_mesIns = new FrmMessage();
                Frm_mesIns.TopMost = true;
                Frm_mesIns.WindowState = FormWindowState.Normal;
                Frm_mesIns.Show();
                Frm_mesIns.Activate();
            }
            else
            {
                Frm_mesIns.BringToFront();
                Frm_mesIns.WindowState = FormWindowState.Normal;
                Frm_mesIns.Activate();
            }
        }
        private void cmb_tj_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_tj.Focus();
        }
        #endregion

        #region  "窗体界面按钮" 

        //ICD10疾病编码
        private void buttonX15_Click(object sender, EventArgs e)
        {
            Frm_ICD10 Frm_IcdIns = new Frm_ICD10();
            Frm_IcdIns.Owner = this;
            Frm_IcdIns.TopMost = true;
            Control ctrl = sender as Control;
            Point startPoint = superTabControlPanel1.PointToScreen(ctrl.Location);
            startPoint.Y += ctrl.Height;
            Frm_IcdIns.StartPosition = FormStartPosition.Manual;
            Frm_IcdIns.Location = startPoint;
            if (Frm_IcdIns.ShowDialog() == DialogResult.OK)
            {
                this.txt_ICD10.Text = Frm_IcdIns.icd_code;
            }
        }

        //综合查询
        private void buttonX13_Click(object sender, EventArgs e)
        {
            Frm_Zhcx ins = new Frm_Zhcx();
            ins.Owner = this;
            if (ins.ShowDialog() == DialogResult.OK)
            {
                string str_tj = ins.str_tj;
                if (!str_tj.Equals("||"))
                {
                    DataSet _DataSet = null;
                    string key_str = "综合诊断意见";
                    //初始统计计数
                    Init_Js();
                    //执行查询
                    if (_DataSet != null)
                    {
                        _DataSet.Clear();
                    }
                    _DataSet = new DataSet();
                    DBHelper.BLL.exam_master exam_mas_Ins = new DBHelper.BLL.exam_master();
                    _DataSet = exam_mas_Ins.QueryDsBGExam_master(key_str, str_tj, Program.workstation_type_db);
                    if (_DataSet != null && _DataSet.Tables[0].Rows.Count > 0)
                    {
                        superGridControl1.PrimaryGrid.ClearSelectedCells();
                        superGridControl1.PrimaryGrid.ClearSelectedRows();
                        superGridControl1.PrimaryGrid.DataSource = _DataSet;
                        superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Red'><b>{0}</b></font> 条记录", _DataSet.Tables[0].Rows.Count);

                    }
                    else
                    {
                        superGridControl1.PrimaryGrid.DataSource = null;
                        Frm_TJInfo("病人不存在", string.Format("{0}为{1}的查询为空！", key_str, str_tj.Replace("|", "")));
                        ClearData();
                    }
                }
            }
        }
        //查看pacs影像
        private void buttonX12_Click(object sender, EventArgs e)
        {

        }

        //查看电子病历
        private void buttonX8_Click(object sender, EventArgs e)
        {
            if (!txt_sqd.Text.Trim().Equals(""))
            {
                DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                DataTable dt = ins.GetDt(string.Format("select exam_no,new_flag,visit_id from exam_master where exam_no='{0}'", txt_sqd.Text.Trim()));
                if (dt != null && dt.Rows.Count == 1)
                {
                    if (dt.Rows[0]["new_flag"].ToString().Equals("0"))
                    {
                        if (dt.Rows[0]["visit_id"].ToString().Length > 3)
                        {
                            Process ps = new Process();
                            string EMrURL = string.Format(Program.EmrUrlStr, dt.Rows[0]["visit_id"].ToString());
                            ps.StartInfo.FileName = "iexplore.exe";
                            ps.StartInfo.Arguments = EMrURL;
                            ps.Start();
                        }
                        else
                        {
                            Frm_TJInfo("提示", "此病人无电子病历信息！");
                        }
                    }
                    else
                    {
                        Frm_TJInfo("提示", "手工登记病人不能查看！");
                    }
                }
            }
        }
        //复制
        private void buttonX10_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (report_type == 0)
                {
                    Clipboard.SetText(txt_zdyj.Text.Trim());
                    Frm_TJInfo("提示", "复制诊断内容成功！");
                }
            }
            catch
            {
            }
        }
        //冰冻列表
        private void buttonX9_Click_1(object sender, EventArgs e)
        {
            DataSet _DataSet = null;
            string dt_tj = "两周";
            for (int i = 0; i < this.QueryBtn.SubItems.Count; i++)
            {
                ButtonItem BtnItem = (ButtonItem)this.QueryBtn.SubItems[i];
                if (BtnItem.Checked == true)
                {
                    dt_tj = BtnItem.Text;
                    break;
                }
            }
            string lb_tj = "all";
            for (int i = 0; i < this.BtnJclb.SubItems.Count; i++)
            {
                ButtonItem BtnItem = (ButtonItem)this.BtnJclb.SubItems[i];
                if (BtnItem.Checked == true)
                {
                    lb_tj = BtnItem.Tag.ToString();
                    break;
                }
            }
            string zt_tj = "";
            if (chK_status.Checked)
            {
                zt_tj = ((DevComponents.Editors.ComboItem)(cmb_ExamStatus.SelectedItem)).Value.ToString();
            }
            //初始统计计数
            Init_Js();
            //执行数据刷新
            if (_DataSet != null)
            {
                _DataSet.Clear();
            }
            _DataSet = new DataSet();
            DBHelper.BLL.exam_master exam_mas_Ins = new DBHelper.BLL.exam_master();

            _DataSet = exam_mas_Ins.GetDsBGExam_master(dt_tj, zt_tj, Program.workstation_type_db, Program.User_Code, lb_tj, 1);
            if (_DataSet != null && _DataSet.Tables[0].Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.ClearSelectedCells();
                superGridControl1.PrimaryGrid.ClearSelectedRows();
                superGridControl1.PrimaryGrid.DataSource = _DataSet;
                //计数
                tj_func(_DataSet.Tables[0]);
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
                btn_up.Enabled = false;
                btn_down.Enabled = false;
                ClearData();
            }
        }
        //快速列表
        private void buttonX11_Click_1(object sender, EventArgs e)
        {
            DataSet _DataSet = null;
            string dt_tj = "两周";
            for (int i = 0; i < this.QueryBtn.SubItems.Count; i++)
            {
                ButtonItem BtnItem = (ButtonItem)this.QueryBtn.SubItems[i];
                if (BtnItem.Checked == true)
                {
                    dt_tj = BtnItem.Text;
                    break;
                }
            }
            string lb_tj = "all";
            for (int i = 0; i < this.BtnJclb.SubItems.Count; i++)
            {
                ButtonItem BtnItem = (ButtonItem)this.BtnJclb.SubItems[i];
                if (BtnItem.Checked == true)
                {
                    lb_tj = BtnItem.Tag.ToString();
                    break;
                }
            }
            string zt_tj = "";
            if (chK_status.Checked)
            {
                zt_tj = ((DevComponents.Editors.ComboItem)(cmb_ExamStatus.SelectedItem)).Value.ToString();
            }
            //初始统计计数
            Init_Js();
            //执行数据刷新
            if (_DataSet != null)
            {
                _DataSet.Clear();
            }
            _DataSet = new DataSet();
            DBHelper.BLL.exam_master exam_mas_Ins = new DBHelper.BLL.exam_master();

            _DataSet = exam_mas_Ins.GetDsBGExam_master(dt_tj, zt_tj, Program.workstation_type_db, Program.User_Code, lb_tj, 2);
            if (_DataSet != null && _DataSet.Tables[0].Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.ClearSelectedCells();
                superGridControl1.PrimaryGrid.ClearSelectedRows();
                superGridControl1.PrimaryGrid.DataSource = _DataSet;
                //计数
                tj_func(_DataSet.Tables[0]);
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
                btn_up.Enabled = false;
                btn_down.Enabled = false;
                ClearData();
            }
        }
        //单击选中项目
        private void imageListView1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Checked == true)
            {
                e.Item.Checked = false;
            }
            else
            {
                e.Item.Checked = true;
            }
        }
        //删除
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (imageListView1.Items.Count > 0)
            {
                superTabControl1.SelectedTabIndex = 1;
                Application.DoEvents();
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;
                if (TaskDialog.Show("删除照片", "确认", "确定要删除选中照片么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    for (int i = 0; i < imageListView1.Items.Count; i++)
                    {
                        if (imageListView1.Items[i].Checked == true)
                        {
                            string filePath = imageListView1.Items[i].FileName;
                            string filename = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                            DBHelper.BLL.multi_media_info ins = new DBHelper.BLL.multi_media_info();
                            if (filename.IndexOf("DT") != -1)
                            {
                                if (ins.DelData(this.txt_pz_blh.Text.Trim(), filename, 1) == 1)
                                {
                                    if (System.IO.File.Exists(filePath))
                                    {
                                        System.IO.File.Delete(filePath);
                                    }
                                }
                                else
                                {
                                    if (System.IO.File.Exists(filePath))
                                    {
                                        System.IO.File.Delete(filePath);
                                    }
                                }
                            }
                            else if (filename.IndexOf("BG") != -1)
                            {
                                if (ins.DelData(this.txt_pz_blh.Text.Trim(), filename, 4) == 1)
                                {
                                    if (System.IO.File.Exists(filePath))
                                    {
                                        System.IO.File.Delete(filePath);
                                    }
                                }
                                else
                                {
                                    if (System.IO.File.Exists(filePath))
                                    {
                                        System.IO.File.Delete(filePath);
                                    }
                                }
                            }
                        }
                    }
                    RefreshMultiList();
                    //更新报告图像
                    string strImage = GetBgImage();
                    DBHelper.BLL.exam_report insImg = new DBHelper.BLL.exam_report();
                    insImg.UpdateReport_Image(txt_pz_blh.Text.Trim(), strImage);
                }
            }
        }
        //选择图像后，直接保存
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            btn_save1.PerformClick();
        }
        //修改组织学肉眼所见内容字体大小
        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txt_rysj.Font = new Font(this.txt_rysj.Font.Name, Convert.ToSingle(this.ComboBox2.Text));
        }

        //高级模式预览补充报告
        public void RefreshBCPreviewReport(ref FastReportLib.BLReportParas InsBLReport, ref string PathImg)
        {
            if (txt_pz_blh.Text.Trim().Equals(""))
            {
                return;
            }
            //预览报告
            if (superGridControl1.ActiveRow == null)
            {
                return;
            }
            int index = superGridControl1.ActiveRow.Index;
            if (index == -1)
            {
                return;
            }
            //获取选中的图像
            GetBgImage();
            //提取当前关键信息
            string study_no = txt_pz_blh.Text.Trim();
            string exam_no = txt_sqd.Text.Trim();
            PathImg = Program.APPdirPath;
            try
            {
                if (report_type == 0)
                {
                    GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                    //病理学报告单内容对象
                    InsBLReport.InitParasDate();
                    InsBLReport.ReportParaHospital = Program.HospitalName;
                    InsBLReport.study_no = Row.Cells["study_no"].Value.ToString();
                    InsBLReport.Txt_Name = Row.Cells["patient_name"].Value.ToString();
                    InsBLReport.Txt_Sex = Row.Cells["sex"].Value.ToString();
                    InsBLReport.Txt_Age = Row.Cells["age"].Value.ToString();
                    InsBLReport.Txt_ly = Row.Cells["patient_source"].Value.ToString();

                    if (InsBLReport.Txt_ly.Equals("外来"))
                    {
                        InsBLReport.Txt_Sjks = Row.Cells["submit_unit"].Value.ToString();
                    }
                    else
                    {
                        InsBLReport.Txt_Sjks = Row.Cells["req_dept"].Value.ToString();
                    }
                    InsBLReport.Txt_Bf = Row.Cells["ward"].Value.ToString();
                    InsBLReport.Txt_Ch = Row.Cells["bed_no"].Value.ToString();
                    InsBLReport.Txt_Zyh = Row.Cells["input_id"].Value.ToString();
                    InsBLReport.Txt_Sjys = Row.Cells["req_physician"].Value.ToString();
                    InsBLReport.Txt_Sqrq = Row.Cells["received_datetime"].Value.ToString().Substring(0, 10);
                    InsBLReport.Txt_mzh = Row.Cells["output_id"].Value.ToString();
                    InsBLReport.Txt_Rysj = txt_rysj.Text.Trim();
                    InsBLReport.Txt_Blzd = txt_zdyj.Text.Trim();
                    InsBLReport.txt_Bgrq = report_bl_datetime.Value.ToString("yyyy-MM-dd");
                    InsBLReport.Txt_Bgys = comboBoxEx1.Text.Trim();
                    InsBLReport.Txt_fjys = comboBoxEx2.Text.Trim();
                    InsBLReport.fileImages = ReportImageStr;
                    InsBLReport.Txt_zdpz = txt_bl_zdpz.Text.Trim();
                    //送检部位
                    InsBLReport.Txt_Sjbw = textBoxX8.Text.Trim();
                    //PId
                    if (Row.Cells["patient_id"].Value.ToString().IndexOf("New") == -1)
                    {
                        InsBLReport.Txt_Pid = Row.Cells["patient_id"].Value.ToString();
                    }
                    else
                    {
                        InsBLReport.Txt_Pid = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Program.FileLog.Error(ex.ToString());
            }
            finally
            {

            }
        }


        //补充报告
        private void buttonX3_Click_1(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                Frm_bcbg Frm_bcbgIns = new Frm_bcbg();
                Frm_bcbgIns.Owner = this;
                Frm_bcbgIns.BLH = txt_pz_blh.Text.Trim();
                Frm_bcbgIns.exam_no = txt_sqd.Text.Trim();
                Frm_bcbgIns.BringToFront();
                Frm_bcbgIns.ShowDialog();
                //补充报告
                DBHelper.BLL.exam_bc_report insbc = new DBHelper.BLL.exam_bc_report();
                DataTable dtbc = insbc.GetData(txt_pz_blh.Text.Trim());
                if (dtbc != null && dtbc.Rows.Count == 1)
                {
                    buttonX3.TextColor = Color.Blue;
                }
                else
                {
                    buttonX3.TextColor = CurBtnColor;
                }
            }
        }
        //修改意见
        private void buttonX4_Click_1(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                Frm_advice Frm_adviceIns = new Frm_advice();
                Frm_adviceIns.Owner = this;
                Frm_adviceIns.study_no = txt_pz_blh.Text.Trim();
                Frm_adviceIns.Y_DocName = comboBoxEx1.Text.Trim();
                Frm_adviceIns.Y_DocCode = comboBoxEx1.SelectedValue.ToString();
                Frm_adviceIns.Y_Time = report_bl_datetime.Value;
                Frm_adviceIns.Y_Zd = txt_zdyj.Text.Trim();
                Frm_adviceIns.BringToFront();
                Frm_adviceIns.ShowDialog();
                //修改意见
                DBHelper.BLL.report_xg_advice insxgyj = new DBHelper.BLL.report_xg_advice();
                DataTable dtxgyj = insxgyj.GetXgAdvice(txt_pz_blh.Text.Trim());
                if (dtxgyj != null && dtxgyj.Rows.Count > 0)
                {
                    buttonX4.TextColor = Color.Blue;
                }
                else
                {
                    buttonX4.TextColor = CurBtnColor;
                }
            }
        }
        //会诊意见
        private void buttonX5_Click_1(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                Frm_hzgl Frm_hzglIns = new Frm_hzgl();
                Frm_hzglIns.Owner = this;
                Frm_hzglIns.study_no = txt_pz_blh.Text.Trim();
                Frm_hzglIns.txt_bl_czys = comboBoxEx1.Text.Trim();
                Frm_hzglIns.txt_zdyj = txt_zdyj.Text.Trim();
                Frm_hzglIns.BringToFront();
                Frm_hzglIns.ShowDialog();
                //会诊意见
                DBHelper.BLL.huizhen_info inshz = new DBHelper.BLL.huizhen_info();
                DataTable dthz = inshz.GetHzinfo(txt_pz_blh.Text.Trim());
                if (dthz != null && dthz.Rows.Count > 0)
                {
                    buttonX5.TextColor = Color.Blue;
                }
                else
                {
                    buttonX5.TextColor = CurBtnColor;
                }
            }
        }
        //追踪随访
        private void buttonX6_Click(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                Frm_zzsf Frm_zzsfIns = new Frm_zzsf();
                Frm_zzsfIns.Owner = this;
                Frm_zzsfIns.study_no = txt_pz_blh.Text.Trim();
                Frm_zzsfIns.BringToFront();
                Frm_zzsfIns.ShowDialog();
                //追踪随访
                DBHelper.BLL.pat_zzsf_advice inszzsf = new DBHelper.BLL.pat_zzsf_advice();
                DataTable dtzzsf = inszzsf.GetZzsfAdvice(txt_pz_blh.Text.Trim());
                if (dtzzsf != null && dtzzsf.Rows.Count > 0)
                {
                    buttonX6.TextColor = Color.Blue;
                }
                else
                {
                    buttonX6.TextColor = CurBtnColor;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txt_zdyj.Font = new Font(this.txt_zdyj.Font.Name, Convert.ToSingle(this.comboBox1.Text));
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            type_dict_value = 3;
            if (this.contextMenuStrip2.Items.Count > 0)
            {
                Control ctrl = sender as Control;
                Point p = superTabControlPanel1.PointToScreen(new Point(ctrl.Right, ctrl.Top));
                this.contextMenuStrip2.Show(p);
            }
        }
        //打印机选择
        private void cmb_printers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_printers.SelectedIndex == -1)
            {
                return;
            }
            string curPrinter = ((DevComponents.Editors.ComboItem)(cmb_printers.SelectedItem)).Text;
            if (curPrinter != "")
            {
                PIS_Sys.Properties.Settings.Default.ReportPrinter = curPrinter;
                Properties.Settings.Default.Save();
            }
        }
        //批量打印报告
        private void buttonItem1_Click_1(object sender, EventArgs e)
        {
            dagl.Frm_Pldy ins = new dagl.Frm_Pldy();
            ins.Owner = this;
            ins.Show();
        }
        //延时报告
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                Frm_Delay_Report Frm_Delay_ReportIns = new Frm_Delay_Report();
                Frm_Delay_ReportIns.Owner = this;
                Frm_Delay_ReportIns.BLH = txt_pz_blh.Text.Trim();
                Frm_Delay_ReportIns.exam_no = txt_sqd.Text.Trim();
                Frm_Delay_ReportIns.BringToFront();
                Frm_Delay_ReportIns.ShowDialog();
                //延时报告
                DBHelper.BLL.exam_delay_report insys = new DBHelper.BLL.exam_delay_report();
                DataTable dtys = insys.GetData(txt_pz_blh.Text.Trim());
                if (dtys != null && dtys.Rows.Count == 1)
                {
                    buttonX1.TextColor = Color.Blue;
                }
                else
                {
                    buttonX1.TextColor = CurBtnColor;
                }

            }
        }
        //选图
        private void buttonX2_Click(object sender, EventArgs e)
        {
            superTabControl2.SelectedTabIndex = 2;
        }
        //临床符合选择
        private void btn_Lcfh_Click(object sender, EventArgs e)
        {
            type_dict_value = 0;
            if (this.contextMenuStrip8.Items.Count > 0)
            {
                Control ctrl = sender as Control;
                Point p = superTabControlPanel1.PointToScreen(new Point(ctrl.Right, ctrl.Top));
                this.contextMenuStrip8.Show(p);
            }
        }
        //记录菜单编码和对应的菜单ＩＤ的字典泛型变量
        Dictionary<string, int> DictionaryValues = new Dictionary<string, int>();
        Dictionary<string, int> DictionaryValues1 = new Dictionary<string, int>();
        Dictionary<string, int> DictionaryValues2 = new Dictionary<string, int>();
        public delegate void MenuEventHandler(string ChildText);
        public event MenuEventHandler MenuItemClick;
        private void CreatePopUpJcbwMenu(int type_dict)
        {
            DBHelper.BLL.exam_bgzd_dict Exam_part_Ins = new DBHelper.BLL.exam_bgzd_dict();
            DataTable dt = Exam_part_Ins.GetDsExam_bgzd_dict(type_dict);
            if (dt != null && dt.Rows.Count > 0)
            {
                int j = 0;
                int k = 0;
                int m = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //生成菜单项 id,part_name,parent_code,order_no
                    //是父菜单时
                    if (dt.Rows[i]["parent_code"].ToString().Equals("0"))
                    {
                        if (type_dict == 0)
                        {
                            k = this.contextMenuStrip8.Items.Add(new System.Windows.Forms.ToolStripMenuItem());
                            string tmp = dt.Rows[i]["id"].ToString();

                            if (DictionaryValues.ContainsKey(tmp) == false)
                            {
                                DictionaryValues.Add(tmp, k);
                            }
                            else
                            {
                                continue;
                            }
                            this.contextMenuStrip8.Items[k].Text = dt.Rows[i]["part_name"].ToString();
                            //添加父菜单的事件
                            (this.contextMenuStrip8.Items[k] as System.Windows.Forms.ToolStripMenuItem).Click += new EventHandler(ActiveEvent);

                        }
                        else if (type_dict == 3)
                        {
                            k = this.contextMenuStrip2.Items.Add(new System.Windows.Forms.ToolStripMenuItem());
                            string tmp = dt.Rows[i]["id"].ToString();

                            if (DictionaryValues2.ContainsKey(tmp) == false)
                            {
                                DictionaryValues2.Add(tmp, k);
                            }
                            else
                            {
                                continue;
                            }
                            this.contextMenuStrip2.Items[k].Text = dt.Rows[i]["part_name"].ToString();
                            //添加父菜单的事件
                            (this.contextMenuStrip2.Items[k] as System.Windows.Forms.ToolStripMenuItem).Click += new EventHandler(ActiveEvent);
                        }
                    }
                    else
                    {
                        if (type_dict == 0)
                        {
                            //获取父菜单的索引
                            if (DictionaryValues.TryGetValue(dt.Rows[i]["parent_code"].ToString(), out j))
                            {
                                //添加子菜单到父菜单中
                                m = (this.contextMenuStrip8.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems.Add(new System.Windows.Forms.ToolStripMenuItem());
                                (this.contextMenuStrip8.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Text = dt.Rows[i]["part_name"].ToString();
                                //记录菜单编码和菜单编号
                                DictionaryValues.Add(dt.Rows[i]["id"].ToString(), m);
                                //添加子菜单的事件
                                (this.contextMenuStrip8.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Click += new EventHandler(ActiveEvent);
                            }
                            else
                            {
                                //如果当前子菜单的父菜单不存在，则创建父菜单后再创建子菜单
                                k = this.contextMenuStrip8.Items.Add(new System.Windows.Forms.ToolStripMenuItem());
                                string tmp = dt.Rows[i]["parent_code"].ToString();
                                if (DictionaryValues.ContainsKey(tmp) == false)
                                {
                                    DictionaryValues.Add(tmp, k);
                                }
                                this.contextMenuStrip8.Items[k].Text = Exam_part_Ins.GetExam_bgzd_Name(type_dict, tmp);
                                //添加子菜单到父菜单中
                                m = (this.contextMenuStrip8.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems.Add(new System.Windows.Forms.ToolStripMenuItem());
                                (this.contextMenuStrip8.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Text = dt.Rows[i]["part_name"].ToString();
                                //记录菜单编码和菜单编号
                                DictionaryValues.Add(dt.Rows[i]["id"].ToString(), m);
                                //添加子菜单的事件
                                (this.contextMenuStrip8.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Click += new EventHandler(ActiveEvent);
                            }

                        }
                        else if (type_dict == 3)
                        {
                            //获取父菜单的索引
                            if (DictionaryValues2.TryGetValue(dt.Rows[i]["parent_code"].ToString(), out j))
                            {
                                //添加子菜单到父菜单中
                                m = (this.contextMenuStrip2.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems.Add(new System.Windows.Forms.ToolStripMenuItem());
                                (this.contextMenuStrip2.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Text = dt.Rows[i]["part_name"].ToString();
                                //记录菜单编码和菜单编号
                                DictionaryValues2.Add(dt.Rows[i]["id"].ToString(), m);
                                //添加子菜单的事件
                                (this.contextMenuStrip2.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Click += new EventHandler(ActiveEvent);
                            }
                            else
                            {
                                //如果当前子菜单的父菜单不存在，则创建父菜单后再创建子菜单
                                k = this.contextMenuStrip2.Items.Add(new System.Windows.Forms.ToolStripMenuItem());
                                string tmp = dt.Rows[i]["parent_code"].ToString();
                                if (DictionaryValues2.ContainsKey(tmp) == false)
                                {
                                    DictionaryValues2.Add(tmp, k);
                                }
                                this.contextMenuStrip2.Items[k].Text = Exam_part_Ins.GetExam_bgzd_Name(type_dict, tmp);
                                //添加子菜单到父菜单中
                                m = (this.contextMenuStrip2.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems.Add(new System.Windows.Forms.ToolStripMenuItem());
                                (this.contextMenuStrip2.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Text = dt.Rows[i]["part_name"].ToString();
                                //记录菜单编码和菜单编号
                                DictionaryValues2.Add(dt.Rows[i]["id"].ToString(), m);
                                //添加子菜单的事件
                                (this.contextMenuStrip2.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Click += new EventHandler(ActiveEvent);
                            }
                        }
                    }
                }
                dt.Clear();
                dt = null;
            }
        }
        // 右键菜单的激发菜单子项的单击事件
        private void ActiveEvent(Object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripMenuItem MI = sender as System.Windows.Forms.ToolStripMenuItem;
            if (MenuItemClick != null && MI.DropDownItems.Count == 0)
            {
                if (btn_save1.Enabled == true)
                {
                    MenuItemClick(MI.Text);
                }
            }
        }
        int type_dict_value = 0;
        private void bbxx_MenuItemClick(string BBBW)
        {
            if (type_dict_value == 0)
            {
                txt_lcfh.Text = BBBW;
            }
            else if (type_dict_value == 3)
            {
                txt_bl_zdpz.Text = BBBW;
            }
        }
        //历史报告
        private void lbl_history_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //申请单号
                string exam_no = Row.Cells["exam_no"].Value.ToString();
                //病人id号
                string patient_id = Row.Cells["patient_id"].Value.ToString();
                //病人姓名
                string patient_name = Row.Cells["patient_name"].Value.ToString();
                //取历次检查数目
                DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                int total = ins.GetExamCount(exam_no, patient_name);
                if (total > 0)
                {
                    Form Frm_Ins = Application.OpenForms["Frm_History"];
                    if (Frm_Ins != null)
                    {
                        Frm_Ins.Close();
                    }
                    Frm_History Frm_HisIns = new Frm_History();
                    Frm_HisIns.patient_name = patient_name;
                    Frm_HisIns.patient_id = patient_id;
                    Frm_HisIns.exam_no = exam_no;
                    Frm_HisIns.BringToFront();
                    Frm_HisIns.Show();
                }
            }
        }

        //诊断模板维护
        private void buttonItem26_Click(object sender, EventArgs e)
        {
            Frm_blzd_Templet ins = new Frm_blzd_Templet();
            ins.BringToFront();
            ins.ShowDialog();
            BuildBgTree();
        }
        //采图按钮
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                btn_PZ_Click(null, null);
            }
        }
        //上一例
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index != -1 && index != 0)
                {
                    //先清空以前选中的所有行
                    this.superGridControl1.PrimaryGrid.ClearSelectedCells();
                    this.superGridControl1.PrimaryGrid.ClearSelectedRows();
                    //最前面的黑色三角号跟着移动
                    this.superGridControl1.PrimaryGrid.SetActiveRow((GridContainer)superGridControl1.PrimaryGrid.Rows[index - 1]);
                    //选中当前行
                    superGridControl1.PrimaryGrid.SetSelectedRows(index - 1, 1, true);
                    superGridControl1.PrimaryGrid.SetActiveRow((GridRow)superGridControl1.PrimaryGrid.Rows[index - 1]);
                    superGridControl1.PrimaryGrid.Rows[index - 1].EnsureVisible();
                    //刷新数据界面
                    if (superTabControl2.SelectedTabIndex != 0)
                    {
                        RefreshData(index - 1);
                    }
                }
            }
        }
        //下一例
        private void buttonItem5_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index != -1 && index < superGridControl1.PrimaryGrid.Rows.Count - 1)
                {
                    //先清空以前选中的所有行
                    this.superGridControl1.PrimaryGrid.ClearSelectedCells();
                    this.superGridControl1.PrimaryGrid.ClearSelectedRows();
                    //最前面的黑色三角号跟着移动
                    this.superGridControl1.PrimaryGrid.SetActiveRow((GridContainer)superGridControl1.PrimaryGrid.Rows[index + 1]);
                    //选中当前行
                    superGridControl1.PrimaryGrid.SetSelectedRows(index + 1, 1, true);
                    superGridControl1.PrimaryGrid.SetActiveRow((GridRow)superGridControl1.PrimaryGrid.Rows[index + 1]);
                    superGridControl1.PrimaryGrid.Rows[index + 1].EnsureVisible();
                    //刷新数据界面
                    if (superTabControl2.SelectedTabIndex != 0)
                    {
                        RefreshData(index + 1);
                    }
                }
            }
        }
        //审核
        private void buttonItem6_Click(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                try
                {
                    //更新报告状态
                    DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                    int status = insM.GetStudyExam_Status(txt_pz_blh.Text.Trim());

                    if (status >= 40 && status < 50)
                    {
                        DBHelper.BLL.exam_report insBll = new DBHelper.BLL.exam_report();
                        Boolean ZXResult = insBll.ShReport(txt_pz_blh.Text.Trim(), Program.User_Code, Program.User_Name);
                        if (ZXResult == true)
                        {
                            //更新检查状态
                            insM.UpdateExam_Status(txt_pz_blh.Text.Trim(), "50");
                            Frm_TJInfo("提示", "报告审核成功！");
                            txt_bl_shys.Text = Program.User_Name;

                            //刷新按钮状态
                            status = insM.GetStudyExam_Status(txt_pz_blh.Text.Trim());
                            ControlBtn(status, txt_pz_blh.Text.Trim());
                            //刷新列表
                            superTabControl2.SelectedTabIndex = 0;
                            QueryBtn_Click(null, null);
                        }
                        else
                        {
                            Frm_TJInfo("审核失败", "请联系系统管理员！");
                        }
                    }
                    else
                    {
                        Frm_TJInfo("提示", "当前报告已经完成审核！");
                    }

                }
                catch (Exception ex)
                {
                    Frm_TJInfo("审核失败", ex.ToString());
                    Program.FileLog.Error("审核报告失败：" + ex.ToString());
                    return;
                }
            }
        }
        //修改
        private void buttonItem7_Click(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                //查询报告状态
                DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                int status = insM.GetStudyExam_Status(txt_pz_blh.Text.Trim());

                if (status >= 55)
                {
                    //只能是自己或更高级别的人进行修改
                    DBHelper.BLL.exam_report InsR = new DBHelper.BLL.exam_report();
                    DataTable dt = InsR.GetReportYsInfo(txt_pz_blh.Text.Trim());
                    if (dt != null && dt.Rows.Count == 1)
                    {
                        DBHelper.BLL.sys_user InsU = new DBHelper.BLL.sys_user();
                        int CurUserRoleCode = InsU.GetUserRoleCode(dt.Rows[0]["cbreport_doc_code"].ToString());
                        if (Program.User_Code.Equals(dt.Rows[0]["cbreport_doc_code"].ToString()) || Program.User_Code.Equals(dt.Rows[0]["zzreport_doc_code"].ToString()) || Program.User_Code.Equals(dt.Rows[0]["shreport_doc_code"].ToString()) || CurUserRoleCode < Program.user_role_code)
                        {

                            eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                            TaskDialog.EnableGlass = false;

                            if (TaskDialog.Show("修改已经打印过的报告", "确认", "确定要修改这个报告么？", Curbutton) == eTaskDialogResult.Ok)
                            {
                                //更新检查状态
                                insM.UpdateExam_Status(txt_pz_blh.Text.Trim(), "50");
                                //原先报告内容写入历史报告表
                                if (InsR.SaveExam_history_report(txt_pz_blh.Text.Trim(), Program.User_Code, Program.User_Name))
                                {
                                    //刷新按钮状态
                                    status = insM.GetStudyExam_Status(txt_pz_blh.Text.Trim());
                                    ControlBtn(status, txt_pz_blh.Text.Trim());
                                    Frm_TJInfo("操作成功", "报告当前已经可编辑！");
                                    //刷新列表
                                    superTabControl2.SelectedTabIndex = 0;
                                    QueryBtn_Click(null, null);
                                }
                            }
                        }
                        else
                        {
                            Frm_TJInfo("权限不够", "您没有权限修改此报告！");
                        }
                    }
                }
            }
        }
        //预览
        private void buttonItem8_Click(object sender, EventArgs e)
        {
            if (superTabControl1.SelectedTabIndex != 3)
            {
                superTabControl1.SelectedTabIndex = 3;
            }
            else
            {
                RefreshPreviewReport();
            }
        }
        //打印
        private void buttonItem9_Click(object sender, EventArgs e)
        {
            buttonX5_Click(null, null);
        }
        //发送给主治医师
        private void buttonItem10_Click(object sender, EventArgs e)
        {
            Frm_Zzys ins = new Frm_Zzys();
            ins.BringToFront();
            ins.Owner = this;
            ins.study_no = txt_pz_blh.Text.Trim();
            if (ins.ShowDialog() == DialogResult.OK)
            {
                this.comboBoxEx2.Text = ins.Zyzy_Name;
                Frm_TJInfo("报告已发送", "报告已发送到指定主治医师！");
            }
        }
        //收藏
        private void buttonItem11_Click(object sender, EventArgs e)
        {
            Frm_SC_Report ins = new Frm_SC_Report();
            ins.BringToFront();
            ins.Owner = this;
            ins.study_no = txt_pz_blh.Text.Trim();
            ins.gjc = "";
            ins.zdbm = txt_ICD10.Text.Trim();
            if (ins.ShowDialog() == DialogResult.OK)
            {
                Frm_TJInfo("报告已收藏", "报告收藏成功！");
            }

        }
        //取材信息
        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                Boolean Refresh_flag = false;
                //检查是否状态小于25（已取材）
                DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                int status = insM.GetStudyExam_Status(txt_pz_blh.Text.Trim());
                if (status < 25 && status >= 20)
                {
                    Refresh_flag = false;
                    return;
                }
                else if (status >= 25)
                {
                    Refresh_flag = true;
                }
                //展示当前病理号下的所有取材信息
                DBHelper.BLL.exam_draw_meterials insQc = new DBHelper.BLL.exam_draw_meterials();
                DataTable dtqc = insQc.GetDtHdMeterials(txt_pz_blh.Text.Trim());
                if (dtqc != null && dtqc.Rows.Count > 0)
                {
                    btn_QC.TextColor = Color.Blue;
                }
                else
                {
                    btn_QC.TextColor = CurBtnColor;
                }
                Form Frm_QcxxIns = Application.OpenForms["Frm_Qcxx"];
                if (Frm_QcxxIns != null)
                {
                    Frm_QcxxIns.Owner = this;
                    Frm_Qcxx.Refresh_Flag = Refresh_flag;
                    Frm_Qcxx.BLH = txt_pz_blh.Text.Trim();
                    Frm_Qcxx.sqd = txt_sqd.Text.Trim();
                    Frm_QcxxIns.BringToFront();
                    Frm_QcxxIns.WindowState = FormWindowState.Normal;
                }
                else
                {
                    Frm_QcxxIns = new Frm_Qcxx();
                    Frm_QcxxIns.WindowState = FormWindowState.Normal;
                    Frm_Qcxx.Refresh_Flag = Refresh_flag;
                    Frm_Qcxx.BLH = txt_pz_blh.Text.Trim();
                    Frm_Qcxx.sqd = txt_sqd.Text.Trim();
                    Frm_QcxxIns.BringToFront();
                    Frm_QcxxIns.Show();
                }
            }
        }
        //冰冻报告
        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {

                Frm_ice_report Frm_ice_reportIns = new Frm_ice_report();
                Frm_ice_reportIns.Owner = this;
                Frm_ice_reportIns.BLH = txt_pz_blh.Text.Trim();
                Frm_ice_reportIns.exam_no = txt_sqd.Text.Trim();
                Frm_ice_reportIns.BringToFront();
                Frm_ice_reportIns.ShowDialog();
                //冰冻报告
                DBHelper.BLL.exam_ice_report insBd = new DBHelper.BLL.exam_ice_report();
                DataTable dtBd = insBd.GetData(txt_pz_blh.Text.Trim());
                if (dtBd != null && dtBd.Rows.Count == 1)
                {
                    btn_BD.TextColor = Color.Blue;
                }
                else
                {
                    btn_BD.TextColor = CurBtnColor;
                }
            }
        }
        //切片信息
        private void buttonX11_Click(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                int status = insM.GetStudyExam_Status(txt_pz_blh.Text.Trim());
                if (status < 30)
                {
                    Frm_TJInfo("提示", "切片还都未审核，不能查看！");
                    return;
                }

                //展示当前病理号下的所有制片信息
                DBHelper.BLL.exam_filmmaking insZp = new DBHelper.BLL.exam_filmmaking();
                DataTable dtzp = insZp.GetDtFilmMakingPj(txt_pz_blh.Text.Trim());
                if (dtzp != null && dtzp.Rows.Count > 0)
                {
                    btn_ZP.TextColor = Color.Blue;
                }
                else
                {
                    btn_ZP.TextColor = CurBtnColor;
                }

                Form Frm_QpxxIns = Application.OpenForms["Frm_Qpxx"];
                if (Frm_QpxxIns != null)
                {
                    Frm_QpxxIns.Owner = this;
                    Frm_Qpxx.CurBLH = txt_pz_blh.Text.Trim();
                    Frm_QpxxIns.BringToFront();
                    Frm_QpxxIns.WindowState = FormWindowState.Normal;
                }
                else
                {
                    Frm_QpxxIns = new Frm_Qpxx();
                    Frm_QpxxIns.WindowState = FormWindowState.Normal;
                    Frm_Qpxx.CurBLH = txt_pz_blh.Text.Trim();
                    Frm_QpxxIns.BringToFront();
                    Frm_QpxxIns.Show();
                }
            }
        }
        //技术医嘱
        private void buttonX9_Click(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {

                Frm_jsyz Frm_jsyzIns = new Frm_jsyz();
                Frm_jsyzIns.BLH = txt_pz_blh.Text.Trim();
                Frm_jsyzIns.SQD = txt_sqd.Text.Trim();
                Frm_jsyzIns.BRXM = txt_pz_xm.Text.Trim();
                Frm_jsyzIns.Owner = this;
                Frm_jsyzIns.BringToFront();
                Frm_jsyzIns.ShowDialog();
                //展示技术医嘱信息
                DBHelper.BLL.exam_jsyz insjsyz = new DBHelper.BLL.exam_jsyz();
                DataTable dtjsyz = insjsyz.GetData(txt_pz_blh.Text.Trim());
                if (dtjsyz != null && dtjsyz.Rows.Count > 0)
                {
                    btn_jsyz.TextColor = Color.Blue;
                }
                else
                {
                    btn_jsyz.TextColor = CurBtnColor;
                }
            }
        }
        //特检医嘱
        private void buttonX10_Click(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {

                Frm_tjyz Frm_tjyzIns = new Frm_tjyz();
                Frm_tjyzIns.BLH = txt_pz_blh.Text.Trim();
                Frm_tjyzIns.SQD = txt_sqd.Text.Trim();
                Frm_tjyzIns.BRXM = txt_pz_xm.Text.Trim();
                Frm_tjyzIns.Owner = this;
                Frm_tjyzIns.BringToFront();
                Frm_tjyzIns.ShowDialog();
                //展示特检医嘱信息
                DBHelper.BLL.exam_tjyz instjyz = new DBHelper.BLL.exam_tjyz();
                DataTable dttjyz = instjyz.GetDataTjyz(txt_pz_blh.Text.Trim());
                if (dttjyz != null && dttjyz.Rows.Count > 0)
                {
                    btn_tjyz.TextColor = Color.Blue;
                }
                else
                {
                    btn_tjyz.TextColor = CurBtnColor;
                }
            }
        }

        #endregion

        #region"快捷键操作"
        public bool IgnoreHotkeys = false;
        private void Frm_Dtcj_HotkeyPress(ushort id, Keys key, Modifiers modifier)
        {
            if (btn_PZ.Enabled == true)
            {
                if (!IgnoreHotkeys)
                {
                    if (this.Tag.Equals("show"))
                    {
                        switch (key)
                        {
                            case Keys.F4:
                                //添加标本信息快捷键
                                btn_PZ_Click(null, null);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                Frm_TJInfo("采图提示", "请先打开摄像头开关！");
            }
        }
        //注册热键
        HotkeyInfo hotkey_bbxx;

        //移除热键
        private void FrmBlzd_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (hotkey_bbxx != null)
            {
                if (hotkey_bbxx.Status == HotkeyStatus.Registered)
                {
                    UnregisterHotkey(hotkey_bbxx);
                }
            }
        }

        #endregion

        #region "热键操作"
        public int HotkeyRepeatLimit { get; set; }

        public delegate void HotkeyEventHandler(ushort id, Keys key, Modifiers modifier);

        public event HotkeyEventHandler HotkeyPress;

        private Stopwatch repeatLimitTimer;

        public static string GetUniqueID()
        {
            return Guid.NewGuid().ToString("N");
        }
        public void RegisterHotkey(HotkeyInfo hotkeyInfo)
        {
            if (hotkeyInfo != null && hotkeyInfo.Status != HotkeyStatus.Registered)
            {
                if (!hotkeyInfo.IsValidHotkey)
                {
                    hotkeyInfo.Status = HotkeyStatus.NotConfigured;
                    return;
                }

                if (hotkeyInfo.ID == 0)
                {
                    string uniqueID = GetUniqueID();
                    hotkeyInfo.ID = NativeMethods.GlobalAddAtom(uniqueID);

                    if (hotkeyInfo.ID == 0)
                    {

                        hotkeyInfo.Status = HotkeyStatus.Failed;
                        return;
                    }
                }

                if (!NativeMethods.RegisterHotKey(Handle, hotkeyInfo.ID, (uint)hotkeyInfo.ModifiersEnum, (uint)hotkeyInfo.KeyCode))
                {
                    NativeMethods.GlobalDeleteAtom(hotkeyInfo.ID);

                    hotkeyInfo.ID = 0;
                    hotkeyInfo.Status = HotkeyStatus.Failed;
                    return;
                }

                hotkeyInfo.Status = HotkeyStatus.Registered;
            }
        }

        public bool UnregisterHotkey(HotkeyInfo hotkeyInfo)
        {
            if (hotkeyInfo != null)
            {
                if (hotkeyInfo.ID > 0)
                {
                    bool result = NativeMethods.UnregisterHotKey(Handle, hotkeyInfo.ID);

                    if (result)
                    {
                        NativeMethods.GlobalDeleteAtom(hotkeyInfo.ID);
                        hotkeyInfo.ID = 0;
                        hotkeyInfo.Status = HotkeyStatus.NotConfigured;
                        return true;
                    }
                }

                hotkeyInfo.Status = HotkeyStatus.Failed;
            }

            return false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WindowsMessages.HOTKEY && CheckRepeatLimitTime())
            {
                ushort id = (ushort)m.WParam;
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                Modifiers modifier = (Modifiers)((int)m.LParam & 0xFFFF);
                OnKeyPressed(id, key, modifier);
                return;
            }

            base.WndProc(ref m);
        }

        protected void OnKeyPressed(ushort id, Keys key, Modifiers modifier)
        {
            if (HotkeyPress != null)
            {
                HotkeyPress(id, key, modifier);
            }
        }

        private bool CheckRepeatLimitTime()
        {
            if (HotkeyRepeatLimit > 0)
            {
                if (repeatLimitTimer.ElapsedMilliseconds >= HotkeyRepeatLimit)
                {
                    repeatLimitTimer.Reset();
                    repeatLimitTimer.Start();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region "报告书写"

        //编辑报告模板
        private void buttonX14_Click_1(object sender, EventArgs e)
        {
            if (superTabControl1.SelectedTabIndex != 3)
            {
                superTabControl1.SelectedTabIndex = 3;
            }
            else
            {
                RefreshPreviewReport();
            }
            this.reportViewer1.EditReport();
        }


        public void InitTabControl()
        {
            buttonX10.Visible = false;
            buttonX14.Visible = false;
            for (int i = 0; i < this.tabControl1.Tabs.Count; i++)
            {
                tabControl1.Tabs[i].Visible = false;
            }
        }

        //对公共变量进行初始化
        public void InitReportIns()
        {

        }
        private void comboBoxItem1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxItem1.SelectedIndex == -1)
            {
                return;
            }
            int tab_index = Convert.ToInt32(((DevComponents.Editors.ComboItem)(comboBoxItem1.SelectedItem)).Value);
            InitTabControl();
            report_type = tab_index;
            if (report_type == 0 || report_type == 3)
            {
                labelX1.Text = "光镜所见：";
            }
            else
            {
                labelX1.Text = "治疗建议：";
            }
            if (tab_index == 0)
            {
                buttonX10.Visible = true;
                buttonX14.Visible = true;
                if (!tabItem1.Visible)
                {
                    tabItem1.Visible = true;
                }
            }
            else if (tab_index == 3)
            {
                if (!this.tabItem4.Visible)
                {
                    tabItem4.Visible = true;
                }
            }
            else if (tab_index == 5)
            {
                if (!this.tabWyHZControl.Visible)
                {
                    tabWyHZControl.Visible = true;
                }
            }
            InitReportIns();
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                //获取检查状态(展示报告信息)
                DBHelper.BLL.exam_master insStatus = new DBHelper.BLL.exam_master();
                int status = insStatus.GetStudyExam_Status(txt_pz_blh.Text.Trim());
                //按钮控制
                ControlBtn(status, txt_pz_blh.Text.Trim());

                if (status < 40)
                {
                    //根据病理号获取申请单号
                    string exam_no = insStatus.GetExam_No(txt_pz_blh.Text.Trim());
                    DBHelper.BLL.exam_specimens InsSpec = new DBHelper.BLL.exam_specimens();
                    DataTable dtSpec = InsSpec.GetBGSpecimensDTQC(exam_no);
                    //检查部位
                    if (dtSpec != null && dtSpec.Rows.Count > 0)
                    {
                        string StrParts = "";
                        for (int i = 0; i <= dtSpec.Rows.Count - 1; i++)
                        {
                            if (StrParts.Equals(""))
                            {
                                StrParts = dtSpec.Rows[i]["parts"].ToString();
                            }
                            else
                            {
                                StrParts += "," + dtSpec.Rows[i]["parts"].ToString();
                            }
                        }
                        textBoxX8.Text = StrParts;
                    }
                }
                else if (status >= 40)
                {
                    DataTable dt = insStatus.SelectParts(txt_pz_blh.Text.Trim());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        textBoxX8.Text = dt.Rows[0]["parts"].ToString();
                    }
                }

            }
            superTabControl2.SelectedTabIndex = 0;
        }
        //获取选中的图像
        public string GetBgImage()
        {
            StringBuilder sb = new StringBuilder();
            int icount = 0;
            for (int i = 0; i < imageListView1.Items.Count; i++)
            {
                if (imageListView1.Items[i].Checked == true)
                {
                    sb.Append(imageListView1.Items[i].Tag);
                    sb.Append("|");
                    icount += 1;
                }
                if (icount >= 2)
                {
                    break;
                }
            }
            string str = sb.ToString();
            if (icount == 0)
            {
                if (imageListView1.Items.Count > 0)
                {
                    for (int i = 0; i < imageListView1.Items.Count; i++)
                    {
                        sb.Append(imageListView1.Items[i].Tag);
                        sb.Append("|");
                        if (i >= 1)
                        {
                            break;
                        }
                    }
                    str = sb.ToString();
                    ReportImageStr = str.Substring(0, str.Length - 1);
                }
                else
                {
                    ReportImageStr = "";
                    return "";
                }
            }
            else
            {
                ReportImageStr = str.Substring(0, str.Length - 1);
            }
            return ReportImageStr;
        }
        //保存报告
        private Boolean SaveReport()
        {
            Boolean saveresult = false;
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                //
                DBHelper.BLL.exam_master insMas = new DBHelper.BLL.exam_master();
                DBHelper.Model.exam_report InsM = new DBHelper.Model.exam_report();
                InsM.lk_num = 1;
                InsM.bp_num = 1;
                InsM.report_gb_doc = cmb_gpys.Text.Trim();
                InsM.wy_study_no = "";
                //保存送检部位
                if (!this.textBoxX8.Text.Trim().Equals(""))
                {
                    insMas.UpdateParts(txt_pz_blh.Text.Trim(), textBoxX8.Text.Trim());
                }
                else
                {
                    //送检部位
                    string strBw = "";
                    DBHelper.BLL.exam_specimens InsSpec = new DBHelper.BLL.exam_specimens();
                    DataTable dtSpec = InsSpec.GetBGSpecimensDTQC(txt_sqd.Text.Trim());
                    if (dtSpec != null)
                    {
                        for (int i = 0; i <= dtSpec.Rows.Count - 1; i++)
                        {
                            strBw = strBw + dtSpec.Rows[i]["parts"].ToString() + " ";
                        }
                        insMas.UpdateParts(txt_pz_blh.Text.Trim(), strBw.Trim());
                    }
                }
                if (report_type == 0)
                {
                    if (!txt_rysj.Text.Trim().Equals(""))
                    {

                        try
                        {
                            InsM.study_no = txt_pz_blh.Text.Trim();
                            InsM.zdyj = txt_zdyj.Text.Trim();
                            InsM.rysj = txt_rysj.Text.Trim();
                            InsM.xbms = "";
                            InsM.report_childtmp_index = "0";
                            GetBgImage();
                            InsM.image = ReportImageStr;
                            InsM.cbreport_doc_code = comboBoxEx1.SelectedValue.ToString();
                            InsM.cbreport_doc_name = comboBoxEx1.Text.Trim();
                            InsM.zzreport_doc_code = comboBoxEx2.SelectedValue.ToString();
                            InsM.zzreport_doc_name = comboBoxEx2.Text.Trim();
                            InsM.cbreport_datetime = report_bl_datetime.Value.ToString("yyyy-MM-dd HH:mm:ss").Trim();
                            InsM.lock_flag = "1";
                            InsM.zdpz = txt_bl_zdpz.Text.Trim();
                            InsM.sfyx = ((DevComponents.Editors.ComboItem)(cmb_bl_sfyx.SelectedItem)).Text.Trim();
                            InsM.zdbm = txt_ICD10.Text.Trim();
                            InsM.zdkey = "";
                            InsM.lcfh = txt_lcfh.Text.Trim();
                            DBHelper.BLL.exam_report insBll = new DBHelper.BLL.exam_report();
                            Boolean ZXResult = insBll.SaveExam_Report(InsM, report_type);
                            if (ZXResult == true)
                            {
                                //更新报告状态
                                int status = insMas.GetStudyExam_Status(InsM.study_no);
                                //小于初步报告状态则更新
                                if (status >= 20 && status < 40)
                                {
                                    insMas.UpdateSaveReport_Status(InsM.study_no, "40", Program.User_Code);
                                }
                                saveresult = true;
                            }
                            else
                            {
                                saveresult = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.FileLog.Error("组织学报告保存失败：" + ex.ToString());
                            saveresult = false;
                        }

                    }
                    else
                    {
                        Frm_TJInfo("提示", "请输入肉眼所见！");
                    }
                }
                else if (report_type == 1)
                {
                    try
                    {
                        InsM.study_no = txt_pz_blh.Text.Trim();

                        InsM.report_childtmp_index = "0";
                        GetBgImage();
                        InsM.image = ReportImageStr;
                        InsM.cbreport_doc_code = comboBoxEx1.SelectedValue.ToString();
                        InsM.cbreport_doc_name = comboBoxEx1.Text.Trim();
                        InsM.zzreport_doc_code = comboBoxEx2.SelectedValue.ToString();
                        InsM.zzreport_doc_name = comboBoxEx2.Text.Trim();
                        InsM.cbreport_datetime = report_bl_datetime.Value.ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        InsM.lock_flag = "1";
                        InsM.zdyj = "";
                        InsM.zdpz = txt_bl_zdpz.Text.Trim();
                        InsM.sfyx = ((DevComponents.Editors.ComboItem)(cmb_bl_sfyx.SelectedItem)).Text.Trim();
                        InsM.zdbm = txt_ICD10.Text.Trim();
                        InsM.zdkey = "";
                        InsM.lcfh = txt_lcfh.Text.Trim();
                        DBHelper.BLL.exam_report insBll = new DBHelper.BLL.exam_report();
                        Boolean ZXResult = insBll.SaveExam_Report(InsM, report_type);
                        if (ZXResult == true)
                        {
                            //更新报告状态
                            int status = insMas.GetStudyExam_Status(InsM.study_no);
                            //小于初步报告状态则更新
                            if (status >= 20 && status < 40)
                            {
                                insMas.UpdateSaveReport_Status(InsM.study_no, "40", Program.User_Code);
                            }
                            saveresult = true;
                        }
                        else
                        {
                            saveresult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("妇科细胞学类型报告保存失败：" + ex.ToString());
                        saveresult = false;
                    }
                }
                else if (report_type == 2)
                {
                    try
                    {
                        InsM.study_no = txt_pz_blh.Text.Trim();

                        InsM.report_childtmp_index = "0";
                        GetBgImage();
                        InsM.image = ReportImageStr;
                        InsM.cbreport_doc_code = comboBoxEx1.SelectedValue.ToString();
                        InsM.cbreport_doc_name = comboBoxEx1.Text.Trim();
                        InsM.zzreport_doc_code = comboBoxEx2.SelectedValue.ToString();
                        InsM.zzreport_doc_name = comboBoxEx2.Text.Trim();
                        InsM.cbreport_datetime = report_bl_datetime.Value.ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        InsM.lock_flag = "1";
                        InsM.zdpz = txt_bl_zdpz.Text.Trim();
                        InsM.sfyx = ((DevComponents.Editors.ComboItem)(cmb_bl_sfyx.SelectedItem)).Text.Trim();
                        InsM.zdbm = txt_ICD10.Text.Trim();
                        InsM.zdkey = "";
                        InsM.lcfh = txt_lcfh.Text.Trim();
                        DBHelper.BLL.exam_report insBll = new DBHelper.BLL.exam_report();
                        Boolean ZXResult = insBll.SaveExam_Report(InsM, report_type);
                        if (ZXResult == true)
                        {
                            //更新报告状态
                            int status = insMas.GetStudyExam_Status(InsM.study_no);
                            //小于初步报告状态则更新
                            if (status >= 20 && status < 40)
                            {
                                insMas.UpdateSaveReport_Status(InsM.study_no, "40", Program.User_Code);
                            }
                            saveresult = true;
                        }
                        else
                        {
                            saveresult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("非妇科细胞学类型报告保存失败：" + ex.ToString());
                        saveresult = false;
                    }
                }
                else if (report_type == 3)
                {
                    try
                    {
                        InsM.study_no = txt_pz_blh.Text.Trim();
                        InsM.zdyj = txt_xbxXfs.Text.Trim();
                        InsM.xbms = "";
                        string strImage = GetBgImage();
                        int report_tmp_type = 0;
                        InsM.report_childtmp_index = report_tmp_type.ToString();
                        InsM.image = ReportImageStr;
                        InsM.cbreport_doc_code = comboBoxEx1.SelectedValue.ToString();
                        InsM.cbreport_doc_name = comboBoxEx1.Text.Trim();
                        InsM.zzreport_doc_code = comboBoxEx2.SelectedValue.ToString();
                        InsM.zzreport_doc_name = comboBoxEx2.Text.Trim();
                        InsM.cbreport_datetime = report_bl_datetime.Value.ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        InsM.lock_flag = "1";
                        InsM.zdpz = txt_bl_zdpz.Text.Trim();
                        InsM.sfyx = ((DevComponents.Editors.ComboItem)(cmb_bl_sfyx.SelectedItem)).Text.Trim();
                        InsM.zdbm = txt_ICD10.Text.Trim();
                        InsM.zdkey = "";
                        InsM.lcfh = txt_lcfh.Text.Trim();
                        DBHelper.BLL.exam_report insBll = new DBHelper.BLL.exam_report();
                        Boolean ZXResult = insBll.SaveExam_Report(InsM, report_type);
                        if (ZXResult == true)
                        {
                            //更新报告状态
                            int status = insMas.GetStudyExam_Status(InsM.study_no);
                            //小于初步报告状态则更新
                            if (status >= 20 && status < 40)
                            {
                                insMas.UpdateSaveReport_Status(InsM.study_no, "40", Program.User_Code);
                            }
                            saveresult = true;
                        }
                        else
                        {
                            saveresult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("胸腹水类型报告保存失败：" + ex.ToString());
                        saveresult = false;
                    }
                }
                else if (report_type == 4)
                {
                    try
                    {
                        InsM.study_no = txt_pz_blh.Text.Trim();

                        GetBgImage();
                        InsM.image = ReportImageStr;
                        InsM.cbreport_doc_code = comboBoxEx1.SelectedValue.ToString();
                        InsM.cbreport_doc_name = comboBoxEx1.Text.Trim();
                        InsM.zzreport_doc_code = comboBoxEx2.SelectedValue.ToString();
                        InsM.zzreport_doc_name = comboBoxEx2.Text.Trim();
                        InsM.cbreport_datetime = report_bl_datetime.Value.ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        InsM.lock_flag = "1";
                        InsM.zdpz = txt_bl_zdpz.Text.Trim();
                        InsM.sfyx = ((DevComponents.Editors.ComboItem)(cmb_bl_sfyx.SelectedItem)).Text.Trim();
                        InsM.zdbm = txt_ICD10.Text.Trim();
                        InsM.zdkey = "";
                        InsM.lcfh = txt_lcfh.Text.Trim();
                        DBHelper.BLL.exam_report insBll = new DBHelper.BLL.exam_report();
                        Boolean ZXResult = insBll.SaveExam_Report(InsM, report_type);
                        if (ZXResult == true)
                        {
                            //更新报告状态
                            int status = insMas.GetStudyExam_Status(InsM.study_no);
                            //小于初步报告状态则更新
                            if (status >= 20 && status < 40)
                            {
                                insMas.UpdateSaveReport_Status(InsM.study_no, "40", Program.User_Code);
                            }
                            saveresult = true;
                        }
                        else
                        {
                            saveresult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("HPV类型报告保存失败：" + ex.ToString());
                        saveresult = false;
                    }
                }
                else if (report_type == 5)
                {
                    if (!txt_Wy_zdyj.Text.Trim().Equals(""))
                    {
                        try
                        {
                            InsM.study_no = txt_pz_blh.Text.Trim();
                            InsM.zdyj = txt_Wy_zdyj.Text.Trim();
                            InsM.rysj = "";
                            InsM.xbms = "";
                            InsM.report_childtmp_index = "0";
                            GetBgImage();
                            InsM.image = ReportImageStr;
                            InsM.cbreport_doc_code = comboBoxEx1.SelectedValue.ToString();
                            InsM.cbreport_doc_name = comboBoxEx1.Text.Trim();
                            InsM.zzreport_doc_code = comboBoxEx2.SelectedValue.ToString();
                            InsM.zzreport_doc_name = comboBoxEx2.Text.Trim();
                            InsM.cbreport_datetime = report_bl_datetime.Value.ToString("yyyy-MM-dd HH:mm:ss").Trim();
                            InsM.lock_flag = "1";
                            InsM.zdpz = txt_bl_zdpz.Text.Trim();
                            InsM.sfyx = ((DevComponents.Editors.ComboItem)(cmb_bl_sfyx.SelectedItem)).Text.Trim();
                            InsM.zdbm = txt_ICD10.Text.Trim();
                            InsM.zdkey = "";
                            InsM.lcfh = txt_lcfh.Text.Trim();
                            InsM.lk_num = integerInput1.Value;
                            InsM.bp_num = integerInput2.Value;
                            InsM.wy_study_no = txt_wyblh.Text.Trim();
                            DBHelper.BLL.exam_report insBll = new DBHelper.BLL.exam_report();
                            Boolean ZXResult = insBll.SaveExam_Report(InsM, report_type);
                            if (ZXResult == true)
                            {
                                //更新报告状态
                                int status = insMas.GetStudyExam_Status(InsM.study_no);
                                //小于初步报告状态则更新
                                if (status >= 20 && status < 40)
                                {
                                    insMas.UpdateSaveReport_Status(InsM.study_no, "40", Program.User_Code);
                                }
                                //更新送检单位
                                insMas.UpdateSubmitDw(txt_sjdw.Text.Trim(), InsM.study_no);
                                saveresult = true;
                            }
                            else
                            {
                                saveresult = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            Program.FileLog.Error("外院会诊报告保存失败：" + ex.ToString());
                            saveresult = false;
                        }

                    }
                    else
                    {
                        Frm_TJInfo("提示", "请输入诊断意见！");
                    }
                }
                else if (report_type == 6)
                {
                    try
                    {
                        InsM.study_no = txt_pz_blh.Text.Trim();

                        GetBgImage();
                        InsM.image = ReportImageStr;
                        InsM.cbreport_doc_code = comboBoxEx1.SelectedValue.ToString();
                        InsM.cbreport_doc_name = comboBoxEx1.Text.Trim();
                        InsM.zzreport_doc_code = comboBoxEx2.SelectedValue.ToString();
                        InsM.zzreport_doc_name = comboBoxEx2.Text.Trim();
                        InsM.cbreport_datetime = report_bl_datetime.Value.ToString("yyyy-MM-dd HH:mm:ss").Trim();
                        InsM.lock_flag = "1";
                        InsM.zdpz = txt_bl_zdpz.Text.Trim();
                        InsM.sfyx = ((DevComponents.Editors.ComboItem)(cmb_bl_sfyx.SelectedItem)).Text.Trim();
                        InsM.zdbm = txt_ICD10.Text.Trim();
                        InsM.zdkey = "";
                        InsM.lcfh = txt_lcfh.Text.Trim();
                        DBHelper.BLL.exam_report insBll = new DBHelper.BLL.exam_report();
                        Boolean ZXResult = insBll.SaveExam_Report(InsM, report_type);
                        if (ZXResult == true)
                        {
                            //更新报告状态
                            int status = insMas.GetStudyExam_Status(InsM.study_no);
                            //小于初步报告状态则更新
                            if (status >= 20 && status < 40)
                            {
                                insMas.UpdateSaveReport_Status(InsM.study_no, "40", Program.User_Code);
                            }
                            saveresult = true;
                        }
                        else
                        {
                            saveresult = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("分子病理报告保存失败：" + ex.ToString());
                        saveresult = false;
                    }
                }
                //更新
                insMas.UpdateMasBgnrFlag(cmbBgnr.Text.Trim(), cmbBbqc.Text.Trim(), txt_pz_blh.Text.Trim());

                DBHelper.BLL.exam_report insHisBll = new DBHelper.BLL.exam_report();
                //插入报告痕迹表
                insHisBll.SaveHis_Report(InsM, Program.User_Code, Program.User_Name);
            }
            else
            {
                Frm_TJInfo("提示", "请先选择一个检查！");
            }
            return saveresult;
        }
        //病理诊断保存
        private void buttonX14_Click(object sender, EventArgs e)
        {
            if (SaveReport())
            {
                comboBoxItem1.Enabled = false;
                //刷新按钮状态
                DBHelper.BLL.exam_master insMas = new DBHelper.BLL.exam_master();
                int status = insMas.GetStudyExam_Status(txt_pz_blh.Text.Trim());
                ControlBtn(status, txt_pz_blh.Text.Trim());
                Frm_TJInfo("保存报告成功！", "");
                //必须切换才刷新
                superTabControl2.SelectedTabIndex = 0;
                //刷新列表
                QueryBtn_Click(null, null);
            }
            else
            {
                Frm_TJInfo("保存报告失败！", "");
            }
        }
        //病理报告打印
        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (txt_pz_blh.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "请先选择一个病人！");
                return;
            }
            //获取选中的图像
            GetBgImage();
            //提取当前关键信息
            string study_no = txt_pz_blh.Text.Trim();
            string exam_no = txt_sqd.Text.Trim();
            //
            btn_print1.Enabled = false;
            btn_print.Enabled = false;
            //打印时执行一次保存
            SaveReport();
            //报告图片格式图像路径
            string PathImg = Program.APPdirPath;
            string filename = string.Format("Report_{0}", txt_pz_blh.Text.Trim());
            string PathReportImg = ReportFolder + @"\" + txt_pz_blh.Text.Trim();
            if (Directory.Exists(PathReportImg) == false)
            {
                Directory.CreateDirectory(PathReportImg);
            }
            string strFileName = string.Format(@"{0}\{1}.jpg", PathReportImg, filename);
            //打印报告执行状态
            Boolean Print_Flag = false;
            //更新报告状态
            DBHelper.BLL.exam_master insMas = new DBHelper.BLL.exam_master();
            int status = insMas.GetStudyExam_Status(txt_pz_blh.Text.Trim());
            if (status >= 40)
            {
                if (report_type == 0)
                {
                    try
                    {
                        if (superTabControl1.SelectedTabIndex == 3)
                        {
                            //预览打印
                            Print_Flag = this.reportViewer1.PrintReport(@strFileName);
                        }
                        else
                        {
                            //直接打印
                            //报告
                            if (superGridControl1.ActiveRow == null)
                            {
                                return;
                            }
                            int index = superGridControl1.ActiveRow.Index;
                            if (index == -1)
                            {
                                return;
                            }
                            GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                            //病理学报告单内容对象
                            FastReportLib.BLReportParas InsBLReport = new FastReportLib.BLReportParas();
                            InsBLReport.InitParasDate();
                            InsBLReport.ReportParaHospital = Program.HospitalName;
                            InsBLReport.study_no = Row.Cells["study_no"].Value.ToString();
                            InsBLReport.Txt_Name = Row.Cells["patient_name"].Value.ToString();
                            InsBLReport.Txt_Sex = Row.Cells["sex"].Value.ToString();
                            InsBLReport.Txt_Age = Row.Cells["age"].Value.ToString();
                            InsBLReport.Txt_ly = Row.Cells["patient_source"].Value.ToString();
                            if (!Lab_WtzdFlag.Tag.Equals("1"))
                            {
                                InsBLReport.wtzd_flag = "0";
                            }
                            else
                            {
                                InsBLReport.wtzd_flag = "1";
                            }
                            if (InsBLReport.Txt_ly.Equals("外来"))
                            {
                                InsBLReport.Txt_Sjks = Row.Cells["submit_unit"].Value.ToString();
                            }
                            else
                            {
                                InsBLReport.Txt_Sjks = Row.Cells["req_dept"].Value.ToString();
                            }
                            InsBLReport.Txt_Bf = Row.Cells["ward"].Value.ToString();
                            InsBLReport.Txt_Ch = Row.Cells["bed_no"].Value.ToString();
                            InsBLReport.Txt_Zyh = Row.Cells["input_id"].Value.ToString();
                            InsBLReport.Txt_Sjys = Row.Cells["req_physician"].Value.ToString();
                            InsBLReport.Txt_Sqrq = Row.Cells["received_datetime"].Value.ToString().Substring(0, 10);
                            InsBLReport.Txt_mzh = Row.Cells["output_id"].Value.ToString();
                            InsBLReport.Txt_Rysj = txt_rysj.Text.Trim();
                            InsBLReport.Txt_Blzd = txt_zdyj.Text.Trim();
                            InsBLReport.txt_Bgrq = report_bl_datetime.Value.ToString("yyyy-MM-dd");
                            InsBLReport.Txt_Bgys = comboBoxEx1.Text.Trim();
                            InsBLReport.Txt_fjys = comboBoxEx2.Text.Trim();
                            InsBLReport.fileImages = ReportImageStr;
                            InsBLReport.Txt_zdpz = txt_bl_zdpz.Text.Trim();
                            InsBLReport.report_gb_doc = cmb_gpys.Text.Trim();
                            InsBLReport.Txt_Sjdw = Row.Cells["submit_unit"].Value.ToString();
                            //送检部位
                            InsBLReport.Txt_Sjbw = textBoxX8.Text.Trim();
                            //PId
                            if (Row.Cells["patient_id"].Value.ToString().IndexOf("New") == -1)
                            {
                                InsBLReport.Txt_Pid = Row.Cells["patient_id"].Value.ToString();
                            }
                            else
                            {
                                InsBLReport.Txt_Pid = "";
                            }
                            Print_Flag = this.reportViewer1.DirectPrintBLReport(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, InsBLReport, PathImg, @strFileName, new Font(this.txt_rysj.Font.Name, Convert.ToSingle(this.ComboBox2.Text)), new Font(this.txt_zdyj.Font.Name, Convert.ToSingle(this.comboBox1.Text)));
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
                        if (superTabControl1.SelectedTabIndex == 3)
                        {
                            //预览打印
                            Print_Flag = this.reportViewer1.PrintReport(@strFileName);
                        }
                        else
                        {
                            //直接打印
                            if (superGridControl1.ActiveRow == null)
                            {
                                return;
                            }
                            int index = superGridControl1.ActiveRow.Index;
                            if (index == -1)
                            {
                                return;
                            }
                            GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                            FastReportLib.XbxReportParas InsXbxReport = new FastReportLib.XbxReportParas();
                            InsXbxReport.InitParasDate();
                            InsXbxReport.ReportParaHospital = Program.HospitalName;
                            InsXbxReport.study_no = Row.Cells["study_no"].Value.ToString();
                            InsXbxReport.Txt_Name = Row.Cells["patient_name"].Value.ToString();
                            InsXbxReport.Txt_Sex = Row.Cells["sex"].Value.ToString();
                            InsXbxReport.Txt_Age = Row.Cells["age"].Value.ToString();
                            InsXbxReport.Txt_ly = Row.Cells["patient_source"].Value.ToString();
                            if (InsXbxReport.Txt_ly.Equals("外来"))
                            {
                                InsXbxReport.Txt_Sjks = Row.Cells["submit_unit"].Value.ToString();
                            }
                            else
                            {
                                InsXbxReport.Txt_Sjks = Row.Cells["req_dept"].Value.ToString();
                            }
                            InsXbxReport.Txt_Zyh = Row.Cells["input_id"].Value.ToString();
                            InsXbxReport.Txt_Sjys = Row.Cells["req_physician"].Value.ToString();
                            InsXbxReport.Txt_Sqrq = Row.Cells["received_datetime"].Value.ToString().Substring(0, 10);
                            InsXbxReport.Txt_mzh = Row.Cells["output_id"].Value.ToString();
                            InsXbxReport.Txt_Bgrq = report_bl_datetime.Value.ToString("yyyy-MM-dd");
                            InsXbxReport.Txt_Bgys = comboBoxEx1.Text.Trim();
                            InsXbxReport.fileImages = ReportImageStr;
                            DBHelper.BLL.exam_report InsBllReport = new DBHelper.BLL.exam_report();
                            DBHelper.Model.exam_report InsMReport = InsBllReport.GetExam_Report(txt_pz_blh.Text.Trim());
                            if (InsMReport != null)
                            {
                                InsXbxReport.Txt_Xbl = InsMReport.xbms;
                                InsXbxReport.Txt_Xbxjg = InsMReport.zdyj;
                                InsXbxReport.Txt_Zljy = InsMReport.zdpz;
                            }
                            //送检标本
                            InsXbxReport.Txt_bblx = textBoxX8.Text.Trim();
                            //病人信息
                            if (InsXbxReport.Txt_ly.Equals("体检") || InsXbxReport.Txt_ly.Equals("门诊"))
                            {
                                InsXbxReport.Txt_mcyj = Row.Cells["hospital_card"].Value.ToString();
                            }
                            Print_Flag = this.reportViewer1.DirectPrintXbxReport(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, InsXbxReport, PathImg, @strFileName);

                        }
                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("打印报告失败：" + ex.ToString());
                        return;
                    }
                }
                else if (report_type == 2)
                {
                    try
                    {
                        if (superTabControl1.SelectedTabIndex == 3)
                        {
                            //预览打印
                            Print_Flag = this.reportViewer1.PrintReport(@strFileName);
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
                        if (superTabControl1.SelectedTabIndex == 3)
                        {
                            //预览打印
                            Print_Flag = this.reportViewer1.PrintReport(@strFileName);
                        }
                        else
                        {
                            //直接打印
                            //报告
                            if (superGridControl1.ActiveRow == null)
                            {
                                return;
                            }
                            int index = superGridControl1.ActiveRow.Index;
                            if (index == -1)
                            {
                                return;
                            }
                            GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                            //胸腹水型报告单对象
                            FastReportLib.BbXfsReportParas InsBbXfsReport = new FastReportLib.BbXfsReportParas();
                            InsBbXfsReport.InitParasDate();
                            InsBbXfsReport.ReportParaHospital = Program.HospitalName;
                            InsBbXfsReport.study_no = Row.Cells["study_no"].Value.ToString();
                            InsBbXfsReport.Txt_Name = Row.Cells["patient_name"].Value.ToString();
                            InsBbXfsReport.Txt_Sex = Row.Cells["sex"].Value.ToString();
                            InsBbXfsReport.Txt_Age = Row.Cells["age"].Value.ToString();
                            InsBbXfsReport.Txt_Bed = Row.Cells["bed_no"].Value.ToString();
                            InsBbXfsReport.Txt_ly = Row.Cells["patient_source"].Value.ToString();
                            InsBbXfsReport.hospital_card = Row.Cells["hospital_card"].Value.ToString();
                            InsBbXfsReport.Txt_Sqd = txt_sqd.Text.Trim();
                            InsBbXfsReport.Txt_Tel = Row.Cells["phone_number"].Value.ToString();
                            InsBbXfsReport.Txt_Addr = Row.Cells["current_place"].Value.ToString();
                            if (Row.Cells["patient_id"].Value.ToString().IndexOf("New") == -1)
                            {
                                InsBbXfsReport.Txt_Pid = Row.Cells["patient_id"].Value.ToString();
                            }
                            else
                            {
                                InsBbXfsReport.Txt_Pid = "";
                            }
                            if (InsBbXfsReport.Txt_ly.Equals("外来"))
                            {
                                InsBbXfsReport.Txt_Sjks = Row.Cells["submit_unit"].Value.ToString();
                            }
                            else
                            {
                                InsBbXfsReport.Txt_Sjks = Row.Cells["req_dept"].Value.ToString();
                            }

                            InsBbXfsReport.Txt_Zyh = Row.Cells["input_id"].Value.ToString();
                            InsBbXfsReport.Txt_Sjys = Row.Cells["req_physician"].Value.ToString();
                            InsBbXfsReport.Txt_Sqrq = Row.Cells["received_datetime"].Value.ToString().Substring(0, 10);
                            InsBbXfsReport.Txt_mzh = Row.Cells["output_id"].Value.ToString();
                            InsBbXfsReport.Txt_Blzd = txt_xbxXfs.Text.Trim();
                            InsBbXfsReport.txt_Bgrq = report_bl_datetime.Value.ToString("yyyy-MM-dd");
                            InsBbXfsReport.Txt_Bgys = comboBoxEx1.Text.Trim();
                            InsBbXfsReport.Txt_Fyys = comboBoxEx2.Text.Trim();
                            InsBbXfsReport.Txt_Zdpz = txt_bl_zdpz.Text.Trim();
                            InsBbXfsReport.fileImages = ReportImageStr;
                            //送检部位
                            InsBbXfsReport.Txt_Sjbw = this.textBoxX8.Text.Trim();
                            Print_Flag = this.reportViewer1.DirectPrintXfsReport(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, InsBbXfsReport, PathImg, @strFileName);
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
                        if (superTabControl1.SelectedTabIndex == 3)
                        {
                            //预览打印
                            Print_Flag = this.reportViewer1.PrintReport(@strFileName);
                        }

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
                        if (superTabControl1.SelectedTabIndex == 3)
                        {
                            //预览打印
                            Print_Flag = this.reportViewer1.PrintReport(@strFileName);
                        }
                        else
                        {
                            //直接打印
                            Print_Flag = this.reportViewer1.DirectPrintWyHzReport(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, exam_no, study_no, PathImg, ReportImageStr, Program.HospitalName, textBoxX8.Text.Trim(), txt_Wy_zdyj.Text.Trim(), txt_wyblh.Text.Trim(), txt_sjdw.Text.Trim(), integerInput1.Value, integerInput2.Value, report_bl_datetime.Value.ToString("yyyy-MM-dd HH:mm:ss").Trim(), @strFileName);
                        }

                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("外院会诊报告打印失败：" + ex.ToString());
                        return;
                    }
                }
                else if (report_type == 6)
                {
                    try
                    {
                        if (superTabControl1.SelectedTabIndex == 3)
                        {
                            //预览打印
                            Print_Flag = this.reportViewer1.PrintReport(@strFileName);
                        }

                    }
                    catch (Exception ex)
                    {
                        Program.FileLog.Error("打印报告失败：" + ex.ToString());
                        return;
                    }
                }
                //打印成功后图像格式报告上传和报告状态修改
                ReportUploadStatus(Print_Flag, strFileName, status, exam_no, study_no);
            }
            else
            {
                Frm_TJInfo("报告还未保存", "请先保存报告，然后执行打印！");
            }
        }
        //打印成功后图像格式报告上传和报告状态修改
        public void ReportUploadStatus(Boolean Print_Flag, string strFileName, int status, string exam_no, string study_no)
        {
            if (Print_Flag)
            {
                string filenameStr = string.Format("Report_{0}", study_no);
                DBHelper.BLL.exam_master insMas = new DBHelper.BLL.exam_master();
                //上传报告图片格式到服务器
                if (System.IO.File.Exists(@strFileName))
                {
                    string remoteFile = string.Format(@"{0}/{1}/{2}/{3}/{4}.jpg", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, study_no, filenameStr);
                    //上传
                    if (FtpUpload(@remoteFile, strFileName))
                    {
                        DBHelper.BLL.multi_media_info insMulti = new DBHelper.BLL.multi_media_info();
                        if (insMulti.InsertData(study_no, remoteFile, string.Format("{0}.jpg", filenameStr), 7) == 1)
                        {

                        }
                    }
                }
                DBHelper.BLL.exam_report insBll = new DBHelper.BLL.exam_report();
                Boolean ZXResult = insBll.PrintReport(study_no);
                if (ZXResult == true)
                {
                    if (superGridControl1.ActiveRow == null)
                    {
                        return;
                    }
                    int index = superGridControl1.ActiveRow.Index;
                    if (index == -1)
                    {
                        return;
                    }
                    GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                    if (status < 55)
                    {
                        //更新检查状态
                        insMas.UpdateExam_Status(study_no, "55");
                        if (examStatus_dic.ContainsKey("55"))
                        {
                            if (superTabControl1.SelectedTabIndex != 3)
                            {
                                if (exam_status_dic.ContainsKey("55"))
                                {
                                    Row.Cells["status_name"].CellStyles.Default.TextColor = exam_status_dic["55"];
                                    Row.Cells["exam_status"].Value = "55";
                                }
                                Row.Cells["status_name"].Value = examStatus_dic["55"];
                            }
                        }
                        //更新质控时间
                        int ZkTotalTime = insMas.UpdateZkTime(exam_no);
                        //获取质控时间限制 
                        DBHelper.BLL.exam_type insType = new DBHelper.BLL.exam_type();
                        string modality = Row.Cells["modality"].Value.ToString();
                        DataTable dt = insType.GetReportLimitFromModality(modality);
                        if (dt.Rows.Count > 0)
                        {
                            int curZkTime = Convert.ToInt32(dt.Rows[0]["report_limit"]);
                            if (ZkTotalTime > curZkTime)
                            {
                                FrmYsRegister insYsFrm = new FrmYsRegister();
                                insYsFrm.exam_no = exam_no;
                                insYsFrm.bgys = comboBoxEx1.Text.Trim();
                                insYsFrm.shys = comboBoxEx2.Text.Trim(); ;
                                insYsFrm.patient_name = Row.Cells["patient_name"].Value.ToString();
                                insYsFrm.study_no = study_no;
                                insYsFrm.ShowDialog();
                            }
                        }
                    }
                    //刷新按钮状态
                    status = insMas.GetStudyExam_Status(study_no);
                    ControlBtn(status, study_no);
                    //是否启用接口服务
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
                    Frm_TJInfo("打印成功！", "");
                    //下一个病人
                    buttonItem5_Click(null, null);
                    superTabControl2.SelectedTabIndex = 0;
                }
                else
                {
                    Frm_TJInfo("打印失败", "请联系系统管理员！");
                }
            }
            else
            {
                Frm_TJInfo("打印失败", "请联系系统管理员！");
            }
        }
        //追加
        private void buttonItem22_Click(object sender, EventArgs e)
        {
            if (btn_save1.Enabled == false)
            {
                return;
            }
            if (richTextBoxEx2.Text.Trim().Equals(""))
            {
                return;
            }
            if (report_type == 0)
            {
                if (txt_zdyj.SelectionLength > 0)
                {
                    txt_zdyj.SelectionStart = txt_zdyj.SelectionStart + txt_zdyj.SelectionLength;
                }
                txt_zdyj.SelectedText = richTextBoxEx2.Text.Trim();
                txt_zdyj.Focus();
            }
            if (report_type == 1)
            {

            }
            else if (report_type == 3)
            {
                if (this.txt_xbxXfs.SelectionLength > 0)
                {
                    txt_xbxXfs.SelectionStart = txt_xbxXfs.SelectionStart + txt_xbxXfs.SelectionLength;
                }
                txt_xbxXfs.SelectedText = richTextBoxEx2.Text.Trim();
                txt_xbxXfs.Focus();
            }
            else if (report_type == 5)
            {
                if (txt_Wy_zdyj.SelectionLength > 0)
                {
                    txt_Wy_zdyj.SelectionStart = txt_Wy_zdyj.SelectionStart + txt_Wy_zdyj.SelectionLength;
                }
                txt_Wy_zdyj.SelectedText = richTextBoxEx2.Text.Trim();
                txt_Wy_zdyj.Focus();
            }
        }
        //替换
        private void buttonItem23_Click(object sender, EventArgs e)
        {
            if (btn_save1.Enabled == false)
            {
                return;
            }
            if (richTextBoxEx2.Text.Trim().Equals(""))
            {
                return;
            }
            if (report_type == 0)
            {
                if (!txt_zdyj.Text.Trim().Equals(""))
                {
                    if (!richTextBoxEx2.Text.Trim().Equals(""))
                    {
                        eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                        TaskDialog.EnableGlass = false;

                        if (TaskDialog.Show("询问", "确认", "确定替换现有的诊断意见么？", Curbutton) == eTaskDialogResult.Ok)
                        {
                            txt_zdyj.Text = richTextBoxEx2.Text.Trim();
                            txt_zdyj.Focus();
                            txt_zdyj.SelectionStart = txt_zdyj.Text.Trim().Length;
                        }
                    }
                }
                else
                {
                    if (!richTextBoxEx2.Text.Trim().Equals(""))
                    {
                        txt_zdyj.Text = richTextBoxEx2.Text.Trim();
                        txt_zdyj.Focus();
                        txt_zdyj.SelectionStart = txt_zdyj.Text.Trim().Length;
                    }
                }

            }
            else if (report_type == 1)
            {

            }
            else if (report_type == 3)
            {
                if (!txt_xbxXfs.Text.Trim().Equals(""))
                {
                    if (!richTextBoxEx2.Text.Trim().Equals(""))
                    {
                        eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                        TaskDialog.EnableGlass = false;

                        if (TaskDialog.Show("询问", "确认", "确定替换现有的诊断意见么？", Curbutton) == eTaskDialogResult.Ok)
                        {
                            txt_xbxXfs.Text = richTextBoxEx2.Text.Trim();
                            txt_xbxXfs.Focus();
                            txt_xbxXfs.SelectionStart = txt_xbxXfs.Text.Trim().Length;
                        }
                    }
                }
                else
                {
                    if (!richTextBoxEx2.Text.Trim().Equals(""))
                    {
                        txt_xbxXfs.Text = richTextBoxEx2.Text.Trim();
                        txt_xbxXfs.Focus();
                        txt_xbxXfs.SelectionStart = txt_xbxXfs.Text.Trim().Length;
                    }
                }
            }
            else if (report_type == 5)
            {
                if (!txt_Wy_zdyj.Text.Trim().Equals(""))
                {
                    if (!richTextBoxEx2.Text.Trim().Equals(""))
                    {
                        eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                        TaskDialog.EnableGlass = false;

                        if (TaskDialog.Show("询问", "确认", "确定替换现有的诊断意见么？", Curbutton) == eTaskDialogResult.Ok)
                        {
                            txt_Wy_zdyj.Text = richTextBoxEx2.Text.Trim();
                            txt_Wy_zdyj.Focus();
                            txt_Wy_zdyj.SelectionStart = txt_Wy_zdyj.Text.Trim().Length;
                        }
                    }
                }
                else
                {
                    if (!richTextBoxEx2.Text.Trim().Equals(""))
                    {
                        txt_Wy_zdyj.Text = richTextBoxEx2.Text.Trim();
                        txt_Wy_zdyj.Focus();
                        txt_Wy_zdyj.SelectionStart = txt_xbxXfs.Text.Trim().Length;
                    }
                }
            }
        }
        //选中追加
        private void btn_zjxz_Click(object sender, EventArgs e)
        {
            if (btn_save1.Enabled == false)
            {
                return;
            }
            if (richTextBoxEx2.Text.Trim().Equals(""))
            {
                return;
            }
            if (report_type == 0)
            {
                txt_zdyj.SelectedText = richTextBoxEx2.SelectedText.Trim();
                txt_zdyj.Focus();

            }
            else if (report_type == 1)
            {

            }
            else if (report_type == 3)
            {
                txt_xbxXfs.SelectedText = richTextBoxEx2.SelectedText.Trim();
                txt_xbxXfs.Focus();
            }
            else if (report_type == 5)
            {
                txt_Wy_zdyj.SelectedText = richTextBoxEx2.SelectedText.Trim();
                txt_Wy_zdyj.Focus();
            }
        }
        //自动打开摄像头逻辑改变
        bool firstOpenDeviceFlag = true;
        //报告相关页签切换
        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (e.NewValue.Text == "病理图像")
            {
                superTabControl2.SelectedTabIndex = 2;
                //自动打开设备
                if (PIS_Sys.Properties.Settings.Default.Open_BG_SXT == true)
                {
                    if (firstOpenDeviceFlag)
                    {
                        firstOpenDeviceFlag = false;
                        //打开视频设备
                        buttonItem2_Click(null, null);
                    }
                }
            }
            else if (e.NewValue.Text == "报告书写")
            {
                superTabControl2.SelectedTabIndex = 0;
            }
            else if (e.NewValue.Text == "报告预览")
            {
                RefreshPreviewReport();
            }
        }
        //预览
        public void RefreshPreviewReport()
        {
            if (txt_pz_blh.Text.Trim().Equals(""))
            {
                return;
            }
            //预览报告
            if (superGridControl1.ActiveRow == null)
            {
                return;
            }
            int index = superGridControl1.ActiveRow.Index;
            if (index == -1)
            {
                return;
            }
            //获取选中的图像
            GetBgImage();
            //提取当前关键信息
            string study_no = txt_pz_blh.Text.Trim();
            string exam_no = txt_sqd.Text.Trim();
            string PathImg = Program.APPdirPath;
            try
            {
                if (report_type == 0)
                {
                    GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                    //病理学报告单内容对象
                    FastReportLib.BLReportParas InsBLReport = new FastReportLib.BLReportParas();
                    InsBLReport.InitParasDate();
                    InsBLReport.ReportParaHospital = Program.HospitalName;
                    InsBLReport.study_no = Row.Cells["study_no"].Value.ToString();
                    InsBLReport.Txt_Name = Row.Cells["patient_name"].Value.ToString();
                    InsBLReport.Txt_Sex = Row.Cells["sex"].Value.ToString();
                    InsBLReport.Txt_Age = Row.Cells["age"].Value.ToString();
                    InsBLReport.Txt_ly = Row.Cells["patient_source"].Value.ToString();

                    if (InsBLReport.Txt_ly.Equals("外来"))
                    {
                        InsBLReport.Txt_Sjks = Row.Cells["submit_unit"].Value.ToString();
                    }
                    else
                    {
                        InsBLReport.Txt_Sjks = Row.Cells["req_dept"].Value.ToString();
                    }

                    if (!Lab_WtzdFlag.Tag.Equals("1"))
                    {
                        InsBLReport.wtzd_flag = "0";
                    }
                    else
                    {
                        InsBLReport.wtzd_flag = "1";
                    }

                    InsBLReport.Txt_Bf = Row.Cells["ward"].Value.ToString();
                    InsBLReport.Txt_Ch = Row.Cells["bed_no"].Value.ToString();
                    InsBLReport.Txt_Zyh = Row.Cells["input_id"].Value.ToString();
                    InsBLReport.Txt_Sjys = Row.Cells["req_physician"].Value.ToString();
                    InsBLReport.Txt_Sqrq = Row.Cells["received_datetime"].Value.ToString().Substring(0, 10);
                    InsBLReport.Txt_mzh = Row.Cells["output_id"].Value.ToString();
                    InsBLReport.Txt_Rysj = txt_rysj.Text.Trim();
                    InsBLReport.Txt_Blzd = txt_zdyj.Text.Trim();
                    InsBLReport.txt_Bgrq = report_bl_datetime.Value.ToString("yyyy-MM-dd");
                    InsBLReport.Txt_Bgys = comboBoxEx1.Text.Trim();
                    InsBLReport.Txt_fjys = comboBoxEx2.Text.Trim();
                    InsBLReport.fileImages = ReportImageStr;
                    InsBLReport.Txt_zdpz = txt_bl_zdpz.Text.Trim();
                    InsBLReport.report_gb_doc = cmb_gpys.Text.Trim();
                    InsBLReport.Txt_Sjdw = Row.Cells["submit_unit"].Value.ToString();
                    //送检部位
                    InsBLReport.Txt_Sjbw = textBoxX8.Text.Trim();
                    //PId
                    if (Row.Cells["patient_id"].Value.ToString().IndexOf("New") == -1)
                    {
                        InsBLReport.Txt_Pid = Row.Cells["patient_id"].Value.ToString();
                    }
                    else
                    {
                        InsBLReport.Txt_Pid = "";
                    }
                    this.reportViewer1.LoadBLPreviewer(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, InsBLReport, PathImg, new Font(this.txt_rysj.Font.Name, Convert.ToSingle(this.ComboBox2.Text)), new Font(this.txt_zdyj.Font.Name, Convert.ToSingle(this.comboBox1.Text)));
                }
                else if (report_type == 1)
                {
                    GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                    //细胞学报告单内容对象
                    FastReportLib.XbxReportParas InsXbxReport = new FastReportLib.XbxReportParas();
                    InsXbxReport.InitParasDate();
                    InsXbxReport.ReportParaHospital = Program.HospitalName;
                    InsXbxReport.study_no = Row.Cells["study_no"].Value.ToString();
                    InsXbxReport.Txt_Name = Row.Cells["patient_name"].Value.ToString();
                    InsXbxReport.Txt_Sex = Row.Cells["sex"].Value.ToString();
                    InsXbxReport.Txt_Age = Row.Cells["age"].Value.ToString();
                    InsXbxReport.Txt_ly = Row.Cells["patient_source"].Value.ToString();
                    if (InsXbxReport.Txt_ly.Equals("外来"))
                    {
                        InsXbxReport.Txt_Sjks = Row.Cells["submit_unit"].Value.ToString();
                    }
                    else
                    {
                        InsXbxReport.Txt_Sjks = Row.Cells["req_dept"].Value.ToString();
                    }
                    InsXbxReport.Txt_Zyh = Row.Cells["input_id"].Value.ToString();
                    InsXbxReport.Txt_Sjys = Row.Cells["req_physician"].Value.ToString();
                    InsXbxReport.Txt_Sqrq = Row.Cells["received_datetime"].Value.ToString().Substring(0, 10);
                    InsXbxReport.Txt_mzh = Row.Cells["output_id"].Value.ToString();
                    InsXbxReport.Txt_Bgrq = report_bl_datetime.Value.ToString("yyyy-MM-dd");
                    InsXbxReport.Txt_Bgys = comboBoxEx1.Text.Trim();
                    InsXbxReport.fileImages = ReportImageStr;
                    DBHelper.BLL.exam_report InsBllReport = new DBHelper.BLL.exam_report();
                    DBHelper.Model.exam_report InsMReport = InsBllReport.GetExam_Report(txt_pz_blh.Text.Trim());
                    if (InsMReport != null)
                    {
                        InsXbxReport.Txt_Xbl = InsMReport.xbms;
                        InsXbxReport.Txt_Xbxjg = InsMReport.zdyj;
                        InsXbxReport.Txt_Zljy = InsMReport.zdpz;
                    }
                    //送检标本
                    InsXbxReport.Txt_bblx = textBoxX8.Text.Trim();
                    //病人信息
                    if (InsXbxReport.Txt_ly.Equals("体检") || InsXbxReport.Txt_ly.Equals("门诊"))
                    {
                        InsXbxReport.Txt_mcyj = Row.Cells["hospital_card"].Value.ToString();
                    }
                    this.reportViewer1.LoadXbxPreviewer(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, InsXbxReport, PathImg);

                }
                else if (report_type == 3)
                {
                    GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                    //胸腹水型报告单对象
                    FastReportLib.BbXfsReportParas InsBbXfsReport = new FastReportLib.BbXfsReportParas();
                    InsBbXfsReport.InitParasDate();
                    InsBbXfsReport.ReportParaHospital = Program.HospitalName;
                    InsBbXfsReport.study_no = Row.Cells["study_no"].Value.ToString();
                    InsBbXfsReport.Txt_Name = Row.Cells["patient_name"].Value.ToString();
                    InsBbXfsReport.Txt_Sex = Row.Cells["sex"].Value.ToString();
                    InsBbXfsReport.Txt_Age = Row.Cells["age"].Value.ToString();
                    InsBbXfsReport.Txt_Bed = Row.Cells["bed_no"].Value.ToString();
                    InsBbXfsReport.Txt_ly = Row.Cells["patient_source"].Value.ToString();
                    InsBbXfsReport.hospital_card = Row.Cells["hospital_card"].Value.ToString();
                    InsBbXfsReport.Txt_Sqd = txt_sqd.Text.Trim();
                    InsBbXfsReport.Txt_Tel = Row.Cells["phone_number"].Value.ToString();
                    InsBbXfsReport.Txt_Addr = Row.Cells["current_place"].Value.ToString();
                    if (Row.Cells["patient_id"].Value.ToString().IndexOf("New") == -1)
                    {
                        InsBbXfsReport.Txt_Pid = Row.Cells["patient_id"].Value.ToString();
                    }
                    else
                    {
                        InsBbXfsReport.Txt_Pid = "";
                    }
                    if (InsBbXfsReport.Txt_ly.Equals("外来"))
                    {
                        InsBbXfsReport.Txt_Sjks = Row.Cells["submit_unit"].Value.ToString();
                    }
                    else
                    {
                        InsBbXfsReport.Txt_Sjks = Row.Cells["req_dept"].Value.ToString();
                    }

                    InsBbXfsReport.Txt_Zyh = Row.Cells["input_id"].Value.ToString();
                    InsBbXfsReport.Txt_Sjys = Row.Cells["req_physician"].Value.ToString();
                    InsBbXfsReport.Txt_Sqrq = Row.Cells["received_datetime"].Value.ToString().Substring(0, 10);
                    InsBbXfsReport.Txt_mzh = Row.Cells["output_id"].Value.ToString();
                    InsBbXfsReport.Txt_Blzd = txt_xbxXfs.Text.Trim();
                    InsBbXfsReport.txt_Bgrq = report_bl_datetime.Value.ToString("yyyy-MM-dd");
                    InsBbXfsReport.Txt_Bgys = comboBoxEx1.Text.Trim();
                    InsBbXfsReport.Txt_Fyys = comboBoxEx2.Text.Trim();
                    InsBbXfsReport.Txt_Zdpz = txt_bl_zdpz.Text.Trim();
                    InsBbXfsReport.fileImages = ReportImageStr;
                    //送检部位
                    InsBbXfsReport.Txt_Sjbw = this.textBoxX8.Text.Trim();
                    this.reportViewer1.LoadXfsPreviewer(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, InsBbXfsReport, PathImg);
                }
                else if (report_type == 5)
                {
                    this.reportViewer1.LoadWyHzPreviewer(PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, exam_no, study_no, PathImg, ReportImageStr, Program.HospitalName, textBoxX8.Text.Trim(), txt_Wy_zdyj.Text.Trim(), txt_wyblh.Text.Trim(), txt_sjdw.Text.Trim(), integerInput1.Value, integerInput2.Value, report_bl_datetime.Value.ToString("yyyy-MM-dd HH:mm:ss").Trim());
                }
            }
            catch (Exception ex)
            {
                Program.FileLog.Error(ex.ToString());
            }
            finally
            {

            }
        }

        //写入特检结果
        public void WriteTjResult(string resultStr)
        {

            resultStr = "\n免疫组化结果显示:" + resultStr;
            if (report_type == 0)
            {
                if (txt_zdyj.Enabled == true)
                {
                    txt_zdyj.Text += resultStr;
                }
            }
            else if (report_type == 1)
            {

            }
            else if (report_type == 3)
            {
                if (txt_xbxXfs.Enabled == true)
                {
                    txt_xbxXfs.Text += resultStr;
                }
            }
            else if (report_type == 2 || report_type == 4)
            {
                if (txt_bl_zdpz.Enabled == true)
                {
                    txt_bl_zdpz.Text += resultStr;
                }
            }
            else if (report_type == 5)
            {
                if (txt_Wy_zdyj.Enabled == true)
                {
                    txt_Wy_zdyj.Text += resultStr;
                }
            }
        }

        #endregion



        //API声明：获取当前焦点控件句柄      

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]

        internal static extern IntPtr GetFocus();

        ///获取 当前拥有焦点的控件
        private Control GetFocusedControl()
        {

            Control focusedControl = null;

            // To get hold of the focused control:

            IntPtr focusedHandle = GetFocus();

            if (focusedHandle != IntPtr.Zero)
            {
                focusedControl = Control.FromChildHandle(focusedHandle);
            }

            return focusedControl;

        }


        //剪切
        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

            try
            {
                Control controlCur = GetFocusedControl();
                if (controlCur != null)
                {
                    Clipboard.SetText(((System.Windows.Forms.RichTextBox)controlCur).SelectedText);
                    ((System.Windows.Forms.RichTextBox)controlCur).SelectedText = String.Empty;
                }
                else
                {
                    return;
                }
            }
            catch
            {
            }
        }
        //复制
        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            try
            {
                Control controlCur = GetFocusedControl();
                if (controlCur != null)
                {
                    Clipboard.SetText(((System.Windows.Forms.RichTextBox)controlCur).SelectedText);
                }
                else
                {
                    return;
                }
            }
            catch
            {
            }
        }
        //粘贴
        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            try
            {
                Control controlCur = GetFocusedControl();
                if (controlCur != null)
                {
                    ((System.Windows.Forms.RichTextBox)controlCur).SelectedText = Clipboard.GetText();
                }
                else
                {
                    return;
                }
            }
            catch
            {
            }
        }
        //时间轴
        private void buttonX17_Click(object sender, EventArgs e)
        {
            string exam_no = "";
            string patient_name = "";
            string study_no = "";
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //申请单号
                exam_no = Row.Cells["exam_no"].Value.ToString();
                patient_name = Row.Cells["patient_name"].Value.ToString();
                study_no = Row.Cells["study_no"].Value.ToString();
            }
            else
            {
                return;
            }
            Form Frm_SjzIns = Application.OpenForms["Frm_Sjz"];
            if (Frm_SjzIns == null)
            {
                Frm_SjzIns = new Frm_Sjz();
                Frm_SjzIns.BringToFront();
                Frm_SjzIns.WindowState = FormWindowState.Normal;
                Frm_Sjz.exam_no = exam_no;
                Frm_Sjz.patient_name = patient_name;
                Frm_Sjz.study_no = study_no;
                Frm_SjzIns.Owner = this;
                Frm_SjzIns.ShowDialog();
            }
            else
            {
                Frm_Sjz.exam_no = exam_no;
                Frm_Sjz.patient_name = patient_name;
                Frm_Sjz.study_no = study_no;
                Frm_SjzIns.BringToFront();
                Frm_SjzIns.WindowState = FormWindowState.Normal;
            }
        }
        //其他检查
        private void buttonX16_Click(object sender, EventArgs e)
        {
            string patient_id = "";
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //标记
                string new_flag = Row.Cells["new_flag"].Value.ToString();
                string input_id = Row.Cells["input_id"].Value.ToString();
                //病人id号
                if (new_flag.Equals("0"))
                {
                    patient_id = Row.Cells["patient_id"].Value.ToString();
                }
                else
                {
                    patient_id = Row.Cells["patient_id"].Value.ToString();
                }
            }
            else
            {
                return;
            }
            Form Frm_qtJcbgIns = Application.OpenForms["Frm_qtJcbg"];
            if (Frm_qtJcbgIns == null)
            {
                Frm_qtJcbgIns = new Frm_qtJcbg();
                Frm_qtJcbgIns.BringToFront();
                Frm_qtJcbgIns.WindowState = FormWindowState.Normal;
                Frm_qtJcbg.patientid = patient_id;
                Frm_qtJcbgIns.Show();
                Frm_qtJcbgIns.Activate();
            }
            else
            {
                Frm_qtJcbg.patientid = patient_id;
                Frm_qtJcbgIns.BringToFront();
                Frm_qtJcbgIns.WindowState = FormWindowState.Normal;
                Frm_qtJcbgIns.Activate();
            }
        }

        private void buttonX18_Click(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {

                Frm_myzh_report Frm_reportIns = new Frm_myzh_report();
                Frm_reportIns.Owner = this;
                Frm_reportIns.BLH = txt_pz_blh.Text.Trim();
                Frm_reportIns.exam_no = txt_sqd.Text.Trim();
                Frm_reportIns.lczd = patientInfo1.GetLczd();
                Frm_reportIns.blzd = txt_zdyj.Text.Trim();
                Frm_reportIns.BringToFront();
                Frm_reportIns.ShowDialog();
                //免疫组化报告
                DBHelper.BLL.myzh_report insMyzh = new DBHelper.BLL.myzh_report();
                DataTable dtMyzh = insMyzh.GetData(txt_pz_blh.Text.Trim());
                if (dtMyzh != null && dtMyzh.Rows.Count == 1)
                {
                    this.buttonX18.TextColor = Color.Blue;
                }
                else
                {
                    buttonX18.TextColor = CurBtnColor;
                }
            }
        }
        //历史痕迹
        private void btn_ReportHj_Click(object sender, EventArgs e)
        {
            if (!txt_pz_blh.Text.Trim().Equals(""))
            {
                Form Frm_QcxxIns = Application.OpenForms["Frm_BgHj"];
                if (Frm_QcxxIns != null)
                {
                    Frm_QcxxIns.Owner = this;
                    Frm_BgHj.BLH = txt_pz_blh.Text.Trim();
                    Frm_BgHj.sqd = txt_sqd.Text.Trim();
                    Frm_QcxxIns.BringToFront();
                    Frm_QcxxIns.WindowState = FormWindowState.Normal;
                }
                else
                {
                    Frm_QcxxIns = new Frm_BgHj();
                    Frm_QcxxIns.WindowState = FormWindowState.Normal;
                    Frm_BgHj.BLH = txt_pz_blh.Text.Trim();
                    Frm_BgHj.sqd = txt_sqd.Text.Trim();
                    Frm_QcxxIns.BringToFront();
                    Frm_QcxxIns.Show();
                }
            }
        }
        //与临床医生沟通
        private void buttonX19_Click(object sender, EventArgs e)
        {

            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                FrmLcgt ins = new FrmLcgt();
                ins.lcdoc = Row.Cells["req_physician"].Value.ToString();
                ins.lcdept = Row.Cells["req_dept"].Value.ToString();
                ins.bldoc = Program.User_Name;
                ins.study_no = Row.Cells["study_no"].Value.ToString();
                ins.ShowDialog();
                DBHelper.BLL.exam_lcys_im insFrm = new DBHelper.BLL.exam_lcys_im();
                DataTable dt = insFrm.GetData(ins.study_no);
                if (dt != null && dt.Rows.Count > 0)
                {
                    buttonX19.TextColor = Color.Blue;
                }
                else
                {
                    buttonX19.TextColor = CurBtnColor;
                }
            }
            else
            {
                Frm_TJInfo("提示", "请选择一个检查！");
            }
        }


    }
}
