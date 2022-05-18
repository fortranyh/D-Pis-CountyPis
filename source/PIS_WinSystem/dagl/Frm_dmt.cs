using DevComponents.DotNetBar;
using Manina.Windows.Forms;
using PIS_Sys.qcgl;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_dmt : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_dmt()
        {
            InitializeComponent();
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.CenterToScreen();
        }
        string ImgFolder;
        string ImgQcFolder;
        //病理号
        public string study_no = "";
        //检查类型
        public string modality = "";
        private void Frm_dmt_Load(object sender, EventArgs e)
        {
            //加载图像列表
            Type colorType = typeof(ImageListViewColor);
            int i = 0;
            foreach (PropertyInfo field in colorType.GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                colorToolStripComboBox.Items.Add(new ColorComboBoxItem(field));
                if (field.Name == "Default")
                    colorToolStripComboBox.SelectedIndex = i;
                i++;
            }
            //报告图像路径
            ImgFolder = Program.APPdirPath + @"\Pis_Cap_Image";
            if (Directory.Exists(ImgFolder) == false)
            {
                Directory.CreateDirectory(ImgFolder);
            }
            //取材图像路径
            ImgQcFolder = Program.APPdirPath + @"\Pis_Image";
            if (Directory.Exists(ImgQcFolder) == false)
            {
                Directory.CreateDirectory(ImgQcFolder);
            }
            //下载
            Frm_Ftp_Down ins = new Frm_Ftp_Down();
            ins.ImgFolder = ImgFolder;
            ins.ImgQcFolder = ImgQcFolder;
            ins.study_no = study_no;
            ins.ShowDialog();
            //缩略图大小设置
            SetThumsSize();
            //加载多媒体信息
            RefreshMultiList();
        }
        //缩略图大小
        int thumsWidth = 0;
        int thumsHeight = 0;
        public void SetThumsSize()
        {
            thumsWidth = PIS_Sys.Properties.Settings.Default.BGthumsWidth;
            thumsHeight = PIS_Sys.Properties.Settings.Default.BGthumsHeight;
            if (thumsWidth < PIS_Sys.Properties.Settings.Default.DTthumsWidth)
            {
                thumsWidth = PIS_Sys.Properties.Settings.Default.DTthumsWidth;
            }
            if (thumsHeight < PIS_Sys.Properties.Settings.Default.DTthumsHeight)
            {
                thumsHeight = PIS_Sys.Properties.Settings.Default.DTthumsHeight;
            }

        }
        public Image GetImage(string path)
        {
            FileStream fs = new System.IO.FileStream(path, FileMode.Open);
            Image result = System.Drawing.Image.FromStream(fs);
            fs.Close();
            return result;
        }
        //刷新本地视频音频和图像
        public void RefreshMultiList()
        {
            listViewEx1.Items.Clear();
            imageListView1.ThumbnailSize = new Size(thumsWidth, thumsHeight);
            imageListView1.SuspendLayout();
            //报告图像
            if (Directory.Exists(ImgFolder + @"\" + study_no) == true)
            {

                foreach (var file in Directory.GetFiles(ImgFolder + @"\" + study_no, "*.jpg"))
                {
                    ImageListViewItem ins = new ImageListViewItem();
                    ins.Tag = file;
                    if (PIS_Sys.Properties.Settings.Default.EditPicName)
                    {
                        string filename1 = file.Substring(file.LastIndexOf(@"\") + 1);
                        DBHelper.BLL.multi_media_info insInfo = new DBHelper.BLL.multi_media_info();
                        string result = insInfo.GetMemo_note(study_no, filename1, 4);
                        if (!result.Equals(""))
                        {
                            ins.Text = result;
                        }
                        else
                        {
                            ins.Text = "BG_" + imageListView1.Items.Count.ToString();
                        }
                    }
                    else
                    {
                        ins.Text = "BG_" + imageListView1.Items.Count.ToString();
                    }

                    ins.FileName = file;
                    if (toolStripLabel2.Text.Equals(""))
                    {
                        Image img = GetImage(file);
                        toolStripLabel2.Text = string.Format("({0}:{1})", img.Width.ToString(), img.Height.ToString());
                    }
                    imageListView1.Items.Add(ins);
                }

            }
            //取材图像
            if (Directory.Exists(ImgQcFolder + @"\" + study_no) == true)
            {
                foreach (var file in Directory.GetFiles(ImgQcFolder + @"\" + study_no, "*.jpg"))
                {
                    ImageListViewItem ins = new ImageListViewItem();
                    ins.Tag = file;
                    if (PIS_Sys.Properties.Settings.Default.EditPicName)
                    {
                        string filename1 = file.Substring(file.LastIndexOf(@"\") + 1);
                        DBHelper.BLL.multi_media_info insInfo = new DBHelper.BLL.multi_media_info();
                        string result = insInfo.GetMemo_note(study_no, filename1, 1);
                        if (!result.Equals(""))
                        {
                            ins.Text = result;
                        }
                        else
                        {
                            ins.Text = "QC_" + imageListView1.Items.Count.ToString();
                        }
                    }
                    else
                    {
                        ins.Text = "QC_" + imageListView1.Items.Count.ToString();
                    }
                    ins.FileName = file;
                    imageListView1.Items.Add(ins);
                }
            }
            imageListView1.ResumeLayout();

            //音频
            string OutputFolder = Path.Combine(Program.APPdirPath, @"Pis_Audio\" + study_no);
            if (Directory.Exists(OutputFolder) == true)
            {

                foreach (var file in Directory.GetFiles(OutputFolder, "*.wav"))
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = "录音";
                    int index = file.LastIndexOf(@"\");
                    string filename = file.Substring(index + 1);
                    lvi.SubItems.Add(filename);
                    string filepath = file.Substring(0, index);
                    lvi.SubItems.Add(filepath);
                    this.listViewEx1.Items.Add(lvi);

                }
            }
            //视频
            OutputFolder = Path.Combine(Program.APPdirPath, @"Pis_Video\" + study_no);
            if (Directory.Exists(OutputFolder) == true)
            {

                foreach (var file in Directory.GetFiles(OutputFolder, "*.avi"))
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = "视频";
                    int index = file.LastIndexOf(@"\");
                    string filename = file.Substring(index + 1);
                    lvi.SubItems.Add(filename);
                    string filepath = file.Substring(0, index);
                    lvi.SubItems.Add(filepath);
                    this.listViewEx1.Items.Add(lvi);

                }
            }
            //报告时视频
            OutputFolder = Path.Combine(Program.APPdirPath, @"Pis_Cap_Video\" + study_no);
            if (Directory.Exists(OutputFolder))
            {
                foreach (var file in Directory.GetFiles(OutputFolder, "*.avi"))
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = "视频";
                    int index = file.LastIndexOf(@"\");
                    string filename = file.Substring(index + 1);
                    lvi.SubItems.Add(filename);
                    string filepath = file.Substring(0, index);
                    lvi.SubItems.Add(filepath);
                    this.listViewEx1.Items.Add(lvi);

                }
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            imageListView1.ScrollBars = !imageListView1.ScrollBars;
        }

        private void x96ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageListView1.ThumbnailSize = new Size(thumsWidth, thumsHeight);
        }

        private void x120ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageListView1.ThumbnailSize = new Size(thumsWidth * 2, thumsHeight * 2);
        }

        private void x200ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageListView1.ThumbnailSize = new Size(thumsWidth * 3, thumsHeight * 3);
        }

        private void 重置缩略图大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSetThums ins = new FrmSetThums();
            ins.TopMost = true;
            ins.Owner = this;
            if (ins.ShowDialog() == DialogResult.OK)
            {
                thumsWidth = ins.ThWidth;
                thumsHeight = ins.ThHeigth;
                imageListView1.ThumbnailSize = new Size(thumsWidth, thumsHeight);
            }
        }

        private void clearThumbsToolStripButton_Click(object sender, EventArgs e)
        {
            imageListView1.ClearThumbnailCache();
        }

        private void paneToolStripButton_Click(object sender, EventArgs e)
        {
            imageListView1.View = Manina.Windows.Forms.View.Pane;

        }

        private void galleryToolStripButton_Click(object sender, EventArgs e)
        {
            imageListView1.View = Manina.Windows.Forms.View.Gallery;
        }

        private void thumbnailsToolStripButton_Click(object sender, EventArgs e)
        {
            imageListView1.View = Manina.Windows.Forms.View.Thumbnails;
        }

        private void colorToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PropertyInfo field = ((ColorComboBoxItem)colorToolStripComboBox.SelectedItem).Field;
            ImageListViewColor color = (ImageListViewColor)field.GetValue(null, null);
            imageListView1.Colors = color;
        }
        private struct ColorComboBoxItem
        {
            public string Name;
            public PropertyInfo Field;

            public override string ToString()
            {
                return Name;
            }

            public ColorComboBoxItem(PropertyInfo field)
            {
                Name = field.Name;
                Field = field;
            }
        }
        //播放
        private void listViewEx1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this.listViewEx1.SelectedIndices.Count > 0)
                {
                    if (listViewEx1.SelectedIndices != null && listViewEx1.SelectedIndices.Count > 0)
                    {
                        ListView.SelectedIndexCollection c = listViewEx1.SelectedIndices;
                        string filename = listViewEx1.Items[c[0]].SubItems[1].Text;// 表示选中行的第二列
                        string filepath = listViewEx1.Items[c[0]].SubItems[2].Text;// 表示选中行的第二列
                        string filePathstr = Path.Combine(filepath, filename);
                        if (System.IO.File.Exists(filePathstr))
                        {
                            Process.Start(filePathstr);
                        }
                        else
                        {
                            Frm_TJInfo("提示", "文件不存在！");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Frm_TJInfo("播放错误", ex.ToString());
            }
        }
        //提示窗体
        public void Frm_TJInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(this);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = true;
            Frm_AlertIns.AutoCloseTimeOut = 3;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }

    }
}
