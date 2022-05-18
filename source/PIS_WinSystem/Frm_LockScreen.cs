using System;
using System.Windows.Forms;

namespace PIS_Sys
{
    public partial class Frm_LockScreen : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_LockScreen()
        {
            InitializeComponent();
        }

        private void PasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                if (PasswordTextBox.Text.ToString().Equals(Program.User_Pwd))
                {
                    CloseFlag = true;
                    this.Close();
                }
            }
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            PasswordTextBox.Focus();

        }

        private void Frm_LockScreen_Load(object sender, EventArgs e)
        {
            UsernameTextBox.Text = Program.User_Name;
            timer1.Enabled = true;
        }

        private Boolean CloseFlag = false;
        private void Frm_LockScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseFlag)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}

