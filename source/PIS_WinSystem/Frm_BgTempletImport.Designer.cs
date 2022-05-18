namespace PIS_Sys
{
    partial class Frm_BgTempletImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BgTempletImport));
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.ComboBox2 = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.tvwCopyTemplete = new System.Windows.Forms.TreeView();
            this.Button1 = new System.Windows.Forms.Button();
            this.tvwSourceTemplete = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(332, 407);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(102, 23);
            this.TextBox2.TabIndex = 17;
            this.TextBox2.Visible = false;
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(328, 24);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(107, 23);
            this.TextBox1.TabIndex = 16;
            this.TextBox1.Visible = false;
            // 
            // ComboBox2
            // 
            this.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox2.FormattingEnabled = true;
            this.ComboBox2.Location = new System.Drawing.Point(314, 331);
            this.ComboBox2.Name = "ComboBox2";
            this.ComboBox2.Size = new System.Drawing.Size(142, 22);
            this.ComboBox2.TabIndex = 15;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(321, 309);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(77, 14);
            this.Label2.TabIndex = 14;
            this.Label2.Text = "模板需求者";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(322, 92);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(77, 14);
            this.Label1.TabIndex = 13;
            this.Label1.Text = "模板所有者";
            // 
            // ComboBox1
            // 
            this.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Location = new System.Drawing.Point(314, 110);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(143, 22);
            this.ComboBox1.TabIndex = 12;
            // 
            // tvwCopyTemplete
            // 
            this.tvwCopyTemplete.Dock = System.Windows.Forms.DockStyle.Right;
            this.tvwCopyTemplete.Location = new System.Drawing.Point(470, 0);
            this.tvwCopyTemplete.Name = "tvwCopyTemplete";
            this.tvwCopyTemplete.Size = new System.Drawing.Size(285, 600);
            this.tvwCopyTemplete.TabIndex = 11;
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(341, 201);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(83, 44);
            this.Button1.TabIndex = 10;
            this.Button1.Text = "》》";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // tvwSourceTemplete
            // 
            this.tvwSourceTemplete.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvwSourceTemplete.Location = new System.Drawing.Point(0, 0);
            this.tvwSourceTemplete.Name = "tvwSourceTemplete";
            this.tvwSourceTemplete.Size = new System.Drawing.Size(306, 600);
            this.tvwSourceTemplete.TabIndex = 9;
            // 
            // Frm_BgTempletImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(755, 600);
            this.Controls.Add(this.TextBox2);
            this.Controls.Add(this.TextBox1);
            this.Controls.Add(this.ComboBox2);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.tvwCopyTemplete);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.tvwSourceTemplete);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Frm_BgTempletImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病理诊断模板导入";
            this.Load += new System.EventHandler(this.Frm_TempletImport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox TextBox2;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.ComboBox ComboBox2;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ComboBox ComboBox1;
        internal System.Windows.Forms.TreeView tvwCopyTemplete;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.TreeView tvwSourceTemplete;
    }
}