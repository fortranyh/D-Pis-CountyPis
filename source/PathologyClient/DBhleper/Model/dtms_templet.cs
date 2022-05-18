using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBhleper.Model
{
  public  class dtms_templet
    {
       public string id{ get; set; }
       public string parentid{ get; set; }
       public string title{ get; set; }
       public string content{ get; set; }
       public string DocNo{ get; set; }
       public int TreeLevel{ get; set; }
       public int Clicked{ get; set; }
       public int flag{ get; set; }
    }
}
