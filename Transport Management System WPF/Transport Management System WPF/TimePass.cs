using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{



    public class TimePass
    {

        public void incrmentDay()
        {
            //pull down a ticket from the orders that are incomplete
            Trip_Ticket trip = new Trip_Ticket();

            double localDaysPassed = trip.Days_Passed;

            double DriveTime = localDaysPassed * 8;
            double StopTime = localDaysPassed * 12;

            //load the route stops
            List<RouteData> stops = new List<RouteData>();

            //find the truck passed on the ticket id
            Truck truck = new Truck();

            int i = 0;
            RouteData current;



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


           





        }

       

    }
}
