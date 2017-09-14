using Moovies.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moovies.DataContract;

namespace Moovies.Tests.Fakes
{
    class FakeMooviesBoardRepository : IMooviesBoardRepository
    {
        public bool AddLeaderboard(ImdbData imdbData)
        {
            return true;
        }

        public bool AddLeaderboard(LeaderboardRecord newLeaderbordRecord)
        {
            return true;
        }

        public bool AddOmdbMovieRecord(OmdbMovieRecord newOmdbMovieRecord)
        {
            throw new NotImplementedException();
        }

        public LeaderboardRecord GetLeaderboardRecord(int leaderbordRecordId)
        {
            return new LeaderboardRecord()
            {
                FavoriteGenre = "drama",
                Created = DateTime.Now,
                FavoriteActor = "John",
                FavoriteActress = "Holly Mary",
                TotalAwards = 123456,
                TotalTime = 12345,
                UserId = "123456"
            };
        }

        public IQueryable<LeaderboardRecord> GetLeaderboardRecords()
        {
            return new List<LeaderboardRecord>()
            {
                 new LeaderboardRecord()
                 {
                    FavoriteGenre = "drama",
                    Created = DateTime.Now,
                    FavoriteActor = "John",
                    FavoriteActress = "Holly Mary",
                    TotalAwards = 123456,
                    TotalTime = 12345,
                    UserId = "123456"
                 },
                 new LeaderboardRecord()
                 {
                    FavoriteGenre = "drama",
                    Created = DateTime.Now,
                    FavoriteActor = "John",
                    FavoriteActress = "Holly Mary",
                    TotalAwards = 123456,
                    TotalTime = 12345,
                    UserId = "123456"
                }
            }.AsQueryable();
        }

        public List<LeaderboardResultsView> GetLeaderboardView()
        {
            return new List<LeaderboardResultsView>()
            {
                 new LeaderboardResultsView()
                 {
                    FavoriteGenre = "drama",
                    Created = DateTime.Now,
                    FavoriteActor = "John",
                    FavoriteActress = "Holly Mary",
                    TotalAwards = 123456,
                    TotalTime = 12345,
                 },
                 new LeaderboardResultsView()
                 {
                    FavoriteGenre = "drama",
                    Created = DateTime.Now,
                    FavoriteActor = "John",
                    FavoriteActress = "Holly Mary",
                    TotalAwards = 123456,
                    TotalTime = 12345,
                }
            };
        }

        public bool Save()
        {
            return true;
        }
    }
}
