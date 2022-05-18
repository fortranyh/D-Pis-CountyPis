namespace PIS_Sys
{
    partial class Frm_Ftp_Down1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Ftp_Down));
            this.ftpDownload1 = new FtpDownloader.FtpDownload();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.SuspendLayout();
            // 
            // ftpDownload1
            // 
            this.ftpDownload1.Location = new System.Drawing.Point(0, 4);
            this.ftpDownload1.Name = "ftpDownload1";
            this.ftpDownload1.Size = new System.Drawing.Size(156, 31);
            this.ftpDownload1.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.Location = new System.Drawing.Point(0, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(156, 31);
            this.labelX1.TabIndex = 1;
            this.labelX1.Text = "多媒体资料下载中……";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // Frm_Ftp_Down
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(162, 46);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.ftpDownload1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Ftp_Down";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "图像资料下载";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_Ftp_Down_FormClosed);
            this.Load += new System.EventHandler(this.Frm_Ftp_Down_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FtpDownloader.FtpDownload ftpDownload1;
        private System.Windows.Forms.Timer timer1;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}