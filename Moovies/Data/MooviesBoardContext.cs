using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Moovies.Data
{
    public class MooviesBoardContext : DbContext
    {
        public MooviesBoardContext() : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;

            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<MooviesBoardContext, MooviesBoardMigrationsConfiguration>()
                );
        }

        public DbSet<LeaderboardRecord> LeaderboardRecords { get; set; }
        
    }
}