using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moovies.DataContract;

namespace Moovies.Data
{
    public interface IMooviesBoardRepository
    {
        IQueryable<LeaderboardRecord> GetLeaderboardRecords();

        LeaderboardRecord GetLeaderboardRecord(int leaderbordRecordId);

        List<LeaderboardResultsView> GetLeaderboardView();

        bool AddLeaderboard(LeaderboardRecord newLeaderbordRecord);

        bool Save();
        bool AddLeaderboard(ImdbData imdbData);
    }
}
