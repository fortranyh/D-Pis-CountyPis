using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBhleper.Model
{
   public class dept_message
    {
       public string fromUser_Code { get; set; }
       public string fromUser_Name { get; set; }
       public string toUser_Code { get; set; }
       public string toUser_Name { get; set; }
       public string  message { get; set; }
    }
}
