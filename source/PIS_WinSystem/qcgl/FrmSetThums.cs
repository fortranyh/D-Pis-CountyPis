using System;
using System.Windows.Forms;

namespace PIS_Sys.qcgl
{
    public partial class FrmSetThums : DevComponents.DotNetBar.Office2007Form
    {
        public int ThWidth = 0;
        public int ThHeigth = 0;
        public FrmSetThums()
        {
            InitializeComponent();
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            ThWidth = integerInput1.Value;
            ThHeigth = integerInput2.Value;
            DialogResult = DialogResult.OK;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
