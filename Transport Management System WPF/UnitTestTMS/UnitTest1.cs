// ADMIN FILE HEADER COMMENT: =================================================================================
/**
 *  \file		Admin.cs
 *  \ingroup	TMSTesting
 *  \date		November 20, 2019
 *  \author		8000 Cigarettes - Megan
 *  \brief	    This file contains the admin functionality	  
 *  \see		MainWindow.xaml
 *  \details    This file holds the functionality of the Admin class. The Admin has the ability to view logs as 
 *              specified by time period, view details of specific logs, alter where the log files are stored, 
 *              initiate backups of the TMS database, choose where the TMS db is backed up to, alter the Carrier 
 *              Data Table, the Route Table, and the Rate / Fee Tables.                                       
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
    *   \class		Admin
    *   \brief		This class runs the Admin UI functionality
    *   \details	... static class?  
    *   
    * -------------------------------------------------------------------------------------------------------- */
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
        public void routCalc2()
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

            ReturnList = graphClass.GetTravelData(Contract.ToCityID(contract.origin), Contract.ToCityID(contract.destination), contract.job_Type);

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
                    Route = x
                };
            }

            //TimePass.IncrementDay(ReturnList, truck, trip_Ticket);

        }
    }
}
