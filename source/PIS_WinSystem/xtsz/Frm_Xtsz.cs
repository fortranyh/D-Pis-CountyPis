using AForge.Video.DirectShow;
using DevComponents.DotNetBar;
using DVPCameraType;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.xtsz
{
    public partial class Frm_Xtsz : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Xtsz()
        {
            InitializeComponent();
            this.CenterToScreen();
        }


        //标本接收模块设置应用
        private void buttonX1_Click(object sender, EventArgs e)
        {
            PIS_Sys.Properties.Settings.Default.Bb_Reg_Curor = Convert.ToInt32(comboBox1.SelectedIndex);
            PIS_Sys.Properties.Settings.Default.djBarcodePrinter = comboBox2.Text;
            PIS_Sys.Properties.Settings.Default.djBarcodePrintNum = Convert.ToInt32(numericUpDown1.Value);
            PIS_Sys.Properties.Settings.Default.Refresh_Date_FW = comboBox3.Text.Trim();
            PIS_Sys.Properties.Settings.Default.djBarcodePrint = switchButton2.Value;
            PIS_Sys.Properties.Settings.Default.RecordBBFlag = switchButton6.Value;
            PIS_Sys.Properties.Settings.Default.DjType = switchButton9.Value;
            PIS_Sys.Properties.Settings.Default.DjNewForm = switchButton10.Value;
            PIS_Sys.Properties.Settings.Default.BlhCreateFlag = switchButton13.Value;
            PIS_Sys.Properties.Settings.Default.His_PatInfo_Flag = switchButton19.Value;

            Properties.Settings.Default.Save();
            if (PIS_Sys.Properties.Settings.Default.His_PatInfo_Flag)
            {
                buttonX8.Visible = true;
            }
            else
            {
                buttonX8.Visible = false;
            }
            //登记提示合并
            UpdateAppConfig("DjTshb", (switchButton15.Value == true ? "1" : "0"));
            Frm_TJInfo("提示", "保存成功！");
        }

        private void Frm_Xtsz_Load(object sender, EventArgs e)
        {
            //包埋盒打号机和玻片打号机设备设置
            string SMFDEVICEFlag = ConfigurationManager.AppSettings["SMFDEVICE"] ?? "0";
            if (SMFDEVICEFlag.Equals("0"))
            {
                buttonX14.Visible = false;
            }
            //HIS接口提取病人基本信息
            switchButton19.Value = PIS_Sys.Properties.Settings.Default.His_PatInfo_Flag;
            if (PIS_Sys.Properties.Settings.Default.His_PatInfo_Flag)
            {
                buttonX8.Visible = true;
            }
            else
            {
                buttonX8.Visible = false;
            }
            this.comboBox1.SelectedIndex = PIS_Sys.Properties.Settings.Default.Bb_Reg_Curor;
            comboBox3.Text = PIS_Sys.Properties.Settings.Default.Refresh_Date_FW;
            comboBox4.Text = PIS_Sys.Properties.Settings.Default.Refresh_Date_QC;
            comboBox5.Text = PIS_Sys.Properties.Settings.Default.Refresh_Date_ZP;
            comboBox7.Text = PIS_Sys.Properties.Settings.Default.Refresh_Date_BG;
            switchButton16.Value = PIS_Sys.Properties.Settings.Default.zkapp_flag;
            switchButton18.Value = PIS_Sys.Properties.Settings.Default.QcBg_flag;
            //数码相机模式
            switchButton17.Value = PIS_Sys.Properties.Settings.Default.Open_Smxj;
            //报告是否主动开启摄像头
            switchButton1.Value = PIS_Sys.Properties.Settings.Default.Open_BG_SXT;
            //大体采集是否主动打开摄像头
            switchButton4.Value = PIS_Sys.Properties.Settings.Default.Open_DT_SXT;
            //报告界面是否导入大体图像
            switchButton5.Value = PIS_Sys.Properties.Settings.Default.Load_DT_IMG;
            //标本登记界面是否必须输入标本信息
            switchButton6.Value = PIS_Sys.Properties.Settings.Default.RecordBBFlag;
            //是否编辑图片名称
            switchButton7.Value = PIS_Sys.Properties.Settings.Default.EditPicName;
            //登记是否区分类别  是否开启新窗体
            switchButton9.Value = PIS_Sys.Properties.Settings.Default.DjType;
            switchButton10.Value = PIS_Sys.Properties.Settings.Default.DjNewForm;
            //加载系统主题
            string[] _styles = Enum.GetNames(typeof(eStyle));
            comboBoxEx1.Items.AddRange(_styles);
            eStyle style;
            if (Enum.TryParse<eStyle>(PIS_Sys.Properties.Settings.Default.SystemUI, out style))
            {
                comboBoxEx1.Text = style.ToString();
            }
            //数字摄像头类别配置(0通用1官方) 
            cmbUsbType.SelectedIndex = Program.UsbCameraType;
            foreach (FilterInfo device in videoDevices)
            {
                if (device.Name.Equals(PIS_Sys.Properties.Settings.Default.DTDevice))
                {
                    comboBox6.Text = PIS_Sys.Properties.Settings.Default.DTDevice;
                }
                if (device.Name.Equals(PIS_Sys.Properties.Settings.Default.BGDevice))
                {
                    comboBox9.Text = PIS_Sys.Properties.Settings.Default.BGDevice;
                }
            }

            //打印机设置
            foreach (String iprt in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                comboBox10.Items.Add(iprt);
                comboBox11.Items.Add(iprt);
                comboBox2.Items.Add(iprt);
                comboBox12.Items.Add(iprt);
                comboBox13.Items.Add(iprt);
            }



            if (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count > 0)
            {

                //报告打印机
                if (comboBox10.Items.Contains(PIS_Sys.Properties.Settings.Default.ReportPrinter))
                {
                    comboBox10.Text = PIS_Sys.Properties.Settings.Default.ReportPrinter;
                }
                else
                {
                    comboBox10.SelectedIndex = 0;
                }
                //登记条码
                if (comboBox2.Items.Contains(PIS_Sys.Properties.Settings.Default.djBarcodePrinter))
                {
                    comboBox2.Text = PIS_Sys.Properties.Settings.Default.djBarcodePrinter;
                }
                else
                {
                    comboBox2.SelectedIndex = 0;
                }
                //制片条码
                if (comboBox11.Items.Contains(PIS_Sys.Properties.Settings.Default.zpBarcodePrinter))
                {
                    comboBox11.Text = PIS_Sys.Properties.Settings.Default.zpBarcodePrinter;
                }
                else
                {
                    comboBox11.SelectedIndex = 0;
                }
                //取材待制片任务表打印机
                if (comboBox12.Items.Contains(PIS_Sys.Properties.Settings.Default.qcListPrinter))
                {
                    comboBox12.Text = PIS_Sys.Properties.Settings.Default.qcListPrinter;
                }
                else
                {
                    comboBox12.SelectedIndex = 0;
                }
                //特检条码打印机
                if (comboBox13.Items.Contains(PIS_Sys.Properties.Settings.Default.tjBarcodePrinter))
                {
                    comboBox13.Text = PIS_Sys.Properties.Settings.Default.tjBarcodePrinter;
                }
                else
                {
                    comboBox13.SelectedIndex = 0;
                }

                numericUpDown2.Value = PIS_Sys.Properties.Settings.Default.ReportPrintNum;
                numericUpDown1.Value = PIS_Sys.Properties.Settings.Default.djBarcodePrintNum;
                numericUpDown3.Value = PIS_Sys.Properties.Settings.Default.zpBarcodePrintNum;
                numericUpDown4.Value = PIS_Sys.Properties.Settings.Default.tjBarcodePrintNum;
                //登记打印条码开关
                switchButton2.Value = PIS_Sys.Properties.Settings.Default.djBarcodePrint;
                //制片打印条码开关
                switchButton3.Value = PIS_Sys.Properties.Settings.Default.zpBarcodePrintSL;

                if (ConfigurationManager.AppSettings["DjTshb"].Equals("1"))
                {
                    switchButton15.Value = true;
                }
                else
                {
                    switchButton15.Value = false;
                }
            }
            else
            {
                Frm_TJInfo("提示", "没有检测到安装的打印机！\n请检测安装。");
            }
            //取材是否自增
            switchButton8.Value = (ConfigurationManager.AppSettings["QchmZj"] == "1" ? true : false);
            //取材信息自动添加一条   取材保存自动核对
            switchButton11.Value = (ConfigurationManager.AppSettings["QcInfoAutoZj"] == "1" ? true : false);
            switchButton12.Value = (ConfigurationManager.AppSettings["QcSaveAutoHd"] == "1" ? true : false);
            //取材是否自动保存
            switchButton14.Value = (ConfigurationManager.AppSettings["QcSaveAuto"] == "1" ? true : false);
            //病理号是否自动生成
            switchButton13.Value = PIS_Sys.Properties.Settings.Default.BlhCreateFlag;
            //读取数据库中颜色方案
            DBHelper.BLL.exam_status insSta = new DBHelper.BLL.exam_status();
            DataTable dt = insSta.GetDsExam_status();
            if (dt != null && dt.Rows.Count == 11)
            {
                PictureBox1.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[0]["status_color"].ToString()));
                PictureBox2.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[1]["status_color"].ToString()));
                PictureBox3.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[2]["status_color"].ToString()));
                PictureBox4.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[3]["status_color"].ToString()));
                PictureBox5.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[4]["status_color"].ToString()));
                PictureBox6.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[5]["status_color"].ToString()));
                pictureBox7.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[6]["status_color"].ToString()));
                pictureBox8.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[7]["status_color"].ToString()));
                pictureBox9.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[8]["status_color"].ToString()));
                pictureBox10.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[9]["status_color"].ToString()));
                pictureBox11.BackColor = Color.FromArgb(Convert.ToInt32(dt.Rows[10]["status_color"].ToString()));
            }
            //颜色方案事件绑定
            foreach (object contorl1 in this.groupPanel4.Controls)
            {
                if (contorl1.GetType().ToString() == "System.Windows.Forms.PictureBox")
                {
                    ((PictureBox)contorl1).Click += new EventHandler(GetColor);
                }
            }

            //获取Ftp服务器配置信息
            DBHelper.BLL.ftp_set_info insFtp = new DBHelper.BLL.ftp_set_info();
            DataTable dtFtp = insFtp.GetData();
            if (dtFtp != null && dtFtp.Rows.Count == 1)
            {
                ftpIP.Value = dtFtp.Rows[0]["ftpip"].ToString();
                ftpPort.Value = Convert.ToInt32(dtFtp.Rows[0]["ftpport"]);
                ftpuser.Text = dtFtp.Rows[0]["ftpuser"].ToString();
                ftppwd.Text = dtFtp.Rows[0]["ftppwd"].ToString();
            }
            else
            {
                Program.FileLog.Error("服务器数据库中Ftp服务器配置信息为空，请配置！");
            }
            //条码上显示的科室名称
            textBoxX1.Text = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
            //超级权限控制
            DBHelper.BLL.sys_user ins = new DBHelper.BLL.sys_user();
            int gLflag = ins.GetUserGL(Program.User_Code);
            if (Program.System_Admin == true || gLflag == 1)
            {
                superTabItem2.Visible = true;
            }
            else
            {
                superTabItem2.Visible = false;
            }
            //当前检查流水号列表
            DBHelper.BLL.sys_sequence InsSeq = new DBHelper.BLL.sys_sequence();
            DataSet ds = InsSeq.GetDsSequence();
            if (ds != null)
            {
                this.dataGridView1.DataSource = ds.Tables[0];
            }

            //当前检查大类列表
            checkedListBox1.CheckOnClick = true;
            DBHelper.BLL.exam_type insbigtype = new DBHelper.BLL.exam_type();
            DataTable dtbigtype = insbigtype.GetAllDTExam_Big_Type();
            if (dtbigtype != null && dtbigtype.Rows.Count > 0)
            {
                checkedListBox1.DataSource = dtbigtype;
                checkedListBox1.DisplayMember = "big_type_name";
                checkedListBox1.ValueMember = "big_type_code";
            }
            //设置检查大类选中
            if (!Program.workstation_type_name.Equals(""))
            {
                string[] txts = Program.workstation_type_name.Split(',');
                for (int j = 0; j < txts.Length; j++)
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        if (checkedListBox1.GetItemText(checkedListBox1.Items[i]).Equals(txts[j]))
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }
            }

        }

        private void GetColor(System.Object sender, System.EventArgs e)
        {
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ((PictureBox)sender).BackColor = colorDialog1.Color;
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

        //加载视频设备
        FilterInfoCollection videoDevices = null;
        //加载摄像头列表
        private void LoadVidoSource()
        {
            comboBox6.Enabled = true;
            comboBox9.Enabled = true;
            //先清空
            comboBox6.Items.Clear();
            comboBox9.Items.Clear();
            //
            if (cmbUsbType.SelectedIndex == 0)
            {
                try
                {
                    videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                    if (videoDevices.Count == 0)
                        throw new ApplicationException();
                    foreach (FilterInfo device in videoDevices)
                    {
                        comboBox6.Items.Add(device.Name);
                        comboBox9.Items.Add(device.Name);
                    }
                }
                catch (ApplicationException)
                {
                    comboBox6.Items.Add("不存在视频设备");
                    comboBox6.Enabled = false;
                    comboBox9.Items.Add("不存在视频设备");
                    comboBox9.Enabled = false;
                }
                comboBox6.SelectedIndex = 0;
                comboBox9.SelectedIndex = 0;
            }
            else if (cmbUsbType.SelectedIndex == 1)
            {
                //加载大体摄像头
                try
                {
                    videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                    if (videoDevices.Count == 0)
                        throw new ApplicationException();
                    foreach (FilterInfo device in videoDevices)
                    {
                        comboBox6.Items.Add(device.Name);
                    }
                }
                catch (ApplicationException)
                {
                    comboBox6.Items.Add("不存在视频设备");
                    comboBox6.Enabled = false;
                }
                comboBox6.SelectedIndex = 0;
                //加载明美官方数字摄像头
                try
                {
                    dvpStatus status;
                    uint i, n = 0;
                    dvpCameraInfo dev_info = new dvpCameraInfo();

                    // 此时，n为成功枚举到的相机数量，将添加到下拉列表中，下拉列表中的内容为每个相机的FriendlyName
                    // 获得当前能连接的相机数量
                    status = DVPCamera.dvpRefresh(ref n);

                    if (status == dvpStatus.DVP_STATUS_OK)
                    {
                        for (i = 0; i < n; i++)
                        {
                            // 逐个枚举出每个相机的信息
                            status = DVPCamera.dvpEnum(i, ref dev_info);

                            if (status == dvpStatus.DVP_STATUS_OK)
                            {
                                // 界面使用的是UNICODE，枚举的设备信息为ANSI字符串，需要将ANSI转UNICODE
                                int item = comboBox9.Items.Add(dev_info.FriendlyName);
                                if (item == 0)
                                {
                                    comboBox9.SelectedIndex = item;
                                }
                            }
                        }
                    }
                    if (n == 0)
                    {
                        comboBox9.Items.Add("不存在视频设备");
                        comboBox9.Enabled = false;
                    }
                }
                catch (ApplicationException)
                {
                    comboBox9.Items.Add("不存在视频设备");
                    comboBox9.Enabled = false;
                }
                comboBox9.SelectedIndex = 0;
            }
        }
        //取材管理模块设置应用
        private void buttonX3_Click(object sender, EventArgs e)
        {
            PIS_Sys.Properties.Settings.Default.Refresh_Date_QC = comboBox4.Text.Trim();
            PIS_Sys.Properties.Settings.Default.DTDevice = comboBox6.Text.Trim();
            PIS_Sys.Properties.Settings.Default.qcListPrinter = comboBox12.Text.Trim();
            PIS_Sys.Properties.Settings.Default.Open_DT_SXT = switchButton4.Value;
            PIS_Sys.Properties.Settings.Default.Open_Smxj = switchButton17.Value;
            //材块号是否自增
            UpdateAppConfig("QchmZj", (switchButton8.Value == true ? "1" : "0"));
            UpdateAppConfig("QcInfoAutoZj", (switchButton11.Value == true ? "1" : "0"));
            UpdateAppConfig("QcSaveAutoHd", (switchButton12.Value == true ? "1" : "0"));
            UpdateAppConfig("QcSaveAuto", (switchButton14.Value == true ? "1" : "0"));
            Properties.Settings.Default.Save();
            Frm_TJInfo("提示", "保存成功！");
        }
        //制片模块
        private void buttonX4_Click(object sender, EventArgs e)
        {
            PIS_Sys.Properties.Settings.Default.zpBarcodePrinter = comboBox11.Text;
            PIS_Sys.Properties.Settings.Default.zpBarcodePrintNum = Convert.ToInt32(numericUpDown3.Value);
            PIS_Sys.Properties.Settings.Default.Refresh_Date_ZP = comboBox5.Text.Trim();
            PIS_Sys.Properties.Settings.Default.zpBarcodePrintSL = switchButton3.Value;
            Properties.Settings.Default.Save();
            Frm_TJInfo("提示", "保存成功！");
        }
        //系统主题
        private void buttonX5_Click(object sender, EventArgs e)
        {
            //更新颜色方案
            DBHelper.BLL.exam_status ins = new DBHelper.BLL.exam_status();
            ins.UpdateColor("10", PictureBox1.BackColor.ToArgb().ToString());
            ins.UpdateColor("15", PictureBox2.BackColor.ToArgb().ToString());
            ins.UpdateColor("20", PictureBox3.BackColor.ToArgb().ToString());
            ins.UpdateColor("25", PictureBox4.BackColor.ToArgb().ToString());
            ins.UpdateColor("27", PictureBox5.BackColor.ToArgb().ToString());
            ins.UpdateColor("30", PictureBox6.BackColor.ToArgb().ToString());
            ins.UpdateColor("36", pictureBox7.BackColor.ToArgb().ToString());
            ins.UpdateColor("40", pictureBox8.BackColor.ToArgb().ToString());
            ins.UpdateColor("50", pictureBox9.BackColor.ToArgb().ToString());
            ins.UpdateColor("55", pictureBox10.BackColor.ToArgb().ToString());
            ins.UpdateColor("60", pictureBox11.BackColor.ToArgb().ToString());
            PIS_Sys.Properties.Settings.Default.SystemUI = comboBoxEx1.Text;
            Properties.Settings.Default.Save();
            Frm_TJInfo("提示", "保存成功！");
        }
        ///<summary>  
        ///在＊.exe.config文件中appSettings配置节增加一对键、值对  
        ///</summary>  
        ///<param name="newKey"></param>  
        ///<param name="newValue"></param>  
        public static void UpdateAppConfig(string newKey, string newValue)
        {
            bool isModified = false;
            foreach (string key in ConfigurationManager.AppSettings)
            {
                if (key == newKey)
                {
                    isModified = true;
                }
            }

            Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (isModified)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            config.AppSettings.Settings.Add(newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        //主题切换
        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            eStyle style;
            if (!Enum.TryParse<eStyle>(comboBoxEx1.Text, out style))
            { return; }
            Program.frmMainins.styleManagerUI.ManagerStyle = style;

        }
        //报告模块
        private void buttonX6_Click(object sender, EventArgs e)
        {
            PIS_Sys.Properties.Settings.Default.Refresh_Date_BG = comboBox7.Text.Trim();
            PIS_Sys.Properties.Settings.Default.BGDevice = comboBox9.Text.Trim();
            //数字摄像头类别配置(0通用1官方) 
            UpdateAppConfig("UsbCameraType", cmbUsbType.SelectedIndex.ToString());
            Program.UsbCameraType = cmbUsbType.SelectedIndex;
            PIS_Sys.Properties.Settings.Default.Open_BG_SXT = switchButton1.Value;
            PIS_Sys.Properties.Settings.Default.ReportPrinter = comboBox10.Text;
            PIS_Sys.Properties.Settings.Default.ReportPrintNum = Convert.ToInt32(numericUpDown2.Value);
            PIS_Sys.Properties.Settings.Default.Load_DT_IMG = switchButton5.Value;
            PIS_Sys.Properties.Settings.Default.EditPicName = switchButton7.Value;
            PIS_Sys.Properties.Settings.Default.zkapp_flag = switchButton16.Value;
            PIS_Sys.Properties.Settings.Default.QcBg_flag = switchButton18.Value;
            Properties.Settings.Default.Save();
            Frm_TJInfo("提示", "保存成功！");
        }
        //特检模块
        private void buttonX7_Click(object sender, EventArgs e)
        {

            PIS_Sys.Properties.Settings.Default.tjBarcodePrinter = comboBox13.Text;
            PIS_Sys.Properties.Settings.Default.tjBarcodePrintNum = Convert.ToInt32(numericUpDown4.Value);
            PIS_Sys.Properties.Settings.Default.Bp_Ksmc = textBoxX1.Text.Trim();
            Properties.Settings.Default.Save();
            Frm_TJInfo("提示", "保存成功！");
        }
        //ftp图像服务器设置
        private void buttonX2_Click(object sender, EventArgs e)
        {
            DBHelper.BLL.ftp_set_info insFtp = new DBHelper.BLL.ftp_set_info();
            insFtp.UpdateFtp(ftpuser.Text.Trim(), ftppwd.Text.Trim(), this.ftpIP.Value, ftpPort.Value);
            //获取Ftp服务器配置信息
            DataTable dtFtp = insFtp.GetData();
            if (dtFtp != null && dtFtp.Rows.Count == 1)
            {
                Program.FtpIP = dtFtp.Rows[0]["ftpip"].ToString();
                Program.FtpPort = Convert.ToInt32(dtFtp.Rows[0]["ftpport"]);
                Program.FtpUser = dtFtp.Rows[0]["ftpuser"].ToString();
                Program.FtpPwd = dtFtp.Rows[0]["ftppwd"].ToString();
                Frm_TJInfo("提示", "设置成功！");
            }
            else
            {
                Program.FileLog.Error("服务器数据库中Ftp服务器配置信息为空，请配置！");
            }
        }
        //诊断编码 临床符合字典维护
        private void buttonX9_Click(object sender, EventArgs e)
        {
            Frm_bgDicWh Frm_Ins = new Frm_bgDicWh();
            Frm_Ins.Owner = this;
            Frm_Ins.BringToFront();
            Frm_Ins.ShowDialog();
        }



        //大体模板导入
        private void buttonX11_Click(object sender, EventArgs e)
        {
            Frm_TempletImport frmbgtypeIns = new Frm_TempletImport();
            frmbgtypeIns.Owner = this;
            frmbgtypeIns.BringToFront();
            frmbgtypeIns.TempletType = 0;
            frmbgtypeIns.ShowDialog();
        }

        //报告模板导入
        private void buttonX12_Click(object sender, EventArgs e)
        {
            Frm_BgTempletImport frmbgtypeIns = new Frm_BgTempletImport();
            frmbgtypeIns.Owner = this;
            frmbgtypeIns.BringToFront();
            frmbgtypeIns.TempletType = 1;
            frmbgtypeIns.ShowDialog();
        }



        private void buttonX14_Click(object sender, EventArgs e)
        {
            Frm_smfjk Frm_smfjkIns = new Frm_smfjk();
            Frm_smfjkIns.Owner = this;
            Frm_smfjkIns.BringToFront();
            Frm_smfjkIns.ShowDialog();
        }

        private void buttonX15_Click(object sender, EventArgs e)
        {
            string checkedValue = string.Empty;
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (this.checkedListBox1.GetItemChecked(i))
                {
                    this.checkedListBox1.SetSelected(i, true);
                    checkedValue += (String.IsNullOrEmpty(checkedValue) ? "" : ",") + this.checkedListBox1.SelectedValue.ToString();
                }
            }
            //工作站类型
            UpdateAppConfig("w_big_type", checkedValue);
            //工作站类型名称
            string checkedText = string.Empty;
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (this.checkedListBox1.GetItemChecked(i))
                {
                    this.checkedListBox1.SetSelected(i, true);
                    checkedText += (String.IsNullOrEmpty(checkedText) ? "" : ",") + this.checkedListBox1.GetItemText(this.checkedListBox1.Items[i]);
                }
            }
            UpdateAppConfig("w_big_type_name", checkedText);
            //提取工作站类型
            Program.workstation_type = ConfigurationManager.AppSettings["w_big_type"];
            Program.workstation_type_name = ConfigurationManager.AppSettings["w_big_type_name"];
            //数据库查询用
            string[] strs = checkedValue.Split(',');
            string checkedValueDB = "";
            for (int i = 0; i < strs.Length; i++)
            {
                if (checkedValueDB == "")
                { checkedValueDB += "'" + strs[i] + "'"; }
                else
                {
                    checkedValueDB += ",'" + strs[i] + "'";
                }
            }
            UpdateAppConfig("w_big_type_db", checkedValueDB);
            Program.workstation_type_db = ConfigurationManager.AppSettings["w_big_type_db"];
            //当前登录用户
            Program.frmMainins.Text = string.Format("病理信息管理系统—【{0}】<{1}:{2}>", Program.workstation_type_name, Program.Dept_Name, Program.User_Name);
            Frm_TJInfo("提示", "工作站类型设置成功！");
        }
        //保存检查流水号的更新
        private void buttonX16_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                DataTable dt = (dataGridView1.DataSource as DataTable);
                DBHelper.BLL.sys_sequence InsSeq = new DBHelper.BLL.sys_sequence();
                InsSeq.UpdateDsSequence(dt);
                dataGridView1.Update();
            }

        }
        //HIS接口测试
        private void buttonX8_Click(object sender, EventArgs e)
        {
            if (PIS_Sys.Properties.Settings.Default.His_PatInfo_Flag)
            {


            }
        }
        private void cmbUsbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //加载摄像头
            LoadVidoSource();
        }
    }
}
