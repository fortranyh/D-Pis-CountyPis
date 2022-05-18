namespace PIS_Sys.dagl
{
    partial class Frm_dmt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_dmt));
            this.imageListView1 = new Manina.Windows.Forms.ImageListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.thumbnailsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.galleryToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.paneToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.clearThumbsToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.x96ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x120ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x200ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重置缩略图大小ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.colorToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.listViewEx1 = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filepath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListView1
            // 
            this.imageListView1.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.imageListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageListView1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imageListView1.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.imageListView1.Location = new System.Drawing.Point(0, 25);
            this.imageListView1.Name = "imageListView1";
            this.imageListView1.PersistentCacheDirectory = "";
            this.imageListView1.PersistentCacheSize = ((long)(100));
            this.imageListView1.Size = new System.Drawing.Size(964, 510);
            this.imageListView1.TabIndex = 6;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thumbnailsToolStripButton,
            this.galleryToolStripButton,
            this.paneToolStripButton,
            this.toolStripSeparator3,
            this.clearThumbsToolStripButton,
            this.toolStripDropDownButton1,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.colorToolStripComboBox,
            this.toolStripLabel2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(964, 25);
            this.toolStrip1.TabIndex = 7;
            // 
            // thumbnailsToolStripButton
            // 
            this.thumbnailsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.thumbnailsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("thumbnailsToolStripButton.Image")));
            this.thumbnailsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.thumbnailsToolStripButton.Name = "thumbnailsToolStripButton";
            this.thumbnailsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.thumbnailsToolStripButton.Text = "平铺模式";
            this.thumbnailsToolStripButton.Click += new System.EventHandler(this.thumbnailsToolStripButton_Click);
            // 
            // galleryToolStripButton
            // 
            this.galleryToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.galleryToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("galleryToolStripButton.Image")));
            this.galleryToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.galleryToolStripButton.Name = "galleryToolStripButton";
            this.galleryToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.galleryToolStripButton.Text = "相册模式";
            this.galleryToolStripButton.Click += new System.EventHandler(this.galleryToolStripButton_Click);
            // 
            // paneToolStripButton
            // 
            this.paneToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.paneToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("paneToolStripButton.Image")));
            this.paneToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.paneToolStripButton.Name = "paneToolStripButton";
            this.paneToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.paneToolStripButton.Text = "分栏模式";
            this.paneToolStripButton.Click += new System.EventHandler(this.paneToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // clearThumbsToolStripButton
            // 
            this.clearThumbsToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearThumbsToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("clearThumbsToolStripButton.Image")));
            this.clearThumbsToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearThumbsToolStripButton.Name = "clearThumbsToolStripButton";
            this.clearThumbsToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.clearThumbsToolStripButton.Text = "清空缩略图存";
            this.clearThumbsToolStripButton.Click += new System.EventHandler(this.clearThumbsToolStripButton_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x96ToolStripMenuItem,
            this.x120ToolStripMenuItem,
            this.x200ToolStripMenuItem,
            this.重置缩略图大小ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(81, 22);
            this.toolStripDropDownButton1.Text = "缩略图大小";
            // 
            // x96ToolStripMenuItem
            // 
            this.x96ToolStripMenuItem.Name = "x96ToolStripMenuItem";
            this.x96ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.x96ToolStripMenuItem.Text = "1/10";
            this.x96ToolStripMenuItem.Click += new System.EventHandler(this.x96ToolStripMenuItem_Click);
            // 
            // x120ToolStripMenuItem
            // 
            this.x120ToolStripMenuItem.Name = "x120ToolStripMenuItem";
            this.x120ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.x120ToolStripMenuItem.Text = "2/10";
            this.x120ToolStripMenuItem.Click += new System.EventHandler(this.x120ToolStripMenuItem_Click);
            // 
            // x200ToolStripMenuItem
            // 
            this.x200ToolStripMenuItem.Name = "x200ToolStripMenuItem";
            this.x200ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.x200ToolStripMenuItem.Text = "3/10";
            this.x200ToolStripMenuItem.Click += new System.EventHandler(this.x200ToolStripMenuItem_Click);
            // 
            // 重置缩略图大小ToolStripMenuItem
            // 
            this.重置缩略图大小ToolStripMenuItem.Name = "重置缩略图大小ToolStripMenuItem";
            this.重置缩略图大小ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.重置缩略图大小ToolStripMenuItem.Text = "重置缩略图大小";
            this.重置缩略图大小ToolStripMenuItem.Click += new System.EventHandler(this.重置缩略图大小ToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(97, 22);
            this.toolStripLabel1.Text = "显示/隐藏滚动条";
            this.toolStripLabel1.Click += new System.EventHandler(this.toolStripLabel1_Click);
            // 
            // colorToolStripComboBox
            // 
            this.colorToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorToolStripComboBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.colorToolStripComboBox.Name = "colorToolStripComboBox";
            this.colorToolStripComboBox.Size = new System.Drawing.Size(80, 25);
            this.colorToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.colorToolStripComboBox_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(0, 22);
            // 
            // listViewEx1
            // 
            // 
            // 
            // 
            this.listViewEx1.Border.Class = "ListViewBorder";
            this.listViewEx1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.listViewEx1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.type,
            this.filename,
            this.filepath});
            this.listViewEx1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listViewEx1.FullRowSelect = true;
            this.listViewEx1.GridLines = true;
            this.listViewEx1.Location = new System.Drawing.Point(0, 535);
            this.listViewEx1.Name = "listViewEx1";
            this.listViewEx1.Size = new System.Drawing.Size(964, 146);
            this.listViewEx1.TabIndex = 8;
            this.listViewEx1.UseCompatibleStateImageBehavior = false;
            this.listViewEx1.View = System.Windows.Forms.View.Details;
            this.listViewEx1.DoubleClick += new System.EventHandler(this.listViewEx1_DoubleClick);
            // 
            // type
            // 
            this.type.Text = "类型";
            // 
            // filename
            // 
            this.filename.Text = "文件名";
            this.filename.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.filename.Width = 220;
            // 
            // filepath
            // 
            this.filepath.Text = "文件目录";
            this.filepath.Width = 600;
            // 
            // Frm_dmt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(964, 681);
            this.Controls.Add(this.imageListView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.listViewEx1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_dmt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "多媒体资料";
            this.Load += new System.EventHandler(this.Frm_dmt_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Manina.Windows.Forms.ImageListView imageListView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton thumbnailsToolStripButton;
        private System.Windows.Forms.ToolStripButton galleryToolStripButton;
        private System.Windows.Forms.ToolStripButton paneToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton clearThumbsToolStripButton;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem x96ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x120ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem x200ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重置缩略图大小ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox colorToolStripComboBox;
        private DevComponents.DotNetBar.Controls.ListViewEx listViewEx1;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader filename;
        private System.Windows.Forms.ColumnHeader filepath;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    }
}