using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Moovies.Data
{
    public class MooviesBoardMigrationsConfiguration :
        DbMigrationsConfiguration<MooviesBoardContext>
    {

        public MooviesBoardMigrationsConfiguration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MooviesBoardContext context)
        {
            base.Seed(context);

#if DEBUG
            if (context.LeaderboardRecords.Count() == 0)
            {
                var leaderboard = new LeaderboardRecord()
                {
                    Created = DateTime.Now,
                    TotalTime = 60,
                    UserId = Guid.NewGuid().ToString()
                };

                context.LeaderboardRecords.Add(leaderboard);

                var leaderboard2 = new LeaderboardRecord()
                {
                    Created = DateTime.Now,
                    TotalTime = 60,
                    UserId = Guid.NewGuid().ToString()
                };

                context.LeaderboardRecords.Add(leaderboard2);

                var leaderboard3 = new LeaderboardRecord()
                {
                    Created = DateTime.Now,
                    TotalTime = 60,
                    UserId = Guid.NewGuid().ToString()
                };

                context.LeaderboardRecords.Add(leaderboard3);

                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    //LOG
                    var msg = ex.Message;
                }
            }
#endif
        }

    }
}