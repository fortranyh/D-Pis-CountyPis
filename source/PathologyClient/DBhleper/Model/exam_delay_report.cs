using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBhleper.Model
{
   public class exam_delay_report
    {
       public string study_no { get; set; }
       public string content { get; set; }
       public string report_doc_name { get; set; }
       public string report_doc_code { get; set; }
       public string sh_doc_name { get; set; }
       public int sh_flag { get; set; }
       public string sh_doc_code { get; set; }
       public string sh_datetime { get; set; }
       public string report_datetime { get; set; }
    }
   public class exam_bc_report
   {
       public string study_no { get; set; }
       public string content { get; set; }
       public string report_doc_name { get; set; }
       public string report_doc_code { get; set; }
       public string sh_doc_name { get; set; }
       public int sh_flag { get; set; }
       public string sh_doc_code { get; set; }
       public string sh_datetime { get; set; }
       public string report_datetime { get; set; }
   }
}
