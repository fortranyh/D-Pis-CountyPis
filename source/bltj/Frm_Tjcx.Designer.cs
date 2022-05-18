using System.Windows.Forms;
namespace PIS_Statistics
{
    partial class Frm_Tjcx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Tjcx));
            this.sideBar1 = new DevComponents.DotNetBar.SideBar();
            this.explorerBar1 = new DevComponents.DotNetBar.ExplorerBar();
            this.explorerBarGroupItem1 = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem19 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem21 = new DevComponents.DotNetBar.ButtonItem();
            this.explorerBarGroupItem2 = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem20 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem18 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem22 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem24 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem23 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem16 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem25 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.explorerBarGroupItem3 = new DevComponents.DotNetBar.ExplorerBarGroupItem();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem7 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem8 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem9 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem10 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem11 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem12 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem14 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem15 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem17 = new DevComponents.DotNetBar.ButtonItem();
            this.ExpandableSplitter1 = new DevComponents.DotNetBar.ExpandableSplitter();
            this.NavTabControl = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.sideBar1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.explorerBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NavTabControl)).BeginInit();
            this.NavTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // sideBar1
            // 
            this.sideBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.sideBar1.BorderStyle = DevComponents.DotNetBar.eBorderType.None;
            this.sideBar1.Controls.Add(this.explorerBar1);
            this.sideBar1.Dock = System.Windows.Forms.DockStyle.Left;
            this.sideBar1.ExpandedPanel = null;
            this.sideBar1.Location = new System.Drawing.Point(0, 0);
            this.sideBar1.Name = "sideBar1";
            this.sideBar1.Size = new System.Drawing.Size(250, 733);
            this.sideBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.sideBar1.TabIndex = 1;
            this.sideBar1.Text = "sideBar1";
            // 
            // explorerBar1
            // 
            this.explorerBar1.AccessibleRole = System.Windows.Forms.AccessibleRole.ToolBar;
            this.explorerBar1.BackColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.explorerBar1.BackStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.ExplorerBarBackground2;
            this.explorerBar1.BackStyle.BackColorGradientAngle = 90;
            this.explorerBar1.BackStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ExplorerBarBackground;
            this.explorerBar1.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.explorerBar1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.explorerBar1.GroupImages = null;
            this.explorerBar1.Groups.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.explorerBarGroupItem1,
            this.explorerBarGroupItem2,
            this.explorerBarGroupItem3});
            this.explorerBar1.Images = null;
            this.explorerBar1.Location = new System.Drawing.Point(0, 0);
            this.explorerBar1.Name = "explorerBar1";
            this.explorerBar1.Size = new System.Drawing.Size(250, 733);
            this.explorerBar1.TabIndex = 0;
            // 
            // explorerBarGroupItem1
            // 
            // 
            // 
            // 
            this.explorerBarGroupItem1.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.explorerBarGroupItem1.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem1.BackStyle.BorderBottomWidth = 1;
            this.explorerBarGroupItem1.BackStyle.BorderColor = System.Drawing.Color.White;
            this.explorerBarGroupItem1.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem1.BackStyle.BorderLeftWidth = 1;
            this.explorerBarGroupItem1.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem1.BackStyle.BorderRightWidth = 1;
            this.explorerBarGroupItem1.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem1.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBarGroupItem1.Expanded = true;
            this.explorerBarGroupItem1.Name = "explorerBarGroupItem1";
            this.explorerBarGroupItem1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.buttonItem19,
            this.buttonItem21});
            this.explorerBarGroupItem1.Text = "登记薄";
            // 
            // 
            // 
            this.explorerBarGroupItem1.TitleHotStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(73)))), ((int)(((byte)(181)))));
            this.explorerBarGroupItem1.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(93)))), ((int)(((byte)(206)))));
            this.explorerBarGroupItem1.TitleHotStyle.CornerDiameter = 3;
            this.explorerBarGroupItem1.TitleHotStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem1.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.explorerBarGroupItem1.TitleStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(73)))), ((int)(((byte)(181)))));
            this.explorerBarGroupItem1.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(93)))), ((int)(((byte)(206)))));
            this.explorerBarGroupItem1.TitleStyle.CornerDiameter = 3;
            this.explorerBarGroupItem1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem1.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem1.TitleStyle.TextColor = System.Drawing.Color.White;
            this.explorerBarGroupItem1.XPSpecialGroup = true;
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem1.HotFontUnderline = true;
            this.buttonItem1.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem1.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem1.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem1.Icon")));
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "登记薄打印";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // buttonItem19
            // 
            this.buttonItem19.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem19.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem19.HotFontUnderline = true;
            this.buttonItem19.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem19.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem19.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem19.Icon")));
            this.buttonItem19.Name = "buttonItem19";
            this.buttonItem19.Text = "不合格送检信息";
            this.buttonItem19.Click += new System.EventHandler(this.buttonItem19_Click);
            // 
            // buttonItem21
            // 
            this.buttonItem21.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem21.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem21.HotFontUnderline = true;
            this.buttonItem21.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem21.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem21.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem21.Icon")));
            this.buttonItem21.Name = "buttonItem21";
            this.buttonItem21.Text = "与临床医师沟通信息";
            this.buttonItem21.Click += new System.EventHandler(this.buttonItem21_Click);
            // 
            // explorerBarGroupItem2
            // 
            // 
            // 
            // 
            this.explorerBarGroupItem2.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.explorerBarGroupItem2.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem2.BackStyle.BorderBottomWidth = 1;
            this.explorerBarGroupItem2.BackStyle.BorderColor = System.Drawing.Color.White;
            this.explorerBarGroupItem2.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem2.BackStyle.BorderLeftWidth = 1;
            this.explorerBarGroupItem2.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem2.BackStyle.BorderRightWidth = 1;
            this.explorerBarGroupItem2.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem2.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBarGroupItem2.Expanded = true;
            this.explorerBarGroupItem2.Name = "explorerBarGroupItem2";
            this.explorerBarGroupItem2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem3,
            this.buttonItem20,
            this.buttonItem18,
            this.buttonItem22,
            this.buttonItem24,
            this.buttonItem23,
            this.buttonItem16,
            this.buttonItem25,
            this.buttonItem2,
            this.buttonItem4});
            this.explorerBarGroupItem2.Text = "查询统计";
            // 
            // 
            // 
            this.explorerBarGroupItem2.TitleHotStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(73)))), ((int)(((byte)(181)))));
            this.explorerBarGroupItem2.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(93)))), ((int)(((byte)(206)))));
            this.explorerBarGroupItem2.TitleHotStyle.CornerDiameter = 3;
            this.explorerBarGroupItem2.TitleHotStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem2.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem2.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem2.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.explorerBarGroupItem2.TitleStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(73)))), ((int)(((byte)(181)))));
            this.explorerBarGroupItem2.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(93)))), ((int)(((byte)(206)))));
            this.explorerBarGroupItem2.TitleStyle.CornerDiameter = 3;
            this.explorerBarGroupItem2.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem2.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem2.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem2.TitleStyle.TextColor = System.Drawing.Color.White;
            this.explorerBarGroupItem2.XPSpecialGroup = true;
            // 
            // buttonItem3
            // 
            this.buttonItem3.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem3.HotFontUnderline = true;
            this.buttonItem3.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem3.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem3.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem3.Icon")));
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Text = "医师工作量分类统计";
            this.buttonItem3.Click += new System.EventHandler(this.buttonItem3_Click);
            // 
            // buttonItem20
            // 
            this.buttonItem20.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem20.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem20.HotFontUnderline = true;
            this.buttonItem20.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem20.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem20.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem20.Icon")));
            this.buttonItem20.Name = "buttonItem20";
            this.buttonItem20.Text = "医师工作量明细统计";
            this.buttonItem20.Click += new System.EventHandler(this.buttonItem20_Click);
            // 
            // buttonItem18
            // 
            this.buttonItem18.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem18.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem18.HotFontUnderline = true;
            this.buttonItem18.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem18.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem18.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem18.Icon")));
            this.buttonItem18.Name = "buttonItem18";
            this.buttonItem18.Text = "科室月度质控统计";
            this.buttonItem18.Click += new System.EventHandler(this.buttonItem18_Click);
            // 
            // buttonItem22
            // 
            this.buttonItem22.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem22.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem22.HotFontUnderline = true;
            this.buttonItem22.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem22.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem22.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem22.Icon")));
            this.buttonItem22.Name = "buttonItem22";
            this.buttonItem22.Text = "技师制片信息统计";
            this.buttonItem22.Click += new System.EventHandler(this.buttonItem22_Click);
            // 
            // buttonItem24
            // 
            this.buttonItem24.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem24.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem24.HotFontUnderline = true;
            this.buttonItem24.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem24.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem24.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem24.Icon")));
            this.buttonItem24.Name = "buttonItem24";
            this.buttonItem24.Text = "报告医师工作量统计";
            this.buttonItem24.Click += new System.EventHandler(this.buttonItem24_Click);
            // 
            // buttonItem23
            // 
            this.buttonItem23.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem23.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem23.HotFontUnderline = true;
            this.buttonItem23.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem23.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem23.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem23.Icon")));
            this.buttonItem23.Name = "buttonItem23";
            this.buttonItem23.Text = "报告质量控制统计";
            this.buttonItem23.Click += new System.EventHandler(this.buttonItem23_Click);
            // 
            // buttonItem16
            // 
            this.buttonItem16.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem16.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem16.HotFontUnderline = true;
            this.buttonItem16.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem16.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem16.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem16.Icon")));
            this.buttonItem16.Name = "buttonItem16";
            this.buttonItem16.Text = "申请医师开单统计";
            this.buttonItem16.Click += new System.EventHandler(this.buttonItem16_Click);
            // 
            // buttonItem25
            // 
            this.buttonItem25.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem25.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem25.HotFontUnderline = true;
            this.buttonItem25.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem25.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem25.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem25.Icon")));
            this.buttonItem25.Name = "buttonItem25";
            this.buttonItem25.Text = "镜下描述统计";
            this.buttonItem25.Click += new System.EventHandler(this.buttonItem25_Click);
            // 
            // buttonItem2
            // 
            this.buttonItem2.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem2.HotFontUnderline = true;
            this.buttonItem2.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem2.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem2.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem2.Icon")));
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Text = "阳性率统计";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // buttonItem4
            // 
            this.buttonItem4.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem4.HotFontUnderline = true;
            this.buttonItem4.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem4.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem4.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem4.Icon")));
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Text = "冰冻统计";
            this.buttonItem4.Click += new System.EventHandler(this.buttonItem4_Click);
            // 
            // explorerBarGroupItem3
            // 
            // 
            // 
            // 
            this.explorerBarGroupItem3.BackStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(223)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem3.BackStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem3.BackStyle.BorderBottomWidth = 1;
            this.explorerBarGroupItem3.BackStyle.BorderColor = System.Drawing.Color.White;
            this.explorerBarGroupItem3.BackStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem3.BackStyle.BorderLeftWidth = 1;
            this.explorerBarGroupItem3.BackStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.explorerBarGroupItem3.BackStyle.BorderRightWidth = 1;
            this.explorerBarGroupItem3.BackStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem3.Cursor = System.Windows.Forms.Cursors.Default;
            this.explorerBarGroupItem3.Expanded = true;
            this.explorerBarGroupItem3.Name = "explorerBarGroupItem3";
            this.explorerBarGroupItem3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem5,
            this.buttonItem6,
            this.buttonItem7,
            this.buttonItem8,
            this.buttonItem9,
            this.buttonItem10,
            this.buttonItem11,
            this.buttonItem12,
            this.buttonItem13,
            this.buttonItem14,
            this.buttonItem15,
            this.buttonItem17});
            this.explorerBarGroupItem3.Text = "质控指标统计";
            // 
            // 
            // 
            this.explorerBarGroupItem3.TitleHotStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem3.TitleHotStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(211)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem3.TitleHotStyle.CornerDiameter = 3;
            this.explorerBarGroupItem3.TitleHotStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem3.TitleHotStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem3.TitleHotStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem3.TitleHotStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            // 
            // 
            // 
            this.explorerBarGroupItem3.TitleStyle.BackColor = System.Drawing.Color.White;
            this.explorerBarGroupItem3.TitleStyle.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(211)))), ((int)(((byte)(247)))));
            this.explorerBarGroupItem3.TitleStyle.CornerDiameter = 3;
            this.explorerBarGroupItem3.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.explorerBarGroupItem3.TitleStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem3.TitleStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded;
            this.explorerBarGroupItem3.TitleStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            // 
            // buttonItem5
            // 
            this.buttonItem5.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem5.HotFontUnderline = true;
            this.buttonItem5.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem5.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.Text = "标本规范化固定率";
            this.buttonItem5.Click += new System.EventHandler(this.buttonItem5_Click);
            // 
            // buttonItem6
            // 
            this.buttonItem6.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem6.HotFontUnderline = true;
            this.buttonItem6.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem6.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.Text = "HE染色切片优良率";
            this.buttonItem6.Click += new System.EventHandler(this.buttonItem6_Click);
            // 
            // buttonItem7
            // 
            this.buttonItem7.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem7.HotFontUnderline = true;
            this.buttonItem7.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem7.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem7.Name = "buttonItem7";
            this.buttonItem7.Text = "免疫组化染色切片优良率";
            this.buttonItem7.Click += new System.EventHandler(this.buttonItem7_Click_1);
            // 
            // buttonItem8
            // 
            this.buttonItem8.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem8.HotFontUnderline = true;
            this.buttonItem8.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem8.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem8.Name = "buttonItem8";
            this.buttonItem8.Text = "术中快速病理诊断及时率";
            this.buttonItem8.Click += new System.EventHandler(this.buttonItem8_Click);
            // 
            // buttonItem9
            // 
            this.buttonItem9.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem9.HotFontUnderline = true;
            this.buttonItem9.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem9.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem9.Name = "buttonItem9";
            this.buttonItem9.Text = "组织病理诊断及时率";
            this.buttonItem9.Click += new System.EventHandler(this.buttonItem9_Click);
            // 
            // buttonItem10
            // 
            this.buttonItem10.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem10.HotFontUnderline = true;
            this.buttonItem10.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem10.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem10.Name = "buttonItem10";
            this.buttonItem10.Text = "细胞病理诊断及时率";
            this.buttonItem10.Click += new System.EventHandler(this.buttonItem10_Click);
            // 
            // buttonItem11
            // 
            this.buttonItem11.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem11.HotFontUnderline = true;
            this.buttonItem11.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem11.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem11.Name = "buttonItem11";
            this.buttonItem11.Text = "各项分子病理检测室内质控合格率";
            this.buttonItem11.Click += new System.EventHandler(this.buttonItem11_Click);
            // 
            // buttonItem12
            // 
            this.buttonItem12.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem12.HotFontUnderline = true;
            this.buttonItem12.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem12.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem12.Name = "buttonItem12";
            this.buttonItem12.Text = "免疫组化染色室间质评合格率";
            this.buttonItem12.Click += new System.EventHandler(this.buttonItem12_Click);
            // 
            // buttonItem13
            // 
            this.buttonItem13.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem13.HotFontUnderline = true;
            this.buttonItem13.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem13.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem13.Name = "buttonItem13";
            this.buttonItem13.Text = "各项分子病理室间质评合格率";
            this.buttonItem13.Click += new System.EventHandler(this.buttonItem13_Click);
            // 
            // buttonItem14
            // 
            this.buttonItem14.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem14.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem14.HotFontUnderline = true;
            this.buttonItem14.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem14.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem14.Name = "buttonItem14";
            this.buttonItem14.Text = "细胞学病理诊断质控符合率";
            this.buttonItem14.Click += new System.EventHandler(this.buttonItem14_Click);
            // 
            // buttonItem15
            // 
            this.buttonItem15.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem15.HotFontUnderline = true;
            this.buttonItem15.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem15.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem15.Name = "buttonItem15";
            this.buttonItem15.Text = "术中快速诊断和石蜡诊断符合率";
            this.buttonItem15.Click += new System.EventHandler(this.buttonItem15_Click);
            // 
            // buttonItem17
            // 
            this.buttonItem17.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem17.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonItem17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(93)))), ((int)(((byte)(198)))));
            this.buttonItem17.HotFontUnderline = true;
            this.buttonItem17.HotForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(142)))), ((int)(((byte)(255)))));
            this.buttonItem17.HotTrackingStyle = DevComponents.DotNetBar.eHotTrackingStyle.None;
            this.buttonItem17.Name = "buttonItem17";
            this.buttonItem17.Text = "临床诊断符合率统计";
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
            this.ExpandableSplitter1.Location = new System.Drawing.Point(250, 0);
            this.ExpandableSplitter1.Name = "ExpandableSplitter1";
            this.ExpandableSplitter1.Size = new System.Drawing.Size(12, 733);
            this.ExpandableSplitter1.Style = DevComponents.DotNetBar.eSplitterStyle.Office2007;
            this.ExpandableSplitter1.TabIndex = 14;
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
            this.NavTabControl.Controls.Add(this.superTabControlPanel1);
            this.NavTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NavTabControl.Location = new System.Drawing.Point(262, 0);
            this.NavTabControl.Name = "NavTabControl";
            this.NavTabControl.ReorderTabsEnabled = true;
            this.NavTabControl.SelectedTabFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold);
            this.NavTabControl.SelectedTabIndex = 0;
            this.NavTabControl.Size = new System.Drawing.Size(1014, 733);
            this.NavTabControl.TabFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NavTabControl.TabIndex = 15;
            this.NavTabControl.Text = "superTabControl1";
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 0);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(1014, 733);
            this.superTabControlPanel1.TabIndex = 1;
            // 
            // Frm_Tjcx
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
            this.Name = "Frm_Tjcx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "统计质控工作站";
            this.Load += new System.EventHandler(this.Frm_Tjcx_Load);
            this.sideBar1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.explorerBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NavTabControl)).EndInit();
            this.NavTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SideBar sideBar1;
        private DevComponents.DotNetBar.ExpandableSplitter ExpandableSplitter1;
        private DevComponents.DotNetBar.SuperTabControl NavTabControl;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.ExplorerBar explorerBar1;
        private DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.ExplorerBarGroupItem explorerBarGroupItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
        private DevComponents.DotNetBar.ButtonItem buttonItem7;
        private DevComponents.DotNetBar.ButtonItem buttonItem8;
        private DevComponents.DotNetBar.ButtonItem buttonItem9;
        private DevComponents.DotNetBar.ButtonItem buttonItem10;
        private DevComponents.DotNetBar.ButtonItem buttonItem11;
        private DevComponents.DotNetBar.ButtonItem buttonItem12;
        private DevComponents.DotNetBar.ButtonItem buttonItem13;
        private DevComponents.DotNetBar.ButtonItem buttonItem14;
        private DevComponents.DotNetBar.ButtonItem buttonItem15;
        private DevComponents.DotNetBar.ButtonItem buttonItem16;
        private DevComponents.DotNetBar.ButtonItem buttonItem17;
        private DevComponents.DotNetBar.ButtonItem buttonItem18;
        private DevComponents.DotNetBar.ButtonItem buttonItem19;
        private DevComponents.DotNetBar.ButtonItem buttonItem20;
        private DevComponents.DotNetBar.ButtonItem buttonItem21;
        private DevComponents.DotNetBar.ButtonItem buttonItem24;
        private DevComponents.DotNetBar.ButtonItem buttonItem22;
        private DevComponents.DotNetBar.ButtonItem buttonItem23;
        private DevComponents.DotNetBar.ButtonItem buttonItem25;
    }
}