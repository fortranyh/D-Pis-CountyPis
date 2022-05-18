﻿namespace PIS_Statistics
{
    partial class Frm_HeQp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_HeQp));
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.chkEnd = new System.Windows.Forms.CheckBox();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.ChkStart = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(8, 82);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(546, 75);
            this.labelX1.TabIndex = 75;
            this.labelX1.Text = "统计值：";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Image = global::PIS_Statistics.Properties.Resources.query_32px_1181401;
            this.buttonX1.Location = new System.Drawing.Point(357, 11);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(112, 64);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 74;
            this.buttonX1.Text = "查询";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // chkEnd
            // 
            this.chkEnd.AutoSize = true;
            this.chkEnd.Checked = true;
            this.chkEnd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnd.Enabled = false;
            this.chkEnd.Location = new System.Drawing.Point(188, 38);
            this.chkEnd.Name = "chkEnd";
            this.chkEnd.Size = new System.Drawing.Size(40, 18);
            this.chkEnd.TabIndex = 72;
            this.chkEnd.Text = "到";
            this.chkEnd.UseVisualStyleBackColor = true;
            // 
            // dtStart
            // 
            this.dtStart.CustomFormat = "yyyy-MM-dd";
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(71, 33);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(111, 23);
            this.dtStart.TabIndex = 70;
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(232, 34);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(107, 23);
            this.dtEnd.TabIndex = 71;
            // 
            // ChkStart
            // 
            this.ChkStart.AutoSize = true;
            this.ChkStart.Checked = true;
            this.ChkStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkStart.Enabled = false;
            this.ChkStart.Location = new System.Drawing.Point(8, 39);
            this.ChkStart.Name = "ChkStart";
            this.ChkStart.Size = new System.Drawing.Size(40, 18);
            this.ChkStart.TabIndex = 73;
            this.ChkStart.Text = "从";
            this.ChkStart.UseVisualStyleBackColor = true;
            // 
            // Frm_HeQp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(567, 170);
            this.Controls.Add(this.dtEnd);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.chkEnd);
            this.Controls.Add(this.dtStart);
            this.Controls.Add(this.ChkStart);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_HeQp";
            this.Text = "HE染色切片优良率";
            this.Load += new System.EventHandler(this.Frm_HeQp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        internal System.Windows.Forms.CheckBox chkEnd;
        internal System.Windows.Forms.DateTimePicker dtStart;
        internal System.Windows.Forms.DateTimePicker dtEnd;
        internal System.Windows.Forms.CheckBox ChkStart;
    }
}