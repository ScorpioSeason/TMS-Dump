using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This class represents the contract table
namespace Transport_Management_System_WPF
{
    public class Contract
    {
        public string client_Name { get; set; }
        public string job_Type { get; set; }
        public string quantity { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string van_Type { get; set; }
    }
}
