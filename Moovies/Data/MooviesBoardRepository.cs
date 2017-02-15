using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moovies.DataContract;

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

        public bool AddLeaderboard(ImdbData imdbData)
        {
            try
            {
                LeaderboardRecord newRecord = new LeaderboardRecord()
                {
                    Created = DateTime.UtcNow,
                    FavoriteActor = "",
                    FavoriteActress = "",
                    FavoriteGenre = imdbData.FavoriteGenre,
                    TotalAwards = 0,
                    TotalTime = imdbData.TotalTime,
                    UserId = Guid.Empty.ToString(),
                    AverageImdbScore = imdbData.TotalImdbRating,
                    AverageUserScore = imdbData.TotalUserRating
                };
                _ctx.LeaderboardRecords.Add(newRecord);
                _ctx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //TODO log this error
                return false;
            }
        }
    }
}