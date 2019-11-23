// MappingClass FILE HEADER COMMENT: =================================================================================
/**
 *  \file		MappingClass.cs
 *  \ingroup	TMS
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Duane
 *  \brief	    This file contains the functionality for Route Calculation. This will be used by the Planner.
 *  \see		PlannerClass.cs
 *  \details    This file holds the struct definitions that will be used to pass data from class to class.                                       
 *              It also holds the definition Mapping class. This class has the functionality that is needed to calculate route data for trip tickets.
 *
 * =========================================================================================================== */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This class calculates the optimal route for a new trip. This calculation would
// occur after the Buyer has selected cities for the order, and the planner 
// nominates carriers for the order. 

namespace Transport_Management_System_WPF
{

    public struct RouteData
    {
        public int RouteDataID;
        public int KM;
        public double PickupTime;
        public double DriveTime;
        public double LtlTime;
        public double DropoffTime;
        public int CityA;
        public int CityB;
    }

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
        *
        * ---------------------------------------------------------------------------------------------------- */
        public MappingClass()
        {

            CityNode Windsor = new CityNode ("Windsor", 0 );
            CityNode London = new CityNode ( "London", 1 );
            CityNode Hamilton = new CityNode ( "Hamilton", 2 );
            CityNode Toronto = new CityNode ( "Toronto", 3 );
            CityNode Oshawa = new CityNode ( "Oshawa", 4 );
            CityNode Belleville = new CityNode ( "Belleville", 5 );
            CityNode Kingston = new CityNode ( "Kingston", 6 );
            CityNode Ottawa = new CityNode ( "Ottawa", 7 );

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
        public List<RouteData> GetTravelData(int OriginID, int DestinationID, bool FLTorLTL) //ftl is true
        {
            //figure out if we need to travel east or west
            CityNode current = nodes.Find(x => x.CityID == OriginID);
            CityNode nextCity;

            List<RouteData> returnList = new List<RouteData>();

            if(OriginID >= 0 && OriginID < Number_of_Cities && DestinationID >= 0 && DestinationID < Number_of_Cities && OriginID != DestinationID)
            {
                do
                {
                    RouteData tripDataPassBack = new RouteData();

                    if (OriginID > DestinationID)
                    {
                        //going west
                        nextCity = current.West;
                        tripDataPassBack.KM = current.WestKM;
                        tripDataPassBack.DriveTime = current.WestHour;
                    }
                    else
                    {
                        //going east
                        nextCity = current.East;
                        tripDataPassBack.KM = current.EastKM;
                        tripDataPassBack.DriveTime = current.EastHour;
                    }

                    tripDataPassBack.CityA = current.CityID;
                    tripDataPassBack.CityB = nextCity.CityID;

                    if (current.CityID == OriginID)
                    {
                        tripDataPassBack.PickupTime += 2;
                    }

                    if (nextCity.CityID == DestinationID)
                    {
                        tripDataPassBack.DropoffTime += 2;
                    }

                    if (FLTorLTL && !(nextCity.CityID == DestinationID))
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
        public RouteSumData SummerizeTrip(List<RouteData> inData)
        {
            RouteSumData outData = new RouteSumData();
            outData.DestinationCity = -1;

            if (inData == null)
            {
                foreach (RouteData x in inData)
                {
                    outData.totalDriveTime += x.DriveTime;

                    outData.totalTripTime += x.DriveTime;

                    outData.totalTripTime += x.PickupTime;
                    outData.totalTripTime += x.DropoffTime;
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
