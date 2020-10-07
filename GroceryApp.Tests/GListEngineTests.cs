﻿using _361Example.Engines;
using _361Example.Models;
using GroceryApp.Tests.MockedAccessors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryApp.Tests
{
    [TestClass]
    public class GListEngineTests
    {

        private readonly IGListEngine gListEngine;
        private readonly MockedGListAccessor mockedGListAccessor;

        public GListEngineTests()
        {
            mockedGListAccessor = new MockedGListAccessor();
            gListEngine = new GListEngine(mockedGListAccessor);
        }


        [TestMethod]
        public void GListEngine_SortLists()
        {
            //Arrange: Seeds the Mocked Accessor's list of GLists and creates an expected list of GLists
            SeedGLists();
            var expected = new List<GList>
            {
                new GList
                {
                    Id = 3,
                    ListName = "MyList"
                },
                 new GList
                {
                    Id = 1,
                    ListName = "Sunday Groceries"
                },
                 new GList
                {
                    Id = 2,
                    ListName = "Wednesday Groceries"
                }
            };

            //Act: Calls the GListEngine SortLists() method and converts the return value to a list
            var result = gListEngine.SortLists().ToList();


            //Assert: Checks whether the expected and result lists are ordered the same
            Assert.AreEqual(expected.ElementAt(0).ListName, result.ElementAt(0).ListName, "The GList was sorted incorrectly.");
            Assert.AreEqual(expected.ElementAt(1).ListName, result.ElementAt(1).ListName, "The GList was sorted incorrectly.");
            Assert.AreEqual(expected.ElementAt(2).ListName, result.ElementAt(2).ListName, "The GList was sorted incorrectly.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GListEngine_SortLists_ExpectArgumentNullException()
        {
            //Arrange: Seed the Mocked Accessor's list of GLists with a null-named list
            mockedGListAccessor.SetState(new List<GList>
            {
                new GList
                {
                    Id = 1,
                    ListName = null
                }
            });

            //Act: Calls the GListEngine SortLists() method
            var result = gListEngine.SortLists();

            //Assert is handled by the ExpectedException attribute on the test method
        }


        public void SeedGLists()
        {
            mockedGListAccessor.SetState(new List<GList>
            {
                new GList
                {
                    Id = 1,
                    ListName = "Sunday Groceries"
                },
                new GList
                {
                    Id = 2,
                    ListName = "Wednesday Groceries"
                },
                new GList
                {
                    Id = 3,
                    ListName = "MyList"
                }
            });
        }

        [TestMethod]
        public void GListEngine_GetList()
        {
            // Arrange: Seeds the Mocked Accessor's list of GLists and retrives the expected list
            SeedGLists();
            var expected = new GList { Id = 2, ListName = "Wednesday Groceries" };

            // Act: Calls the GList Engine GetList() method
            var result = gListEngine.GetList(2);

            // Assert: Checks whether expected and result list are the same
            Assert.AreEqual(expected.ListName, result.ListName, result.Id + " was returned. " + expected.Id + " was expected.");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GListEngine_GetList_EmptyList()
        {
            //Arrange: Seed the Mocked Accessor's list of GLists
            mockedGListAccessor.SetState(new List<GList> { null });

            // Act: Calls the GListEngine GetList() method
            var result = gListEngine.GetList(3);

            // Assert is handled by the ExpectedException attribute on the test method

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void GListEngine_GetList_ElementDoesntExist()
        {
            // Arrange: Seeds the Mocked Accessor's list of GLists
            SeedGLists();

            // Act: Calls the GList Engine GetList() method on a GList that doesn't exist
            var result = gListEngine.GetList(5);

            // Assert checks if the reseult is null
            Assert.AreEqual(null, result.ListName, result.Id + " was returned. ");

        }

        // Implemeted by Noah
        [TestMethod]
        public void GListEngine_UpdateList()
        {
            //Arrange
            SeedGLists();
            var expected = new GList()
            {
                Id = 2,
                ListName = "Updated"
            };

            //Act: mockedGListAccessor.gLists isn't accessible outside of the class so a simple method from MockedGListAccessor is employed
            //to grab the GLists after they're updated.
            gListEngine.UpdateList(2, expected);
            List<GList> results = mockedGListAccessor.GetAllGLists().ToList();

            //Assert: Checks if the Name was successfully updated
            Assert.AreEqual(expected.ListName, results.ElementAt(2).ListName, "The GList wasn't updated correctly.");
        }
    }



}
