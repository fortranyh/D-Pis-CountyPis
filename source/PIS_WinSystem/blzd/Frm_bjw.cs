using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_bjw : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_bjw()
        {
            InitializeComponent();
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
        //标记物名称
        public string bjw_name = "";
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (!textBoxX1.Text.Trim().Equals("") && !comboBoxEx1.Text.Trim().Equals(""))
            {
                DBHelper.BLL.tjbjw ins = new DBHelper.BLL.tjbjw();
                if (ins.GetBjwCount(textBoxX1.Text.Trim()) == 0)
                {
                    if (ins.InsertBjw(textBoxX1.Text.Trim(), comboBoxEx1.Text.Trim(), textBoxX2.Text.Trim(), textBoxX3.Text.Trim()) == 1)
                    {
                        bjw_name = textBoxX1.Text.Trim();
                        DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    Frm_TJInfo("添加失败", "标记物已经存在！");
                }
            }
            else
            {
                Frm_TJInfo("验证失败", "标记物名称和类型不能为空！");
            }
        }

        private void Frm_bjw_Load(object sender, EventArgs e)
        {
            DBHelper.BLL.tjbjw ins = new DBHelper.BLL.tjbjw();
            DataSet ds = ins.GetDstaocan_type();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                comboBoxEx1.DataSource = ds.Tables[0];
                comboBoxEx1.DisplayMember = "taocan_type_name";
                comboBoxEx1.ValueMember = "id";
                comboBoxEx1.SelectedIndex = 0;
            }
            else
            {
                comboBoxEx1.DataSource = null;
            }
        }
    }
}
