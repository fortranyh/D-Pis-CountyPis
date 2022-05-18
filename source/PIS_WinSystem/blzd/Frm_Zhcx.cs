using System;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_Zhcx : DevComponents.DotNetBar.Office2007Form
    {
        public string str_tj = "";
        public Frm_Zhcx()
        {
            InitializeComponent();
        }

        private void Frm_Zhcx_Load(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            str_tj = textBoxX1.Text.Trim().Replace("|", "") + "|" + textBoxX2.Text.Trim().Replace("|", "") + "|" + textBoxX3.Text.Trim().Replace("|", "");
            this.DialogResult = DialogResult.OK;
        }
    }
}
