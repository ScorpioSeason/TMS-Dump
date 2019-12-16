using System;
using System.Collections.Generic;

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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetTableName()
        {
            return "FC_DepotCity";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetSelectStatment()
        {
            return "Select * from FC_DepotCity;";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override int GetColoumInt()
        {
            return 7;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();
            
            try
            {
                
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

                    TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_DepotCity" + " | " + "PackageClasses" + " | " + "Confirmation" + " | " + "Depot city packaged" + " | ");
                    
                }
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_DepotCity" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }

            return outList;
            
        }
        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public FC_TripTicketLine()
        {
            FC_TripTicketID = -1;
            FC_LocalContractID = -1;
            PalletsOnTicket = -1;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public FC_TripTicketLine(int IN_FC_TripTicketID, int IN_FC_LocalContractID, int IN_PalletsOnTicket = 1)
        {
            FC_TripTicketID = IN_FC_TripTicketID;
            FC_LocalContractID = IN_FC_LocalContractID;
            PalletsOnTicket = IN_PalletsOnTicket;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetTableName()
        {
            return "FC_TripTicketLine";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetSelectStatment()
        {
            return "Select * from FC_TripTicketLine;";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override int GetColoumInt()
        {
            return 3;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetInsertStatment()
        {
            return "insert into FC_TripTicketLine(FC_TripTicketID, FC_LocalContractID, PalletsOnTicket) value (" +
                FC_TripTicketID.ToString() + "," +
                FC_LocalContractID.ToString() + "," +
                PalletsOnTicket.ToString() + ");";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_TripTicketID");
            outList.Add("FC_LocalContractID");
            outList.Add("PalletsOnTicket");

            return outList;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            try
            {
                for (int i = 0; i < inList[0].Count; i++)
                {
                    FC_TripTicketLine current = new FC_TripTicketLine();

                    current.FC_TripTicketID = int.Parse(inList[0][i]);
                    current.FC_LocalContractID = int.Parse(inList[1][i]);
                    current.PalletsOnTicket = int.Parse(inList[2][i]);

                    outList.Add(current);
                }
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_TripTicketLine" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetTableName()
        {
            return "FC_RouteSeg";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetSelectStatment()
        {
            return "Select * from FC_RouteSeg;";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override int GetColoumInt()
        {
            return 8;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            try
            {
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
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_RouteSeg" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }
            
            return outList;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

    public class FC_TripTicket_WProgress : ParentTable
    {
        public FC_TripTicket instance = new FC_TripTicket();
        public int FC_TripTicketID { get; set; }
        public int FC_CarrierID { get; set; }
        public string CurrentLocation { get; set; }
        public int Size_in_Palettes { get; set; }
        public int Days_Passes { get; set; }
        public int Is_Complete { get; set; }
        public int progress { get; set; }

        public FC_TripTicket_WProgress(FC_TripTicket temp)
        {
            instance = temp;
            FC_TripTicketID = temp.FC_TripTicketID;
            FC_CarrierID = temp.FC_CarrierID;
            CurrentLocation = temp.CurrentLocation;
            Size_in_Palettes = temp.Size_in_Palettes;
            Days_Passes = temp.Days_Passes;
            Is_Complete = temp.Is_Complete;
            progress = 0;
        }

        public void GetTicketProgress(FC_TripTicket InTicket)
        {
            string Location = InTicket.CurrentLocation;

            List<FC_RouteSeg> TicketSegs = PlannerClass.RoutSegsPerTicket_Populate(InTicket);

            List<FC_RouteSeg> PassedTickets = new List<FC_RouteSeg>();

            foreach (FC_RouteSeg x in TicketSegs)
            {
                if (LoadCSV.ToCityName(x.CityA).ToUpper() == InTicket.CurrentLocation.ToUpper())
                {
                    break;
                }

                PassedTickets.Add(x);
            }

            RouteSumData TraveledData = new RouteSumData();
            TraveledData = TraveledData.SummerizeTrip(PassedTickets);

            RouteSumData TotalData = new RouteSumData();
            TotalData = TotalData.SummerizeTrip(TicketSegs);

            double output = (double)TraveledData.totalKM / (double)TotalData.totalKM;
            output *= 100;

            int outInt = (int)output;

            progress = outInt;

            TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_TripTicket_WProgress" + " | " + "GetTicketProgress" + " | " + "Confirmation" + " | " + "Ticket progress calculated" + " | ");

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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public FC_TripTicket(int IN_FC_TripTicketID, int IN_FC_CarrierID, string IN_CurrentLocation, int IN_Size_in_Palettes, int IN_Days_Passes, int IN_Is_Complete)
        {
            FC_TripTicketID = IN_FC_TripTicketID;
            FC_CarrierID = IN_FC_CarrierID;
            CurrentLocation = IN_CurrentLocation;
            Size_in_Palettes = IN_Size_in_Palettes;
            Days_Passes = IN_Days_Passes;
            Is_Complete = IN_Is_Complete;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetTableName()
        {
            return "FC_TripTicket";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetSelectStatment()
        {
            return "Select * from FC_TripTicket;";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override int GetColoumInt()
        {
            return 6;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            try
            {
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
            }
            catch(Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_TripTicket" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }
            
            return outList;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public FC_BuyerToPlannerContract()
        {
            FC_BuyerToPlannerContractID = -1;
            FC_LocalContractID = -1;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public FC_BuyerToPlannerContract(int IN_FC_BuyerToPlannerContractID, int IN_FC_LocalContractID)
        {
            FC_BuyerToPlannerContractID = IN_FC_BuyerToPlannerContractID;
            FC_LocalContractID = IN_FC_LocalContractID;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetTableName()
        {
            return "FC_BuyerToPlannerContract";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetSelectStatment()
        {
            return "Select * from FC_BuyerToPlannerContract;";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override int GetColoumInt()
        {
            return 2;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetInsertStatment()
        {
            return "insert into FC_BuyerToPlannerContract(FC_BuyerToPlannerContractID, FC_LocalContractID) value (" +
                FC_BuyerToPlannerContractID.ToString() + "," +
                FC_LocalContractID.ToString() + ");";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_BuyerToPlannerContractID");
            outList.Add("FC_LocalContractID");

            return outList;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            try
            {
                for (int i = 0; i < inList[0].Count; i++)
                {
                    FC_BuyerToPlannerContract current = new FC_BuyerToPlannerContract();

                    current.FC_BuyerToPlannerContractID = int.Parse(inList[0][i]);
                    current.FC_LocalContractID = int.Parse(inList[1][i]);

                    outList.Add(current);
                }
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_BuyerToPlannerContract" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }
            
            return outList;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public FC_Carrier()
        {
            FC_CarrierID = -1;
            Carrier_Name = "not_set";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public FC_Carrier(int IN_FC_CarrierID, string IN_Carrier_Name)
        {
            FC_CarrierID = IN_FC_CarrierID;
            Carrier_Name = IN_Carrier_Name;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetTableName()
        {
            return "FC_Carrier";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetSelectStatment()
        {
            return "Select * from FC_Carrier;";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override int GetColoumInt()
        {
            return 2;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetInsertStatment()
        {
            return "insert into FC_Carrier(FC_CarrierID, Carrier_Name) value (" +
                FC_CarrierID.ToString() + "," +
                "\"" + Carrier_Name + "\");";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_CarrierID");
            outList.Add("Carrier_Name");

            return outList;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            try
            {
                for (int i = 0; i < inList[0].Count; i++)
                {
                    FC_Carrier current = new FC_Carrier();

                    current.FC_CarrierID = int.Parse(inList[0][i]);
                    current.Carrier_Name = inList[1][i];

                    outList.Add(current);
                }
            }
            catch(Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_Carrier" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }

            return outList;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetTableName()
        {
            return "FC_LocalContract";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetSelectStatment()
        {
            return "Select * from FC_LocalContract;";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override int GetColoumInt()
        {
            return 8;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            try
            {
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
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_LocalContracts" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }
            
            return outList;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public FC_ContractFromRuss()
        {
            Client_Name = "not_set";
            Job_type = -1;
            Quantity = -1;
            Origin = "not_set";
            Destination = "not_set";
            Van_type = -1;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public FC_ContractFromRuss(string IN_Client_Name, int IN_Job_type, int IN_Quantity, string IN_Origin, string IN_Destination, int IN_Van_type)
        {
            Client_Name = IN_Client_Name;
            Job_type = IN_Job_type;
            Quantity = IN_Quantity;
            Origin = IN_Origin;
            Destination = IN_Destination;
            Van_type = IN_Van_type;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetTableName()
        {
            return "FC_ContractFromRuss";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetSelectStatment()
        {
            return "Select * from FC_ContractFromRuss;";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override int GetColoumInt()
        {
            return 6;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            try
            {
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
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_ContractsFromRuss" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }
            
            return outList;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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


        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public FC_CarrierNom()
        {
            FC_BuyerToPlannerContractID = -1;
            FC_CarrierID = -1;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public FC_CarrierNom(int IN_FC_BuyerToPlannerContractID, int IN_FC_CarrierID)
        {
            FC_BuyerToPlannerContractID = IN_FC_BuyerToPlannerContractID;
            FC_CarrierID = IN_FC_CarrierID;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetTableName()
        {
            return "FC_CarrierNom";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetSelectStatment()
        {
            return "Select * from FC_CarrierNom;";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override int GetColoumInt()
        {
            return 2;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetInsertStatment()
        {
            return "insert into FC_CarrierNom(FC_BuyerToPlannerContractID, FC_CarrierID) value (" +
                FC_BuyerToPlannerContractID.ToString() + "," +
                FC_CarrierID.ToString() + ");";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_BuyerToPlannerContractID");
            outList.Add("FC_CarrierID");

            return outList;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            try
            {
                for (int i = 0; i < inList[0].Count; i++)
                {
                    FC_CarrierNom current = new FC_CarrierNom();

                    current.FC_BuyerToPlannerContractID = int.Parse(inList[0][i]);
                    current.FC_CarrierID = int.Parse(inList[1][i]);

                    outList.Add(current);
                }
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_CarrierNom" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }
            
            return outList;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetTableName()
        {
            return "CarrierWithDepot_View";
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override string GetSelectStatment()
        {
            TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "Carrier_WithDepotView" + " | " + "GetSelectStatement" + " | " + "Oh heck" + " | " + "This should not have been called" + " | ");
            return "This should not have been called";
        }


        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override int GetColoumInt()
        {
            return 8;
        }


        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            try
            {
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
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "CarrierWithDepot_View" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }
            
            return outList;
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn		
        *	\brief			
        *	\param[in]
        *	\param[out]	 
        *	\return		
        * ---------------------------------------------------------------------------------------------------- */
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

    public class FC_Customer : ParentTable
    {
        public int FC_CustomerID { get; set; }
        public string CustName { get; set; }
        public double Balance { get; set; }


        public FC_Customer()
        {
            FC_CustomerID = -1;
            CustName = "not_set";
            Balance = -1;
        }

        public FC_Customer(int IN_FC_CustomerID, string IN_CustName, double IN_Balance)
        {
            FC_CustomerID = IN_FC_CustomerID;
            CustName = IN_CustName;
            Balance = IN_Balance;
        }


        public override string GetTableName()
        {
            return "FC_Customer";
        }

        public override string GetSelectStatment()
        {
            return "Select * from FC_Customer;";
        }

        public override int GetColoumInt()
        {
            return 3;
        }

        public override string GetInsertStatment()
        {
            return "insert into FC_Customer(FC_CustomerID, CustName, Balance) value (" +
                FC_CustomerID.ToString() + "," +
                "\"" + CustName + "\"," +
                Balance.ToString() + ");";
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_CustomerID");
            outList.Add("CustName");
            outList.Add("Balance");

            return outList;
        }

        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            try
            {
                for (int i = 0; i < inList[0].Count; i++)
                {
                    FC_Customer current = new FC_Customer();

                    current.FC_CustomerID = int.Parse(inList[0][i]);
                    current.CustName = inList[1][i];
                    current.Balance = double.Parse(inList[2][i]);

                    outList.Add(current);
                }
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_Customer" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }

            return outList;
        }


        public List<FC_Customer> ObjToTable(List<object> inList)
        {
            List<FC_Customer> ConvertList = new List<FC_Customer>();

            foreach (object x in inList)
            {
                ConvertList.Add((FC_Customer)x);
            }

            return ConvertList;
        }
    }

    public class FC_CustContratLine : ParentTable
    {
        public int FC_CustomerID { get; set; }
        public int FC_LocalContractID { get; set; }

        public FC_CustContratLine()
        {
            FC_CustomerID = -1;
            FC_LocalContractID = -1;
        }

        public FC_CustContratLine(int IN_FC_CustomerID, int IN_FC_LocalContractID)
        {
            FC_CustomerID = IN_FC_CustomerID;
            FC_LocalContractID = IN_FC_LocalContractID;
        }


        public override string GetTableName()
        {
            return "FC_CustContratLine";
        }

        public override string GetSelectStatment()
        {
            return "Select * from FC_CustContratLine;";
        }

        public override int GetColoumInt()
        {
            return 2;
        }

        public override string GetInsertStatment()
        {
            return "insert into FC_CustContratLine(FC_CustomerID, FC_LocalContractID) value (" +
                FC_CustomerID.ToString() + "," +
                FC_LocalContractID.ToString() + ");";
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_CustomerID");
            outList.Add("FC_LocalContractID");

            return outList;
        }


        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            try
            {
                for (int i = 0; i < inList[0].Count; i++)
                {
                    FC_CustContratLine current = new FC_CustContratLine();

                    current.FC_CustomerID = int.Parse(inList[0][i]);
                    current.FC_LocalContractID = int.Parse(inList[1][i]);

                    outList.Add(current);
                }
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_CustContractLine" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }
            

            return outList;
        }


        public List<FC_CustContratLine> ObjToTable(List<object> inList)
        {
            List<FC_CustContratLine> ConvertList = new List<FC_CustContratLine>();

            foreach (object x in inList)
            {
                ConvertList.Add((FC_CustContratLine)x);
            }

            return ConvertList;
        }
    }

    public class FC_Invoice : ParentTable
    {
        public int FC_InvoiceID { get; set; }
        public double TotalCost { get; set; }


        public FC_Invoice()
        {
            FC_InvoiceID = -1;
            TotalCost = -1;
        }

        public FC_Invoice(int IN_FC_InvoiceID, double IN_TotalCost)
        {
            FC_InvoiceID = IN_FC_InvoiceID;
            TotalCost = IN_TotalCost;
        }


        public override string GetTableName()
        {
            return "FC_Invoice";
        }

        public override string GetSelectStatment()
        {
            return "Select * from FC_Invoice;";
        }

        public override int GetColoumInt()
        {
            return 2;
        }

        public override string GetInsertStatment()
        {
            return "insert into FC_Invoice(FC_InvoiceID, TotalCost) value (" +
                FC_InvoiceID.ToString() + "," +
                TotalCost.ToString() + ");";
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_InvoiceID");
            outList.Add("TotalCost");

            return outList;
        }


        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();
            try
            {
                for (int i = 0; i < inList[0].Count; i++)
                {
                    FC_Invoice current = new FC_Invoice();

                    current.FC_InvoiceID = int.Parse(inList[0][i]);
                    current.TotalCost = double.Parse(inList[1][i]);

                    outList.Add(current);
                }
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_Invoice" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }

            return outList;
        }
 
        public List<FC_Invoice> ObjToTable(List<object> inList)
        {
            List<FC_Invoice> ConvertList = new List<FC_Invoice>();

            foreach (object x in inList)
            {
                ConvertList.Add((FC_Invoice)x);
            }

            return ConvertList;
        }
    }

    public class Contract_Invoice
    {

        public List<FC_LocalContract> theContracts;
        public double TotalCost { get; set; }

        public Contract_Invoice(List<FC_LocalContract> inContract, FC_Invoice InInvoice)
        {
            TotalCost = InInvoice.TotalCost;
            theContracts = inContract;
        }
    }

    public class FC_InvoiceContractLine : ParentTable
    {
        public int FC_LocalContractID { get; set; }
        public int FC_InvoiceID { get; set; }


        public FC_InvoiceContractLine()
        {
            FC_LocalContractID = -1;
            FC_InvoiceID = -1;
        }

        public FC_InvoiceContractLine(int IN_FC_LocalContractID, int IN_FC_InvoiceID)
        {
            FC_LocalContractID = IN_FC_LocalContractID;
            FC_InvoiceID = IN_FC_InvoiceID;
        }

        public override string GetTableName()
        {
            return "FC_InvoiceContractLine";
        }

        public override string GetSelectStatment()
        {
            return "Select * from FC_InvoiceContractLine;";
        }

        public override int GetColoumInt()
        {
            return 2;
        }

        public override string GetInsertStatment()
        {
            return "insert into FC_InvoiceContractLine(FC_LocalContractID, FC_InvoiceID) value (" +
                FC_LocalContractID.ToString() + "," +
                FC_InvoiceID.ToString() + ");";
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("FC_LocalContractID");
            outList.Add("FC_InvoiceID");

            return outList;
        }

        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            try
            {
                for (int i = 0; i < inList[0].Count; i++)
                {
                    FC_InvoiceContractLine current = new FC_InvoiceContractLine();

                    current.FC_LocalContractID = int.Parse(inList[0][i]);
                    current.FC_InvoiceID = int.Parse(inList[1][i]);

                    outList.Add(current);
                }
            }
            catch (Exception e)
            {
                TMSLogger.LogIt(" | " + "ParentTables.cs" + " | " + "FC_InvoiceContractLine" + " | " + "PackageClasses" + " | " + e.GetType().ToString() + " | " + e.Message + " | ");
            }
            

            return outList;
        }

        public List<FC_InvoiceContractLine> ObjToTable(List<object> inList)
        {
            List<FC_InvoiceContractLine> ConvertList = new List<FC_InvoiceContractLine>();

            foreach (object x in inList)
            {
                ConvertList.Add((FC_InvoiceContractLine)x);
            }

            return ConvertList;
        }
    }

    public class Cust_Price : ParentTable
    {
        public string Client_Name { get; set; }
        public double BalanceTotal { get; set; }


        public Cust_Price()
        {
            Client_Name = "not_set";
            BalanceTotal = -1;
        }

        public Cust_Price(string IN_Client_Name, double IN_BalanceTotal)
        {
            Client_Name = IN_Client_Name;
            BalanceTotal = IN_BalanceTotal;
        }


        public override string GetTableName()
        {
            return "Cust_Price";
        }

        public override string GetSelectStatment()
        {
            return "Select c.Client_Name, SUM(inn.TotalCost) as BalanceTotal " +
                "from FC_LocalContract as c " +
                "left join FC_InvoiceContractLine as cl on cl.FC_LocalContractID = c.FC_LocalContractID " +
                "left join FC_Invoice as inn on cl.FC_InvoiceID = inn.FC_InvoiceID " +
                "group by(c.Client_Name);";
        }

        public override int GetColoumInt()
        {
            return 2;
        }

        public override string GetInsertStatment()
        {
            return "insert into Cust_Price(Client_Name, BalanceTotal) value (" +
                "\"" + Client_Name + "\"," +
                BalanceTotal.ToString() + ");";
        }

        public override List<string> GetColoumNames()
        {
            List<string> outList = new List<string>();

            outList.Add("Client_Name");
            outList.Add("BalanceTotal");

            return outList;
        }


        public override List<object> PackageClasses(List<string>[] inList)
        {
            List<object> outList = new List<object>();

            for (int i = 0; i < inList[0].Count; i++)
            {
                Cust_Price current = new Cust_Price();

                current.Client_Name = inList[0][i];
                current.BalanceTotal = double.Parse(inList[1][i]);

                outList.Add(current);
            }

            return outList;
        }


        public List<Cust_Price> ObjToTable(List<object> inList)
        {
            List<Cust_Price> ConvertList = new List<Cust_Price>();

            foreach (object x in inList)
            {
                ConvertList.Add((Cust_Price)x);
            }

            return ConvertList;
        }



    }

}
