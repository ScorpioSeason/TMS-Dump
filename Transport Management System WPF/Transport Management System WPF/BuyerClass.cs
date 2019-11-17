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
            SQL_Query.ContractCalling();
            int i = 0;
            Contract block = new Contract();
            while (i <= SQL_Query.sqlReads.Count())
            {
                block.client_Name = SQL_Query.sqlReads[i+0];
                block.job_Type = SQL_Query.sqlReads[i+1];
                block.quantity = SQL_Query.sqlReads[i+2];
                block.origin = SQL_Query.sqlReads[i+3];
                block.destination = SQL_Query.sqlReads[i+4];
                block.van_Type = SQL_Query.sqlReads[i+5];
                contracts.Add(block);
                i += 6;
            }
            
        } 
    }
}
