using DevComponents.DotNetBar;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class FrmYsRegister
    {
        public FrmYsRegister()
        {
            InitializeComponent();
        }
        public string exam_no = "";
        public string bgys = "";
        public string shys = "";
        public string patient_name = "";
        public string study_no = "";
        private void FrmYsRegister_Load(object sender, EventArgs e)
        {
            txtBgys.Text = bgys;
            txtShys.Text = shys;
            this.Text += string.Format("—病理号：{0}，病人姓名：{1}", study_no, patient_name);
            this.BringToFront();
        }
        //查看时间轴
        private void buttonX1_Click(object sender, EventArgs e)
        {
            Form Frm_SjzIns = Application.OpenForms["Frm_Sjz"];
            if (Frm_SjzIns != null)
            {
                Frm_SjzIns.Close();
            }
            Application.DoEvents();
            Frm_SjzIns = new Frm_Sjz();
            Frm_SjzIns.BringToFront();
            Frm_SjzIns.WindowState = FormWindowState.Normal;
            Frm_Sjz.exam_no = exam_no;
            Frm_Sjz.patient_name = patient_name;
            Frm_Sjz.study_no = study_no;
            Frm_SjzIns.Owner = this;
            Frm_SjzIns.Show();
            Frm_SjzIns.BringToFront();
        }
        //提示窗体
        public void Frm_TJInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(this);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = true;
            Frm_AlertIns.AutoCloseTimeOut = 2;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }
        //保存延时原因
        int CloseFrmFlag = 0;
        private void buttonX2_Click(object sender, EventArgs e)
        {
            string delay_reason = "";
            for (int i = 0; i < groupPanel1.Controls.Count; i++)
            {
                if (groupPanel1.Controls[i].GetType().Name.Equals("CheckBoxX"))
                {
                    DevComponents.DotNetBar.Controls.CheckBoxX chk = (DevComponents.DotNetBar.Controls.CheckBoxX)groupPanel1.Controls[i];
                    if (chk.Checked)
                    {
                        delay_reason = chk.Text;
                    }
                }
            }
            if (delay_reason.Equals(""))
            {
                if (txtQt.Text.Equals(""))
                {
                    Frm_TJInfo("提示", "请选择或者输入报告迟发原因！");
                    return;
                }
                else
                {
                    delay_reason = txtQt.Text.Trim();
                }
            }

            DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
            int index = ins.UpdateDelay_reason(study_no, delay_reason);
            if (index > 0)
            {
                CloseFrmFlag = 1;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void FrmYsRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseFrmFlag != 1)
            {
                Frm_TJInfo("提示", "请选择或者输入报告迟发原因！");
                e.Cancel = true;
            }
        }
    }
}
