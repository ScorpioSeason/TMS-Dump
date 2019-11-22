// ADMIN FILE HEADER COMMENT: =================================================================================
/**
 *  \file		Admin.cs
 *  \ingroup	TMS
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Megan
 *  \brief	    This file contains the admin functionality	  
 *  \see		MainWindow.xaml
 *  \details    This file holds the functionality of the Admin class. The Admin has the ability to view logs as 
 *              specified by time period, view details of specific logs, alter where the log files are stored, 
 *              initiate backups of the TMS database, choose where the TMS db is backed up to, alter the Carrier 
 *              Data Table, the Route Table, and the Rate / Fee Tables.                                       
 *
 * =========================================================================================================== */

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

    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		Admin
    *   \brief		This class runs the Admin UI functionality
    *   \details	... static class?  
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class BuyerClass
    {
        private List<Contract> contracts= new List<Contract>();

        internal List<Contract> Contracts { get => contracts; set => contracts = value; }

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			int Square()
        *	\brief		To create a new Square by validating or else defaulting new values
        *	\details	THis is if you have more to say about what the function does and don't want to inline comment
        *	\param[in]	char[]	newColour		An incoming value meant to become the square's colour
        *	\param[out]	char[]	newSideLength	An incoming value meant to become the square's side length
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        public void ParseContracts()
        {
            contracts.Clear();
            SQL_Query SQL = new SQL_Query();
            
            List<string>[] temp = new List<string>[6];
            temp = SQL.Select_Contracts();

            for(int i = 0; i < temp[0].Count; i++)
            {
                Contract block = new Contract();

                block.client_Name = temp[0][i];

                if(temp[1][i] == "0")
                {
                    block.job_Type = false;
                }
                else
                {
                    block.job_Type = true;
                }

                block.quantity = int.Parse(temp[2][i]);
                block.origin = temp[3][i];
                block.destination = temp[4][i];

                if (temp[5][i] == "0")
                {
                    block.van_Type = false;
                }
                else
                {
                    block.van_Type = true;
                }

                contracts.Add(block);
            }
        } 
    }
}
