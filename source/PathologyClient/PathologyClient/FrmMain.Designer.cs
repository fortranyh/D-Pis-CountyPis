using System.Windows.Forms;
namespace PathologyClient
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
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
        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem3 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem4 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem6 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem14 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem11 = new DevComponents.DotNetBar.ButtonItem();
            this.BtnRelogin = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem15 = new DevComponents.DotNetBar.ButtonItem();
            this.styleManagerUI = new DevComponents.DotNetBar.StyleManager(this.components);
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.SuspendLayout();
            // 
            // bar1
            // 
            this.bar1.AccessibleDescription = "DotNetBar Bar (bar1)";
            this.bar1.AccessibleName = "DotNetBar Bar";
            this.bar1.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuBar;
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar1.Font = new System.Drawing.Font("宋体", 12F);
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem2,
            this.buttonItem3,
            this.buttonItem4,
            this.buttonItem5,
            this.buttonItem6,
            this.buttonItem13,
            this.buttonItem14});
            this.bar1.Location = new System.Drawing.Point(0, 0);
            this.bar1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.bar1.MenuBar = true;
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(1096, 29);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 5;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // buttonItem2
            // 
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.Tag = "bbdj";
            this.buttonItem2.Text = "标本登记(&R)";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // buttonItem3
            // 
            this.buttonItem3.Name = "buttonItem3";
            this.buttonItem3.Tag = "qcxx";
            this.buttonItem3.Text = "取材信息(&M)";
            this.buttonItem3.Click += new System.EventHandler(this.buttonItem3_Click);
            // 
            // buttonItem4
            // 
            this.buttonItem4.Name = "buttonItem4";
            this.buttonItem4.Tag = "zpxx";
            this.buttonItem4.Text = "制片信息(&G)";
            this.buttonItem4.Click += new System.EventHandler(this.buttonItem4_Click);
            // 
            // buttonItem5
            // 
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.Tag = "tjyz";
            this.buttonItem5.Text = "特检医嘱(&T)";
            this.buttonItem5.Click += new System.EventHandler(this.buttonItem5_Click);
            // 
            // buttonItem6
            // 
            this.buttonItem6.Name = "buttonItem6";
            this.buttonItem6.Tag = "blbg";
            this.buttonItem6.Text = "病理报告(&P)";
            this.buttonItem6.Click += new System.EventHandler(this.buttonItem6_Click);
            // 
            // buttonItem13
            // 
            this.buttonItem13.Name = "buttonItem13";
            this.buttonItem13.Tag = "xtsz";
            this.buttonItem13.Text = "系统设置(&X)";
            this.buttonItem13.Click += new System.EventHandler(this.buttonItem13_Click);
            // 
            // buttonItem14
            // 
            this.buttonItem14.Name = "buttonItem14";
            this.buttonItem14.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem11,
            this.BtnRelogin,
            this.buttonItem15,
            this.buttonItem1});
            this.buttonItem14.Text = "系统相关(&H)";
            // 
            // buttonItem11
            // 
            this.buttonItem11.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem11.Icon")));
            this.buttonItem11.Name = "buttonItem11";
            this.buttonItem11.Text = "修改密码";
            this.buttonItem11.Click += new System.EventHandler(this.buttonItem11_Click);
            // 
            // BtnRelogin
            // 
            this.BtnRelogin.Image = global::PathologyClient.Properties.Resources.idcard;
            this.BtnRelogin.Name = "BtnRelogin";
            this.BtnRelogin.Text = "切换用户";
            this.BtnRelogin.Click += new System.EventHandler(this.BtnRelogin_Click);
            // 
            // buttonItem15
            // 
            this.buttonItem15.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem15.Icon")));
            this.buttonItem15.Name = "buttonItem15";
            this.buttonItem15.Text = "关于本系统";
            this.buttonItem15.Click += new System.EventHandler(this.buttonItem15_Click);
            // 
            // styleManagerUI
            // 
            this.styleManagerUI.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
            this.styleManagerUI.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
            // 
            // buttonItem1
            // 
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "病人列表测试";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(1096, 519);
            this.Controls.Add(this.bar1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病理诊断中心客户端";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem3;
        private DevComponents.DotNetBar.ButtonItem buttonItem4;
        private DevComponents.DotNetBar.ButtonItem buttonItem6;
        private DevComponents.DotNetBar.ButtonItem buttonItem13;
        private DevComponents.DotNetBar.ButtonItem buttonItem14;
        private DevComponents.DotNetBar.ButtonItem buttonItem11;
        private DevComponents.DotNetBar.ButtonItem buttonItem15;
        public DevComponents.DotNetBar.StyleManager styleManagerUI;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.ButtonItem BtnRelogin;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
    }
}

