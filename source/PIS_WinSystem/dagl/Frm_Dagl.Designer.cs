using System.Windows.Forms;
namespace PIS_Sys.dagl
{
    partial class Frm_Dagl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0xA1 && (int)m.WParam == 2)
            {
                m.Msg = 0x201;
                m.LParam = System.IntPtr.Zero;
            }
            base.WndProc(ref m);
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Dagl));
            this.sideBar1 = new DevComponents.DotNetBar.SideBar();
            this.sideBarPanelItem1 = new DevComponents.DotNetBar.SideBarPanelItem();
            this.buttonItem11 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem12 = new DevComponents.DotNetBar.ButtonItem();
            this.QueryReport = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem18 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem15 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem19 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem20 = new DevComponents.DotNetBar.ButtonItem();
            this.sideBarPanelItem4 = new DevComponents.DotNetBar.SideBarPanelItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.sideBarPanelItem2 = new DevComponents.DotNetBar.SideBarPanelItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.sideBarPanelItem3 = new DevComponents.DotNetBar.SideBarPanelItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.sideBarPanelItem5 = new DevComponents.DotNetBar.SideBarPanelItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.haocaiguanli = new DevComponents.DotNetBar.SideBarPanelItem();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem8 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem10 = new DevComponents.DotNetBar.ButtonItem();
            this.sideBarPanelItem6 = new DevComponents.DotNetBar.SideBarPanelItem();
            this.buttonItem16 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem17 = new DevComponents.DotNetBar.ButtonItem();
            this.ExpandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.NavTabControl = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.NavTabControl)).BeginInit();
            this.SuspendLayout();
            // 
            // sideBar1
            // 
            this.sideBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.sideBar1.BorderStyle = DevComponents.DotNetBar.eBorderType.None;
            this.sideBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.sideBar1.ExpandedPanel = this.sideBarPanelItem1;
            this.sideBar1.Location = new System.Drawing.Point(0, 0);
            this.sideBar1.Name = "sideBar1";
            this.sideBar1.Panels.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.sideBarPanelItem1,
            this.sideBarPanelItem4,
            this.sideBarPanelItem2,
            this.sideBarPanelItem3,
            this.sideBarPanelItem5,
            this.haocaiguanli,
            this.sideBarPanelItem6});
            this.sideBar1.Size = new System.Drawing.Size(168, 733);
            this.sideBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sideBar1.TabIndex = 0;
            this.sideBar1.Text = "sideBar1";
            // 
            // sideBarPanelItem1
            // 
            this.sideBarPanelItem1.FontBold = true;
            this.sideBarPanelItem1.Name = "sideBarPanelItem1";
            this.sideBarPanelItem1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem11,
            this.buttonItem12,
            this.QueryReport,
            this.buttonItem18,
            this.buttonItem15,
            this.buttonItem19,
            this.buttonItem20});
            this.sideBarPanelItem1.Text = "报告相关";
            // 
            // buttonItem11
            // 
            this.buttonItem11.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem11.Image = global::PIS_Sys.Properties.Resources.BDreport;
            this.buttonItem11.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem11.Name = "buttonItem11";
            this.buttonItem11.Text = "冰冻报告";
            this.buttonItem11.Click += new System.EventHandler(this.buttonItem11_Click);
            // 
            // buttonItem12
            // 
            this.buttonItem12.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem12.Image = global::PIS_Sys.Properties.Resources.ysreport;
            this.buttonItem12.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem12.Name = "buttonItem12";
            this.buttonItem12.Text = "延时报告";
            this.buttonItem12.Click += new System.EventHandler(this.buttonItem12_Click);
            // 
            // QueryReport
            // 
            this.QueryReport.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.QueryReport.Image = global::PIS_Sys.Properties.Resources.medical_report_48px;
            this.QueryReport.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.QueryReport.Name = "QueryReport";
            this.QueryReport.Text = "报告查询";
            this.QueryReport.Click += new System.EventHandler(this.QueryReport_Click);
            // 
            // buttonItem18
            // 
            this.buttonItem18.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem18.Image = global::PIS_Sys.Properties.Resources.booklet_32px_1180477_easyicon1;
            this.buttonItem18.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem18.Name = "buttonItem18";
            this.buttonItem18.Text = "补充报告";
            this.buttonItem18.Click += new System.EventHandler(this.buttonItem18_Click);
            // 
            // buttonItem15
            // 
            this.buttonItem15.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem15.Image = global::PIS_Sys.Properties.Resources.stack_32px_1180615_easyicon1;
            this.buttonItem15.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem15.Name = "buttonItem15";
            this.buttonItem15.Text = "收藏报告";
            this.buttonItem15.Click += new System.EventHandler(this.buttonItem15_Click);
            // 
            // buttonItem19
            // 
            this.buttonItem19.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem19.Image = global::PIS_Sys.Properties.Resources.adjust_32px;
            this.buttonItem19.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem19.Name = "buttonItem19";
            this.buttonItem19.Text = "随访信息";
            this.buttonItem19.Click += new System.EventHandler(this.buttonItem19_Click);
            // 
            // buttonItem20
            // 
            this.buttonItem20.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem20.Image = global::PIS_Sys.Properties.Resources.browser_32px_1180481_easyicon_net;
            this.buttonItem20.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem20.Name = "buttonItem20";
            this.buttonItem20.Text = "修改意见";
            this.buttonItem20.Click += new System.EventHandler(this.buttonItem20_Click);
            // 
            // sideBarPanelItem4
            // 
            this.sideBarPanelItem4.FontBold = true;
            this.sideBarPanelItem4.Name = "sideBarPanelItem4";
            this.sideBarPanelItem4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem3});
            this.sideBarPanelItem4.Text = "标本查询";
            // 
            // buttonItem3
            // 
            this.buttonItem3.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem3.Image = global::PIS_Sys.Properties.Resources.medicine_bottle_48;
            this.buttonItem3.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "标本查询";
            this.buttonItem3.Click += new System.EventHandler(this.buttonItem3_Click);
            // 
            // sideBarPanelItem2
            // 
            this.sideBarPanelItem2.FontBold = true;
            this.sideBarPanelItem2.Name = "sideBarPanelItem2";
            this.sideBarPanelItem2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1});
            this.sideBarPanelItem2.Text = "蜡块相关";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem1.Image = global::PIS_Sys.Properties.Resources.soaps_soap_48px_525397_easyicon_net;
            this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "蜡块查询";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // sideBarPanelItem3
            // 
            this.sideBarPanelItem3.FontBold = true;
            this.sideBarPanelItem3.Name = "sideBarPanelItem3";
            this.sideBarPanelItem3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem2});
            this.sideBarPanelItem3.Text = "切片相关";
            // 
            // buttonItem2
            // 
            this.buttonItem2.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem2.Image = global::PIS_Sys.Properties.Resources.Blood_Slide_48px_561440_easyicon_net;
            this.buttonItem2.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "玻片查询";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // sideBarPanelItem5
            // 
            this.sideBarPanelItem5.FontBold = true;
            this.sideBarPanelItem5.Name = "sideBarPanelItem5";
            this.sideBarPanelItem5.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem4});
            this.sideBarPanelItem5.Text = "借阅信息";
            // 
            // buttonItem4
            // 
            this.buttonItem4.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem4.Image = global::PIS_Sys.Properties.Resources.compose_32px_1180500_easyicon3;
            this.buttonItem4.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Text = "借阅信息登记";
            this.buttonItem4.Click += new System.EventHandler(this.buttonItem4_Click);
            // 
            // haocaiguanli
            // 
            this.haocaiguanli.FontBold = true;
            this.haocaiguanli.Name = "haocaiguanli";
            this.haocaiguanli.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem5,
            this.buttonItem6,
            this.buttonItem7,
            this.buttonItem8});
            this.haocaiguanli.Text = "耗材管理";
            // 
            // buttonItem5
            // 
            this.buttonItem5.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem5.Image")));
            this.buttonItem5.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.Text = "脱水液更换记录";
            this.buttonItem5.Click += new System.EventHandler(this.buttonItem5_Click);
            // 
            // buttonItem6
            // 
            this.buttonItem6.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem6.Image")));
            this.buttonItem6.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.Text = "染液更换记录";
            this.buttonItem6.Click += new System.EventHandler(this.buttonItem6_Click);
            // 
            // buttonItem7
            // 
            this.buttonItem7.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem7.Image")));
            this.buttonItem7.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem7.Name = "buttonItem7";
            this.buttonItem7.Text = "医疗废物废液管理";
            this.buttonItem7.Click += new System.EventHandler(this.buttonItem7_Click);
            // 
            // buttonItem8
            // 
            this.buttonItem8.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem8.Image")));
            this.buttonItem8.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem8.Name = "buttonItem8";
            this.buttonItem8.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem9,
            this.buttonItem10});
            this.buttonItem8.Text = "危险化学品使用管理";
            // 
            // buttonItem9
            // 
            this.buttonItem9.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem9.Image")));
            this.buttonItem9.Name = "buttonItem9";
            this.buttonItem9.Text = "易燃易爆物品管理";
            this.buttonItem9.Click += new System.EventHandler(this.buttonItem9_Click);
            // 
            // buttonItem10
            // 
            this.buttonItem10.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem10.Image")));
            this.buttonItem10.Name = "buttonItem10";
            this.buttonItem10.Text = "有毒试剂管理";
            this.buttonItem10.Click += new System.EventHandler(this.buttonItem10_Click);
            // 
            // sideBarPanelItem6
            // 
            this.sideBarPanelItem6.FontBold = true;
            this.sideBarPanelItem6.Name = "sideBarPanelItem6";
            this.sideBarPanelItem6.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem16,
            this.buttonItem17});
            this.sideBarPanelItem6.Text = "试剂管理";
            // 
            // buttonItem16
            // 
            this.buttonItem16.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem16.Image")));
            this.buttonItem16.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem16.Name = "buttonItem16";
            this.buttonItem16.Text = "试剂入库登记管理";
            this.buttonItem16.Click += new System.EventHandler(this.buttonItem16_Click);
            // 
            // buttonItem17
            // 
            this.buttonItem17.Image = global::PIS_Sys.Properties.Resources.storage_32px_1182272_easyicon_net;
            this.buttonItem17.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem17.Name = "buttonItem17";
            this.buttonItem17.Text = "试剂出库登记管理";
            this.buttonItem17.Click += new System.EventHandler(this.buttonItem17_Click);
            // 
            // ExpandableSplitter1
            // 
            this.ExpandableSplitter1.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.ExpandableSplitter1.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.ExpandableSplitter1.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.ExpandableSplitter1.ExpandableControl = this.sideBar1;
            this.ExpandableSplitter1.ExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.ExpandableSplitter1.ExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.ExpandableSplitter1.ExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ExpandableSplitter1.ExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.ExpandableSplitter1.GripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ExpandableSplitter1.GripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.ExpandableSplitter1.GripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ExpandableSplitter1.GripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.ExpandableSplitter1.HotBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(151)))), ((int)(((byte)(61)))));
            this.ExpandableSplitter1.HotBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(184)))), ((int)(((byte)(94)))));
            this.ExpandableSplitter1.HotBackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground2;
            this.ExpandableSplitter1.HotBackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemPressedBackground;
            this.ExpandableSplitter1.HotExpandFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.ExpandableSplitter1.HotExpandFillColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.ExpandableSplitter1.HotExpandLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ExpandableSplitter1.HotExpandLineColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.ExpandableSplitter1.HotGripDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(207)))));
            this.ExpandableSplitter1.HotGripDarkColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.ExpandableSplitter1.HotGripLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.ExpandableSplitter1.HotGripLightColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.ExpandableSplitter1.Location = new System.Drawing.Point(168, 0);
            this.ExpandableSplitter1.Name = "ExpandableSplitter1";
            this.ExpandableSplitter1.Size = new System.Drawing.Size(10, 733);
            this.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.ExpandableSplitter1.TabIndex = 13;
            this.ExpandableSplitter1.TabStop = false;
            // 
            // NavTabControl
            // 
            this.NavTabControl.CloseButtonOnTabsVisible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.NavTabControl.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.NavTabControl.ControlBox.MenuBox.Name = "";
            this.NavTabControl.ControlBox.Name = "";
            this.NavTabControl.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.NavTabControl.ControlBox.MenuBox,
            this.NavTabControl.ControlBox.CloseBox});
            this.NavTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavTabControl.Location = new System.Drawing.Point(178, 0);
            this.NavTabControl.Name = "NavTabControl";
            this.NavTabControl.ReorderTabsEnabled = true;
            this.NavTabControl.SelectedTabFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.NavTabControl.SelectedTabIndex = 0;
            this.NavTabControl.Size = new System.Drawing.Size(1098, 733);
            this.NavTabControl.TabFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NavTabControl.TabIndex = 14;
            this.NavTabControl.Text = "superTabControl1";
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 10);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(1246, 791);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "superTabItem1";
            // 
            // buttonItem13
            // 
            this.buttonItem13.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem13.Image = global::PIS_Sys.Properties.Resources.tjqp;
            this.buttonItem13.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem13.Name = "buttonItem13";
            this.buttonItem13.Text = "特检切片";
            // 
            // Frm_Dagl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(1276, 733);
            this.Controls.Add(this.NavTabControl);
            this.Controls.Add(this.ExpandableSplitter1);
            this.Controls.Add(this.sideBar1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Dagl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "档案管理工作站";
            this.Load += new System.EventHandler(this.Frm_Dagl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.NavTabControl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SideBar sideBar1;
        private DevComponents.DotNetBar.ExpandableSplitter ExpandableSplitter1;
        private DevComponents.DotNetBar.SuperTabControl NavTabControl;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        private DevComponents.DotNetBar.SideBarPanelItem sideBarPanelItem1;
        private DevComponents.DotNetBar.ButtonItem QueryReport;
        private DevComponents.DotNetBar.SideBarPanelItem sideBarPanelItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.SideBarPanelItem sideBarPanelItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.SideBarPanelItem sideBarPanelItem4;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.SideBarPanelItem sideBarPanelItem5;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.SideBarPanelItem haocaiguanli;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
        private DevComponents.DotNetBar.ButtonItem buttonItem7;
        private DevComponents.DotNetBar.ButtonItem buttonItem8;
        private DevComponents.DotNetBar.ButtonItem buttonItem9;
        private DevComponents.DotNetBar.ButtonItem buttonItem10;
        private DevComponents.DotNetBar.ButtonItem buttonItem11;
        private DevComponents.DotNetBar.ButtonItem buttonItem12;
        private DevComponents.DotNetBar.ButtonItem buttonItem13;
        private DevComponents.DotNetBar.ButtonItem buttonItem15;
        private DevComponents.DotNetBar.SideBarPanelItem sideBarPanelItem6;
        private DevComponents.DotNetBar.ButtonItem buttonItem16;
        private DevComponents.DotNetBar.ButtonItem buttonItem17;
        private DevComponents.DotNetBar.ButtonItem buttonItem18;
        private DevComponents.DotNetBar.ButtonItem buttonItem19;
        private DevComponents.DotNetBar.ButtonItem buttonItem20;
    }
}