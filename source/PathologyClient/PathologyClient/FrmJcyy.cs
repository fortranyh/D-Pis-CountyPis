using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;


namespace PathologyClient
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
            AutoCompleteStringCollection asc = new AutoCompleteStringCollection();
            Txt_Sjdw.Text = Program.HospitalName;
            txt_Sqrq.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //检查类型
            radioButton2_CheckedChanged(null, null);
            //民族  
            string xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetMzDicXML", null);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                cmb_nation.DataSource = ds.Tables[0];
                cmb_nation.DisplayMember = "nation_name";
                cmb_nation.ValueMember = "id";
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    asc.Add(ds.Tables[0].Rows[i]["nation_name"].ToString());
                }
                this.cmb_nation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                this.cmb_nation.AutoCompleteSource = AutoCompleteSource.CustomSource;
                this.cmb_nation.AutoCompleteCustomSource = asc;
            }
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
            //
            int left = 0;
            if ((this.Width - panelEx1.Width) / 2 < 0)
            {
                left = 0;
            }
            else
            {
                left = (this.Width - panelEx1.Width) / 2;
            }
            int top = 0;
            if ((this.Height - panelEx1.Height) / 2 < 0)
            {
                top = 0;
            }
            else
            {
                top = (this.Height - panelEx1.Height) / 2;
                if (top > 50)
                {
                    top = 40;
                }
                else if (top > 40)
                {
                    top = 30;
                }
                else if (top > 30)
                {
                    top = 20;
                }
                else if (top > 20)
                {
                    top = 10;
                }
                else if (top > 10)
                {
                    top = 0;
                }
            }
            this.panelEx1.Location = new Point(left, top);
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
            cmb_patSex.SelectedIndex = 0;
            Txt_Sjdw.Text = Program.HospitalName;
            this.numericUpDown1.Value = 1;
            txt_Sqks.Text = "";
            txt_Sqys.Text = "";
            txt_inputid.Text = "";
            current_place.Text = "";
            txt_items.Text = "";
            dt_birth.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txt_patName.Text = "";
            txt_age.Text = "";
            txt_sjbb.Text = "";
            cmb_nation.Text = "";
            txt_sfz.Text = "";
            txt_tel.Text = "";
            txt_Py.Text = "";
            txt_inputid.Text = "";
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
            txt_patName.Select();
            txt_patName.Focus();
            txt_Sqrq.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                //组织post请求参数
                parameters.Add("flag", "0");
                string xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetExamTypeXML", parameters);
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xmldata));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
                Txt_Sjdw.Text = Program.HospitalName;
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                //组织post请求参数
                parameters.Add("flag", "1");
                string xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetExamTypeXML", parameters);
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xmldata));
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    cmbExam_type.DataSource = ds.Tables[0];
                    cmbExam_type.DisplayMember = "modality_cn";
                    cmbExam_type.ValueMember = "modality";
                }
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

        //保存登记标本病人基本信息
        public Boolean Save_PatMI(ref string faild_info, string PidStr)
        {
            Boolean ResultDb_Flag = false;
            EntityModel.exam_pat_mi patmi_Ins = new EntityModel.exam_pat_mi();
            patmi_Ins.patient_id = PidStr;
            patmi_Ins.patient_name = txt_patName.Text.Trim();
            if (txt_Py.Text.Trim().Equals(""))
            {
                patmi_Ins.name_phonetic = PinYinConverter.Get(patmi_Ins.patient_name);
            }
            else
            {
                patmi_Ins.name_phonetic = txt_Py.Text.Trim();
            }
            patmi_Ins.sex = ((DevComponents.Editors.ComboItem)(cmb_patSex.SelectedItem)).Text.Trim();
            patmi_Ins.date_of_birth = dt_birth.Text.Trim();
            patmi_Ins.nation = cmb_nation.Text.Trim();
            patmi_Ins.hospital_card = "";
            patmi_Ins.si_card = "";
            patmi_Ins.identity = txt_sfz.Text.Trim();
            patmi_Ins.phone_number = txt_tel.Text.Trim();
            patmi_Ins.current_place = current_place.Text.Trim();
            string jsonStr = PublicBaseLib.JsonHelper.ToJson(patmi_Ins);
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            //组织post请求参数
            parameters.Add("JsonStr", jsonStr);
            string Retdata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "Process_Patmi", parameters);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(Retdata));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                faild_info = ds.Tables[0].Rows[0]["INFO"].ToString();
                if (ds.Tables[0].Rows[0]["RESULT_CODE"].ToString().Equals("0"))
                {
                    Frm_TJInfo("失败", "保存病人信息失败！" + Environment.NewLine + "失败原因：" + faild_info);
                }
                else
                {
                    ResultDb_Flag = true;
                }
            }
            return ResultDb_Flag;
        }

        //保存登记标本申请单信息
        public Boolean Save_ExamMaster(ref string faild_info, string exam_no, string PidStr)
        {
            Boolean ResultDb_Flag = false;
            //电子申请单描述性
            EntityModel.exam_requisition exam_req_ins = new EntityModel.exam_requisition();
            exam_req_ins.exam_no = exam_no;
            exam_req_ins.history_note = txt_bszy.Text.Trim();
            exam_req_ins.infectious_note = txt_crb.Text.Trim();
            exam_req_ins.clinical_diag = txt_lczd.Text.Trim();
            exam_req_ins.ops_note = txt_sssj.Text.Trim();

            string jsonStr = PublicBaseLib.JsonHelper.ToJson(exam_req_ins);
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            //组织post请求参数
            parameters.Add("JsonStr", jsonStr);
            string Retdata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "Process_Requisition", parameters);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(Retdata));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                faild_info = ds.Tables[0].Rows[0]["INFO"].ToString();
                if (ds.Tables[0].Rows[0]["RESULT_CODE"].ToString().Equals("0"))
                {
                    Frm_TJInfo("失败", "保存申请单描述信息失败！" + Environment.NewLine + "失败原因：" + faild_info);
                    return ResultDb_Flag;
                }
                else
                {
                    ResultDb_Flag = true;
                }
            }
            //检查
            EntityModel.exam_master examMas_Ins = new EntityModel.exam_master();
            examMas_Ins.age = txt_age.Text.Trim();
            examMas_Ins.ageUint = ((DevComponents.Editors.ComboItem)(cmb_ageUnit.SelectedItem)).Text.Trim();
            examMas_Ins.exam_no = exam_no;
            examMas_Ins.patient_id = PidStr;
            examMas_Ins.examItems = txt_items.Text.Trim();
            examMas_Ins.inout_type = (radioButton1.Checked == true ? 0 : 1);
            examMas_Ins.input_id = txt_inputid.Text.Trim();
            examMas_Ins.output_id = "";
            examMas_Ins.patient_source = ((DevComponents.Editors.ComboItem)(cmb_patSource.SelectedItem)).Text.Trim();
            examMas_Ins.new_flag = 1;
            examMas_Ins.modality = cmbExam_type.SelectedValue.ToString();
            //取大类
            parameters.Clear();
            //组织post请求参数
            parameters.Add("modality", examMas_Ins.modality);
            string xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetExamTypeCode", parameters);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmldata);
            examMas_Ins.exam_type = xmlDoc.DocumentElement.InnerText;
            if (examMas_Ins.exam_type.Equals(""))
            {
                Frm_TJInfo("服务器错误", "获取检查大类编码失败！");
                return false;
            }
            examMas_Ins.parts = txt_sjbb.Text.Trim();
            examMas_Ins.bed_no = Txt_bedNo.Text.Trim();
            examMas_Ins.ward = "";
            examMas_Ins.wtzd_flag = Chk_Wtzd.Checked == true ? 1 : 0;
            examMas_Ins.req_date_time = (txt_Sqrq.Text.Trim().Equals("") ? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : txt_Sqrq.Text.Trim());
            examMas_Ins.req_dept = txt_Sqks.Text.Trim();
            examMas_Ins.req_dept_code = "";
            examMas_Ins.req_physician = txt_Sqys.Text.ToString();
            examMas_Ins.req_physician_code = "";
            examMas_Ins.submit_unit = Txt_Sjdw.Text.Trim();
            examMas_Ins.ice_flag = (((DevComponents.Editors.ComboItem)(cmb_ice.SelectedItem)).Text.Equals("是") ? 1 : 0);
            examMas_Ins.ks_flag = (((DevComponents.Editors.ComboItem)(cmb_ks.SelectedItem)).Text.Equals("是") ? 1 : 0);
            examMas_Ins.fk_flag = (chk_fkhz.Checked == true ? 1 : 0);
            examMas_Ins.zl_flag = (chK_zlhz.Checked == true ? 1 : 0);
            examMas_Ins.exam_status = "15";
            examMas_Ins.receive_doctor_name = "";
            examMas_Ins.received_doctor_code = "";
            examMas_Ins.received_datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            examMas_Ins.merge_exam_no = "";
            examMas_Ins.study_no = "";
            parameters.Clear();
            jsonStr = PublicBaseLib.JsonHelper.ToJson(examMas_Ins);
            //组织post请求参数
            parameters.Add("JsonStr", jsonStr);
            Retdata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "Process_Master", parameters);
            ds = new DataSet();
            ds.ReadXml(new StringReader(Retdata));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                faild_info = ds.Tables[0].Rows[0]["INFO"].ToString();
                if (ds.Tables[0].Rows[0]["RESULT_CODE"].ToString().Equals("0"))
                {
                    Frm_TJInfo("失败", "保存信息失败！" + Environment.NewLine + "失败原因：" + faild_info);
                    ResultDb_Flag = false;
                }
                else
                {
                    ResultDb_Flag = true;
                }
            }
            return ResultDb_Flag;
        }
        //保存肿瘤信息
        public Boolean Save_exam_tumour(ref string faild_info, string exam_no)
        {
            Boolean ResultDb_Flag = false;
            EntityModel.exam_tumour exam_tuIns = new EntityModel.exam_tumour();
            exam_tuIns.exam_no = exam_no;
            exam_tuIns.find_date = dt_zlrq.Value.ToString("yyyy-MM-dd");
            exam_tuIns.memo = txt_zlbz.Text.Trim();
            exam_tuIns.parts = txt_zlbz.Text.Trim();
            exam_tuIns.radiate_flag = ((DevComponents.Editors.ComboItem)(cmb_fszl.SelectedItem)).Text.Trim();
            exam_tuIns.sizes = txt_zldx.Text.Trim();
            exam_tuIns.trans_parts = txt_zybw.Text.Trim();
            exam_tuIns.transfer_flag = ((DevComponents.Editors.ComboItem)(cmb_zlzy.SelectedItem)).Text.Trim();
            exam_tuIns.chemotherapy = ((DevComponents.Editors.ComboItem)(cmb_hl.SelectedItem)).Text.Trim();

            string jsonStr = PublicBaseLib.JsonHelper.ToJson(exam_tuIns);
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            //组织post请求参数
            parameters.Add("JsonStr", jsonStr);
            string Retdata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "Process_Tumour", parameters);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(Retdata));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                faild_info = ds.Tables[0].Rows[0]["INFO"].ToString();
                if (ds.Tables[0].Rows[0]["RESULT_CODE"].ToString().Equals("0"))
                {
                    Frm_TJInfo("失败", "保存肿瘤信息失败！" + Environment.NewLine + "失败原因：" + faild_info);
                }
                else
                {
                    ResultDb_Flag = true;
                }
            }
            return ResultDb_Flag;
        }
        //保存标本信息
        public Boolean Save_ExamSpecimens(string exam_no, ref string faild_info)
        {
            Boolean ResultDb_Flag = false;
            faild_info = "";
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            //组织post请求参数
            parameters.Add("sjbbStr", txt_sjbb.Text.Trim());
            parameters.Add("Icount", numericUpDown1.Value.ToString());
            parameters.Add("exam_no", exam_no);
            parameters.Add("doctor_code", Program.User_Code);
            parameters.Add("doctor_name", "");
            string Retdata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "Process_Specimens", parameters);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(Retdata));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                faild_info = ds.Tables[0].Rows[0]["INFO"].ToString();
                if (ds.Tables[0].Rows[0]["RESULT_CODE"].ToString().Equals("0"))
                {
                    Frm_TJInfo("失败", "保存标本信息失败！" + Environment.NewLine + "失败原因：" + faild_info);
                }
                else
                {
                    ResultDb_Flag = true;
                }
            }
            return ResultDb_Flag;
        }
        //保存妇科
        public Boolean Save_Exam_obstetric(ref string faild_info, string exam_no)
        {
            Boolean ResultDb_Flag = false;
            EntityModel.exam_obstetric exam_obs_Ins = new EntityModel.exam_obstetric();
            exam_obs_Ins.exam_no = exam_no;
            exam_obs_Ins.pre_date = dt_preyj.Value.ToString("yyyy-MM-dd");
            exam_obs_Ins.last_date = dt_lastyj.Value.ToString("yyyy-MM-dd");
            exam_obs_Ins.memo = txt_yjbz.Text.Trim();
            exam_obs_Ins.ops_date = txt_rgsj.Text.Trim();
            exam_obs_Ins.ops_unit = txt_jl.Text.Trim();
            exam_obs_Ins.production = txt_cnum.Text.Trim();
            exam_obs_Ins.foetus = txt_tnum.Text.Trim();
            exam_obs_Ins.absolute_flag = ((DevComponents.Editors.ComboItem)(cmb_jjflag.SelectedItem)).Text.Trim();
            exam_obs_Ins.ops_flag = ((DevComponents.Editors.ComboItem)(cmb_rgzl.SelectedItem)).Text.Trim();


            string jsonStr = PublicBaseLib.JsonHelper.ToJson(exam_obs_Ins);
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            //组织post请求参数
            parameters.Add("JsonStr", jsonStr);
            string Retdata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "Process_Obstetric", parameters);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(Retdata));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                faild_info = ds.Tables[0].Rows[0]["INFO"].ToString();
                if (ds.Tables[0].Rows[0]["RESULT_CODE"].ToString().Equals("0"))
                {
                    Frm_TJInfo("失败", "保存妇科信息失败！" + Environment.NewLine + "失败原因：" + faild_info);
                }
                else
                {
                    ResultDb_Flag = true;
                }
            }
            return ResultDb_Flag;
        }

        //新建
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            string Pat_Id = "";
            string Pat_Sqd = "";
            //是否保存成功
            Boolean save_flag = false;
            //保存失败原因
            string faild_info = "";
            //有效性验证
            if (!check_YouXiao())
            {
                return;
            }
            //生成申请单号 病人编号
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            //组织post请求参数
            parameters.Add("msg", "pid");
            string xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetUidInfo", parameters);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmldata);
            Pat_Id = xmlDoc.DocumentElement.InnerText;
            if (Pat_Id.Equals(""))
            {
                Frm_TJInfo("服务器错误", "生成病人ID号失败！");
                return;
            }
            parameters.Clear();
            //组织post请求参数
            parameters.Add("msg", "sqd");
            xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetUidInfo", parameters);
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmldata);
            Pat_Sqd = xmlDoc.DocumentElement.InnerText;
            if (Pat_Sqd.Equals(""))
            {
                Frm_TJInfo("服务器错误", "生成申请单号失败！");
                return;
            }
            //验证是否正确
            parameters.Clear();
            parameters.Add("SqdStr", string.Format("{0}{1}", Program.H_Pre_Char, Pat_Sqd));
            xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "verifySqd", parameters);
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmldata);
            int sl = Convert.ToInt32(xmlDoc.DocumentElement.InnerText);
            if (sl > 0)
            {
                Frm_TJInfo("提示", "病人生成申请单号已经存在。");
                return;
            }
            parameters.Clear();
            parameters.Add("PidStr", string.Format("{0}{1}", Program.H_Pre_Char, Pat_Id));
            xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "verifyPid", parameters);
            xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmldata);
            sl = Convert.ToInt32(xmlDoc.DocumentElement.InnerText);
            if (sl > 0)
            {
                Frm_TJInfo("提示", "病人生成PID已经存在。");
                return;
            }
            //1.病人基本信息
            save_flag = Save_PatMI(ref faild_info, string.Format("{0}{1}", Program.H_Pre_Char, Pat_Id));
            if (!save_flag)
            {
                Frm_TJInfo("提示", "保存病人基本信息失败！");
                return;
            }
            //2.妇科患者
            if (chk_fkhz.Checked == true)
            {
                save_flag = Save_Exam_obstetric(ref faild_info, string.Format("{0}{1}", Program.H_Pre_Char, Pat_Sqd));
                if (!save_flag)
                {
                    return;
                }
            }
            //3.肿瘤患者
            if (chK_zlhz.Checked == true)
            {
                save_flag = Save_exam_tumour(ref faild_info, string.Format("{0}{1}", Program.H_Pre_Char, Pat_Sqd));
                if (!save_flag)
                {
                    return;
                }
            }
            //4.标本信息
            save_flag = Save_ExamSpecimens(string.Format("{0}{1}", Program.H_Pre_Char, Pat_Sqd), ref faild_info);
            if (!save_flag)
            {
                return;
            }
            //5.申请单信息
            save_flag = Save_ExamMaster(ref faild_info, string.Format("{0}{1}", Program.H_Pre_Char, Pat_Sqd), string.Format("{0}{1}", Program.H_Pre_Char, Pat_Id));
            if (!save_flag)
            {
                return;
            }

            //结果提示
            if (save_flag)
            {
                Frm_TJInfo("标本登记成功！", string.Format("\n操作时间:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                //打印纸质申请单
                if (PathologyClient.Properties.Settings.Default.Print_Sqd_Flag && !PathologyClient.Properties.Settings.Default.CurPrinter.Equals(""))
                {
                    EntityModel.Exam_BlSqd BlSqdIns = Program.GetSqdInfo(string.Format("{0}{1}", Program.H_Pre_Char, Pat_Sqd));
                    if (BlSqdIns != null)
                    {
                        BLSqdPrint.PrintBlSQD(BlSqdIns, PathologyClient.Properties.Settings.Default.CurPrinter, 1);
                    }
                }
                //清空数据
                ClearData();
            }
            else
            {
                Frm_TJInfo("标本登记失败！", string.Format("\n失败原因:{0}\n操作时间:{1}", faild_info, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
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
            if (txt_age.Text.Trim() == "" || Microsoft.VisualBasic.Information.IsNumeric(txt_age.Text.Trim()) == false)
            {
                Frm_TJInfo("数据验证", "请输入病人年龄！");
                txt_age.Focus();
                return false;
            }
            if (txt_tel.Text.Trim().Equals(""))
            {
                Frm_TJInfo("数据验证", "请输入病人联系电话！");
                txt_tel.Focus();
                return false;
            }
            string strSource = ((DevComponents.Editors.ComboItem)(cmb_patSource.SelectedItem)).Text.Trim();
            if (strSource.Equals("住院") && txt_inputid.Text.Equals(""))
            {
                Frm_TJInfo("数据验证", "住院患者请输入住院号和床号！");
                txt_inputid.Focus();
                return false;
            }
            if (txt_Sqks.Text.Trim().Equals(""))
            {
                Frm_TJInfo("数据验证", "请输入申请科室！");
                txt_Sqks.Focus();
                return false;
            }
            if (txt_Sqys.Text.Trim().Equals(""))
            {
                Frm_TJInfo("数据验证", "请输入申请医生！");
                txt_Sqys.Focus();
                return false;
            }
            if (txt_lczd.Text.Trim().Equals(""))
            {
                Frm_TJInfo("数据验证", "请输入临床诊断！");
                txt_lczd.Focus();
                return false;
            }
            if (txt_sjbb.Text.Trim().Equals(""))
            {
                Frm_TJInfo("数据验证", "请输入送检标本名称！");
                txt_sjbb.Focus();
                return false;
            }
            return youxiao_flag;
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

        public int calculationDate(DateTime beginDateTime, DateTime endDateTime) { if (beginDateTime > endDateTime) throw new Exception("开始时间应小于或等与结束时间！");          /*计算出生日期到当前日期总月数*/      int Months = endDateTime.Month - beginDateTime.Month + 12 * (endDateTime.Year - beginDateTime.Year);       /*出生日期加总月数后，如果大于当前日期则减一个月*/      int totalMonth = (beginDateTime.AddMonths(Months) > endDateTime) ? Months - 1 : Months;       /*计算整年*/      int fullYear = totalMonth / 12;       /*计算整月*/      int fullMonth = totalMonth % 12;       /*计算天数*/      DateTime changeDate = beginDateTime.AddMonths(totalMonth); double days = (endDateTime - changeDate).TotalDays; return fullYear; }

        //获取拼音
        private void txt_patName_Leave(object sender, EventArgs e)
        {
            if (!txt_patName.Text.Trim().Equals(""))
            {
                txt_Py.Text = PinYinConverter.Get(txt_patName.Text.Trim());
            }
        }
    }

}
