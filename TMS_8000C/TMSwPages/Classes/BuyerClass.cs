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
using TMSwPages.Classes;

namespace TMSwPages
{
    
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		BuyerClass
    *   \brief		This class runs the Buyer functionality
    *   \details	public class
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class BuyerClass
    {
        public List<FC_ContractFromRuss> contracts = new List<FC_ContractFromRuss>();

        internal List<FC_ContractFromRuss> Contracts { get => contracts; set => contracts = value; }

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
        public void ParseContracts()
        {
            contracts.Clear();

            FC_ContractFromRuss f = new FC_ContractFromRuss();
            contracts = f.ObjToTable(SQL.SelectFromCMP(f));
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

* ---------------------------------------------------------------------------------------------------- */
