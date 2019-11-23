// ADMIN FILE HEADER COMMENT: =================================================================================
/**
 *  \file		Contract.cs
 *  \ingroup	TMS
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Ivan,Megan,Ivan,Duane
 *  \brief	    This file contains the buyer functionality 
 *  \see		MainWindow.xaml
 *  \details    This file holds the functionality of the buyer class. The buyer has the ability to accept contracts from the
 *              contract market place using the SQL_Query Class, to create invopices and nominate carriers for each contract.
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
    *   \class		BuyerClass
    *   \brief		This class runs the Buyer functionality
    *   \details	public class
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class BuyerClass
    {
        private List<Contract> contracts= new List<Contract>();

        internal List<Contract> Contracts { get => contracts; set => contracts = value; }

        // COP-OUT METHOD HEADER COMMENT -------------------------------------------------------------------------------
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


/**
*	\fn			int Send_Contacts_W_Nominations()
*	\brief		sets the nominations through possible 
*	\details	Calls SQL_Query.Select_Contracts from a instance of the class and uses the list it returns to populate the list.
*	\param[in]  void
*	\param[out]	void
*	\exception	This is if we have some big ol try catches?
*	\see		CallsMade()
*	\return		None
*
* ---------------------------------------------------------------------------------------------------- */
