using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PIS_WinSystem.dagl
{
    public partial class Frm_Myzhsj : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Myzhsj()
        {
            InitializeComponent();
        }

        private void Frm_Myzhsj_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            panel1.MinRowHeight = 30;
            panel1.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].ReadOnly = true;
                panel1.Columns[i].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel1.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //
            this.dateTimePicker1.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
            //显示本年度信息
            RefreshData();
        }

        private void RefreshData()
        {
            if (dateTimePicker1.Value > dtEnd.Value)
            {
                MessageBox.Show("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" date>='{0} 00:00:00' and date<='{1} 23:59:59'", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DataTable dt = new DataTable();
            DBHelper.BLL.Sjzk_info ins = new DBHelper.BLL.Sjzk_info();
            dt = ins.GetInfoMyzhsj(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                //激活列表
                tj_func(dt);
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
            }
        }

        //查询
        private void buttonX4_Click_1(object sender, EventArgs e)
        {
            RefreshData();
        }
        //调出添加窗体
        private void buttonX6_Click(object sender, EventArgs e)
        {
            Frm_Myzhsj_add ins = new Frm_Myzhsj_add();
            ins.BringToFront();
            if (ins.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }
        }
        string id = "";
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
                //
                id = Row.Cells["id"].Value.ToString();
                textBox8.Text = Row.Cells["name"].Value.ToString();
                comboBox1.Text = Row.Cells["result"].Value.ToString();
                dateTimePicker2.Value = Convert.ToDateTime(Row.Cells["date"].Value.ToString());
                textBox5.Text = Row.Cells["bz"].Value.ToString();

            }
        }

        //计数
        public void tj_func(DataTable dt)
        {

            int rows = dt.Rows.Count;
            int gfh_int = 0;
            for (int i = 0; i < rows; i++)
            {
                //是否合格
                string bbgfh_flag = dt.Rows[i]["result"].ToString();
                if (bbgfh_flag.Equals("合格"))
                {
                    gfh_int += 1;
                }
                //最后一行开始赋值
                if (i == rows - 1)
                {
                    if (superGridControl1.PrimaryGrid.Footer == null)
                    {
                        superGridControl1.PrimaryGrid.Footer = new GridFooter();
                    }
                    float yxl = 0;
                    if (rows != 0)
                    {
                        yxl = (gfh_int * 100 / rows);
                    }
                    string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
                    superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='blue'><b>{0}</b></font> 条记录:合格<font color='blue'><b>{1}</b></font>,其他<font color='blue'><b>{2}</b></font>,免疫组化染色室间质评合格率<font color='blue'><b>{3}% </b></font>", rows, gfh_int, (rows - gfh_int), yxlstr);
                }
            }


        }


    }
}
