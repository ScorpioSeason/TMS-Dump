using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transport_Management_System_WPF;




namespace UnitTestTMS
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            

            GraphClass graphClass = new GraphClass();
            List<Transport_Management_System_WPF.GraphClass.TripDataPassBack> AH;// = new List<Transport_Management_System_WPF.GraphClass.TripDataPassBack>();

            AH = graphClass.getTravelData(2, 4, true);

            int i = 3;

        }
    }
}
