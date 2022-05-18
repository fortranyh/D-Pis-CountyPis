using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;

namespace PathologyClient
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
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
            //显示登录窗体
            LoginFrmMain frmLogin = new LoginFrmMain();
            frmLogin.BringToFront();
            DialogResult rt = frmLogin.ShowDialog();
            if (rt == DialogResult.Cancel)
            {
                frmLogin.ShowInTaskbar = false;
                frmLogin.Close();
                frmLogin.Dispose();
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
            {
                frmLogin.ShowInTaskbar = false;
                frmLogin.Close();
                frmLogin.Dispose();
            }

            InitializeComponent();
            //
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            //给MDI 父窗体添加背景和解决闪烁的问题
            BackgroundNoSplash();
            //修改日期格式
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();
            Program.frmMainins = this;
            //创建窗体句柄时发生
            this.HandleCreated += MainForm_HandleCreated;

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
        private void OnMdiClientPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(Properties.Resources.bg, new Rectangle(0, 0, mdiClient.Width, mdiClient.Height));
            string msg = AssemblyProduct + "{" + Assembly.GetEntryAssembly().GetName().Version + "}";
            SizeF size = e.Graphics.MeasureString(msg, this.Font);
            g.DrawString(msg, this.Font, new SolidBrush(Color.DodgerBlue), mdiClient.Width - size.Width, mdiClient.Height - size.Height);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            buttonItem1.Visible = false;
            BtnOldForColor = this.buttonItem2.ForeColor;
            //当前登录用户
            this.Text = string.Format("病理诊断中心客户端—【{0}-{1}】", Program.HospitalName, Program.User_Code);
            this.BringToFront();
            string xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetExamStatusDic", null);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    //添加颜色表
                    Program.exam_status_dic[ds.Tables[0].Rows[i]["status_code"].ToString()] = Color.FromArgb(Convert.ToInt32(ds.Tables[0].Rows[i]["status_color"].ToString()));
                    Program.exam_status_name_dic[ds.Tables[0].Rows[i]["status_name"].ToString()] = Color.FromArgb(Convert.ToInt32(ds.Tables[0].Rows[i]["status_color"].ToString()));
                }
            }
        }
        public void MainForm_HandleCreated(Object sender, EventArgs e)
        {
            Program.frmMainins.BringToFront();
        }
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;

                int ret = SendMessage(this.Handle, 0x000B, 0, 0);
                if (TaskDialog.Show("病理诊断中心客户端", "确认", "确定要退出本系统么？", Curbutton) == eTaskDialogResult.Ok)
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
                }
                else
                {
                    ret = SendMessage(this.Handle, 0x000B, 1, 0);
                    RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
                    e.Cancel = true;
                }
            }
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

        //关于本系统
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
        public void BtnForColorSet(ButtonItem BtnIns)
        {
            //绑定事件处理
            for (int i = 0; i < this.bar1.Items.Count; i++)
            {
                ButtonItem BtnItem = ((ButtonItem)this.bar1.Items[i]);
                BtnItem.ForeColor = BtnOldForColor;
            }
            if (BtnIns != null)
            {
                BtnIns.ForeColor = BtnForColor;
            }
        }
        private Color BtnForColor = Color.Blue;
        private Color BtnOldForColor;
        //标本登记
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            ButtonItem BtnIns = (ButtonItem)sender;
            BtnForColorSet(BtnIns);
            if (!isExist("FrmJcyy"))
            {
                FrmJcyy FrmBbdjIns = new FrmJcyy();
                FrmBbdjIns.Tag = "show";
                FrmBbdjIns.MdiParent = this;
                int ret = SendMessage(this.Handle, 0x000B, 0, 0);

                FrmBbdjIns.WindowState = FormWindowState.Maximized;
                FrmBbdjIns.BringToFront();
                FrmBbdjIns.Show();
                ret = SendMessage(this.Handle, 0x000B, 1, 0);
                RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
            }
        }
        //取材信息
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            ButtonItem BtnIns = (ButtonItem)sender;
            BtnForColorSet(BtnIns);
            if (!isExist("Frm_Qcxx"))
            {
                Frm_Qcxx FrmqcIns = new Frm_Qcxx();
                FrmqcIns.Tag = "show";
                FrmqcIns.MdiParent = this;
                int ret = SendMessage(this.Handle, 0x000B, 0, 0);

                FrmqcIns.WindowState = FormWindowState.Maximized;
                FrmqcIns.BringToFront();
                FrmqcIns.Show();
                ret = SendMessage(this.Handle, 0x000B, 1, 0);
                RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
            }
        }
        //制片信息 
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            ButtonItem BtnIns = (ButtonItem)sender;
            BtnForColorSet(BtnIns);
            if (!isExist("Frm_Qpxx"))
            {
                Frm_Qpxx Frm_QpxxIns = new Frm_Qpxx();
                Frm_QpxxIns.Tag = "show";
                Frm_QpxxIns.MdiParent = this;
                int ret = SendMessage(this.Handle, 0x000B, 0, 0);

                Frm_QpxxIns.WindowState = FormWindowState.Maximized;
                Frm_QpxxIns.BringToFront();
                Frm_QpxxIns.Show();
                ret = SendMessage(this.Handle, 0x000B, 1, 0);
                RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
            }
        }
        //病理报告
        private void buttonItem6_Click(object sender, EventArgs e)
        {
            ButtonItem BtnIns = (ButtonItem)sender;
            BtnForColorSet(BtnIns);
            if (!isExist("Frm_ReportQuery"))
            {
                Frm_ReportQuery FrmBlzdIns = new Frm_ReportQuery();
                FrmBlzdIns.Tag = "show";
                FrmBlzdIns.MdiParent = this;
                int ret = SendMessage(this.Handle, 0x000B, 0, 0);
                FrmBlzdIns.WindowState = FormWindowState.Maximized;
                FrmBlzdIns.BringToFront();
                FrmBlzdIns.Show();
                ret = SendMessage(this.Handle, 0x000B, 1, 0);
                RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
            }
        }
        //系统设置
        private void buttonItem13_Click(object sender, EventArgs e)
        {
            Form FrmSetIns = Application.OpenForms["FrmSet"];
            if (FrmSetIns == null)
            {
                FrmSetIns = new FrmSet();
                FrmSetIns.Owner = this;
                FrmSetIns.WindowState = FormWindowState.Normal;
                FrmSetIns.BringToFront();
                FrmSetIns.Show();

            }
            else
            {
                FrmSetIns.WindowState = FormWindowState.Normal;
                FrmSetIns.BringToFront();
                FrmSetIns.Activate();
            }
        }
        //特检医嘱
        private void buttonItem5_Click(object sender, EventArgs e)
        {
            ButtonItem BtnIns = (ButtonItem)sender;
            BtnForColorSet(BtnIns);
            if (!isExist("Frm_tjyz"))
            {
                Frm_tjyz Frm_tjyzIns = new Frm_tjyz();
                Frm_tjyzIns.Tag = "show";
                Frm_tjyzIns.MdiParent = this;
                int ret = SendMessage(this.Handle, 0x000B, 0, 0);
                Frm_tjyzIns.WindowState = FormWindowState.Maximized;
                Frm_tjyzIns.BringToFront();
                Frm_tjyzIns.Show();
                ret = SendMessage(this.Handle, 0x000B, 1, 0);
                RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
            }
        }

        private void BtnRelogin_Click(object sender, EventArgs e)
        {
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

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            if (!isExist("FrmPatCardList"))
            {
                FrmPatCardList FrmPatCardIns = new FrmPatCardList();
                FrmPatCardIns.Tag = "show";
                FrmPatCardIns.MdiParent = this;
                int ret = SendMessage(this.Handle, 0x000B, 0, 0);
                FrmPatCardIns.WindowState = FormWindowState.Maximized;
                FrmPatCardIns.BringToFront();
                FrmPatCardIns.Show();
                ret = SendMessage(this.Handle, 0x000B, 1, 0);
                RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, 0x0491);
            }
        }
    }
}
