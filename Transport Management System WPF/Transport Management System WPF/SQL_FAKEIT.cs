using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{
    class SQL_FAKEIT
    {
        public List<string>[] Select_Carriers()
        {
            //Create a list to store the result
            List<string>[] list = new List<string>[7];

            list[0].Add("Trucking 1");
            list[1].Add("");
            list[2].Add("");
            list[3].Add("");
            list[4].Add("");

            list[0].Add("Trucking 2");
            list[1].Add("");
            list[2].Add("");
            list[3].Add("");
            list[4].Add("");

            list[0].Add("Trucking 1");
            list[1].Add("");
            list[2].Add("");
            list[3].Add("");
            list[4].Add("");

            list[0].Add("Trucking 2");
            list[1].Add("");
            list[2].Add("");
            list[3].Add("");
            list[4].Add("");

            list[0].Add("Trucking 1");
            list[1].Add("");
            list[2].Add("");
            list[3].Add("");
            list[4].Add("");

            list[0].Add("Trucking 2");
            list[1].Add("");
            list[2].Add("");
            list[3].Add("");
            list[4].Add("");

            return list;
        }
    }
}
