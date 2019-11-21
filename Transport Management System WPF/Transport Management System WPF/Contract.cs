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
        public bool job_Type { get; set; }
        public int quantity { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public bool van_Type { get; set; }
    }
}
