using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSwPages.Classes
{
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class	    CreateTripInfo
    *   \brief		This is a class that is used to enter information for the Trip Tciket table
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class CreateTripInfo
    {
        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		       CreatTripInfo
        *	\brief		   This inserts data into the columns for the Trip Ticket table
        *	\param[in]     FC_LocalContract inContract, FC_Carrier inCarrier, FC_TripTicket partTicket
        *	\param[out]	   None
        *	\return		   None
        * ---------------------------------------------------------------------------------------------------- */
        public CreateTripInfo(FC_LocalContract inContract, FC_Carrier inCarrier, FC_TripTicket partTicket)
        {
            FC_TripTicket theTicket = new FC_TripTicket();
            theTicket.FC_TripTicketID = SQL.GetNextID("FC_TripTicket");
            theTicket.FC_CarrierID = inCarrier.FC_CarrierID;
            theTicket.Days_Passes = 0;
            theTicket.Size_in_Palettes = partTicket.Size_in_Palettes;
            theTicket.Is_Complete = 0; //0 is not done. 1 is done
            theTicket.CurrentLocation = inContract.Origin;

            SQL.Insert(theTicket);

            MappingClass mapping = new MappingClass();

            List<FC_RouteSeg> routeSegs = mapping.GetTravelData(inContract.Origin, inContract.Destination, inContract.Job_type, theTicket.FC_TripTicketID);

            foreach (FC_RouteSeg x in routeSegs)
            {
                SQL.Insert(x);
            }

            FC_TripTicketLine tripTicketLine = new FC_TripTicketLine(theTicket.FC_TripTicketID, inContract.FC_LocalContractID, partTicket.Size_in_Palettes);
            SQL.Insert(tripTicketLine);

            TMSLogger.LogIt(" | " + "CreateTripInfo.cs" + " | " + "CreateTripInfo" + " | " + "CreateTripInfo" + " | " + "Confirmation" + " | " + "Trip info created" + " | ");

        }
    }
}
