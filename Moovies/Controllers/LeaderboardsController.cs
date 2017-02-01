using Moovies.Data;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Moovies.Controllers
{
    public class LeaderboardsController : ApiController
    {
        private IMooviesBoardRepository _repo;

        public LeaderboardsController(IMooviesBoardRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<LeaderboardResultsView> Get()
        {
            var leaderboards = _repo.GetLeaderboardView();
            return leaderboards;
        }

        public HttpResponseMessage Post([FromBody]LeaderboardRecord newLeaderboardRecord)
        {
            if (newLeaderboardRecord.Created == default(DateTime))
            {
                newLeaderboardRecord.Created = DateTime.UtcNow;
            }

            if (_repo.AddLeaderboard(newLeaderboardRecord) &&
                _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created, newLeaderboardRecord);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
    }
}
