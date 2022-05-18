using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.ksgl
{
    public partial class Frm_UserUpdate : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_UserUpdate()
        {
            InitializeComponent();
        }
        public string user_code;
        private void Frm_UserUpdate_Load(object sender, EventArgs e)
        {
            DBHelper.BLL.sys_user ins = new DBHelper.BLL.sys_user();
            DataTable dt = ins.sysLoginCheck(user_code);
            if (dt != null)
            {
                textBoxX1.Text = dt.Rows[0]["user_name"].ToString();
                textBoxX2.Text = dt.Rows[0]["user_code"].ToString();
                textBoxX3.Text = dt.Rows[0]["user_pwd"].ToString();
                comboBox1.Text = dt.Rows[0]["user_role"].ToString();
                chk_gbFlag.Checked = dt.Rows[0]["gb_flag"].ToString().Equals("1") ? true : false;
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
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text.Trim().Equals("") || textBoxX2.Text.Trim().Equals("") || textBoxX3.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "用户信息不允许为空！");
                return;
            }
            int role_code = 9;
            if (comboBox1.SelectedIndex == 0)
            {
                role_code = 6;
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                role_code = 7;
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                role_code = 8;
            }
            int gb_flag = chk_gbFlag.Checked == true ? 1 : 0;
            //执行更新
            DBHelper.BLL.sys_user ins = new DBHelper.BLL.sys_user();
            if (ins.QueryUserCode(textBoxX2.Text.Trim()) == 1)
            {
                if (ins.UpdateUser(textBoxX2.Text.Trim(), textBoxX3.Text.Trim(), comboBox1.Text, role_code, gb_flag))
                {
                    Frm_TJInfo("提示", "更新成功！");
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    Frm_TJInfo("提示", "更新失败！");
                    return;
                }
            }
            else
            {
                Frm_TJInfo("提示", "当前用户不存在！");
                DialogResult = DialogResult.OK;
            }
        }
    }
}
