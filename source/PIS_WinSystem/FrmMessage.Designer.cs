namespace PIS_Sys
{
    partial class FrmMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMessage));
            this.richTextBoxEx1 = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.comboBoxItem1 = new DevComponents.DotNetBar.ComboBoxItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.richTextBoxEx2 = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.SuspendLayout();
            // 
            // richTextBoxEx1
            // 
            // 
            // 
            // 
            this.richTextBoxEx1.BackgroundStyle.Class = "RichTextBoxBorder";
            this.richTextBoxEx1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.richTextBoxEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.richTextBoxEx1.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxEx1.Name = "richTextBoxEx1";
            this.richTextBoxEx1.ReadOnly = true;
            this.richTextBoxEx1.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\nouicompat\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fn" +
    "il\\fcharset134 \\\'cb\\\'ce\\\'cc\\\'e5;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4" +
    "\\uc1 \r\n\\pard\\f0\\fs21\\lang2052\\par\r\n}\r\n";
            this.richTextBoxEx1.Size = new System.Drawing.Size(673, 318);
            this.richTextBoxEx1.TabIndex = 1;
            this.richTextBoxEx1.TabStop = false;
            // 
            // ribbonBar1
            // 
            this.ribbonBar1.AutoOverflowEnabled = true;
            // 
            // 
            // 
            this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBar1.ContainerControlProcessDialogKey = true;
            this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ribbonBar1.DragDropSupport = true;
            this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.comboBoxItem1,
            this.buttonItem1,
            this.buttonItem2});
            this.ribbonBar1.ItemSpacing = 6;
            this.ribbonBar1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBar1.Location = new System.Drawing.Point(0, 441);
            this.ribbonBar1.Name = "ribbonBar1";
            this.ribbonBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ribbonBar1.Size = new System.Drawing.Size(673, 26);
            this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBar1.TabIndex = 2;
            // 
            // 
            // 
            this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBar1.TitleVisible = false;
            this.ribbonBar1.VerticalItemAlignment = DevComponents.DotNetBar.eVerticalItemsAlignment.Middle;
            // 
            // labelItem1
            // 
            this.labelItem1.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "发送给";
            this.labelItem1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // comboBoxItem1
            // 
            this.comboBoxItem1.ComboWidth = 100;
            this.comboBoxItem1.DropDownHeight = 106;
            this.comboBoxItem1.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
            this.comboBoxItem1.ItemHeight = 18;
            this.comboBoxItem1.Name = "comboBoxItem1";
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem1.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem1.Icon")));
            this.buttonItem1.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.buttonItem1.SubItemsExpandWidth = 14;
            this.buttonItem1.Text = "发送";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // buttonItem2
            // 
            this.buttonItem2.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem2.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem2.Icon")));
            this.buttonItem2.Name = "buttonItem2";
            this.buttonItem2.SubItemsExpandWidth = 14;
            this.buttonItem2.Text = "刷新";
            this.buttonItem2.Click += new System.EventHandler(this.buttonItem2_Click);
            // 
            // richTextBoxEx2
            // 
            // 
            // 
            // 
            this.richTextBoxEx2.BackgroundStyle.Class = "RichTextBoxBorder";
            this.richTextBoxEx2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.richTextBoxEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxEx2.Location = new System.Drawing.Point(0, 318);
            this.richTextBoxEx2.MaxLength = 50;
            this.richTextBoxEx2.Name = "richTextBoxEx2";
            this.richTextBoxEx2.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\nouicompat\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fn" +
    "il\\fcharset134 \\\'cb\\\'ce\\\'cc\\\'e5;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4" +
    "\\uc1 \r\n\\pard\\f0\\fs21\\lang2052\\par\r\n}\r\n";
            this.richTextBoxEx2.Size = new System.Drawing.Size(673, 123);
            this.richTextBoxEx2.TabIndex = 2;
            // 
            // FrmMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(673, 467);
            this.Controls.Add(this.richTextBoxEx2);
            this.Controls.Add(this.ribbonBar1);
            this.Controls.Add(this.richTextBoxEx1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "科室留言";
            this.Activated += new System.EventHandler(this.FrmMessage_Activated);
            this.Load += new System.EventHandler(this.FrmMessage_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.RichTextBoxEx richTextBoxEx1;
        private DevComponents.DotNetBar.RibbonBar ribbonBar1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ComboBoxItem comboBoxItem1;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx richTextBoxEx2;
        private DevComponents.DotNetBar.ButtonItem buttonItem2;
    }
}