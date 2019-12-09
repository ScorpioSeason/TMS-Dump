using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSwPages.Classes
{
    public class TimePass
    {

        public void IncrementOneDay()
        {
            FC_TripTicket t = new FC_TripTicket();
            List<FC_TripTicket> AllTickets = t.ObjToTable(SQL.Select(t));

            foreach(FC_TripTicket x in AllTickets)
            {
                FC_RouteSeg s = new FC_RouteSeg();
                List<FC_RouteSeg> AllSegments = s.ObjToTable(SQL.Select(s, x.FC_TripTicketID));

                double totalTime = x.Days_Passes * 12;
                double DriveTime = x.Days_Passes * 8;

                int SegIndex = 0;
                FC_RouteSeg currentSegment = AllSegments[SegIndex];


                while(true)
                {
                    totalTime -= currentSegment.PickUpTime;

                    totalTime -= currentSegment.DrivenTime;
                    DriveTime -= currentSegment.DrivenTime;

                    totalTime -= currentSegment.LtlTime;

                    totalTime -= currentSegment.DropOffTime;

                    if(totalTime <= 0 || DriveTime <= 0)
                    {
                        x.CurrentLocation = LoadCSV.ToCityName(currentSegment.CityA);
                        break;
                    }
                    else
                    {
                        x.CurrentLocation = LoadCSV.ToCityName(currentSegment.CityB);
                        currentSegment = AllSegments[++SegIndex];

                        if(currentSegment == null)
                        {
                            break;
                        }
                    }
                }


            }
        }
    }
}
