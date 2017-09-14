using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moovies.DataContract
{
    public class ImdbData
    {
        public KeyValuePair<string, int> FavoriteActorActress { get; internal set; }
        public KeyValuePair<string, int> FavoriteCountry { get; internal set; }
        public KeyValuePair<string, int> FavoriteDirector { get; internal set; }
        public KeyValuePair<string, int> FavoriteGenre { get; internal set; }
        public KeyValuePair<string, int> FavoriteLanguage { get; internal set; }
        public KeyValuePair<string, int> FavoriteWriter { get; internal set; }
        public decimal TotalImdbRating { get; internal set; }
        public int TotalTime { get; internal set; }
        public decimal TotalUserRating { get; internal set; }
    }
}