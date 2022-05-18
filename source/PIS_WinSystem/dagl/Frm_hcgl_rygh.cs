using DevComponents.DotNetBar;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_hcgl_rygh : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_hcgl_rygh()
        {
            InitializeComponent();
        }

        private void Frm_hcgl_rygh_Load(object sender, EventArgs e)
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
            if (textBox1.Text.Trim().Equals("") || textBox2.Text.Trim().Equals("") || textBox3.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "物品名称、剂量、签名不能为空！");
                return;
            }
            DBHelper.BLL.hcgl ins = new DBHelper.BLL.hcgl();
            if (ins.AddInfoRygh(textBox1.Text.Trim(), textBox2.Text.Trim(), dtStart.Value.ToString("yyyy-MM-dd HH:mm:ss"), textBox3.Text.Trim(), textBox4.Text.Trim()) == 1)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void Frm_hcgl_rygh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
    }
}
