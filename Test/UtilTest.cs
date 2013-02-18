using System;
using System.Drawing;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Contour;
using Emgu.CV.Structure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    /// <summary>
    /// Summary description for UtilTest
    /// </summary>
    [TestClass]
    public class UtilTest
    {
        public UtilTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;
        private const double Epsilon = 0.000000000000001;

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

        [TestMethod]
        public void TestMethod1()
        {
            var line = new LineSegment2D(new Point(1, 1), new Point(3, 3));
            Assert.IsTrue(line.Distance(new Point(1, 0)) - 1/Math.Sqrt(2) < Epsilon);
            Assert.IsTrue(line.Distance(new Point(0, 0)) < Epsilon);
            Assert.IsTrue(line.Distance(new Point(0, 4)) - 2*Math.Sqrt(2) < Epsilon);
        }
    }
}
