using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSwPages.Classes
{
    public static class TimePass
    {

        public static void IncrementOneDay()
        {
            FC_TripTicket t = new FC_TripTicket();
            List<FC_TripTicket> AllTickets = t.ObjToTable(SQL.Select(t));

            foreach (FC_TripTicket x in AllTickets)
            {
                x.Days_Passes++;

                FC_RouteSeg s = new FC_RouteSeg();

                string query = "Select FC_TripTicketID, CityA, CityB, PickUpTime, DropOffTime, LtlTime, DrivenTime, KM from FC_RouteSeg where FC_TripTicketID = " + x.FC_TripTicketID + ";";

                List<FC_RouteSeg> AllSegments = s.ObjToTable(SQL.Select(s, query));

                double totalTime = x.Days_Passes * 12;
                double DriveTime = x.Days_Passes * 8;

                int SegIndex = 0;
                FC_RouteSeg currentSegment = AllSegments[SegIndex];

                while (true)
                {
                    totalTime -= currentSegment.PickUpTime;

                    totalTime -= currentSegment.DrivenTime;
                    DriveTime -= currentSegment.DrivenTime;

                    totalTime -= currentSegment.LtlTime;

                    totalTime -= currentSegment.DropOffTime;

                    if (totalTime <= 0 || DriveTime <= 0)
                    {
                        x.CurrentLocation = LoadCSV.ToCityName(currentSegment.CityA);
                        break;
                    }
                    else
                    {
                        SegIndex++;

                        if (SegIndex >= AllSegments.Count)
                        {
                            x.CurrentLocation = LoadCSV.ToCityName(currentSegment.CityB);
                            break;
                        }
                        else
                        {
                            currentSegment = AllSegments[SegIndex];
                        }

                        x.CurrentLocation = LoadCSV.ToCityName(currentSegment.CityB);
                    }
                }

                PlannerClass.UpdateTicketLocation(x);
            }



            string Query = "select FC_LocalContractID, Client_Name, Job_type, Quantity, Origin, Destination, Van_type, Contract_Status from " +
                "FC_LocalContract where Contract_Status = 1";

            FC_LocalContract l = new FC_LocalContract();
            List<FC_LocalContract> OnrouteContracts = l.ObjToTable(SQL.Select(l, Query));

            MappingClass map = new MappingClass();

            foreach(FC_LocalContract x in OnrouteContracts)
            {
                bool isComplete = true;

                List<FC_TripTicket> theTickets = PlannerClass.ConnectedTickets_Populate(x);

                foreach(FC_TripTicket y in theTickets)
                {
                    if(!map.AtOrPastCity(x,y))
                    {
                        isComplete = false;
                        break;
                    }
                }

                if(isComplete)
                {
                    PlannerClass.UpdateContratState(x, 2);
                }
            }

        }
    }
}
