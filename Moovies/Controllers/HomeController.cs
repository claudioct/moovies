using Moovies.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moovies.Controllers
{
    public class HomeController : Controller
    {
        IMooviesBoardRepository _mooviesBoardRepository;


        public HomeController(IMooviesBoardRepository mooviesBoardRepository)
        {
            _mooviesBoardRepository = mooviesBoardRepository;
        }

        public ActionResult Index()
        {
            var leaderboards = _mooviesBoardRepository.GetLeaderboardRecords()
                                                      .OrderByDescending(l => l.TotalTime)
                                                      .Take(3)
                                                      .ToList();
            return View(leaderboards);
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is a simple application made with angularJS / .Net to measure your IMDb data.";

            return View();
        }

        public ActionResult How()
        {
            ViewBag.Message = "Just download you IMDd data and drop it at the cow.";

            return View();
        }
    }
}