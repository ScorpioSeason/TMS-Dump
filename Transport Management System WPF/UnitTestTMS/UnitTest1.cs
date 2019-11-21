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

        struct TestingContract
        {
            public string client_Name { get; set; }
            public bool job_Type { get; set; }
            public string quantity { get; set; }
            public int origin { get; set; }
            public int destination { get; set; }
            public string van_Type { get; set; }
        };


        [TestMethod]
        public void routCalc4()
        {

           

            //ARRANGE
            MappingClass graphClass = new MappingClass();
            List<RouteData> ReturnList = new List<RouteData>();

            Contract contract = new Contract
            {
                client_Name = "Wally World",
                origin = "Windsor",
                destination = "Hamilton",
                job_Type = true
            };

            ReturnList = graphClass.getTravelData(Contract.ToCityID(contract.origin), Contract.ToCityID(contract.destination), contract.job_Type);

            Truck truck = new Truck
            {
                TruckID = ReturnList[0].CityA,
                CurrentCityID = 0,
                Is_Reefer = false
            };

            Trip_Ticket trip_Ticket = new Trip_Ticket
            {
                TicketID = 32,
                Days_Passed = 0,
                FTL_or_LTL = true,
                Is_Reefer = false,
                Size_In_Palette = 3,
                TruckID = truck
            };

            List<Trip_Ticket_Line> trip_Ticket_Lines = new List<Trip_Ticket_Line>();

            foreach(RouteData x in ReturnList)
            {
                Trip_Ticket_Line TTL1 = new Trip_Ticket_Line
                {
                    Ticket = trip_Ticket,
                    Order = contract
                };
            }

            TimePass.incrementDay(ReturnList, truck, trip_Ticket);

            



        }
    }
}
