using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{

    public static class LoadCMPintoDatabase
    {
        public static void Load()
        {
            SQL_Query sQL = new SQL_Query();
            List<Contract> readInContracts = new List<Contract>();

            sQL.OpenConnection();
            List<string>[] downCMP = sQL.Select_Contracts();
            sQL.CloseConnection();

            for (int i = 0; i < downCMP[0].Count; i++)
            {
                Contract current = new Contract();

                current.client_Name = downCMP[0][i];
                current.job_Type = int.Parse(downCMP[1][i]);
                current.quantity = int.Parse(downCMP[2][i]);
                current.origin = downCMP[3][i];
                current.destination = downCMP[4][i];
                current.van_Type = int.Parse(downCMP[5][i]);

                readInContracts.Add(current);
            }

            PlannerSQL plannerSQL = new PlannerSQL();

            plannerSQL.Open();
            plannerSQL.PushLocalContracts(readInContracts);
            plannerSQL.Close();

        }

        
    }



    public class Noninated_Contract
    {
        public int New_ContractID;
        public Contract TheContract;
    }

    public class Contract
    {
        public int ContractID
        {
            get;
            set;
        }

        public string client_Name
        {
            get;
            set;
        }
        public int job_Type
        {
            get;
            set;
        }
        public int quantity
        {
            get;
            set;
        }
        public string origin
        {
            get;
            set;
        }
        public string destination
        {
            get;
            set;
        }
        public int van_Type
        {
            get;
            set;
        }

        public static int ToCityID(string inputCity)
        {
            inputCity = inputCity.ToUpper();

            if (inputCity == "WINDSOR")
            {
                return 0;
            }
            else if (inputCity == "LONDON")
            {
                return 1;
            }
            else if (inputCity == "HAMILTON")
            {
                return 2;
            }
            else if (inputCity == "TORONTO")
            {
                return 3;
            }
            else if (inputCity == "OSHAWA")
            {
                return 4;
            }
            else if (inputCity == "BELLEVILLE")
            {
                return 5;
            }
            else if (inputCity == "KINGSTON")
            {
                return 6;
            }
            else if (inputCity == "OTTAWA")
            {
                return 7;
            }

            return -1;
        }
    }

    public class CarrierLine
    {
        public int New_ContractID;
        public int CarrierID;
    }


    public class CompleteNomination
    {
        public int New_ContractID;
        public Contract theContract;
        public List<Carrier> ListOfCarriers;

        public CompleteNomination()
        {
            New_ContractID = -1;
            theContract = null;
            ListOfCarriers = new List<Carrier>();
        }
    }

    public class CarrierDepot
    {
        public int CarrierID;
        public string CityName;
        public int FTL_Availibility;
        public int LTL_Availibility;
        public double FTL_Rate;
        public double LTL_Rate;
        public double reeferCharge;

        public CarrierDepot()
        {

        }

        public CarrierDepot(int inCarrier, string inCity, int inFTLA, int inLTLA, double inFTLRate, double inLTLRate, double inReeferCharge)
        {
            CarrierID = inCarrier;
            CityName = inCity;
            FTL_Availibility = inFTLA;
            LTL_Availibility = inLTLA;
            FTL_Rate = inFTLRate;
            LTL_Rate = inLTLRate;
            reeferCharge = inReeferCharge;
        }
    }


    public class Carrier
    {
        public int CarrierID;
        public string CarrierName;
        public List<CarrierDepot> CityList;

        public Carrier()
        {
            CarrierID = -1;
            CarrierName = "";
            CityList = new List<CarrierDepot>();
        }

        public Carrier(int inCarrierID, string Carrier_Name)
        {
            CarrierID = inCarrierID;
            CarrierName = Carrier_Name;
            CityList = new List<CarrierDepot>();
        }


        public void AddCity(string inCity, int inFTLA, int inLTLA, double inFTLRate, double inLTLRate, double inReeferCharge)
        {
            CarrierDepot temp = new CarrierDepot(CarrierID, inCity, inFTLA, inLTLA, inFTLRate, inLTLRate, inReeferCharge);
            CityList.Add(temp);
        }
    }
}
