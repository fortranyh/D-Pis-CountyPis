using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PathologyClient
{
    public partial class FrmRelogin : DevComponents.DotNetBar.Office2007Form
    {
        public FrmRelogin()
        {
            InitializeComponent();
        }

        private void FrmRelogin_Load(object sender, EventArgs e)
        {
            this.textBoxX2.Text = ConfigurationManager.AppSettings["hospital_code"];
            this.textBoxX1.Text = ConfigurationManager.AppSettings["ServicesUrl"];
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("hospital_code", this.textBoxX2.Text.Trim());
            string RetStr = PublicBaseLib.PostWebService.PostCallWebServiceForTxt(string.Format("{0}{1}", this.textBoxX1.Text.Trim(), "ClientWebService.asmx"), "GetThirdHospitalXmlStr", parameters);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(RetStr));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Frm_TJInfo("提示", "连接成功！");
            }
            else
            {
                Frm_TJInfo("提示", "连接失败！");
            }
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
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (!this.textBoxX1.Text.Trim().Equals("") && !this.textBoxX2.Text.Trim().Equals(""))
            {
                UpdateAppConfig("ServicesUrl", this.textBoxX1.Text.Trim());
                UpdateAppConfig("hospital_code", this.textBoxX2.Text.Trim());
                this.DialogResult = DialogResult.OK;
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
    }
}
