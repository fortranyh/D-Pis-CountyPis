using System;
using System.Data;
using System.Windows.Forms;

namespace PIS_Sys
{
    public partial class PatientInfo : UserControl
    {
        public PatientInfo()
        {

            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

        }
        public string GetSjdw()
        {
            return txt_sjdw.Text.Trim();
        }

        public string GetLczd()
        {
            return txt_lczd.Text.Trim();
        }

        public void SetPatinfo(string patient_id, string exam_no)
        {
            //检查类型
            radioButton1_CheckedChanged(null, null);
            //1.赋值病人基本信息
            SetPatient_Info(patient_id);
            //2.赋值申请单信息
            SetSqd_Info(exam_no);

            //4.赋值妇科肿瘤信息
            SetFk_Info(exam_no);
            SetZl_Info(exam_no);
        }

        //1.赋值病人基本信息
        private void SetPatient_Info(string patient_id)
        {
            DBHelper.BLL.exam_pat_mi Pat_ins = new DBHelper.BLL.exam_pat_mi();
            DBHelper.Model.exam_pat_mi MPat_ins = Pat_ins.GetPatientInfo(patient_id);
            if (MPat_ins != null)
            {
                txt_birth.Text = MPat_ins.date_of_birth;
                txt_patid.Text = MPat_ins.patient_id;

                txt_patName.Text = MPat_ins.patient_name;
                txt_sex.Text = MPat_ins.sex;
                txt_birth.Text = "";
                txt_mz.Text = MPat_ins.nation;
                txt_tel.Text = MPat_ins.phone_number;
                txt_sfz.Text = MPat_ins.identity;
                txt_Hkh.Text = MPat_ins.hospital_card;
                current_place.Text = MPat_ins.current_place;
            }

        }
        //2.赋值申请单信息
        private void SetSqd_Info(string exam_no)
        {
            DBHelper.BLL.exam_master mas_ins = new DBHelper.BLL.exam_master();
            DBHelper.Model.exam_master Mmas_ins = mas_ins.GetExam_MasterInfo(exam_no);
            if (Mmas_ins != null)
            {
                txt_age.Text = Mmas_ins.age + Mmas_ins.ageUint;
                txt_sqd.Text = Mmas_ins.exam_no;
                txt_sjdw.Text = Mmas_ins.submit_unit;
                txt_items.Text = Mmas_ins.examItems;
                txt_dept.Text = Mmas_ins.req_dept;
                txt_sqys.Text = Mmas_ins.req_physician;
                txt_Sqrq.Text = Mmas_ins.req_date_time;
                txt_inputid.Text = Mmas_ins.input_id;
                txt_costs.Text = Mmas_ins.costs;
                txt_BedNo.Text = Mmas_ins.bed_no;
                this.txt_patSource.Text = Mmas_ins.patient_source;
                txt_ice.Text = (Mmas_ins.ice_flag == 1 ? "是" : "否");
                if (Mmas_ins.inout_type == 1)
                {
                    this.radioButton2.Checked = true;
                }
                else
                {
                    this.radioButton1.Checked = true;
                }
                cmb_type.SelectedValue = Mmas_ins.modality;

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
                    txt_lastyj.Text = Mins.last_date;
                    txt_preyj.Text = Mins.pre_date;
                    txt_rgyj.Text = Mins.ops_flag;
                    txt_yjjj.Text = Mins.absolute_flag;
                    txt_jl.Text = Mins.ops_unit;
                    txt_tnum.Text = Mins.foetus;
                    txt_cnum.Text = Mins.production;
                    txt_yjbz.Text = Mins.memo;
                    txt_yjsj.Text = Mins.ops_date;
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
                    txt_zlfxrq.Text = Mins.find_date;
                    txt_zlbw.Text = Mins.parts;
                    txt_zlbz.Text = Mins.memo;
                    txt_zldx.Text = Mins.sizes;
                    txt_sfzy.Text = Mins.transfer_flag;
                    txt_zlhl.Text = Mins.chemotherapy;
                    txt_zlfszl.Text = Mins.radiate_flag;
                    txt_zlzybw.Text = Mins.trans_parts;
                }
            }
        }
        private void ClearData()
        {
            current_place.Text = "";
            txt_BedNo.Text = "";
            txt_inputid.Text = "";
            cmb_type.Text = "";
            txt_patSource.Text = "";
            txt_birth.Text = "";
            txt_sex.Text = "";
            txt_patName.Text = "";
            txt_age.Text = "";
            txt_items.Text = "";
            txt_mz.Text = "";
            txt_sfz.Text = "";
            txt_tel.Text = "";
            txt_Hkh.Text = "";

            txt_sqd.Text = "";
            txt_patid.Text = "";

            txt_Sqrq.Text = "";
            txt_dept.Text = "";
            txt_sqys.Text = "";

            txt_sjdw.Text = "";
            txt_ice.Text = "";
            txt_bszy.Text = "";
            txt_sssj.Text = "";
            txt_lczd.Text = "";

            txt_crb.Text = "";


            chk_fkhz.Checked = false;
            chK_zlhz.Checked = false;

            txt_zlfxrq.Text = "";
            txt_zlbw.Text = "";
            txt_zlbz.Text = "";
            txt_zldx.Text = "";
            txt_sfzy.Text = "";
            txt_zlhl.Text = "";
            txt_zlfszl.Text = "";
            txt_zlzybw.Text = "";

            txt_tnum.Text = "";
            txt_cnum.Text = "";
            txt_preyj.Text = "";
            txt_lastyj.Text = "";
            txt_rgyj.Text = "";
            txt_yjsj.Text = "";
            txt_jl.Text = "";
            txt_yjbz.Text = "";
            txt_yjjj.Text = "";

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                DBHelper.BLL.exam_type exam_type_ins = new DBHelper.BLL.exam_type();
                DataSet ds = exam_type_ins.GetDsExam_Type(0, false, Program.workstation_type_db);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    cmb_type.DataSource = ds.Tables[0];
                    cmb_type.DisplayMember = "modality_cn";
                    cmb_type.ValueMember = "modality";
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                DBHelper.BLL.exam_type exam_type_ins = new DBHelper.BLL.exam_type();
                DataSet ds = exam_type_ins.GetDsExam_Type(1, false, Program.workstation_type_db);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    cmb_type.DataSource = ds.Tables[0];
                    cmb_type.DisplayMember = "modality_cn";
                    cmb_type.ValueMember = "modality";
                }
            }
        }


    }
}
