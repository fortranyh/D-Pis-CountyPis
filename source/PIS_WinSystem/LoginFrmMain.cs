using DevComponents.DotNetBar;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace PIS_Sys
{
    public partial class LoginFrmMain : DevComponents.DotNetBar.Office2007Form
    {
        public LoginFrmMain()
        {
            InitializeComponent();
            //创建窗体句柄时发生
            this.HandleCreated += MainForm_HandleCreated;
            this.UsernameTextBox.GotFocus += new EventHandler(UsernameTextBox_GotFocus);
            UsernameTextBox.KeyPress += new KeyPressEventHandler(UsernameTextBox_KeyPress);
            PasswordTextBox.GotFocus += new EventHandler(PasswordTextBox_GotFocus);
            PasswordTextBox.KeyPress += new KeyPressEventHandler(PasswordTextBox_KeyPress);
            UsernameTextBox.Text = ConfigurationManager.AppSettings["LastUser"];
            timer1.Enabled = true;
        }
        private void UsernameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                PasswordTextBox.Focus();
            }
        }
        private void PasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                OK.PerformClick();
            }
        }
        private void PasswordTextBox_GotFocus(object sender, EventArgs e)
        {
            PasswordTextBox.SelectAll();
        }
        private void UsernameTextBox_GotFocus(object sender, EventArgs e)
        {
            UsernameTextBox.SelectAll();
        }
        public void MainForm_HandleCreated(Object sender, EventArgs e)
        {
            this.BringToFront();
        }
        //登录
        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (UsernameTextBox.Text.Trim().Length == 0)
                {
                    Frm_TJInfo("系统登录提示", "请输入用户名！");
                    UsernameTextBox.Focus();
                    return;
                }
                this.Cursor = Cursors.AppStarting;
                //开始登录
                DBHelper.BLL.sys_user ins = new DBHelper.BLL.sys_user();
                DataTable dt = ins.sysLoginCheck(UsernameTextBox.Text.Trim());
                this.Cursor = Cursors.Arrow;
                if (dt != null && dt.Rows.Count == 1)
                {
                    if (dt.Rows[0]["user_pwd"].ToString().Equals(PasswordTextBox.Text.Trim()))
                    {
                        //科室名称
                        Program.Dept_Name = dt.Rows[0]["dept_name"].ToString();
                        //科室编码
                        Program.Dept_Code = dt.Rows[0]["dept_code"].ToString();
                        //操作人员编码
                        Program.User_Code = dt.Rows[0]["user_code"].ToString();
                        //操作人员姓名
                        Program.User_Name = dt.Rows[0]["user_name"].ToString();
                        //操作人员级别
                        Program.user_role_code = Convert.ToInt16(dt.Rows[0]["user_role_code"]);
                        if (dt.Rows[0]["user_role"].ToString().Equals("系统超级管理员"))
                        {
                            Program.System_Admin = true;
                        }
                        else
                        {
                            Program.System_Admin = false;
                        }
                        if (checkBoxX1.Checked == true)
                        {
                            UpdateAppConfig("LastUser", UsernameTextBox.Text.Trim());
                        }
                        else
                        {
                            UpdateAppConfig("LastUser", "");
                        }
                        Program.User_Pwd = dt.Rows[0]["user_pwd"].ToString();
                        Frm_TJInfo("系统消息", "系统登录成功！", true);
                        //删除历史科内留言消息
                        DBHelper.BLL.dept_message insMsg = new DBHelper.BLL.dept_message();
                        if (insMsg.DeleteMessageInfo())
                        {
                            Program.FileLog.Info("删除历史科内留言消息成功");
                        }
                        //删除超出60天没有登记的申请
                        DBHelper.BLL.exam_master insMas = new DBHelper.BLL.exam_master();
                        int ret = insMas.DelteExamMaster60Days();
                        if (ret > 0)
                        {
                            Program.FileLog.Info("共删除申请未登记的检查超过60天的总数为：" + ret.ToString());
                        }

                        //检查升级 
                        string UpdateFilePath = Program.APPdirPath + @"\update.xml";
                        if (File.Exists(UpdateFilePath))
                        {
                            DataSet ds = new DataSet();
                            if (ConvertXmlToDataset(UpdateFilePath, ref ds))
                            {
                                if (ds != null && ds.Tables[0].Rows.Count == 1)
                                {
                                    ds.Tables[0].Rows[0]["system_code"] = Program.System_Code; ;
                                    ds.Tables[0].Rows[0]["server_ip"] = Program.FtpIP;
                                    ds.Tables[0].Rows[0]["server_port"] = Program.FtpPort;
                                    ds.Tables[0].Rows[0]["User"] = Program.FtpUser;
                                    ds.Tables[0].Rows[0]["Pwd"] = Program.FtpPwd;
                                    ds.Tables[0].Rows[0]["version"] = Program.newversion;
                                    ds.Tables[0].Rows[0]["update_date"] = Program.update_date;
                                    ds.Tables[0].Rows[0]["update_info"] = Program.update_info;
                                    ds.Tables[0].Rows[0]["update_subDir"] = Program.update_subDir;
                                    ds.Tables[0].Rows[0]["system_name"] = "PIS_Sys";
                                    string oldVerison = ConfigurationManager.AppSettings["SysVersion"];
                                    if (oldVerison.Equals(""))
                                    {
                                        ds.Tables[0].Rows[0]["curversion"] = Program.oldversion;
                                        oldVerison = Program.oldversion;
                                    }
                                    else
                                    {
                                        ds.Tables[0].Rows[0]["curversion"] = oldVerison;
                                    }
                                    ds.Tables[0].Rows[0]["strProcessName"] = Program.process_name;
                                    ds.WriteXml(UpdateFilePath, XmlWriteMode.IgnoreSchema);
                                    try
                                    {
                                        Version NewVersion = Version.Parse(Program.newversion);
                                        Version OldVersion = Version.Parse(oldVerison);
                                        if (NewVersion > OldVersion)
                                        {
                                            //关闭即时消息
                                            Process[] Processes = Process.GetProcessesByName("WinImApp");
                                            IntPtr hWnd = IntPtr.Zero;
                                            foreach (Process p in Processes)
                                            {
                                                p.Kill();
                                            }
                                            //开始升级
                                            UpdateAppConfig("SysVersion", Program.newversion);
                                            PublicVbLIb.PublicFunc.InvokeExe(Program.APPdirPath, "update.exe", Microsoft.VisualBasic.Strings.Chr(34) + UpdateFilePath + Microsoft.VisualBasic.Strings.Chr(34), true);
                                            //退出当前进程
                                            System.Diagnostics.Process.GetCurrentProcess().Kill();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Frm_TJInfo("升级失败", "系统自动升级异常：\n" + ex.ToString());
                                    }
                                }
                                ds = null;
                            }
                        }
                        this.ShowInTaskbar = false;
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        PasswordTextBox.Text = "";
                        PasswordTextBox.Focus();
                        Frm_TJInfo("密码错误", "系统登录失败！\n请再次输入密码。\n");
                        return;
                    }
                }
                else
                {
                    UsernameTextBox.SelectionStart = UsernameTextBox.Text.Length;
                    UsernameTextBox.Focus();
                    Frm_TJInfo("用户错误", "系统登录失败！\n请确认输入的用户名或者编码正确。\n");
                    return;
                }


            }
            catch (Exception ex)
            {
                Frm_TJInfo("系统错误", ex.Message.ToString());
                DialogResult = DialogResult.Cancel;
            }

        }
        //转换为dataset
        public static bool ConvertXmlToDataset(string xmlPath, ref System.Data.DataSet ds)
        {
            bool ConvertXmlToDataset;
            try
            {
                FileStream fsReadXml = new FileStream(xmlPath, FileMode.Open);
                XmlTextReader myXmlReader = new XmlTextReader(fsReadXml);
                ds.ReadXml(myXmlReader);
                myXmlReader.Close();
                fsReadXml.Close();
                ConvertXmlToDataset = true;
            }
            catch (Exception expr_22)
            {
                Program.FileLog.Error(expr_22.ToString());
                ConvertXmlToDataset = false;
            }
            return ConvertXmlToDataset;
        }
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
        //取消
        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void LoginFrmMain_Load(object sender, EventArgs e)
        {
            this.Activate();
        }

        //提示窗体
        public void Frm_TJInfo(string Title, string B_info, Boolean DelayFlag = false)
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
            if (DelayFlag)
            {
                System.Threading.Thread.Sleep(500);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            if (UsernameTextBox.Text.Trim().Length > 0)
            {

                PasswordTextBox.Focus();

                this.checkBoxX1.Checked = true;
            }
            else
            {

                UsernameTextBox.Focus();

                this.checkBoxX1.Checked = false;
            }
        }

    }
}
