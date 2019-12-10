using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMSwPages.Classes
{

    public struct RouteSumData
    {
        public double totalDriveTime;
        public double totalTripTime;
        public int totalKM;
        public int OriginCity;
        public int DestinationCity;
    };

    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		CityNode
    *   \brief		This class runs the CityNode functionality
    *   \details	... static class?  
    *   
    * -------------------------------------------------------------------------------------------------------- */
    public class CityNode
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public CityNode East { get; set; }
        public int EastKM { get; set; }
        public double EastHour { get; set; }
        public CityNode West { get; set; }
        public int WestKM { get; set; }
        public double WestHour { get; set; }


        public CityNode(string inName, int inCityId)
        {
            CityName = inName;
            CityID = inCityId;
        }
    }

    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		MappingClass
    *   \brief		This class will be used by the planner to calculate route information for trip tickets.
    *   \details	This is a public class that will be used by the planner.
    * -------------------------------------------------------------------------------------------------------- */
    public class MappingClass
    {
        private const int Number_of_Cities = 8;
        public List<CityNode> nodes = new List<CityNode>();



        // MappingClass Constructor HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			MappingClass() Constructor
        *	\brief		The constructor for the Mapping Class
        *	\details	The data for each city will be read out of the database. It will then converted into a List datastructure so that it can be steped though easily.
        *	\param[in]	null
        *	\param[out]	null
        *	\exception	null
        *	\see		na
        *	\return		null
        * ---------------------------------------------------------------------------------------------------- */
        public MappingClass()
        {

            CityNode Windsor = new CityNode("Windsor", 0);
            CityNode London = new CityNode("London", 1);
            CityNode Hamilton = new CityNode("Hamilton", 2);
            CityNode Toronto = new CityNode("Toronto", 3);
            CityNode Oshawa = new CityNode("Oshawa", 4);
            CityNode Belleville = new CityNode("Belleville", 5);
            CityNode Kingston = new CityNode("Kingston", 6);
            CityNode Ottawa = new CityNode("Ottawa", 7);

            Windsor.West = null;
            London.West = Windsor;
            Hamilton.West = London;
            Toronto.West = Hamilton;
            Oshawa.West = Toronto;
            Belleville.West = Oshawa;
            Kingston.West = Belleville;
            Ottawa.West = Kingston;

            Windsor.East = London;
            London.East = Hamilton;
            Hamilton.East = Toronto;
            Toronto.East = Oshawa;
            Oshawa.East = Belleville;
            Belleville.East = Kingston;
            Kingston.East = Ottawa;
            Ottawa.East = null;

            nodes.Add(Windsor);
            nodes.Add(London);
            nodes.Add(Hamilton);
            nodes.Add(Toronto);
            nodes.Add(Oshawa);
            nodes.Add(Belleville);
            nodes.Add(Kingston);
            nodes.Add(Ottawa);

            int[] eastKMArray = new int[Number_of_Cities] { 191, 128, 68, 60, 134, 82, 196, -1 };
            for (int i = 0; i < Number_of_Cities; i++)
            {
                nodes[i].EastKM = eastKMArray[i];
            }

            int[] westKMArray = new int[Number_of_Cities] { -1, 191, 128, 68, 60, 134, 82, 196 };
            for (int i = 0; i < Number_of_Cities; i++)
            {
                nodes[i].WestKM = westKMArray[i];
            }

            double[] eastHourArray = new double[Number_of_Cities] { 2.5, 1.75, 1.25, 1.3, 1.65, 1.2, 2.5, -1 };
            for (int i = 0; i < Number_of_Cities; i++)
            {
                nodes[i].EastHour = eastHourArray[i];
            }

            double[] WestHourArray = new double[Number_of_Cities] { -1, 2.5, 1.75, 1.25, 1.3, 1.65, 1.2, 2.5 };
            for (int i = 0; i < Number_of_Cities; i++)
            {
                nodes[i].WestHour = WestHourArray[i];
            }

            int k = 0;
        }

        // GetTravelData METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			List<RouteData> GetTravelData(int OriginID, int DestinationID, bool FLTorLTL)
        *	\brief		This method will determine the info for a trip the between to cities.
        *	\details	A list of RouteData structs, each struct will hold all timing and KM data. 
        *	            The timing for each element will keep track of the pick up time at the origin city, the drop off time at the destination, and the LTL time at intermediate cities.
        *	\param[in]	int OriginID, int DestinationID, bool FLTorLTL  The origin and destination cities, and if the trip is Flt or Ltl.
        *	\param[out]	null
        *	\exception	null
        *	\see		Struct: RouteData
        *	\return		List<RouteData> A list of the RouteData structs. The list all together will hold all the data for a trip.
        *
        * ---------------------------------------------------------------------------------------------------- */
        public List<FC_RouteSeg> GetTravelData(string Origin, string Destination, int inFTL, int InTicketID) //one is ltl
        {
            int OriginID = LoadCSV.ToCityID(Origin);
            int DestinationID = LoadCSV.ToCityID(Destination);

            bool FTL = true;

            if (inFTL == 1)
            {
                FTL = false;
            }

            //figure out if we need to travel east or west
            CityNode current = nodes.Find(x => x.CityID == OriginID);
            CityNode nextCity;

            List<FC_RouteSeg> returnList = new List<FC_RouteSeg>();

            if (OriginID >= 0 && OriginID < Number_of_Cities && DestinationID >= 0 && DestinationID < Number_of_Cities && OriginID != DestinationID)
            {
                do
                {
                    FC_RouteSeg tripDataPassBack = new FC_RouteSeg(InTicketID, 0, 0, 0, 0, 0, 0, 0);

                    if (OriginID > DestinationID)
                    {
                        //going west
                        nextCity = current.West;
                        tripDataPassBack.KM = current.WestKM;
                        tripDataPassBack.DrivenTime = current.WestHour;
                    }
                    else
                    {
                        //going east
                        nextCity = current.East;
                        tripDataPassBack.KM = current.EastKM;
                        tripDataPassBack.DrivenTime = current.EastHour;
                    }

                    tripDataPassBack.CityA = current.CityID;
                    tripDataPassBack.CityB = nextCity.CityID;

                    if (current.CityID == OriginID)
                    {
                        tripDataPassBack.PickUpTime += 2;
                    }

                    if (nextCity.CityID == DestinationID)
                    {
                        tripDataPassBack.DropOffTime += 2;
                    }

                    if (!FTL && (nextCity.CityID != DestinationID))
                    {
                        tripDataPassBack.LtlTime += 2;
                    }


                    returnList.Add(tripDataPassBack);

                    current = nextCity;

                } while (nextCity.CityID != DestinationID);

                return returnList;
            }

            return null;
        }

        public bool AtOrPastCity(FC_LocalContract inContract, FC_TripTicket inTicket)
        {
            int OrginId = LoadCSV.ToCityID(inContract.Origin);
            int DestId = LoadCSV.ToCityID(inContract.Destination);

            int current = LoadCSV.ToCityID(inTicket.CurrentLocation);

            if (OrginId > DestId)
            {
                //going west

                if(current <= DestId)
                {
                    return true;
                }
            }
            else
            {
                if (current >= DestId)
                {
                    return true;
                }
            }

            return  false;
        }

        // SummerizeTrip METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			RouteSumData SummerizeTrip(List<RouteData> inData)
        *	\brief		Summarize a list of Route Data structs into the total time and Km. This data will be used in invoice generation.
        *	\details	The resultant struct will hold the Origin City, Destination City, total drive time, total trip time(drive + load/unload/LTL), and total KM.
        *	\param[in]	List<RouteData> inData The List holding all the data for the trip.
        *	\param[out]	null
        *	\exception	null
        *	\see		na
        *	\return		RouteSumData The condensed data
        * ---------------------------------------------------------------------------------------------------- */
        public RouteSumData SummerizeTrip(List<FC_RouteSeg> inData)
        {
            RouteSumData outData = new RouteSumData();
            outData.DestinationCity = -1;

            if (inData == null)
            {
                foreach (FC_RouteSeg x in inData)
                {
                    outData.totalDriveTime += x.DrivenTime;

                    outData.totalTripTime += x.DrivenTime;

                    outData.totalTripTime += x.PickUpTime;
                    outData.totalTripTime += x.DropOffTime;
                    outData.totalTripTime += x.LtlTime;

                    outData.totalKM += x.KM;
                }

                outData.OriginCity = inData[0].CityA;
                outData.DestinationCity = inData[inData.Count - 1].CityB;
            }

            return outData;
        }
    }
}
