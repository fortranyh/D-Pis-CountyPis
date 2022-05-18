using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_edit_bjw : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_edit_bjw(int id)
        {
            InitializeComponent();
            curid = id;
        }
        private int curid;
        private void Frm_edit_bjw_Load(object sender, EventArgs e)
        {
            string sqlstr = "select id,bjw_name,bjw_type,rs_code,rs_name from tj_bjw_dict where id=" + curid.ToString();
            DBHelper.BLL.tjbjw ins = new DBHelper.BLL.tjbjw();
            DataSet ds = ins.GetBjwInfo(sqlstr);
            if (ds != null && ds.Tables[0].Rows.Count == 1)
            {
                textBoxX1.Text = ds.Tables[0].Rows[0]["bjw_name"].ToString();
                textBoxX4.Text = ds.Tables[0].Rows[0]["bjw_type"].ToString();
                textBoxX2.Text = ds.Tables[0].Rows[0]["rs_name"].ToString();
                textBoxX3.Text = ds.Tables[0].Rows[0]["rs_code"].ToString();
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
            if (!textBoxX2.Text.Trim().Equals("") && !textBoxX3.Text.Trim().Equals(""))
            {
                DBHelper.BLL.tjbjw ins = new DBHelper.BLL.tjbjw();
                int ret = ins.UpdateBjwInfo(textBoxX3.Text.Trim(), textBoxX2.Text.Trim(), curid);
                if (ret == 1)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    Frm_TJInfo("提示", "编辑失败！");
                }
            }
            else
            {
                Frm_TJInfo("编辑验证", "染色编码和名称都不能为空！");
            }
        }
    }
}
