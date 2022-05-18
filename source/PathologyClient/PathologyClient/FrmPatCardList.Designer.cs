namespace PathologyClient
{
    partial class FrmPatCardList
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPatCardList));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnSetBackColor = new System.Windows.Forms.ToolStripButton();
            this.btnHighlight = new System.Windows.Forms.ToolStripButton();
            this.btnBlink = new System.Windows.Forms.ToolStripButton();
            this.btnPush = new System.Windows.Forms.ToolStripButton();
            this.btnCardBG = new System.Windows.Forms.ToolStripButton();
            this.lvwPatients = new ASwartz.WinForms.Controls.ASCardListViewControl();
            this.myContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmViewDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.cmViewDoc = new System.Windows.Forms.ToolStripMenuItem();
            this.cmOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.myContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnSetBackColor,
            this.btnHighlight,
            this.btnBlink,
            this.btnPush,
            this.btnCardBG});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(767, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(36, 22);
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSetBackColor
            // 
            this.btnSetBackColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSetBackColor.Image = ((System.Drawing.Image)(resources.GetObject("btnSetBackColor.Image")));
            this.btnSetBackColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetBackColor.Name = "btnSetBackColor";
            this.btnSetBackColor.Size = new System.Drawing.Size(96, 22);
            this.btnSetBackColor.Text = "设置卡片背景色";
            this.btnSetBackColor.Click += new System.EventHandler(this.btnSetBackColor_Click);
            // 
            // btnHighlight
            // 
            this.btnHighlight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnHighlight.Image = ((System.Drawing.Image)(resources.GetObject("btnHighlight.Image")));
            this.btnHighlight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHighlight.Name = "btnHighlight";
            this.btnHighlight.Size = new System.Drawing.Size(72, 22);
            this.btnHighlight.Text = "高亮度显示";
            this.btnHighlight.Click += new System.EventHandler(this.btnHighlight_Click);
            // 
            // btnBlink
            // 
            this.btnBlink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnBlink.Image = ((System.Drawing.Image)(resources.GetObject("btnBlink.Image")));
            this.btnBlink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBlink.Name = "btnBlink";
            this.btnBlink.Size = new System.Drawing.Size(36, 22);
            this.btnBlink.Text = "闪烁";
            this.btnBlink.Click += new System.EventHandler(this.btnBlink_Click);
            // 
            // btnPush
            // 
            this.btnPush.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPush.Image = ((System.Drawing.Image)(resources.GetObject("btnPush.Image")));
            this.btnPush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPush.Name = "btnPush";
            this.btnPush.Size = new System.Drawing.Size(60, 22);
            this.btnPush.Text = "设置边框";
            this.btnPush.Click += new System.EventHandler(this.btnPush_Click);
            // 
            // btnCardBG
            // 
            this.btnCardBG.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCardBG.Image = ((System.Drawing.Image)(resources.GetObject("btnCardBG.Image")));
            this.btnCardBG.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCardBG.Name = "btnCardBG";
            this.btnCardBG.Size = new System.Drawing.Size(84, 22);
            this.btnCardBG.Text = "设置卡片背景";
            this.btnCardBG.Click += new System.EventHandler(this.btnCardBG_Click);
            // 
            // lvwPatients
            // 
            this.lvwPatients.AutoScroll = true;
            this.lvwPatients.AutoScrollMinSize = new System.Drawing.Size(8, 10);
            this.lvwPatients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwPatients.Location = new System.Drawing.Point(0, 25);
            this.lvwPatients.Name = "lvwPatients";
            this.lvwPatients.Size = new System.Drawing.Size(767, 355);
            this.lvwPatients.TabIndex = 3;
            // 
            // myContextMenu
            // 
            this.myContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmViewDetails,
            this.cmViewDoc,
            this.cmOut});
            this.myContextMenu.Name = "myContextMenu";
            this.myContextMenu.Size = new System.Drawing.Size(173, 70);
            // 
            // cmViewDetails
            // 
            this.cmViewDetails.Name = "cmViewDetails";
            this.cmViewDetails.Size = new System.Drawing.Size(172, 22);
            this.cmViewDetails.Text = "查看病人基本信息";
            this.cmViewDetails.Click += new System.EventHandler(this.cmViewDetails_Click);
            // 
            // cmViewDoc
            // 
            this.cmViewDoc.Name = "cmViewDoc";
            this.cmViewDoc.Size = new System.Drawing.Size(172, 22);
            this.cmViewDoc.Text = "查看病历";
            this.cmViewDoc.Click += new System.EventHandler(this.cmViewDoc_Click);
            // 
            // cmOut
            // 
            this.cmOut.Name = "cmOut";
            this.cmOut.Size = new System.Drawing.Size(172, 22);
            this.cmOut.Text = "病人出院";
            this.cmOut.Click += new System.EventHandler(this.cmOut_Click);
            // 
            // FrmPatCardList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 380);
            this.Controls.Add(this.lvwPatients);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmPatCardList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病人卡片测试";
            this.Load += new System.EventHandler(this.FrmPatCardList_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.myContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnSetBackColor;
        private System.Windows.Forms.ToolStripButton btnHighlight;
        private System.Windows.Forms.ToolStripButton btnBlink;
        private System.Windows.Forms.ToolStripButton btnPush;
        private System.Windows.Forms.ToolStripButton btnCardBG;
        private ASwartz.WinForms.Controls.ASCardListViewControl lvwPatients;
        private System.Windows.Forms.ContextMenuStrip myContextMenu;
        private System.Windows.Forms.ToolStripMenuItem cmViewDetails;
        private System.Windows.Forms.ToolStripMenuItem cmViewDoc;
        private System.Windows.Forms.ToolStripMenuItem cmOut;
    }
}