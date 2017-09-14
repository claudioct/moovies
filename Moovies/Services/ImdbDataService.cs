using CsvHelper;
using Moovies.DataContract;
using Moovies.Services.DataContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Moovies.Helper;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using RestSharp;
using Moovies.Data;
using System.Text.RegularExpressions;
using System.Threading;

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

                ConcurrentBag<FullMovieData> moviesCollection = new ConcurrentBag<FullMovieData>();


                Parallel.ForEach(new ConcurrentBag<ImdbMovieCsv>(imdbMovieCsvCollection), new ParallelOptions { MaxDegreeOfParallelism = 500 }, m =>
                {
                    

                    var ctx = new Moovies.Data.MooviesBoardContext();
                    var repository = new Moovies.Data.MooviesBoardRepository(ctx);
                    var omdbRecord = repository.GetOmdbMovieRecord(m.Id);
                    if (omdbRecord == null)
                    {
                        int i = 0;
                        bool tryagain = true;
                        while (i < 20 && tryagain)
                        {
                            var client = new RestClient("http://www.omdbapi.com/");
                            var request = new RestRequest(Method.GET);
                            request.AddParameter("i", m.Id);
                            request.AddQueryParameter("apikey", "c5fea23a");
                            var response = client.Execute<OmdbMovieRecord>(request);                            

                            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                            {
                                i++;
                                tryagain = true;
                                Thread.Sleep(5000);
                            }
                            else
                            {
                                tryagain = false;
                            }

                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                if (response.Data != null && response.Data.ImdbID != null)
                                {
                                    omdbRecord = response.Data;
                                    repository.AddOmdbMovieRecord(omdbRecord);
                                    repository.Save();
                                    break;
                                }
                            }
                        }
                    }

                    var movieData = new FullMovieData();
                    movieData.Directors = m.Directors;
                    movieData.Genres = m.Genres;
                    movieData.ImdbID = m.Id;
                    movieData.ImdbRating = m.ImdbRating;
                    movieData.Runtime = m.Runtime;
                    movieData.Title = m.Title;
                    movieData.TitleType = m.TitleType;
                    movieData.UserRating = m.UserRating;
                    movieData.Year = m.Year;

                    if (omdbRecord != null)
                    {
                        movieData.Actors = this.GetCommaSeparetedValue(omdbRecord.Actors);
                        movieData.Awards = omdbRecord.Awards;
                        movieData.Country = this.GetCommaSeparetedValue(omdbRecord.Country);
                        movieData.Director = this.GetCommaSeparetedValue(omdbRecord.Director);
                        movieData.ImdbVotes = omdbRecord.ImdbVotes;
                        movieData.Language = omdbRecord.Language;
                        movieData.Metascore = omdbRecord.Metascore;
                        movieData.Plot = omdbRecord.Plot;
                        movieData.Poster = omdbRecord.Poster;
                        movieData.Rated = omdbRecord.Rated;
                        movieData.Released = omdbRecord.Released;
                        movieData.Writer = this.GetCommaSeparetedValue(omdbRecord.Writer);                        
                    }

                    moviesCollection.Add(movieData);
                });


                int totalUserRating = 0;
                decimal totalImdbRating = 0M;
                int totalTime = 0;

                Dictionary<string, int> favoriteDirector = new Dictionary<string, int>();
                Dictionary<string, int> favoriteGenre = new Dictionary<string, int>();
                Dictionary<string, int> favoriteActorActress = new Dictionary<string, int>();
                Dictionary<string, int> favoriteWriter = new Dictionary<string, int>();
                Dictionary<string, int> favoriteCountry = new Dictionary<string, int>();
                Dictionary<string, int> favoriteLanguage = new Dictionary<string, int>();

                foreach (var movie in moviesCollection)
                {
                    totalUserRating = totalUserRating + movie.UserRating;
                    totalImdbRating = totalImdbRating + movie.ImdbRating;
                    totalTime = totalTime + movie.Runtime;


                    favoriteDirector.AddOrUpdateCount(movie.Directors);

                    
                    foreach (var genre in movie.Genres)
                    {
                        favoriteGenre.AddOrUpdateCount(genre);
                    }

                    foreach (var actor in movie.Actors)
                    {
                        favoriteActorActress.AddOrUpdateCount(actor);
                    }

                    foreach (var writer in movie.Writer)
                    {
                        favoriteWriter.AddOrUpdateCount(writer);
                    }

                    foreach (var country in movie.Country)
                    {
                        favoriteCountry.AddOrUpdateCount(country);
                    }

                    favoriteLanguage.AddOrUpdateCount(movie.Language);
                }

                imdbData.TotalTime = totalTime;
                imdbData.TotalImdbRating = decimal.Round(totalImdbRating / imdbMovieCsvCollection.Count, 1);
                imdbData.TotalUserRating = decimal.Round(Convert.ToDecimal(totalUserRating) / imdbMovieCsvCollection.Count, 1);
                imdbData.FavoriteDirector = favoriteDirector.OrderByDescending(o => o.Value).First();
                imdbData.FavoriteGenre = favoriteGenre.OrderByDescending(o => o.Value).First();
                imdbData.FavoriteActorActress = favoriteActorActress.OrderByDescending(o => o.Value).First();
                imdbData.FavoriteWriter = favoriteWriter.OrderByDescending(o => o.Value).First();
                imdbData.FavoriteCountry = favoriteCountry.OrderByDescending(o => o.Value).First();
                imdbData.FavoriteLanguage = favoriteLanguage.OrderByDescending(o => o.Value).First();
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
                    imdbMovieCsv.ImdbRating = ConvertToDecimalWithCulture(csv.GetField<string>(9));
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

        private decimal ConvertToDecimalWithCulture(string value)
        {
            CultureInfo culture = new CultureInfo("en-US");
            return Convert.ToDecimal(value, culture);
        }

        private string[] GetCommaSeparetedValue(string value)
        {

            var stringArray = value.Split(',');
            for (int i = 0; i < stringArray.Length; i++)
            {
                string input = stringArray[i];
                string regex = "(\\[.*\\])|(\".*\")|('.*')|(\\(.*\\))";
                string output = Regex.Replace(input, regex, "");
                output = output.Replace("  ", " ");
                output = output.Replace("  ", " ");
                stringArray[i] = output.TrimEnd().TrimStart();
            }

            return stringArray;


        }
    }
}