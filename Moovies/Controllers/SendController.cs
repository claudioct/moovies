using Moovies.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        // GET: Send file angular
        public ActionResult IndexAngular()
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
                //StreamReader stream = new StreamReader(Request.InputStream);
                //string csv = stream.ReadToEnd();
                //var imdbData = _imdbDataService.Retrieve(csv);
                // Read bytes from http input stream
                BinaryReader b = new BinaryReader(file.InputStream);
                byte[] binData = b.ReadBytes(Convert.ToInt32(file.InputStream.Length));

                string csv = System.Text.Encoding.UTF8.GetString(binData);
                var imdbData = _imdbDataService.Retrieve(csv);
                imdbData.TotalImdbRating = Decimal.Round(imdbData.TotalImdbRating, 1);
                return View("Result", imdbData);
            }

            return RedirectToAction("UploadDocument");
        }        

        public ActionResult Result()
        {
            return View("Result");
        }
    }
}
