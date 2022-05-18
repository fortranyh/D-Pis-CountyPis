using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_AddJydj : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_AddJydj()
        {
            InitializeComponent();
        }
        //添加
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (!textBoxX1.Text.Trim().Equals(""))
            {
                DBHelper.BLL.Jydjb ins = new DBHelper.BLL.Jydjb();
                if (ins.AddInfo(textBoxX1.Text.Trim(), textBoxX2.Text.Trim(), textBoxX3.Text.Trim(), textBoxX4.Text.Trim(), textBoxX6.Text.Trim(), textBoxX7.Text.Trim(), dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"), textBoxX5.Text.Trim(), textBoxX8.Text.Trim(), textBoxX9.Text.Trim()) == 1)
                {
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void Frm_AddJydj_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void textBoxX1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                string str_tj = textBoxX1.Text.Trim().Replace("'", "");
                if (!str_tj.Equals(""))
                {
                    //
                    DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                    DataTable dt = ins.GetDt("select patient_name from exam_master_view where study_no='" + str_tj + "'");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        textBoxX2.Text = dt.Rows[0]["patient_name"].ToString();
                        textBoxX2.Focus();
                    }
                }
            }
        }

        private void Frm_AddJydj_KeyPress(object sender, KeyPressEventArgs e)
        {

            //用回车代替Tab
            if (e.KeyChar == 13)
            {
                e.Handled = false;
                SendKeys.Send("{TAB}");
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text.Trim() != "" && textBoxX2.Text.Trim() != "" && textBoxX7.Text.Trim() != "")
            {
                FastReportLib.Print_Jyd insM = new FastReportLib.Print_Jyd();
                insM.BLH = textBoxX1.Text.Trim();
                insM.name = textBoxX2.Text.Trim();
                insM.count = textBoxX3.Text.Trim();
                insM.byzd = textBoxX4.Text.Trim();
                insM.hzyy = textBoxX5.Text.Trim();
                insM.yj = textBoxX6.Text.Trim();
                insM.jcr = textBoxX7.Text.Trim();
                insM.qt = textBoxX8.Text.Trim();
                insM.bz = textBoxX9.Text.Trim();
                insM.datetime = dateTimePicker1.Text.Trim();
                if (FastReportLib.Print_Jydjd.PrintJydjd(insM, PIS_Sys.Properties.Settings.Default.ReportPrinter))
                {
                    Frm_TJInfo("提示", "打印成功！");
                }
            }
            else
            {
                Frm_TJInfo("提示", "病理号、姓名、借出人不能为空！");
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

    }
}
