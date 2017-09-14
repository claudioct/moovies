using System.Collections.Generic;
using System.Linq;
using Moovies.DataContract;

namespace Moovies.Data
{
    public interface IMooviesBoardRepository
    {
        IQueryable<LeaderboardRecord> GetLeaderboardRecords();

        LeaderboardRecord GetLeaderboardRecord(int leaderbordRecordId);

        List<LeaderboardResultsView> GetLeaderboardView();

        bool AddLeaderboard(LeaderboardRecord newLeaderbordRecord);

        bool AddOmdbMovieRecord(OmdbMovieRecord newOmdbMovieRecord);

        bool Save();
        bool AddLeaderboard(ImdbData imdbData);
    }
}
