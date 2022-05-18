using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;

namespace PathologyClient
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
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
                //开始登录
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                //组织post请求参数
                parameters.Add("user_code", UsernameTextBox.Text.Trim());
                parameters.Add("user_pwd", PasswordTextBox.Text.Trim());
                parameters.Add("hospital_code", ConfigurationManager.AppSettings["hospital_code"]);
                string RetStr = PublicBaseLib.PostWebService.PostCallWebServiceForTxt(Program.WebServerUrl, "LoginSys", parameters);
                if (RetStr.Equals("1"))
                {
                    //操作人员编码
                    Program.User_Code = UsernameTextBox.Text.Trim();
                    parameters.Clear();
                    parameters.Add("hospital_code", ConfigurationManager.AppSettings["hospital_code"]);
                    RetStr = PublicBaseLib.PostWebService.PostCallWebServiceForTxt(Program.WebServerUrl, "GetThirdHospitalXmlStr", parameters);
                    DataSet ds = new DataSet();
                    ds.ReadXml(new StringReader(RetStr));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        Program.HospitalName = ds.Tables[0].Rows[0]["hospital_name"].ToString();
                        Program.H_Pre_Char = ds.Tables[0].Rows[0]["pre_char"].ToString();
                        if (Program.H_Pre_Char.Equals(""))
                        {
                            Application.Exit();
                        }
                    }
                    else
                    {
                        Application.Exit();
                    }
                    if (checkBoxX1.Checked == true)
                    {
                        UpdateAppConfig("LastUser", UsernameTextBox.Text.Trim());
                    }
                    else
                    {
                        UpdateAppConfig("LastUser", "");
                    }
                    Frm_TJInfo("系统消息", "系统登录成功！", true);
                    this.ShowInTaskbar = false;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    UsernameTextBox.SelectionStart = UsernameTextBox.Text.Length;
                    UsernameTextBox.Focus();
                    Frm_TJInfo("用户错误", "系统登录失败！\n请确认输入正确。\n");
                    return;
                }
            }
            catch (Exception ex)
            {
                Frm_TJInfo("系统错误", ex.Message.ToString());
                DialogResult = DialogResult.Cancel;
            }
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
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
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
            //更改系统时间格式
            SetComputerDateTime insDateTimeFormat = new SetComputerDateTime();
            insDateTimeFormat.SetDateTimeFormat();
            Application.DoEvents();
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

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            FrmRelogin ins = new FrmRelogin();
            if (ins.ShowDialog() == DialogResult.OK)
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
        }
        //同时按下ALT+CTRL+F1打开设置窗体
        private void LoginFrmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt && e.Control && e.KeyCode == Keys.F1)
            {
                PictureBox2_Click(null, null);
            }
        }
    }
}
