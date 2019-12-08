// TIMEPASS FILE HEADER COMMENT: =================================================================================
/**
 *  \file		TimePass.cs
 *  \ingroup	TMS
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Duane
 *  \brief	    This file contains the functionality to simulate time.	   
 *  \see		PlannerClass.cs
 *  \details    See TimePass Class description for more details.                                  
 *
 * =========================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSwPages
{
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		TimePass
    *   \brief		This class will be used to simulate the passage of time
    *   \details	This class is public and will be used by the PlannerClass
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class TimePass
    {
        // This function represents the planner's ability to simulate the passage of time by 1 day
        // This will calculate where each active truck is after 24h: which is convoluted by the 
        // amount of time between stops and time spent at each stop.
        
        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			static void IncrementDay()
        *	\brief		This method will be used to simulate the passage of time.
        *	\details	The data from each of the current trip tickets and their Route Data will be read from the data base.
        *	            The time that each ticket has been alive will be used to determine the current location of the truck. This data will be saved back into the database.
        *	\param[in]	null
        *	\param[out]	null
        *	\exception	null
        *	\see		na
        *	\return		void
        *
        * ---------------------------------------------------------------------------------------------------- */
        public static void IncrementDay()
        {
            //pull down a ticket from the orders that are incomplete
            // Trip_Ticket trip = new Trip_Ticket();

            double localDaysPassed = 0; //From the data base
            double DriveTime = localDaysPassed * 8;
            double StopTime = localDaysPassed * 12;

            //load the route stops from data base
            List<RouteData> stops = new List<RouteData>();

            //find the truck passed on the ticket id
            //Truck truck = new Truck();  -----Zena: Commented out for debugging purposes

            int i = 0;
            RouteData current;

            // Calculate how much distance is left for the truck's delivery. 
            while (true)
            {
                current = stops[i];

                DriveTime -= current.PickupTime;
                StopTime -= current.PickupTime;

                DriveTime -= current.DriveTime;

                DriveTime -= current.LtlTime;
                StopTime -= current.LtlTime;

                DriveTime -= current.DropoffTime;
                StopTime -= current.DropoffTime;

                if(DriveTime <= 0 || StopTime <= 0)
                {
                    break;
                }

                i++;
            }

            //update the truck location in the data base
            //truck.CurrentCityID = current.CityA;

        }
    }
}
