namespace PIS_Sys
{
    partial class Frm_splash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_splash));
            this.Versiontxt = new System.Windows.Forms.Label();
            this.ApplicationProductName = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.Copyright = new System.Windows.Forms.Label();
            this.Labsqinfo = new System.Windows.Forms.Label();
            this.Labyymc = new System.Windows.Forms.Label();
            this.TimerMain = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Versiontxt
            // 
            this.Versiontxt.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Versiontxt.Location = new System.Drawing.Point(16, 131);
            this.Versiontxt.Name = "Versiontxt";
            this.Versiontxt.Size = new System.Drawing.Size(457, 21);
            this.Versiontxt.TabIndex = 7;
            this.Versiontxt.Text = "版本信息";
            this.Versiontxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ApplicationProductName
            // 
            this.ApplicationProductName.Font = new System.Drawing.Font("隶书", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ApplicationProductName.Location = new System.Drawing.Point(12, 75);
            this.ApplicationProductName.Name = "ApplicationProductName";
            this.ApplicationProductName.Size = new System.Drawing.Size(461, 46);
            this.ApplicationProductName.TabIndex = 6;
            this.ApplicationProductName.Text = "麦禾病理信息管理系统";
            this.ApplicationProductName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PictureBox1
            // 
            this.PictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.PictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("PictureBox1.Image")));
            this.PictureBox1.Location = new System.Drawing.Point(0, 259);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(112, 43);
            this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PictureBox1.TabIndex = 8;
            this.PictureBox1.TabStop = false;
            // 
            // Copyright
            // 
            this.Copyright.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Copyright.Location = new System.Drawing.Point(114, 258);
            this.Copyright.Name = "Copyright";
            this.Copyright.Size = new System.Drawing.Size(370, 43);
            this.Copyright.TabIndex = 9;
            this.Copyright.Text = "版权信息";
            this.Copyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Labsqinfo
            // 
            this.Labsqinfo.AutoSize = true;
            this.Labsqinfo.BackColor = System.Drawing.Color.Transparent;
            this.Labsqinfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.Labsqinfo.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Labsqinfo.ForeColor = System.Drawing.Color.Green;
            this.Labsqinfo.Location = new System.Drawing.Point(0, 0);
            this.Labsqinfo.Name = "Labsqinfo";
            this.Labsqinfo.Size = new System.Drawing.Size(0, 20);
            this.Labsqinfo.TabIndex = 10;
            this.Labsqinfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Labyymc
            // 
            this.Labyymc.AutoSize = true;
            this.Labyymc.BackColor = System.Drawing.Color.Transparent;
            this.Labyymc.Dock = System.Windows.Forms.DockStyle.Left;
            this.Labyymc.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Labyymc.ForeColor = System.Drawing.Color.Green;
            this.Labyymc.Location = new System.Drawing.Point(0, 0);
            this.Labyymc.Name = "Labyymc";
            this.Labyymc.Size = new System.Drawing.Size(0, 20);
            this.Labyymc.TabIndex = 11;
            this.Labyymc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimerMain
            // 
            this.TimerMain.Tick += new System.EventHandler(this.TimerMain_Tick);
            // 
            // Frm_splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(487, 303);
            this.Controls.Add(this.Labyymc);
            this.Controls.Add(this.Labsqinfo);
            this.Controls.Add(this.Copyright);
            this.Controls.Add(this.PictureBox1);
            this.Controls.Add(this.Versiontxt);
            this.Controls.Add(this.ApplicationProductName);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_splash";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_splash";
            this.Load += new System.EventHandler(this.Frm_splash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Versiontxt;
        internal System.Windows.Forms.Label ApplicationProductName;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.Label Copyright;
        internal System.Windows.Forms.Label Labsqinfo;
        internal System.Windows.Forms.Label Labyymc;
        private System.Windows.Forms.Timer TimerMain;
    }
}