using CsvHelper;
using Moovies.DataContract;
using Moovies.Services.DataContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Moovies.Helper;

namespace Moovies.Services
{
    public class ImdbDataService : IImdbDataService
    {
        public ImdbData Retrieve(string csv)
        {
            try
            {
                ImdbData imdbData = new ImdbData();
                List<ImdbMovieCsv> imdbMovieCsvCollection = GetRecords(csv);
                int totalUserRating = 0;
                decimal totalImdbRating = 0M;
                int totalTime = 0;

                Dictionary<string, int> favoriteDirector = new Dictionary<string, int>();
                Dictionary<string, int> favoriteGenre = new Dictionary<string, int>();               

                foreach (var movie in imdbMovieCsvCollection)
                {
                    totalUserRating = totalUserRating + movie.UserRating;
                    totalImdbRating = totalImdbRating + movie.ImdbRating;
                    totalTime = totalTime + movie.Runtime;
                    favoriteDirector.AddOrUpdateCount(movie.Title);
                    
                    foreach (var genre in movie.Genres)
                    {
                        favoriteGenre.AddOrUpdateCount(genre);
                    }
                }

                imdbData.TotalTime = totalTime;
                imdbData.TotalImdbRating = totalImdbRating;
                imdbData.TotalUserRating = totalUserRating;
                imdbData.FavoriteDirector = "tommy";
                imdbData.FavoriteGenre = "war";

                return imdbData;
            }
            catch (Exception ex)
            {
                // Add logging
                return null;
            }
        }

        public List<ImdbMovieCsv> GetRecords(string stringCsv)
        {
            List<ImdbMovieCsv> imdbMovieCsvCollection = new List<ImdbMovieCsv>();
            using (TextReader reader = new StringReader(stringCsv))
            {
                var csv = new CsvReader(reader);
                
                while (csv.Read())
                {
                    ImdbMovieCsv imdbMovieCsv = new ImdbMovieCsv();

                    //ImdbMovieCsv.Id = csv.GetField<string>(0);
                    imdbMovieCsv.Id = csv.GetField<string>(1);
                    imdbMovieCsv.Title = csv.GetField<string>(5);
                    imdbMovieCsv.TitleType = csv.GetField<string>(6);
                    imdbMovieCsv.Directors = csv.GetField<string>(7);
                    imdbMovieCsv.UserRating = csv.GetField<int>(8);
                    imdbMovieCsv.ImdbRating = Convert.ToDecimal(csv.GetField<string>(9))/10;
                    imdbMovieCsv.Runtime = csv.GetField<int>(10);
                    imdbMovieCsv.Year = csv.GetField<int>(11);
                    var genres = csv.GetField<string>(12).Split(',');
                    for (int i=0; i<genres.Length; i++)
                    {
                        genres[i] = genres[i].TrimEnd().TrimStart();
                    }

                    imdbMovieCsv.Genres = genres;
                    imdbMovieCsvCollection.Add(imdbMovieCsv);
                }

                return imdbMovieCsvCollection;
            }
        }
    }
}