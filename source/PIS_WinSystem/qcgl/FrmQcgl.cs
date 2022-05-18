using AForge.Video;
using AForge.Video.DirectShow;
using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar.SuperGrid.Style;
using Greenshot;
using Greenshot.Drawing;
using Greenshot.IniFile;
using Greenshot.Plugin;
using GreenshotPlugin.Core;
using Manina.Windows.Forms;
using NAudio.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace PIS_Sys.qcgl
{

    public partial class FrmQcgl : DevComponents.DotNetBar.Office2007Form
    {
        Dictionary<string, Color> exam_status_dic = new Dictionary<string, Color>();
        Dictionary<string, Color> exam_status_name_dic = new Dictionary<string, Color>();
        Dictionary<string, string> examStatus_dic = new Dictionary<string, string>();
        public FrmQcgl()
        {
            InitializeComponent();
            //热键操作
            HotkeyRepeatLimit = 1000;
            repeatLimitTimer = Stopwatch.StartNew();
            if (ConfigurationManager.AppSettings["QcSaveAutoHd"] == "1")
            {
                btn_ckhd.Visible = false;
            }
            //隐藏部位字典
            if (this.splitContainer2.Panel1Collapsed)
            {
                splitContainer2.Panel1Collapsed = false;
            }
            else
            {
                splitContainer2.Panel1Collapsed = true;
            }
        }
        private void FrmQcgl_HandleCreated(object sender, EventArgs e)
        {
            //注册热键
            hotkey_bbxx = new HotkeyInfo(Keys.F4);
            if (hotkey_bbxx.Status == HotkeyStatus.Registered)
            {
                UnregisterHotkey(hotkey_bbxx);
            }
            if (hotkey_bbxx.Status != HotkeyStatus.Registered && hotkey_bbxx.IsValidHotkey)
            {
                RegisterHotkey(hotkey_bbxx);

                if (hotkey_bbxx.Status == HotkeyStatus.Registered)
                {

                }
                else if (hotkey_bbxx.Status == HotkeyStatus.Failed)
                {

                }
            }
        }

        #region "热键操作"
        public int HotkeyRepeatLimit { get; set; }

        public delegate void HotkeyEventHandler(ushort id, Keys key, Modifiers modifier);

        public event HotkeyEventHandler HotkeyPress;

        private Stopwatch repeatLimitTimer;

        public static string GetUniqueID()
        {
            return Guid.NewGuid().ToString("N");
        }
        public void RegisterHotkey(HotkeyInfo hotkeyInfo)
        {
            if (hotkeyInfo != null && hotkeyInfo.Status != HotkeyStatus.Registered)
            {
                if (!hotkeyInfo.IsValidHotkey)
                {
                    hotkeyInfo.Status = HotkeyStatus.NotConfigured;
                    return;
                }

                if (hotkeyInfo.ID == 0)
                {
                    string uniqueID = GetUniqueID();
                    hotkeyInfo.ID = NativeMethods.GlobalAddAtom(uniqueID);

                    if (hotkeyInfo.ID == 0)
                    {

                        hotkeyInfo.Status = HotkeyStatus.Failed;
                        return;
                    }
                }

                if (!NativeMethods.RegisterHotKey(Handle, hotkeyInfo.ID, (uint)hotkeyInfo.ModifiersEnum, (uint)hotkeyInfo.KeyCode))
                {
                    NativeMethods.GlobalDeleteAtom(hotkeyInfo.ID);

                    hotkeyInfo.ID = 0;
                    hotkeyInfo.Status = HotkeyStatus.Failed;
                    return;
                }

                hotkeyInfo.Status = HotkeyStatus.Registered;
            }
        }

        public bool UnregisterHotkey(HotkeyInfo hotkeyInfo)
        {
            if (hotkeyInfo != null)
            {
                if (hotkeyInfo.ID > 0)
                {
                    bool result = NativeMethods.UnregisterHotKey(Handle, hotkeyInfo.ID);

                    if (result)
                    {
                        NativeMethods.GlobalDeleteAtom(hotkeyInfo.ID);
                        hotkeyInfo.ID = 0;
                        hotkeyInfo.Status = HotkeyStatus.NotConfigured;
                        return true;
                    }
                }

                hotkeyInfo.Status = HotkeyStatus.Failed;
            }

            return false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WindowsMessages.HOTKEY && CheckRepeatLimitTime())
            {
                ushort id = (ushort)m.WParam;
                Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
                Modifiers modifier = (Modifiers)((int)m.LParam & 0xFFFF);
                OnKeyPressed(id, key, modifier);
                return;
            }

            base.WndProc(ref m);
        }

        protected void OnKeyPressed(ushort id, Keys key, Modifiers modifier)
        {
            if (HotkeyPress != null)
            {
                HotkeyPress(id, key, modifier);
            }
        }

        private bool CheckRepeatLimitTime()
        {
            if (HotkeyRepeatLimit > 0)
            {
                if (repeatLimitTimer.ElapsedMilliseconds >= HotkeyRepeatLimit)
                {
                    repeatLimitTimer.Reset();
                    repeatLimitTimer.Start();
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        #endregion

        #region"快捷键操作"
        public bool IgnoreHotkeys = false;
        private void Frm_Dtcj_HotkeyPress(ushort id, Keys key, Modifiers modifier)
        {
            if (btn_PZ.Enabled == true)
            {
                if (!IgnoreHotkeys)
                {
                    if (this.Tag.Equals("show"))
                    {
                        switch (key)
                        {

                            case Keys.F4:
                                //添加标本信息快捷键

                                btn_PZ_Click(null, null);

                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            else
            {
                Frm_TJInfo("拍照提示", "请先打开摄像头开关！");
            }
        }


        //移除热键
        private void Frm_Dtcj_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (hotkey_bbxx != null)
            {
                if (hotkey_bbxx.Status == HotkeyStatus.Registered)
                {
                    UnregisterHotkey(hotkey_bbxx);
                }
            }
        }


        #endregion

        FilterInfoCollection videoDevices;
        private string device;
        // opened video source
        private IVideoSource videoSource = null;
        public string VideoFileName = "";
        private void LoadVidoSource()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();
                foreach (FilterInfo device in videoDevices)
                {
                    devicesCombo.Items.Add(device.Name);
                }
            }
            catch (ApplicationException)
            {
                devicesCombo.Items.Add("不存在视频设备");
                devicesCombo.Enabled = false;
            }

            devicesCombo.SelectedIndex = 0;

        }
        private void FrmQcgl_Activated(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            superTabControl2.SelectedTabIndex = 0;
            superTabControl1.SelectedTabIndex = 1;
            //开启焦点并刷新数据
            if (timer1.Enabled == false)
            {
                timer1.Enabled = true;
            }
            //硬件模式选择
            if (PIS_Sys.Properties.Settings.Default.Open_Smxj == false)
            {
                superTabItem3.Visible = true;
                buttonItem19.Visible = false;
                this.timer2.Enabled = false;
                if (PIS_Sys.Properties.Settings.Default.Open_DT_SXT == true)
                {
                    buttonItem15_Click(null, null);
                }
            }
            else
            {
                superTabItem3.Visible = false;
                buttonItem19.Visible = true;
            }
        }
        //材块核对
        private void btn_ckhd_Click(object sender, EventArgs e)
        {

            if (!txt_blh.Text.Trim().Equals(""))
            {
                Boolean Refresh_flag = false;


                //先执行一次保存
                if (dtQC != null && dtQC.Rows.Count > 0 && BBid != 0)
                {
                    if (!txt_ckzs.Text.Trim().Equals("") && !txt_lkzs.Text.Trim().Equals(""))
                    {
                        string result_str = "";
                        DBHelper.BLL.exam_specimens insDT = new DBHelper.BLL.exam_specimens();
                        if (insDT.UpdateSpecimensDTQC(BBid, txt_rysj.Text.Trim(), cmb_lrr.SelectedValue.ToString(), cmb_lrr.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), cmb_bbcl.Text.Trim(), txt_cfwz.Text.Trim(), Convert.ToInt16(txt_lkzs.Text.Trim()), Convert.ToInt16(txt_ckzs.Text.Trim()), ref result_str) == false)
                        {
                            Frm_TJInfo("大体描述保存失败！", result_str);
                            return;
                        }
                    }

                    DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                    string Str_Result = "";
                    if (ins.Process_draw_meterials(dtQC, txt_blh.Text.Trim(), ref Str_Result) == false)
                    {
                        Frm_TJInfo("取材信息保存失败！", Str_Result);
                        return;
                    }

                    //刷新下本次保存完毕的取材列表
                    RefreshQCList();

                }

                //检查是否状态小于25（已取材）
                DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                int status = insM.GetStudyExam_Status(txt_blh.Text.Trim());
                if (status < 25 && status >= 20)
                {
                    Refresh_flag = false;
                }
                else if (status >= 25)
                {
                    Refresh_flag = true;
                }
                //查询是否存在取材信息
                DBHelper.BLL.exam_draw_meterials insDr = new DBHelper.BLL.exam_draw_meterials();
                int count = insDr.GetMeterialsCount(txt_blh.Text.Trim());
                if (count > 0)
                {
                    FrmCkhd FrmCkhdIns = new FrmCkhd();
                    FrmCkhdIns.Owner = this;
                    FrmCkhdIns.Refresh_Flag = Refresh_flag;
                    FrmCkhdIns.BLH = txt_blh.Text.Trim();
                    FrmCkhdIns.sqd = txt_sqd.Text.Trim();
                    FrmCkhdIns.BringToFront();
                    FrmCkhdIns.ShowDialog();
                    //刷新列表
                    QueryBtn_Click(QueryBtn, null);
                }
                else
                {
                    Frm_TJInfo("当前不能执行核对操作", "请确认当前选择的病理号下面的标本已经取材且取材后执行了保存操作！");
                }
            }
            else
            {
                Frm_TJInfo("当前不能执行此操作", "请先选择一个要进行核对的病理号！");
            }
        }

        private void cmb_tj_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_tj.Focus();
        }
        //清除筛选条件
        private void buttonX3_Click(object sender, EventArgs e)
        {
            txt_tj.Text = "";
            txt_tj.Focus();
        }
        public void SetMdiForm(String caption, String title)
        {
            //然后分配给SuperTab控件，创建一个SuperTabItem并显示
            SuperTabItem tabItem = superTabControl1.CreateTab(caption);
            tabItem.Name = caption;
            tabItem.Text = title;
            Frm_Dtcj ins = new Frm_Dtcj();
            ins.FormBorderStyle = FormBorderStyle.None;
            ins.TopLevel = false;
            ins.Visible = true;
            ins.Dock = DockStyle.Fill;
            tabItem.Icon = ins.Icon;
            tabItem.AttachedControl.Controls.Add(ins);
            //superTabControl1.SelectedTab = tabItem;
        }
        DataTable dtQC = null;
        private void FrmQcgl_Load(object sender, EventArgs e)
        {
            //原始颜色
            CurBtnColor = buttonX13.TextColor;
            //电子病历接口
            if (Program.EmrUrlStr.Equals(""))
            {
                buttonX11.Visible = false;
            }
            //PACS接口
            if (Program.PACSUrlStr.Equals(""))
            {
                buttonX5.Visible = false;
            }
            //
            btn_closecj.Enabled = false;
            btn_PZ.Enabled = false;
            superTabControl2.AllowDrop = false;
            superTabControl2.ReorderTabsEnabled = false;
            superTabControl1.ReorderTabsEnabled = false;
            superTabControl1.AllowDrop = false;

            //创建取材内存表
            dtQC = new DataTable("QCXX");
            //取材表中数据库中的id字段值
            dtQC.Columns.Add("id", typeof(int));
            //标本表id 
            dtQC.Columns.Add("specimens_id", typeof(int));
            //任务来源
            dtQC.Columns.Add("work_source", typeof(string));
            //材块号
            dtQC.Columns.Add("meterial_no", typeof(string));
            //取材部位
            dtQC.Columns.Add("parts", typeof(string));
            //组织数
            dtQC.Columns.Add("group_num", typeof(string));
            //组织单位
            dtQC.Columns.Add("group_unite", typeof(string));
            //备注
            dtQC.Columns.Add("memo_note", typeof(string));
            //取材医生姓名
            dtQC.Columns.Add("draw_doctor_name", typeof(string));
            //取材时间
            dtQC.Columns.Add("draw_datetime", typeof(string));
            //是否新加
            dtQC.Columns.Add("new_flag", typeof(int));
            //蜡块上脱水机时间
            dtQC.Columns.Add("lktsdt_flag", typeof(string));

            dtQC.DefaultView.Sort = " draw_datetime asc";
            superGridControl4.PrimaryGrid.DataSource = dtQC;
            //缩略图大小
            if (PIS_Sys.Properties.Settings.Default.DTthumsWidth == 0 || PIS_Sys.Properties.Settings.Default.DTthumsHeight == 0)
            {
                thumsFlag = true;
            }
            else
            {
                thumsFlag = false;
                imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth, PIS_Sys.Properties.Settings.Default.DTthumsHeight);
            }

            //加载检查状态
            DBHelper.BLL.exam_status exam_status_ins = new DBHelper.BLL.exam_status();
            List<DBHelper.Model.exam_status> lst = exam_status_ins.GetQCModelExam_Status();
            if (lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    DevComponents.Editors.ComboItem cmbitem = new DevComponents.Editors.ComboItem();
                    cmbitem.Text = lst[i].status_name;
                    cmbitem.Value = lst[i].status_code;
                    //添加颜色表
                    exam_status_dic[lst[i].status_code] = Color.FromArgb(Convert.ToInt32(lst[i].status_color));
                    exam_status_name_dic[lst[i].status_name] = Color.FromArgb(Convert.ToInt32(lst[i].status_color));
                    examStatus_dic.Add(lst[i].status_code, lst[i].status_name);
                    cmb_ExamStatus.Items.Add(cmbitem);
                }
                lst.Clear();
            }

            //绑定取材医生
            DBHelper.BLL.doctor_dict user_ins = new DBHelper.BLL.doctor_dict();
            DataSet ds = user_ins.GetDsExam_User(Program.Dept_Code);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                cmb_qcys.DataSource = ds.Tables[0];
                cmb_qcys.DisplayMember = "user_name";
                cmb_qcys.ValueMember = "user_code";
                cmb_qcys.Text = Program.User_Name;
            }
            else
            {
                cmb_qcys.DataSource = null;
            }
            //
            DBHelper.BLL.sys_user doctor_dict_ins = new DBHelper.BLL.sys_user();
            DataTable dt1 = doctor_dict_ins.SysAllUsers(Program.Dept_Code);
            if (dt1 != null && dt1.Rows.Count > 0)
            {

                cmb_lrr.DataSource = dt1;
                cmb_lrr.DisplayMember = "user_name";
                cmb_lrr.ValueMember = "user_code";
            }
            else
            {
                cmb_lrr.DataSource = null;
            }
            DataTable dt2 = doctor_dict_ins.SysAllUsers(Program.Dept_Code);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                cmb_bbclr.DataSource = dt2;
                cmb_bbclr.DisplayMember = "user_name";
                cmb_bbclr.ValueMember = "user_code";
            }
            else
            {
                cmb_bbclr.DataSource = null;
            }
            cmb_bbclr.Text = Program.User_Name;
            cmb_qcys.Text = Program.User_Name;
            cmb_lrr.Text = Program.User_Name;

            //绑定组织单位字典
            DBHelper.BLL.group_dict insGrop = new DBHelper.BLL.group_dict();
            DataTable dtGrop = insGrop.Get_group_dict();
            if (dtGrop != null && dtGrop.Rows.Count > 0)
            {
                cmb_zzdw.DataSource = dtGrop;
                cmb_zzdw.DisplayMember = "unite_name";
                cmb_zzdw.ValueMember = "unite_code";
            }
            else
            {
                cmb_zzdw.DataSource = null;
            }
            //Getunite_name
            List<string> lst_Unite = insGrop.Getunite_name();
            if (lst_Unite.Count > 0)
            {
                superGridControl4.PrimaryGrid.Columns["group_unite"].EditorType = typeof(FragrantComboBox);
                superGridControl4.PrimaryGrid.Columns["group_unite"].EditorParams = new object[] { lst_Unite.ToArray() };
                lst_Unite.Clear();
            }
            //
            cmb_tj.SelectedIndex = 0;
            cmb_ExamStatus.SelectedIndex = 0;
            chK_status.Checked = true;

            //蜡块脱水
            List<string> lst_lkts = new List<string>();
            lst_lkts.Add("快");
            lst_lkts.Add("慢");
            superGridControl4.PrimaryGrid.Columns["lktsdt_flag"].EditorType = typeof(FragrantComboBox);
            superGridControl4.PrimaryGrid.Columns["lktsdt_flag"].EditorParams = new object[] { lst_lkts };

            //绑定事件处理
            for (int i = 0; i < this.QueryBtn.SubItems.Count; i++)
            {
                ButtonItem BtnItem = ((ButtonItem)this.QueryBtn.SubItems[i]);
                BtnItem.Click += new System.EventHandler(Chk_BtnRq);
                BtnItem.Checked = false;
                if (BtnItem.Text.Equals(PIS_Sys.Properties.Settings.Default.Refresh_Date_QC))
                {
                    BtnItem.Checked = true;
                }
            }
            //列表样式
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel2 = superGridControl1.PrimaryGrid;
            panel2.Name = "exam_master_view";
            panel2.MinRowHeight = 30;
            //锁定
            panel2.FrozenColumnCount = 1;
            panel2.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ReadOnly = true;
                panel2.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //
            superGridControl3.PrimaryGrid.ShowRowHeaders = true;
            GridPanel panel3 = superGridControl3.PrimaryGrid;
            panel3.MinRowHeight = 30;
            panel3.AutoGenerateColumns = true;
            for (int i = 0; i < panel3.Columns.Count; i++)
            {
                panel3.Columns[i].ReadOnly = true;
            }
            GridPanel panel5 = superGridControl5.PrimaryGrid;
            panel5.MinRowHeight = 30;
            panel5.AutoGenerateColumns = true;
            for (int i = 0; i < panel5.Columns.Count; i++)
            {
                panel5.Columns[i].ReadOnly = true;
            }
            //任务来源字典
            DBHelper.BLL.work_source_dict w_ins = new DBHelper.BLL.work_source_dict();
            DataTable dtw = w_ins.Get_work_source_dict();
            if (dtw != null && dtw.Rows.Count > 0)
            {
                cmb_rwly.DataSource = dtw;
                cmb_rwly.DisplayMember = "work_name";
                cmb_rwly.ValueMember = "work_code";
            }
            else
            {
                cmb_rwly.DataSource = null;
            }
            //标本处理字典
            DBHelper.BLL.specimen_process_dict sp_ins = new DBHelper.BLL.specimen_process_dict();
            DataTable dt4 = sp_ins.Get_specimen_process_dict();
            if (dt4 != null && dt4.Rows.Count > 0)
            {
                cmb_bbcl.DataSource = dt4;
                cmb_bbcl.DisplayMember = "process_name";
                cmb_bbcl.ValueMember = "process_code";
            }
            else
            {
                cmb_bbcl.DataSource = null;
            }
            //任务来源  
            List<string> lst_w_ins = w_ins.Getwork_source();
            if (lst_w_ins.Count > 0)
            {
                superGridControl4.PrimaryGrid.Columns["work_source"].EditorType = typeof(FragrantComboBox);
                superGridControl4.PrimaryGrid.Columns["work_source"].EditorParams = new object[] { lst_w_ins.ToArray() };
                lst_w_ins.Clear();
            }

            //初始化标本类型下拉框
            DBHelper.BLL.specimens_type specimens_type_ins = new DBHelper.BLL.specimens_type();
            List<string> lst_speci = specimens_type_ins.Getspecimens_type(1);
            if (lst_speci.Count > 0)
            {
                superGridControl2.PrimaryGrid.Columns["specimens_class"].EditorType = typeof(FragrantComboBox);
                superGridControl2.PrimaryGrid.Columns["specimens_class"].EditorParams = new object[] { lst_speci.ToArray() };
                lst_speci.Clear();
            }
            //初始化包埋和制片医师下拉框
            DBHelper.BLL.sys_user Sys_UserIns = new DBHelper.BLL.sys_user();
            List<string> lst_User = Sys_UserIns.GetUsersName();
            if (lst_User.Count > 0)
            {
                superGridControl4.PrimaryGrid.Columns["draw_doctor_name"].EditorType = typeof(FragrantComboBox);
                superGridControl4.PrimaryGrid.Columns["draw_doctor_name"].EditorParams = new object[] { lst_User.ToArray() };
                lst_User.Clear();
            }
            //高度自动适应
            superGridControl2.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl4.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl3.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl5.PrimaryGrid.DefaultRowHeight = 0;
            //初始化控件
            ClearData();
            //创建字典
            BuildPartsTree();
            BuildDTTree();
            superTabControl1.SelectedTabIndex = 1;
            //是否存在多媒体文件
            //1视频路径
            VideoFolder = Program.APPdirPath + @"\Pis_Video";
            if (Directory.Exists(VideoFolder) == false)
            {
                Directory.CreateDirectory(VideoFolder);
            }
            //2图像路径
            ImgFolder = Program.APPdirPath + @"\Pis_Image";
            if (Directory.Exists(ImgFolder) == false)
            {
                Directory.CreateDirectory(ImgFolder);
            }
            //3录音路径
            AudioFolder = Program.APPdirPath + @"\Pis_Audio";
            if (Directory.Exists(AudioFolder) == false)
            {
                Directory.CreateDirectory(AudioFolder);
            }
            //加载图像列表
            Type colorType = typeof(ImageListViewColor);
            int j = 0;
            foreach (PropertyInfo field in colorType.GetProperties(BindingFlags.Public | BindingFlags.Static))
            {
                colorToolStripComboBox.Items.Add(new ColorComboBoxItem(field));
                if (field.Name == "Default")
                    colorToolStripComboBox.SelectedIndex = j;
                j++;
            }
            //缩略图不显示滚动条
            imageListView1.ScrollBars = false;
            //
            ftpDownload1.Visible = false;
            Application.DoEvents();
            ftpDownload1.DownLoadComplete += new FtpDownloader.FtpDownload.DownLoadCompleteEventHandler(OnDownLoadComplete);
            ftpDownload1.DownLoadError += new FtpDownloader.FtpDownload.DownLoadErrorEventHandler(OnDownLoadError);
            //模板跳转
            this.txt_rysj.Enter += new EventHandler(txt_rysj_enter);
            //添加材块号字典
            DBHelper.BLL.exam_draw_meterials insDr = new DBHelper.BLL.exam_draw_meterials();
            DataTable dtDr = insDr.GetCkhDict();
            if (dtDr != null && dtDr.Rows.Count > 0)
            {
                for (int i = 0; i < dtDr.Rows.Count; i++)
                {
                    DevComponents.Editors.ComboItem cmbitem = new DevComponents.Editors.ComboItem();
                    cmbitem.Text = dtDr.Rows[i]["ckh"].ToString();
                    cmbitem.Value = dtDr.Rows[i]["value"].ToString();
                    cmb_ckh.Items.Add(cmbitem);
                }
                cmb_ckh.SelectedIndex = 0;
            }
            //动态生成检查部位右键菜单 contextMenuStrip4
            this.MenuItemClick += new MenuEventHandler(bbxx_MenuItemClick);
            CreatePopUpJcbwMenu();
            //
            LoadVidoSource();
        }
        private void bbxx_MenuItemClick(string BBBW)
        {
            if (!txt_blh.Text.Trim().Equals(""))
            {
                DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                Boolean ZxResult = ins.InsertSpecimensInfo(txt_sqd.Text.Trim(), BBBW, "", "", (superGridControl2.PrimaryGrid.Rows.Count + 1).ToString(), Program.User_Code, Program.User_Name);
                if (ZxResult)
                {
                    //查询标本信息
                    DataTable Spdt = ins.GetQCSpecimensInfo(txt_sqd.Text.Trim());
                    if (Spdt != null)
                    {
                        superGridControl2.PrimaryGrid.DataSource = Spdt;
                        //选中第一行
                        if (Spdt.Rows.Count > 0)
                        {
                            superGridControl2.PrimaryGrid.SetSelectedRows(0, 1, true);
                        }

                    }
                    else
                    {
                        superGridControl2.PrimaryGrid.DataSource = null;
                    }
                }
            }
            else
            {
                Frm_TJInfo("提示", "请选择一个病人后再添加！");
            }
        }
        //记录菜单编码和对应的菜单ＩＤ的字典泛型变量
        Dictionary<string, int> DictionaryValues = new Dictionary<string, int>();
        public delegate void MenuEventHandler(string ChildText);
        public event MenuEventHandler MenuItemClick;
        private void CreatePopUpJcbwMenu()
        {
            DBHelper.BLL.exam_parts_dict Exam_part_Ins = new DBHelper.BLL.exam_parts_dict();
            DataTable dt = Exam_part_Ins.GetDsExam_parts_dict();
            if (dt != null && dt.Rows.Count > 0)
            {
                int j = 0;
                int k = 0;
                int m = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //生成菜单项 id,part_name,parent_code,order_no
                    //是父菜单时
                    if (dt.Rows[i]["parent_code"].ToString().Equals("0"))
                    {
                        k = this.contextMenuStrip4.Items.Add(new System.Windows.Forms.ToolStripMenuItem());
                        string tmp = dt.Rows[i]["id"].ToString();
                        if (DictionaryValues.ContainsKey(tmp) == false)
                        {
                            DictionaryValues.Add(tmp, k);
                        }
                        this.contextMenuStrip4.Items[k].Text = dt.Rows[i]["part_name"].ToString();
                        //添加父菜单的事件
                        (this.contextMenuStrip4.Items[k] as System.Windows.Forms.ToolStripMenuItem).Click += new EventHandler(ActiveEvent);
                    }
                    else
                    {
                        //获取父菜单的索引
                        if (DictionaryValues.TryGetValue(dt.Rows[i]["parent_code"].ToString(), out j))
                        {
                            //添加子菜单到父菜单中
                            m = (this.contextMenuStrip4.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems.Add(new System.Windows.Forms.ToolStripMenuItem());
                            (this.contextMenuStrip4.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Text = dt.Rows[i]["part_name"].ToString();
                            //记录菜单编码和菜单编号
                            DictionaryValues.Add(dt.Rows[i]["id"].ToString(), m);
                            //添加子菜单的事件
                            (this.contextMenuStrip4.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Click += new EventHandler(ActiveEvent);
                        }
                        else
                        {
                            //如果当前子菜单的父菜单不存在，则创建父菜单后再创建子菜单
                            k = this.contextMenuStrip4.Items.Add(new System.Windows.Forms.ToolStripMenuItem());
                            string tmp = dt.Rows[i]["parent_code"].ToString();
                            if (DictionaryValues.ContainsKey(tmp) == false)
                            {
                                DictionaryValues.Add(tmp, k);
                            }
                            this.contextMenuStrip4.Items[k].Text = Exam_part_Ins.GetExam_parts_Name(tmp);
                            //添加子菜单到父菜单中
                            m = (this.contextMenuStrip4.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems.Add(new System.Windows.Forms.ToolStripMenuItem());
                            (this.contextMenuStrip4.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Text = dt.Rows[i]["part_name"].ToString();
                            //记录菜单编码和菜单编号
                            DictionaryValues.Add(dt.Rows[i]["id"].ToString(), m);
                            //添加子菜单的事件
                            (this.contextMenuStrip4.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Click += new EventHandler(ActiveEvent);
                        }
                    }
                }
                dt.Clear();
                dt = null;
            }
        }
        // 右键菜单的激发菜单子项的单击事件
        private void ActiveEvent(Object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripMenuItem MI = sender as System.Windows.Forms.ToolStripMenuItem;
            if (MenuItemClick != null && MI.DropDownItems.Count == 0)
            {
                MenuItemClick(MI.Text);
            }
        }
        //光标位置记录
        public void txt_rysj_enter(object sender, EventArgs e)
        {
            superTabControl2.SelectedTabIndex = 1;
            txt_rysj.Focus();
            if (!txt_qcbw.Text.Trim().Equals(""))
            {
                if (advTree2.Nodes[0].Nodes.Count > 0)
                {
                    if (advTree2.Nodes[0].Nodes[0].Nodes.Count > 0)
                    {
                        for (int i = 0; i < advTree2.Nodes[0].Nodes[0].Nodes.Count; i++)
                        {
                            advTree2.Nodes[0].Nodes[0].Nodes[i].CollapseAll();
                        }
                        for (int i = 0; i < advTree2.Nodes[0].Nodes[0].Nodes.Count; i++)
                        {
                            if (txt_qcbw.Text.Trim().Contains(advTree2.Nodes[0].Nodes[0].Nodes[i].Text.Trim()))
                            {
                                advTree2.Nodes[0].Nodes[0].Nodes[i].Expand();
                                advTree2.Nodes[0].Nodes[0].Nodes[i].EnsureVisible();
                                break;
                            }
                        }
                    }
                }
            }
        }
        //技术医嘱
        public void RefreshJsyz()
        {
            DBHelper.BLL.exam_jsyz ins = new DBHelper.BLL.exam_jsyz();
            DataTable dt = ins.GetQcJsyzData();
            if (dt != null && dt.Rows.Count > 0)
            {
                labelItem1.Text = dt.Rows.Count.ToString();
            }
            else
            {
                labelItem1.Text = "0";
            }
        }
        // 定义调用方法的委托
        delegate void FunDelegateStart();
        void OnDownLoadComplete()
        {
            if (InvokeRequired)
            {
                // 要调用的方法的委托
                FunDelegateStart funDelegate = new FunDelegateStart(ftpDownload1_DownLoadComplete);
                this.BeginInvoke(funDelegate);
            }
        }
        private void ftpDownload1_DownLoadComplete()
        {
            ftpDownload1.Visible = false;
            RefreshMultiList();
        }
        delegate void FunFtpError(string errorinfo);
        void OnDownLoadError(string errorinfo)
        {
            if (InvokeRequired)
            {
                // 要调用的方法的委托
                FunFtpError funDelegate = new FunFtpError(ftpDownload1_DownLoadError);
                this.BeginInvoke(funDelegate, new object[] { errorinfo });
            }
        }
        private void ftpDownload1_DownLoadError(string errorinfo)
        {
            Program.FileLog.Error(errorinfo);
            ftpDownload1.Visible = false;
            Application.DoEvents();
            ftpDownload1.SetOver();
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
        string VideoFolder;
        string ImgFolder;
        string AudioFolder;
        //创建部位字典
        private void BuildPartsTree()
        {
            DBHelper.BLL.exam_parts_dict ins = new DBHelper.BLL.exam_parts_dict();
            DataTable dt = ins.GetTreeExam_parts_dict();
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.DefaultView.Sort = " order_no asc";
                string fliter = "parent_code='0'";
                DataRow[] drArr = dt.Select(fliter);
                advTree1.BeginUpdate();
                foreach (DataRow dr in drArr)
                {
                    Node node = new Node();
                    node.Tag = dr["id"].ToString();
                    node.TagString = dr["id"].ToString();
                    node.Text = dr["part_name"].ToString();
                    node.ImageExpanded = global::PIS_Sys.Properties.Resources.NoteHS;
                    CreateTreeViewRecursive(node.Nodes, dt, dr["id"].ToString());
                    advTree1.Nodes[0].Nodes.Add(node);
                    if (node.Nodes.Count > 0)
                    {
                        node.ExpandVisibility = eNodeExpandVisibility.Visible;
                        node.Image = global::PIS_Sys.Properties.Resources.cc;
                    }
                    else
                    {
                        node.ExpandVisibility = eNodeExpandVisibility.Hidden;
                        node.Image = global::PIS_Sys.Properties.Resources.ShowRulelinesHS;
                    }
                    //移除已添加行，提高性能
                    dt.Rows.Remove(dr);
                }
                advTree1.EndUpdate();
                //添加单击事件处理程序
                this.advTree1.NodeClick += new TreeNodeMouseEventHandler(advTree1_NodeClick);
            }
        }
        //赋值选中的部位
        private void advTree1_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {
            if (btn_save.Enabled == false)
            {
                return;
            }
            if (e.Node.Nodes.Count == 0 && e.Node.TagString != "-1")
            {

                txt_qcbw.Text = e.Node.Text;
                txt_qcbw.Focus();
                txt_qcbw.SelectionStart = e.Node.Text.Length;

            }
        }
        //展示选中的大体描述
        private void advTree2_NodeClick(object sender, TreeNodeMouseEventArgs e)
        {

            if (e.Node.Nodes.Count == 0)
            {
                string id = Convert.ToString(e.Node.Tag);
                DBHelper.BLL.dtms_templet ins = new DBHelper.BLL.dtms_templet();
                string content = ins.GetContent(id, Program.User_Code).Trim();
                richTextBoxEx2.Focus();
                richTextBoxEx2.Text = content;
                richTextBoxEx2.SelectionStart = content.Length;
            }
        }
        private void advTree2_NodeDoubleClick(object sender, TreeNodeMouseEventArgs e)
        {
            if (btn_save.Enabled == false)
            {
                return;
            }
            if (e.Node.Nodes.Count == 0)
            {
                string id = Convert.ToString(e.Node.Tag);
                DBHelper.BLL.dtms_templet ins = new DBHelper.BLL.dtms_templet();
                string content = ins.GetContent(id, Program.User_Code).Trim();
                if (!content.Equals("") && !txt_rysj.Text.Trim().Equals(""))
                {
                    eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                    TaskDialog.EnableGlass = false;

                    if (TaskDialog.Show("询问", "确认", "确定替换现有的大体描述信息么？", Curbutton) == eTaskDialogResult.Ok)
                    {
                        txt_rysj.Focus();
                        txt_rysj.Text = content;
                        this.txt_rysj.SelectionStart = content.Length;
                    }
                }
                else
                {
                    if (!content.Equals(""))
                    {
                        txt_rysj.Focus();
                        txt_rysj.Text = content;
                        this.txt_rysj.SelectionStart = content.Length;
                    }
                }
            }
        }
        private void CreateTreeViewRecursive2(NodeCollection nodes, DataTable dataSource, string parentId)
        {
            dataSource.DefaultView.Sort = "  TreeLevel asc,autoid asc";
            string fliter = "parentid='" + parentId + "'";
            //查询子节点
            DataRow[] drArr = dataSource.Select(fliter);
            Node node;
            foreach (DataRow dr in drArr)
            {
                node = new Node();
                node.Tag = dr["id"].ToString();
                node.Text = dr["title"].ToString();
                node.ImageExpanded = global::PIS_Sys.Properties.Resources.NoteHS;
                nodes.Add(node);
                //递归创建子节点
                CreateTreeViewRecursive2(node.Nodes, dataSource, Convert.ToString(dr["id"]));

                if (node.Nodes.Count > 0)
                {
                    node.ExpandVisibility = eNodeExpandVisibility.Visible;
                    node.Image = global::PIS_Sys.Properties.Resources.cc;
                }
                else
                {
                    node.ExpandVisibility = eNodeExpandVisibility.Hidden;
                    node.Image = global::PIS_Sys.Properties.Resources.ShowRulelinesHS;
                }
                //移除已添加行，提高性能
                dataSource.Rows.Remove(dr);
            }
        }
        private void CreateTreeViewRecursive(NodeCollection nodes, DataTable dataSource, string parentId)
        {
            dataSource.DefaultView.Sort = " order_no asc";
            string fliter = "parent_code='" + parentId + "'";
            //查询子节点
            DataRow[] drArr = dataSource.Select(fliter);
            foreach (DataRow dr in drArr)
            {
                Node node = new Node();
                node.Tag = dr["id"].ToString();
                node.TagString = dr["id"].ToString();
                node.Text = dr["part_name"].ToString();
                node.Image = global::PIS_Sys.Properties.Resources.ShowRulelinesHS;
                nodes.Add(node);
                //移除已添加行，提高性能
                dataSource.Rows.Remove(dr);
            }
        }
        //创建大体描述模版
        private void BuildDTTree()
        {
            //移除单击事件处理程序
            this.advTree2.NodeClick -= new TreeNodeMouseEventHandler(advTree2_NodeClick);
            this.advTree2.NodeDoubleClick -= new TreeNodeMouseEventHandler(advTree2_NodeDoubleClick);
            string RootID;
            string strRootText;
            advTree2.Nodes[0].Nodes.Clear();
            DBHelper.BLL.dtms_templet ins = new DBHelper.BLL.dtms_templet();
            DataTable dt = ins.GetTreeDtms_Templet(Program.User_Code);
            if (dt != null && dt.Rows.Count == 0)
            {
                //插入根节点
                string uid = Helper.GetUidStr();
                ins.InsertDTtempletRoot(Program.User_Code, uid);
                dt = ins.GetTreeDtms_Templet(Program.User_Code);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                string fliter = "parentid='-1'";
                DataRow[] drArr = dt.Select(fliter);
                advTree2.BeginUpdate();
                int i = 0;
                foreach (DataRow dr in drArr)
                {
                    RootID = dr["id"].ToString();
                    strRootText = dr["title"].ToString();
                    Node node = new Node();
                    node.Tag = RootID;
                    node.Text = strRootText;
                    node.ImageExpanded = global::PIS_Sys.Properties.Resources.NoteHS;
                    advTree2.Nodes[0].Nodes.Add(node);

                    CreateTreeViewRecursive2(advTree2.Nodes[0].Nodes[i].Nodes, dt, RootID);

                    if (node.Nodes.Count > 0)
                    {
                        node.ExpandVisibility = eNodeExpandVisibility.Visible;
                        node.Image = global::PIS_Sys.Properties.Resources.cc;
                    }
                    else
                    {
                        node.ExpandVisibility = eNodeExpandVisibility.Hidden;
                        node.Image = global::PIS_Sys.Properties.Resources.ShowRulelinesHS;
                    }
                    //移除已添加行，提高性能
                    dt.Rows.Remove(dr);
                    i += 1;
                }
                advTree2.EndUpdate();
                if (advTree2.Nodes[0].Nodes.Count > 0)
                {
                    advTree2.Nodes[0].Nodes[0].Expand();
                }
                //添加单击事件处理程序
                this.advTree2.NodeClick += new TreeNodeMouseEventHandler(advTree2_NodeClick);
                this.advTree2.NodeDoubleClick += new TreeNodeMouseEventHandler(advTree2_NodeDoubleClick);
            }
        }

        private void ClearData()
        {
            BBid = 0;
            cal_no = "";
            dtQC.Rows.Clear();
            superGridControl4.PrimaryGrid.InvalidateLayout();
            txt_blh.Text = "";
            textBoxBLH.Text = "";
            txt_brxm.Text = "";
            txt_cfwz.Text = "";
            txt_ckzs.Text = "";
            txt_lkzs.Text = "";
            txt_qcbw.Text = ""; ;
            txt_rysj.Text = "";
            txt_sqd.Text = "";

            txt_crb.Text = "";
            txt_zzs.Text = "";
            superGridControl2.PrimaryGrid.DataSource = null;
            //刷新
            superGridControl2.PrimaryGrid.InvalidateLayout();
            cmb_zzdw.SelectedIndex = 0;

            if (cmb_ckh.Items.Count > 0)
            {
                cmb_ckh.SelectedIndex = 0;
                cal_no = cmb_ckh.Text.Trim();
            }
            else
            {
                cmb_ckh.Text = "";
                cal_no = "";
            }

            cmb_rwly.SelectedIndex = 1;
            cmb_bbcl.SelectedIndex = 0;

        }
        private void Chk_BtnRq(object sender, EventArgs e)
        {
            ButtonItem BtnItem = (ButtonItem)sender;
            for (int i = 0; i < this.QueryBtn.SubItems.Count; i++)
            {
                ((ButtonItem)this.QueryBtn.SubItems[i]).Checked = false;
            }
            BtnItem.Checked = true;
            QueryBtn_Click(null, null);
        }

        public void QueryBtn_Click(object sender, EventArgs e)
        {
            DataSet _DataSet = null;
            string dt_tj = "两周";
            for (int i = 0; i < this.QueryBtn.SubItems.Count; i++)
            {
                ButtonItem BtnItem = (ButtonItem)this.QueryBtn.SubItems[i];
                if (BtnItem.Checked == true)
                {
                    dt_tj = BtnItem.Text;
                    break;
                }
            }

            string zt_tj = "";
            if (chK_status.Checked)
            {
                zt_tj = ((DevComponents.Editors.ComboItem)(cmb_ExamStatus.SelectedItem)).Value.ToString();
            }
            //初始统计计数
            Init_Js();
            //执行数据刷新
            if (_DataSet != null)
            {
                _DataSet.Clear();
            }
            _DataSet = new DataSet();
            DBHelper.BLL.exam_master exam_mas_Ins = new DBHelper.BLL.exam_master();
            _DataSet = exam_mas_Ins.GetQCDsExam_master(dt_tj, zt_tj, Program.workstation_QC_TYPE);
            if (_DataSet != null && _DataSet.Tables[0].Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.ClearSelectedCells();
                superGridControl1.PrimaryGrid.ClearSelectedRows();
                superGridControl1.PrimaryGrid.DataSource = _DataSet;
                //计数
                tj_func(_DataSet.Tables[0]);
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
                ClearData();
            }

        }
        //计数
        public void tj_func(DataTable dt)
        {
            int rows = dt.Rows.Count;

            for (int i = 0; i < rows; i++)
            {
                //是否冰冻
                string ice_flag = dt.Rows[i]["ice_flag"].ToString();
                //状态值
                string status = dt.Rows[i]["exam_status"].ToString();
                //会诊标记
                string huizhen_flag = dt.Rows[i]["huizhen_flag"].ToString();
                //随访标记
                string suifang_flag = dt.Rows[i]["suifang_flag"].ToString();
                //随访完成标记
                string suifang_wc_flag = dt.Rows[i]["suifang_wc_flag"].ToString();

                if (ice_flag.Equals("1"))
                {

                    bdbl_int += 1;

                }


                switch (status)
                {

                    case "20":
                        dj_int += 1;
                        break;
                    case "25":
                        qc_int += 1;
                        break;

                    default:
                        break;
                }
                //最后一行开始赋值
                if (i == rows - 1)
                {
                    if (superGridControl1.PrimaryGrid.Footer == null)
                    {
                        superGridControl1.PrimaryGrid.Footer = new GridFooter();
                    }
                    superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='blue'><b>{0}</b></font> 条记录:登<font color='blue'><b>{1}</b></font>,材<font color='blue'><b>{2}</b></font>", rows, dj_int, qc_int);
                    lbl_bdyy.Text = string.Format(bdyy_str, bdbl_int);

                }
            }


        }
        //计数
        string bdyy_str = "冰冻病例:<font color='blue'><b>{0}</b></font> 例";

        //冰冻特别标识（重要类别计数）

        int dj_int = 0;
        int qc_int = 0;
        int bdbl_int = 0;

        private void Init_Js()
        {

            dj_int = 0;
            qc_int = 0;
            bdbl_int = 0;

            if (superGridControl1.PrimaryGrid.Footer == null)
            {
                superGridControl1.PrimaryGrid.Footer = new GridFooter();
            }
            superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Red'><b>{0}</b></font> 条记录", 0);
            lbl_bdyy.Text = string.Format(bdyy_str, bdbl_int);
        }
        //快速检索
        private void buttonX4_Click(object sender, EventArgs e)
        {
            DataSet _DataSet = null;
            string str_tj = txt_tj.Text.Trim().Replace("'", "");
            if (!str_tj.Equals(""))
            {
                string key_str = ((DevComponents.Editors.ComboItem)(cmb_tj.SelectedItem)).Text.Trim();
                //初始统计计数
                Init_Js();
                //执行查询
                if (_DataSet != null)
                {
                    _DataSet.Clear();
                }
                _DataSet = new DataSet();
                DBHelper.BLL.exam_master exam_mas_Ins = new DBHelper.BLL.exam_master();
                _DataSet = exam_mas_Ins.QueryDsExam_master(key_str, str_tj, Program.workstation_type_db);
                if (_DataSet != null && _DataSet.Tables[0].Rows.Count > 0)
                {
                    superGridControl1.PrimaryGrid.ClearSelectedCells();
                    superGridControl1.PrimaryGrid.ClearSelectedRows();
                    superGridControl1.PrimaryGrid.DataSource = _DataSet;
                    superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Red'><b>{0}</b></font> 条记录", _DataSet.Tables[0].Rows.Count);

                }
                else
                {
                    superGridControl1.PrimaryGrid.DataSource = null;
                    Frm_TJInfo("病人不存在", string.Format("{0}为{1}的查询为空！", key_str, str_tj));
                    ClearData();

                }
            }
        }



        //科室留言
        private void lbl_knly_Click(object sender, EventArgs e)
        {
            Form Frm_mesIns = Application.OpenForms["FrmMessage"];
            if (Frm_mesIns == null)
            {
                Frm_mesIns = new FrmMessage();
                Frm_mesIns.TopMost = true;
                Frm_mesIns.WindowState = FormWindowState.Normal;
                Frm_mesIns.Show();
                Frm_mesIns.Activate();
            }
            else
            {
                Frm_mesIns.BringToFront();
                Frm_mesIns.WindowState = FormWindowState.Normal;
                Frm_mesIns.Activate();
            }

        }

        private void txt_tj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                string str_tj = txt_tj.Text.Trim();
                if (!str_tj.Equals(""))
                {
                    buttonX4_Click(null, null);
                }
            }
        }

        //选中指定exam_no的记录
        private void SelectedOldExam_NO(string exam_no)
        {
            for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count; i++)
            {
                string cur_studyno = ((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["exam_no"].Value.ToString();
                if (cur_studyno.Equals(exam_no))
                {
                    //先清空以前选中的所有行
                    this.superGridControl1.PrimaryGrid.ClearSelectedCells();
                    this.superGridControl1.PrimaryGrid.ClearSelectedRows();
                    //最前面的黑色三角号跟着移动
                    this.superGridControl1.PrimaryGrid.SetActiveRow((GridContainer)superGridControl1.PrimaryGrid.Rows[i]);
                    //选中当前行
                    superGridControl1.PrimaryGrid.SetSelectedRows(i, 1, true);
                    superGridControl1.PrimaryGrid.SetActiveRow((GridRow)superGridControl1.PrimaryGrid.Rows[i]);
                    superGridControl1.PrimaryGrid.Rows[i].EnsureVisible();
                    break;
                }
            }
        }
        string lcjc_str = "历次检查:<font color='blue'><b>{0}</b></font>例";
        Boolean thumsFlag = false;
        private void ShowJcInfo(string exam_no, string patient_id)
        {
            try
            {
                //取信息并赋值给界面
                ClearData();
                //1.病人基本信息
                DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                DataTable dt = insM.GetQcPatInfo(exam_no);
                if (dt != null)
                {
                    txt_blh.Text = dt.Rows[0]["study_no"].ToString();
                    textBoxBLH.Text = txt_blh.Text;
                    txt_sqd.Text = exam_no;
                    //取申请单传染病信息
                    DBHelper.BLL.exam_requisition insReq = new DBHelper.BLL.exam_requisition();
                    txt_crb.Text = insReq.GetInfectious_note(exam_no);
                    txt_brxm.Text = dt.Rows[0]["patient_name"].ToString();
                }
                //2.标本信息
                DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                DataTable Spdt = ins.GetQCSpecimensInfo(exam_no);
                if (Spdt != null)
                {
                    superGridControl2.PrimaryGrid.DataSource = Spdt;
                    //选中第一行
                    if (Spdt.Rows.Count > 0)
                    {
                        superGridControl2.PrimaryGrid.SetSelectedRows(0, 1, true);
                    }
                    superGridControl2.PrimaryGrid.InvalidateLayout();
                }
                else
                {
                    superGridControl2.PrimaryGrid.DataSource = null;
                }
                //3.多媒体资料下载
                DBHelper.BLL.multi_media_info insMulti = new DBHelper.BLL.multi_media_info();
                DataTable dtMulit = insMulti.GetData(txt_blh.Text.Trim(), 1);
                if (dtMulit != null && dtMulit.Rows.Count > 0)
                {
                    string PathImg = ImgFolder + @"\" + txt_blh.Text.Trim();
                    if (Directory.Exists(PathImg) == false)
                    {
                        Directory.CreateDirectory(PathImg);
                        //开始下载
                        List<string> RemoteLst = new List<string>();
                        List<string> LocalLst = new List<string>();
                        for (int i = 0; i < dtMulit.Rows.Count; i++)
                        {
                            RemoteLst.Add(dtMulit.Rows[i]["path"].ToString());
                            LocalLst.Add(string.Format(@"{0}\{1}", PathImg, dtMulit.Rows[i]["filename"].ToString()));
                        }
                        try
                        {
                            ftpDownload1.ExecuteDownLoad(Program.FtpIP, Program.FtpPort, Program.FtpUser, Program.FtpPwd, RemoteLst, LocalLst, 50000, RemoteLst.Count);
                            ftpDownload1.Visible = true;
                        }
                        catch
                        {
                            ftpDownload1.Visible = false;
                        }

                        Application.DoEvents();
                    }
                    else
                    {
                        DirectoryInfo Dir = new DirectoryInfo(PathImg);
                        int fileCount = Dir.GetFiles().Count();
                        if (dtMulit.Rows.Count > fileCount)
                        {
                            //下载剩余的
                            List<string> RemoteLst = new List<string>();
                            List<string> LocalLst = new List<string>();
                            Boolean downFlag = true;
                            for (int j = 0; j < dtMulit.Rows.Count; j++)
                            {
                                foreach (FileInfo FI in Dir.GetFiles())
                                {
                                    if (FI.Name.Equals(dtMulit.Rows[j]["filename"].ToString()))
                                    {
                                        downFlag = false;
                                        break;
                                    }
                                }
                                if (downFlag)
                                {
                                    RemoteLst.Add(dtMulit.Rows[j]["path"].ToString());
                                    LocalLst.Add(string.Format(@"{0}\{1}", PathImg, dtMulit.Rows[j]["filename"].ToString()));
                                }
                                downFlag = true;
                            }
                            try
                            {
                                ftpDownload1.ExecuteDownLoad(Program.FtpIP, Program.FtpPort, Program.FtpUser, Program.FtpPwd, RemoteLst, LocalLst, 50000, RemoteLst.Count);
                                ftpDownload1.Visible = true;
                            }
                            catch
                            {
                                ftpDownload1.Visible = false;
                            }
                            Application.DoEvents();

                        }
                        else
                        {
                            //刷新多媒体列表
                            RefreshMultiList();
                        }
                    }
                }
                else
                {
                    imageListView1.Items.Clear();
                    imageListView1.SuspendLayout();
                    imageListView1.ResumeLayout();
                }

            }
            catch (Exception ex)
            {
                Program.FileLog.ErrorFormat("病人信息展示异常", "请确认FTP服务器正常设置.\n{0}", ex.ToString());
            }


        }
        private void superGridControl1_SelectionChanged(object sender, GridEventArgs e)
        {

            if (superGridControl1.PrimaryGrid.Rows.Count > 0)

            {
                if (ConfigurationManager.AppSettings["QcSaveAuto"] == "1")
                {
                    //自动保存一次
                    btn_saveAuto_Click();
                }

                if (e.GridPanel.ActiveRow == null)
                {
                    superGridControl1.PrimaryGrid.Rows.Clear();
                    superGridControl1.PrimaryGrid.InvalidateLayout();
                    return;
                }
                int index = e.GridPanel.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //申请单号
                string exam_no = Row.Cells["exam_no"].Value.ToString();
                //病人id号
                string patient_id = Row.Cells["patient_id"].Value.ToString();
                //病人姓名
                string patient_name = Row.Cells["patient_name"].Value.ToString();
                //取历次检查数目
                DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                int total = ins.GetExamCount(exam_no, patient_name);
                lbl_history.Text = string.Format(lcjc_str, total);
                // 实时查询状态信息
                int CurStatus = ins.GetExam_Status(exam_no);
                if (CurStatus > 20)
                {
                    cmb_qcys.Text = ins.GetQCYS(Row.Cells["study_no"].Value.ToString());
                }
                if (CurStatus != Convert.ToInt32(Row.Cells["exam_status"].Value.ToString()))
                {
                    if (examStatus_dic.ContainsKey(CurStatus.ToString()))
                    {
                        if (exam_status_dic.ContainsKey(CurStatus.ToString()))
                        {
                            Row.Cells["status_name"].CellStyles.Default.TextColor = exam_status_dic[CurStatus.ToString()];
                            Row.Cells["exam_status"].Value = CurStatus.ToString();
                        }
                        Row.Cells["status_name"].Value = examStatus_dic[CurStatus.ToString()];
                    }
                    else
                    {
                        //刷新列表
                        QueryBtn_Click(null, null);
                    }
                }
                //
                if (Convert.ToInt32(Row.Cells["ks_flag"].Value.ToString()) == 1)
                {
                    ks_flag_str = "快速";
                }
                else
                {
                    ks_flag_str = "";
                }
                //数据展示
                ShowJcInfo(exam_no, patient_id);
                //基本信息
                this.patientInfo1.SetPatinfo(patient_id, exam_no);
            }
            superGridControl1.Select();
            superGridControl1.Focus();
        }

        public string ks_flag_str = "";

        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            GridPanel panel = e.GridPanel;
            panel.DefaultVisualStyles.CaptionStyles.Default.Alignment = Alignment.MiddleCenter;
            panel.DefaultVisualStyles.CellStyles.Default.Alignment = Alignment.MiddleCenter;
            if (panel.Footer == null)
            {
                panel.Footer = new GridFooter();
            }
            //选中先前的那条
            if (!txt_sqd.Text.Trim().Equals(""))
            {
                SelectedOldExam_NO(txt_sqd.Text.Trim());
            }
        }

        private void superGridControl1_GetCellStyle(object sender, GridGetCellStyleEventArgs e)
        {
            if (e.GridCell.GridColumn.Name.Equals("status_name") == true)
            {
                string strStatus = (string)e.GridCell.Value;
                if (exam_status_name_dic.ContainsKey(strStatus))
                {
                    e.Style.TextColor = exam_status_name_dic[strStatus];
                }
            }
        }

        private void superGridControl1_GetRowHeaderStyle(object sender, GridGetRowHeaderStyleEventArgs e)
        {
            try
            {
                if (superGridControl1.PrimaryGrid.Rows.Count > 0)
                {
                    //是否冰冻
                    string ice_flag = ((GridRow)e.GridRow).Cells["ice_flag"].Value.ToString(); //  GridPanel panel = e.GridPanel; panel.GetCell(e.GridRow.Index, 18).Value.ToString();

                    if (ice_flag.Equals("1"))
                    {
                        e.GridRow.RowHeaderText = "冰";
                        e.Style.TextColor = Color.Red;
                    }
                    string ks_flag = ((GridRow)e.GridRow).Cells["ks_flag"].Value.ToString();
                    if (ks_flag.Equals("1"))
                    {
                        e.GridRow.RowHeaderText = "快";
                        e.Style.TextColor = Color.Red;
                    }
                }

            }
            catch
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //刷新列表
            QueryBtn_Click(null, null);
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (!txt_blh.Text.Trim().Equals(""))
            {

                if (dtQC != null && dtQC.Rows.Count > 0 && BBid != 0)
                {

                    if (!txt_ckzs.Text.Trim().Equals("") && !txt_lkzs.Text.Trim().Equals(""))
                    {
                        string result_str = "";
                        DBHelper.BLL.exam_specimens insDT = new DBHelper.BLL.exam_specimens();
                        string lrcode = "";
                        if (cmb_lrr.SelectedValue != null)
                        {
                            lrcode = cmb_lrr.SelectedValue.ToString();
                        }
                        if (insDT.UpdateSpecimensDTQC(BBid, txt_rysj.Text.Trim(), lrcode, cmb_lrr.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), cmb_bbcl.Text.Trim(), txt_cfwz.Text.Trim(), Convert.ToInt16(txt_lkzs.Text.Trim()), Convert.ToInt16(txt_ckzs.Text.Trim()), ref result_str) == false)
                        {
                            Frm_TJInfo("大体描述保存失败！", result_str);
                            return;
                        }
                    }

                    DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                    string Str_Result = "";
                    if (ins.Process_draw_meterials(dtQC, txt_blh.Text.Trim(), ref Str_Result) == false)
                    {
                        Frm_TJInfo("取材信息保存失败！", Str_Result);
                        return;
                    }
                    Frm_TJInfo("提示！", "取材信息保存成功！");
                    //刷新下本次保存完毕的取材列表
                    RefreshQCList();
                    //
                    superTabControl2.SelectedTabIndex = 0;
                    //保存自动核对
                    if (ConfigurationManager.AppSettings["QcSaveAutoHd"] == "1")
                    {
                        //更新全部取材为核对状态
                        DBHelper.BLL.exam_draw_meterials insQC = new DBHelper.BLL.exam_draw_meterials();
                        int resultM = insQC.UpdateAllHDDrawInfo(txt_blh.Text.Trim());

                        //检查是否状态小于25（已取材）
                        DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                        int status = insM.GetStudyExam_Status(txt_blh.Text.Trim());
                        if (status < 25 && status >= 20)
                        {
                            //更新为取材完成状态
                            DBHelper.BLL.exam_master insHd = new DBHelper.BLL.exam_master();
                            string qcys_name = Program.User_Name;
                            string qcys_code = Program.User_Code;
                            if (cmb_qcys.SelectedValue != null)
                            {
                                qcys_name = cmb_qcys.Text.Trim();
                                qcys_code = cmb_qcys.SelectedValue.ToString();
                            }
                            int result = insHd.UpdateQC_Flag(txt_blh.Text.Trim(), qcys_code, qcys_name);
                            if (result == 1)
                            {
                                //刷新列表
                                QueryBtn_Click(QueryBtn, null);
                            }
                        }
                    }
                    //
                    superTabControl2.SelectedTabIndex = 0;

                }
                else
                {
                    if (txt_blh.Text.Trim().Equals(""))
                    {
                        Frm_TJInfo("当前不能执行保存操作", "请先选择一个病理号，取材后再进行保存！");
                    }
                    else if (dtQC != null && dtQC.Rows.Count == 0)
                    {
                        Frm_TJInfo("当前不能执行保存操作", "请输入此标本取材信息后再进行保存！");
                    }
                    else
                    {
                        if (superGridControl2.PrimaryGrid.Rows.Count == 0)
                        {
                            Frm_TJInfo("提示", "当前不存在标本采集信息；故不能保存！");
                        }
                        else
                        {
                            Frm_TJInfo("提示", "当前不需要保存！");
                        }
                    }
                }
            }
        }



        private void btn_saveAuto_Click()
        {
            if (!txt_blh.Text.Trim().Equals(""))
            {


                if (BBid != 0)
                {
                    if (txt_ckzs.Text.Trim().Equals(""))
                    {
                        txt_ckzs.Text = "0";
                    }
                    if (txt_lkzs.Text.Trim().Equals(""))
                    {
                        txt_lkzs.Text = "0";
                    }
                    string result_str = "";
                    DBHelper.BLL.exam_specimens insDT = new DBHelper.BLL.exam_specimens();
                    string lrcode = "";
                    if (cmb_lrr.SelectedValue != null)
                    {
                        lrcode = cmb_lrr.SelectedValue.ToString();
                    }
                    if (insDT.UpdateSpecimensDTQC(BBid, txt_rysj.Text.Trim(), lrcode, cmb_lrr.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), cmb_bbcl.Text.Trim(), txt_cfwz.Text.Trim(), Convert.ToInt16(txt_lkzs.Text.Trim()), Convert.ToInt16(txt_ckzs.Text.Trim()), ref result_str) == false)
                    {
                        return;
                    }
                }

                if (dtQC != null && dtQC.Rows.Count > 0 && BBid != 0)
                {
                    DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                    string Str_Result = "";
                    if (ins.Process_draw_meterials(dtQC, txt_blh.Text.Trim(), ref Str_Result) == false)
                    {
                        return;
                    }

                    //刷新下本次保存完毕的取材列表
                    RefreshQCList();
                    //
                    superTabControl2.SelectedTabIndex = 0;

                }
            }
        }

        private void superGridControl2_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {

            //更新当前行
            GridCell cell = e.GridCell;
            if (cell == null)
            {
                return;
            }
            GridRow row = cell.GridRow;
            if (row == null)
            {
                return;
            }
            if (row.Index == -1)
            {
                return;
            }
            int id = Convert.ToInt32(row.Cells["id"].Value);

            if (cell.GridColumn.Name.Equals("specimens_class") == true)
            {
                if (e.NewValue != e.OldValue)
                {
                    //更新原先这条标本的标本类型
                    DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                    ins.UpdateSpecimensClass(id, e.NewValue.ToString());
                }
            }
            if (cell.GridColumn.Name.Equals("pack_order") == true)
            {
                if (e.NewValue != e.OldValue && txt_blh.Text.Trim() != "")
                {
                    //更新原先这条标本的容器号和编码
                    DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                    ins.UpdateSpecimensRqh(id, e.NewValue.ToString());
                }
            }
            if (cell.GridColumn.Name.Equals("parts") == true)
            {
                if (e.NewValue != e.OldValue && !e.NewValue.ToString().Equals(""))
                {
                    //更新原先这条标本的部位名称
                    DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                    ins.UpdateSpecimensParts(id, e.NewValue.ToString());
                }
            }
            if (cell.GridColumn.Name.Equals("memo_note") == true)
            {
                if (e.NewValue != e.OldValue)
                {
                    //更新原先这条标本的备注
                    DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                    ins.UpdateSpecimensNote(id, e.NewValue.ToString());
                }
            }
            e.GridCell.InvalidateLayout();
        }
        //切换标本或者病人时，先进行一次保存取材信息
        public void save_preQCInfo()
        {


            if (!txt_blh.Text.Trim().Equals("") && BBid != 0)
            {

                if (dtQC != null && dtQC.Rows.Count > 0)
                {
                    if (!txt_ckzs.Text.Trim().Equals("") && !txt_lkzs.Text.Trim().Equals(""))
                    {
                        string result_str = "";
                        DBHelper.BLL.exam_specimens insDT = new DBHelper.BLL.exam_specimens();
                        if (insDT.UpdateSpecimensDTQC(BBid, txt_rysj.Text.Trim(), cmb_lrr.SelectedValue.ToString(), cmb_lrr.Text.Trim(), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), cmb_bbcl.Text.Trim(), txt_cfwz.Text.Trim(), Convert.ToInt16(txt_lkzs.Text.Trim()), Convert.ToInt16(txt_ckzs.Text.Trim()), ref result_str) == false)
                        {
                            return;
                        }
                    }

                    DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                    string Str_Result = "";
                    if (ins.Process_draw_meterials(dtQC, txt_blh.Text.Trim(), ref Str_Result) == false)
                    {
                        return;
                    }

                    //保存成功后清空数据
                    dtQC.Rows.Clear();
                    superGridControl4.PrimaryGrid.InvalidateLayout();
                    txt_rysj.Text = "";
                    txt_cfwz.Text = "";

                    cmb_bbcl.SelectedIndex = 0;
                    txt_zzs.Text = "";
                    txt_lkzs.Text = "";
                    BBid = 0;
                }
            }
        }
        int BBid = 0;
        private void superGridControl2_SelectionChanged(object sender, GridEventArgs e)
        {
            if (superGridControl2.PrimaryGrid.Rows.Count > 0)
            {
                if (e.GridPanel.ActiveRow == null)
                {
                    superGridControl2.PrimaryGrid.Rows.Clear();
                    superGridControl2.PrimaryGrid.InvalidateLayout();
                    return;
                }
                int index = e.GridPanel.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                //触发上一次的保存逻辑

                save_preQCInfo();

                //标本取材部位
                txt_qcbw.Text = ((GridRow)e.GridPanel.ActiveRow).Cells["parts"].Value.ToString();
                if (!txt_qcbw.Text.Trim().Equals(""))
                {
                    txt_qcbw.SelectionStart = txt_qcbw.Text.Length;
                }
                //当前标本所在数据库表中的id字段值
                BBid = Convert.ToInt32(((GridRow)e.GridPanel.ActiveRow).Cells["id"].Value);
                //查询是否规范化固定
                DataSet _DataSet = new DataSet();
                DBHelper.BLL.exam_specimens insbhg = new DBHelper.BLL.exam_specimens();
                int BuIcount = insbhg.GetBbBuGfhFlag(BBid);
                if (BuIcount > 0)
                {
                    buttonX13.TextColor = Color.Blue;
                    buttonX14.TextColor = CurBtnColor;
                }
                else
                {
                    buttonX14.TextColor = Color.Blue;
                    buttonX13.TextColor = CurBtnColor;
                }
                //刷新取材列表
                RefreshQCList();
            }

        }
        //刷新取材列表
        private void RefreshQCList()
        {
            //当前取材信息
            if (!txt_blh.Text.Trim().Equals(""))
            {
                //清空数据
                dtQC.Rows.Clear();
                superGridControl4.PrimaryGrid.InvalidateLayout();

                //展示材块数目及材块处理及大体描述
                DBHelper.BLL.exam_specimens insDT = new DBHelper.BLL.exam_specimens();
                DataTable dtDT = insDT.GetSpecimensDTQC(BBid);
                if (dtDT != null)
                {
                    txt_rysj.Text = dtDT.Rows[0]["see_memo"].ToString();
                    txt_cfwz.Text = dtDT.Rows[0]["specimens_location"].ToString();
                    //获取检查状态(展示报告信息)
                    DBHelper.BLL.exam_master insStatus = new DBHelper.BLL.exam_master();
                    int Curstatus = insStatus.GetStudyExam_Status(txt_blh.Text.Trim());
                    if (Curstatus >= 25)
                    {
                        cmb_lrr.Text = dtDT.Rows[0]["record_doctor_name"].ToString();
                    }
                    else
                    {
                        if (cmb_lrr.Text.Trim().Equals(""))
                        {
                            cmb_lrr.Text = Program.User_Name;
                        }
                    }
                    cmb_bbcl.Text = dtDT.Rows[0]["specimens_process"].ToString();
                    txt_ckzs.Text = dtDT.Rows[0]["ck_count"].ToString();
                    txt_lkzs.Text = dtDT.Rows[0]["lk_count"].ToString();
                    dtDT.Clear();
                }


                DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                DataTable dt = ins.GetMeterialsInfo(txt_blh.Text.Trim(), BBid);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dtQC.NewRow();
                        //取材表中数据库中的id字段值
                        dr["id"] = dt.Rows[i]["id"];
                        //标本表id 
                        dr["specimens_id"] = BBid;
                        //任务来源
                        dr["work_source"] = dt.Rows[i]["work_source"];
                        //材块编号
                        dr["meterial_no"] = dt.Rows[i]["meterial_no"];
                        //取材部位
                        dr["parts"] = dt.Rows[i]["parts"];
                        //组织数
                        dr["group_num"] = dt.Rows[i]["group_num"];
                        //组织单位
                        dr["group_unite"] = dt.Rows[i]["group_unite"];
                        //备注
                        dr["memo_note"] = dt.Rows[i]["memo_note"];

                        //取材医生姓名
                        dr["draw_doctor_name"] = dt.Rows[i]["draw_doctor_name"];
                        //取材时间
                        dr["draw_datetime"] = dt.Rows[i]["draw_datetime"];
                        //是否新加
                        dr["new_flag"] = 0;
                        dr["lktsdt_flag"] = dt.Rows[i]["lktsdt_flag"];
                        dtQC.Rows.Add(dr);
                    }
                    dt.Clear();
                    //刷新
                    superGridControl4.PrimaryGrid.DataSource = dtQC;
                    Application.DoEvents();
                    //选中最新添加行
                    if (dtQC.Rows.Count > 0)
                    {
                        superGridControl4.PrimaryGrid.SetSelectedRows(dtQC.Rows.Count - 1, 1, true);
                        superGridControl4.PrimaryGrid.Rows[dtQC.Rows.Count - 1].EnsureVisible();
                    }
                }
                else
                {
                    //取材信息自动添加一行
                    if (ConfigurationManager.AppSettings["QcInfoAutoZj"] == "1")
                    {
                        DataRow dr = dtQC.NewRow();
                        //取材表中数据库中的id字段值
                        dr["id"] = 0;
                        //标本表id 
                        dr["specimens_id"] = BBid;
                        //任务来源
                        dr["work_source"] = cmb_rwly.Text.ToString();
                        //材块编号
                        if (ConfigurationManager.AppSettings["QchmZj"] == "1")
                        {
                            dr["meterial_no"] = CalMeterial_No(cal_no);
                        }
                        else
                        {
                            dr["meterial_no"] = cmb_ckh.Text.Trim();
                        }
                        //取材部位
                        dr["parts"] = txt_qcbw.Text.Trim();
                        //组织数
                        dr["group_num"] = "1";
                        //组织单位
                        dr["group_unite"] = cmb_zzdw.Text.ToString();
                        //备注
                        dr["memo_note"] = "";

                        //取材医生姓名
                        dr["draw_doctor_name"] = cmb_qcys.Text.ToString();
                        //取材时间
                        dr["draw_datetime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        //是否新加
                        dr["new_flag"] = 1;
                        dr["lktsdt_flag"] = "快";
                        dtQC.Rows.Add(dr);
                        //刷新
                        superGridControl4.PrimaryGrid.InvalidateLayout();
                        //选中最新添加行
                        if (dtQC.Rows.Count > 0)
                        {
                            superGridControl4.PrimaryGrid.SetSelectedRows(dtQC.Rows.Count - 1, 1, true);
                            superGridControl4.PrimaryGrid.Rows[dtQC.Rows.Count - 1].EnsureVisible();
                        }
                        //计算腊块数和材块数
                        Cal_lkck();
                    }
                }
                //查询检查状态 自动填充肉眼所见的部位开头
                //DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                //int status = insM.GetStudyExam_Status(txt_blh.Text.Trim());
                //if (status < 25 && status >= 20)
                //{
                //    if (txt_rysj.Text.Trim().Equals(""))
                //    {
                //        DBHelper.BLL.exam_specimens insSpec = new DBHelper.BLL.exam_specimens();
                //        DataTable dtSpec = insSpec.GetQCSpecimensInfo(txt_sqd.Text.Trim());
                //        if (dtSpec != null && dtSpec.Rows.Count > 0)
                //        {
                //            StringBuilder sb = new StringBuilder();
                //            for (int i = 0; i <= dtSpec.Rows.Count - 1; i++)
                //            {
                //                sb.AppendFormat("{0}:", dtSpec.Rows[i]["parts"].ToString());
                //                if (i != dtSpec.Rows.Count - 1)
                //                {
                //                    sb.AppendLine();
                //                }
                //            }
                //            txt_rysj.Text = sb.ToString();
                //            sb.Clear();
                //            sb = null;
                //        }
                //    }
                //}

            }
        }
        private void btn_clear_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        //提示窗体
        public void Frm_TJInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(this);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = true;
            Frm_AlertIns.AutoCloseTimeOut = 1;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }
        private string CalMeterial_No(string meterial_no)
        {
            if (meterial_no.Equals(""))
            {
                meterial_no = "1";
            }

            meterial_no = meterial_no.ToUpper();

            Boolean y_flag = false;
            for (int i = 0; i < dtQC.Rows.Count; i++)
            {
                if (dtQC.Rows[i]["meterial_no"].ToString() == meterial_no)
                {
                    y_flag = true;
                    break;
                }
            }
            if (y_flag)
            {
                if (meterial_no.Length == 1)
                {
                    if (Microsoft.VisualBasic.Information.IsNumeric(meterial_no) == true)
                    {
                        meterial_no = (Convert.ToInt32(meterial_no) + 1).ToString();
                    }
                    else
                    {
                        //取字母表
                        meterial_no = Convert.ToChar((Microsoft.VisualBasic.Strings.Asc(meterial_no) + 1)).ToString();

                    }
                }
                else if (meterial_no.Length == 2)
                {
                    if (Microsoft.VisualBasic.Information.IsNumeric(meterial_no) == true)
                    {
                        meterial_no = (Convert.ToInt32(meterial_no) + 1).ToString();
                    }
                    else
                    {
                        string first = meterial_no.Substring(0, 1);
                        string second = meterial_no.Substring(1, 1);
                        //取字母表
                        second = Convert.ToChar((Microsoft.VisualBasic.Strings.Asc(second) + 1)).ToString();
                        if (second.Equals(":"))
                        {
                            second = "10";
                        }
                        meterial_no = first + second;
                    }
                }
                else if (meterial_no.Length == 3)
                {
                    if (Microsoft.VisualBasic.Information.IsNumeric(meterial_no) == true)
                    {
                        meterial_no = (Convert.ToInt32(meterial_no) + 1).ToString();
                    }
                    else
                    {
                        string first = meterial_no.Substring(0, 2);

                        string second = meterial_no.Substring(2, 1);

                        string mid = meterial_no.Substring(1, 2);
                        string mFirst = meterial_no.Substring(0, 1);
                        if (Microsoft.VisualBasic.Information.IsNumeric(mid) == true)
                        {
                            meterial_no = mFirst + (Convert.ToInt32(mid) + 1).ToString();
                        }
                        else
                        {
                            //取字母表
                            second = Convert.ToChar((Microsoft.VisualBasic.Strings.Asc(second) + 1)).ToString();
                            if (second.Equals(":"))
                            {
                                second = "10";
                            }
                            meterial_no = first + second;
                        }
                    }
                }
            }
            cal_no = meterial_no;
            return meterial_no;

        }
        //增加取材
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (superGridControl2.PrimaryGrid.Rows.Count == 0)
            {
                Frm_TJInfo("提示", "当前不存在标本采集信息；故不能添加取材信息！\n请到标本接收模块，输入标本采集信息！");
                return;
            }

            string rwly = cmb_rwly.Text.ToString();
            if (rwly.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "请输入任务来源！");

                cmb_rwly.Focus();
                return;
            }

            if (txt_zzs.Text.Trim().Equals(""))
            {
                txt_zzs.Text = "1";
            }

            if (Microsoft.VisualBasic.Information.IsNumeric(txt_zzs.Text.Trim()) == false)
            {
                txt_zzs.Text = "";
                txt_zzs.Focus();
                return;
            }
            DataRow dr = dtQC.NewRow();
            //取材表中数据库中的id字段值
            dr["id"] = 0;
            //标本表id 
            dr["specimens_id"] = BBid;
            //任务来源
            dr["work_source"] = rwly;
            //材块编号
            if (ConfigurationManager.AppSettings["QchmZj"] == "1")
            {
                dr["meterial_no"] = CalMeterial_No(cal_no);
            }
            else
            {
                dr["meterial_no"] = cmb_ckh.Text.Trim();
            }
            //取材部位
            dr["parts"] = txt_qcbw.Text.Trim();
            //组织数
            dr["group_num"] = txt_zzs.Text.Trim();
            //组织单位
            dr["group_unite"] = cmb_zzdw.Text.ToString();
            //备注
            dr["memo_note"] = ks_flag_str;
            //取材医生姓名
            dr["draw_doctor_name"] = cmb_qcys.Text.ToString();
            //取材时间
            dr["draw_datetime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //是否新加
            dr["new_flag"] = 1;
            //蜡块脱水
            dr["lktsdt_flag"] = "快";
            dtQC.Rows.Add(dr);
            superGridControl4.PrimaryGrid.InvalidateLayout();
            //选中最新添加行
            if (dtQC.Rows.Count > 0)
            {
                //选中当前行
                superGridControl4.PrimaryGrid.SetSelectedRows(dtQC.Rows.Count - 1, 1, true);
                superGridControl4.PrimaryGrid.Rows[dtQC.Rows.Count - 1].EnsureVisible();
            }
            //计算腊块数和材块数
            Cal_lkck();
            //包埋盒打印
            Print_bmh(txt_blh.Text.Trim(), dr["meterial_no"].ToString());

        }

        private void Print_bmh(string barcode, string ckh)
        {
            //模式
            if (ConfigurationManager.AppSettings["sfmbmh_enable"] != null)
            {
                if (ConfigurationManager.AppSettings["sfmbmh_enable"].ToString().Equals("1"))
                {
                    string bmh_path = ConfigurationManager.AppSettings["sfmbmh_path"].ToString().Trim();
                    if (Directory.Exists(bmh_path) == true)
                    {
                        string uid = Helper.GetUidStr();
                        string file_path = bmh_path + @"/" + uid + ".txt";
                        try
                        {
                            System.IO.StreamWriter sw = new StreamWriter(file_path, false, System.Text.Encoding.Default);
                            //组织数目
                            string zzs_str = "";
                            if (txt_zzs.Text.Trim().Equals("0"))
                            {
                                txt_zzs.Text = "";
                                zzs_str = " ";
                            }
                            else
                            {
                                zzs_str = "(" + txt_zzs.Text.Trim() + ")";
                            }
                            //蜡块号
                            if (!ckh.Equals(""))
                            {
                                ckh = "-" + ckh;
                            }
                            else
                            {
                                ckh = " ";
                            }
                            sw.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", barcode, ckh, txt_qcbw.Text.Trim(), zzs_str, cmb_zzdw.Text.Trim(), DateTime.Now.ToString("yyyyMMdd").Substring(2, 2), DateTime.Now.ToString("yyyyMMdd").Substring(2, 6), cmb_rwly.Text.Trim()));
                            sw.Close();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("请打开设置系统，对接口路径进行设置！", "包埋盒打印接口", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

        }



        private void Cal_lkck()
        {
            if (dtQC != null && dtQC.Rows.Count > 0)
            {
                txt_lkzs.Text = dtQC.Rows.Count.ToString();
                int cksm = 0;
                for (int i = 0; i < dtQC.Rows.Count; i++)
                {
                    cksm += Convert.ToInt32(dtQC.Rows[i]["group_num"]);
                }
                txt_ckzs.Text = cksm.ToString();
            }
        }
        //删除取材
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (dtQC.Rows.Count > 0)
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;

                if (TaskDialog.Show("删除取材信息", "确认", "确定要删除这条取材信息么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    int index = superGridControl4.PrimaryGrid.ActiveRow.Index;
                    if (index == -1)
                    {
                        return;
                    }
                    if (!dtQC.Rows[index]["id"].ToString().Equals("0") && dtQC.Rows[index]["new_flag"].ToString().Equals("0"))
                    {
                        DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                        ins.DelMeterials(Convert.ToInt32(dtQC.Rows[index]["id"]));
                    }
                    dtQC.Rows.RemoveAt(index);
                }

            }
            else
            {
                dtQC.Rows.Clear();
            }
            //刷新
            superGridControl4.PrimaryGrid.InvalidateLayout();
            //计算材块数目和腊块数目
            Cal_lkck();
        }
        //更新材块编号
        string cal_no = "";
        private void cmb_ckh_TextChanged(object sender, EventArgs e)
        {
            if (!cmb_ckh.Text.Trim().Equals(""))
            {
                cal_no = cmb_ckh.Text;
            }

        }

        /// 转全角的函数(SBC case)
        ///
        ///任意字符串
        ///全角字符串
        ///
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///
        public static String ToSBC(String input)
        {
            // 半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }

        /**/
        // /
        // / 转半角的函数(DBC case)
        // /
        // /任意字符串
        // /半角字符串
        // /
        // /全角空格为12288，半角空格为32
        // /其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        // /
        public static String ToDBC(String input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }

        private void superGridControl4_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            //更新当前行备注
            GridCell cell = e.GridCell;
            if (cell == null)
            {
                return;
            }
            GridRow row = cell.GridRow;
            if (row == null)
            {
                return;
            }
            if (row.Index == -1)
            {
                return;
            }
            int id = Convert.ToInt32(row.Cells["id"].Value);
            if (Convert.ToInt32(row.Cells["new_flag"].Value) == 0 && id != 0)
            {
                if (cell.GridColumn.Name.Equals("memo_note") == true)
                {
                    if (e.NewValue != e.OldValue)
                    {
                        //更新原先这条取材的备注信息
                        DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                        ins.UpdateDraw_Meterials(id, e.NewValue.ToString());
                    }
                }
                if (cell.GridColumn.Name.Equals("group_num") == true)
                {
                    string strNum = e.NewValue.ToString().Trim();

                    //if (e.NewValue.ToString().Trim() == "" || Microsoft.VisualBasic.Information.IsNumeric(strNum) == false)
                    //{
                    //    return;
                    //}
                    if (e.NewValue != e.OldValue && Convert.ToInt32(strNum) > 0)
                    {
                        //更新原先这条取材的材块数目
                        DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                        ins.UpdateDraw_Group_num(id, Convert.ToInt32(strNum));
                        Cal_lkck();
                    }
                }
                if (cell.GridColumn.Name.Equals("group_unite") == true)
                {
                    if (e.NewValue != e.OldValue)
                    {
                        //更新原先这条标本的标本类型
                        DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                        ins.UpdateDraw_group_unite(id, e.NewValue.ToString());
                    }
                }
                if (cell.GridColumn.Name.Equals("meterial_no") == true)
                {
                    //更新原先这条取材的编号
                    DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                    ins.UpdateDraw_Meterials_N0(id, e.NewValue.ToString(), txt_blh.Text.Trim());
                }

                if (cell.GridColumn.Name.Equals("parts") == true)
                {
                    if (e.NewValue != e.OldValue)
                    {
                        //更新原先这条取材的材块数目
                        DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                        ins.UpdateParts(id, e.NewValue.ToString());
                    }
                }
                if (cell.GridColumn.Name.Equals("draw_doctor_name") == true)
                {
                    if (e.NewValue != e.OldValue)
                    {
                        //更新原先这条取材的取材医师
                        DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                        ins.UpdateDraw_doctor_name(id, e.NewValue.ToString());
                    }
                }
                if (cell.GridColumn.Name.Equals("work_source") == true)
                {
                    if (e.NewValue != e.OldValue)
                    {
                        //更新原先这条取材的取材医师
                        DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                        ins.UpdateWork_source(id, e.NewValue.ToString());
                    }
                }
                if (cell.GridColumn.Name.Equals("lktsdt_flag") == true)
                {
                    if (e.NewValue != e.OldValue)
                    {
                        //更新上脱水机时间
                        DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                        ins.UpdateTsjDt(id, e.NewValue.ToString());
                    }
                }
            }
            else
            {
                if (cell.GridColumn.Name.Equals("group_num") == true)
                {
                    if (e.NewValue != e.OldValue && Convert.ToInt32(e.NewValue) > 0)
                    {
                        Cal_lkck();
                    }
                }
            }
            e.GridCell.InvalidateLayout();
        }
        //追加大体描述
        private void buttonItem11_Click(object sender, EventArgs e)
        {
            if (btn_save.Enabled == false)
            {
                return;
            }
            if (!richTextBoxEx2.Text.Trim().Equals(""))
            {
                if (txt_rysj.SelectionLength > 0)
                {
                    txt_rysj.SelectionStart = txt_rysj.SelectionStart + txt_rysj.SelectionLength;
                }
                txt_rysj.SelectedText = richTextBoxEx2.Text.Trim();
                txt_rysj.Focus();
            }
        }
        //替换大体描述
        private void buttonItem12_Click(object sender, EventArgs e)
        {
            if (btn_save.Enabled == false)
            {
                return;
            }
            if (!txt_rysj.Text.Trim().Equals(""))
            {
                if (!richTextBoxEx2.Text.Trim().Equals(""))
                {
                    eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                    TaskDialog.EnableGlass = false;

                    if (TaskDialog.Show("询问", "确认", "确定替换现有的大体描述信息么？", Curbutton) == eTaskDialogResult.Ok)
                    {

                        txt_rysj.Text = richTextBoxEx2.Text.Trim();
                        txt_rysj.Focus();
                        txt_rysj.SelectionStart = txt_rysj.Text.Trim().Length;
                    }
                }
            }
            else
            {
                if (!richTextBoxEx2.Text.Trim().Equals(""))
                {

                    txt_rysj.Text = richTextBoxEx2.Text.Trim();
                    txt_rysj.Focus();
                    txt_rysj.SelectionStart = txt_rysj.Text.Trim().Length;
                }
            }
        }
        //追加选中
        private void btn_zjxz_Click(object sender, EventArgs e)
        {
            if (btn_save.Enabled == false)
            {
                return;
            }
            if (!richTextBoxEx2.SelectedText.Trim().Equals(""))
            {
                txt_rysj.SelectedText = richTextBoxEx2.SelectedText.Trim();
                txt_rysj.Focus();
            }
        }
        private void 剪切ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txt_rysj.SelectedText);
                txt_rysj.SelectedText = String.Empty;
            }
            catch
            {
            }
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(txt_rysj.SelectedText);
            }
            catch
            {
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txt_rysj.SelectedText = Clipboard.GetText();
            }
            catch
            {
            }
        }
        //显示大体采集界面
        private void buttonItem13_Click(object sender, EventArgs e)
        {
            if (!txt_blh.Text.Trim().Equals(""))
            {
                Frm_Dtcj Frm_DtcjIns = new Frm_Dtcj();
                Frm_DtcjIns.Owner = this;
                Frm_DtcjIns.Cap_BLH = txt_blh.Text.Trim();
                Frm_DtcjIns.Cap_Sqdh = txt_sqd.Text.Trim();
                Frm_DtcjIns.Cap_XM = txt_brxm.Text.Trim();
                Frm_DtcjIns.BringToFront();
                Frm_DtcjIns.ShowDialog();
                //刷新多媒体信息
                RefreshMultiList();
            }
            else
            {
                Frm_TJInfo("当前不能执行此操作", "请先选择一个要进行大体采集的病理号！");
            }
        }


        private IWaveIn waveIn;
        private WaveFileWriter writer;
        private string AudioFileName;
        private string AudioFolderPath;

        private void Cleanup()
        {
            if (waveIn != null)
            {
                waveIn.Dispose();
                waveIn = null;
            }
            FinalizeWaveFile();
        }

        private void FinalizeWaveFile()
        {
            if (writer != null)
            {
                writer.Dispose();
                writer = null;
            }
        }
        private void CreateWaveInDevice()
        {
            waveIn = new WaveIn();
            waveIn.WaveFormat = new WaveFormat(8000, 1);

            waveIn.DataAvailable += OnDataAvailable;
            waveIn.RecordingStopped += OnRecordingStopped;
        }
        void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.InvokeRequired)
            {

                this.BeginInvoke(new EventHandler<WaveInEventArgs>(OnDataAvailable), sender, e);
            }
            else
            {

                writer.Write(e.Buffer, 0, e.BytesRecorded);
                int secondsRecorded = (int)(writer.Length / writer.WaveFormat.AverageBytesPerSecond);

                AudioTimeProgress.Text = string.Format("已录音:{0}秒", secondsRecorded.ToString());
            }
        }



        void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<StoppedEventArgs>(OnRecordingStopped), sender, e);
            }
            else
            {
                FinalizeWaveFile();
                AudioTimeProgress.Text = "";
                if (e.Exception != null)
                {
                    Frm_TJInfo("录音错误", String.Format("录音遇到一个问题： {0}", e.Exception.Message));
                }
                //加载
                ListViewItem lvi = new ListViewItem();
                lvi.Text = "录音";
                lvi.SubItems.Add(AudioFileName);
                lvi.SubItems.Add(AudioFolderPath);
                ListViewItem insItem = this.listViewEx1.Items.Add(lvi);
                insItem.Selected = true;
                SetControlStates(false);
            }
        }


        //开始录音
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txt_blh.Text.Trim().Equals(""))
                {
                    Frm_TJInfo("当前不能执行此操作", "请先选择一个要进行录音的病理号！");
                }
                else
                {

                    Cleanup();
                    if (waveIn == null)
                    {
                        CreateWaveInDevice();
                    }

                    //录音路径
                    AudioFolderPath = Path.Combine(Program.APPdirPath, @"Pis_Audio\" + txt_blh.Text.Trim());
                    if (Directory.Exists(AudioFolderPath) == false)
                    {
                        Directory.CreateDirectory(AudioFolderPath);
                    }

                    AudioFileName = String.Format("Audio_{0}.wav", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                    writer = new WaveFileWriter(Path.Combine(AudioFolderPath, AudioFileName), waveIn.WaveFormat);
                    waveIn.StartRecording();
                    SetControlStates(true);

                }
            }
            catch (Exception ex)
            {
                Frm_TJInfo("录音错误", ex.ToString());
            }
        }
        private void SetControlStates(bool isRecording)
        {
            toolStripButton1.Enabled = !isRecording;
            toolStripButton2.Enabled = isRecording;
        }
        void StopRecording()
        {
            //Debug.WriteLine("StopRecording");
            if (waveIn != null) waveIn.StopRecording();
        }
        //结束录音
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txt_blh.Text.Trim().Equals(""))
                {
                    Frm_TJInfo("当前不能执行此操作", "请先选择一个病理号！");
                }
                else
                {
                    StopRecording();
                }
            }
            catch (Exception ex)
            {
                Frm_TJInfo("停止录音错误", ex.ToString());
            }
        }
        //播放
        private void toolStripButton4_Click(object sender, EventArgs e)
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

            if (!txt_blh.Text.Trim().Equals(""))
            {
                listViewEx1.Items.Clear();
                //音频
                string OutputFolder = Path.Combine(Program.APPdirPath, @"Pis_Audio\" + txt_blh.Text.Trim());
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
                OutputFolder = Path.Combine(Program.APPdirPath, @"Pis_Video\" + txt_blh.Text.Trim());
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
                //图像
                OutputFolder = Path.Combine(Program.APPdirPath, @"Pis_Image\" + txt_blh.Text.Trim());
                if (Directory.Exists(OutputFolder) == true)
                {

                    imageListView1.Items.Clear();
                    imageListView1.SuspendLayout();
                    foreach (var file in Directory.GetFiles(OutputFolder, "*.jpg"))
                    {
                        ImageListViewItem ins = new ImageListViewItem();
                        ins.Tag = file;
                        string filePath = file;
                        if (PIS_Sys.Properties.Settings.Default.EditPicName)
                        {
                            string filename = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                            DBHelper.BLL.multi_media_info insInfo = new DBHelper.BLL.multi_media_info();
                            string result = insInfo.GetMemo_note(txt_blh.Text.Trim(), filename, 1);
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
                        if (toolStripLabel2.Text.Equals(""))
                        {
                            Image img = GetImage(file);
                            toolStripLabel2.Text = string.Format("({0}:{1})", img.Width.ToString(), img.Height.ToString());
                            Application.DoEvents();
                            if (thumsFlag == true)
                            {
                                thumsFlag = false;
                                PIS_Sys.Properties.Settings.Default.DTthumsWidth = img.Width / 10;
                                PIS_Sys.Properties.Settings.Default.DTthumsHeight = img.Height / 10;
                                PIS_Sys.Properties.Settings.Default.Save();
                                imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth, PIS_Sys.Properties.Settings.Default.DTthumsHeight);
                            }
                        }
                    }
                    imageListView1.ResumeLayout();
                }

            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            imageListView1.ScrollBars = !imageListView1.ScrollBars;
        }

        private void x96ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth, PIS_Sys.Properties.Settings.Default.DTthumsHeight);
        }

        private void x120ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth * 2, PIS_Sys.Properties.Settings.Default.DTthumsHeight * 2);
        }

        private void x200ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth * 3, PIS_Sys.Properties.Settings.Default.DTthumsHeight * 3);
        }

        private void 重置缩略图大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSetThums ins = new FrmSetThums();
            ins.BringToFront();
            ins.Owner = this;
            if (ins.ShowDialog() == DialogResult.OK)
            {
                PIS_Sys.Properties.Settings.Default.DTthumsWidth = ins.ThWidth;
                PIS_Sys.Properties.Settings.Default.DTthumsHeight = ins.ThHeigth;
                PIS_Sys.Properties.Settings.Default.Save();
                imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth, PIS_Sys.Properties.Settings.Default.DTthumsHeight);
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
        //编辑图片
        private void imageListView1_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            //if (System.IO.File.Exists(e.Item.FileName))
            //{
            //    AnnotateImage(e.Item.FileName, e.Item.FilePath);
            //}
            if (PIS_Sys.Properties.Settings.Default.EditPicName)
            {
                string filePath = e.Item.FileName;
                string filename = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                Frm_Memo insFrm = new Frm_Memo();
                if (insFrm.ShowDialog() == DialogResult.OK)
                {
                    DBHelper.BLL.multi_media_info ins = new DBHelper.BLL.multi_media_info();
                    if (ins.UpdateMemo_note(txt_blh.Text.Trim(), filename, 1, insFrm.Memo_note) == 1)
                    {
                        e.Item.Text = insFrm.Memo_note;
                    }
                }
            }
        }
        public static Image LoadImage(string filePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(filePath) && IsImageFile(filePath) && File.Exists(filePath))
                {
                    return Image.FromStream(new MemoryStream(File.ReadAllBytes(filePath)));
                }
            }
            catch
            {

            }

            return null;
        }
        public static bool IsImageFile(string filePath)
        {
            return IsValidFile(filePath, typeof(ImageFileExtensions));
        }
        private static bool IsValidFile(string filePath, Type enumType)
        {
            string ext = GetFilenameExtension(filePath);

            if (!string.IsNullOrEmpty(ext))
            {
                return Enum.GetNames(enumType).Any(x => ext.Equals(x, StringComparison.InvariantCultureIgnoreCase));
            }

            return false;
        }
        public static string GetFilenameExtension(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                int pos = filePath.LastIndexOf('.');

                if (pos >= 0)
                {
                    return filePath.Substring(pos + 1).ToLowerInvariant();
                }
            }

            return null;
        }

        public static void AnnotateImage(string imgPath, string pathImage)
        {
            if (!IniConfig.isInitialized)
            {
                IniConfig.AllowSave = true;
                IniConfig.Init(@pathImage);
            }

            using (Image cloneImage = LoadImage(imgPath))
            using (ICapture capture = new Capture { Image = cloneImage })
            using (Surface surface = new Surface(capture))
            using (ImageEditorForm editor = new ImageEditorForm(surface, true))
            {
                editor.IsTaskWork = false;
                editor.SetImagePath(imgPath);
                DialogResult result = editor.ShowDialog();

                if (result == DialogResult.OK && editor.IsTaskWork)
                {

                }
            }
        }


        private void FrmQcgl_FormClosing(object sender, FormClosingEventArgs e)
        {
            //结束下载
            ftpDownload1.SetOver();
        }
        //切片工作单打印
        private void btn_print_Click(object sender, EventArgs e)
        {
            Frm_QcList InsQcgl = new Frm_QcList();
            InsQcgl.Owner = this;
            InsQcgl.BringToFront();
            InsQcgl.ShowDialog();
        }
        //补取列表
        private void btn_BQQuery_Click(object sender, EventArgs e)
        {
            superTabControl2.SelectedTabIndex = 3;
        }

        private void FrmQcgl_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }
        //编辑部位字典
        private void advTree1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (advTree1.SelectedNode != null)
                {
                    添加ToolStripMenuItem.Visible = true;
                    删除ToolStripMenuItem.Visible = true;
                    编辑ToolStripMenuItem.Visible = true;

                    if (advTree1.SelectedNode.Parent == null)
                    {
                        删除ToolStripMenuItem.Visible = false;
                        编辑ToolStripMenuItem.Visible = false;
                        Point p = advTree1.PointToScreen(new Point(e.X, e.Y));
                        this.contextMenuStrip2.Show(p);
                        return;
                    }

                    if (advTree1.SelectedNode.Parent.Parent == null)
                    {
                        Point p = advTree1.PointToScreen(new Point(e.X, e.Y));
                        this.contextMenuStrip2.Show(p);
                    }
                    else
                    {
                        if (advTree1.SelectedNode.TagString != "-1")
                        {
                            添加ToolStripMenuItem.Visible = false;
                            Point p = advTree1.PointToScreen(new Point(e.X, e.Y));
                            this.contextMenuStrip2.Show(p);
                        }

                    }
                }
            }
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNode();
        }
        //添加节点
        private void AddNode()
        {
            //添加树形节点
            if (advTree1.SelectedNode != null)
            {
                //----------------------------
                string uid = Helper.GetUidStr();
                //
                string puid = advTree1.SelectedNode.TagString;
                DBHelper.BLL.exam_parts_dict ins = new DBHelper.BLL.exam_parts_dict();
                Boolean zxResult = false;
                if (advTree1.SelectedNode.TagString == "-1")
                {
                    zxResult = ins.InsertParts(uid, "新节点", "0");
                }
                else
                {

                    zxResult = ins.InsertParts(uid, "新节点", puid);
                }
                if (zxResult == true)
                {
                    //----------------------------
                    DevComponents.AdvTree.Node lvm;
                    lvm = new DevComponents.AdvTree.Node();
                    lvm.Tag = uid;
                    lvm.Text = "新节点";
                    advTree1.SelectedNode.Nodes.Add(lvm);
                    advTree1.SelectedNode.Expand();
                    advTree1.SelectedNode = lvm;
                    advTree1.CellEdit = true;
                    advTree1.SelectedNode.BeginEdit();
                }
            }
        }
        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modifyNode();
        }
        private void modifyNode()
        {
            //更新树形节点
            if (advTree1.SelectedNode != null && advTree1.SelectedNode.Parent != null)
            {
                advTree1.CellEdit = true;
                if (!advTree1.SelectedNode.IsEditing)
                {
                    advTree1.SelectedNode.BeginEdit();
                }
            }

        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelNode();
        }
        //删除节点
        public void DelNode()
        {
            //删除树形节点
            if (advTree1.SelectedNode != null && advTree1.SelectedNode.Parent != null)
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;

                if (TaskDialog.Show("询问", "确认", "确定要删除此部位么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    DevComponents.AdvTree.Node curPTreeNode;
                    curPTreeNode = advTree1.SelectedNode.Parent;

                    //遍历删除库
                    delTreeDb(advTree1.SelectedNode.Tag.ToString());
                    //遍历删除节点
                    DelNodeCache(advTree1.SelectedNode);
                }

            }
        }
        public void DelNodeCache(DevComponents.AdvTree.Node node)
        {
            if (node.Nodes.Count > 0)
            {
                for (int i = node.Nodes.Count - 1; i >= 0; i--)
                {
                    DelNodeCache(node.Nodes[i]);
                }
            }
            node.Remove();
        }

        private void delTreeDb(string id)
        {
            //删库
            DBHelper.BLL.exam_parts_dict ins = new DBHelper.BLL.exam_parts_dict();
            DataTable dt = ins.GetChildParts(id);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    delTreeDb(dt.Rows[i][0].ToString());
                }
            }
            ins.DelParts(id);
        }

        private void advTree1_AfterCellEditComplete(object sender, CellEditEventArgs e)
        {
            if (e.Cell != null)
            {
                if (e.NewText.Length > 0)
                {

                    advTree1.SelectedNode.EndEdit(false);
                    //更新标题
                    DBHelper.BLL.exam_parts_dict ins = new DBHelper.BLL.exam_parts_dict();
                    ins.updatePartText(e.NewText, Convert.ToString(e.Cell.Tag));
                }
                else
                {
                    advTree1.SelectedNode.EndEdit(true);
                    e.Cell.Editable = true;
                    advTree1.SelectedNode.BeginEdit();
                }
            }
        }

        private void lbl_history_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //申请单号
                string exam_no = Row.Cells["exam_no"].Value.ToString();
                //病人id号
                string patient_id = Row.Cells["patient_id"].Value.ToString();
                //病人姓名
                string patient_name = Row.Cells["patient_name"].Value.ToString();
                //取历次检查数目
                DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                int total = ins.GetExamCount(exam_no, patient_name);
                if (total > 0)
                {
                    Form Frm_Ins = Application.OpenForms["Frm_History"];
                    if (Frm_Ins != null)
                    {
                        Frm_Ins.Close();
                    }
                    Frm_History Frm_HisIns = new Frm_History();
                    Frm_HisIns.patient_name = patient_name;
                    Frm_HisIns.patient_id = patient_id;
                    Frm_HisIns.exam_no = exam_no;
                    Frm_HisIns.BringToFront();
                    Frm_HisIns.Show();
                }
            }
        }
        //添加标本
        private void buttonX8_Click(object sender, EventArgs e)
        {
            if (this.contextMenuStrip4.Items.Count > 0)
            {
                Control ctrl = sender as Control;
                Point p = this.PointToScreen(new Point(ctrl.Right, ctrl.Top));
                this.contextMenuStrip4.Show(p);
            }
            else
            {
                //添加标本
                添加行ToolStripMenuItem1_Click(null, null);
            }
        }
        //删除标本信息
        private void buttonX6_Click(object sender, EventArgs e)
        {
            if (superGridControl2.PrimaryGrid.Rows.Count > 0)
            {
                int index = superGridControl2.PrimaryGrid.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;
                if (TaskDialog.Show("删除标本信息", "确认", "确定要删除这条标本信息么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    GridRow Row = (GridRow)superGridControl2.PrimaryGrid.Rows[index];
                    //申请单号
                    string id = Row.Cells["id"].Value.ToString();
                    //查询此标本是否已经取材
                    DBHelper.BLL.exam_draw_meterials insM = new DBHelper.BLL.exam_draw_meterials();
                    int count = insM.GetSpecimens_MeterialsCount(Convert.ToInt32(id));
                    if (count == 0)
                    {
                        DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                        ins.DelSpecimens(Convert.ToInt32(id));
                        //清除明细
                        dtQC.Rows.Clear();
                        superGridControl4.PrimaryGrid.InvalidateLayout();
                        //查询标本信息
                        DataTable Spdt = ins.GetQCSpecimensInfo(txt_sqd.Text.Trim());
                        if (Spdt != null)
                        {
                            superGridControl2.PrimaryGrid.DataSource = Spdt;
                            //选中第一行
                            if (Spdt.Rows.Count > 0)
                            {
                                superGridControl2.PrimaryGrid.SetSelectedRows(0, 1, true);
                            }

                        }
                        else
                        {
                            superGridControl2.PrimaryGrid.DataSource = null;
                            //清除明细
                            dtQC.Rows.Clear();
                            superGridControl4.PrimaryGrid.InvalidateLayout();
                        }
                    }
                    else
                    {
                        Frm_TJInfo("不能删除", "此标本已经进行了取材！");
                        return;
                    }
                }
            }
        }

        private void 添加行ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!txt_blh.Text.Trim().Equals(""))
            {
                DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                Boolean ZxResult = ins.InsertSpecimensInfo(txt_sqd.Text.Trim(), "标本采集部位", "", "", (superGridControl2.PrimaryGrid.Rows.Count + 1).ToString(), Program.User_Code, Program.User_Name);
                if (ZxResult)
                {
                    //查询标本信息
                    DataTable Spdt = ins.GetQCSpecimensInfo(txt_sqd.Text.Trim());
                    if (Spdt != null)
                    {
                        superGridControl2.PrimaryGrid.DataSource = Spdt;
                        //选中第一行
                        if (Spdt.Rows.Count > 0)
                        {
                            superGridControl2.PrimaryGrid.SetSelectedRows(0, 1, true);
                        }

                    }
                    else
                    {
                        superGridControl2.PrimaryGrid.DataSource = null;
                    }
                }
            }
            else
            {
                Frm_TJInfo("提示", "请选择一个病人后再添加！");
            }
        }

        private void 删除行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonX6_Click(null, null);
        }
        //大体描述维护
        private void buttonItem14_Click(object sender, EventArgs e)
        {
            Frm_DtmsTemplet Frm_DtmsTempletins = new Frm_DtmsTemplet();
            Frm_DtmsTempletins.BringToFront();
            Frm_DtmsTempletins.ShowDialog();
            BuildDTTree();
        }
        // 关闭先前的视频源
        private void CloseVideoSource()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                // 停止当前视频源
                videoSourcePlayer1.SignalToStop();
                // 等待视频源关闭
                for (int i = 0; (i < 50) && (videoSourcePlayer1.IsRunning); i++)
                {
                    Thread.Sleep(100);
                }
                if (videoSourcePlayer1.IsRunning)
                    videoSourcePlayer1.Stop();
                //videoSourcePlayer1.BorderColor = Color.Black;
                this.Cursor = Cursors.Default;
            }
            catch
            {

            }
        }
        private void OpenVideoSource(IVideoSource source)
        {
            // 设置忙
            this.Cursor = Cursors.WaitCursor;

            // 关闭之前的视频源
            CloseVideoSource();
            //保持视频缩放比例
            if (Program.KeepAspectRatioFlag == 1)
            {
                videoSourcePlayer1.KeepAspectRatio = true;
            }
            // 开始新的视频源
            videoSourcePlayer1.VideoSource = new AsyncVideoSource(source);
            videoSourcePlayer1.Start();
            videoSource = source;
            this.Cursor = Cursors.Default;
        }
        //连接视频源
        private void buttonItem15_Click(object sender, EventArgs e)
        {
            if (btn_PZ.Enabled == false)
            {
                if (devicesCombo.Enabled == false)
                {
                    return;
                }
                try
                {
                    device = videoDevices[devicesCombo.SelectedIndex].MonikerString;
                    //创建视频源
                    VideoCaptureDevice videoSource = new VideoCaptureDevice(device);
                    OpenVideoSource(videoSource);
                    btn_closecj.Enabled = true;
                    btn_PZ.Enabled = true;
                }
                catch
                {

                }
            }
        }
        //相机属性
        private void buttonItem16_Click(object sender, EventArgs e)
        {
            if ((videoSource != null) && (videoSource is VideoCaptureDevice))
            {
                try
                {
                    ((VideoCaptureDevice)videoSource).DisplayPropertyPage(this.Handle);
                }
                catch (NotSupportedException ex)
                {
                    Frm_TJInfo("错误", ex.Message);
                }
            }
        }
        public static string FtpIp = Program.FtpIP;
        public static int FtpPort = Program.FtpPort;
        public static string FtpUser = Program.FtpUser;
        public static string FtpPwd = Program.FtpPwd;
        private void btn_PZ_Click(object sender, EventArgs e)
        {
            try
            {
                if (videoSourcePlayer1.IsRunning)
                {
                    //1.拍照
                    Bitmap bmps1 = videoSourcePlayer1.GetCurrentVideoFrame();
                    if (bmps1 != null)
                    {
                        if (thumsFlag)
                        {
                            thumsFlag = false;
                            PIS_Sys.Properties.Settings.Default.DTthumsWidth = bmps1.Width / 10;
                            PIS_Sys.Properties.Settings.Default.DTthumsHeight = bmps1.Height / 10;
                            PIS_Sys.Properties.Settings.Default.Save();
                            imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth, PIS_Sys.Properties.Settings.Default.DTthumsHeight);
                        }
                        //只有当有病理号时，才可以进行上传及保存和展示
                        if (!txt_blh.Text.Trim().Equals(""))
                        {
                            string PathImg = ImgFolder + @"\" + txt_blh.Text.Trim();
                            if (Directory.Exists(PathImg) == false)
                            {
                                Directory.CreateDirectory(PathImg);
                            }
                            string filename = string.Format("DT_{0}", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                            string strFileName = string.Format(@"{0}\{1}.jpg", PathImg, filename);
                            string remoteFile = string.Format(@"{0}/{1}/{2}/{3}/{4}.jpg", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, txt_blh.Text.Trim(), filename);
                            bmps1.Save(strFileName);
                            //2.上传 VBProcess.ftpUpload(strFileName, remoteFile,"127.0.0.1", "21", "peerct", "125353Ct")
                            if (FtpUpload(@remoteFile, strFileName))
                            {
                                DBHelper.BLL.multi_media_info insMulti = new DBHelper.BLL.multi_media_info();
                                if (insMulti.InsertData(txt_blh.Text.Trim(), remoteFile, string.Format("{0}.jpg", filename), 1) == 1)
                                {
                                    //3.展示
                                    ImageListViewItem ins = new ImageListViewItem();
                                    ins.Tag = strFileName;
                                    ins.Text = string.Format("QC_{0}", imageListView1.Items.Count.ToString());
                                    if (toolStripLabel2.Text.Equals(""))
                                    {
                                        toolStripLabel2.Text = string.Format("({0}:{1})", bmps1.Width.ToString(), bmps1.Height.ToString());
                                        Application.DoEvents();
                                    }
                                    ins.FileName = strFileName;
                                    imageListView1.Items.Insert(0, ins);
                                }
                            }
                        }
                        else
                        {
                            Frm_TJInfo("提示", "病理号不能为空\n请选择一个病人！");
                        }
                    }
                    if (bmps1 != null)
                    {
                        bmps1.Dispose();
                    }
                }

            }
            catch
            {
            }

        }
        //上传文件
        public static Boolean FtpUpload(string ftpPath, string localFile)
        {
            //检查目录是否存在，不存在创建
            FtpCheckDirectoryExist(ftpPath);
            FileInfo fi = new FileInfo(localFile);
            FileStream fs = fi.OpenRead();
            long length = fs.Length;
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(string.Format(@"ftp://{0}:{1}/", FtpIp, FtpPort) + ftpPath);
            req.Credentials = new NetworkCredential(FtpUser, FtpPwd);
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.ContentLength = length;
            req.Timeout = 10 * 1000;
            try
            {
                Stream stream = req.GetRequestStream();
                int BufferLength = 2048; //2K   
                byte[] b = new byte[BufferLength];
                int i;
                while ((i = fs.Read(b, 0, BufferLength)) > 0)
                {
                    stream.Write(b, 0, i);
                }
                stream.Close();
                stream.Dispose();
            }
            catch
            {
                return false;
            }
            finally
            {
                fs.Close();
                req.Abort();
            }
            req.Abort();
            return true;
        }

        //判断文件的目录是否存,不存则创建
        public static void FtpCheckDirectoryExist(string destFilePath)
        {
            string fullDir = FtpParseDirectory(destFilePath);
            string[] dirs = fullDir.Split('/');
            string curDir = "/";
            for (int i = 0; i < dirs.Length; i++)
            {
                string dir = dirs[i];
                //如果是以/开始的路径,第一个为空  
                if (dir != null && dir.Length > 0)
                {
                    try
                    {
                        curDir += dir + "/";
                        FtpMakeDir(curDir);
                    }
                    catch (Exception)
                    { }
                }
            }
        }

        public static string FtpParseDirectory(string destFilePath)
        {
            return destFilePath.Substring(0, destFilePath.LastIndexOf("/"));
        }

        //创建目录
        public static Boolean FtpMakeDir(string curDir)
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create(string.Format(@"ftp://{0}:{1}/", FtpIp, FtpPort) + curDir);
            req.Credentials = new NetworkCredential(FtpUser, FtpPwd);
            req.Method = WebRequestMethods.Ftp.MakeDirectory;
            try
            {
                FtpWebResponse response = (FtpWebResponse)req.GetResponse();
                response.Close();
            }
            catch (Exception)
            {
                req.Abort();
                return false;
            }
            req.Abort();
            return true;
        }

        private void btn_closecj_Click(object sender, EventArgs e)
        {
            // 关闭之前的视频源
            CloseVideoSource();
            btn_closecj.Enabled = false;
            btn_PZ.Enabled = false;
        }
        //双击拍照
        private void videoSourcePlayer1_DoubleClick(object sender, EventArgs e)
        {
            //添加标本信息快捷键
            btn_PZ_Click(null, null);
        }
        //注册热键
        HotkeyInfo hotkey_bbxx;
        private void FrmQcgl_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (hotkey_bbxx != null)
            {
                if (hotkey_bbxx.Status == HotkeyStatus.Registered)
                {
                    UnregisterHotkey(hotkey_bbxx);
                }
            }
        }

        private void imageListView1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.Item.Checked == true)
            {
                e.Item.Checked = false;
            }
            else
            {
                e.Item.Checked = true;
            }
        }
        //删除
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (imageListView1.Items.Count > 0)
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;
                if (TaskDialog.Show("删除大体取材照片", "确认", "确定要删除选中这些大体取材照片么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    for (int i = 0; i < imageListView1.Items.Count; i++)
                    {
                        if (imageListView1.Items[i].Checked == true)
                        {
                            string filePath = imageListView1.Items[i].FileName;
                            string filename = filePath.Substring(filePath.LastIndexOf(@"\") + 1);
                            DBHelper.BLL.multi_media_info ins = new DBHelper.BLL.multi_media_info();
                            if (ins.DelData(txt_blh.Text.Trim(), filename, 1) == 1)
                            {
                                if (System.IO.File.Exists(filePath))
                                {
                                    System.IO.File.Delete(filePath);
                                }
                            }
                        }
                    }
                    RefreshMultiList();
                }
            }
        }
        //复制
        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(richTextBoxEx2.SelectedText);
            }
            catch
            {
            }
        }

        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (e.NewValue.Text == "病历信息")
            {
                //superTabControl2.SelectedTabIndex = 0;
            }
            else if (e.NewValue.Text == "取材信息")
            {
                superTabControl2.SelectedTabIndex = 0;
            }
            else if (e.NewValue.Text == "大体照相" || e.NewValue.Text == "大体数码照相")
            {
                superTabControl2.SelectedTabIndex = 2;
            }
        }
        //技术医嘱的执行
        private void buttonX9_Click(object sender, EventArgs e)
        {
            if (superGridControl3.PrimaryGrid.Rows.Count > 0)
            {
                DBHelper.BLL.exam_jsyz ins = new DBHelper.BLL.exam_jsyz();
                SelectedElementCollection col = superGridControl3.PrimaryGrid.GetSelectedRows();
                if (col.Count > 0)
                {
                    for (int i = 0; i < col.Count; i++)
                    {
                        GridRow row = col[i] as GridRow;
                        int id = Convert.ToInt32(row.Cells["id"].Value);
                        //更新状态
                        ins.UpdateJsyzStatus(id, Program.User_Name);
                    }
                    RefreshData();
                }
            }
        }
        private void RefreshData()
        {
            DBHelper.BLL.exam_jsyz ins = new DBHelper.BLL.exam_jsyz();
            DataTable dt = ins.GetQcJsyzData();
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl3.PrimaryGrid.DataSource = dt;
            }
            else
            {
                superGridControl3.PrimaryGrid.DataSource = null;
            }
        }

        private void superTabControl2_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (e.NewValue.Text.Equals("技术医嘱"))
            {
                RefreshData();
            }
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
        //已经执行的技术医嘱查询
        private void buttonX10_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (textBoxX1.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and zx_datetime>='{0} 00:00:00' and zx_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            }
            else
            {
                sb.AppendFormat(" and study_no='{0}' ", textBoxX1.Text.Trim());
            }
            DBHelper.BLL.exam_jsyz ins = new DBHelper.BLL.exam_jsyz();
            DataTable dt = ins.GetQcYzxJsyzData(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl5.PrimaryGrid.DataSource = dt;
            }
            else
            {
                superGridControl5.PrimaryGrid.DataSource = null;
                Frm_TJInfo("提示", "没有获取到信息！");
            }
        }

        private void SuperTabC_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (e.NewValue.Text.ToString().Equals("已执行医嘱"))
            {
                buttonX10.PerformClick();
            }
        }
        //
        private void superGridControl3_RowDoubleClick(object sender, GridRowDoubleClickEventArgs e)
        {
            if (superGridControl3.PrimaryGrid.Rows.Count > 0)
            {
                GridRow Row = ((GridRow)e.GridRow);
                //病理号
                string study_no = Row.Cells["study_no"].Value.ToString();
                txt_tj.Text = study_no;
                buttonX4.PerformClick();
                //取历次检查数目
                DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                DataTable dt = ins.GetData(study_no);
                //申请单号
                string exam_no = "";
                //病人id号
                string patient_id = "";
                //病人姓名
                string patient_name = "";
                if (dt != null && dt.Rows.Count == 1)
                {
                    exam_no = dt.Rows[0]["exam_no"].ToString();
                    patient_id = dt.Rows[0]["patient_id"].ToString();
                    patient_name = dt.Rows[0]["patient_name"].ToString();
                    int total = ins.GetExamCount(exam_no, patient_name);
                    lbl_history.Text = string.Format(lcjc_str, total);
                    //数据展示
                    ShowJcInfo(exam_no, patient_id);
                    //基本信息
                    this.patientInfo1.SetPatinfo(patient_id, exam_no);
                    //取材信息
                    cmb_rwly.Text = Row.Cells["work_source"].Value.ToString();
                    this.cmb_ckh.Text = Row.Cells["lk_no"].Value.ToString().Split(new char[] { '-' })[1];
                    txt_qcbw.Text = Row.Cells["parts"].Value.ToString();
                    txt_zzs.Text = Row.Cells["group_num"].Value.ToString();
                }
            }
        }

        private void buttonItem17_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.splitContainer2.Panel1Collapsed)
                {
                    splitContainer2.Panel1Collapsed = false;
                }
                else
                {
                    splitContainer2.Panel1Collapsed = true;
                }
            }
            catch (Exception ex)
            {
                Program.FileLog.Error(ex.ToString());
            }
        }
        //电子病历
        private void buttonX11_Click(object sender, EventArgs e)
        {
            if (!txt_sqd.Text.Trim().Equals(""))
            {
                DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                DataTable dt = ins.GetDt(string.Format("select exam_no,new_flag,visit_id from exam_master where exam_no='{0}'", txt_sqd.Text.Trim()));
                if (dt != null && dt.Rows.Count == 1)
                {
                    if (dt.Rows[0]["new_flag"].ToString().Equals("0"))
                    {
                        if (dt.Rows[0]["visit_id"].ToString().Length > 3)
                        {
                            Process ps = new Process();
                            string EMrURL = string.Format(Program.EmrUrlStr, dt.Rows[0]["visit_id"].ToString());
                            ps.StartInfo.FileName = "iexplore.exe";
                            ps.StartInfo.Arguments = EMrURL;
                            ps.Start();
                        }
                        else
                        {
                            Frm_TJInfo("提示", "此病人无电子病历信息！");
                        }
                    }
                    else
                    {
                        Frm_TJInfo("提示", "手工登记病人不能查看！");
                    }
                }
            }
        }
        //数字摄像头关闭事件
        private void FrmQcgl_VisibleChanged(object sender, EventArgs e)
        {
            //if (!this.Visible)
            //{
            //        btn_closecj_Click(null, null);
            //}
        }
        //更新脱水上机时间
        private void buttonX12_Click(object sender, EventArgs e)
        {
            DBHelper.BLL.exam_master Ins = new DBHelper.BLL.exam_master();
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                int flag = 0;
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count; i++)
                {
                    GridRow Row = superGridControl1.PrimaryGrid.Rows[i] as GridRow;
                    if (Row.Cells["exam_status"].Value.ToString() == "25")
                    {
                        string study_no = Row.Cells["study_no"].Value.ToString();
                        flag = Ins.UpdateLkTs_DateTime(study_no);
                    }
                }
                Frm_TJInfo("提示", "上机脱水时间更新成功！");
            }
        }
        //取材归档底单打印
        private void buttonItem18_Click(object sender, EventArgs e)
        {
            DBHelper.BLL.exam_specimens InsSpec = new DBHelper.BLL.exam_specimens();
            DataTable dtSpec = InsSpec.GetBGSpecimensDTQC(txt_sqd.Text.Trim());
            string strRysj = "";
            if (dtSpec != null)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i <= dtSpec.Rows.Count - 1; i++)
                {
                    sb.AppendFormat("{0}", dtSpec.Rows[i]["see_memo"].ToString());
                    if (i != dtSpec.Rows.Count - 1)
                    {
                        sb.AppendLine();
                    }
                }
                strRysj = sb.ToString();
                sb.Clear();
                sb = null;
            }
            if (FastReportLib.PrintGddd.DirectPrintQcGddd(txt_blh.Text.Trim(), strRysj, cmb_qcys.Text.ToString(), cmb_lrr.Text.ToString(), PIS_Sys.Properties.Settings.Default.qcListPrinter))
            {
                Frm_TJInfo("提示", txt_blh.Text.Trim() + "取材归档底单打印完毕！");
            }
        }
        Color CurBtnColor;
        //不规范固定
        private void buttonX13_Click(object sender, EventArgs e)
        {
            if (superGridControl2.PrimaryGrid.Rows.Count > 0)
            {
                DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                int result = ins.UpdateBbGfh(BBid, 0);
                Frm_TJInfo("提示", "标本不规范化固定设置成功！");
                buttonX13.TextColor = Color.Blue;
                buttonX14.TextColor = CurBtnColor;
            }
        }

        private void buttonX14_Click(object sender, EventArgs e)
        {
            if (superGridControl2.PrimaryGrid.Rows.Count > 0)
            {
                DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                int result = ins.UpdateBbGfh(BBid, 1);
                Frm_TJInfo("提示", "标本规范化固定设置成功！");
                buttonX14.TextColor = Color.Blue;
                buttonX13.TextColor = CurBtnColor;
            }
        }
        //启动第三方程序
        private void buttonItem19_Click(object sender, EventArgs e)
        {
            if (System.Diagnostics.Process.GetProcessesByName("PowerVCap").Length == 0)
            {
                superTabControl2.SelectedTab = superTabItem5;
                string ConPathImg = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "RemotePhoto");
                if (Directory.Exists(ConPathImg) == false)
                {
                    Directory.CreateDirectory(ConPathImg);
                }
                ///实例化一个进程
                Process process = new Process();
                ///设置进程的应用程序
                process.StartInfo.FileName = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"RemotePhoto\PowerVCap.exe";
                if (!File.Exists(process.StartInfo.FileName))
                {
                    return;
                }
                if (timer2.Enabled == false)
                {
                    timer2.Enabled = true;
                }
                ///启动指定的进程资源
                process.Start();
            }
            else
            {
                Frm_TJInfo("提示", "数码照相已开启！");
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;

            try
            {
                string ConPathImg = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory.ToString(), "RemotePhoto");
                if (Directory.Exists(ConPathImg) == false)
                {
                    Directory.CreateDirectory(ConPathImg);
                }

                //照相目录
                if (Directory.Exists(ConPathImg) == true)
                {
                    DirectoryInfo dir = new DirectoryInfo(ConPathImg);
                    foreach (FileInfo fi in dir.GetFiles("*.jpg", SearchOption.AllDirectories))
                    {
                        //1.读取
                        FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read);
                        Bitmap bmps1 = new Bitmap(System.Drawing.Image.FromStream(fs));
                        fs.Close();
                        if (bmps1 != null)
                        {
                            if (thumsFlag)
                            {
                                thumsFlag = false;
                                PIS_Sys.Properties.Settings.Default.DTthumsWidth = bmps1.Width / 10;
                                PIS_Sys.Properties.Settings.Default.DTthumsHeight = bmps1.Height / 10;
                                PIS_Sys.Properties.Settings.Default.Save();
                                imageListView1.ThumbnailSize = new Size(PIS_Sys.Properties.Settings.Default.DTthumsWidth, PIS_Sys.Properties.Settings.Default.DTthumsHeight);
                            }
                            //只有当有病理号时，才可以进行上传及保存和展示
                            if (!txt_blh.Text.Trim().Equals(""))
                            {
                                string PathImg = ImgFolder + @"\" + txt_blh.Text.Trim();
                                if (Directory.Exists(PathImg) == false)
                                {
                                    Directory.CreateDirectory(PathImg);
                                }
                                string filename = string.Format("DT_{0}", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                                string strFileName = string.Format(@"{0}\{1}.jpg", PathImg, filename);
                                string remoteFile = string.Format(@"{0}/{1}/{2}/{3}/{4}.jpg", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, txt_blh.Text.Trim(), filename);
                                bmps1.Save(strFileName);
                                //2.上传 VBProcess.ftpUpload(strFileName, remoteFile,"127.0.0.1", "21", "peerct", "125353Ct")
                                if (FtpUpload(@remoteFile, strFileName))
                                {
                                    DBHelper.BLL.multi_media_info insMulti = new DBHelper.BLL.multi_media_info();
                                    if (insMulti.InsertData(txt_blh.Text.Trim(), remoteFile, string.Format("{0}.jpg", filename), 1) == 1)
                                    {
                                        //3.展示
                                        ImageListViewItem ins = new ImageListViewItem();
                                        ins.Tag = strFileName;
                                        ins.Text = string.Format("QC_{0}", imageListView1.Items.Count.ToString());
                                        if (toolStripLabel2.Text.Equals(""))
                                        {
                                            toolStripLabel2.Text = string.Format("({0}:{1})", bmps1.Width.ToString(), bmps1.Height.ToString());
                                            Application.DoEvents();
                                        }
                                        ins.FileName = strFileName;
                                        imageListView1.Items.Insert(0, ins);
                                    }
                                }
                            }
                            else
                            {
                                Frm_TJInfo("提示", "病理号不能为空\n请选择一个病人！");
                            }
                        }
                        if (bmps1 != null)
                        {
                            bmps1.Dispose();
                        }

                        //删除
                        fi.Delete();
                    }
                }

            }
            catch (Exception ex)
            {
                Program.FileLog.Error("定时取相机图异常：" + ex.Message);
            }
            finally
            {
                timer2.Enabled = true;
            }
        }
    }

    //先定义一个combox类
    internal class FragrantComboBox : GridComboBoxExEditControl
    {
        public FragrantComboBox(IEnumerable orderArray)
        {
            DataSource = orderArray;
            AutoCompleteMode = AutoCompleteMode.Suggest;
        }
    }
}
