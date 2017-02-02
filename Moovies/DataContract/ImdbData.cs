using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moovies.DataContract
{
    public class ImdbData
    {
        public string FavoriteDirector { get; internal set; }
        public string FavoriteGenre { get; internal set; }
        public decimal TotalImdbRating { get; internal set; }
        public int TotalTime { get; internal set; }
        public decimal TotalUserRating { get; internal set; }
    }
}