using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ASwartz.WinForms.Controls;
namespace PathologyClient
{
    public partial class FrmPatCardList : DevComponents.DotNetBar.Office2007Form
    {
        public FrmPatCardList()
        {
            InitializeComponent();
        }

        private void FrmPatCardList_Load(object sender, EventArgs e)
        {
            InitDCCardListViewTemplate();
            lvwPatients.ShowCardShade = true;
            lvwPatients.MouseClick += new MouseEventHandler(_cardListViewControl_MouseClick);
            btnRefresh_Click(null, null);
        }
        private void InitDCCardListViewTemplate()
        {
            lvwPatients.CardBorderColor = Color.Blue;
            lvwPatients.CardBorderWith = 2;
            lvwPatients.ImageAnimateInterval = 100;
            this.lvwPatients.CardWidth = 133;
            this.lvwPatients.CardHeight = 163;
            ASCardStringItem ChuangWeiHao = this.lvwPatients.CardTemplate.AddString("BedID", null, 80, 10, 80, 20);
            ChuangWeiHao.Align = StringAlignment.Near;
            ChuangWeiHao.FontName = "宋体";
            ChuangWeiHao.FontSize = 14;
            ChuangWeiHao.FontStyle = FontStyle.Bold;
            ChuangWeiHao.Color = Color.Blue;

            ASCardStringItem NianLing = this.lvwPatients.CardTemplate.AddString("Age", null, 80, 30, 80, 20);
            NianLing.Align = StringAlignment.Near;
            NianLing.FontName = "宋体";
            NianLing.FontSize = 12;
            NianLing.FontStyle = FontStyle.Bold;

            ASCardStringItem XingBie = this.lvwPatients.CardTemplate.AddString("SexText", null, 80, 50, 80, 20);
            XingBie.Align = StringAlignment.Near;
            XingBie.FontName = "宋体";
            XingBie.FontSize = 12;
            XingBie.FontStyle = FontStyle.Bold;

            ASCardStringItem XingMing = this.lvwPatients.CardTemplate.AddString("Name", null, 2, 70, 130, 20);
            XingMing.Align = StringAlignment.Near;
            XingMing.FontName = "宋体";
            XingMing.FontSize = 10;
            XingMing.FontStyle = FontStyle.Bold;

            ASCardStringItem FeiBie = this.lvwPatients.CardTemplate.AddString("PayType", null, 2, 88, 130, 20);
            FeiBie.Align = StringAlignment.Near;
            FeiBie.FontName = "宋体";
            FeiBie.FontSize = 10;

            ASCardStringItem YuJiaoJin_Label = this.lvwPatients.CardTemplate.AddString(null, "总金额：", 2, 106, 75, 20);
            YuJiaoJin_Label.Align = StringAlignment.Near;
            YuJiaoJin_Label.FontName = "宋体";
            YuJiaoJin_Label.FontSize = 10;

            ASCardStringItem YuJiaoJin = this.lvwPatients.CardTemplate.AddString("TotalCost", null, 65, 105, 55, 20);
            YuJiaoJin.Align = StringAlignment.Near;
            YuJiaoJin.FontName = "宋体";
            YuJiaoJin.FontSize = 10;

            ASCardStringItem ShiJiFeiYong_Label = this.lvwPatients.CardTemplate.AddString(null, "实际费用：", 2, 124, 75, 20);
            ShiJiFeiYong_Label.Align = StringAlignment.Near;
            ShiJiFeiYong_Label.FontName = "宋体";
            ShiJiFeiYong_Label.FontSize = 10;

            ASCardStringItem ShiJiFeiYong = this.lvwPatients.CardTemplate.AddString("SpendCost", null, 65, 123, 55, 20);
            ShiJiFeiYong.Align = StringAlignment.Near;
            ShiJiFeiYong.FontName = "宋体";
            ShiJiFeiYong.FontSize = 10;

            ASCardStringItem ShiJiYuE_Label = this.lvwPatients.CardTemplate.AddString(null, "实际余额：", 2, 142, 75, 20);
            ShiJiYuE_Label.Align = StringAlignment.Near;
            ShiJiYuE_Label.FontName = "宋体";
            ShiJiYuE_Label.FontSize = 10;

            ASCardStringItem ShiJiYuE = this.lvwPatients.CardTemplate.AddString("Balance", null, 65, 141, 55, 20);
            ShiJiYuE.Align = StringAlignment.Near;
            ShiJiYuE.FontName = "宋体";
            ShiJiYuE.FontSize = 10;

            this.lvwPatients.CardTemplate.AddImage(
                "FaceImage",
                 null,
                3, 3, 71, 63);

            this.lvwPatients.CardTemplate.AddImage(
                "hulidengji",
                 null,
                0, 0, 133, 177);

            this.lvwPatients.CardTemplate.AddImage(
                "KangShengSu",
                 null,
                35, 90, 55, 61);

            this.lvwPatients.ItemTooltipDataFieldName = "EnterHospitalTime";

            this.lvwPatients.TooltipWidth = 290;
            this.lvwPatients.TooltipHeight = 163;
            ASCardStringItem YaoWuGuoMinShi_Label = this.lvwPatients.TooltipContentItems.AddString(null, "药物过敏史:", 5, 10, 200, 30);
            YaoWuGuoMinShi_Label.Color = Color.Red;
            YaoWuGuoMinShi_Label.FontSize = 14;
            YaoWuGuoMinShi_Label.FontStyle = FontStyle.Bold;


            ASCardStringItem YaoWuGuoMinShi = this.lvwPatients.TooltipContentItems.AddString("Allergy", null, 115, 10, 200, 30);
            YaoWuGuoMinShi.Color = Color.Red;
            YaoWuGuoMinShi.FontSize = 14;
            YaoWuGuoMinShi.FontStyle = FontStyle.Bold;

            ASCardStringItem ZhuYuanHao_Label = this.lvwPatients.TooltipContentItems.AddString(null, "住院号:", 5, 40, 70, 20);
            ZhuYuanHao_Label.FontSize = 12;
            ZhuYuanHao_Label.FontStyle = FontStyle.Bold;

            ASCardStringItem ZhuYuanHao = this.lvwPatients.TooltipContentItems.AddString("MRID", null, 75, 39, 80, 20);
            ZhuYuanHao.FontSize = 12;
            ZhuYuanHao.FontStyle = FontStyle.Bold;

            ASCardStringItem BingFangHao_Label = this.lvwPatients.TooltipContentItems.AddString(null, "病房号:", 160, 40, 70, 20);
            BingFangHao_Label.FontSize = 12;
            BingFangHao_Label.FontStyle = FontStyle.Bold;

            ASCardStringItem BingFangHao = this.lvwPatients.TooltipContentItems.AddString("RoomID", null, 220, 39, 50, 20);
            BingFangHao.FontSize = 12;
            BingFangHao.FontStyle = FontStyle.Bold;

            ASCardStringItem RuYuanShiJian_Label = this.lvwPatients.TooltipContentItems.AddString(null, "入院时间:", 5, 65, 80, 20);
            RuYuanShiJian_Label.FontSize = 12;
            RuYuanShiJian_Label.FontStyle = FontStyle.Bold;

            ASCardStringItem RuYuanShiJian = this.lvwPatients.TooltipContentItems.AddString("EnterHospitalTime", null, 85, 64, 190, 20);
            RuYuanShiJian.FontSize = 12;
            RuYuanShiJian.FontStyle = FontStyle.Bold;

            ASCardStringItem ZhuYuanYiSheng_Label = this.lvwPatients.TooltipContentItems.AddString(null, "住院医生:", 5, 90, 80, 20);
            ZhuYuanYiSheng_Label.FontSize = 12;
            ZhuYuanYiSheng_Label.FontStyle = FontStyle.Bold;

            ASCardStringItem ZhuYuanYiSheng = this.lvwPatients.TooltipContentItems.AddString("DoctoryName", null, 85, 90, 190, 20);
            ZhuYuanYiSheng.FontSize = 12;
            ZhuYuanYiSheng.FontStyle = FontStyle.Bold;

            ASCardStringItem FuZeHuShi_Label = this.lvwPatients.TooltipContentItems.AddString(null, "负责护士:", 5, 115, 80, 20);
            FuZeHuShi_Label.FontSize = 12;
            FuZeHuShi_Label.FontStyle = FontStyle.Bold;

            ASCardStringItem FuZeHuShi = this.lvwPatients.TooltipContentItems.AddString("NurseName", null, 85, 115, 190, 20);
            FuZeHuShi.FontSize = 12;
            FuZeHuShi.FontStyle = FontStyle.Bold;

            ASCardStringItem LianXiDianHua_Label = this.lvwPatients.TooltipContentItems.AddString(null, "联系电话:", 5, 140, 80, 20);
            LianXiDianHua_Label.FontSize = 12;
            LianXiDianHua_Label.FontStyle = FontStyle.Bold;

            ASCardStringItem LianXiDianHua = this.lvwPatients.TooltipContentItems.AddString("Phone", null, 85, 140, 190, 20);
            LianXiDianHua.FontSize = 12;
            LianXiDianHua.FontStyle = FontStyle.Bold;
        }

        private PatientInfo2 _CurrentPatient = null;
        void _cardListViewControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                _CurrentPatient = null;
                ASCardListViewItem item = this.lvwPatients.GetItemAt(e.X, e.Y);
                if (item != null)
                {
                    _CurrentPatient = item.DataBoundItem as PatientInfo2;
                    myContextMenu.Show(this.lvwPatients, e.X, e.Y);
                }
            }
        }


        private void cmViewDetails_Click(object sender, EventArgs e)
        {
            if (this._CurrentPatient != null)
            {
                MessageBox.Show(this, "查看" + _CurrentPatient.Name + "的详细信息");
            }
        }

        private void cmViewDoc_Click(object sender, EventArgs e)
        {
            if (this._CurrentPatient != null)
            {
                MessageBox.Show(this, "查看" + _CurrentPatient.Name + "的病历");
            }
        }

        private void cmOut_Click(object sender, EventArgs e)
        {
            if (this._CurrentPatient != null)
            {
                MessageBox.Show(this, "病人 " + _CurrentPatient.Name + " 出院");
            }
        }


        private List<PatientInfo2> GetPatientEntities()
        {
            List<PatientInfo2> entities = new List<PatientInfo2>();

            for (int iCount = 0; iCount < 100; iCount++)
            {
                entities.Add(new PatientInfo2()
                {
                    BedID = "B111",
                    Name = "萝莉11",
                    Allergy = "青霉素",
                    RoomID = "001",
                    NurseName = "王护士",
                    Sex = PatientSexType.Female,
                    DepartmentName = "儿科",
                    MRID = "BL011",
                    Age = 12,
                    Birthday = DateTime.Now,
                    DoctorID = "AA001",
                    LeaveHospitalTime = DateTime.Now,
                    SpendCost = 1234,
                    TotalCost = 12434,
                    Balance = 345,
                    Phone = "12353434234234",
                    DoctoryName = "王二麻"

                });
                entities.Add(new PatientInfo2()
                {
                    BedID = "B222",
                    Name = "萝莉22",
                    Allergy = "青霉素",
                    RoomID = "001",
                    NurseName = "李护士",
                    Sex = PatientSexType.Female,
                    DepartmentName = "妇科",
                    MRID = "BL011",
                    Age = 13,
                    Birthday = DateTime.Now,
                    DoctorID = "AA002",

                    LeaveHospitalTime = DateTime.Now,
                    SpendCost = 1234,
                    TotalCost = 12434,
                    Balance = 345,
                    HospitalizationState = HospitalizationState.LeaveHospital,
                    Phone = "66353432324234",
                    DoctoryName = "诸葛亮"
                });
                entities.Add(new PatientInfo2()
                {
                    BedID = "B333",
                    Name = "萝莉33",
                    Allergy = "青霉素",
                    RoomID = "001",
                    NurseName = "张虎师",
                    Sex = PatientSexType.Female,
                    DepartmentName = "骨科",
                    MRID = "BL011",
                    Age = 14,
                    Birthday = DateTime.Now,
                    DoctorID = "AA003",
                    HospitalizationState = HospitalizationState.Visit,
                    Phone = "134353344231",
                    DoctoryName = "菩提老祖"
                });
                entities.Add(new PatientInfo2()
                {
                    BedID = "B444",
                    Name = "萝莉44",
                    Allergy = "青霉素",
                    RoomID = "001",
                    NurseName = "",
                    Sex = PatientSexType.Female,
                    MRID = "BL011",
                    Age = 15,
                    Birthday = DateTime.Now,
                    DoctorID = "AA004",
                    HospitalizationState = HospitalizationState.Hospitalized,
                    DoctoryName = "华佗"
                });
                entities.Add(new PatientInfo2()
                {
                    BedID = "B555",
                    Name = "萝莉55",
                    Allergy = "青霉素",
                    RoomID = "001",
                    NurseName = "",
                    Sex = PatientSexType.Female,
                    MRID = "BL011",
                    Age = 16,
                    Birthday = DateTime.Now,
                    DoctorID = "AA005",
                    DoctoryName = "神农"

                });
                entities.Add(new PatientInfo2()
                {
                    BedID = "B666",
                    Name = "萝莉66",
                    Allergy = "青霉素",
                    RoomID = "001",
                    NurseName = "",
                    Sex = PatientSexType.Female,
                    MRID = "BL011",
                    Age = 17,
                    Birthday = DateTime.Now,
                    DoctorID = "AA006",

                });
                entities.Add(new PatientInfo2()
                {
                    BedID = "B777",
                    Name = "萝莉77",
                    Allergy = "青霉素",
                    RoomID = "001",
                    NurseName = "",
                    Sex = PatientSexType.Female,
                    MRID = "BL011",
                    Age = 18,
                    Birthday = DateTime.Now,
                    DoctorID = "AA0071",

                });
                entities.Add(new PatientInfo2()
                {
                    BedID = "B888",
                    Name = "萝莉88",
                    Allergy = "",
                    RoomID = "001",
                    NurseName = "",
                    Sex = PatientSexType.Female,
                    MRID = "BL011",
                    Age = 19,
                    Birthday = DateTime.Now,
                    DoctorID = "AA0019",
                });
                entities.Add(new PatientInfo2()
                {
                    BedID = "B111",
                    Name = "萝莉11",
                    Allergy = "青霉素",
                    RoomID = "001",
                    NurseName = "",
                    Sex = PatientSexType.Female,
                    MRID = "BL011",
                    Age = 12,
                    Birthday = DateTime.Now,
                    DoctorID = "AA001",
                });
                entities.Add(new PatientInfo2()
                {
                    BedID = "B222",
                    Name = "萝莉22",
                    Allergy = "",
                    RoomID = "001",
                    NurseName = "",
                    Sex = PatientSexType.Female,
                    MRID = "BL011",
                    Age = 13,
                    Birthday = DateTime.Now,
                    DoctorID = "AA002",
                });
                entities.Add(new PatientInfo2()
                {
                    BedID = "B333",
                    Name = "萝莉33",
                    Allergy = "",
                    RoomID = "001",
                    NurseName = "",
                    Sex = PatientSexType.Female,
                    MRID = "BL011",
                    Age = 14,
                    Birthday = DateTime.Now,
                    DoctorID = "AA003",
                });
            }
            Image image1 = OpenAppImage("luoli.jpg");
            Image image2 = OpenAppImage("luoliA.jpg");
            Image image3 = OpenAppImage("Test.gif");

            foreach (PatientInfo2 entity in entities)
            {
                switch (entity.HospitalizationState)
                {
                    case HospitalizationState.Hospitalized:
                        {
                            entity.FaceImage = image2;
                        }
                        break;
                    case HospitalizationState.LeaveHospital:
                        {
                            entity.FaceImage = image3;
                        }
                        break;
                    case HospitalizationState.Visit:
                        {
                            entity.FaceImage = image1;
                        }
                        break;
                }
            }
            return entities;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            List<PatientInfo2> ds = GetPatientEntities();
            int tick = System.Environment.TickCount;
            this.lvwPatients.DataSource = ds;
            tick = Environment.TickCount - tick;
            MessageBox.Show(this, "填充了" + this.lvwPatients.Items.Count + " 个项目,耗时 " + tick + " 毫秒");
        }

        private void btnSetBackColor_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            foreach (ASCardListViewItem item in lvwPatients.Items)
            {
                if (rnd.NextDouble() < 0.2)
                {
                    item.BackColor = Color.Red;
                    item.Invalidate();
                }
            }
        }

        private void btnHighlight_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            foreach (ASCardListViewItem item in lvwPatients.Items)
            {
                if (rnd.NextDouble() < 0.3)
                {
                    item.Highlight = true;
                    item.Invalidate();
                }
            }
        }

        private void btnBlink_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            foreach (ASCardListViewItem item in lvwPatients.Items)
            {
                if (rnd.NextDouble() < 0.1)
                {
                    item.Blink = true;
                }
            }
        }

        private void btnPush_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            foreach (ASCardListViewItem item in lvwPatients.Items)
            {
                if (rnd.NextDouble() < 0.2)
                {
                    item.BorderColor = Color.Red;
                    item.BorderWidth = 3;
                    item.Invalidate();
                }
            }
        }

        private void btnCardBG_Click(object sender, EventArgs e)
        {
            this.lvwPatients.CardBackgroundImage = OpenAppImage("bg.gif");
        }
        private Image OpenAppImage(string fileName)
        {
            fileName = System.IO.Path.Combine(Application.StartupPath, fileName);
            if (System.IO.File.Exists(fileName))
            {
                return Image.FromFile(fileName);
            }
            return null;
        }
    }

    public enum PatientSexType
    {
        /// <summary>
        /// 女性
        /// </summary>

        Female = 0,
        /// <summary>
        /// 男性
        /// </summary>

        Male = 1,
        /// <summary>
        /// 未知
        /// </summary>

        Unknow = 2
    }

    public class PatientInfo2
    {
        /// <summary>
        /// 初始化对象
        /// </summary>
        public PatientInfo2()
        {
        }



        private string _ID = null;
        /// <summary>
        /// 病人编号
        /// </summary>
        public string ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    _ID = value;

                }
            }
        }

        [NonSerialized]
        private Image _FaceImage = null;
        /// <summary>
        /// 图标图片
        /// </summary>
        public Image FaceImage
        {
            get { return _FaceImage; }
            set { _FaceImage = value; }
        }

        private string _Name = null;
        /// <summary>
        /// 姓名
        /// </summary>

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        private string _MRID = null;
        /// <summary>
        /// 病历号
        /// </summary>

        public string MRID
        {
            get
            {
                return _MRID;
            }
            set
            {
                if (_MRID != value)
                {
                    _MRID = value;

                }
            }
        }

        private string _Diagnose = null;
        /// <summary>
        /// 诊断
        /// </summary>

        public string Diagnose
        {
            get { return _Diagnose; }
            set { _Diagnose = value; }
        }

        private string _Area = null;
        /// <summary>
        /// 病区
        /// </summary>

        public string Area
        {
            get { return _Area; }
            set { _Area = value; }
        }

        private string _DepartmentID = null;
        /// <summary>
        /// 所属科室编号
        /// </summary>

        public string DepartmentID
        {
            get { return _DepartmentID; }
            set { _DepartmentID = value; }
        }

        private string _DepartmentName = null;
        /// <summary>
        /// 所属部门和科室的名称
        /// </summary>

        public string DepartmentName
        {
            get { return _DepartmentName; }
            set { _DepartmentName = value; }
        }

        private string _DepartmentName2 = null;
        /// <summary>
        /// 所属部门和科室的名称
        /// </summary>
        public string DepartmentName2
        {
            get { return _DepartmentName2; }
            set { _DepartmentName2 = value; }
        }

        private string _RoomID = null;
        /// <summary>
        /// 病房号
        /// </summary>
        public string RoomID
        {
            get { return _RoomID; }
            set { _RoomID = value; }
        }
        private string _BedID = null;
        public string BedID
        {
            get { return _BedID; }
            set { _BedID = value; }
        }

        private string _BedID2 = null;
        public string BedID2
        {
            get { return _BedID2; }
            set { _BedID2 = value; }
        }

        private PatientSexType _Sex = PatientSexType.Unknow;
        /// <summary>
        /// 性别
        /// </summary>
        public PatientSexType Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }

        private string _SexText = null;
        /// <summary>
        /// 性别文本
        /// </summary>
        public string SexText
        {
            get { return _SexText; }
            set { _SexText = value; }
        }

        private float _Age = 0;
        /// <summary>
        /// 年龄
        /// </summary>

        public float Age
        {
            get { return _Age; }
            set { _Age = value; }
        }

        private string _NationName = null;
        /// <summary>
        /// 民族名称
        /// </summary>
        [DefaultValue(null)]
        public string NationName
        {
            get { return _NationName; }
            set { _NationName = value; }
        }

        private string _NationCode = null;
        /// <summary>
        /// 民族编码
        /// </summary>
        [DefaultValue(null)]

        public string NationCode
        {
            get { return _NationCode; }
            set { _NationCode = value; }
        }

        private string _CountryName = null;
        /// <summary>
        /// 国籍名称
        /// </summary>
        [DefaultValue(null)]

        public string CountryName
        {
            get { return _CountryName; }
            set { _CountryName = value; }
        }

        private string _CountryCode = null;
        /// <summary>
        /// 国籍编码
        /// </summary>
        [DefaultValue(null)]

        public string CountryCode
        {
            get { return _CountryCode; }
            set { _CountryCode = value; }
        }

        private string _IdentityID = null;
        /// <summary>
        /// 身份证编号
        /// </summary>
        [DefaultValue(null)]

        public string IdentityID
        {
            get { return _IdentityID; }
            set { _IdentityID = value; }
        }

        private string _IdentityType = null;
        /// <summary>
        /// 身份证类型
        /// </summary>
        [DefaultValue(null)]

        public string IdentityType
        {
            get { return _IdentityType; }
            set { _IdentityType = value; }
        }

        private DateTime _EnterHospitalTime = new DateTime(1900, 1, 1);
        /// <summary>
        /// 入院时间
        /// </summary>

        public DateTime EnterHospitalTime
        {
            get { return _EnterHospitalTime; }
            set { _EnterHospitalTime = value; }
        }

        private DateTime _EnterDepartmentTime = new DateTime(1900, 1, 1);
        /// <summary>
        /// 入科时间
        /// </summary>

        public DateTime EnterDepartmentTime
        {
            get { return _EnterDepartmentTime; }
            set { _EnterDepartmentTime = value; }
        }


        private DateTime _LeaveDepartmentTime = new DateTime(1900, 1, 1);
        /// <summary>
        /// 出科时间
        /// </summary>

        public DateTime LeaveDepartmentTime
        {
            get { return _LeaveDepartmentTime; }
            set { _LeaveDepartmentTime = value; }
        }

        private DateTime _LeaveHospitalTime = new DateTime(1900, 1, 1);
        /// <summary>
        /// 出院时间
        /// </summary>

        public DateTime LeaveHospitalTime
        {
            get { return _LeaveHospitalTime; }
            set { _LeaveHospitalTime = value; }
        }

        private DateTime _DeadTime = new DateTime(1900, 1, 1);
        /// <summary>
        /// 死亡时间
        /// </summary>

        public DateTime DeadTime
        {
            get { return _DeadTime; }
            set { _DeadTime = value; }
        }

        private string _NurseLevelCode = null;
        /// <summary>
        /// 护理等级编码
        /// </summary>

        public string NurseLevelCode
        {
            get { return _NurseLevelCode; }
            set { _NurseLevelCode = value; }
        }

        private string _NurseLevel = null;
        /// <summary>
        /// 护理等级
        /// </summary>

        public string NurseLevel
        {
            get { return _NurseLevel; }
            set { _NurseLevel = value; }
        }

        private string _NurseID = null;
        /// <summary>
        /// 责任护士编号
        /// </summary>

        public string NurseID
        {
            get { return _NurseID; }
            set { _NurseID = value; }
        }

        private string _NurseName = null;
        /// <summary>
        /// 责任护士名称
        /// </summary>

        public string NurseName
        {
            get { return _NurseName; }
            set { _NurseName = value; }
        }

        private string _DoctorID = null;
        /// <summary>
        /// 责任医生编号
        /// </summary>

        public string DoctorID
        {
            get { return _DoctorID; }
            set { _DoctorID = value; }
        }

        private string _DoctoryName = null;
        /// <summary>
        /// 责任医生名称
        /// </summary>

        public string DoctoryName
        {
            get { return _DoctoryName; }
            set { _DoctoryName = value; }
        }

        private string _HomeAddress = null;
        /// <summary>
        /// 家庭住址
        /// </summary>

        public string HomeAddress
        {
            get { return _HomeAddress; }
            set { _HomeAddress = value; }
        }

        private string _Phone = null;
        /// <summary>
        /// 联系电话
        /// </summary>

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        private string _MarriageCode = null;
        /// <summary>
        /// 婚姻状态编号
        /// </summary>

        public string MarriageCode
        {
            get { return _MarriageCode; }
            set { _MarriageCode = value; }
        }

        private string _Marriage = null;
        /// <summary>
        /// 婚姻状态
        /// </summary>

        public string Marriage
        {
            get { return _Marriage; }
            set { _Marriage = value; }
        }

        private DateTime _Birthday = new DateTime(1900, 1, 1);
        /// <summary>
        /// 出生时间
        /// </summary>'

        public DateTime Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; }
        }

        private string _NativePlace = null;
        /// <summary>
        /// 籍贯
        /// </summary>

        public string NativePlace
        {
            get { return _NativePlace; }
            set { _NativePlace = value; }
        }
        private string _BirthAddress = null;
        /// <summary>
        /// 出生地
        /// </summary>

        public string BirthAddress
        {
            get { return _BirthAddress; }
            set { _BirthAddress = value; }
        }

        private string _ProfessionCode = null;
        /// <summary>
        /// 职业编码
        /// </summary>

        public string ProfessionCode
        {
            get { return _ProfessionCode; }
            set { _ProfessionCode = value; }
        }

        private string _Profession = null;
        /// <summary>
        /// 职业
        /// </summary>

        public string Profession
        {
            get { return _Profession; }
            set { _Profession = value; }
        }

        private string _WorkUnit = null;
        /// <summary>
        /// 工作单位
        /// </summary>

        public string WorkUnit
        {
            get { return _WorkUnit; }
            set { _WorkUnit = value; }
        }

        private string _WorkAddress = null;
        /// <summary>
        /// 工作地址
        /// </summary>

        public string WorkAddress
        {
            get { return _WorkAddress; }
            set { _WorkAddress = value; }
        }

        private string _EnterHospitalSourceCode = null;
        /// <summary>
        /// 入院来源编号
        /// </summary>

        public string EnterHospitalSourceCode
        {
            get { return _EnterHospitalSourceCode; }
            set { _EnterHospitalSourceCode = value; }
        }

        private string _EnterHospitalSource = null;
        /// <summary>
        /// 入院来源方式
        /// </summary>

        public string EnterHospitalSource
        {
            get { return _EnterHospitalSource; }
            set { _EnterHospitalSource = value; }
        }
        private string _Occupation = null;
        /// <summary>
        /// 职位
        /// </summary>

        public string Occupation
        {
            get { return _Occupation; }
            set { _Occupation = value; }
        }
        private string _PayTypeCode = null;
        /// <summary>
        /// 支付类型代码
        /// </summary>

        public string PayTypeCode
        {
            get { return _PayTypeCode; }
            set { _PayTypeCode = value; }
        }

        private string _PayType = null;
        /// <summary>
        /// 支付类型
        /// </summary>

        public string PayType
        {
            get { return _PayType; }
            set { _PayType = value; }
        }

        private decimal _TotalCost = 0;
        /// <summary>
        /// 总金额
        /// </summary>

        public decimal TotalCost
        {
            get { return _TotalCost; }
            set { _TotalCost = value; }
        }

        private decimal _SpendCost = 0;
        /// <summary>
        /// 花费的金额
        /// </summary>
        public decimal SpendCost
        {
            get { return _SpendCost; }
            set { _SpendCost = value; }
        }

        private decimal _Balance = 0;
        /// <summary>
        /// 余额
        /// </summary>

        public decimal Balance
        {
            get { return _Balance; }
            set { _Balance = value; }
        }

        private string _Allergy = null;
        /// <summary>
        /// 药物过敏
        /// </summary>

        public string Allergy
        {
            get { return _Allergy; }
            set { _Allergy = value; }
        }

        private string _LinkManName = null;
        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string LinkManName
        {
            get { return _LinkManName; }
            set { _LinkManName = value; }
        }

        private string _LinkManAddress = null;
        /// <summary>
        /// 联系人地址
        /// </summary>

        public string LinkManAddress
        {
            get { return _LinkManAddress; }
            set { _LinkManAddress = value; }
        }

        private HospitalizationState _HospitalizationState = HospitalizationState.Visit;

        public HospitalizationState HospitalizationState
        {
            get { return _HospitalizationState; }
            set { _HospitalizationState = value; }
        }

    }

    public enum HospitalizationState
    {
        /// <summary>
        /// 就诊
        /// </summary>
        Visit = 0,
        /// <summary>
        /// 出院
        /// </summary>
        LeaveHospital = 1,
        /// <summary>
        /// 住院
        /// </summary>
        Hospitalized = 2
    }

}
