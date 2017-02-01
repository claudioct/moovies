using System.Web;
using Moovies.DataContract;
using Moovies.Services.DataContracts;
using System.Collections.Generic;

namespace Moovies.Services
{
    public interface IImdbDataService
    {
        ImdbData Retrieve(string csv);

        List<ImdbMovieCsv> GetRecords(string stringCsv);
    }
}