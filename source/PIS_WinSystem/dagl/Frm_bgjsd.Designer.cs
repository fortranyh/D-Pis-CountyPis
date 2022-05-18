namespace PIS_Sys.dagl
{
    partial class Frm_bgjsd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_bgjsd));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.chkEnd = new System.Windows.Forms.CheckBox();
            this.ChkStart = new System.Windows.Forms.CheckBox();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.reportViewer1 = new FastReportLib.ReportViewer();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cmb_patSource = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cmb_patSource);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxEx1);
            this.splitContainer1.Panel1.Controls.Add(this.labelX2);
            this.splitContainer1.Panel1.Controls.Add(this.labelX1);
            this.splitContainer1.Panel1.Controls.Add(this.dtStart);
            this.splitContainer1.Panel1.Controls.Add(this.buttonX1);
            this.splitContainer1.Panel1.Controls.Add(this.chkEnd);
            this.splitContainer1.Panel1.Controls.Add(this.ChkStart);
            this.splitContainer1.Panel1.Controls.Add(this.dtEnd);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.reportViewer1);
            this.splitContainer1.Size = new System.Drawing.Size(1175, 462);
            this.splitContainer1.SplitterDistance = 232;
            this.splitContainer1.TabIndex = 1;
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 20;
            this.comboBoxEx1.Location = new System.Drawing.Point(92, 134);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(126, 26);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 51;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(14, 136);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(98, 23);
            this.labelX1.TabIndex = 50;
            this.labelX1.Text = "报告医师：";
            // 
            // dtStart
            // 
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(27, 39);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(191, 26);
            this.dtStart.TabIndex = 44;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(61, 195);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(125, 74);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 48;
            this.buttonX1.Text = "查询";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // chkEnd
            // 
            this.chkEnd.AutoSize = true;
            this.chkEnd.Checked = true;
            this.chkEnd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnd.Enabled = false;
            this.chkEnd.Location = new System.Drawing.Point(14, 66);
            this.chkEnd.Name = "chkEnd";
            this.chkEnd.Size = new System.Drawing.Size(43, 20);
            this.chkEnd.TabIndex = 46;
            this.chkEnd.Text = "到";
            this.chkEnd.UseVisualStyleBackColor = true;
            // 
            // ChkStart
            // 
            this.ChkStart.AutoSize = true;
            this.ChkStart.Checked = true;
            this.ChkStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkStart.Enabled = false;
            this.ChkStart.Location = new System.Drawing.Point(14, 21);
            this.ChkStart.Name = "ChkStart";
            this.ChkStart.Size = new System.Drawing.Size(43, 20);
            this.ChkStart.TabIndex = 47;
            this.ChkStart.Text = "从";
            this.ChkStart.UseVisualStyleBackColor = true;
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(27, 86);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(191, 26);
            this.dtEnd.TabIndex = 45;
            // 
            // reportViewer1
            // 
            this.reportViewer1.BackColor = System.Drawing.Color.SteelBlue;
            this.reportViewer1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("reportViewer1.BackgroundImage")));
            this.reportViewer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(939, 462);
            this.reportViewer1.TabIndex = 0;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(14, 165);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(98, 23);
            this.labelX2.TabIndex = 50;
            this.labelX2.Text = "病人来源：";
            // 
            // cmb_patSource
            // 
            this.cmb_patSource.DisplayMember = "Text";
            this.cmb_patSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_patSource.FormattingEnabled = true;
            this.cmb_patSource.ItemHeight = 20;
            this.cmb_patSource.Location = new System.Drawing.Point(92, 163);
            this.cmb_patSource.Name = "cmb_patSource";
            this.cmb_patSource.Size = new System.Drawing.Size(126, 26);
            this.cmb_patSource.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmb_patSource.TabIndex = 51;
            // 
            // Frm_bgjsd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(1175, 462);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_bgjsd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报告接收单查询打印";
            this.Load += new System.EventHandler(this.Frm_bgjsd_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        internal System.Windows.Forms.DateTimePicker dtStart;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        internal System.Windows.Forms.CheckBox chkEnd;
        internal System.Windows.Forms.CheckBox ChkStart;
        internal System.Windows.Forms.DateTimePicker dtEnd;
        private FastReportLib.ReportViewer reportViewer1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmb_patSource;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}