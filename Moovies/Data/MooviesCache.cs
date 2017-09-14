using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moovies.Data
{
    public static class MooviesCache
    {
        public static List<OmdbMovieRecord> MoviceCache { get; set; }

        public static bool IsEmpty { get { return MoviceCache == null || MoviceCache.Count == 0; } }

        private static bool trying;

        public static void Retrieve()
        {
            if (trying)
            {
                return;
            }
            else
            {
                trying = true;
            }

            var context = new MooviesBoardContext();
            MoviceCache = context.OmdbMovieRecords.ToList();

            trying = false;
        }
    }
}