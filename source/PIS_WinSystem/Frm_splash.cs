using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace PIS_Sys
{
    public partial class Frm_splash : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_splash()
        {
            InitializeComponent();
        }

        private void Frm_splash_Load(object sender, EventArgs e)
        {

            //版本信息
            Versiontxt.Text = String.Format("版本 {0}", ConfigurationManager.AppSettings["SysVersion"]);
            //版权信息
            Copyright.Text = AssemblyCompany + AssemblyCopyright + " " + DateTime.Now.Year;
            //显示授权信息
            Labsqinfo.Text = "系统正在验证授权信息……";
            //启用定时器
            TimerMain.Enabled = true;
        }
        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
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
        public void ReStart()
        {
            Program.frmMainins = null;
            //关闭即时消息
            Process[] Processes = Process.GetProcessesByName("WinImApp");
            IntPtr hWnd = IntPtr.Zero;
            foreach (Process p1 in Processes)
            {
                p1.Kill();
            }
            //质控应用
            Process[] Processes11 = Process.GetProcessesByName("ZhiKongIM");
            foreach (Process p1 in Processes11)
            {
                p1.Kill();
            }
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
        private void TimerMain_Tick(object sender, EventArgs e)
        {
            //禁用定时器
            TimerMain.Enabled = false;
            try
            {
                //获取Ftp服务器配置信息
                DBHelper.BLL.ftp_set_info insFtp = new DBHelper.BLL.ftp_set_info();
                DataTable dtFtp = insFtp.GetData();
                if (dtFtp != null && dtFtp.Rows.Count == 1)
                {
                    Program.FtpIP = dtFtp.Rows[0]["ftpip"].ToString();
                    Program.FtpPort = Convert.ToInt32(dtFtp.Rows[0]["ftpport"]);
                    Program.FtpUser = dtFtp.Rows[0]["ftpuser"].ToString();
                    Program.FtpPwd = dtFtp.Rows[0]["ftppwd"].ToString();
                }
                else
                {
                    Program.FileLog.Error("服务器数据库中Ftp服务器配置信息为空，请配置！");
                    MessageBox.Show("获取授权信息失败！系统将重启，请确认网络连接正常……", "敬告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReStart();
                }
                //获取当前医院信息
                DBHelper.BLL.sys_info InsSys = new DBHelper.BLL.sys_info();
                DataTable DtSys = InsSys.GetData();
                if (DtSys != null && DtSys.Rows.Count == 1)
                {
                    Program.System_Code = DtSys.Rows[0]["system_code"].ToString();
                    Program.HospitalName = DtSys.Rows[0]["system_hospital"].ToString();
                    string HospitalInfo = Program.Decrypt(Program.HospitalName, "12,@5353");
                    string[] infos = HospitalInfo.Split('|');
                    if (infos.Length == 4)
                    {
                        Program.HospitalName = infos[2];
                        if (!infos[3].Equals("1"))
                        {
                            DateTime DateTime1, DateTime2 = DateTime.Now;//现在时间
                            DateTime1 = Convert.ToDateTime(infos[1]); //设置要求的减的时间              
                            int dateDiff = 0;
                            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                            TimeSpan ts = ts1.Subtract(ts2).Duration();          //显示时间
                            dateDiff = ts.Days;
                            if (ts.Days <= 30 && ts.Days > 0)
                            {
                                string tjInfo = string.Format("系统{0}天内将停止服务！请速度联系厂商更换授权码！！！", ts.Days);
                                MessageBox.Show(tjInfo, "敬告", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            else if (ts.Days <= 0)
                            {
                                MessageBox.Show("系统服务周期已满;已经停止服务！请速度联系厂商获取授权码！！！", "敬告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                //强制资源回收
                                GC.Collect();
                                //关闭整个系统
                                System.Diagnostics.Process.GetCurrentProcess().Kill();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("获取授权信息失败！系统不能启动！！！", "敬告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReStart();
                    }
                    Program.sys_exe_name = DtSys.Rows[0]["sys_exe_name"].ToString();
                    Program.newversion = DtSys.Rows[0]["newversion"].ToString();
                    Program.oldversion = DtSys.Rows[0]["oldversion"].ToString();
                    if (DtSys.Rows[0]["update_date"].ToString().Length >= 18)
                    {
                        if (DtSys.Rows[0]["update_date"].ToString().Length >= 19)
                        {
                            Program.update_date = DtSys.Rows[0]["update_date"].ToString().Substring(0, 19);
                        }
                        else
                        {
                            Program.update_date = DtSys.Rows[0]["update_date"].ToString().Substring(0, 18);
                        }
                    }
                    Program.update_subDir = DtSys.Rows[0]["update_subDir"].ToString();
                    Program.process_name = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                    Program.update_info = DtSys.Rows[0]["update_info"].ToString();
                }
                //显示授权信息
                Labsqinfo.Text = "此系统使用权已经颁发给";
                Labyymc.Text = Program.HospitalName;
                if (!Program.HospitalName.Equals("医院名称"))
                {
                    //非常有用的VB函数，可以及时更新UI界面内容
                    Application.DoEvents();
                    //延迟供用户看到授权信息
                    System.Threading.Thread.Sleep(1000);
                    this.ShowInTaskbar = false;
                    this.Hide();
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
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("获取授权信息失败！系统将重启，请确认网络连接正常……", "敬告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReStart();
                }
            }
            catch (Exception ex)
            {
                Program.FileLog.Error("启动系统发生异常：" + ex.ToString());
                MessageBox.Show("启动系统发生异常：" + ex.ToString(), "敬告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ReStart();
            }
        }

    }
}
