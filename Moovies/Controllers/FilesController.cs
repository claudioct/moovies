﻿using Moovies.Data;
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
        private IMooviesBoardRepository _repository;

        public FilesController(IImdbDataService imdbDataService, IMooviesBoardRepository repository)
        {
            _imdbDataService = imdbDataService;
            _repository = repository;
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
                var filesReadToProvider = await Request.Content.ReadAsMultipartAsync();

                foreach (var stream in filesReadToProvider.Contents)
                {
                    var bytes = await stream.ReadAsByteArrayAsync();

                    string csv = System.Text.Encoding.UTF8.GetString(bytes);
                    var imdbData = _imdbDataService.Retrieve(csv);

                    _repository.AddLeaderboard(imdbData);


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
