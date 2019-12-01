// BUYERCLASS FILE HEADER COMMENT: =================================================================================
/**
 *  \file		BuyerClass.cs
 *  \ingroup	TMS
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Ivan,Megan,Zena,Duane
 *  \brief	    This file contains the buyer functionality 
 *  \see		MainWindow.xaml
 *  \details    This file holds the functionality of the buyer class. The buyer has the ability to accept contracts from the
 *              contract market place using the SQL_Query Class, to create invoices and nominate carriers for each contract.
 *              This class represents the functions that a Buyer can execute from the UI. 
 *              i.e. Initiate an Order (read and display new contracts from CMP via DAL, select a 
 *              contract from CMP, select cities for the contract, and submit the contract to an 
 *              intermediate table for planner completion.-- This would potentially raise an event... later. )
 *              The buyer can also view the table of completed orders (read and display from db, 
 *              via DAL,select an order, preview an invoice, and then generate the invoice to a text file)
 *
 * =========================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{
    
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		BuyerClass
    *   \brief		This class runs the Buyer functionality
    *   \details	public class
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public static class BuyerClass
    {
        public static List<Contract> acceptedContracts = new List<Contract>();

        private static List<Contract> contracts= new List<Contract>();

        internal static List<Contract> Contracts { get => contracts; set => contracts = value; }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			int ParseContracts()
        *	\brief		To call the sql query to populate list of contract class objects in order to display to the program all available contracts.
        *	\details	Calls SQL_Query.Select_Contracts from a instance of the class and uses the list it returns to populate the list.
        *	\param[in]	void
        *	\param[out]	void 
        *	\exception	none
        *	\see		SQL_Query Class
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */
        public static void ParseContracts()
        {
            contracts.Clear();
            SQL_Query SQL = new SQL_Query();
            
            List<string>[] temp = new List<string>[6];
            temp = SQL.Select_Contracts();

            for(int i = 0; i < temp[0].Count; i++)
            {
                Contract block = new Contract();

                block.client_Name = temp[0][i];

                //Duane Changed this line. the contract has job type as a int again
                block.job_Type = int.Parse(temp[1][i]);


                block.quantity = int.Parse(temp[2][i]);
                block.origin = temp[3][i];
                block.destination = temp[4][i];

                //Duane Changed this line. the contract has van type as a int again
                block.van_Type = int.Parse(temp[5][i]);

                

                contracts.Add(block);
            }
        }
        /**
        *	\fn			int contract_Nominations()
        *	\brief		sets the nominations through possible 
        *	\details	Calls SQL_Query.Select_Contracts from a instance of the class and uses the list it returns to populate the list.
        *	\param[in]  void
        *	\param[out]	void
        *	\exception	This is if we have some big ol try catches?
        *	\see		CallsMade()
        *	\return		None
        *
        * ---------------------------------------------------------------------------------------------------- */

        static int contract_Nominations()
        {
            SQL_Query_TMS SQL = new SQL_Query_TMS();

            List<string>[] temp = new List<string>[6];
            temp = SQL.Select_Carriers();

            for (int i = 0; i < temp[0].Count; i++)
            {
                Contract block = new Contract();

                block.client_Name = temp[0][i];


                //Duane Changed this line. the contract has job type as a int again
                block.job_Type = int.Parse(temp[1][i]);

                block.quantity = int.Parse(temp[2][i]);
                block.origin = temp[3][i];
                block.destination = temp[4][i];

                //Duane Changed this line. the contract has van type as a int again
                block.van_Type = int.Parse(temp[5][i]);

                contracts.Add(block);
            }

            return 1;
        }
    }

}


    
