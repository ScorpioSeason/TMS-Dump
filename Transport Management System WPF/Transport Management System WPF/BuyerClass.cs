using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{
    public class BuyerClass
    {

        private List<Contract> contracts= new List<Contract>();

        internal List<Contract> Contracts { get => contracts; set => contracts = value; }

        public void ParseContracts()
        {

            SQL_Query SQL = new SQL_Query();
            Contract block = new Contract();
            List<string>[] temp = new List<string>[6];
            temp = SQL.Select_Contracts();
            foreach (List<string> entry in temp )
            {
                block.client_Name = entry[0];
                block.job_Type = entry[1];
                block.quantity = entry[2];
                block.origin = entry[3];
                block.destination = entry[4];
                block.van_Type = entry[5];
                contracts.Add(block);
            }
            
        } 
    }
}
