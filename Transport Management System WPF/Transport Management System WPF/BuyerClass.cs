using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{
    // This class represents the functions that a Buyer can execute from the UI. 
    // i.e. Initiate an Order (read and display new contracts from CMP via DAL, select a 
    // contract from CMP, select cities for the contract, and submit the contract to an 
    // intermediate table for planner completion.-- This would potentially raise an event... later. )
    // The buyer can also view the table of completed orders (read and display from db, 
    // via DAL,select an order, preview an invoice, and then generate the invoice to a text file)

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
