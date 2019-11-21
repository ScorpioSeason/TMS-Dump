using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transport_Management_System_WPF;


namespace UnitTestTMS
{
    [TestClass]
    public class UnitTest1
    {

        
        //Test city 7 - 0, FTL
        [TestMethod]
        public void routCalc1()
        {

            //ARRANGE
            MappingClass graphClass = new MappingClass();
            List<RouteData> ReturnList = new List<RouteData>();

            ReturnList = graphClass.getTravelData(7, 0, true);

            RouteSumData totalTrip = graphClass.SummerizeTrip(ReturnList);

            //ASSERT
            Assert.AreEqual(totalTrip.DestinationCity, 0);
            Assert.AreEqual(totalTrip.OriginCity, 7);
            Assert.AreEqual((int)(totalTrip.totalDriveTime * 100), 1214);
            Assert.AreEqual((int)(totalTrip.totalTripTime * 100), 2815);
            Assert.AreEqual(totalTrip.totalKM, 859);
        }

        //Test city 7 - 0, LTL
        [TestMethod]
        public void routCalc2()
        {

            //ARRANGE
            MappingClass graphClass = new MappingClass();
            List<RouteData> ReturnList = new List<RouteData>();

            ReturnList = graphClass.getTravelData(7, 0, false);

            RouteSumData totalTrip = graphClass.SummerizeTrip(ReturnList);

            //ASSERT
            Assert.AreEqual(totalTrip.DestinationCity, 0);
            Assert.AreEqual(totalTrip.OriginCity, 7);
            Assert.AreEqual((int)(totalTrip.totalDriveTime * 100), 1214);
            Assert.AreEqual((int)(totalTrip.totalTripTime * 100), 1614);
            Assert.AreEqual(totalTrip.totalKM, 859);
        }

        //Test city 7 - 0, LTL
        [TestMethod]
        public void routCalc3()
        {

            //ARRANGE
            MappingClass graphClass = new MappingClass();
            List<RouteData> ReturnList = new List<RouteData>();

            ReturnList = graphClass.getTravelData(3, 4, true); 

            RouteSumData totalTrip = graphClass.SummerizeTrip(ReturnList);

            //ASSERT
            Assert.AreEqual(totalTrip.OriginCity, 3);
            Assert.AreEqual(totalTrip.DestinationCity, 4);
            Assert.AreEqual((int)(totalTrip.totalDriveTime * 100), 130);
            Assert.AreEqual((int)(totalTrip.totalTripTime * 100), 530);
            Assert.AreEqual(totalTrip.totalKM, 60);
        }


        [TestMethod]
        public void routCalc4()
        {

            //ARRANGE
            MappingClass graphClass = new MappingClass();
            List<RouteData> ReturnList = new List<RouteData>();

            ReturnList = graphClass.getTravelData(0, 3, true);



        }


    }
}
