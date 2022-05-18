using System;
using System.Windows.Forms;

namespace PIS_Sys.jszp
{
    public partial class FrmSmHd : DevComponents.DotNetBar.Office2007Form
    {
        public FrmSmHd()
        {
            InitializeComponent();
        }

        private void FrmSmHd_Load(object sender, EventArgs e)
        {

        }

        private void textBoxX1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                string str_tj = textBoxX1.Text.Trim().Replace("'", "");
                if (!str_tj.Equals(""))
                {
                    // 触发事件
                    Middle.Run("hd", str_tj);
                    textBoxX1.Text = "";
                    textBoxX1.Focus();
                }
            }
        }
    }
}
