﻿using System;
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
        public static List<FC_RouteSeg> RouteSegsPerTicket = new List<FC_RouteSeg>();
        //for selecting tickets
        public static List<FC_TripTicket> PendingTickets = new List<FC_TripTicket>();
        //Active contracts and their connected tickets
        public static List<FC_LocalContract> ActiveContracts = new List<FC_LocalContract>();
        public static List<FC_TripTicket> ConnectedTickets = new List<FC_TripTicket>();
        //completed contracts to be confirmed.
        public static List<FC_LocalContract> ToBeConfirmedContracts = new List<FC_LocalContract>();
        //completed contracts
        public static List<FC_LocalContract> ConfirmedContracts = new List<FC_LocalContract>();
        //Ivan
        public static List<FC_LocalContract> ContractsByStatus_Populate(int status)
        {
            string query = "select * from FC_LocalContract where Contract_Status = " + status.ToString() + ";";
            FC_LocalContract temp = new FC_LocalContract();
            return temp.ObjToTable(SQL.Select(temp, query));
        }

        //Ivan
        public static List<FC_TripTicket> ConnectedTickets_Populate(FC_LocalContract temp)
        {
            ConnectedTickets.Clear();
            string query = "select t.FC_TripTicketID, t.FC_CarrierID, t.CurrentLocation, t.Size_in_Palettes, t.Days_Passes, t.Is_Complete from FC_LocalContract as lc " +
                "left join FC_TripTicketLine as tt on tt.FC_LocalContractID = lc.FC_LocalContractID " +
                "left join FC_TripTicket as t on t.FC_TripTicketID = tt.FC_TripTicketID " +
                "where lc.FC_LocalContractID = " + temp.FC_LocalContractID + ";";
            FC_TripTicket lc = new FC_TripTicket();
            ConnectedTickets = lc.ObjToTable(SQL.Select(lc, query));

            return ConnectedTickets;
        }

        //Ivan
        public static List<FC_LocalContract> ContractsPerTicket_Populate(FC_TripTicket temp)
        {
            string query = "select LC.FC_LocalContractID, LC.Client_Name, LC.Job_type, LC.Quantity, LC.Origin, LC.Destination, LC.Van_type, LC.Contract_Status " +
                "from FC_LocalContract as LC " +
                "left join FC_TripTicketLine as ttl on ttl.FC_LocalContractID = LC.FC_LocalContractID " +
                "left join FC_TripTicket as tt on tt.FC_TripTicketID = ttl.FC_TripTicketID " +
                "where tt.FC_TripTicketID = " + temp.FC_TripTicketID + ";";
            FC_LocalContract lc = new FC_LocalContract();
            return lc.ObjToTable(SQL.Select(lc, query));
        }

        //Duane
        public static void UpdateTicketLocation(FC_TripTicket inTicket)
        {
            string query = "update FC_TripTicket set CurrentLocation = \'" + inTicket.CurrentLocation + "\' , Days_Passes = " + inTicket.Days_Passes + "  where FC_TripTicketID = " + inTicket.FC_TripTicketID + ";";
            SQL.GenericFunction(query);
        }

        //Duane
        public static void UpdateContratState(FC_LocalContract inContract, int newState)
        {
            string query = "update FC_LocalContract set Contract_Status = " + newState.ToString() + " where FC_LocalContractID = " + inContract.FC_LocalContractID + ";";
            SQL.GenericFunction(query);
        }

        //Duane
        public static void UpdateTicketState(FC_TripTicket inTicket, int newState)
        {
            string query = "update FC_TripTicket set Is_Complete = " + newState.ToString() + " where FC_TripTicketID = " + inTicket.FC_TripTicketID.ToString() + ";";
            SQL.GenericFunction(query);
        }

        //Ivan
        public static List<FC_RouteSeg> RoutSegsPerTicket_Populate(FC_TripTicket inTicket)
        {
            if (inTicket != null)
            {
                string query = "select * from FC_RouteSeg where FC_TripTicketID =  " + inTicket.FC_TripTicketID + ";";

                FC_RouteSeg c = new FC_RouteSeg();
                RouteSegsPerTicket = c.ObjToTable(SQL.Select(c, query));

                return RouteSegsPerTicket;
            }

            return null;
        }

        //Ivan
        public static List<FC_TripTicket> TicketsWithStatus_Populate(int status)
        {
            string query = "select * from FC_TripTicket where Is_Complete = " + status.ToString() + ";";
            FC_TripTicket temp = new FC_TripTicket();
            return temp.ObjToTable(SQL.Select(temp, query));
        }

        public static int GetTicketProgress(FC_TripTicket InTicket)
        {
            string Location = InTicket.CurrentLocation;

            List<FC_RouteSeg> TicketSegs = RoutSegsPerTicket_Populate(InTicket);

            List<FC_RouteSeg> PassedTickets = new List<FC_RouteSeg>();

            foreach(FC_RouteSeg x in TicketSegs)
            {
                if(LoadCSV.ToCityName(x.CityB).ToUpper() == InTicket.CurrentLocation.ToUpper())
                {
                    break;
                }

                PassedTickets.Add(x);
            }

            RouteSumData TraveledData = new RouteSumData();
            TraveledData = TraveledData.SummerizeTrip(PassedTickets);

            RouteSumData TotalData = new RouteSumData();
            TotalData = TotalData.SummerizeTrip(TicketSegs);

             return (int)(TraveledData.totalKM / TotalData.totalKM);
        }

        public static void DeleteNominations(FC_LocalContract InContract)
        {
            string Query = "select bp.FC_BuyerToPlannerContractID, bp.FC_LocalContractID " +
                "from FC_BuyerToPlannerContract as bp " +
                "left join FC_LocalContract on FC_LocalContract.FC_LocalContractID = bp.FC_LocalContractID " +
                "where FC_LocalContract.FC_LocalContractID = " + InContract.FC_LocalContractID + ";";

            FC_BuyerToPlannerContract p = new FC_BuyerToPlannerContract();
            List<FC_BuyerToPlannerContract> B2P = p.ObjToTable(SQL.Select(p, Query));

            Query = "delete from FC_CarrierNom where FC_BuyerToPlannerContractID = " + B2P[0].FC_BuyerToPlannerContractID + ";";
            SQL.GenericFunction(Query);

            Query = "delete from FC_BuyerToPlannerContract where FC_BuyerToPlannerContractID = " + B2P[0].FC_BuyerToPlannerContractID + ";";

            SQL.GenericFunction(Query);
        }

        public static int AddContractToTicket(FC_TripTicket OriginalTick, FC_TripTicket TempTicket, FC_LocalContract TheContract)
        {
            string Query = "select * from FC_RouteSeg where FC_TripTicketID = " + OriginalTick.FC_TripTicketID + ";";

            FC_RouteSeg r = new FC_RouteSeg();
            List<FC_RouteSeg> OriginalSegs = r.ObjToTable(SQL.Select(r, Query));

            MappingClass map = new MappingClass();

            List<FC_RouteSeg> NewSegs = map.GetTravelData(TheContract.Origin, TheContract.Destination, 0, -1);

            //check that these tickets are going in the same direction
            if(OriginalSegs[0].CityA == NewSegs[0].CityA && OriginalSegs[0].CityB == NewSegs[0].CityB && TempTicket.Size_in_Palettes != 0)
            {

                int PalletesAddedToOgrinal = 26 - OriginalTick.Size_in_Palettes;

                if(PalletesAddedToOgrinal > TempTicket.Size_in_Palettes)
                {
                    PalletesAddedToOgrinal = TempTicket.Size_in_Palettes;
                }

                int OrignalTickNewSize = OriginalTick.Size_in_Palettes + PalletesAddedToOgrinal;

                if (OrignalTickNewSize == 26)
                {
                    OrignalTickNewSize = 0;
                }

                string query = "update FC_TripTicket " +
                      "set Size_in_Palettes = " + OrignalTickNewSize +
                      " where FC_TripTicketID = " + OriginalTick.FC_TripTicketID + ";";

                SQL.GenericFunction(query);

                int FTL = 0;

                List<FC_RouteSeg> NewSegmentsFTL = new List<FC_RouteSeg>();
                List<FC_RouteSeg> NewSegmentsLTL = new List<FC_RouteSeg>();

                if (NewSegs.Count > OriginalSegs.Count)
                {
                    string orginCity = LoadCSV.ToCityName(OriginalSegs[0].CityA);
                    string EndOfFTL = LoadCSV.ToCityName(OriginalSegs[OriginalSegs.Count - 1].CityB);
                    string startLTL = LoadCSV.ToCityName(NewSegs[OriginalSegs.Count].CityA);
                    string endCity = LoadCSV.ToCityName(NewSegs[NewSegs.Count - 1].CityB);

                    NewSegmentsFTL = map.GetTravelData(orginCity, EndOfFTL, FTL, OriginalTick.FC_TripTicketID);
                    NewSegmentsLTL = map.GetTravelData(startLTL, endCity, 1, OriginalTick.FC_TripTicketID);
                }
                else if (NewSegs.Count < OriginalSegs.Count)
                {
                    string orginCity = LoadCSV.ToCityName(OriginalSegs[0].CityA);
                    string EndOfFTL = LoadCSV.ToCityName(NewSegs[NewSegs.Count - 1].CityB);
                    string startLTL = LoadCSV.ToCityName(OriginalSegs[NewSegs.Count + 1].CityA);
                    string endCity = LoadCSV.ToCityName(OriginalSegs[NewSegs.Count - 1].CityB);

                    NewSegmentsFTL = map.GetTravelData(orginCity, EndOfFTL, FTL, OriginalTick.FC_TripTicketID);
                    NewSegmentsLTL = map.GetTravelData(startLTL, endCity, 1, OriginalTick.FC_TripTicketID);
                }
                else
                {
                    string orginCity = LoadCSV.ToCityName(OriginalSegs[0].CityA);
                    string endCity = LoadCSV.ToCityName(OriginalSegs[NewSegs.Count - 1].CityB);

                    NewSegmentsLTL = map.GetTravelData(orginCity, endCity, FTL, OriginalTick.FC_TripTicketID);
                }

                if(NewSegmentsLTL != null)
                {
                    NewSegmentsLTL[0].PickUpTime = 0;
                }


                query = "delete from FC_RouteSeg where FC_TripTicketID = " + OriginalTick.FC_TripTicketID + ";";
                SQL.GenericFunction(query);

                foreach (FC_RouteSeg x in NewSegmentsFTL)
                {
                    SQL.Insert(x);
                }

                foreach (FC_RouteSeg x in NewSegmentsLTL)
                {
                    SQL.Insert(x);
                }

                FC_TripTicketLine NewLine = new FC_TripTicketLine(OriginalTick.FC_TripTicketID, TheContract.FC_LocalContractID, PalletesAddedToOgrinal);
                SQL.Insert(NewLine);

                return PalletesAddedToOgrinal;
            }

            return -1;
        }

        public static List<FC_TripTicket> CreateTicketsFromContract(FC_LocalContract InContract)
        {
            List<FC_TripTicket> ReturnTickets = new List<FC_TripTicket>();

            int TempTickedID = -10;

            if (InContract.Quantity == 0 || InContract.Quantity == 26)
            {
                FC_TripTicket newTicket = new FC_TripTicket(TempTickedID, -1, InContract.Origin, 0, 0, 0);
                ReturnTickets.Add(newTicket);
            }
            else if(InContract.Quantity < 26)
            {
                FC_TripTicket newTicket = new FC_TripTicket(TempTickedID, -1, InContract.Origin, InContract.Quantity, 0, 0);
                ReturnTickets.Add(newTicket);
            }
            else if(InContract.Quantity > 26)
            {
                FC_TripTicket newTicket = new FC_TripTicket();

                do
                {
                    if(InContract.Quantity >= 26)
                    {
                        newTicket = new FC_TripTicket(TempTickedID--, -1, InContract.Origin, 0, 0, 0);
                    }
                    else if (InContract.Quantity < 26)
                    {
                        newTicket = new FC_TripTicket(TempTickedID--, -1, InContract.Origin, InContract.Quantity, 0, 0);
                    }

                    ReturnTickets.Add(newTicket);

                    InContract.Quantity -= 26;

                } while (InContract.Quantity > 0);
            }

            return ReturnTickets;
        }

        public static List<FC_LocalContract> GetNominatedContracts()
        {
            string query = "select LC.FC_LocalContractID, LC.Client_Name, LC.Job_type, LC.Quantity, LC.Origin, LC.Destination, LC.Van_type, LC.Contract_Status " +
               "from FC_BuyerToPlannerContract as bp " +
               "left join FC_LocalContract as LC on LC.FC_LocalContractID = bp.FC_LocalContractID;";

            FC_LocalContract n = new FC_LocalContract();
            return n.ObjToTable(SQL.Select(n, query));
        }

        public static List<CarrierWithDepot_View> GetNomCarriers_withDepot(FC_LocalContract inContract)
        {
            if(inContract != null)
            {
                string query = "select ca.FC_CarrierID, ca.Carrier_Name, dc.CityName, dc.FTL_Availibility, dc.LTL_Availibility, dc.FTL_Rate, dc.LTL_Rate, dc.Reefer_Charge " +
                           "from FC_BuyerToPlannerContract as bp " +
                           "left join FC_CarrierNom as CN on CN.FC_BuyerToPlannerContractID = bp.FC_BuyerToPlannerContractID " +
                           "left join FC_Carrier as ca on ca.FC_CarrierID = CN.FC_CarrierID " +
                           "left join FC_DepotCity as dc on dc.FC_CarrierID = ca.FC_CarrierID " +
                           "where bp.FC_LocalContractID = " + inContract.FC_LocalContractID.ToString() + " and dc.CityName = \"" + inContract.Origin + "\";";

                CarrierWithDepot_View c = new CarrierWithDepot_View();
                return c.ObjToTable(SQL.Select(c, query));
            }

            return null;
        }
    }
}
