using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using LHAL.WebAPI.DAL;
using LHAL.WebAPI.Models;

namespace LHAL.WebAPI.Controllers
{
    public class MatchesController : ApiController
    {
        private readonly IDataAccess _dataAccess;

        public MatchesController() : this(new DataAccess()) { }
        public MatchesController(IDataAccess access)
        {
            _dataAccess = access;
        }


        public List<Match> Get()
        {

            var query = _dataAccess.GetMatches();
            if (!string.IsNullOrEmpty(Request.RequestUri.Query))
            {
                var qs = HttpUtility.ParseQueryString(Request.RequestUri.Query);

                if (qs.HasKeys())
                {
                    var seasonQS = qs["season"];
                    int seasonID;
                    if (int.TryParse(seasonQS, out seasonID))
                        query = query.Where(x => x.Stagione == seasonID);
                }
            }

            var output = query.OrderBy(x => x.Data).SelectMatches().ToList();
            if (!output.Any())
                return null;
            return output;
        }
    }
}
