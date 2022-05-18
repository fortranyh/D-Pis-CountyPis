using DevComponents.DotNetBar;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.ksgl
{
    public partial class Frm_UserAdd : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_UserAdd()
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text.Trim().Equals("") || textBoxX2.Text.Trim().Equals("") || textBoxX3.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "新用户信息不允许为空！");
                return;
            }
            int role_code = 6;
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
            //执行插入
            DBHelper.BLL.sys_user ins = new DBHelper.BLL.sys_user();
            if (ins.QueryUserCode(textBoxX2.Text.Trim()) == 0)
            {
                if (ins.InsertUser(textBoxX1.Text.Trim(), textBoxX2.Text.Trim(), textBoxX3.Text.Trim(), Program.Dept_Code, comboBox1.Text, role_code, gb_flag))
                {

                    Frm_TJInfo("提示", "添加成功！");
                    ins.InsertDoctor(textBoxX1.Text.Trim(), textBoxX2.Text.Trim(), Program.Dept_Code, comboBox1.Text);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    Frm_TJInfo("提示", "添加失败！");
                    return;
                }
            }
            else
            {
                Frm_TJInfo("提示", "新用户用户编码已经存在，请重新输入一个！");
                return;
            }
        }

        private void Frm_UserAdd_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }
    }
}
