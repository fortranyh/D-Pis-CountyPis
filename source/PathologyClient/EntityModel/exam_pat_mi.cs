using System;
using System.Collections.Generic;
using System.Text;

namespace EntityModel
{
    [Serializable]
   public class exam_pat_mi
    {
        public string patient_id { get; set; }
        public string patient_name { get; set; }
        public string name_phonetic { get; set; }
        public string sex { get; set; }
        public string date_of_birth { get; set; }
        public string nation { get; set; }
        public string identity { get; set; }
        public string current_place { get; set; }
        public string si_card { get; set; }
        public string hospital_card { get; set; }
        public string phone_number { get; set; }
    }
}
