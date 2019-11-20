using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{
    class Carrier
    {
        public string Carrier_Name { get; set; }
        public string Depot_City { get; set; }
        public float FTL_Rate { get; set; }
        public float LTL_Rate { get; set; }
        public float Reefer_Charge { get; set; }
    }
}
