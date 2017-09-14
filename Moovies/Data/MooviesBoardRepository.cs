using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Moovies.DataContract;
using System.Threading.Tasks;

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

        public bool AddOmdbMovieRecord(OmdbMovieRecord newOmdbMovieRecord)
        {
            try
            {
                _ctx.OmdbMovieRecords.Add(newOmdbMovieRecord);
                return true;
            }
            catch (Exception ex)
            {
                //TODO log this error
                return false;
            }
        }

        public OmdbMovieRecord GetOmdbMovieRecord(string imdbId)
        {
            if (MooviesCache.IsEmpty)
            {
                Task.Run(() => { MooviesCache.Retrieve(); });                
                return _ctx.OmdbMovieRecords.Where(l => l.ImdbID == imdbId).FirstOrDefault();                
            }
            else
            {
                return MooviesCache.MoviceCache.Where(l => l.ImdbID == imdbId).FirstOrDefault();
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
                    FavoriteGenre = imdbData.FavoriteGenre.Key,
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