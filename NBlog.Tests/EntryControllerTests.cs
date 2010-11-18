using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBlog.Web.Application.Service;
using NBlog.Web.Application.Service.Entity;
using NBlog.Web.Controllers;
using NSubstitute;

namespace NBlog.Tests
{
    /// <summary>
    /// Summary description for EntryControllerTests
    /// </summary>
    [TestClass]
    public class EntryControllerTests
    {
        public EntryControllerTests()
        {
            var a = 100;
            var b = "yeah";
            var str = new List<Object>();


            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

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
        public void ListTest()
        {
            // arrange
            var services = Substitute.For<IServices>();
            services.Entry.GetList().Returns(new List<Entry>(new[] {
                new Entry{ Title = "Entry 1" },
                new Entry{ Title = "Entry 2" },  
                new Entry{ Title = "Entry 3" },  
            }));

            // act
            var controller = new HomeController(services);
            var model = controller.Index().ViewData.Model as HomeController.IndexModel;

            // assert
            services.Entry.Received().GetList();
            Assert.AreEqual(model.Entries.Count(), services.Entry.GetList().Count);
        }
    }
}
