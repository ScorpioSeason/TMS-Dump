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
        public void routCalc1()
        {

            GraphClass graphClass = new GraphClass();

            List<RouteData> RD = new List<RouteData>();

            RD = graphClass.getTravelData(0, 3, true);


            int i = 0;
        
        }
    }
}
