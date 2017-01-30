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

        public int UserId { get; set; }

        public int TotalTime { get; set; }

        public DateTime Created { get; set; }
    }
}