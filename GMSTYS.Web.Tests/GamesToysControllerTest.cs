using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Web.Http;
using System.Collections.Generic;
using GMSTYS.Web.Controllers;
using GMSTYS.Web.Models;
using GMSTYS.Model;
using GMSTYS.Data;
using GMSTYS.Repositories;
using System.Net.Http;

namespace GMSTYS.Web.Tests
{
    [TestClass]
    public class GamesToysControllerTest
    {
        [TestMethod]
        public void GetAllGames_Test()
        {
            var controller = new APIGamesToysController();
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            
            var response = controller.Get();
            Assert.IsNotNull(response);

        }
      

        private IEnumerable<GamesModel> GetTestGames()
        {
            var testGames = new List<GamesModel>();
            testGames.Add(new GamesModel { Id = 1, Name = "Demo1", Description = "Description 1", AgeRestriction = 4, Company = "Company 1", Price = 1 });
            testGames.Add(new GamesModel { Id = 2, Name = "Demo2", Description = "Description 2", AgeRestriction = 5, Company = "Company 2", Price = 3.75M });
            testGames.Add(new GamesModel { Id = 3, Name = "Demo3", Description = "Description 3", AgeRestriction = 6, Company = "Company 3", Price = 16.99M });
            testGames.Add(new GamesModel { Id = 4, Name = "Demo4", Description = "Description 4", AgeRestriction = 7, Company = "Company 4", Price = 11.00M });

            return testGames;
        }
        private GamesModel GetMockTestGame()
        {
           return new GamesModel { Id = 1, Name = "Demo1", Description = "Description 1", Price = 1 };


        }
    }
}
