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
                MemoryStream ms = (file.InputStream as MemoryStream);
                string csv = Encoding.UTF8.GetString(ms.ToArray());


                var imdbData = _imdbDataService.Retrieve(csv);
            }

            return RedirectToAction("UploadDocument");
        }        
    }
}
