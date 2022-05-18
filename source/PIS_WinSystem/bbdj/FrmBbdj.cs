using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar.SuperGrid.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace PIS_Sys.bbdj
{
    //实现标本登记、新建登记、修改登记
    public partial class FrmBbdj : DevComponents.DotNetBar.Office2007Form
    {
        Dictionary<string, Color> exam_status_dic = new Dictionary<string, Color>();
        Dictionary<string, Color> exam_status_name_dic = new Dictionary<string, Color>();
        Dictionary<string, string> examStatus_dic = new Dictionary<string, string>();
        Color CurBtnColor;
        public FrmBbdj()
        {
            InitializeComponent();
            //热键操作
            HotkeyRepeatLimit = 1000;
            repeatLimitTimer = Stopwatch.StartNew();
            //修改日期格式
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";
            //
            //HandleCreated += frmBBdj_HandleCreated;
            //this.HotkeyPress += hotkeyForm_HotkeyPress;
        }
        #region"快捷键操作"
        public bool IgnoreHotkeys = false;
        private void hotkeyForm_HotkeyPress(ushort id, Keys key, Modifiers modifier)
        {
            if (!IgnoreHotkeys)
            {
                if (this.Tag.Equals("show"))
                {
                    switch (key)
                    {

                        case Keys.F2:
                            //添加清空界面数据快捷键
                            clearUIData();
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        //清空当前信息
        private void clearUIData()
        {
            ClearData();
            //设置光标位置
            switch (PIS_Sys.Properties.Settings.Default.Bb_Reg_Curor)
            {
                case 0:
                    txt_sm_tj.Focus();
                    break;
                case 1:
                    txt_patName.Focus();
                    break;
                case 2:
                    txt_tj.Focus();
                    break;
                default:
                    txt_tj.Focus();
                    break;
            }
        }

        //注册热键
        HotkeyInfo hotkey_bbxx;
        private void frmBBdj_HandleCreated(object sender, EventArgs e)
        {
            //注册热键
            hotkey_bbxx = new HotkeyInfo(Keys.F2);
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
        //移除热键
        private void FrmBbdj_FormClosed(object sender, FormClosedEventArgs e)
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

        public int calculationDate(DateTime beginDateTime, DateTime endDateTime) { if (beginDateTime > endDateTime) throw new Exception("开始时间应小于或等与结束时间！");          /*计算出生日期到当前日期总月数*/      int Months = endDateTime.Month - beginDateTime.Month + 12 * (endDateTime.Year - beginDateTime.Year);       /*出生日期加总月数后，如果大于当前日期则减一个月*/      int totalMonth = (beginDateTime.AddMonths(Months) > endDateTime) ? Months - 1 : Months;       /*计算整年*/      int fullYear = totalMonth / 12;       /*计算整月*/      int fullMonth = totalMonth % 12;       /*计算天数*/      DateTime changeDate = beginDateTime.AddMonths(totalMonth); double days = (endDateTime - changeDate).TotalDays; return fullYear; }

        private void FrmBbdj_Load(object sender, EventArgs e)
        {

            //原始颜色
            CurBtnColor = buttonX3.TextColor;
            //送检单位
            DBHelper.BLL.doctor_dict sjdw_ins = new DBHelper.BLL.doctor_dict();
            DataTable dt = sjdw_ins.GetSjdwList();
            if (dt != null && dt.Rows.Count > 0)
            {
                this.cmb_sjdw.DataSource = dt;
                cmb_sjdw.DisplayMember = "name";
                cmb_sjdw.ValueMember = "id";
            }

            //加载检查状态
            DBHelper.BLL.exam_status exam_status_ins = new DBHelper.BLL.exam_status();
            List<DBHelper.Model.exam_status> lst = exam_status_ins.GetDJModelExam_Status();
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
            //检查类型
            radioButton1_CheckedChanged(null, null);
            //民族  
            DBHelper.BLL.exam_nation exam_nation_ins = new DBHelper.BLL.exam_nation();
            DataSet ds = exam_nation_ins.GetDsExam_Nation();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                cmb_nation.DataSource = ds.Tables[0];
                cmb_nation.DisplayMember = "nation_name";
                cmb_nation.ValueMember = "id";
            }
            //申请科室 
            DBHelper.BLL.dept_dict dept_dict_ins = new DBHelper.BLL.dept_dict();
            DataSet dsDept = dept_dict_ins.GetDsExam_Dept();
            if (dsDept != null && dsDept.Tables[0].Rows.Count > 0)
            {
                this.cmb_sqks.DataSource = dsDept.Tables[0];
                cmb_sqks.DisplayMember = "dept_name";
                cmb_sqks.ValueMember = "dept_code";
            }

            //病人来源 
            DBHelper.BLL.patient_source patient_source_ins = new DBHelper.BLL.patient_source();
            List<string> lst_source = patient_source_ins.GetPatient_source();
            for (int i = 0; i < lst_source.Count; i++)
            {
                cmb_patSource.Items.Add(lst_source[i]);
            }
            lst_source.Clear();

            //默认选择
            cmb_ice.SelectedIndex = 0;
            com_ks.SelectedIndex = 0;
            cmb_ageUnit.SelectedIndex = 0;
            cmb_patSex.SelectedIndex = 0;

            //若启用HIS直连
            if (PIS_Sys.Properties.Settings.Default.His_PatInfo_Flag == true)
            {
                cmb_sm_tj.SelectedIndex = cmb_sm_tj.Items.Count - 1;
            }
            else
            {
                cmb_sm_tj.SelectedIndex = 0;
            }
            cmb_tj.SelectedIndex = 0;
            cmb_patSource.SelectedIndex = 0;
            cmb_ExamStatus.SelectedIndex = 3;
            chK_status.Checked = true;
            //初始化控件
            ClearData();
            //绑定事件处理
            for (int i = 0; i < this.QueryBtn.SubItems.Count; i++)
            {
                ButtonItem BtnItem = ((ButtonItem)this.QueryBtn.SubItems[i]);
                BtnItem.Click += new System.EventHandler(Chk_BtnRq);
                BtnItem.Checked = false;
                if (BtnItem.Text.Equals(PIS_Sys.Properties.Settings.Default.Refresh_Date_FW))
                {
                    BtnItem.Checked = true;
                }
            }
            dt_birth.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //根据年龄计算出生日期
            txt_age.LostFocus += new EventHandler(txt_age_LostFocus);


            //列表样式
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel2 = superGridControl1.PrimaryGrid;
            panel2.Name = "exam_master_view";
            panel2.MinRowHeight = 30;
            //锁定1列
            panel2.FrozenColumnCount = 1;
            panel2.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ReadOnly = true;
                panel2.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //动态生成检查部位右键菜单 contextMenuStrip2
            this.MenuItemClick += new MenuEventHandler(bbxx_MenuItemClick);
            CreatePopUpJcbwMenu();
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
                        k = this.contextMenuStrip2.Items.Add(new System.Windows.Forms.ToolStripMenuItem());
                        string tmp = dt.Rows[i]["id"].ToString();
                        if (DictionaryValues.ContainsKey(tmp) == false)
                        {
                            DictionaryValues.Add(tmp, k);
                        }
                        this.contextMenuStrip2.Items[k].Text = dt.Rows[i]["part_name"].ToString();
                        //添加父菜单的事件
                        (this.contextMenuStrip2.Items[k] as System.Windows.Forms.ToolStripMenuItem).Click += new EventHandler(ActiveEvent);
                    }
                    else
                    {
                        //获取父菜单的索引
                        if (DictionaryValues.TryGetValue(dt.Rows[i]["parent_code"].ToString(), out j))
                        {
                            //添加子菜单到父菜单中
                            m = (this.contextMenuStrip2.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems.Add(new System.Windows.Forms.ToolStripMenuItem());
                            (this.contextMenuStrip2.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Text = dt.Rows[i]["part_name"].ToString();
                            //记录菜单编码和菜单编号
                            DictionaryValues.Add(dt.Rows[i]["id"].ToString(), m);
                            //添加子菜单的事件
                            (this.contextMenuStrip2.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Click += new EventHandler(ActiveEvent);
                        }
                        else
                        {
                            //如果当前子菜单的父菜单不存在，则创建父菜单后再创建子菜单
                            k = this.contextMenuStrip2.Items.Add(new System.Windows.Forms.ToolStripMenuItem());
                            string tmp = dt.Rows[i]["parent_code"].ToString();
                            if (DictionaryValues.ContainsKey(tmp) == false)
                            {
                                DictionaryValues.Add(tmp, k);
                            }
                            this.contextMenuStrip2.Items[k].Text = Exam_part_Ins.GetExam_parts_Name(tmp);
                            //添加子菜单到父菜单中
                            m = (this.contextMenuStrip2.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems.Add(new System.Windows.Forms.ToolStripMenuItem());
                            (this.contextMenuStrip2.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Text = dt.Rows[i]["part_name"].ToString();
                            //记录菜单编码和菜单编号
                            DictionaryValues.Add(dt.Rows[i]["id"].ToString(), m);
                            //添加子菜单的事件
                            (this.contextMenuStrip2.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Click += new EventHandler(ActiveEvent);
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
        private void bbxx_MenuItemClick(string BBBW)
        {
            txt_sjbb.Text = BBBW;
        }
        //自动计算出生年份
        private void txt_age_LostFocus(object sender, EventArgs e)
        {
            if (txt_age.Text.Trim() == "" || Microsoft.VisualBasic.Information.IsNumeric(txt_age.Text.Trim()) == false)
            {
                return;
            }
            if (dt_birth.Value.Year == DateTime.Now.Year)
            {
                dt_birth.Value = Convert.ToDateTime(DateTime.Now.Date.AddYears(0 - Convert.ToInt32(txt_age.Text.Trim())).ToString("yyyy-MM-dd"));
            }
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

        private void QueryBtn_Click(object sender, EventArgs e)
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
            _DataSet = exam_mas_Ins.GetDJDsExam_master(dt_tj, PIS_Sys.Properties.Settings.Default.DjType, zt_tj, Program.workstation_type_db);
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
            //设置光标位置
            switch (PIS_Sys.Properties.Settings.Default.Bb_Reg_Curor)
            {
                case 0:
                    txt_sm_tj.Focus();
                    break;
                case 1:
                    txt_patName.Focus();
                    break;
                case 2:
                    txt_tj.Focus();
                    break;
                default:
                    txt_tj.Focus();
                    break;
            }
        }

        private void FrmBbdj_Activated(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;

            //开启焦点并刷新数据
            if (timer1.Enabled == false)
            {
                timer1.Enabled = true;
            }

        }
        //写当前日期为申请日期
        private void buttonX4_Click(object sender, EventArgs e)
        {
            txt_Sqrq.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        //清除当前条件值
        private void buttonX2_Click(object sender, EventArgs e)
        {
            txt_tj.Text = "";
            txt_tj.Focus();
        }
        //检查类型变换
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                DBHelper.BLL.exam_type exam_type_ins = new DBHelper.BLL.exam_type();
                DataSet ds = exam_type_ins.GetDsExam_Type(0, PIS_Sys.Properties.Settings.Default.DjType, Program.workstation_type_db);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    cmbExam_type.DataSource = ds.Tables[0];
                    cmbExam_type.DisplayMember = "modality_cn";
                    cmbExam_type.ValueMember = "modality";
                }
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                DBHelper.BLL.exam_type exam_type_ins = new DBHelper.BLL.exam_type();
                DataSet ds = exam_type_ins.GetDsExam_Type(1, PIS_Sys.Properties.Settings.Default.DjType, Program.workstation_type_db);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    cmbExam_type.DataSource = ds.Tables[0];
                    cmbExam_type.DisplayMember = "modality_cn";
                    cmbExam_type.ValueMember = "modality";
                }
            }
        }
        //根据科室检索医生
        private void cmb_sqks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_sqks.Text != "")
            {
                DBHelper.BLL.doctor_dict doctor_dict_ins = new DBHelper.BLL.doctor_dict();
                DataSet ds = doctor_dict_ins.GetDsExam_Dcotor(cmb_sqks.SelectedValue.ToString());
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    this.cmb_sqys.DataSource = ds.Tables[0];
                    cmb_sqys.DisplayMember = "doctor_name";
                    cmb_sqys.ValueMember = "doctor_code";
                }
                else
                {
                    this.cmb_sqys.DataSource = null;
                }
            }
        }
        //保存登记标本病人基本信息
        public Boolean Save_PatMI(ref string faild_info)
        {
            Boolean ResultDb_Flag = false;
            DBHelper.Model.exam_pat_mi patmi_Ins = new DBHelper.Model.exam_pat_mi();
            patmi_Ins.patient_name = txt_patName.Text.Trim();
            patmi_Ins.name_phonetic = PinYinConverter.Get(patmi_Ins.patient_name);
            patmi_Ins.sex = ((DevComponents.Editors.ComboItem)(cmb_patSex.SelectedItem)).Text.Trim();
            patmi_Ins.date_of_birth = dt_birth.Text.Trim();
            patmi_Ins.nation = cmb_nation.Text.Trim();
            patmi_Ins.hospital_card = txt_Hkh.Text.Trim();
            patmi_Ins.si_card = "";
            patmi_Ins.identity = txt_sfz.Text.Trim();
            patmi_Ins.patient_id = txt_patid.Text.Trim();
            patmi_Ins.phone_number = txt_tel.Text.Trim();
            patmi_Ins.current_place = current_place.Text.Trim();

            DBHelper.BLL.exam_pat_mi Patmi_Dll_ins = new DBHelper.BLL.exam_pat_mi();
            ResultDb_Flag = Patmi_Dll_ins.Process_Patmi(patmi_Ins, ref faild_info);
            if (!ResultDb_Flag)
            {
                Frm_TJInfo("失败", "保存病人信息失败！" + Environment.NewLine + "失败原因：" + faild_info);
                Program.FileLog.Error("保存病人信息失败！\n失败原因：" + faild_info);
            }
            return ResultDb_Flag;
        }

        //保存登记标本申请单信息
        public Boolean Save_ExamMaster(ref string faild_info, int new_flag, ref string cur_Blh, Boolean HB_Flag, string Source_Exam_no)
        {
            Boolean ResultDb_Flag = false;
            //电子申请单描述性
            if (HB_Flag == false)
            {
                DBHelper.Model.exam_requisition exam_req_ins = new DBHelper.Model.exam_requisition();
                exam_req_ins.exam_no = txt_sqd.Text.Trim();
                exam_req_ins.history_note = txt_bszy.Text.Trim();
                exam_req_ins.infectious_note = txt_crb.Text.Trim();
                exam_req_ins.clinical_diag = txt_lczd.Text.Trim();
                exam_req_ins.ops_note = txt_sssj.Text.Trim();
                DBHelper.BLL.exam_requisition exam_B_req_Ins = new DBHelper.BLL.exam_requisition();
                ResultDb_Flag = exam_B_req_Ins.Process_exam_requisition(exam_req_ins, ref faild_info);
                if (!ResultDb_Flag)
                {
                    Frm_TJInfo("失败", "保存申请单描述信息失败！" + Environment.NewLine + "失败原因：" + faild_info);
                    Program.FileLog.Error("保存申请单描述信息失败！\n失败原因：" + faild_info);
                    return ResultDb_Flag;
                }
            }
            //检查
            DBHelper.Model.exam_master examMas_Ins = new DBHelper.Model.exam_master();
            examMas_Ins.age = txt_age.Text.Trim();
            examMas_Ins.ageUint = ((DevComponents.Editors.ComboItem)(cmb_ageUnit.SelectedItem)).Text.Trim();
            examMas_Ins.exam_no = txt_sqd.Text.Trim();
            examMas_Ins.patient_id = txt_patid.Text.Trim();
            examMas_Ins.examItems = txt_items.Text.Trim();
            examMas_Ins.inout_type = (radioButton1.Checked == true ? 0 : 1);
            examMas_Ins.input_id = txt_inputid.Text.Trim();
            examMas_Ins.output_id = "";
            examMas_Ins.patient_source = cmb_patSource.Text.Trim();
            examMas_Ins.new_flag = new_flag;
            examMas_Ins.modality = cmbExam_type.SelectedValue.ToString();
            examMas_Ins.costs = txt_costs.Text.Trim();
            //取大类
            DBHelper.BLL.exam_type insType = new DBHelper.BLL.exam_type();
            string big_type = insType.GetBigType(examMas_Ins.modality);
            examMas_Ins.exam_type = big_type;
            examMas_Ins.bed_no = txt_BedNo.Text.Trim();
            examMas_Ins.ward = "";
            examMas_Ins.wtzd_flag = Chk_Wtzd.Checked == true ? 1 : 0;
            examMas_Ins.req_date_time = (txt_Sqrq.Text.Trim().Equals("") ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : txt_Sqrq.Text.Trim());
            if (cmb_sqks.SelectedIndex != -1)
            {
                examMas_Ins.req_dept = cmb_sqks.Text.ToString();
                examMas_Ins.req_dept_code = cmb_sqks.SelectedValue.ToString();
            }
            else
            {
                examMas_Ins.req_dept = cmb_sqks.Text.ToString();
                examMas_Ins.req_dept_code = "";
            }
            if (cmb_sqys.SelectedIndex != -1)
            {
                examMas_Ins.req_physician = cmb_sqys.Text.ToString();
                examMas_Ins.req_physician_code = cmb_sqys.SelectedValue.ToString();
            }
            else
            {
                examMas_Ins.req_physician = cmb_sqys.Text.ToString();
                examMas_Ins.req_physician_code = "";
            }
            examMas_Ins.submit_unit = cmb_sjdw.Text.Trim();
            examMas_Ins.ice_flag = (((DevComponents.Editors.ComboItem)(cmb_ice.SelectedItem)).Text.Equals("是") ? 1 : 0);
            examMas_Ins.ks_flag = (((DevComponents.Editors.ComboItem)(com_ks.SelectedItem)).Text.Equals("是") ? 1 : 0);
            examMas_Ins.fk_flag = (chk_fkhz.Checked == true ? 1 : 0);
            examMas_Ins.zl_flag = (chK_zlhz.Checked == true ? 1 : 0);
            examMas_Ins.exam_status = "20";
            examMas_Ins.receive_doctor_name = Program.User_Name;
            examMas_Ins.received_doctor_code = Program.User_Code;
            examMas_Ins.received_datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (HB_Flag == true)
            {
                examMas_Ins.merge_exam_no = Source_Exam_no;
            }
            else
            {
                examMas_Ins.merge_exam_no = "";
            }

            if (cur_Blh != "")
            {
                examMas_Ins.study_no = cur_Blh;
            }
            else
            {
                if (examMas_Ins.ice_flag == 0)
                {
                    //生成病理号
                    DBHelper.BLL.sys_sequence sys_sequence_ins = new DBHelper.BLL.sys_sequence();
                    examMas_Ins.study_no = sys_sequence_ins.Create_BLH(examMas_Ins.modality, examMas_Ins.exam_type, examMas_Ins.inout_type);
                    if (examMas_Ins.study_no == "")
                    {
                        faild_info = "生成病理号失败！";
                        return false;
                    }
                    cur_Blh = examMas_Ins.study_no;
                    //查询病理号是否已经存在
                    int count = sys_sequence_ins.CheckBLH_Enable(cur_Blh, examMas_Ins.exam_type);
                    while (count > 0)
                    {
                        examMas_Ins.study_no = sys_sequence_ins.Create_BLH(examMas_Ins.modality, examMas_Ins.exam_type, examMas_Ins.inout_type);
                        cur_Blh = examMas_Ins.study_no;
                        count = sys_sequence_ins.CheckBLH_Enable(cur_Blh, examMas_Ins.exam_type);
                    }
                }
                else
                {
                    if (Program.BD_BHFun)
                    {
                        //生成病理号
                        DBHelper.BLL.sys_sequence sys_sequence_ins = new DBHelper.BLL.sys_sequence();
                        examMas_Ins.study_no = sys_sequence_ins.Create_BDBLH();
                        if (examMas_Ins.study_no == "")
                        {
                            faild_info = "生成病理号失败！";
                            return false;
                        }
                        cur_Blh = examMas_Ins.study_no;
                        //查询病理号是否已经存在
                        int count = sys_sequence_ins.CheckBLH_Enable(cur_Blh, examMas_Ins.exam_type);
                        while (count > 0)
                        {
                            examMas_Ins.study_no = sys_sequence_ins.Create_BDBLH();
                            cur_Blh = examMas_Ins.study_no;
                            count = sys_sequence_ins.CheckBLH_Enable(cur_Blh, examMas_Ins.exam_type);
                        }
                    }
                    else
                    {
                        //生成病理号
                        DBHelper.BLL.sys_sequence sys_sequence_ins = new DBHelper.BLL.sys_sequence();
                        examMas_Ins.study_no = sys_sequence_ins.Create_BLH(examMas_Ins.modality, examMas_Ins.exam_type, examMas_Ins.inout_type);
                        if (examMas_Ins.study_no == "")
                        {
                            faild_info = "生成病理号失败！";
                            return false;
                        }
                        cur_Blh = examMas_Ins.study_no;
                        //查询病理号是否已经存在
                        int count = sys_sequence_ins.CheckBLH_Enable(cur_Blh, examMas_Ins.exam_type);
                        while (count > 0)
                        {
                            examMas_Ins.study_no = sys_sequence_ins.Create_BLH(examMas_Ins.modality, examMas_Ins.exam_type, examMas_Ins.inout_type);
                            cur_Blh = examMas_Ins.study_no;
                            count = sys_sequence_ins.CheckBLH_Enable(cur_Blh, examMas_Ins.exam_type);
                        }
                    }
                }
            }
            DBHelper.BLL.exam_master exam_Bll_masterIns = new DBHelper.BLL.exam_master();
            ResultDb_Flag = exam_Bll_masterIns.Process_ExamMaster(examMas_Ins, ref faild_info);
            if (!ResultDb_Flag)
            {
                Frm_TJInfo("失败", "保存申请单信息失败！" + Environment.NewLine + "失败原因：" + faild_info);
            }
            return ResultDb_Flag;
        }
        //保存标本信息
        public Boolean Save_ExamSpecimens(string exam_no, ref string faild_info)
        {
            Boolean ResultDb_Flag = false;
            faild_info = "";
            //插入当前
            if (!txt_sjbb.Text.Trim().Equals(""))
            {
                DBHelper.BLL.exam_specimens speci_Ins = new DBHelper.BLL.exam_specimens();
                ResultDb_Flag = speci_Ins.Process_exam_specimens(txt_sjbb.Text.Trim(), Convert.ToInt32(numericUpDown1.Value), exam_no, Program.User_Code, Program.User_Name, false, ref faild_info);
            }
            //标本是否必须登记
            if (PIS_Sys.Properties.Settings.Default.RecordBBFlag == false)
            {
                if (ResultDb_Flag == false)
                {
                    ResultDb_Flag = true;
                }
            }
            if (!ResultDb_Flag)
            {
                Frm_TJInfo("失败", "保存标本信息失败！" + Environment.NewLine + "失败原因：" + faild_info);
                Program.FileLog.Error("保存标本信息失败！\n失败原因：" + faild_info);
            }
            return ResultDb_Flag;
        }
        //保存肿瘤信息
        public Boolean Save_exam_tumour(ref string faild_info)
        {
            Boolean ResultDb_Flag = false;
            DBHelper.Model.exam_tumour exam_tuIns = new DBHelper.Model.exam_tumour();
            exam_tuIns.exam_no = txt_sqd.Text.Trim();
            exam_tuIns.find_date = dt_zlrq.Value.ToString("yyyy-MM-dd");
            exam_tuIns.memo = txt_zlbz.Text.Trim();
            exam_tuIns.parts = txt_zlbz.Text.Trim();
            exam_tuIns.radiate_flag = ((DevComponents.Editors.ComboItem)(cmb_fszl.SelectedItem)).Text.Trim();
            exam_tuIns.sizes = txt_zldx.Text.Trim();
            exam_tuIns.trans_parts = txt_zybw.Text.Trim();
            exam_tuIns.transfer_flag = ((DevComponents.Editors.ComboItem)(cmb_zlzy.SelectedItem)).Text.Trim();
            exam_tuIns.chemotherapy = ((DevComponents.Editors.ComboItem)(cmb_hl.SelectedItem)).Text.Trim();
            DBHelper.BLL.exam_tumour tum_B_Ins = new DBHelper.BLL.exam_tumour();
            ResultDb_Flag = tum_B_Ins.Process_exam_tumour(exam_tuIns, ref faild_info);
            if (!ResultDb_Flag)
            {
                Frm_TJInfo("失败", "保存肿瘤信息失败！" + Environment.NewLine + "失败原因：" + faild_info);
                Program.FileLog.Error("保存肿瘤信息失败！\n失败原因：" + faild_info);
            }
            return ResultDb_Flag;
        }

        //保存妇科
        public Boolean Save_Exam_obstetric(ref string faild_info)
        {
            Boolean ResultDb_Flag = false;
            DBHelper.Model.exam_obstetric exam_obs_Ins = new DBHelper.Model.exam_obstetric();
            exam_obs_Ins.exam_no = txt_sqd.Text.Trim();
            exam_obs_Ins.pre_date = dt_preyj.Value.ToString("yyyy-MM-dd");
            exam_obs_Ins.last_date = dt_lastyj.Value.ToString("yyyy-MM-dd");
            exam_obs_Ins.memo = txt_yjbz.Text.Trim();
            exam_obs_Ins.ops_date = txt_rgsj.Text.Trim();
            exam_obs_Ins.ops_unit = txt_jl.Text.Trim();
            exam_obs_Ins.production = txt_cnum.Text.Trim();
            exam_obs_Ins.foetus = txt_tnum.Text.Trim();
            exam_obs_Ins.absolute_flag = ((DevComponents.Editors.ComboItem)(cmb_jjflag.SelectedItem)).Text.Trim();
            exam_obs_Ins.ops_flag = ((DevComponents.Editors.ComboItem)(cmb_rgzl.SelectedItem)).Text.Trim();
            DBHelper.BLL.exam_obstetric exam_B_Ins = new DBHelper.BLL.exam_obstetric();
            ResultDb_Flag = exam_B_Ins.Process_exam_obstetric(exam_obs_Ins, ref faild_info);
            if (!ResultDb_Flag)
            {
                Frm_TJInfo("失败", "保存妇科信息失败！" + Environment.NewLine + "失败原因：" + faild_info);
                Program.FileLog.Error("保存妇科信息失败！\n失败原因：" + faild_info);
            }
            return ResultDb_Flag;
        }

        private Boolean check_YouXiao()
        {
            Boolean youxiao_flag = true;
            if (this.txt_patName.Text.Trim().Equals(""))
            {
                Frm_TJInfo("数据验证", "请输入病人姓名!");
                txt_patName.Focus();
                return false;
            }
            if (txt_patid.Text.Trim().Equals(""))
            {
                Frm_TJInfo("数据验证", "病人ID号不能为空!");
                txt_patid.Focus();
                return false;
            }
            //if (this.txt_age.Text.Trim().Equals("") || this.txt_age.Text.Trim().Equals("0"))
            //{
            //    Frm_TJInfo("数据验证", "请输入病人年龄！");
            //    txt_age.Focus();
            //    return false;
            //}
            if (this.chk_fkhz.Checked == true)
            {
                string sex = ((DevComponents.Editors.ComboItem)(cmb_patSex.SelectedItem)).Text.Trim();
                if (!sex.Equals("女"))
                {
                    Frm_TJInfo("数据验证", "当前妇科患者标示选中！\n病人性别错误！！");
                    this.cmb_patSex.Focus();
                    return false;
                }
            }

            if (PIS_Sys.Properties.Settings.Default.RecordBBFlag == true)
            {
                if (txt_sjbb.Text.Trim().Equals(""))
                {
                    Frm_TJInfo("数据验证", "请输入采集标本信息！");
                    txt_sjbb.Focus();
                    return false;
                }
            }
            return youxiao_flag;
        }
        private void NewBtn_Click(object sender, EventArgs e)
        {
            //解决编辑supergrid的bug
            superGridControl1.Select();
            superGridControl1.Focus();

            if (PIS_Sys.Properties.Settings.Default.DjNewForm == true)
            {
                //开启新的窗体来进行新建病人登记
                jcyy.FrmJcyy Frm_New = new jcyy.FrmJcyy();
                Frm_New.Owner = this;
                Frm_New.TopMost = true;
                Frm_New.ShowDialog();
                //刷新列表
                QueryBtn_Click(null, null);
            }
            else
            {
                if (txt_sqd.Text.Trim().Equals("") == false)
                {
                    Frm_TJInfo("这不是新建检查", "请点击保存按钮！");
                    return;
                }
                //是否保存成功
                Boolean save_flag = false;
                //病理号
                string Cur_BLH = "";
                //保存失败原因
                string faild_info = "";
                //有效性验证
                if (!check_YouXiao())
                {
                    return;
                }
                //生成申请单号 病人编号
                DBHelper.BLL.sys_sequence sys_sequence_ins = new DBHelper.BLL.sys_sequence();
                //--------------手动输入病理号------------------------------------
                if (ChkMauBLH.Checked == true)
                {
                    if (txt_blh.Text.Trim() == "")
                    {
                        Frm_TJInfo("当前已选中病理号手动输入", "请输入手动分配给的病理号！");
                        txt_blh.Focus();
                        return;
                    }
                    else
                    {
                        Cur_BLH = txt_blh.Text.Trim().Replace("'", "");
                        //取大类
                        DBHelper.BLL.exam_type insType = new DBHelper.BLL.exam_type();
                        string big_type = insType.GetBigType(cmbExam_type.SelectedValue.ToString());
                        //查询手动输入病理号是否已经存在
                        int count = sys_sequence_ins.CheckBLH_Enable(Cur_BLH, big_type);
                        if (count > 0)
                        {
                            Frm_TJInfo("病理号已经存在", "请重新输入手动分配给的病理号！");
                            txt_blh.Focus();
                            return;
                        }
                    }
                }
                if (this.txt_patid.Text.Trim().Equals(""))
                {
                    string PID = "New" + sys_sequence_ins.GetPID_Sequence();
                    while (PID.Equals("New"))
                    {
                        PID = "New" + sys_sequence_ins.GetPID_Sequence();
                    }
                    txt_patid.Text = PID;
                }

                if (txt_patid.Text.Trim().Equals(""))
                {
                    return;
                }

                if (txt_sqd.Text.Trim().Equals(""))
                {
                    string SQD = sys_sequence_ins.GetSQD_Sequence();
                    while (SQD == string.Empty)
                    {
                        SQD = sys_sequence_ins.GetSQD_Sequence();
                    }
                    //赋值
                    txt_sqd.Text = "N" + SQD + "S";
                }
                else
                {
                    //验证用户输入的申请单是否正确
                    DBHelper.BLL.exam_master exam_Bll_masterIns = new DBHelper.BLL.exam_master();
                    int sl = exam_Bll_masterIns.GetExamNoCount(txt_sqd.Text.Trim());
                    if (sl > 0)
                    {
                        Frm_TJInfo("提示", "病人生成申请单号已经存在。" + Environment.NewLine + "请确认输入是否有误！");
                        txt_sqd.Focus();
                        txt_sqd.SelectionStart = txt_sqd.Text.Length;
                        return;
                    }
                }

                if (txt_sqd.Text.Trim().Equals("") || this.txt_patid.Text.Trim().Equals(""))
                {
                    Frm_TJInfo("系统错误", "新建病人生成申请单号 病人编号失败！" + Environment.NewLine + "请联系系统管理员解决！");
                    Program.FileLog.DebugFormat("\n新建病人生成申请单号 病人编号失败！{0}方法执行错误:{1}!\n", "NewBtn_Click", "数据库中sys_sequence表中必须维护name字段为PID和SQD的记录，请查看数据库表解决此问题！");
                    return;
                }
                //----------------------------------------------------------------------------
                //1.病人基本信息
                save_flag = Save_PatMI(ref faild_info);
                if (!save_flag)
                {
                    return;
                }
                //2.妇科患者
                if (chk_fkhz.Checked == true)
                {
                    save_flag = Save_Exam_obstetric(ref faild_info);
                    if (!save_flag)
                    {
                        return;
                    }
                }
                //3.肿瘤患者
                if (chK_zlhz.Checked == true)
                {
                    save_flag = Save_exam_tumour(ref faild_info);
                    if (!save_flag)
                    {
                        return;
                    }
                }
                //4.标本信息
                save_flag = Save_ExamSpecimens(txt_sqd.Text.Trim(), ref faild_info);
                if (!save_flag)
                {
                    return;
                }
                //5.申请单信息
                save_flag = Save_ExamMaster(ref faild_info, 1, ref Cur_BLH, false, "");
                if (!save_flag)
                {
                    return;
                }
                //结果提示
                if (save_flag)
                {
                    Frm_TJInfo("新建病人成功！", string.Format("\n分配病理号:{0}\n操作时间:{1}", Cur_BLH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                    //如果状态为标本接收状态则打印登记条码
                    DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                    if (insM.GetDjPrintFlag(Cur_BLH) == 0)
                    {
                        if (PIS_Sys.Properties.Settings.Default.djBarcodePrint)
                        {

                            FastReportLib.PrintBarCode.PrintBarcode(Cur_BLH, txt_patName.Text.Trim(), PIS_Sys.Properties.Settings.Default.djBarcodePrinter, PIS_Sys.Properties.Settings.Default.djBarcodePrintNum);

                            insM.UpdateDjPrintFlag(Cur_BLH);
                        }
                    }
                    //清空数据
                    ClearData();
                    //刷新列表
                    QueryBtn_Click(null, null);
                }
                else
                {
                    Frm_TJInfo("新建病人失败！", string.Format("\n失败原因:{0}\n操作时间:{1}", faild_info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                }
            }
        }


        //保存信息
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            //解决编辑supergrid的bug
            superGridControl1.Select();
            superGridControl1.Focus();
            if (txt_sqd.Text.Trim() != "" && txt_sqd.Enabled == false)
            {
                //合并申请单标记
                Boolean HB_Flag = false;
                //原先申请单号
                string source_exam_no = "";
                //是否保存成功
                Boolean save_flag = false;
                //病理号
                string Cur_BLH = "";
                //保存失败原因
                string faild_info = "";
                //有效性验证
                if (!check_YouXiao())
                {
                    return;
                }

                if (ConfigurationManager.AppSettings["DjTshb"].Equals("1"))
                {
                    //针对冷冻后送等情况，查询是否已经存在此病人之前登记的还未出报告的标本；保证一个病人只出一份报告
                    DBHelper.BLL.exam_master exam_masterIns = new DBHelper.BLL.exam_master();
                    string old_Exam_no = "";
                    string old_Study_no = "";
                    int SameStudyCount = exam_masterIns.GetSameExamCount(txt_sqd.Text.Trim(), txt_patid.Text.Trim());
                    if (SameStudyCount > 0)
                    {
                        FrmMerge FrmMergeIns = new FrmMerge();
                        FrmMergeIns.Owner = this;
                        FrmMergeIns.Pat_id = txt_patid.Text.Trim();
                        FrmMergeIns.Exam_no = txt_sqd.Text.Trim();
                        FrmMergeIns.Ice_flag = ((DevComponents.Editors.ComboItem)(cmb_ice.SelectedItem)).Text.Trim();
                        FrmMergeIns.TopMost = true;
                        if (FrmMergeIns.ShowDialog() == DialogResult.OK)
                        {
                            //预存新的申请单号
                            source_exam_no = txt_sqd.Text.Trim();
                            //赋值老的申请单号
                            txt_sqd.Text = FrmMergeIns.source_exam_no;
                            //当前检查为合并申请单类检查
                            HB_Flag = true;
                            txt_blh.Text = FrmMergeIns.source_study_no;
                            old_Study_no = FrmMergeIns.source_study_no;
                            old_Exam_no = FrmMergeIns.source_exam_no;
                        }

                    }
                }
                //手动输入病理号
                if (ChkMauBLH.Checked == true)
                {
                    if (txt_blh.Text.Trim() == "")
                    {
                        Frm_TJInfo("当前已选中病理号手动输入", "请输入手动分配给的病理号！");
                        txt_blh.Focus();
                        return;
                    }
                    else
                    {
                        Cur_BLH = txt_blh.Text.Trim().Replace("'", "");
                        if (HB_Flag == false)
                        {
                            //取大类
                            DBHelper.BLL.exam_type insType = new DBHelper.BLL.exam_type();
                            string big_type = insType.GetBigType(cmbExam_type.SelectedValue.ToString());
                            //查询手动输入病理号是否已经存在
                            DBHelper.BLL.sys_sequence sys_sequence_ins = new DBHelper.BLL.sys_sequence();
                            int count = sys_sequence_ins.CheckBLH_Enable(Cur_BLH, big_type);
                            if (count > 0)
                            {
                                Frm_TJInfo("病理号已经存在", "请重新输入手动分配给的病理号！");
                                txt_blh.Focus();
                                return;
                            }
                        }
                    }
                }
                else
                {

                    if (txt_blh.Text.Trim() != "")
                    {
                        Cur_BLH = txt_blh.Text.Trim();
                    }
                }
                //1.病人基本信息
                if (HB_Flag == false)
                {
                    save_flag = Save_PatMI(ref faild_info);
                    if (!save_flag)
                    {
                        return;
                    }
                }
                //2.妇科患者
                if (HB_Flag == false)
                {
                    if (chk_fkhz.Checked == true)
                    {
                        save_flag = Save_Exam_obstetric(ref faild_info);
                        if (!save_flag)
                        {
                            return;
                        }
                    }
                }
                //3.肿瘤患者
                if (HB_Flag == false)
                {
                    if (chK_zlhz.Checked == true)
                    {
                        save_flag = Save_exam_tumour(ref faild_info);
                        if (!save_flag)
                        {
                            return;
                        }
                    }
                }
                //4.标本信息

                save_flag = Save_ExamSpecimens(txt_sqd.Text.Trim(), ref faild_info);
                if (!save_flag)
                {
                    return;
                }

                //5.申请单信息
                save_flag = Save_ExamMaster(ref faild_info, 0, ref Cur_BLH, HB_Flag, source_exam_no);
                if (!save_flag)
                {
                    return;
                }

                //结果提示
                if (save_flag)
                {
                    Frm_TJInfo("保存病人信息成功！", string.Format("\n操作时间:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));

                    //如果状态为标本接收状态则打印登记条码
                    DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                    if (insM.GetDjPrintFlag(Cur_BLH) == 0)
                    {
                        if (PIS_Sys.Properties.Settings.Default.djBarcodePrint)
                        {

                            FastReportLib.PrintBarCode.PrintBarcode(Cur_BLH, txt_patName.Text.Trim(), PIS_Sys.Properties.Settings.Default.djBarcodePrinter, PIS_Sys.Properties.Settings.Default.djBarcodePrintNum);

                            insM.UpdateDjPrintFlag(Cur_BLH);
                        }
                    }
                    //是否启用接口服务
                    if (Program.Interface_SetInfo != null)
                    {
                        if (Program.Interface_SetInfo.enable_flag == 1)
                        {
                            //标本接收是否启用了接口服务
                            if (Program.Interface_SetInfo.receive_flag == 1)
                            {
                                ClientSCS.SynchronizedClient("dj|" + Cur_BLH);
                            }
                        }
                    }

                    //清空数据
                    ClearData();
                    //刷新列表
                    QueryBtn_Click(null, null);

                }
                else
                {
                    Frm_TJInfo("保存病人信息失败！", string.Format("\n失败原因:{0}\n操作时间:{1}", faild_info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                }
            }
            else
            {
                Frm_TJInfo("提示", "这个病人属于科室新建麽？");
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

        private void ClearData()
        {
            Chk_Wtzd.Checked = false;
            numericUpDown1.Value = 1;
            txt_BedNo.Text = "";
            current_place.Text = "";
            dt_birth.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_patName.Text = "";
            txt_age.Text = "";
            txt_sjbb.Text = "";
            cmb_nation.Text = "";
            txt_sfz.Text = "";
            txt_tel.Text = "";
            txt_items.Text = "";
            txt_Hkh.Text = "";
            txt_sqd.Text = "";
            txt_patid.Text = "";
            txt_Sqrq.Text = "";
            cmb_sqks.SelectedIndex = -1;
            cmb_sqys.SelectedIndex = -1;
            cmb_sqks.Text = "";
            cmb_sqys.Text = "";
            cmb_sjdw.Text = "本院";
            cmb_ice.SelectedIndex = 0;
            com_ks.SelectedIndex = 0;
            txt_bszy.Text = "";
            txt_sssj.Text = "";
            txt_lczd.Text = "";
            txt_crb.Text = "";
            dt_zlrq.Value = DateTime.Now;
            dt_preyj.Value = DateTime.Now;
            dt_lastyj.Value = DateTime.Now;
            txt_rgsj.Value = DateTime.Now;
            cmb_fszl.SelectedIndex = 0;
            cmb_zlzy.SelectedIndex = 0;
            cmb_hl.SelectedIndex = 0;
            cmb_rgzl.SelectedIndex = 0;
            chk_fkhz.Checked = false;
            chK_zlhz.Checked = false;
            txt_zlbw.Text = "";
            txt_zldx.Text = "";
            txt_zybw.Text = "";
            txt_zlbz.Text = "";
            txt_jl.Text = "";
            txt_tnum.Text = "";
            txt_cnum.Text = "";
            txt_rgsj.Value = DateTime.Now;
            cmb_jjflag.SelectedIndex = 0;
            txt_yjbz.Text = "";
            if (PIS_Sys.Properties.Settings.Default.BlhCreateFlag)
            {
                ChkMauBLH.Checked = false;
            }
            else
            {
                ChkMauBLH.Checked = true;
                ChkMauBLH.Enabled = false;
            }
            txt_blh.Text = "";
            txt_tj.Text = "";
            txt_inputid.Text = "";
            txt_sm_tj.Text = "";
            txt_patid.Enabled = true;
        }
        //扫描条件变动
        private void cmb_sm_tj_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_sm_tj.Focus();
        }
        //提取条件变动
        private void cmb_tj_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_tj.Focus();
        }
        //退单
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            //登记状态>=20 <55才可作废
            if (txt_sqd.Text.Trim() == "")
            {
                Frm_TJInfo("当前不能执行此操作", "请先选择要退单的病人，然后再执行此操作！");
                return;
            }
            DBHelper.BLL.exam_master insMas = new DBHelper.BLL.exam_master();
            int Sqd_Status = insMas.GetExam_Status(txt_sqd.Text.Trim());
            if (Sqd_Status >= 20 && Sqd_Status < 55)
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;

                if (TaskDialog.Show("病理科作废申请单", "确认", "确定要作废本申请单么？", Curbutton) == eTaskDialogResult.Ok)
                {

                    DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                    string result_str = "";
                    if (ins.ZF_ExamMaster(txt_sqd.Text.Trim(), Program.User_Code, ref result_str))
                    {

                        Frm_TJInfo("作废成功", result_str);

                        //是否启用接口服务
                        if (Program.Interface_SetInfo != null)
                        {
                            if (Program.Interface_SetInfo.enable_flag == 1)
                            {
                                //病理系统申请单作废调用接口服务
                                if (Program.Interface_SetInfo.pis_cancel_flag == 1)
                                {
                                    ClientSCS.SynchronizedClient("zf|" + txt_sqd.Text.Trim());
                                }
                            }
                        }

                        ClearData();
                        //刷新
                        QueryBtn_Click(QueryBtn, null);
                    }
                    else
                    {
                        Frm_TJInfo("作废失败", result_str);
                    }
                }
            }
            else
            {
                Frm_TJInfo("提示", "当前状态下不能退单！");
            }
        }
        //查询科室
        private void buttonX6_Click(object sender, EventArgs e)
        {
            Frm_dept Frm_deptIns = new Frm_dept();
            Frm_deptIns.Owner = this;
            Frm_deptIns.TopMost = true;
            Control ctrl = sender as Control;
            Point startPoint = groupPanel2.PointToScreen(ctrl.Location);
            startPoint.Y += ctrl.Height;
            Frm_deptIns.StartPosition = FormStartPosition.Manual;
            Frm_deptIns.Location = startPoint;
            if (Frm_deptIns.ShowDialog() == DialogResult.OK)
            {
                cmb_sqks.Text = Frm_deptIns.Dept_Str;
            }

        }
        //查询医生
        private void buttonX7_Click(object sender, EventArgs e)
        {
            if (cmb_sqks.Text != "" && cmb_sqks.SelectedValue != null)
            {
                Frm_doctor Frm_doctorIns = new Frm_doctor(cmb_sqks.SelectedValue.ToString());
                Frm_doctorIns.Owner = this;
                Frm_doctorIns.TopMost = true;
                Control ctrl = sender as Control;
                Point startPoint = groupPanel2.PointToScreen(ctrl.Location);
                startPoint.Y += ctrl.Height;
                Frm_doctorIns.StartPosition = FormStartPosition.Manual;
                Frm_doctorIns.Location = startPoint;
                if (Frm_doctorIns.ShowDialog() == DialogResult.OK)
                {
                    cmb_sqys.Text = Frm_doctorIns.Doc_Str;
                }
            }
            else
            {
                Frm_TJInfo("提示", "请先选择申请科室！");
            }
        }

        //条件快速扫描登记
        private void txt_sm_tj_KeyPress(object sender, KeyPressEventArgs e)
        {
            DataSet _DataSet = null;
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                string str_tj = txt_sm_tj.Text.Trim().Replace("'", "");
                if (!str_tj.Equals(""))
                {
                    string key_str = ((DevComponents.Editors.ComboItem)(cmb_sm_tj.SelectedItem)).Text.Trim();
                    if (DirectFetchFromTjHis(key_str, str_tj) == false)
                    {
                        //初始统计计数
                        Init_Js();
                        //执行查询
                        if (_DataSet != null)
                        {
                            _DataSet.Clear();
                        }
                        _DataSet = new DataSet();
                        DBHelper.BLL.exam_master exam_mas_Ins = new DBHelper.BLL.exam_master();
                        _DataSet = exam_mas_Ins.QuerySMDsExam_master(key_str, PIS_Sys.Properties.Settings.Default.DjType, str_tj, Program.workstation_type_db);
                        if (_DataSet != null && _DataSet.Tables[0].Rows.Count > 0)
                        {
                            superGridControl1.PrimaryGrid.ClearSelectedCells();
                            superGridControl1.PrimaryGrid.ClearSelectedRows();
                            superGridControl1.PrimaryGrid.DataSource = _DataSet;

                        }
                        else
                        {
                            superGridControl1.PrimaryGrid.DataSource = null;
                            Frm_TJInfo("病人不存在", string.Format("{0}为{1}的查询为空！\n此查询仅仅支持预约或者申请状态病人扫描。", key_str, str_tj));
                            ClearData();
                        }
                    }
                }
                //查询结束即清空
                txt_sm_tj.Text = "";
            }
        }
        //直接从HIS提取数据
        public Boolean DirectFetchFromTjHis(string key_str, string str_tj)
        {
            if (PIS_Sys.Properties.Settings.Default.His_PatInfo_Flag == false)
            {
                return false;
            }
            try
            {
                DataSet _DataSet = null;
                //初始统计计数
                Init_Js();
                //执行查询
                if (_DataSet != null)
                {
                    _DataSet.Clear();
                }
                _DataSet = new DataSet();
                DBHelper.BLL.exam_master exam_mas_Ins = new DBHelper.BLL.exam_master();
                _DataSet = exam_mas_Ins.QuerySMDsExam_master(key_str, PIS_Sys.Properties.Settings.Default.DjType, str_tj, Program.workstation_type_db);
                if (_DataSet != null && _DataSet.Tables[0].Rows.Count > 0)
                {
                    superGridControl1.PrimaryGrid.ClearSelectedCells();
                    superGridControl1.PrimaryGrid.ClearSelectedRows();
                    superGridControl1.PrimaryGrid.DataSource = _DataSet;
                    return true;
                }
                else
                {
                    superGridControl1.PrimaryGrid.DataSource = null;
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                Program.FileLog.Error(String.Format("从HIS直接提取数据，条件文本{0},条件值{1}，异常为：{2}", key_str, str_tj, ex.ToString()));
            }
            return false;
        }
        //直接从HIS提取数据
        public Boolean QBDirectFetchFromTjHis(string key_str, string str_tj)
        {
            if (PIS_Sys.Properties.Settings.Default.His_PatInfo_Flag == false)
            {
                return false;
            }
            try
            {
                DataSet _DataSet = null;
                //初始统计计数
                Init_Js();
                //执行查询
                if (_DataSet != null)
                {
                    _DataSet.Clear();
                }
                _DataSet = new DataSet();
                DBHelper.BLL.exam_master exam_mas_Ins = new DBHelper.BLL.exam_master();
                _DataSet = exam_mas_Ins.QueryDJDsExam_master(key_str, PIS_Sys.Properties.Settings.Default.DjType, str_tj, Program.workstation_type_db);
                if (_DataSet != null && _DataSet.Tables[0].Rows.Count > 0)
                {
                    superGridControl1.PrimaryGrid.ClearSelectedCells();
                    superGridControl1.PrimaryGrid.ClearSelectedRows();
                    superGridControl1.PrimaryGrid.DataSource = _DataSet;
                    return true;
                }
                else
                {
                    superGridControl1.PrimaryGrid.DataSource = null;
                    ClearData();
                }
            }
            catch (Exception ex)
            {
                Program.FileLog.Error(String.Format("从HIS直接提取数据，条件文本{0},条件值{1}，异常为：{2}", key_str, str_tj, ex.ToString()));
            }
            return false;
        }

        //快速检索
        private void txt_tj_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                string str_tj = txt_tj.Text.Trim();
                if (!str_tj.Equals(""))
                {
                    buttonX1_Click(null, null);
                }
            }
        }
        //快速检索
        private void buttonX1_Click(object sender, EventArgs e)
        {
            DataSet _DataSet = null;
            string str_tj = txt_tj.Text.Trim().Replace("'", "");
            if (!str_tj.Equals(""))
            {
                string key_str = ((DevComponents.Editors.ComboItem)(cmb_tj.SelectedItem)).Text.Trim();
                if (QBDirectFetchFromTjHis(key_str, str_tj) == false)
                {
                    //初始统计计数
                    Init_Js();
                    //执行查询
                    if (_DataSet != null)
                    {
                        _DataSet.Clear();
                    }
                    _DataSet = new DataSet();
                    DBHelper.BLL.exam_master exam_mas_Ins = new DBHelper.BLL.exam_master();
                    _DataSet = exam_mas_Ins.QueryDJDsExam_master(key_str, PIS_Sys.Properties.Settings.Default.DjType, str_tj, Program.workstation_type_db);
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
        }
        //添加标本信息
        private void buttonX8_Click(object sender, EventArgs e)
        {
            if (this.contextMenuStrip2.Items.Count > 0)
            {
                Control ctrl = sender as Control;
                Point p = groupPanel2.PointToScreen(new Point(ctrl.Right, ctrl.Top));
                this.contextMenuStrip2.Show(p);
            }
        }
        //肿瘤患者
        private void chK_zlhz_CheckedChanged(object sender, EventArgs e)
        {
            if (chK_zlhz.Checked == true)
            {
                groupPanel3.Focus();
            }
        }
        //妇科患者
        private void chk_fkhz_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_fkhz.Checked == true)
            {
                groupPanel4.Focus();
            }
        }
        //列表样式设置
        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            GridPanel panel = e.GridPanel;
            panel.DefaultVisualStyles.CaptionStyles.Default.Alignment = Alignment.MiddleCenter;
            panel.DefaultVisualStyles.CellStyles.Default.Alignment = Alignment.MiddleCenter;
            if (panel.Footer == null)
            {
                panel.Footer = new GridFooter();
            }
            if (!oldSqd.Equals(""))
            {
                SelectedOldExam_NO(oldSqd);
            }
        }
        //计数
        string bdyy_str = "冰冻病例:<font color='blue'><b>{0}</b></font> 例";
        //冰冻特别标识（重要类别计数）
        int yy_int = 0;
        int sq_int = 0;
        int dj_int = 0;
        int bdyy_int = 0;

        private void Init_Js()
        {
            yy_int = 0;
            sq_int = 0;
            dj_int = 0;
            bdyy_int = 0;

            if (superGridControl1.PrimaryGrid.Footer == null)
            {
                superGridControl1.PrimaryGrid.Footer = new GridFooter();
            }
            superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Red'><b>{0}</b></font> 条记录", 0);
            lbl_bdyy.Text = string.Format(bdyy_str, bdyy_int);

        }
        private void superGridControl1_GetRowHeaderStyle(object sender, GridGetRowHeaderStyleEventArgs e)
        {
            try
            {
                if (superGridControl1.PrimaryGrid.Rows.Count > 0)
                {
                    //是否冰冻
                    string ice_flag = ((GridRow)e.GridRow).Cells["ice_flag"].Value.ToString(); //GridPanel panel = e.GridPanel;panel.GetCell(e.GridRow.Index, 18).Value.ToString();

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
                    bdyy_int += 1;
                }


                switch (status)
                {
                    case "10":
                        yy_int += 1;
                        break;
                    case "15":
                        sq_int += 1;
                        break;
                    case "20":
                        dj_int += 1;
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
                    superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='blue'><b>{0}</b></font> 条记录:预<font color='blue'><b>{1}</b></font>,申<font color='blue'><b>{2}</b></font>,登<font color='blue'><b>{3}</b></font>", rows, yy_int, sq_int, dj_int);
                    lbl_bdyy.Text = string.Format(bdyy_str, bdyy_int);
                }
            }


        }
        //检查状态颜色标识
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
        //激活窗体时设置焦点位置
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //刷新列表
            QueryBtn_Click(null, null);
            //设置光标位置
            switch (PIS_Sys.Properties.Settings.Default.Bb_Reg_Curor)
            {
                case 0:
                    txt_sm_tj.Focus();
                    break;
                case 1:
                    txt_patName.Focus();
                    break;
                case 2:
                    txt_tj.Focus();
                    break;
                default:
                    txt_tj.Focus();
                    break;
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


        string oldSqd = "";
        string lcjc_str = "历次检查:<font color='blue'><b>{0}</b></font>例";
        //选中展示
        private void superGridControl1_SelectionChanged(object sender, GridEventArgs e)
        {

            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {

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
                oldSqd = exam_no;
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
                //取信息并赋值给界面
                ClearData();
                //1.赋值病人基本信息
                SetPatient_Info(patient_id);
                //2.赋值申请单信息
                SetSqd_Info(exam_no);

                //4.赋值妇科肿瘤信息
                SetFk_Info(exam_no);
                SetZl_Info(exam_no);
                //5.标本采集信息
                SetSpecimens(exam_no);

            }
            superGridControl1.Select();
            superGridControl1.Focus();
        }
        //1.赋值病人基本信息
        private void SetPatient_Info(string patient_id)
        {
            DBHelper.BLL.exam_pat_mi Pat_ins = new DBHelper.BLL.exam_pat_mi();
            DBHelper.Model.exam_pat_mi MPat_ins = Pat_ins.GetPatientInfo(patient_id);
            if (MPat_ins != null)
            {
                txt_patid.Text = MPat_ins.patient_id;
                txt_patid.Enabled = false;
                txt_patName.Text = MPat_ins.patient_name;
                cmb_patSex.Text = MPat_ins.sex;
                if (MPat_ins.date_of_birth.Equals(""))
                {
                    dt_birth.Value = DateTime.Now;
                }
                else
                {
                    dt_birth.Value = Convert.ToDateTime(MPat_ins.date_of_birth);
                }
                //if (txt_age.Text.Trim().Equals("") || txt_age.Text.Trim().Equals("0"))
                //{
                //    txt_age.Text = Convert.ToString(calculationDate(dt_birth.Value, DateTime.Now));
                //}
                current_place.Text = MPat_ins.current_place;
                cmb_nation.Text = MPat_ins.nation;
                txt_tel.Text = MPat_ins.phone_number;
                txt_sfz.Text = MPat_ins.identity;
                txt_Hkh.Text = MPat_ins.hospital_card;
            }

        }
        //2.赋值申请单信息
        private void SetSqd_Info(string exam_no)
        {
            DBHelper.BLL.exam_master mas_ins = new DBHelper.BLL.exam_master();
            DBHelper.Model.exam_master Mmas_ins = mas_ins.GetExam_MasterInfo(exam_no);
            if (Mmas_ins != null)
            {
                if (Mmas_ins.age.Equals("") || Mmas_ins.age.Equals("0"))
                {
                    //txt_age.Text = Convert.ToString(calculationDate(dt_birth.Value, DateTime.Now));
                }
                else
                {
                    txt_age.Text = Mmas_ins.age;
                }
                if (Mmas_ins.wtzd_flag == 1)
                {
                    Chk_Wtzd.Checked = true;
                }
                else
                {
                    Chk_Wtzd.Checked = false;
                }
                txt_BedNo.Text = Mmas_ins.bed_no;
                cmb_ageUnit.Text = Mmas_ins.ageUint;
                txt_sqd.Text = Mmas_ins.exam_no;
                txt_sqd.Enabled = false;
                txt_inputid.Text = Mmas_ins.input_id;
                cmb_sjdw.Text = Mmas_ins.submit_unit;
                txt_blh.Text = Mmas_ins.study_no;
                cmb_sqks.Text = Mmas_ins.req_dept;
                cmb_sqys.Text = Mmas_ins.req_physician;
                txt_Sqrq.Text = Mmas_ins.req_date_time;
                txt_items.Text = Mmas_ins.examItems;
                cmb_patSource.Text = Mmas_ins.patient_source;
                cmb_ice.Text = (Mmas_ins.ice_flag == 1 ? "是" : "否");
                com_ks.Text = (Mmas_ins.ks_flag == 1 ? "是" : "否");
                txt_costs.Text = Mmas_ins.costs;
                if (Mmas_ins.inout_type == 1)
                {
                    this.radioButton2.Checked = true;
                }
                else
                {
                    this.radioButton1.Checked = true;
                }
                cmbExam_type.SelectedValue = Mmas_ins.modality;

                chk_fkhz.Checked = (Mmas_ins.fk_flag == 1 ? true : false);
                chK_zlhz.Checked = (Mmas_ins.zl_flag == 1 ? true : false);
                DBHelper.BLL.exam_requisition Rins = new DBHelper.BLL.exam_requisition();
                DBHelper.Model.exam_requisition RMins = Rins.GetRequisitionInfo(exam_no);
                if (RMins != null)
                {
                    txt_bszy.Text = RMins.history_note;
                    txt_lczd.Text = RMins.clinical_diag;
                    txt_sssj.Text = RMins.ops_note;
                    txt_crb.Text = RMins.infectious_note;
                }
            }
        }

        //4.赋值妇科信息
        private void SetFk_Info(string exam_no)
        {
            if (chk_fkhz.Checked == true)
            {
                DBHelper.BLL.exam_obstetric ins = new DBHelper.BLL.exam_obstetric();
                DBHelper.Model.exam_obstetric Mins = ins.GetExam_obstetricInfo(exam_no);
                if (Mins != null)
                {
                    dt_preyj.Value = Convert.ToDateTime(Mins.last_date);
                    dt_lastyj.Value = Convert.ToDateTime(Mins.pre_date);
                    cmb_rgzl.Text = Mins.ops_flag;
                    cmb_jjflag.Text = Mins.absolute_flag;
                    txt_jl.Text = Mins.ops_unit;
                    txt_tnum.Text = Mins.foetus;
                    txt_cnum.Text = Mins.production;
                    txt_yjbz.Text = Mins.memo;
                    txt_rgsj.Value = Convert.ToDateTime(Mins.ops_date);
                }
            }
        }
        //5.赋值肿瘤信息
        private void SetZl_Info(string exam_no)
        {
            if (chK_zlhz.Checked == true)
            {
                DBHelper.BLL.exam_tumour ins = new DBHelper.BLL.exam_tumour();
                DBHelper.Model.exam_tumour Mins = ins.GetExam_tumourInfo(exam_no);
                if (Mins != null)
                {
                    dt_zlrq.Value = Convert.ToDateTime(Mins.find_date);
                    txt_zlbw.Text = Mins.parts;
                    txt_zlbz.Text = Mins.memo;
                    txt_zldx.Text = Mins.sizes;
                    cmb_zlzy.Text = Mins.transfer_flag;
                    cmb_hl.Text = Mins.chemotherapy;
                    cmb_fszl.Text = Mins.radiate_flag;
                    txt_zybw.Text = Mins.trans_parts;
                }
            }
        }
        //6标本采集信息
        private void SetSpecimens(string exam_no)
        {
            DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
            DataTable dt = ins.GetSpecimensInfo(exam_no);
            if (dt != null)
            {
                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    sb.Append(dt.Rows[k]["parts"].ToString());
                    sb.Append(" ");
                }
                txt_sjbb.Text = sb.ToString();
                sb.Clear();
                this.numericUpDown1.Value = Convert.ToInt32(dt.Rows[0]["pack_order"]);
            }
            //查询是否存在不合格标本登记信息
            DataSet _DataSet = new DataSet();
            DBHelper.BLL.exam_specimens_qualified insbhg = new DBHelper.BLL.exam_specimens_qualified();
            _DataSet = insbhg.GetDsSpecimens_qualified(exam_no);
            if (_DataSet != null && _DataSet.Tables[0].Rows.Count > 0)
            {
                buttonX3.TextColor = Color.Blue;
            }
            else
            {
                buttonX3.TextColor = CurBtnColor;
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

        //手动输入病理号选中设置
        private void ChkMauBLH_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            if (ChkMauBLH.Checked == false)
            {
                txt_blh.Enabled = false;
            }
            else
            {
                txt_blh.Enabled = true;
            }
        }

        //再次打印条码
        private void btn_barcode_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    Frm_TJInfo("提示", "请先选择一个检查；\n然后再执行打印。");
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index == -1)
                {
                    Frm_TJInfo("提示", "请先选择一个检查；\n然后再执行打印。");
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];

                //病理号
                string study_no = Row.Cells["study_no"].Value.ToString();
                //病人姓名
                string patient_name = Row.Cells["patient_name"].Value.ToString();
                //当前检查状态
                int status = Convert.ToInt32(Row.Cells["curstatus"].Value);
                if (status >= 20)
                {
                    //如果状态为标本接收状态则打印登记条码
                    FastReportLib.PrintBarCode.PrintBarcode(study_no, txt_patName.Text.Trim(), PIS_Sys.Properties.Settings.Default.djBarcodePrinter, PIS_Sys.Properties.Settings.Default.djBarcodePrintNum);
                }
                else
                {
                    Frm_TJInfo("提示", "当前状态不能打印条码。");
                }

            }
        }

        private void FrmBbdj_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void cmb_patSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_patSource.Text == "外来")
            {
                if (cmb_sjdw.Text.Trim().Equals("本院"))
                {
                    cmb_sjdw.Text = "";
                }
                radioButton2.Checked = true;
            }
            else
            {
                if (cmb_sjdw.Text.Trim().Equals(""))
                {
                    cmb_sjdw.Text = "本院";
                }
                radioButton1.Checked = true;
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

        private void FrmBbdj_KeyPress(object sender, KeyPressEventArgs e)
        {
            //用回车代替Tab
            if (e.KeyChar == 13)
            {
                e.Handled = false;
                SendKeys.Send("{TAB}");
            }
        }



        //清空界面
        private void buttonItem13_Click(object sender, EventArgs e)
        {
            clearUIData();
        }
        //获取病人基本信息
        private void txt_patid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (PIS_Sys.Properties.Settings.Default.His_PatInfo_Flag)
                {
                    try
                    {
                        string str_tj = txt_patid.Text.Trim().Replace("'", "").ToUpper();
                        if (!str_tj.Equals(""))
                        {
                            string Receive_Message = ClientSCS.ZSynchronizedClient("pat|" + str_tj);
                            string[] keys = Receive_Message.Split('|');
                            if (keys.Length == 4)
                            {
                                if (keys[3].Length >= 8)
                                {
                                    // patient_id,name,sex,date_of_birth,charge_type
                                    dt_birth.Text = keys[3].ToString();
                                    if (txt_age.Text.Trim().Equals("") || txt_age.Text.Trim().Equals("0"))
                                    {
                                        txt_age.Text = Convert.ToString(calculationDate(dt_birth.Value, DateTime.Now));
                                    }
                                }
                                else
                                {
                                    txt_age.Text = "0";
                                }
                                txt_patName.Text = keys[1];
                                cmb_patSex.Text = keys[2];
                            }
                        }
                    }
                    catch
                    {
                        Frm_TJInfo("提示", "服务查询出错！");
                    }
                }
            }
        }
        //不合格标本的登记 FrmBhgbb
        private void buttonX3_Click_1(object sender, EventArgs e)
        {
            if (!txt_sqd.Text.Trim().Equals(""))
            {
                FrmBhgbb FrmBhgbbIns = new FrmBhgbb();
                FrmBhgbbIns.Owner = this;
                FrmBhgbbIns.TopMost = true;
                FrmBhgbbIns.study_no = txt_blh.Text.Trim();
                FrmBhgbbIns.dept_name = cmb_sqks.Text.Trim();
                FrmBhgbbIns.doctor_name = cmb_sqys.Text.Trim();
                FrmBhgbbIns.special_name = txt_sjbb.Text.Trim();
                FrmBhgbbIns.exam_no = txt_sqd.Text.Trim();
                FrmBhgbbIns.patient_id = txt_patid.Text.Trim();
                FrmBhgbbIns.input_id = txt_inputid.Text.Trim();
                FrmBhgbbIns.ShowDialog();
            }
            else
            {
                Frm_TJInfo("提示", "申请单为空，无法登记！");
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


