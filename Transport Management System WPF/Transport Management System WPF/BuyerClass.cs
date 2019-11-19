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
            
            List<string>[] temp = new List<string>[6];
            temp = SQL.Select_Contracts();


            for(int i = 0; i < temp[0].Count; i++)
            {
                Contract block = new Contract();

                block.client_Name = temp[0][i];
                block.job_Type = temp[1][i];
                block.quantity = temp[2][i];
                block.origin = temp[3][i];
                block.destination = temp[4][i];
                block.van_Type = temp[5][i];
                contracts.Add(block);
            }

            int j = 0;

            //foreach (List<string> entry in temp )
            //{
            //    Contract block = new Contract();

            //    block.client_Name = entry[0];
            //    block.job_Type = entry[1];
            //    block.quantity = entry[2];
            //    block.origin = entry[3];
            //    block.destination = entry[4];
            //    block.van_Type = entry[5];
            //    contracts.Add(block);
            //}
            					

        
            
        } 
    }
}
