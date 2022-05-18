namespace MshotDigitsCamControl
{
    partial class DigitsCamUserCtl
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
            DigitsCamClosed();
            this.HandleCreated -= DigitsCamUserCtl_HandleCreated;
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelControl = new System.Windows.Forms.Panel();
            this.BUTTON_SAVE = new System.Windows.Forms.Button();
            this.BUTTON_CAPImg = new System.Windows.Forms.Button();
            this.General = new System.Windows.Forms.GroupBox();
            this.BUTTON_PROPERTY = new System.Windows.Forms.Button();
            this.BUTTON_START = new System.Windows.Forms.Button();
            this.BUTTON_SCAN = new System.Windows.Forms.Button();
            this.BUTTON_OPEN = new System.Windows.Forms.Button();
            this.COMBO_DEVICES = new System.Windows.Forms.ComboBox();
            this.pictureBoxCam = new System.Windows.Forms.PictureBox();
            this.panelControl.SuspendLayout();
            this.General.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCam)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl
            // 
            this.panelControl.BackColor = System.Drawing.Color.Transparent;
            this.panelControl.Controls.Add(this.BUTTON_SAVE);
            this.panelControl.Controls.Add(this.BUTTON_CAPImg);
            this.panelControl.Controls.Add(this.General);
            this.panelControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl.Location = new System.Drawing.Point(0, 541);
            this.panelControl.Name = "panelControl";
            this.panelControl.Size = new System.Drawing.Size(593, 59);
            this.panelControl.TabIndex = 0;
            // 
            // BUTTON_SAVE
            // 
            this.BUTTON_SAVE.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BUTTON_SAVE.Image = global::MshotDigitsCamControl.Properties.Resources.save32px;
            this.BUTTON_SAVE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BUTTON_SAVE.Location = new System.Drawing.Point(328, 6);
            this.BUTTON_SAVE.Name = "BUTTON_SAVE";
            this.BUTTON_SAVE.Size = new System.Drawing.Size(102, 50);
            this.BUTTON_SAVE.TabIndex = 23;
            this.BUTTON_SAVE.Text = "另存";
            this.BUTTON_SAVE.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BUTTON_SAVE.UseVisualStyleBackColor = false;
            this.BUTTON_SAVE.Click += new System.EventHandler(this.BUTTON_SAVE_Click);
            // 
            // BUTTON_CAPImg
            // 
            this.BUTTON_CAPImg.Font = new System.Drawing.Font("黑体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BUTTON_CAPImg.Image = global::MshotDigitsCamControl.Properties.Resources.image32px;
            this.BUTTON_CAPImg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BUTTON_CAPImg.Location = new System.Drawing.Point(434, 6);
            this.BUTTON_CAPImg.Name = "BUTTON_CAPImg";
            this.BUTTON_CAPImg.Size = new System.Drawing.Size(102, 50);
            this.BUTTON_CAPImg.TabIndex = 23;
            this.BUTTON_CAPImg.Text = "采图";
            this.BUTTON_CAPImg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BUTTON_CAPImg.UseVisualStyleBackColor = false;
            this.BUTTON_CAPImg.Click += new System.EventHandler(this.BUTTON_CAPImg_Click);
            // 
            // General
            // 
            this.General.Controls.Add(this.BUTTON_PROPERTY);
            this.General.Controls.Add(this.BUTTON_START);
            this.General.Controls.Add(this.BUTTON_SCAN);
            this.General.Controls.Add(this.BUTTON_OPEN);
            this.General.Controls.Add(this.COMBO_DEVICES);
            this.General.Location = new System.Drawing.Point(3, 6);
            this.General.Name = "General";
            this.General.Size = new System.Drawing.Size(322, 50);
            this.General.TabIndex = 16;
            this.General.TabStop = false;
            this.General.Text = "通用";
            // 
            // BUTTON_PROPERTY
            // 
            this.BUTTON_PROPERTY.Location = new System.Drawing.Point(272, 18);
            this.BUTTON_PROPERTY.Name = "BUTTON_PROPERTY";
            this.BUTTON_PROPERTY.Size = new System.Drawing.Size(47, 23);
            this.BUTTON_PROPERTY.TabIndex = 5;
            this.BUTTON_PROPERTY.Text = "属性";
            this.BUTTON_PROPERTY.UseVisualStyleBackColor = true;
            this.BUTTON_PROPERTY.Click += new System.EventHandler(this.BUTTON_PROPERTY_Click);
            // 
            // BUTTON_START
            // 
            this.BUTTON_START.Location = new System.Drawing.Point(220, 18);
            this.BUTTON_START.Name = "BUTTON_START";
            this.BUTTON_START.Size = new System.Drawing.Size(46, 23);
            this.BUTTON_START.TabIndex = 4;
            this.BUTTON_START.Text = "3开始";
            this.BUTTON_START.UseVisualStyleBackColor = true;
            this.BUTTON_START.Click += new System.EventHandler(this.BUTTON_START_Click);
            // 
            // BUTTON_SCAN
            // 
            this.BUTTON_SCAN.Location = new System.Drawing.Point(112, 17);
            this.BUTTON_SCAN.Name = "BUTTON_SCAN";
            this.BUTTON_SCAN.Size = new System.Drawing.Size(47, 23);
            this.BUTTON_SCAN.TabIndex = 3;
            this.BUTTON_SCAN.Text = "1设备";
            this.BUTTON_SCAN.UseVisualStyleBackColor = true;
            this.BUTTON_SCAN.Click += new System.EventHandler(this.BUTTON_SCAN_Click);
            // 
            // BUTTON_OPEN
            // 
            this.BUTTON_OPEN.Location = new System.Drawing.Point(165, 18);
            this.BUTTON_OPEN.Name = "BUTTON_OPEN";
            this.BUTTON_OPEN.Size = new System.Drawing.Size(50, 23);
            this.BUTTON_OPEN.TabIndex = 2;
            this.BUTTON_OPEN.Text = "2打开";
            this.BUTTON_OPEN.UseVisualStyleBackColor = true;
            this.BUTTON_OPEN.Click += new System.EventHandler(this.BUTTON_OPEN_Click);
            // 
            // COMBO_DEVICES
            // 
            this.COMBO_DEVICES.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.COMBO_DEVICES.FormattingEnabled = true;
            this.COMBO_DEVICES.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.COMBO_DEVICES.Location = new System.Drawing.Point(5, 18);
            this.COMBO_DEVICES.Name = "COMBO_DEVICES";
            this.COMBO_DEVICES.Size = new System.Drawing.Size(104, 20);
            this.COMBO_DEVICES.TabIndex = 0;
            // 
            // pictureBoxCam
            // 
            this.pictureBoxCam.BackColor = System.Drawing.Color.Black;
            this.pictureBoxCam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxCam.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxCam.Name = "pictureBoxCam";
            this.pictureBoxCam.Size = new System.Drawing.Size(593, 541);
            this.pictureBoxCam.TabIndex = 1;
            this.pictureBoxCam.TabStop = false;
            this.pictureBoxCam.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDoubleClick);
            // 
            // DigitsCamUserCtl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBoxCam);
            this.Controls.Add(this.panelControl);
            this.Name = "DigitsCamUserCtl";
            this.Size = new System.Drawing.Size(593, 600);
            this.panelControl.ResumeLayout(false);
            this.General.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCam)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControl;
        private System.Windows.Forms.GroupBox General;
        private System.Windows.Forms.Button BUTTON_PROPERTY;
        private System.Windows.Forms.Button BUTTON_START;
        private System.Windows.Forms.Button BUTTON_SCAN;
        private System.Windows.Forms.Button BUTTON_OPEN;
        private System.Windows.Forms.ComboBox COMBO_DEVICES;
        private System.Windows.Forms.PictureBox pictureBoxCam;
        private System.Windows.Forms.Button BUTTON_CAPImg;
        private System.Windows.Forms.Button BUTTON_SAVE;
    }
}
