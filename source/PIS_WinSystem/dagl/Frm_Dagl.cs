using DevComponents.DotNetBar;
using System;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_Dagl : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Dagl()
        {
            InitializeComponent();
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();

        }



        private void Frm_Dagl_Load(object sender, EventArgs e)
        {
            if (Program.workstation_type_db.Contains("XBX") && !Program.workstation_type_db.Contains("PL"))
            {
                this.sideBarPanelItem2.Visible = false;
                buttonItem11.Visible = false;
            }
            sideBarPanelItem1.Expanded = true;
            SetMdiForm("Frm_ReportQuery", "报告查询", typeof(Frm_ReportQuery));
        }
        //查询报告按钮
        private void QueryReport_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_ReportQuery", "报告查询", typeof(Frm_ReportQuery));
        }

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

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_LkQuery", "蜡块查询", typeof(Frm_LkQuery));
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_QpQuery", "玻片查询", typeof(Frm_QpQuery));
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_BbQuery", "标本信息查询", typeof(Frm_BbQuery));
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_Jydj", "借阅信息登记", typeof(Frm_Jydj));
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_Tsyjl", "脱水液更换记录", typeof(Frm_Tsyjl));
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_Rygh", "染液更换记录", typeof(Frm_Rygh));
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_fwfy", @"医疗废物\废液记录", typeof(Frm_fwfy));
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_yryb", "易燃易爆物品管理", typeof(Frm_yryb));
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_ydsj", "有毒试剂管理", typeof(Frm_ydsj));
        }
        //冰冻报告
        private void buttonItem11_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_BdReport", "冰冻报告列表", typeof(Frm_BdReport));
        }
        //延时报告
        private void buttonItem12_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_YsReport", "延时报告列表", typeof(Frm_YsReport));

        }
        //特检切片
        private void buttonItem14_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_tjQp", "特检玻片查询", typeof(Frm_tjQp));
        }
        //收藏报告
        private void buttonItem15_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_QueryScReport", "收藏报告列表", typeof(Frm_QueryScReport));
        }

        private void buttonItem16_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_Sjdj", "试剂入库登记管理", typeof(Frm_Sjdj));
        }

        private void buttonItem17_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_Sjckdj", "试剂出库登记管理", typeof(Frm_Sjckdj));
        }
        //补充报告
        private void buttonItem18_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_bcReport", "补充报告列表", typeof(Frm_bcReport));
        }


        private void buttonItem19_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_SF", "随访信息查询", typeof(Frm_SF));
        }

        private void buttonItem20_Click(object sender, EventArgs e)
        {
            SetMdiForm("Frm_XG", "报告修改建议信息查询", typeof(Frm_XG));
        }

    }
}
