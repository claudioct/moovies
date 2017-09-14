using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moovies.Controllers;
using Moovies.Tests.Fakes;
using System.Linq;
using Moovies.Data;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Moovies.Tests.Controllers
{
    [TestClass]
    public class LeaderboardsControllerTest
    {
        private LeaderboardsController _ctrl;

        [TestInitialize]
        public void Init()
        {
            _ctrl = new LeaderboardsController(new FakeMooviesBoardRepository()); ;
        }



        [TestMethod]
        public void LeaderboardsController_Get()
        {
            _ctrl = new LeaderboardsController(new FakeMooviesBoardRepository());

            var results = new List<LeaderboardResultsView>();// _ctrl.Get();
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Count() > 0);
            Assert.IsNotNull(results.First());
            Assert.IsNotNull(results.First().FavoriteActor);

        }

        public void TopicsController_Post()
        {
            var newLeaderboardRecord = new LeaderboardRecord()
            {
                FavoriteGenre = "drama",
                Created = DateTime.Now,
                FavoriteActor = "John",
                FavoriteActress = "Holly Mary",
                TotalAwards = 123456,
                TotalTime = 12345,
                UserId = "123456"
            };

            var result =_ctrl.Post(newLeaderboardRecord);
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

            var json = result.Content.ReadAsStringAsync().Result;
            var record = JsonConvert.DeserializeObject<LeaderboardRecord>(json);

            Assert.IsNotNull(record);
            Assert.IsTrue(record.Id > 0);
            Assert.IsTrue(record.Created > DateTime.MinValue);
        }
    }
}
