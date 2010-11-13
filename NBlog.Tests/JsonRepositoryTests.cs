﻿using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBlog.Web.Application;
using NBlog.Web.Application.Service.Entity;
using NBlog.Web.Application.Storage.Json;

namespace NBlog.Tests
{
    /// <summary>
    /// Summary description for JsonRepositoryTests
    /// </summary>
    [TestClass]
    public class JsonRepositoryTests
    {
        public JsonRepositoryTests()
        {
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
        public void Single_Should_Return_Correct_Entity_By_Key()
        {
            // arrange
            const string title = "Top 10 C# Tips";
            var entry = new Entry { Title = title };
            var jsonRepository = new JsonRepository(TestContext.TestDir);

            // act
            jsonRepository.Save(entry);
            var retrievedEntry = jsonRepository.Single<Entry>("top-10-c-tips");

            // assert
            Assert.AreEqual(retrievedEntry.Title, title);
        }


        [TestMethod]
        public void List_Should_Return_All_Entities()
        {
            // arrange
            var jsonRepository = new JsonRepository(TestContext.TestDir);
            jsonRepository.DeleteAll<Entry>();
            jsonRepository.Save(new Entry { Title = "Entry 1" });
            jsonRepository.Save(new Entry { Title = "Entry 2" });
            jsonRepository.Save(new Entry { Title = "Entry 3" });

            // act
            var all = jsonRepository.All<Entry>();

            // assert
            Assert.IsTrue(all.Count() == 3);
        }


        [TestMethod]
        public void ToSlugUrl_Should_Build_Correct_Slugs()
        {
            // arrange
            var slugs = new Dictionary<string, string>
            {
                {"myentry", "myentry"},
                {"my-entry", "my-entry"},
                {"MyEntry", "myentry"},
                {"my entry", "my-entry"},
                {" my  entry ", "my-entry"},
                {"my<script></script>entry", "my-script-script-entry"},
                {">my.'& entry*?", "my-entry"}
            };

            // act
            foreach (var slug in slugs)
            {
                // assert
                Assert.AreEqual(slug.Key.ToUrlSlug(), slug.Value);
            }
        }
    }
}
