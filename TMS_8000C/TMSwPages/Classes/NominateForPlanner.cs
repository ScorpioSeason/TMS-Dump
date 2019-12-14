using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSwPages.Classes
{
    class NominateForPlanner
    {

        private FC_ContractFromRuss InContract;
        private FC_LocalContract TheContract;
        private List<FC_Carrier> TheCarriers;

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public NominateForPlanner()
        {
            InContract = null;
            TheContract = null;
            TheCarriers = new List<FC_Carrier>();
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public bool AddCarrier(FC_Carrier inCarrier)
        {
            TheCarriers.Add(inCarrier);

            return true;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public bool AddCarrier(int inID, string inName)
        {
            FC_Carrier temp = new FC_Carrier(inID, inName);
            TheCarriers.Add(temp);

            return true;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public bool Add_Contract(FC_ContractFromRuss inContract)
        {
            InContract = inContract;

            return true;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		    Add_Contract
        *	\brief			
        *	\param[in]  string in_clientName, int in_jobtype, int IN_Quantity, string IN_Origin, 
        *	            string IN_Destination, int IN_Van_type
        *	\param[out] none	 
        *	\return		bool
        * ---------------------------------------------------------------------------------------------------- */
        public bool Add_Contract(string in_clientName, int in_jobtype, int IN_Quantity, string IN_Origin, string IN_Destination, int IN_Van_type)
        {
            InContract = new FC_ContractFromRuss(in_clientName, in_jobtype, IN_Quantity, IN_Origin, IN_Destination, IN_Van_type);

            return true;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		    PushToDataBase
        *	\brief		This method inserts a new contract into the database	
        *	\param[in]  none
        *	\param[out]	none 
        *	\return		bool
        * ---------------------------------------------------------------------------------------------------- */
        public bool PushToDataBase()
        {
            TheContract = new FC_LocalContract(SQL.GetNextID("FC_LocalContract"), InContract.Client_Name, InContract.Job_type, InContract.Quantity, InContract.Origin, InContract.Destination, InContract.Van_type, 0);
           
            SQL.Insert(TheContract);

            FC_BuyerToPlannerContract B2PC = new FC_BuyerToPlannerContract();
            B2PC.FC_BuyerToPlannerContractID = SQL.GetNextID("FC_BuyerToPlannerContract");
            B2PC.FC_LocalContractID = TheContract.FC_LocalContractID;

            SQL.Insert(B2PC);

            foreach (FC_Carrier x in TheCarriers)
            {
                SQL.Insert(new FC_CarrierNom(B2PC.FC_BuyerToPlannerContractID, x.FC_CarrierID));
            } 
            
            return true;
        }

    }
}
