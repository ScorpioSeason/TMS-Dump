using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport_Management_System_WPF
{
    public struct GraphNode
    {
        public int CityID;
        public string CityName;
        public object East;
        public int EastKM;
        public double EastHour;
        public object West;
        public int WestKM;
        public double WestHour;
    };

    public class GraphClass
    {
        private const int Number_of_Cities = 8;
        public  List<GraphNode> nodes = new List<GraphNode>();

        public GraphClass()
        {
            GraphNode Windsor = new GraphNode { CityName = "Windsor", CityID = 0 };
            GraphNode London = new GraphNode { CityName = "London", CityID = 1 };
            GraphNode Hamilton = new GraphNode { CityName = "Hamilton", CityID = 2 };
            GraphNode Toronto = new GraphNode { CityName = "Toronto", CityID = 3 };
            GraphNode Oshawa = new GraphNode { CityName = "Oshawa", CityID = 4 };
            GraphNode Belleville = new GraphNode { CityName = "Belleville", CityID = 5 };
            GraphNode Kingston = new GraphNode { CityName = "Kingston", CityID = 6 };
            GraphNode Ottawa = new GraphNode { CityName = "Ottawa", CityID = 7 };

            GraphNode[] graphNodes = new GraphNode[Number_of_Cities] { Windsor, London, Hamilton, Toronto, Oshawa, Belleville, Kingston, Ottawa };

            object[] westCityArray = new object[] { null, Windsor, London, Hamilton, Toronto, Oshawa, Belleville, Kingston };
            for (int i = 0; i < Number_of_Cities; i++)
            {
                graphNodes[i].West = westCityArray[i];
            }

            object[] eastCityArray = new object[] { London, Hamilton, Toronto, Oshawa, Belleville, Kingston, Ottawa, null };
            for (int i = 0; i < Number_of_Cities; i++)
            {
                graphNodes[i].East = eastCityArray[i];
            }

            int[] eastKMArray = new int[Number_of_Cities] { 191, 128, 68, 60, 134, 82, 196, -1 };
            for (int i = 0; i < Number_of_Cities; i++)
            {
                graphNodes[i].EastKM = eastKMArray[i];
            }

            int[] westKMArray = new int[Number_of_Cities] { -1, 191, 128, 68, 60, 134, 82, 196 };
            for (int i = 0; i < Number_of_Cities; i++)
            {
                graphNodes[i].WestKM = westKMArray[i];
            }

            double[] eastHourArray = new double[Number_of_Cities] { 2.5, 1.75, 1.25, 1.3, 1.65, 1.2, 2.5, -1 };
            for (int i = 0; i < Number_of_Cities; i++)
            {
                graphNodes[i].EastHour = eastHourArray[i];
            }

            double[] WestHourArray = new double[Number_of_Cities] { -1, 2.5, 1.75, 1.25, 1.3, 1.65, 1.2, 2.5 };
            for (int i = 0; i < Number_of_Cities; i++)
            {
                graphNodes[i].WestHour = WestHourArray[i];
            }

            for (int i = 0; i < Number_of_Cities; i++)
            {
                nodes.Add(graphNodes[i]);
            }
        }

        public struct TripDataPassBack
        {
            public int KM;
            public double hours;
            public int CityA;
            public int CityB;
        }


        public List<TripDataPassBack> getTravelData(int OriginID, int DestinationID, bool FLTorLTL) //ftl is true
        {
            //figure out if we need to travel east or west
            GraphNode current = nodes.Find(x => x.CityID == OriginID);
            GraphNode nextCity;

            List<TripDataPassBack> returnList = new List<TripDataPassBack>();


            do
            {

                TripDataPassBack tripDataPassBack = new TripDataPassBack();

                if (OriginID > DestinationID)
                {
                    //going west
                    nextCity = (GraphNode)current.West;
                    tripDataPassBack.KM = current.WestKM;
                    tripDataPassBack.hours = current.WestHour;
                }
                else
                {
                    //going east
                    nextCity = (GraphNode)current.East;
                    tripDataPassBack.KM = current.EastKM;
                    tripDataPassBack.hours = current.EastHour;
                }

                tripDataPassBack.CityA = current.CityID;
                tripDataPassBack.CityB = nextCity.CityID;

                if (current.CityID == OriginID)
                {
                    tripDataPassBack.hours += 2;
                }

                if (nextCity.CityID == DestinationID)
                {
                    tripDataPassBack.hours += 2;
                }

                if (current.CityID != OriginID && nextCity.CityID != DestinationID)
                {
                    if (FLTorLTL)
                    {
                        tripDataPassBack.hours += 2;
                    }
                }

                current = nextCity;

            } while (nextCity.CityID != DestinationID);

            return returnList;
        }
    }
}
