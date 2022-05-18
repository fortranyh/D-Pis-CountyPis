using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace PIS_Sys.jcyy
{

    public partial class FrmJcyy : DevComponents.DotNetBar.Office2007Form
    {
        public FrmJcyy()
        {
            InitializeComponent();
        }
        private void FrmJcyy_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
            //送检单位
            DBHelper.BLL.doctor_dict sjdw_ins = new DBHelper.BLL.doctor_dict();
            DataTable dt = sjdw_ins.GetSjdwList();
            if (dt != null && dt.Rows.Count > 0)
            {
                this.cmb_sjdw.DataSource = dt;
                cmb_sjdw.DisplayMember = "name";
                cmb_sjdw.ValueMember = "id";
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
            cmb_ks.SelectedIndex = 0;
            cmb_ageUnit.SelectedIndex = 0;
            cmb_patSex.SelectedIndex = 0;
            cmb_patSource.SelectedIndex = 0;
            //初始化控件
            ClearData();
            dt_birth.Text = DateTime.Now.ToString("yyyy-MM-dd");
            //根据年龄计算出生日期
            txt_age.LostFocus += new EventHandler(txt_age_LostFocus);

            //动态生成检查部位右键菜单 contextMenuStrip2
            this.MenuItemClick += new MenuEventHandler(bbxx_MenuItemClick);
            CreatePopUpJcbwMenu();

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


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.txt_patName.Select();
            this.txt_patName.Focus();
        }

        private void FrmJcyy_Activated(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
        }
        //清空
        private void btn_clear_Click(object sender, EventArgs e)
        {
            ClearData();
            //设置光标位置
            this.txt_patName.Select();
            this.txt_patName.Focus();

        }
        private void ClearData()
        {
            this.Chk_Wtzd.Checked = false;
            this.numericUpDown1.Value = 1;
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
            txt_inputid.Text = "";
            txt_sqd.Text = "";
            current_place.Text = "";
            txt_items.Text = "";
            dt_birth.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_patName.Text = "";
            txt_age.Text = "";
            txt_sjbb.Text = "";
            cmb_nation.Text = "";
            txt_sfz.Text = "";
            txt_tel.Text = "";
            txt_Hkh.Text = "";
            txt_patid.Text = "";
            txt_inputid.Text = "";
            txt_Sqrq.Text = "";
            cmb_sqks.SelectedIndex = -1;
            cmb_sqys.SelectedIndex = -1;
            cmb_sqks.Text = "";
            cmb_sqys.Text = "";
            cmb_sjdw.Text = "本院";
            cmb_ice.SelectedIndex = 0;
            cmb_ks.SelectedIndex = 0;
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
            Txt_bedNo.Text = "";
        }

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
        //申请科室切换
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
        //选择标本部位
        private void buttonX8_Click(object sender, EventArgs e)
        {
            if (this.contextMenuStrip2.Items.Count > 0)
            {
                Control ctrl = sender as Control;
                Point p = groupPanel2.PointToScreen(new Point(ctrl.Right, ctrl.Top));
                this.contextMenuStrip2.Show(p);
            }
        }
        //申请科室
        private void buttonX6_Click(object sender, EventArgs e)
        {
            bbdj.Frm_dept Frm_deptIns = new bbdj.Frm_dept();
            Frm_deptIns.Owner = this;
            Frm_deptIns.TopMost = true;
            Control ctrl = sender as Control;
            Point startPoint = groupPanel2.PointToScreen(ctrl.Location);
            startPoint.Y += ctrl.Height;
            Frm_deptIns.StartPosition = FormStartPosition.Manual;
            Frm_deptIns.Location = startPoint;

            Frm_deptIns.ShowDialog();

            if (Frm_deptIns.DialogResult == DialogResult.OK)
            {
                cmb_sqks.Text = Frm_deptIns.Dept_Str;
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
        //申请日期
        private void buttonX4_Click_1(object sender, EventArgs e)
        {
            txt_Sqrq.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        //申请医师
        private void buttonX7_Click(object sender, EventArgs e)
        {
            if (cmb_sqks.Text != "")
            {
                bbdj.Frm_doctor Frm_doctorIns = new bbdj.Frm_doctor(cmb_sqks.SelectedValue.ToString());
                Frm_doctorIns.Owner = this;
                Frm_doctorIns.TopMost = true;
                Control ctrl = sender as Control;
                Point startPoint = groupPanel2.PointToScreen(ctrl.Location);
                startPoint.Y += ctrl.Height;
                Frm_doctorIns.StartPosition = FormStartPosition.Manual;
                Frm_doctorIns.Location = startPoint;

                Frm_doctorIns.ShowDialog();

                if (Frm_doctorIns.DialogResult == DialogResult.OK)
                {
                    cmb_sqys.Text = Frm_doctorIns.Doc_Str;
                }
            }
            else
            {
                Frm_TJInfo("提示", "请先选择申请科室！");
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
            //取大类
            DBHelper.BLL.exam_type insType = new DBHelper.BLL.exam_type();
            string big_type = insType.GetBigType(examMas_Ins.modality);
            examMas_Ins.exam_type = big_type;
            examMas_Ins.bed_no = Txt_bedNo.Text.Trim();
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
            examMas_Ins.ks_flag = (((DevComponents.Editors.ComboItem)(cmb_ks.SelectedItem)).Text.Equals("是") ? 1 : 0);
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

        //新建
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            //解决编辑supergrid的bug
            this.txt_patName.Select();
            txt_patName.Focus();
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
                Frm_TJInfo("数据验证", "病人ID号不能为空,请重试!");
                txt_patid.Focus();
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
            }
            else
            {
                Frm_TJInfo("新建病人失败！", string.Format("\n失败原因:{0}\n操作时间:{1}", faild_info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            }
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


        private void FrmJcyy_KeyPress(object sender, KeyPressEventArgs e)
        {
            //用回车代替Tab
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                SendKeys.Send("{TAB}");
            }
        }
        //手动输入病理号
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
        public int calculationDate(DateTime beginDateTime, DateTime endDateTime) { if (beginDateTime > endDateTime) throw new Exception("开始时间应小于或等与结束时间！");          /*计算出生日期到当前日期总月数*/      int Months = endDateTime.Month - beginDateTime.Month + 12 * (endDateTime.Year - beginDateTime.Year);       /*出生日期加总月数后，如果大于当前日期则减一个月*/      int totalMonth = (beginDateTime.AddMonths(Months) > endDateTime) ? Months - 1 : Months;       /*计算整年*/      int fullYear = totalMonth / 12;       /*计算整月*/      int fullMonth = totalMonth % 12;       /*计算天数*/      DateTime changeDate = beginDateTime.AddMonths(totalMonth); double days = (endDateTime - changeDate).TotalDays; return fullYear; }

        //查询his获取病人基本信息
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
