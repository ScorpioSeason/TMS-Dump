using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftQualv1
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

    

    class GraphClass
    {
        private const int Number_of_Cities = 8;
        private List<GraphNode> nodes = new List<GraphNode>();

        public GraphClass()
        {
            GraphNode Windsor = new GraphNode { CityName = "Windsor", CityID = 0};
            GraphNode London = new GraphNode { CityName = "London", CityID = 1 };
            GraphNode Hamilton = new GraphNode { CityName = "Hamilton", CityID = 2 };
            GraphNode Toronto = new GraphNode { CityName = "Toronto", CityID = 3 };
            GraphNode Oshawa = new GraphNode { CityName = "Oshawa", CityID = 4 };
            GraphNode Belleville = new GraphNode { CityName = "Belleville", CityID = 5 };
            GraphNode Kingston = new GraphNode { CityName = "Kingston", CityID = 6 };
            GraphNode Ottawa = new GraphNode { CityName = "Ottawa", CityID = 7 };

            GraphNode[] graphNodes = new GraphNode[Number_of_Cities] { Windsor, London, Hamilton, Toronto, Oshawa, Belleville, Kingston, Ottawa };

            object[] westCityArray = new object[]{null, Windsor, London, Hamilton, Toronto, Oshawa, Belleville, Kingston};
            for (int i = 0; i < Number_of_Cities; i++)
            {
                graphNodes[i].West = westCityArray[i];
            }

            object[] eastCityArray = new object[] {London, Hamilton, Toronto, Oshawa, Belleville, Kingston, Ottawa, null};
            for (int i = 0; i < Number_of_Cities; i++)
            {
                graphNodes[i].East = eastCityArray[i];
            }

            int[] eastKMArray = new int[Number_of_Cities] { 191, 128, 68, 60, 134, 82, 196, -1 };
            for (int i = 0; i < Number_of_Cities; i++)
            {
                graphNodes[i].EastKM = eastKMArray[i];
            }

            int[] westKMArray = new int[Number_of_Cities] { -1, 191, 128, 68, 60, 134, 82, 196};
            for (int i = 0; i < Number_of_Cities; i++)
            {
                graphNodes[i].WestKM = westKMArray[i];
            }

            double[] eastHourArray = new double[Number_of_Cities] {2.5, 1.75, 1.25, 1.3, 1.65, 1.2, 2.5, -1};
            for (int i = 0; i < Number_of_Cities; i++)
            {
                graphNodes[i].EastHour = eastHourArray[i];
            }

            double[]WestHourArray = new double[Number_of_Cities] {-1, 2.5, 1.75, 1.25, 1.3, 1.65, 1.2, 2.5};
            for (int i = 0; i < Number_of_Cities; i++)
            {
                graphNodes[i].WestHour = WestHourArray[i];
            }

            for(int i = 0; i < Number_of_Cities; i++)
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


        public void getTravelData(int OriginID, int DestinationID, bool FLTorLTL)
        {
            //figure out if we need to travel east or west
            GraphNode current = nodes.Find(x => x.CityID == OriginID);
            GraphNode nextCity;



            if (OriginID > DestinationID)
            {
                //going west
                nextCity = (GraphNode)current.West;

                TripDataPassBack tripDataPassBack = new TripDataPassBack();
                tripDataPassBack.CityA = current


            }
            else
            {
                //going east
            }






           
        }






    }
}
