using Moovies.Extensions;
using Moovies.Models;
using Moovies.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Moovies.Controllers
{
    public class FilesController : ApiController
    {
        private readonly string workingFolder = HttpRuntime.AppDomainAppPath + @"\Uploads";
        private IImdbDataService _imdbDataService;

        public FilesController(IImdbDataService imdbDataService)
        {
            _imdbDataService = imdbDataService;
        }

        /// <summary>
        ///   Add a photo
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Add()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }
            try
            {
                var provider = new CustomMultipartFormDataStreamProvider(workingFolder);
                //await Request.Content.ReadAsMultipartAsync(provider);
                await Task.Run(async () => await Request.Content.ReadAsMultipartAsync(provider));
                                
                foreach (var file in provider.FileData)
                {
                    var fileInfo = new FileInfo(file.LocalFileName);
                    var bytes = File.ReadAllBytes(file.LocalFileName);

                    string csv = System.Text.Encoding.UTF8.GetString(bytes);
                    var imdbData = _imdbDataService.Retrieve(csv);
                    imdbData.TotalImdbRating = Decimal.Round(imdbData.TotalImdbRating, 1);

                    return Ok(imdbData);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }
    }
}
