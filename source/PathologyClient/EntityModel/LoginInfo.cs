using System;

namespace EntityModel
{
    [Serializable]
    public class LoginInfo
    {
        public string user_code { get; set; }
        public string user_name { get; set; }
        public string user_pwd { get; set; }
        public string hospital_code { get; set; }
        public string hospital_name { get; set; }
        public string hospital_version { get; set; }
        public string app_info { get; set; }
        public Int64 update_datetime { get; set; }
    }
}
