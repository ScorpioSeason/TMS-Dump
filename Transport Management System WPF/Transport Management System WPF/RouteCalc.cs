using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{
    public struct RouteData
    {
        public int KM;
        public double StopTime;
        public double DriveTime;
        public int CityA;
        public int CityB;
    }
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

    public class GraphClass
    {
        private const int Number_of_Cities = 8;
        public List<CityNode> nodes = new List<CityNode>();

        public GraphClass()
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
            Ottawa.East = Kingston;

            Windsor.East = London;
            London.East = Hamilton;
            Hamilton.East = Toronto;
            Toronto.East = Oshawa;
            Oshawa.East = Belleville;
            Belleville.East = Kingston;
            Kingston.East = Oshawa;
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

            int j = 12;
        }



        public List<RouteData> getTravelData(int OriginID, int DestinationID, bool FLTorLTL) //ftl is true
        {
            //figure out if we need to travel east or west
            CityNode current = nodes.Find(x => x.CityID == OriginID);
            CityNode nextCity;

            List<RouteData> returnList = new List<RouteData>();

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
                    tripDataPassBack.StopTime += 2;
                }

                if (nextCity.CityID == DestinationID)
                {
                    tripDataPassBack.StopTime += 2;
                }

                if (current.CityID != OriginID && nextCity.CityID != DestinationID)
                {
                    if (FLTorLTL)
                    {
                        tripDataPassBack.StopTime += 2;
                    }
                }

                returnList.Add(tripDataPassBack);

                current = nextCity;

            } while (nextCity.CityID != DestinationID);

            return returnList;
        }
    }
}
