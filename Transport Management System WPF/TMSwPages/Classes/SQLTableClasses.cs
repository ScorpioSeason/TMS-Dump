using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSwPages.Classes
{
    public class CarrierDepot
    {
        public int CarrierID { get; set; }
        public string CityName{ get; set; }
        public int FTL_Availibility{ get; set; }
        public int LTL_Availibility{ get; set; }
        public double FTL_Rate{ get; set; }
        public double LTL_Rate{ get; set; }
        public double Reefer_Charge{ get; set; }

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
            Reefer_Charge = inReeferCharge;
        }
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
