using System;
using System.Windows.Forms;

namespace PIS_Sys.qcgl
{
    public partial class Frm_Memo : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Memo()
        {
            InitializeComponent();
        }
        public string Memo_note = "";
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (textBoxX1.Text.Trim() != "")
            {
                Memo_note = textBoxX1.Text.Trim();
                DialogResult = DialogResult.OK;
            }
        }

        private void Frm_Memo_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }
    }
}
