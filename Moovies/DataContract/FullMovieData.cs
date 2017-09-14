using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moovies.DataContract
{
    public class FullMovieData
    {
        public string Directors { get; set; }
        public string[] Genres { get; set; }
        public string Id { get; set; }
        public decimal ImdbRating { get; set; }
        public int Runtime { get; set; }
        public string Title { get; set; }
        public string TitleType { get; set; }
        public int UserRating { get; set; }
        public int Year { get; set; }
        public string Rated { get; set; }
        public string Released { get; set; }        
        public string[] Director { get; set; }
        public string[] Writer { get; set; }
        public string[] Actors { get; set; }
        public string Plot { get; set; }
        public string Language { get; set; }
        public string[] Country { get; set; }
        public string Awards { get; set; }
        public string Poster { get; set; }
        public string Metascore { get; set; }
        public string ImdbVotes { get; set; }
        public string ImdbID { get; set; }
        public string Type { get; set; }
        
    }
}