using System.Windows.Forms;

namespace PathologyClient
{
    public partial class FrmAlert : DevComponents.DotNetBar.Balloon
    {
        public FrmAlert()
        {
            InitializeComponent();
        }

        public void SetInfo(string Title, string info)
        {
            this.labelX1.Text = Title;
            this.labelX2.Text = info;
            Application.DoEvents();
        }

        private void FrmAlert_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }






    }
}
