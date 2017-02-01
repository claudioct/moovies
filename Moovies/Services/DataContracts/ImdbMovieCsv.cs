using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moovies.Services.DataContracts
{
    public class ImdbMovieCsv
    {
        public string Directors { get; internal set; }
        public string[] Genres { get; internal set; }
        public string Id { get; internal set; }
        public decimal ImdbRating { get; internal set; }
        public int Runtime { get; internal set; }
        public string Title { get; internal set; }
        public string TitleType { get; internal set; }
        public int UserRating { get; internal set; }
        public int Year { get; internal set; }
    }
}