using DevComponents.DotNetBar;
using PIS_Statistics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PIS_Sys
{
    public partial class FrmMain : DevComponents.DotNetBar.Office2007Form
    {

        #region  声明 

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wparam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hwnd, int wMsg, bool wparam, int lParam);

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "RedrawWindow")]
        [return: System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool RedrawWindow([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, [System.Runtime.InteropServices.InAttribute()] System.IntPtr lprcUpdate, [System.Runtime.InteropServices.InAttribute()] System.IntPtr hrgnUpdate, uint flags);

        #endregion

        #region 创建mdi子窗体 

        private string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        private bool isExist(string ChildTypeName)
        {
            if (Directory.GetCurrentDirectory() != BasePath)
                Directory.SetCurrentDirectory(BasePath);

            bool b_result = false;
            foreach (DevComponents.DotNetBar.Office2007Form frm in MdiChildren)
            {
                if (frm.GetType().Name == ChildTypeName)
                {
                    if (frm.Tag.Equals("show"))
                    {
                        frm.WindowState = FormWindowState.Maximized;
                        frm.Activate();
                        return true;
                    }
                }
            }
            foreach (DevComponents.DotNetBar.Office2007Form frm in MdiChildren)
            {
                frm.Hide();
                frm.Tag = "hide";
            }
            foreach (DevComponents.DotNetBar.Office2007Form frm in MdiChildren)
            {
                if (frm.GetType().Name == ChildTypeName)
                {
                    frm.WindowState = FormWindowState.Maximized;
                    int ret = SendMessage(this.Handle, 0x000B, 0, 0);
                    frm.BringToFront();
                    frm.Show();
                    frm.Tag = "show";
                    frm.Activate();
                    ret = SendMessage(this.Handle, 0x000B, 1, 0);
                    RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
                    b_result = true;

                    break;
                }
            }
            return b_result;
        }
        #endregion


        ///<summary>  
        ///在＊.exe.config文件中appSettings配置节增加一对键、值对  
        ///</summary>  
        ///<param name="newKey"></param>  
        ///<param name="newValue"></param>  
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            bool isModified = false;
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == newKey)
                {
                    isModified = true;
                    break;
                }
            }

            Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (isModified)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            config.AppSettings.Settings.Add(newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        public FrmMain()
        {
            //获取欲启动进程名
            string strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //检查进程是否已经启动，已经启动则显示报错信息退出程序。 
            if (System.Diagnostics.Process.GetProcessesByName(strProcessName).Length > 2)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
                return;
            }
            //获取授权信息
            Frm_splash SplashScreenMainSL = new Frm_splash();
            SplashScreenMainSL.BringToFront();
            SplashScreenMainSL.ShowDialog();
            SplashScreenMainSL.Close();
            SplashScreenMainSL.Dispose();
            //
            InitializeComponent();
            //
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            //给MDI 父窗体添加背景和解决闪烁的问题
            BackgroundNoSplash();
            //修改日期格式
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();
            Program.frmMainins = this;
            //主题切换
            eStyle style;
            if (Enum.TryParse<eStyle>(PIS_Sys.Properties.Settings.Default.SystemUI, out style))
            {
                styleManagerUI.ManagerStyle = style;
            }
            //创建窗体句柄时发生
            this.HandleCreated += MainForm_HandleCreated;
            //获取接口服务设置信息
            DBHelper.BLL.interface_set_info InsInter = new DBHelper.BLL.interface_set_info();
            Program.Interface_SetInfo = InsInter.GetInterfaceSetInfo();
            //数字摄像头类别配置(0通用1官方) 
            if (ConfigurationManager.AppSettings["UsbCameraType"] == null)
            {
                UpdateAppConfig("UsbCameraType", "0");
                Program.UsbCameraType = 0;
            }
            else
            {
                try
                {
                    Program.UsbCameraType = Convert.ToInt32(ConfigurationManager.AppSettings["UsbCameraType"]);
                }
                catch
                {
                    Program.UsbCameraType = 0;
                }
            }
            //即时消息配置
            if (ConfigurationManager.AppSettings["IM"] == null)
            {
                UpdateAppConfig("IM", "1");
            }
            //视频保持缩放比例
            if (ConfigurationManager.AppSettings["KeepAspectRatioFlag"] == null)
            {
                UpdateAppConfig("KeepAspectRatioFlag", "0");
                Program.KeepAspectRatioFlag = 0;
            }
            else
            {
                Program.KeepAspectRatioFlag = Convert.ToInt32(ConfigurationManager.AppSettings["KeepAspectRatioFlag"]);
            }
            //全自动免疫组化设备通信
            if (ConfigurationManager.AppSettings["MYZH"] == null)
            {
                UpdateAppConfig("MYZH", "0");
                UpdateAppConfig("MYZH_IP", "127.0.0.1");
                UpdateAppConfig("MYZH_PORT", "8888");
            }
            //
            string ImFlag = ConfigurationManager.AppSettings["IM"] ?? "0";
            if (ImFlag.Equals("1"))
            {
                //启动即时消息
                IntPtr hWnd = GetProcessHandler("WinImApp");
                if (hWnd == IntPtr.Zero)
                {
                    string filePath = Program.APPdirPath + @"\WinImApp";
                    if (Directory.Exists(filePath) == true)
                    {
                        filePath = Program.APPdirPath + @"\WinImApp\WinImApp.exe";
                        if (!File.Exists(filePath))
                        {
                            return;
                        }
                        else
                        {
                            string adminflag = "1";
                            DBHelper.BLL.sys_user ins = new DBHelper.BLL.sys_user();
                            int gLflag = ins.GetUserGL(Program.User_Code);
                            if (Program.System_Admin == true || gLflag == 1)
                            {
                                adminflag = "1";
                            }
                            else
                            {
                                adminflag = "0";
                            }
                            InvokeExe(Program.APPdirPath, @"WinImApp\WinImApp.exe", Program.User_Code + "|" + Program.User_Name + "|" + adminflag);
                        }
                    }
                }
            }
        }

        public static MdiClient mdiClient = new MdiClient();

        private void BackgroundNoSplash()
        {
            foreach (Control var in this.Controls)
            {
                if (var is MdiClient)
                {
                    mdiClient = var as MdiClient;
                    break;
                }
            }

            if (mdiClient != null)
            {
                mdiClient.Paint += new PaintEventHandler(OnMdiClientPaint);
                System.Reflection.MethodInfo mi = (mdiClient as Control).GetType().GetMethod("SetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                mi.Invoke(mdiClient, new object[] { ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer
                 | ControlStyles.ResizeRedraw, true });

            }
        }

        private void OnMdiClientPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(Properties.Resources.bg, new Rectangle(0, 0, mdiClient.Width, mdiClient.Height));
            string msg = AssemblyProduct + "{" + ConfigurationManager.AppSettings["SysVersion"] + "}";
            SizeF size = e.Graphics.MeasureString(msg, this.Font);
            g.DrawString(msg, this.Font, new SolidBrush(Color.DodgerBlue), mdiClient.Width - size.Width, mdiClient.Height - size.Height);
        }

        #region 程序集特性访问器

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
        public static string GetCurrentDirectory()
        {
            return (new FileInfo(Assembly.GetExecutingAssembly().Location)).Directory.FullName;
        }
        public void MainForm_HandleCreated(Object sender, EventArgs e)
        {
            Program.frmMainins.BringToFront();

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
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;

                int ret = SendMessage(this.Handle, 0x000B, 0, 0);
                if (TaskDialog.Show("潤沁實嶪病理信息管理系统", "确认", "确定要退出本系统么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    ret = SendMessage(this.Handle, 0x000B, 1, 0);
                    RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
                    e.Cancel = false;
                    this.HandleCreated -= MainForm_HandleCreated;
                    //关闭所有子窗体
                    foreach (DevComponents.DotNetBar.Office2007Form frm in MdiChildren)
                    {
                        frm.Close();
                        frm.Dispose();
                    }
                    mdiClient = null;
                    Program.frmMainins = null;
                    DBHelper.DBProcess.disposeDb();
                    //关闭即时消息
                    Process[] Processes = Process.GetProcessesByName("WinImApp");
                    IntPtr hWnd = IntPtr.Zero;
                    foreach (Process p in Processes)
                    {
                        p.Kill();
                    }
                    //质控应用
                    Process[] Processes11 = Process.GetProcessesByName("ZhiKongIM");
                    foreach (Process p1 in Processes11)
                    {
                        p1.Kill();
                    }
                    //强制资源回收
                    GC.Collect();
                    //关闭整个系统
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
                else
                {
                    ret = SendMessage(this.Handle, 0x000B, 1, 0);
                    RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
                    e.Cancel = true;
                }
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //获取接口服务设置信息表
            DBHelper.BLL.interface_set_info ins = new DBHelper.BLL.interface_set_info();
            Program.Interface_SetInfo = ins.GetInterfaceSetInfo();
            //获取第三方接口配置
            Program.EmrUrlStr = ins.GetThirdUrlinfo("EMR");
            Program.PACSUrlStr = ins.GetThirdUrlinfo("PACS");
            Program.HistoryPIS = ins.GetThirdUrlinfo("PIS");
            //用户菜单权限设置
            if (Program.System_Admin == false)
            {
                //读取用户权限
                DBHelper.BLL.sys_user insU = new DBHelper.BLL.sys_user();
                DataTable dt = insU.GetUserQx(Program.User_Code);
                List<string> listParentMenu = new List<String>();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (listParentMenu.Contains(dt.Rows[i]["menu_tag"].ToString()) == false)
                        {
                            listParentMenu.Add(dt.Rows[i]["menu_tag"].ToString());
                        }
                    }
                }
                //循环进行权限菜单设置
                if (listParentMenu.Contains("bbdj") == false)
                {
                    buttonItem2.Visible = false;
                }
                else
                {
                    buttonItem2.Visible = true;
                }
                if (listParentMenu.Contains("qcgl") == false)
                {
                    buttonItem3.Visible = false;
                }
                else
                {
                    buttonItem3.Visible = true;
                }
                if (listParentMenu.Contains("zpgl") == false)
                {
                    buttonItem4.Visible = false;
                }
                else
                {
                    buttonItem4.Visible = true;
                }
                if (listParentMenu.Contains("blzd") == false)
                {
                    buttonItem6.Visible = false;
                }
                else
                {
                    buttonItem6.Visible = true;
                }
                if (listParentMenu.Contains("jxgzz") == false)
                {
                    buttonItem7.Visible = false;
                }
                else
                {
                    buttonItem7.Visible = true;
                }
                if (listParentMenu.Contains("dagl") == false)
                {
                    buttonItem9.Visible = false;
                }
                else
                {
                    buttonItem9.Visible = true;
                }
                if (listParentMenu.Contains("tjcx") == false)
                {
                    buttonItem10.Visible = false;
                }
                else
                {
                    buttonItem10.Visible = true;
                }
                if (listParentMenu.Contains("xtsz") == false)
                {
                    buttonItem13.Visible = false;
                }
                else
                {
                    buttonItem13.Visible = true;
                }
                if (listParentMenu.Contains("ksgl") == false)
                {
                    buttonItem8.Visible = false;
                }
                else
                {
                    buttonItem8.Visible = true;
                }
                if (ConfigurationManager.AppSettings["w_big_type_db"].Contains("XBX") && !Program.workstation_type_db.Contains("PL"))
                {
                    buttonItem3.Visible = false;
                    buttonItem4.Visible = false;
                }

            }
            //当前登录用户
            this.Text = string.Format("潤沁實嶪病理信息管理系统—【{0}】<{1}:{2}>", ConfigurationManager.AppSettings["w_big_type_name"], Program.Dept_Name, Program.User_Name);

            //读取当前试剂管理中是否存在需要提示的信息
            DBHelper.BLL.shijiguanlics sjIns = new DBHelper.BLL.shijiguanlics();
            if (sjIns.SelectedSjmcTjCount() > 0)
            {
                Frm_TJInfo("试剂管理提示信息", "当前存在需要立即采购的试剂。\n请进入‘试剂管理’模块进行维护！");
            }

        }


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
        //消息服务
        public void Frm_MsgInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(this);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = false;
            Frm_AlertIns.TopMost = true;
            Frm_AlertIns.labelX2.Click += new EventHandler(Msg_Click);
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }

        private void Msg_Click(object sender, EventArgs e)
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

        //显示进度条
        public void showProgress()
        {
            Rectangle r = Screen.GetWorkingArea(this);
            panelEx1.Location = new Point((r.Width - panelEx1.Width) / 2, (r.Height - panelEx1.Height) / 2);
            panelEx1.BringToFront();
            panelEx1.Visible = true;
            Application.DoEvents();
        }
        //隐藏进度条
        public void HideProgress()
        {
            panelEx1.Visible = false;
            Application.DoEvents();
        }



        private void Delay(int Millisecond) //延迟系统时间，但系统又能同时能执行其它任务；
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(Millisecond) > DateTime.Now)
            {
                Application.DoEvents();//转让控制权            
            }
            return;
        }
        //标本登记
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            DateTime testTime = DateTime.Now;
            if (!isExist("FrmBbdj"))
            {
                showProgress();
                bbdj.FrmBbdj FrmBbdjIns = new bbdj.FrmBbdj();
                FrmBbdjIns.Tag = "show";
                FrmBbdjIns.MdiParent = this;
                int ret = SendMessage(this.Handle, 0x000B, 0, 0);

                FrmBbdjIns.WindowState = FormWindowState.Maximized;
                FrmBbdjIns.BringToFront();
                FrmBbdjIns.Show();
                HideProgress();
                ret = SendMessage(this.Handle, 0x000B, 1, 0);
                RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
            }
            if (Program.FileLog.IsDebugEnabled)
            {
                Program.FileLog.DebugFormat("\n激活窗口{0}所耗总时间为:{1}!\n", "FrmBbdj", (DateTime.Now - testTime).TotalSeconds);
            }
        }
        //大体取材
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            DateTime testTime = DateTime.Now;
            if (!isExist("FrmQcgl"))
            {

                showProgress();
                qcgl.FrmQcgl FrmQcglIns = new qcgl.FrmQcgl();
                FrmQcglIns.Tag = "show";
                FrmQcglIns.MdiParent = this;
                HideProgress();
                int ret = SendMessage(this.Handle, 0x000B, 0, 0);

                FrmQcglIns.WindowState = FormWindowState.Maximized;
                FrmQcglIns.BringToFront();
                FrmQcglIns.Show();
                ret = SendMessage(this.Handle, 0x000B, 1, 0);
                RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
            }
            if (Program.FileLog.IsDebugEnabled)
            {
                Program.FileLog.DebugFormat("\n激活窗口{0}所耗总时间为:{1}!\n", "FrmQcgl", (DateTime.Now - testTime).TotalSeconds);
            }

        }
        //技术制片
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            DateTime testTime = DateTime.Now;
            if (!isExist("Frm_jszp"))
            {
                showProgress();
                jszp.Frm_jszp FrmJszpIns = new jszp.Frm_jszp();
                FrmJszpIns.Tag = "show";
                FrmJszpIns.MdiParent = this;
                HideProgress();
                int ret = SendMessage(this.Handle, 0x000B, 0, 0);
                FrmJszpIns.BringToFront();

                FrmJszpIns.WindowState = FormWindowState.Maximized;
                FrmJszpIns.Show();
                ret = SendMessage(this.Handle, 0x000B, 1, 0);
                RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
            }
            if (Program.FileLog.IsDebugEnabled)
            {
                Program.FileLog.DebugFormat("\n激活窗口{0}所耗总时间为:{1}!\n", "Frm_PlJszp", (DateTime.Now - testTime).TotalSeconds);
            }
        }

        //病理诊断
        private void buttonItem6_Click(object sender, EventArgs e)
        {
            DateTime testTime = DateTime.Now;
            if (!isExist("FrmBlzd"))
            {
                showProgress();
                blzd.FrmBlzd FrmBlzdIns = new blzd.FrmBlzd();
                FrmBlzdIns.Tag = "show";
                FrmBlzdIns.MdiParent = this;
                HideProgress();
                int ret = SendMessage(this.Handle, 0x000B, 0, 0);

                FrmBlzdIns.WindowState = FormWindowState.Maximized;
                FrmBlzdIns.BringToFront();
                FrmBlzdIns.Show();
                ret = SendMessage(this.Handle, 0x000B, 1, 0);
                RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
            }
            if (Program.FileLog.IsDebugEnabled)
            {
                Program.FileLog.DebugFormat("\n病理诊断激活窗口{0}所耗总时间为:{1}!\n", "FrmBlzd", (DateTime.Now - testTime).TotalSeconds);
            }
        }

        //归档工作站
        private void buttonItem9_Click(object sender, EventArgs e)
        {
            Form FrmDaglIns = Application.OpenForms["Frm_Dagl"];
            if (FrmDaglIns == null)
            {
                FrmDaglIns = new dagl.Frm_Dagl();
                FrmDaglIns.Owner = this;
                FrmDaglIns.WindowState = FormWindowState.Normal;
                FrmDaglIns.BringToFront();
                FrmDaglIns.Show();

            }
            else
            {
                FrmDaglIns.WindowState = FormWindowState.Normal;
                FrmDaglIns.BringToFront();
                FrmDaglIns.Activate();
            }

        }


        //检查统计
        private void buttonItem10_Click(object sender, EventArgs e)
        {

            Form FrmTjcxIns = Application.OpenForms["Frm_Tjcx"];
            if (FrmTjcxIns == null)
            {
                FrmTjcxIns = new Frm_Tjcx(PIS_Sys.Properties.Settings.Default.djBarcodePrinter);
                FrmTjcxIns.Owner = this;
                Frm_Tjcx.CurDept_Code = Program.Dept_Code;
                FrmTjcxIns.BringToFront();
                FrmTjcxIns.WindowState = FormWindowState.Normal;
                FrmTjcxIns.Show();

            }
            else
            {

                FrmTjcxIns.BringToFront();
                FrmTjcxIns.WindowState = FormWindowState.Normal;
                FrmTjcxIns.Activate();
            }
        }

        //系统设置
        private void buttonItem13_Click(object sender, EventArgs e)
        {
            Form Frm_XtszIns = Application.OpenForms["Frm_Xtsz"];
            if (Frm_XtszIns == null)
            {
                Frm_XtszIns = new xtsz.Frm_Xtsz();
                Frm_XtszIns.Owner = this;
                Frm_XtszIns.BringToFront();
                Frm_XtszIns.WindowState = FormWindowState.Normal;
                Frm_XtszIns.Show();
                Frm_XtszIns.Activate();

            }
            else
            {
                Frm_XtszIns.BringToFront();
                Frm_XtszIns.WindowState = FormWindowState.Normal;
                Frm_XtszIns.Activate();
            }
        }
        //检查预约
        private void buttonItem1_Click_1(object sender, EventArgs e)
        {
            Form FrmJcyyIns = Application.OpenForms["FrmJcyy"];
            if (FrmJcyyIns == null)
            {
                FrmJcyyIns = new jcyy.FrmJcyy();
                FrmJcyyIns.Owner = this;
                FrmJcyyIns.Tag = "show";
                FrmJcyyIns.BringToFront();
                FrmJcyyIns.Show();
            }
            else
            {
                FrmJcyyIns.BringToFront();
                FrmJcyyIns.WindowState = FormWindowState.Normal;
                FrmJcyyIns.Activate();
            }
        }
        //科室管理
        private void buttonItem8_Click(object sender, EventArgs e)
        {
            ksgl.Frm_ksgl Frm_ksglIns = new ksgl.Frm_ksgl();
            Frm_ksglIns.Owner = this;
            Frm_ksglIns.Tag = "show";
            Frm_ksglIns.BringToFront();
            Frm_ksglIns.ShowDialog();
        }
        //教学工作站
        private void buttonItem7_Click(object sender, EventArgs e)
        {

        }

        //费用补录
        private void Frm_Fybl(object sender, EventArgs e)
        {
            Form Frm_ExamItemIns = Application.OpenForms["Frm_ExamItem"];
            if (Frm_ExamItemIns == null)
            {
                Frm_ExamItemIns = new bbdj.Frm_ExamItem();
                Frm_ExamItemIns.Owner = this;
                Frm_ExamItemIns.Tag = "show";
                Frm_ExamItemIns.BringToFront();
                Frm_ExamItemIns.Show();
            }
            else
            {
                Frm_ExamItemIns.BringToFront();
                Frm_ExamItemIns.WindowState = FormWindowState.Normal;
                Frm_ExamItemIns.Activate();
            }
        }

        //切换用户
        private void buttonItem1_Click_2(object sender, EventArgs e)
        {
            eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
            TaskDialog.EnableGlass = false;
            int ret = SendMessage(this.Handle, 0x000B, 0, 0);
            if (TaskDialog.Show("潤沁實嶪病理信息管理系统", "确认", "确定要切换用户么？", Curbutton) == eTaskDialogResult.Ok)
            {
                ret = SendMessage(this.Handle, 0x000B, 1, 0);
                RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
                this.HandleCreated -= MainForm_HandleCreated;
                //关闭所有子窗体
                foreach (DevComponents.DotNetBar.Office2007Form frm in MdiChildren)
                {
                    frm.Close();
                    frm.Dispose();
                }
                mdiClient = null;
                Program.frmMainins = null;
                //重启系统
                Process p = new Process();
                p.StartInfo.FileName = Program.sys_exe_name;
                if (p.Start())
                {
                    //强制资源回收
                    GC.Collect();
                    //退出当前进程
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }
            }
            else
            {
                ret = SendMessage(this.Handle, 0x000B, 1, 0);
                RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
            }
        }
        /// <summary>
        /// 重启程序
        /// </summary>
        private void Restart()
        {
            Application.ExitThread();
            Thread thtmp = new Thread(new ParameterizedThreadStart(run));
            object appName = Application.ExecutablePath;
            Thread.Sleep(500);
            thtmp.Start(appName);
        }
        private void run(Object obj)
        {
            Process ps = new Process();
            ps.StartInfo.FileName = obj.ToString();
            ps.Start();
        }
        //关于
        private void buttonItem15_Click(object sender, EventArgs e)
        {
            AboutSystem AboutSysIns = new AboutSystem();
            AboutSysIns.Tag = "show";
            int ret = SendMessage(this.Handle, 0x000B, 0, 0);
            AboutSysIns.BringToFront();
            AboutSysIns.ShowDialog();
            ret = SendMessage(this.Handle, 0x000B, 1, 0);
            RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
            this.Focus();
        }
        //修改密码
        private void buttonItem11_Click(object sender, EventArgs e)
        {
            Frm_Pwd Frm_PwdIns = new Frm_Pwd();
            Frm_PwdIns.Tag = "show";
            int ret = SendMessage(this.Handle, 0x000B, 0, 0);
            Frm_PwdIns.BringToFront();
            Frm_PwdIns.ShowDialog();
            ret = SendMessage(this.Handle, 0x000B, 1, 0);
            RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
            this.Focus();
        }
        //锁屏
        private void buttonItem12_Click(object sender, EventArgs e)
        {
            Frm_LockScreen ins = new Frm_LockScreen();
            ins.ShowDialog();
        }
        //病人信息维护
        private void buttonItem16_Click(object sender, EventArgs e)
        {
            Form Frm_XtszIns = Application.OpenForms["Frm_ModityInfo"];
            if (Frm_XtszIns == null)
            {
                Frm_XtszIns = new Frm_ModityInfo();
                Frm_XtszIns.Owner = this;
                Frm_XtszIns.BringToFront();
                Frm_XtszIns.WindowState = FormWindowState.Normal;
                Frm_XtszIns.Show();
                Frm_XtszIns.Activate();

            }
            else
            {
                Frm_XtszIns.BringToFront();
                Frm_XtszIns.WindowState = FormWindowState.Normal;
                Frm_XtszIns.Activate();
            }
        }
    }
}
