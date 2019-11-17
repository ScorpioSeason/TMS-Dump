using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{
    struct Contract
    {
        public string client_Name;
        public string job_Type;
        public string quantity;
        public string origin;
        public string destination;
        public string van_Type;
    }
    public class BuyerClass
    {

        private List<Contract> contracts;

        internal List<Contract> Contracts { get => contracts; set => contracts = value; }

        public void ParseContracts()
        {
            SQL_Query.ContractCalling();
            foreach (string a  in SQL_Query.sqlReads){
                String[] strlist = a.Split('|');
                Contract b;
                b.client_Name = strlist[0];
                b.job_Type = strlist[1];
                b.quantity = strlist[2];
                b.origin = strlist[3];
                b.destination = strlist[4];
                b.van_Type = strlist[5];
                Contracts.Add(b);
            }
            
        } 
    }
}
