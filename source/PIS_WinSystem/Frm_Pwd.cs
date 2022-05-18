using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys
{
    public partial class Frm_Pwd : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Pwd()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text.Trim().Equals("") || textBoxX2.Text.Trim().Equals("") || textBoxX3.Text.Trim().Equals(""))
            {
                Frm_TJInfo(this.Text, "密码信息都不允许为空！");
                return;
            }
            if (!textBoxX2.Text.Trim().Equals("").Equals(textBoxX3.Text.Trim().Equals("")))
            {
                Frm_TJInfo(this.Text, "两次输入新密码不一样！");
                return;
            }
            //
            DBHelper.BLL.sys_user ins = new DBHelper.BLL.sys_user();
            DataTable dt = ins.sysLoginCheck(Program.User_Code);
            this.Cursor = Cursors.Arrow;
            if (dt != null && dt.Rows.Count == 1)
            {
                if (dt.Rows[0]["user_pwd"].ToString().Equals(textBoxX1.Text.Trim()))
                {
                    if (ins.UpdateUserPwd(Program.User_Code, textBoxX2.Text.Trim()))
                    {
                        Frm_TJInfo(this.Text, "密码修改成功！", true);
                        Program.User_Pwd = textBoxX2.Text.Trim();
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        Frm_TJInfo(this.Text, "密码修改失败！");
                        return;
                    }
                }
                else
                {
                    Frm_TJInfo(this.Text, "旧密码输入错误！");
                    return;
                }
            }
        }

        private void Frm_Pwd_Load(object sender, EventArgs e)
        {

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
        //
        private void Frm_Pwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            //用回车代替Tab
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
    }
}
