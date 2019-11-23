// UNIT TEST2 FILE HEADER COMMENT: =================================================================================
/**
 *  \file		UnitTest2.cs
 *  \ingroup	TMSTesting
 *  \date		November 21, 2019
 *  \author		8000 Cigarettes - Megan
 *  \brief	    This file tests the Admin functionality  
 *  \see		UnitTest1.cs
 *  \details    This file tests the functionality of the Admin-related classes.                                      
 *
 * =========================================================================================================== */

using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMSwPages;

namespace UnitTestTMS
{
    // CLASS HEADER COMMENT -----------------------------------------------------------------------------------
    /**   
    *   \class		UnitTest2
    *   \brief		This class tests the Admin functionality
    *   \details	Currently tested files include: AdminPage.xaml.cs, ViewLogDetails.xaml.cs, AdminClasses.cs
    *   
    * -------------------------------------------------------------------------------------------------------- */
    [TestClass]
    public class UnitTest2
    {
        public UnitTest2()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        // =====================================================================================================

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			TestAdminClasses1
        *	\brief		
        *	\see		AdminClasses.cs
        *	\return		void
        *
        * ---------------------------------------------------------------------------------------------------- */
        [TestMethod]
        public void TestAdminClasses1()
        {
            // AdminClasses: TMSLog /TMSLogger 
            // TMSLogger(ReadExistingLogFile, AppendLogFile)

            //ARRANGE ------------------------------------------
            TMSLogger.LoggerPath = "C:\Users\School User";                     /// Stores location of the log file
            TMSLogger.logs.Clear();

            List<TMSLog> testList = new List<TMSLog>(); 

            /// Test incorrect input string
            TMSLog testLog1 = new TMSLog("");
            /// Test correct input with 6 '|'
            TMSLog testLog2 = new TMSLog("|2019-11-22 5:37:37 PM|C:\Users\School User\Code\TMS-Dump\Transport Management System WPF\TMSwPages\bin\Debug/AdminClasses.cs|TMSLogger|ReadExistingLogFile|Exception|Could not find file 'C:\Users\School User\Code\TMS-Dump\Transport Management System WPF\TMSwPages\bin\Debug\TMSLogger.txt'.|");
            /// Test correct input with 7 '|'
            TMSLog testLog3 = new TMSLog("|AdminClasses.cs|TMSLog|Constructor|LogParseError|The log message did not enter as the correct string format|");

            testList.Add(testLog1);
            testList.Add(testLog2);
            testList.Add(testLog3);

            Assert.AreEqual(testLog1.BSV, "|" + testLog1.logTime.ToString() + "|AdminClasses.cs|TMSLog|Constructor|LogParseError|The log message did not enter as the correct string format|");
            Assert.AreEqual(testLog2.BSV, "|2019-11-22 5:37:37 PM|C:\Users\School User\Code\TMS-Dump\Transport Management System WPF\TMSwPages\bin\Debug/AdminClasses.cs|TMSLogger|ReadExistingLogFile|Exception|Could not find file 'C:\Users\School User\Code\TMS-Dump\Transport Management System WPF\TMSwPages\bin\Debug\TMSLogger.txt'.|");
            Assert.AreEqual(testLog3.BSV, "");

            //foreach (TMSLog l in TMSLogger.logs)
            //{
            //    Console.WriteLine(l.BSV);
            //}

            //ACT ----------------------------------------------
            /// Add a new log with a blank string
            TMSLogger.LogIt("");
            /// Add a new log with a 5 piece input
            TMSLogger.LogIt("|2019-11-22 5:37:37 PM|C:\Users\School User\Code\TMS-Dump\Transport Management System WPF\TMSwPages\bin\Debug/AdminClasses.cs|TMSLogger|ReadExistingLogFile|Exception|Could not find file 'C:\Users\School User\Code\TMS-Dump\Transport Management System WPF\TMSwPages\bin\Debug\TMSLogger.txt'.|");
            /// Add a new log with a 6 piece input
            TMSLogger.LogIt("|AdminClasses.cs|TMSLog|Constructor|LogParseError|The log message did not enter as the correct string format|");

            //foreach (TMSLog l in TMSLogger.logs)
            //{
            //    Console.WriteLine(l.BSV); 
            //}

            /// Add a new log with a blank string
            TMSLogger.NewLog("");
            /// Add a new log with a 5 piece input
            TMSLogger.NewLog("|2019-11-22 5:37:37 PM|C:\Users\School User\Code\TMS-Dump\Transport Management System WPF\TMSwPages\bin\Debug/AdminClasses.cs|TMSLogger|ReadExistingLogFile|Exception|Could not find file 'C:\Users\School User\Code\TMS-Dump\Transport Management System WPF\TMSwPages\bin\Debug\TMSLogger.txt'.|");
            /// Add a new log with a 6 piece input
            TMSLogger.NewLog("|AdminClasses.cs|TMSLog|Constructor|LogParseError|The log message did not enter as the correct string format|");

            //ASSERT
            //Assert.AreEqual(totalTrip.DestinationCity, 0);
            //Assert.AreEqual(totalTrip.OriginCity, 7);
            //Assert.AreEqual((int)(totalTrip.totalDriveTime * 100), 1214);
            //Assert.AreEqual((int)(totalTrip.totalTripTime * 100), 1614);
            //Assert.AreEqual(totalTrip.totalKM, 859);
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			TestAdminClasses1
        *	\brief		
        *	\see		AdminClasses.cs
        *	\return		void
        *
        * ---------------------------------------------------------------------------------------------------- */
        [TestMethod]
        public void TestAdminClasses2()
        {

            // AdminClasses: TMSLog /TMSLogger 
            // TMSLog (constructor), TMSLogger(LogIt, NewLog, ReadExistingLogFile, AppendLogFile)

            //ARRANGE
            //MappingClass graphClass = new MappingClass();
            //List<RouteData> ReturnList = new List<RouteData>();

            //ACT
            //ReturnList = graphClass.getTravelData(7, 0, false);
            //RouteSumData totalTrip = graphClass.SummerizeTrip(ReturnList);

            //ASSERT
            //Assert.AreEqual(totalTrip.DestinationCity, 0);
            //Assert.AreEqual(totalTrip.OriginCity, 7);
            //Assert.AreEqual((int)(totalTrip.totalDriveTime * 100), 1214);
            //Assert.AreEqual((int)(totalTrip.totalTripTime * 100), 1614);
            //Assert.AreEqual(totalTrip.totalKM, 859);
        }

        // METHOD HEADER COMMENT -------------------------------------------------------------------------------
        /**
        *	\fn			TestAdminClasses1
        *	\brief		
        *	\see		AdminClasses.cs
        *	\return		void
        *
        * ---------------------------------------------------------------------------------------------------- */
        [TestMethod]
        public void TestAdminClasses3()
        {
            // AdminClasses: TMSLog /TMSLogger 
            // TMSLog (constructor), TMSLogger(LogIt, NewLog, ReadExistingLogFile, AppendLogFile)

            //ARRANGE
            //MappingClass graphClass = new MappingClass();
            //List<RouteData> ReturnList = new List<RouteData>();

            //ACT
            //ReturnList = graphClass.getTravelData(7, 0, false);
            //RouteSumData totalTrip = graphClass.SummerizeTrip(ReturnList);

            //ASSERT
            //Assert.AreEqual(totalTrip.DestinationCity, 0);
            //Assert.AreEqual(totalTrip.OriginCity, 7);
            //Assert.AreEqual((int)(totalTrip.totalDriveTime * 100), 1214);
            //Assert.AreEqual((int)(totalTrip.totalTripTime * 100), 1614);
            //Assert.AreEqual(totalTrip.totalKM, 859);
        }

    }
}

