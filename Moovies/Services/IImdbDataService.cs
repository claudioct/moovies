using System.Web;
using Moovies.DataContract;

namespace Moovies.Services
{
    public interface IImdbDataService
    {
        ImdbData Retrieve(HttpPostedFileBase file);
    }
}