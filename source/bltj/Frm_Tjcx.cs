using DevComponents.DotNetBar;
using PIS_WinSystem.dagl;
using System;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_Tjcx : DevComponents.DotNetBar.Office2007Form
    {
        public static string CurDept_Code;
        public Frm_Tjcx(string strprinter)
        {
            InitializeComponent();
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();
            BarcodePrinter = strprinter;
        }
        public static string BarcodePrinter = "";
        public void SetMdiForm(String caption, String title, Type formType)
        {
            Boolean IsOpened = false;
            //遍历现有的Tab页面，如果存在，那么设置为选中即可
            foreach (SuperTabItem tabitem in NavTabControl.Tabs)
            {
                if (tabitem.Name == caption)
                {
                    NavTabControl.SelectedTab = tabitem;
                    IsOpened = true;
                    break;
                }
            }
            //如果在现有Tab页面中没有找到，那么就要初始化了Tab页面了
            if (!IsOpened)
            {
                //为了方便管理，调用LoadMdiForm函数来创建一个新的窗体，并作为MDI的子窗体
                //然后分配给SuperTab控件，创建一个SuperTabItem并显示
                DevComponents.DotNetBar.Office2007Form Form = ChildWinManagement.LoadMdiForm(this, formType) as DevComponents.DotNetBar.Office2007Form;
                SuperTabItem tabItem = NavTabControl.CreateTab(caption);
                tabItem.Name = caption;
                tabItem.Text = title;
                Form.FormBorderStyle = FormBorderStyle.None;
                Form.TopLevel = false;
                Form.Visible = true;
                Form.Dock = DockStyle.Fill;
                tabItem.Icon = Form.Icon;
                tabItem.AttachedControl.Controls.Add(Form);
                NavTabControl.SelectedTab = tabItem;
            }

        }
        private void Frm_Tjcx_Load(object sender, EventArgs e)
        {

            SetMdiForm("Frm_Djb", "病理登记薄", typeof(Frm_Djb));
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_Djb", "病理登记薄", typeof(Frm_Djb));
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_tj", "医师工作量分类统计", typeof(Frm_tj));
        }
        //阳性率统计
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            PIS_Statistics.Frm_Yxl ins = new PIS_Statistics.Frm_Yxl();
            ins.BringToFront();
            ins.ShowDialog();
        }
        //冰冻统计
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            Frm_bdtj ins = new Frm_bdtj();
            ins.BringToFront();
            ins.ShowDialog();
        }
        //质控：标本规范化固定统计
        private void buttonItem5_Click(object sender, EventArgs e)
        {
            Frm_bbgfh ins = new Frm_bbgfh();
            ins.BringToFront();
            ins.ShowDialog();
        }
        //质控：HE染色切片优良率
        private void buttonItem6_Click(object sender, EventArgs e)
        {
            PIS_Statistics.Frm_HeQp ins = new PIS_Statistics.Frm_HeQp();
            ins.BringToFront();
            ins.ShowDialog();
        }
        //质控：免疫组化切片优良率
        private void buttonItem7_Click_1(object sender, EventArgs e)
        {
            PIS_Statistics.Frm_Myzh ins = new PIS_Statistics.Frm_Myzh();
            ins.BringToFront();
            ins.ShowDialog();
        }
        //质控：术中快速病理诊断及时率
        private void buttonItem8_Click(object sender, EventArgs e)
        {
            PIS_Statistics.Frm_Szzd ins = new PIS_Statistics.Frm_Szzd();
            ins.BringToFront();
            ins.ShowDialog();
        }
        //质控：组织病理诊断及时率
        private void buttonItem9_Click(object sender, EventArgs e)
        {
            PIS_Statistics.Frm_Zzbl ins = new PIS_Statistics.Frm_Zzbl();
            ins.BringToFront();
            ins.ShowDialog();
        }
        //质控：细胞病理诊断及时率
        private void buttonItem10_Click(object sender, EventArgs e)
        {
            PIS_Statistics.Frm_Xbbl ins = new PIS_Statistics.Frm_Xbbl();
            ins.BringToFront();
            ins.ShowDialog();
        }
        //质控：细胞各项分子病理检测室内质控合格率
        private void buttonItem11_Click(object sender, EventArgs e)
        {
            PIS_Statistics.Frm_Fzsn ins = new PIS_Statistics.Frm_Fzsn();
            ins.BringToFront();
            ins.ShowDialog();
        }
        //质控：细胞学病理质控诊断符合率
        private void buttonItem14_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_Xbzk", "细胞学病理质控诊断符合率", typeof(Frm_Xbzk));
        }
        //质控：术中快速诊断和石蜡诊断符合率
        private void buttonItem15_Click(object sender, EventArgs e)
        {
            PIS_Statistics.Frm_Szsl ins = new PIS_Statistics.Frm_Szsl();
            ins.BringToFront();
            ins.ShowDialog();
        }
        //质控：免疫组化染色室间质评合格率
        private void buttonItem12_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_Myzhsj", "免疫组化染色室间质评合格率", typeof(Frm_Myzhsj));
        }
        //质控：各项分子病理室间质评合格率
        private void buttonItem13_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_Fzblsj", "各项分子病理室间质评合格率", typeof(Frm_Fzblsj));
        }
        //申请医师开单明细统计
        private void buttonItem16_Click(object sender, EventArgs e)
        {
            PIS_Statistics.Frm_SqmxTj ins = new PIS_Statistics.Frm_SqmxTj();
            ins.BringToFront();
            ins.ShowDialog();
        }

        private void buttonItem17_Click(object sender, EventArgs e)
        {
            PIS_Statistics.Frm_lcfh ins = new PIS_Statistics.Frm_lcfh();
            ins.BringToFront();
            ins.ShowDialog();
        }
        //月质控统计
        private void buttonItem18_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_zhtj", "科室月度质控统计", typeof(Frm_zhtj));
        }
        //不合格送检信息
        private void buttonItem19_Click(object sender, EventArgs e)
        {
            SetMdiForm("FrmBhgsj", "不合格送检信息", typeof(FrmBhgsj));
        }
        //医师工作量统计2
        private void buttonItem20_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_tj2", "医师工作量明细统计", typeof(Frm_tj2));
        }
        //
        private void buttonItem21_Click(object sender, EventArgs e)
        {
            SetMdiForm("FrmLcgtList", "与临床医师沟通信息查询", typeof(FrmLcgtList));
        }
        //报告医师工作量
        private void buttonItem24_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_BgYsgzz", "报告医师工作量", typeof(Frm_BgYsgzz));
        }
        //技师制片信息
        private void buttonItem22_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_JsPjInfo", "技师制片信息统计", typeof(Frm_JsPjInfo));
        }
        //报告质量控制
        private void buttonItem23_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_BgZlKz", "报告质量控制", typeof(Frm_BgZlKz));
        }
        //镜下描述统计
        private void buttonItem25_Click(object sender, EventArgs e)
        {
            SetMdiForm("FtmJxtj", "镜下描述统计", typeof(FtmJxtj));
        }

    }


}
