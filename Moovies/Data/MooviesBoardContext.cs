using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Moovies.Data
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MooviesBoardContext : IdentityDbContext
    {
        public MooviesBoardContext() : base("DefaultConnection")
        {  
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<LeaderboardRecord> LeaderboardRecords { get; set; }

        public DbSet<OmdbMovieRecord> OmdbMovieRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Microsoft.AspNet.Identity.EntityFramework.IdentityRole>()
                .Property(c => c.Name).HasMaxLength(128).IsRequired();

            modelBuilder.Entity<Microsoft.AspNet.Identity.EntityFramework.IdentityUser>().ToTable("AspNetUsers")//I have to declare the table name, otherwise IdentityUser will be created
                .Property(c => c.UserName).HasMaxLength(128).IsRequired();
        }

    }


}