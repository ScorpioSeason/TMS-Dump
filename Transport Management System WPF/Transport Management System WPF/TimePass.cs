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
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		Admin
    *   \brief		This class runs the Admin UI functionality
    *   \details	... static class?  
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class TimePass
    {
        // This function represents the planner's ability to simulate the passage of time by 1day
        // This will calculate where each active truck is after 24h: which is convoluted by the 
        // amount of time between stops and time spent at each stop.
        
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
        public static void incrementDay(List<RouteData> routes, /*out*/ Truck truck, /*out*/ Trip_Ticket ticket)
        {
            //pull down a ticket from the orders that are incomplete
            // Trip_Ticket trip = new Trip_Ticket();

            double localDaysPassed = ticket.Days_Passed;
            double DriveTime = localDaysPassed * 8;
            double StopTime = localDaysPassed * 12;

            //load the route stops
            //List<RouteData> stops = new List<RouteData>();

            //find the truck passed on the ticket id
            //Truck truck = new Truck();  -----Zena: Commented out for debugging purposes

            int i = 0;
            RouteData current;

            // Calculate how much distance is left for the truck's delivery. 
            while (true)
            {
                current = routes[i];

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
            truck.CurrentCityID = current.CityA;

         
        }
    }
}
