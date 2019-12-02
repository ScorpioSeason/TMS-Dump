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
}
