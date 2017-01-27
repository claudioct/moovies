using Moovies.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Moovies.Services
{
    public class ImdbDataService : IImdbDataService
    {
        public ImdbData Retrieve(HttpPostedFileBase file)
        {
            try
            {
                return new ImdbData();
            }
            catch (Exception ex)
            {
                // Add logging
                return null;
            }            
        }

    }
}