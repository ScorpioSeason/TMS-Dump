// ADMIN FILE HEADER COMMENT: =================================================================================
/**
 *  \file		PlannerClassTests.cs
 *  \ingroup	TMSTesting
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Duane
 *  \brief	    This file contains unit tests for the parts of the code related to the planner.
 *  \see	    PlannerClass.cs
 *  \details    These tests will test the basic functionality needed for the planner class.                              
 *
 * =========================================================================================================== */

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transport_Management_System_WPF;

namespace UnitTestTMS
{
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		PlannerClassTests
    *   \brief		This class will run a suite of test for the planner class. 
    *   \details	See the test plan document attached to this milestone.
    *   
    * -------------------------------------------------------------------------------------------------------- */
    [TestClass]
    public class PlannerClassTests
    {
        //Test city 7 - 0, FTL
        [TestMethod]
        public void testReadInCMP()
        {
            LoadCMPintoDatabase.Load();
        }

        [TestMethod]
        public void testHighestnumber()
        {




            PlannerSQL psql = new PlannerSQL();
            psql.Open();
            psql.LoadTheCSV();

            psql.Close();
        }


        //Test city 7 - 0, FTL
        [TestMethod]
        public void GetTravelData1()
        {

            //ARRANGE
            MappingClass graphClass = new MappingClass();
            List<RouteData> ReturnList = new List<RouteData>();

            ReturnList = graphClass.GetTravelData(7, 0, true);

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
        public void GetTravelData2()
        {

            //ARRANGE
            MappingClass graphClass = new MappingClass();
            List<RouteData> ReturnList = new List<RouteData>();

            ReturnList = graphClass.GetTravelData(7, 0, false);

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
        public void GetTravelData3()
        {

            //ARRANGE
            MappingClass graphClass = new MappingClass();
            List<RouteData> ReturnList = new List<RouteData>();

            ReturnList = graphClass.GetTravelData(8, -1, false);

            RouteSumData totalTrip = graphClass.SummerizeTrip(ReturnList);

            //ASSERT
            Assert.AreEqual(totalTrip.DestinationCity, -1);
        }



        //Test city 7 - 0, LTL
        [TestMethod]
        public void routCalc3()
        {
            //ARRANGE
            MappingClass graphClass = new MappingClass();
            List<RouteData> ReturnList = new List<RouteData>();

            ReturnList = graphClass.GetTravelData(3, 4, true); 

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

            ////ARRANGE
            //MappingClass graphClass = new MappingClass();
            //List<RouteData> ReturnList = new List<RouteData>();

            //Contract contract = new Contract
            //{
            //    client_Name = "Wally World",
            //    origin = "Windsor",
            //    destination = "Hamilton",
            //    //job_Type = true
            //};

            //ReturnList = graphClass.GetTravelData(Contract.ToCityID(contract.origin), Contract.ToCityID(contract.destination), contract.job_Type);

            //Truck truck = new Truck
            //{
            //    TruckID = ReturnList[0].CityA,
            //    CurrentCityID = 0,
            //    Is_Reefer = false
            //};

            //Trip_Ticket trip_Ticket = new Trip_Ticket
            //{
            //    TicketID = 32,
            //    Days_Passed = 0,
            //    FTL_or_LTL = true,
            //    Is_Reefer = false,
            //    Size_In_Palette = 3,
            //    TruckID = truck
            //};

            //List<Trip_Ticket_Line> trip_Ticket_Lines = new List<Trip_Ticket_Line>();

            //foreach(RouteData x in ReturnList)
            //{
            //    Trip_Ticket_Line TTL1 = new Trip_Ticket_Line
            //    {
            //        Ticket = trip_Ticket,
            //        Route = x
            //    };
            //}

            ////TimePass.IncrementDay(ReturnList, truck, trip_Ticket);

        }
    }
}
