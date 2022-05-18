using DevComponents.DotNetBar;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_hcgl_yryb : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_hcgl_yryb()
        {
            InitializeComponent();
        }

        private void Frm_hcgl_yryb_Load(object sender, EventArgs e)
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

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (textBox5.Text.Trim().Equals("") || textBox6.Text.Trim().Equals("") || textBox1.Text.Trim().Equals("") || textBox3.Text.Trim().Equals("") || textBox2.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "信息不能为空！");
                return;
            }
            DBHelper.BLL.hcgl ins = new DBHelper.BLL.hcgl();
            if (ins.AddInfoYryb(dtStart.Value.ToString("yyyy-MM-dd HH:mm:ss"), textBox5.Text.Trim(), textBox6.Text.Trim(), dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"), textBox1.Text.Trim(), textBox3.Text.Trim(), textBox2.Text.Trim(), textBox7.Text.Trim()) == 1)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void Frm_hcgl_yryb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
    }
}
