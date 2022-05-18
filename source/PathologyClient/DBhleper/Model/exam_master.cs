using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBhleper.Model
{
   public class exam_master
    {
       public string exam_no { get; set; }
       public string modality { get; set; }
       public string exam_type { get; set; }
       public string study_no { get; set; }
       public string patient_id { get; set; }
       public string age { get; set; }
       public string ageUint { get; set; }
       public int new_flag { get; set; }
       public int inout_type { get; set; }
       public string input_id { get; set; }
       public string output_id { get; set; }
       public string ward { get; set; }
       public string bed_no { get; set; }
       public string patient_source { get; set; }
       public string submit_unit { get; set; }
       public string req_dept { get; set; }
       public string req_dept_code { get; set; }
       public string req_physician { get; set; }
       public string req_physician_code { get; set; }
       public string req_date_time { get; set; }
       public int ice_flag { get; set; }
       public int ks_flag { get; set; }
       public int zl_flag { get; set; }
       public int fk_flag { get; set; }
       public string exam_status { get; set; }
       public string received_datetime { get; set; }
       public string received_doctor_code { get; set; }
       public string receive_doctor_name { get; set; }
       public string costs { get; set; }
       public string merge_exam_no { get; set; }
       public string examItems { get; set; }
       public int wtzd_flag { get; set; }
    }
}
