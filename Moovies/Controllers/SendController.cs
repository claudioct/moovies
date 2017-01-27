using Moovies.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Moovies.Controllers
{
    public class SendController : Controller
    {
        private IImdbDataService _imdbDataService;

        public SendController(IImdbDataService imdbDataService)
        {
            _imdbDataService = imdbDataService;
        }

        [Authorize]
        // GET: Send
        public ActionResult Index()
        {
            return View();
        }        

        [Authorize]
        [HttpPost]
        // POST: Send (UPLOAD)
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/ImdbData/"), fileName);
                //file.SaveAs(path);
                var imdbData = _imdbDataService.Retrieve(file);
            }

            return RedirectToAction("UploadDocument");
        }        
    }
}
