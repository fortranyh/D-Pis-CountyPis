using DevComponents.DotNetBar;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PathologyClient
{
    public partial class FrmSet : DevComponents.DotNetBar.Office2007Form
    {
        public FrmSet()
        {
            InitializeComponent();
        }

        private void FrmSet_Load(object sender, EventArgs e)
        {
            //打印机设置
            foreach (String iprt in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                comboBox10.Items.Add(iprt);
            }
            if (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count > 0)
            {

                //报告打印机
                if (comboBox10.Items.Contains(PathologyClient.Properties.Settings.Default.CurPrinter))
                {
                    comboBox10.Text = PathologyClient.Properties.Settings.Default.CurPrinter;
                }
                else
                {
                    comboBox10.SelectedIndex = 0;
                }

                this.switchButton1.Value = PathologyClient.Properties.Settings.Default.Print_Sqd_Flag;
            }
            else
            {
                Frm_TJInfo("提示", "没有检测到安装的打印机！\n请检测安装。");
            }
        }
        //提示窗体
        public void Frm_TJInfo(string Title, string B_info, Boolean DelayFlag = false)
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
            if (DelayFlag)
            {
                System.Threading.Thread.Sleep(500);
            }
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            PathologyClient.Properties.Settings.Default.CurPrinter = comboBox10.Text;
            PathologyClient.Properties.Settings.Default.Print_Sqd_Flag = switchButton1.Value;
            PathologyClient.Properties.Settings.Default.Save();
            Frm_TJInfo("提示", "保存成功！");
            this.Close();
        }
    }
}
