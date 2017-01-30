using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moovies.Data
{
    public class MooviesBoardRepository : IMooviesBoardRepository
    {
        MooviesBoardContext _ctx;
        public MooviesBoardRepository(MooviesBoardContext ctx)
        {
            _ctx = ctx;
        }

        public LeaderboardRecord GetLeaderboardRecord(int leaderbordRecordId)
        {
            return _ctx.LeaderboardRecords.Where(l => l.Id == leaderbordRecordId).FirstOrDefault();
        }

        public IQueryable<LeaderboardRecord> GetLeaderboardRecords()
        {
            return _ctx.LeaderboardRecords;
        }

        public List<LeaderboardResultsView> GetLeaderboardView()
        {
            return null;
            //var leaderboardView = _ctx.LeaderboardRecords.SqlQuery("Select * from Student").ToList<LeaderboardResultsView>();
        }
    }
}