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

        public int ToCityID(string inputCity)
        {
            if(inputCity == "Windsor")
            {
                return 0;
            }
            else if(inputCity == "London")
            {
                return 1;
            }
            else if (inputCity == "Hamilton")
            {
                return 2;
            }
            else if (inputCity == "Toronto")
            {
                return 3;
            }
            else if (inputCity == "Oshawa")
            {
                return 4;
            }
            else if (inputCity == "Belleville")
            {
                return 5;
            }
            else if (inputCity == "Kingston")
            {
                return 6;
            }
            else if (inputCity == "Ottawa")
            {
                return 7;
            }

            return -1;
        }
    }



}
