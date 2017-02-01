using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Moovies.Data
{
    public class LeaderboardRecord
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int TotalTime { get; set; }

        public int TotalAwards { get; set; }

        public string FavoriteActress { get; set; }

        public string FavoriteActor { get; set; }

        public string FavoriteGenre { get; set; }

        public DateTime Created { get; set; }
    }
}