using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

namespace PIS_Sys.xtsz
{
    public partial class Frm_smfjk : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_smfjk()
        {
            InitializeComponent();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            Frm_Xtsz xtsz_ins = this.Owner as Frm_Xtsz;
            //包埋盒打号机
            if (checkBoxX1.Checked == true)
            {
                UpdateAppConfig("sfmbmh_enable", "1");
                if (!textBoxX1.Text.Trim().Equals("") || !Directory.Exists(textBoxX1.Text.Trim()))
                {
                    UpdateAppConfig("sfmbmh_path", textBoxX1.Text.Trim());
                }
                else
                {
                    xtsz_ins.Frm_TJInfo("提示", "必须设置接口路径！");
                    return;
                }
            }
            else
            {
                UpdateAppConfig("sfmbmh_enable", "0");
            }
            //玻片打号机
            if (checkBoxX2.Checked == true)
            {
                UpdateAppConfig("sfmbp_enable", "1");
                if (!textBoxX2.Text.Trim().Equals("") || !Directory.Exists(textBoxX2.Text.Trim()))
                {
                    UpdateAppConfig("sfmbp_path", textBoxX2.Text.Trim());
                }
                else
                {
                    xtsz_ins.Frm_TJInfo("提示", "必须设置接口路径！");
                    return;
                }
            }
            else
            {
                UpdateAppConfig("sfmbp_enable", "0");
            }

            xtsz_ins.Frm_TJInfo("提示", "接口设置成功");
            System.Threading.Thread.Sleep(500);
            this.Close();
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxX1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxX2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void Frm_smfjk_Load(object sender, EventArgs e)
        {
            //打号机模式
            if (ConfigurationManager.AppSettings["sfmbmh_enable"] != null)
            {
                if (ConfigurationManager.AppSettings["sfmbmh_enable"].ToString().Equals("1"))
                {
                    checkBoxX1.Checked = true;
                    string bmh_path = ConfigurationManager.AppSettings["sfmbmh_path"].ToString().Trim();
                    if (Directory.Exists(bmh_path) == true)
                    {
                        textBoxX1.Text = bmh_path;
                    }
                }
                else
                {
                    checkBoxX1.Checked = false;
                }
            }
            if (ConfigurationManager.AppSettings["sfmbp_enable"] != null)
            {
                if (ConfigurationManager.AppSettings["sfmbp_enable"].ToString().Equals("1"))
                {
                    checkBoxX2.Checked = true;
                    string bp_path = ConfigurationManager.AppSettings["sfmbp_path"].ToString().Trim();
                    if (Directory.Exists(bp_path) == true)
                    {
                        textBoxX2.Text = bp_path;
                    }
                }
                else
                {
                    checkBoxX2.Checked = false;
                }
            }


        }
    }
}
