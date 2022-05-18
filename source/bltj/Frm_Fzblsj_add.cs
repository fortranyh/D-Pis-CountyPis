using System;
using System.Windows.Forms;

namespace PIS_WinSystem.dagl
{
    public partial class Frm_Fzblsj_add : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Fzblsj_add()
        {
            InitializeComponent();
        }

        private void Frm_Fzblsj_add_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            if (textBox8.Text.Trim().Equals("") || comboBox1.Text.Trim().Equals(""))
            {
                MessageBox.Show("提示", "项目名称、项目结果不能为空！");
                return;
            }
            DBHelper.BLL.Sjzk_info ins = new DBHelper.BLL.Sjzk_info();
            if (ins.AddInfoFzblsj(textBox8.Text.Trim(), comboBox1.Text.Trim(), dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"), textBox5.Text.Trim()) == 1)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void Frm_Fzblsj_add_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
    }
}
