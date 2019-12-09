using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSwPages.Classes
{
    public static class PlannerClass
    {
        public static List<FC_LocalContract> ContractsPerTicket = new List<FC_LocalContract>();
        public static List<FC_TripTicket> ActiveTickets = new List<FC_TripTicket>();
        public static void ContractsPerTicket_Populate(FC_TripTicket temp)
        {
            ContractsPerTicket.Clear();
            string query = "select LC.FC_LocalContractID, LC.Client_Name, LC.Job_type, LC.Quantity, LC.Origin, LC.Destination, LC.Van_type " +
                "from FC_LocalContract as LC " +
                "left join FC_TripTicketLine as ttl on ttl.FC_LocalContractID = LC.FC_LocalContractID " +
                "left join FC_TripTicket as tt on tt.FC_TripTicketID = ttl.FC_TripTicketID " +
                "where tt.FC_TripTicketID = " + temp.FC_TripTicketID + ";";
            FC_LocalContract lc = new FC_LocalContract();
            ContractsPerTicket = lc.ObjToTable(SQL.Select(lc, query));
        }

        public static void TicketsWithStatus_Populate(int status)
        {
            ActiveTickets.Clear();
            string query = "select * from FC_TripTicket where Is_Complete = " + status.ToString() + ";";
            FC_TripTicket temp = new FC_TripTicket();
            ActiveTickets = temp.ObjToTable(SQL.Select(temp, query));
        }
    }
}
