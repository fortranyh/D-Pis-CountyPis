namespace PIS_Sys.qcgl
{
    partial class Frm_Dtcj
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Dtcj));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btn_playVideo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.btn_PZ = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_LX = new System.Windows.Forms.ToolStripButton();
            this.btn_lxstop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_closecj = new System.Windows.Forms.ToolStripButton();
            this.VideoTimeProgress = new System.Windows.Forms.ToolStripLabel();
            this.bar3 = new DevComponents.DotNetBar.Bar();
            this.lblsqks = new DevComponents.DotNetBar.LabelItem();
            this.devicesCombo = new DevComponents.DotNetBar.ComboBoxItem();
            this.buttonItem13 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem14 = new DevComponents.DotNetBar.ButtonItem();
            this.ftpDownload1 = new FtpDownloader.FtpDownload();
            this.imageListView1 = new Manina.Windows.Forms.ImageListView();
            this.listViewEx1 = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.filepath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.txt_pz_blh = new DevComponents.DotNetBar.TextBoxItem();
            this.txt_pz_xm = new DevComponents.DotNetBar.TextBoxItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar3)).BeginInit();
            this.toolStrip1.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.videoSourcePlayer1);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            this.splitContainer1.Panel1.Controls.Add(this.bar3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ftpDownload1);
            this.splitContainer1.Panel2.Controls.Add(this.imageListView1);
            this.splitContainer1.Panel2.Controls.Add(this.listViewEx1);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(1276, 733);
            this.splitContainer1.SplitterDistance = 758;
            this.splitContainer1.TabIndex = 0;
            // 
            // videoSourcePlayer1
            // 
            this.videoSourcePlayer1.BackColor = System.Drawing.Color.Black;
            this.videoSourcePlayer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoSourcePlayer1.Location = new System.Drawing.Point(0, 30);
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.Size = new System.Drawing.Size(758, 664);
            this.videoSourcePlayer1.TabIndex = 42;
            this.videoSourcePlayer1.Text = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            this.videoSourcePlayer1.NewFrame += new AForge.Controls.VideoSourcePlayer.NewFrameHandler(this.videoSourcePlayer1_NewFrame);
            this.videoSourcePlayer1.DoubleClick += new System.EventHandler(this.videoSourcePlayer1_DoubleClick);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_playVideo,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.btn_PZ,
            this.toolStripSeparator5,
            this.btn_LX,
            this.btn_lxstop,
            this.toolStripSeparator1,
            this.btn_closecj,
            this.VideoTimeProgress});
            this.toolStrip2.Location = new System.Drawing.Point(0, 694);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStrip2.Size = new System.Drawing.Size(758, 39);
            this.toolStrip2.TabIndex = 44;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btn_playVideo
            // 
            this.btn_playVideo.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_playVideo.Image = global::PIS_Sys.Properties.Resources.Media_Player_Windows_32px_1072557_easyicon1;
            this.btn_playVideo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_playVideo.Name = "btn_playVideo";
            this.btn_playVideo.Size = new System.Drawing.Size(101, 36);
            this.btn_playVideo.Text = "播放视频";
            this.btn_playVideo.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_playVideo.Click += new System.EventHandler(this.btn_playVideo_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(65, 36);
            this.toolStripLabel2.Text = "按 F4 拍照";
            // 
            // btn_PZ
            // 
            this.btn_PZ.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_PZ.Image = global::PIS_Sys.Properties.Resources.Camera;
            this.btn_PZ.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_PZ.Name = "btn_PZ";
            this.btn_PZ.Size = new System.Drawing.Size(73, 36);
            this.btn_PZ.Text = "拍照";
            this.btn_PZ.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_PZ.Click += new System.EventHandler(this.btn_PZ_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 39);
            // 
            // btn_LX
            // 
            this.btn_LX.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_LX.Image = global::PIS_Sys.Properties.Resources.video_start;
            this.btn_LX.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_LX.Name = "btn_LX";
            this.btn_LX.Size = new System.Drawing.Size(101, 36);
            this.btn_LX.Text = "开始录像";
            this.btn_LX.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_LX.Click += new System.EventHandler(this.btn_ckhd_Click);
            // 
            // btn_lxstop
            // 
            this.btn_lxstop.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_lxstop.Image = global::PIS_Sys.Properties.Resources.Video;
            this.btn_lxstop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_lxstop.Name = "btn_lxstop";
            this.btn_lxstop.Size = new System.Drawing.Size(101, 36);
            this.btn_lxstop.Text = "结束录像";
            this.btn_lxstop.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_lxstop.Click += new System.EventHandler(this.btn_lxstop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // btn_closecj
            // 
            this.btn_closecj.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_closecj.Image = global::PIS_Sys.Properties.Resources.Cross_32px;
            this.btn_closecj.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_closecj.Name = "btn_closecj";
            this.btn_closecj.Size = new System.Drawing.Size(115, 36);
            this.btn_closecj.Text = "关闭视频源";
            this.btn_closecj.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btn_closecj.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // VideoTimeProgress
            // 
            this.VideoTimeProgress.Name = "VideoTimeProgress";
            this.VideoTimeProgress.Size = new System.Drawing.Size(0, 36);
            // 
            // bar3
            // 
            this.bar3.AntiAlias = true;
            this.bar3.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar3.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bar3.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.lblsqks,
            this.devicesCombo,
            this.buttonItem13,
            this.buttonItem14,
            this.txt_pz_blh,
            this.txt_pz_xm});
            this.bar3.ItemSpacing = 6;
            this.bar3.Location = new System.Drawing.Point(0, 0);
            this.bar3.Name = "bar3";
            this.bar3.Size = new System.Drawing.Size(758, 30);
            this.bar3.Stretch = true;
            this.bar3.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
            this.bar3.TabIndex = 41;
            this.bar3.TabStop = false;
            this.bar3.Text = "bar3";
            // 
            // lblsqks
            // 
            this.lblsqks.Name = "lblsqks";
            this.lblsqks.Text = "视频源";
            // 
            // devicesCombo
            // 
            this.devicesCombo.ComboWidth = 90;
            this.devicesCombo.DropDownHeight = 106;
            this.devicesCombo.ItemHeight = 19;
            this.devicesCombo.Name = "devicesCombo";
            // 
            // buttonItem13
            // 
            this.buttonItem13.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem13.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem13.Icon")));
            this.buttonItem13.Name = "buttonItem13";
            this.buttonItem13.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(4);
            this.buttonItem13.Text = "连接视频源";
            this.buttonItem13.Click += new System.EventHandler(this.buttonItem13_Click_1);
            // 
            // buttonItem14
            // 
            this.buttonItem14.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem14.Icon = ((System.Drawing.Icon)(resources.GetObject("buttonItem14.Icon")));
            this.buttonItem14.Name = "buttonItem14";
            this.buttonItem14.Text = "相机属性设置";
            this.buttonItem14.Click += new System.EventHandler(this.buttonItem14_Click_1);
            // 
            // ftpDownload1
            // 
            this.ftpDownload1.Location = new System.Drawing.Point(87, 269);
            this.ftpDownload1.Name = "ftpDownload1";
            this.ftpDownload1.Size = new System.Drawing.Size(264, 31);
            this.ftpDownload1.TabIndex = 7;
            // 
            // imageListView1
            // 
            this.imageListView1.ColumnHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.imageListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageListView1.GroupHeaderFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.imageListView1.Location = new System.Drawing.Point(0, 25);
            this.imageListView1.Name = "imageListView1";
            this.imageListView1.PersistentCacheDirectory = "";
            this.imageListView1.PersistentCacheSize = ((long)(100));
            this.imageListView1.Size = new System.Drawing.Size(514, 602);
            this.imageListView1.TabIndex = 0;
            this.imageListView1.ItemDoubleClick += new Manina.Windows.Forms.ItemDoubleClickEventHandler(this.imageListView1_ItemDoubleClick);
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
            this.listViewEx1.Location = new System.Drawing.Point(0, 627);
            this.listViewEx1.Name = "listViewEx1";
            this.listViewEx1.Size = new System.Drawing.Size(514, 106);
            this.listViewEx1.TabIndex = 6;
            this.listViewEx1.UseCompatibleStateImageBehavior = false;
            this.listViewEx1.View = System.Windows.Forms.View.Details;
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
            this.toolStripLabel3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(514, 25);
            this.toolStrip1.TabIndex = 1;
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
            this.colorToolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(0, 22);
            // 
            // txt_pz_blh
            // 
            this.txt_pz_blh.Name = "txt_pz_blh";
            this.txt_pz_blh.TextBoxWidth = 100;
            this.txt_pz_blh.WatermarkColor = System.Drawing.SystemColors.GrayText;
            // 
            // txt_pz_xm
            // 
            this.txt_pz_xm.Name = "txt_pz_xm";
            this.txt_pz_xm.TextBoxWidth = 100;
            this.txt_pz_xm.WatermarkColor = System.Drawing.SystemColors.GrayText;
            // 
            // Frm_Dtcj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(1276, 733);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Dtcj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "show";
            this.Text = "大体采集工作站";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_Dtcj_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Frm_Dtcj_FormClosed);
            this.Load += new System.EventHandler(this.Frm_Dtcj_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar3)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        private DevComponents.DotNetBar.Bar bar3;
        private DevComponents.DotNetBar.LabelItem lblsqks;
        private DevComponents.DotNetBar.ComboBoxItem devicesCombo;
        private DevComponents.DotNetBar.ButtonItem buttonItem13;
        private DevComponents.DotNetBar.ButtonItem buttonItem14;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox colorToolStripComboBox;
        private System.Windows.Forms.ToolStripMenuItem 重置缩略图大小ToolStripMenuItem;
        private DevComponents.DotNetBar.Controls.ListViewEx listViewEx1;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader filename;
        private System.Windows.Forms.ColumnHeader filepath;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btn_closecj;
        private System.Windows.Forms.ToolStripButton btn_lxstop;
        private System.Windows.Forms.ToolStripButton btn_LX;
        private System.Windows.Forms.ToolStripButton btn_PZ;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btn_playVideo;
        private System.Windows.Forms.ToolStripLabel VideoTimeProgress;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private FtpDownloader.FtpDownload ftpDownload1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private DevComponents.DotNetBar.TextBoxItem txt_pz_blh;
        private DevComponents.DotNetBar.TextBoxItem txt_pz_xm;

    }
}