using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSwPages.Classes
{
    public class ParentTable
    {
        //return the SQL insert statement needed to insert into this table
        public virtual string GetInsertStatment()
        {
            return null;
        }

        //return the name of the table
        public virtual string GetTableName()
        {
            return null;
        }

        //return the SQL select statement needed to call this table
        public virtual string GetSelectStatment()
        {
            return null;
        }

        //return the amount of columns in this table
        public virtual int GetColoumInt()
        {
            return -1;
        }

        //Construct and return a list of all the column names in this table
        public virtual List<string> GetColoumNames()
        {
            return null;
        }

        //The SQL class will return an array of lists, convert that data into a list of objects
        public virtual List<object> PackageClasses(List<string>[] inList)
        {
            return null;
        }
    }

    public class FC_DepotCity : ParentTable
    {
        public int FC_CarrierID { get; set; }
        public string CityName { get; set; }
        public int FTL_Availibility { get; set; }
        public int LTL_Availibility { get; set; }
        public double FTL_Rate { get; set; }
        public double LTL_Rate { get; set; }
        public double Reefer_Charge { get; set; }

        public FC_DepotCity()
        {
            FC_CarrierID = -1;
            CityName = "not_set";
            FTL_Availibility = -1;
            LTL_Availibility = -1;
            FTL_Rate = -1;
            LTL_Rate = -1;
            Reefer_Charge = -1;
        }

        public FC_DepotCity(int IN_FC_CarrierID, string IN_CityName, int IN_FTL_Availibility, int IN_LTL_Availibility, double IN_FTL_Rate, double IN_LTL_Rate, double IN_Reefer_Charge)
        {
            FC_CarrierID = IN_FC_CarrierID;
            CityName = IN_CityName;
            FTL_Availibility = IN_FTL_Availibility;
            LTL_Availibility = IN_LTL_Availibility;
            FTL_Rate = IN_FTL_Rate;
            LTL_Rate = IN_LTL_Rate;
            Reefer_Charge = IN_Reefer_Charge;
        }

        public override string GetTableName()
        {
            return "FC_DepotCity";
        }

        public override string GetSelectStatment()
        {
            return "Select * from FC_DepotCity;";
        }

        public override int GetColoumInt()
        {
            return 7;
        }

        public override string GetInsertStatment()
        {
            return "insert into FC_DepotCity(FC_CarrierID, CityName, FTL_Availibility, LTL_Availibility, FTL_Rate, LTL_Rate, Reefer_Charge) value (" +
                FC_CarrierID.ToString() + "," +
                "\"" + CityName + "\"," +
                FTL_Availibility.ToString() + "," +
                LTL_Availibility.ToString() + "," +
                FTL_Rate.ToString() + "," +
                LTL_Rate.ToString() + "," +
                Reefer_Charge.ToString() + ");";
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_CarrierID");
            outList.Add("CityName");
            outList.Add("FTL_Availibility");
            outList.Add("LTL_Availibility");
            outList.Add("FTL_Rate");
            outList.Add("LTL_Rate");
            outList.Add("Reefer_Charge");

            return outList;
        }


        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            for (int i = 0; i < inList[0].Count; i++)
            {
                FC_DepotCity current = new FC_DepotCity();

                current.FC_CarrierID = int.Parse(inList[0][i]);
                current.CityName = inList[1][i];
                current.FTL_Availibility = int.Parse(inList[2][i]);
                current.LTL_Availibility = int.Parse(inList[3][i]);
                current.FTL_Rate = double.Parse(inList[4][i]);
                current.LTL_Rate = double.Parse(inList[5][i]);
                current.Reefer_Charge = double.Parse(inList[6][i]);

                outList.Add(current);
            }

            return outList;
        }
        public List<FC_DepotCity> ObjToTable(List<object> inList)
        {
            List<FC_DepotCity> ConvertList = new List<FC_DepotCity>();

            foreach (object x in inList)
            {
                ConvertList.Add((FC_DepotCity)x);
            }

            return ConvertList;
        }
    }

    public class FC_TripTicketLine : ParentTable
    {
        public int FC_TripTicketID { get; set; }
        public int FC_LocalContractID { get; set; }
        public int PalletsOnTicket { get; set; }


        public FC_TripTicketLine()
        {
            FC_TripTicketID = -1;
            FC_LocalContractID = -1;
            PalletsOnTicket = -1;
        }

        public FC_TripTicketLine(int IN_FC_TripTicketID, int IN_FC_LocalContractID, int IN_PalletsOnTicket = 1)
        {
            FC_TripTicketID = IN_FC_TripTicketID;
            FC_LocalContractID = IN_FC_LocalContractID;
            PalletsOnTicket = IN_PalletsOnTicket;
        }


        public override string GetTableName()
        {
            return "FC_TripTicketLine";
        }

        public override string GetSelectStatment()
        {
            return "Select * from FC_TripTicketLine;";
        }

        public override int GetColoumInt()
        {
            return 3;
        }

        public override string GetInsertStatment()
        {
            return "insert into FC_TripTicketLine(FC_TripTicketID, FC_LocalContractID, PalletsOnTicket) value (" +
                FC_TripTicketID.ToString() + "," +
                FC_LocalContractID.ToString() + "," +
                PalletsOnTicket.ToString() + ");";
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_TripTicketID");
            outList.Add("FC_LocalContractID");
            outList.Add("PalletsOnTicket");

            return outList;
        }


        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            for (int i = 0; i < inList[0].Count; i++)
            {
                FC_TripTicketLine current = new FC_TripTicketLine();

                current.FC_TripTicketID = int.Parse(inList[0][i]);
                current.FC_LocalContractID = int.Parse(inList[1][i]);
                current.PalletsOnTicket = int.Parse(inList[2][i]);

                outList.Add(current);
            }

            return outList;
        }


        public List<FC_TripTicketLine> ObjToTable(List<object> inList)
        {
            List<FC_TripTicketLine> ConvertList = new List<FC_TripTicketLine>();

            foreach (object x in inList)
            {
                ConvertList.Add((FC_TripTicketLine)x);
            }

            return ConvertList;
        }



    }

    public class FC_RouteSeg : ParentTable
    {
        public int FC_TripTicketID { get; set; }
        public int CityA { get; set; }
        public int CityB { get; set; }
        public double PickUpTime { get; set; }
        public double DropOffTime { get; set; }
        public double LtlTime { get; set; }
        public double DrivenTime { get; set; }
        public int KM { get; set; }

        public FC_RouteSeg()
        {
            FC_TripTicketID = -1;
            CityA = -1;
            CityB = -1;
            PickUpTime = -1;
            DropOffTime = -1;
            LtlTime = -1;
            DrivenTime = -1;
            KM = -1;
        }

        public FC_RouteSeg(int IN_FC_TripTicketID, int IN_CityA, int IN_CityB, double IN_PickUpTime, double IN_DropOffTime, double IN_LtlTime, double IN_DrivenTime, int IN_KM)
        {
            FC_TripTicketID = IN_FC_TripTicketID;
            CityA = IN_CityA;
            CityB = IN_CityB;
            PickUpTime = IN_PickUpTime;
            DropOffTime = IN_DropOffTime;
            LtlTime = IN_LtlTime;
            DrivenTime = IN_DrivenTime;
            KM = IN_KM;
        }

        public override string GetTableName()
        {
            return "FC_RouteSeg";
        }

        public override string GetSelectStatment()
        {
            return "Select * from FC_RouteSeg;";
        }

        public override int GetColoumInt()
        {
            return 8;
        }

        public override string GetInsertStatment()
        {
            return "insert into FC_RouteSeg(FC_TripTicketID, CityA, CityB, PickUpTime, DropOffTime, LtlTime, DrivenTime, KM) value (" +
                FC_TripTicketID.ToString() + "," +
                CityA.ToString() + "," +
                CityB.ToString() + "," +
                PickUpTime.ToString() + "," +
                DropOffTime.ToString() + "," +
                LtlTime.ToString() + "," +
                DrivenTime.ToString() + "," +
                KM.ToString() + ");";
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_TripTicketID");
            outList.Add("CityA");
            outList.Add("CityB");
            outList.Add("PickUpTime");
            outList.Add("DropOffTime");
            outList.Add("LtlTime");
            outList.Add("DrivenTime");
            outList.Add("KM");

            return outList;
        }

        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            for (int i = 0; i < inList[0].Count; i++)
            {
                FC_RouteSeg current = new FC_RouteSeg();

                current.FC_TripTicketID = int.Parse(inList[0][i]);
                current.CityA = int.Parse(inList[1][i]);
                current.CityB = int.Parse(inList[2][i]);
                current.PickUpTime = double.Parse(inList[3][i]);
                current.DropOffTime = double.Parse(inList[4][i]);
                current.LtlTime = double.Parse(inList[5][i]);
                current.DrivenTime = double.Parse(inList[6][i]);
                current.KM = int.Parse(inList[7][i]);

                outList.Add(current);
            }

            return outList;
        }

        public List<FC_RouteSeg> ObjToTable(List<object> inList)
        {
            List<FC_RouteSeg> ConvertList = new List<FC_RouteSeg>();

            foreach (object x in inList)
            {
                ConvertList.Add((FC_RouteSeg)x);
            }

            return ConvertList;
        }
    }

    public class FC_TripTicket : ParentTable
    {
        public int FC_TripTicketID { get; set; }
        public int FC_CarrierID { get; set; }
        public string CurrentLocation { get; set; }
        public int Size_in_Palettes { get; set; }
        public int Days_Passes { get; set; }
        public int Is_Complete { get; set; }

        public FC_TripTicket()
        {
            FC_TripTicketID = -1;
            FC_CarrierID = -1;
            CurrentLocation = "not_set";
            Size_in_Palettes = -1;
            Days_Passes = -1;
            Is_Complete = -1;
        }

        public FC_TripTicket(int IN_FC_TripTicketID, int IN_FC_CarrierID, string IN_CurrentLocation, int IN_Size_in_Palettes, int IN_Days_Passes, int IN_Is_Complete)
        {
            FC_TripTicketID = IN_FC_TripTicketID;
            FC_CarrierID = IN_FC_CarrierID;
            CurrentLocation = IN_CurrentLocation;
            Size_in_Palettes = IN_Size_in_Palettes;
            Days_Passes = IN_Days_Passes;
            Is_Complete = IN_Is_Complete;
        }

        public override string GetTableName()
        {
            return "FC_TripTicket";
        }

        public override string GetSelectStatment()
        {
            return "Select * from FC_TripTicket;";
        }

        public override int GetColoumInt()
        {
            return 6;
        }

        public override string GetInsertStatment()
        {
            return "insert into FC_TripTicket(FC_TripTicketID, FC_CarrierID, CurrentLocation, Size_in_Palettes, Days_Passes, Is_Complete) value (" +
                FC_TripTicketID.ToString() + "," +
                FC_CarrierID.ToString() + "," +
                "\"" + CurrentLocation + "\"," +
                Size_in_Palettes.ToString() + "," +
                Days_Passes.ToString() + "," +
                Is_Complete.ToString() + ");";
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_TripTicketID");
            outList.Add("FC_CarrierID");
            outList.Add("CurrentLocation");
            outList.Add("Size_in_Palettes");
            outList.Add("Days_Passes");
            outList.Add("Is_Complete");

            return outList;
        }

        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            for (int i = 0; i < inList[0].Count; i++)
            {
                FC_TripTicket current = new FC_TripTicket();

                current.FC_TripTicketID = int.Parse(inList[0][i]);
                current.FC_CarrierID = int.Parse(inList[1][i]);
                current.CurrentLocation = inList[2][i];
                current.Size_in_Palettes = int.Parse(inList[3][i]);
                current.Days_Passes = int.Parse(inList[4][i]);
                current.Is_Complete = int.Parse(inList[5][i]);

                outList.Add(current);
            }

            return outList;
        }

        public List<FC_TripTicket> ObjToTable(List<object> inList)
        {
            List<FC_TripTicket> ConvertList = new List<FC_TripTicket>();

            foreach (object x in inList)
            {
                ConvertList.Add((FC_TripTicket)x);
            }

            return ConvertList;
        }
    }

    public class FC_BuyerToPlannerContract : ParentTable
    {
        public int FC_BuyerToPlannerContractID { get; set; }
        public int FC_LocalContractID { get; set; }

        public FC_BuyerToPlannerContract()
        {
            FC_BuyerToPlannerContractID = -1;
            FC_LocalContractID = -1;
        }

        public FC_BuyerToPlannerContract(int IN_FC_BuyerToPlannerContractID, int IN_FC_LocalContractID)
        {
            FC_BuyerToPlannerContractID = IN_FC_BuyerToPlannerContractID;
            FC_LocalContractID = IN_FC_LocalContractID;
        }

        public override string GetTableName()
        {
            return "FC_BuyerToPlannerContract";
        }

        public override string GetSelectStatment()
        {
            return "Select * from FC_BuyerToPlannerContract;";
        }

        public override int GetColoumInt()
        {
            return 2;
        }

        public override string GetInsertStatment()
        {
            return "insert into FC_BuyerToPlannerContract(FC_BuyerToPlannerContractID, FC_LocalContractID) value (" +
                FC_BuyerToPlannerContractID.ToString() + "," +
                FC_LocalContractID.ToString() + ");";
        }
        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_BuyerToPlannerContractID");
            outList.Add("FC_LocalContractID");

            return outList;
        }

        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            for (int i = 0; i < inList[0].Count; i++)
            {
                FC_BuyerToPlannerContract current = new FC_BuyerToPlannerContract();

                current.FC_BuyerToPlannerContractID = int.Parse(inList[0][i]);
                current.FC_LocalContractID = int.Parse(inList[1][i]);

                outList.Add(current);
            }

            return outList;
        }

        public List<FC_BuyerToPlannerContract> ObjToTable(List<object> inList)
        {
            List<FC_BuyerToPlannerContract> ConvertList = new List<FC_BuyerToPlannerContract>();

            foreach (object x in inList)
            {
                ConvertList.Add((FC_BuyerToPlannerContract)x);
            }

            return ConvertList;
        }
    }

    public class FC_Carrier : ParentTable
    {
        public int FC_CarrierID { get; set; }
        public string Carrier_Name { get; set; }


        public FC_Carrier()
        {
            FC_CarrierID = -1;
            Carrier_Name = "not_set";
        }

        public FC_Carrier(int IN_FC_CarrierID, string IN_Carrier_Name)
        {
            FC_CarrierID = IN_FC_CarrierID;
            Carrier_Name = IN_Carrier_Name;
        }

        public override string GetTableName()
        {
            return "FC_Carrier";
        }

        public override string GetSelectStatment()
        {
            return "Select * from FC_Carrier;";
        }

        public override int GetColoumInt()
        {
            return 2;
        }

        public override string GetInsertStatment()
        {
            return "insert into FC_Carrier(FC_CarrierID, Carrier_Name) value (" +
                FC_CarrierID.ToString() + "," +
                "\"" + Carrier_Name + "\");";
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_CarrierID");
            outList.Add("Carrier_Name");

            return outList;
        }


        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            for (int i = 0; i < inList[0].Count; i++)
            {
                FC_Carrier current = new FC_Carrier();

                current.FC_CarrierID = int.Parse(inList[0][i]);
                current.Carrier_Name = inList[1][i];

                outList.Add(current);
            }

            return outList;
        }

        public List<FC_Carrier> ObjToTable(List<object> inList)
        {
            List<FC_Carrier> ConvertList = new List<FC_Carrier>();

            foreach (object x in inList)
            {
                ConvertList.Add((FC_Carrier)x);
            }

            return ConvertList;
        }
    }


    public class FC_LocalContract : ParentTable
    {
        public int FC_LocalContractID { get; set; }
        public string Client_Name { get; set; }
        public int Job_type { get; set; }
        public int Quantity { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int Van_type { get; set; }
        public int Contract_Status { get; set; }


        public FC_LocalContract()
        {
            FC_LocalContractID = -1;
            Client_Name = "not_set";
            Job_type = -1;
            Quantity = -1;
            Origin = "not_set";
            Destination = "not_set";
            Van_type = -1;
            Contract_Status = -1;
        }

        public FC_LocalContract(int IN_FC_LocalContractID, string IN_Client_Name, int IN_Job_type, int IN_Quantity, string IN_Origin, string IN_Destination, int IN_Van_type, int IN_Contract_Status = -1)
        {
            FC_LocalContractID = IN_FC_LocalContractID;
            Client_Name = IN_Client_Name;
            Job_type = IN_Job_type;
            Quantity = IN_Quantity;
            Origin = IN_Origin;
            Destination = IN_Destination;
            Van_type = IN_Van_type;
            Contract_Status = IN_Contract_Status;
        }

        public override string GetTableName()
        {
            return "FC_LocalContract";
        }

        public override string GetSelectStatment()
        {
            return "Select * from FC_LocalContract;";
        }

        public override int GetColoumInt()
        {
            return 8;
        }

        public override string GetInsertStatment()
        {
            return "insert into FC_LocalContract(FC_LocalContractID, Client_Name, Job_type, Quantity, Origin, Destination, Van_type, Contract_Status) value (" +
                FC_LocalContractID.ToString() + "," +
                "\"" + Client_Name + "\"," +
                Job_type.ToString() + "," +
                Quantity.ToString() + "," +
                "\"" + Origin + "\"," +
                "\"" + Destination + "\"," +
                Van_type.ToString() + "," +
                Contract_Status.ToString() + ");";
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_LocalContractID");
            outList.Add("Client_Name");
            outList.Add("Job_type");
            outList.Add("Quantity");
            outList.Add("Origin");
            outList.Add("Destination");
            outList.Add("Van_type");
            outList.Add("Contract_Status");

            return outList;
        }


        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            for (int i = 0; i < inList[0].Count; i++)
            {
                FC_LocalContract current = new FC_LocalContract();

                current.FC_LocalContractID = int.Parse(inList[0][i]);
                current.Client_Name = inList[1][i];
                current.Job_type = int.Parse(inList[2][i]);
                current.Quantity = int.Parse(inList[3][i]);
                current.Origin = inList[4][i];
                current.Destination = inList[5][i];
                current.Van_type = int.Parse(inList[6][i]);
                current.Contract_Status = int.Parse(inList[7][i]);

                outList.Add(current);
            }

            return outList;
        }


        public List<FC_LocalContract> ObjToTable(List<object> inList)
        {
            List<FC_LocalContract> ConvertList = new List<FC_LocalContract>();

            foreach (object x in inList)
            {
                ConvertList.Add((FC_LocalContract)x);
            }

            return ConvertList;
        }



    }

    public class FC_ContractFromRuss : ParentTable
    {
        public string Client_Name { get; set; }
        public int Job_type { get; set; }
        public int Quantity { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int Van_type { get; set; }

        public FC_ContractFromRuss()
        {
            Client_Name = "not_set";
            Job_type = -1;
            Quantity = -1;
            Origin = "not_set";
            Destination = "not_set";
            Van_type = -1;
        }

        public FC_ContractFromRuss(string IN_Client_Name, int IN_Job_type, int IN_Quantity, string IN_Origin, string IN_Destination, int IN_Van_type)
        {
            Client_Name = IN_Client_Name;
            Job_type = IN_Job_type;
            Quantity = IN_Quantity;
            Origin = IN_Origin;
            Destination = IN_Destination;
            Van_type = IN_Van_type;
        }

        public override string GetTableName()
        {
            return "FC_ContractFromRuss";
        }

        public override string GetSelectStatment()
        {
            return "Select * from FC_ContractFromRuss;";
        }

        public override int GetColoumInt()
        {
            return 6;
        }

        public override string GetInsertStatment()
        {
            return "insert into FC_ContractFromRuss(Client_Name, Job_type, Quantity, Origin, Destination, Van_type) value (" +
                "\"" + Client_Name + "\"," +
                Job_type.ToString() + "," +
                Quantity.ToString() + "," +
                "\"" + Origin + "\"," +
                "\"" + Destination + "\"," +
                Van_type.ToString() + ");";
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("Client_Name");
            outList.Add("Job_type");
            outList.Add("Quantity");
            outList.Add("Origin");
            outList.Add("Destination");
            outList.Add("Van_type");

            return outList;
        }

        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            for (int i = 0; i < inList[0].Count; i++)
            {
                FC_ContractFromRuss current = new FC_ContractFromRuss();

                current.Client_Name = inList[0][i];
                current.Job_type = int.Parse(inList[1][i]);
                current.Quantity = int.Parse(inList[2][i]);
                current.Origin = inList[3][i];
                current.Destination = inList[4][i];
                current.Van_type = int.Parse(inList[5][i]);

                outList.Add(current);
            }

            return outList;
        }

        public List<FC_ContractFromRuss> ObjToTable(List<object> inList)
        {
            List<FC_ContractFromRuss> ConvertList = new List<FC_ContractFromRuss>();

            foreach (object x in inList)
            {
                ConvertList.Add((FC_ContractFromRuss)x);
            }

            return ConvertList;
        }
    }


    public class FC_CarrierNom : ParentTable
    {
        public int FC_BuyerToPlannerContractID { get; set; }
        public int FC_CarrierID { get; set; }

        public FC_CarrierNom()
        {
            FC_BuyerToPlannerContractID = -1;
            FC_CarrierID = -1;
        }

        public FC_CarrierNom(int IN_FC_BuyerToPlannerContractID, int IN_FC_CarrierID)
        {
            FC_BuyerToPlannerContractID = IN_FC_BuyerToPlannerContractID;
            FC_CarrierID = IN_FC_CarrierID;
        }

        public override string GetTableName()
        {
            return "FC_CarrierNom";
        }

        public override string GetSelectStatment()
        {
            return "Select * from FC_CarrierNom;";
        }

        public override int GetColoumInt()
        {
            return 2;
        }

        public override string GetInsertStatment()
        {
            return "insert into FC_CarrierNom(FC_BuyerToPlannerContractID, FC_CarrierID) value (" +
                FC_BuyerToPlannerContractID.ToString() + "," +
                FC_CarrierID.ToString() + ");";
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_BuyerToPlannerContractID");
            outList.Add("FC_CarrierID");

            return outList;
        }

        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            for (int i = 0; i < inList[0].Count; i++)
            {
                FC_CarrierNom current = new FC_CarrierNom();

                current.FC_BuyerToPlannerContractID = int.Parse(inList[0][i]);
                current.FC_CarrierID = int.Parse(inList[1][i]);

                outList.Add(current);
            }

            return outList;
        }

        public List<FC_CarrierNom> ObjToTable(List<object> inList)
        {
            List<FC_CarrierNom> ConvertList = new List<FC_CarrierNom>();

            foreach (object x in inList)
            {
                ConvertList.Add((FC_CarrierNom)x);
            }

            return ConvertList;
        }


    }

    public class CarrierWithDepot_View : ParentTable
    {
        public int FC_CarrierID { get; set; }
        public string Carrier_Name { get; set; }
        public string CityName { get; set; }
        public int FTL_Availibility { get; set; }
        public int LTL_Availibility { get; set; }
        public double FTL_Rate { get; set; }
        public double LTL_Rate { get; set; }
        public double Reefer_Charge { get; set; }


        public CarrierWithDepot_View()
        {
            FC_CarrierID = -1;
            Carrier_Name = "not_set";
            CityName = "not_set";
            FTL_Availibility = -1;
            LTL_Availibility = -1;
            FTL_Rate = -1;
            LTL_Rate = -1;
            Reefer_Charge = -1;
        }

        public CarrierWithDepot_View(int IN_FC_CarrierID, string IN_Carrier_Name, string IN_CityName, int IN_FTL_Availibility, int IN_LTL_Availibility, double IN_FTL_Rate, double IN_LTL_Rate, double IN_Reefer_Charge)
        {
            FC_CarrierID = IN_FC_CarrierID;
            Carrier_Name = IN_Carrier_Name;
            CityName = IN_CityName;
            FTL_Availibility = IN_FTL_Availibility;
            LTL_Availibility = IN_LTL_Availibility;
            FTL_Rate = IN_FTL_Rate;
            LTL_Rate = IN_LTL_Rate;
            Reefer_Charge = IN_Reefer_Charge;
        }


        public override string GetTableName()
        {
            return "CarrierWithDepot_View";
        }

        public override string GetSelectStatment()
        {
            return "This shoulc not have been called";
        }

        public override int GetColoumInt()
        {
            return 8;
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_CarrierID");
            outList.Add("Carrier_Name");
            outList.Add("CityName");
            outList.Add("FTL_Availibility");
            outList.Add("LTL_Availibility");
            outList.Add("FTL_Rate");
            outList.Add("LTL_Rate");
            outList.Add("Reefer_Charge");

            return outList;
        }


        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            for (int i = 0; i < inList[0].Count; i++)
            {
                CarrierWithDepot_View current = new CarrierWithDepot_View();

                current.FC_CarrierID = int.Parse(inList[0][i]);
                current.Carrier_Name = inList[1][i];
                current.CityName = inList[2][i];
                current.FTL_Availibility = int.Parse(inList[3][i]);
                current.LTL_Availibility = int.Parse(inList[4][i]);
                current.FTL_Rate = double.Parse(inList[5][i]);
                current.LTL_Rate = double.Parse(inList[6][i]);
                current.Reefer_Charge = double.Parse(inList[7][i]);

                outList.Add(current);
            }

            return outList;
        }


        public List<CarrierWithDepot_View> ObjToTable(List<object> inList)
        {
            List<CarrierWithDepot_View> ConvertList = new List<CarrierWithDepot_View>();

            foreach (object x in inList)
            {
                ConvertList.Add((CarrierWithDepot_View)x);
            }

            return ConvertList;
        }



    }


}
