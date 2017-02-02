using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moovies.Services;

namespace Moovies.Tests
{
    [TestClass]
    public class ImdbServiceTest
    {
        private string _csv;
        private IImdbDataService _imdbService;

        [TestInitialize]
        public void TestInitialize()
        {
            _csv = "\"position\",\"const\",\"created\",\"modified\",\"description\",\"Title\",\"Title type\",\"Directors\",\"You rated\",\"IMDb Rating\",\"Runtime (mins)\",\"Year\",\"Genres\",\"Num. Votes\",\"Release Date (month/day/year)\",\"URL\"\r\n\"1\",\"tt1255953\",\"Sat Jan 28 00:00:00 2017\",\"\",\"\",\"Incêndios\",\"Feature Film\",\"Denis Villeneuve\",\"10\",\"8.2\",\"131\",\"2010\",\"mystery, war\",\"88199\",\"2010-09-03\",\"http://www.imdb.com/title/tt1255953/\"\r\n\"2\",\"tt4698684\",\"Sat Jan 21 00:00:00 2017\",\"\",\"\",\"A Incrível Aventura de Rick Baker\",\"Feature Film\",\"Taika Waititi\",\"9\",\"7.9\",\"101\",\"2016\",\"comedy\",\"40156\",\"2016-01-22\",\"http://www.imdb.com/title/tt4698684/\"\r\n\"3\",\"tt4698684\",\"Sat Jan 21 00:00:00 2017\",\"\",\"\",\"A Incrível Aventura de Rick Baker\",\"Feature Film\",\"Taika Waititi\",\"9\",\"7.9\",\"101\",\"2016\",\"adventure, comedy, drama\",\"40156\",\"2016-01-22\",\"http://www.imdb.com/title/tt4698684/\"";
            _imdbService = new ImdbDataService();
        }

        [TestMethod]
        public void Read_Id_Ok()
        {
            var imdbMoviesCollection = _imdbService.GetRecords(_csv);
            Assert.AreEqual("tt1255953", imdbMoviesCollection[0].Id);
        }

        [TestMethod]
        public void Read_Title_Ok()
        {
            var imdbMoviesCollection = _imdbService.GetRecords(_csv);
            Assert.AreEqual("Incêndios", imdbMoviesCollection[0].Title);
        }

        [TestMethod]
        public void Read_TitleType_Ok()
        {
            var imdbMoviesCollection = _imdbService.GetRecords(_csv);
            Assert.AreEqual("Feature Film", imdbMoviesCollection[0].TitleType);
        }

        [TestMethod]
        public void Read_Directors_Ok()
        {
            var imdbMoviesCollection = _imdbService.GetRecords(_csv);
            Assert.AreEqual("Denis Villeneuve", imdbMoviesCollection[0].Directors);
        }

        [TestMethod]
        public void Read_UserRating_Ok()
        {
            var imdbMoviesCollection = _imdbService.GetRecords(_csv);
            Assert.AreEqual(10, imdbMoviesCollection[0].UserRating);
        }

        [TestMethod]
        public void Read_ImdbRating_Ok()
        {
            var imdbMoviesCollection = _imdbService.GetRecords(_csv);
            Assert.AreEqual(8.2M, imdbMoviesCollection[0].ImdbRating);
        }

        [TestMethod]
        public void Read_Runtime_Ok()
        {
            var imdbMoviesCollection = _imdbService.GetRecords(_csv);
            Assert.AreEqual(131, imdbMoviesCollection[0].Runtime);
        }

        [TestMethod]
        public void Read_Year_Ok()
        {
            var imdbMoviesCollection = _imdbService.GetRecords(_csv);
            Assert.AreEqual(2010, imdbMoviesCollection[0].Year);
        }

        [TestMethod]
        public void Read_Genres_Ok()
        {
            var imdbMoviesCollection = _imdbService.GetRecords(_csv);
            Assert.AreEqual(new string[] { "mystery", "war" }[0], imdbMoviesCollection[0].Genres[0]);
            Assert.AreEqual(new string[] { "mystery", "war" }[1], imdbMoviesCollection[0].Genres[1]);
        }

        [TestMethod]
        public void Calculate_Total_Time()
        {
            var imdbData = _imdbService.Retrieve(_csv);
            Assert.AreEqual(333, imdbData.TotalTime);
        }

        [TestMethod]
        public void Calculate_Total_Imdb_Rating()
        {
            var imdbData = _imdbService.Retrieve(_csv);
            Assert.AreEqual(8M, imdbData.TotalImdbRating);
        }

        [TestMethod]
        public void Calculate_Total_User_Rating()
        {
            var imdbData = _imdbService.Retrieve(_csv);
            Assert.AreEqual(9.3M, imdbData.TotalUserRating);
        }

        [TestMethod]
        public void Calculate_Favorite_Director()
        {
            var imdbData = _imdbService.Retrieve(_csv);
            Assert.AreEqual("Taika Waititi", imdbData.FavoriteDirector);
        }

        [TestMethod]
        public void Calculate_Favorite_Genre()
        {
            var imdbData = _imdbService.Retrieve(_csv);
            Assert.AreEqual("comedy", imdbData.FavoriteGenre);
        }
    }
}
