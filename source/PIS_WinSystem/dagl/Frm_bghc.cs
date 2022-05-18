using DevComponents.DotNetBar;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_bghc : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_bghc()
        {
            InitializeComponent();
        }

        private void Frm_bghc_Load(object sender, EventArgs e)
        {

        }
        //提示窗体
        public void Frm_TJInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(this);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = true;
            Frm_AlertIns.AutoCloseTimeOut = 2;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (!textBoxX1.Text.Trim().Equals(""))
            {
                //是否启用打印上报接口服务
                if (Program.Interface_SetInfo != null)
                {
                    if (Program.Interface_SetInfo.enable_flag == 1)
                    {
                        //已经打印是否调用接口服务
                        if (Program.Interface_SetInfo.print_flag == 1)
                        {
                            ClientSCS.SynchronizedClient("dy|" + textBoxX1.Text.Trim().ToUpper());
                            Frm_TJInfo("提示", "回传完毕");
                        }
                    }
                }
            }
            else
            {
                Frm_TJInfo("提示", "请输入病理号");
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (integerInput1.Value != 0 && integerInput2.Value != 0 && integerInput1.Value < integerInput2.Value)
            {
                //是否启用打印上报接口服务
                if (Program.Interface_SetInfo != null)
                {
                    if (Program.Interface_SetInfo.enable_flag == 1)
                    {
                        //已经打印是否调用接口服务
                        if (Program.Interface_SetInfo.print_flag == 1)
                        {
                            buttonX2.Enabled = false;
                            for (int i = integerInput1.Value; i <= integerInput2.Value; i++)
                            {
                                ClientSCS.SynchronizedClient("dy|" + textBoxX4.Text.Trim().ToUpper() + i.ToString());
                                System.Threading.Thread.Sleep(300);
                                Frm_TJInfo("提示", textBoxX4.Text.Trim().ToUpper() + i.ToString() + "回传完毕");
                            }
                            buttonX2.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                Frm_TJInfo("提示", "请输入病理号");
            }
        }
    }
}
