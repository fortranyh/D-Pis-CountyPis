using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBhleper.Model
{
   public class exam_tumour
    {  
       public string exam_no { get; set; }
       public string find_date { get; set; }
       public string parts { get; set; }
        public string sizes { get; set; }
       public string radiate_flag { get; set; }
       public string transfer_flag { get; set; }
        public string trans_parts { get; set; }
        public string memo { get; set; }
        public string chemotherapy { get; set; }
    }
}
