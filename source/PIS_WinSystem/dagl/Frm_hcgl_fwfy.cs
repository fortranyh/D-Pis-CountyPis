using DevComponents.DotNetBar;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_hcgl_fwfy : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_hcgl_fwfy()
        {
            InitializeComponent();
        }

        private void Frm_hcgl_fwfy_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
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
            if (textBox11.Text.Trim().Equals("") || textBox7.Text.Trim().Equals("") || textBox1.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "物品名称、执行者、回收者不能为空！");
                return;
            }
            DBHelper.BLL.hcgl ins = new DBHelper.BLL.hcgl();
            if (ins.AddInfoFwfy(textBox11.Text.Trim(), textBox10.Text.Trim(), textBox5.Text.Trim(), dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"), textBox6.Text.Trim(), textBox7.Text.Trim(), textBox1.Text.Trim(), textBox9.Text.Trim()) == 1)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void Frm_hcgl_fwfy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
    }
}
