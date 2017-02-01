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

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                //TODO log this error
                return false;
            }
        }

        public bool AddLeaderboard(LeaderboardRecord newLeaderbordRecord)
        {
            try
            {
                _ctx.LeaderboardRecords.Add(newLeaderbordRecord);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log this error
                return false;
            }
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
            string query = Queries.LeaderboardsView;
            var leaderboardView = _ctx.Database.SqlQuery<LeaderboardResultsView>(query);
            return leaderboardView.ToList();
        }
    }
}