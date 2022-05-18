using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class FrmLcgt : DevComponents.DotNetBar.Office2007Form
    {
        public FrmLcgt()
        {
            InitializeComponent();
        }
        public string lcdept = "";
        public string lcdoc = "";
        public string bldoc = "";
        public string study_no = "";

        private void FrmLcgt_Load(object sender, EventArgs e)
        {
            DBHelper.BLL.exam_lcys_im ins = new DBHelper.BLL.exam_lcys_im();
            DataTable dt = ins.GetData(study_no);
            if (dt.Rows.Count == 1)
            {
                txtBlys.Text = dt.Rows[0]["im_bl_doc"].ToString();
                txtLcks.Text = dt.Rows[0]["im_lc_dept"].ToString();
                txtLcys.Text = dt.Rows[0]["im_lc_doc"].ToString();
                dtGtsj.Value = Convert.ToDateTime(dt.Rows[0]["im_time"].ToString());
                txtGtnr.Text = dt.Rows[0]["im_info"].ToString();
            }
            else
            {
                txtBlys.Text = bldoc;
                txtLcks.Text = lcdept;
                txtLcys.Text = lcdoc;
                dtGtsj.Value = DateTime.Now;
            }
            txtGtnr.Focus();
            this.BringToFront();
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
        //保存
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (txtBlys.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "病理医生不能为空！");
                return;
            }
            if (txtLcys.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "临床医生不能为空！");
                return;
            }
            if (txtGtnr.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "沟通内容不能为空！");
                return;
            }
            DBHelper.Model.exam_lcgt_model insM = new DBHelper.Model.exam_lcgt_model();
            insM.study_no = study_no;
            insM.im_bl_doc = txtBlys.Text.Trim();
            insM.im_lc_dept = txtLcks.Text.Trim();
            insM.im_lc_doc = txtLcys.Text.Trim();
            insM.im_time = dtGtsj.Value.ToString("yyyy-MM-dd HH:mm:ss");
            insM.im_info = txtGtnr.Text.Trim();
            DBHelper.BLL.exam_lcys_im ins = new DBHelper.BLL.exam_lcys_im();
            Boolean flag = ins.SaveLcgt(insM);
            if (flag)
            {
                Frm_TJInfo("提示", "保存成功！");
                Application.DoEvents();
                System.Threading.Thread.Sleep(500);
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                Frm_TJInfo("提示", "保存失败！");
            }
        }
    }
}
