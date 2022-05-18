using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBhleper.Model
{
   public class interface_set_info
    {
       //接口服务ip地址
        public string tcp_ip { get; set; }
       //接口服务端口
        public int tcp_port { get; set; }
       //接口服务启用标记
        public int enable_flag { get; set; }

	      public int receive_flag { get; set; }   //'标本接收标记',
	     public int draw_flag{ get; set; }   // '取材结束标记',
         public int baomai_flag { get; set; }   // '包埋结束标记',
	public int slide_flag{ get; set; }   // 制片结束标记',
	public int  special_flag{ get; set; }   // '特检结束标记',
	public int  init_report_flag{ get; set; }   //  '初步报告标记',
	public int  verify_flag{ get; set; }   // '审核报告标记',
	public int  print_flag{ get; set; }   // '打印报告标记',
	public int  appointment_flag{ get; set; }   // '预约标记',
	public int  application_flag{ get; set; }   //  '申请标记',
	public int  frozen_flag{ get; set; }   //  '冰冻标记',
	public int  pis_cancel_flag{ get; set; }   //  'pis作废标记',
	public int  archives_flag{ get; set; }   // '归档标记',
	public int  his_cancel_flag{ get; set; }   //  'his作废标记',
	public int  consultation_flag{ get; set; }   //  '会诊标记',
    public int consultation_finish_flag { get; set; }   //  '会诊结束标记',
    public int bhg_flag { get; set; }   //  '会诊结束标记',
    }
}
