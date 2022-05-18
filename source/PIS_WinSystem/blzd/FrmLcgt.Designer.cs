namespace PIS_Sys.blzd
{
    partial class FrmLcgt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLcgt));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtLcks = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtLcys = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.txtBlys = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.dtGtsj = new System.Windows.Forms.DateTimePicker();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txtGtnr = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(22, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(105, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "临床科室：";
            // 
            // txtLcks
            // 
            // 
            // 
            // 
            this.txtLcks.Border.Class = "TextBoxBorder";
            this.txtLcks.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLcks.Location = new System.Drawing.Point(97, 10);
            this.txtLcks.Name = "txtLcks";
            this.txtLcks.PreventEnterBeep = true;
            this.txtLcks.Size = new System.Drawing.Size(127, 26);
            this.txtLcks.TabIndex = 1;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(251, 13);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(105, 23);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "临床医生：";
            // 
            // txtLcys
            // 
            // 
            // 
            // 
            this.txtLcys.Border.Class = "TextBoxBorder";
            this.txtLcys.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLcys.Location = new System.Drawing.Point(329, 10);
            this.txtLcys.Name = "txtLcys";
            this.txtLcys.PreventEnterBeep = true;
            this.txtLcys.Size = new System.Drawing.Size(127, 26);
            this.txtLcys.TabIndex = 1;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(22, 53);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(105, 23);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "病理医生：";
            // 
            // txtBlys
            // 
            // 
            // 
            // 
            this.txtBlys.Border.Class = "TextBoxBorder";
            this.txtBlys.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBlys.Location = new System.Drawing.Point(97, 50);
            this.txtBlys.Name = "txtBlys";
            this.txtBlys.PreventEnterBeep = true;
            this.txtBlys.Size = new System.Drawing.Size(127, 26);
            this.txtBlys.TabIndex = 1;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(251, 53);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(105, 23);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "沟通时间：";
            // 
            // dtGtsj
            // 
            this.dtGtsj.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtGtsj.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtGtsj.Location = new System.Drawing.Point(329, 50);
            this.dtGtsj.Name = "dtGtsj";
            this.dtGtsj.Size = new System.Drawing.Size(189, 26);
            this.dtGtsj.TabIndex = 2;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(22, 92);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(105, 23);
            this.labelX5.TabIndex = 0;
            this.labelX5.Text = "沟通内容：";
            // 
            // txtGtnr
            // 
            // 
            // 
            // 
            this.txtGtnr.BackgroundStyle.Class = "RichTextBoxBorder";
            this.txtGtnr.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtGtnr.Location = new System.Drawing.Point(102, 96);
            this.txtGtnr.MaxLength = 500;
            this.txtGtnr.Name = "txtGtnr";
            this.txtGtnr.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fnil\\fcharset" +
    "134 \\\'cb\\\'ce\\\'cc\\\'e5;}}\r\n\\viewkind4\\uc1\\pard\\lang2052\\f0\\fs24\\par\r\n}\r\n";
            this.txtGtnr.Size = new System.Drawing.Size(416, 191);
            this.txtGtnr.TabIndex = 3;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(433, 293);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(85, 45);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 4;
            this.buttonX1.Text = "保存";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // FrmLcgt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(543, 340);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.txtGtnr);
            this.Controls.Add(this.dtGtsj);
            this.Controls.Add(this.txtLcys);
            this.Controls.Add(this.txtBlys);
            this.Controls.Add(this.txtLcks);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmLcgt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "与临床医生沟通";
            this.Load += new System.EventHandler(this.FrmLcgt_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLcks;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLcys;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBlys;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.DateTimePicker dtGtsj;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx txtGtnr;
        private DevComponents.DotNetBar.ButtonX buttonX1;
    }
}