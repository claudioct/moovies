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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}