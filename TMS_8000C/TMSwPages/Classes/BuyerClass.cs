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

using System.Collections.Generic;
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
    public static class BuyerClass
    {
        public static List<FC_ContractFromRuss> acceptedContracts = new List<FC_ContractFromRuss>();
        public static List<FC_ContractFromRuss> contracts = new List<FC_ContractFromRuss>();

        internal static List<FC_ContractFromRuss> Contracts { get => contracts; set => contracts = value; }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			void ParseContracts()
        *	\brief		To call the sql query to populate list of contract class objects in order to display to the program all available contracts.
        *	\details	Calls SQL_Query.Select_Contracts from a instance of the class and uses the list it returns to populate the list.
        *	\param[in]	void
        *	\param[out]	void 
        *	\exception	none
        *	\see		SQL_Query Class
        *	\return		None
        * ---------------------------------------------------------------------------------------------------- */
        public static void ParseContracts()
        {
            contracts.Clear();

            FC_ContractFromRuss f = new FC_ContractFromRuss();
            contracts = f.ObjToTable(SQL.SelectFromCMP(f));

            TMSLogger.LogIt(" | " + "BuyerClass.cs" + " | " + "BuyerClass" + " | " + "ParseContracts" + " | " + "Confirmation" + " | " + "Contracts parsed successfully" + " | ");

        }
        public static List<FC_Carrier> nomCarriers = new List<FC_Carrier>();

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			void Nominations()
        *	\brief		This method nominates
        *	\param[in]	void
        *	\param[out]	void 
        *	\return		None
        * ---------------------------------------------------------------------------------------------------- */
        public static void Nominations()
        {
            FC_Carrier f = new FC_Carrier();
            List<FC_Carrier> AllCarriers = f.ObjToTable(SQL.Select(f));
            foreach (FC_ContractFromRuss y in acceptedContracts) {
                NominateForPlanner NewNomination = new NominateForPlanner();
                NewNomination.Add_Contract(y);
                foreach (FC_Carrier x in AllCarriers)
                {
                    string query = "select dc.FC_CarrierID, dc.CityName, dc.FTL_Availibility, dc.LTL_Availibility, dc.FTL_Rate, dc.LTL_Rate, dc.Reefer_Charge " +
                               "from FC_Carrier as c " +
                               "left join FC_DepotCity as dc on dc.FC_CarrierID = c.FC_CarrierID " +
                               "where c.FC_CarrierID = " + x.FC_CarrierID + ";";

                    FC_DepotCity dc = new FC_DepotCity();
                    List<FC_DepotCity> Depots = dc.ObjToTable(SQL.Select(dc, query));
                    foreach(FC_DepotCity l in Depots)
                    {
                        if (l.CityName.ToUpper() == y.Origin.ToUpper())
                        {
                            NewNomination.AddCarrier(x);
                        }
                    }
                }
                NewNomination.PushToDataBase();
            }
            acceptedContracts.Clear();

            TMSLogger.LogIt(" | " + "BuyerClass.cs" + " | " + "BuyerClass" + " | " + "Nominations" + " | " + "Confirmation" + " | " + "Nomination completed" + " | ");
        }

        public static List<FC_Carrier> tempCarriers = new List<FC_Carrier>();

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		        void NominationView
        *	\brief			
        *	\param[in]      FC_ContractFromRuss temp
        *	\param[out]	    none
        *	\return		    none
        * ---------------------------------------------------------------------------------------------------- */
        public static void NominationView(FC_ContractFromRuss temp)
        {
            tempCarriers.Clear();
            FC_Carrier f = new FC_Carrier();
            List<FC_Carrier> AllCarriers = f.ObjToTable(SQL.Select(f));
            
            foreach (FC_Carrier x in AllCarriers)
            {
                string query = "select dc.FC_CarrierID, dc.CityName, dc.FTL_Availibility, dc.LTL_Availibility, dc.FTL_Rate, dc.LTL_Rate, dc.Reefer_Charge " +
                               "from FC_Carrier as c " +
                               "left join FC_DepotCity as dc on dc.FC_CarrierID = c.FC_CarrierID " +
                               "where c.FC_CarrierID = " + x.FC_CarrierID + ";";

                FC_DepotCity dc = new FC_DepotCity();
                List<FC_DepotCity> Depots = dc.ObjToTable(SQL.Select(dc, query));
                foreach (FC_DepotCity l in Depots)
                {
                    if (l.CityName.ToUpper() == temp.Origin.ToUpper())
                    {
                        tempCarriers.Add(x);
                    }
                }
            }

            TMSLogger.LogIt(" | " + "BuyerClass.cs" + " | " + "BuyerClass" + " | " + "NominationView" + " | " + "Confirmation" + " | " + "Nomination outputted" + " | ");

        }

        public static List<Cust_Price> AllCustomers = new List<Cust_Price>();
        public static List<FC_LocalContract> ContractsForCustomer = new List<FC_LocalContract>();
        public static List<FC_LocalContract> SelectedForInvoice= new List<FC_LocalContract>();
    }
    public class CustomerName
    {
        public string CustName { get; set; }
    }
}
