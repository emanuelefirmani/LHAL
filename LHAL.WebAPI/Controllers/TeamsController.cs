using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using LHAL.WebAPI.DAL;

namespace LHAL.WebAPI.Controllers
{
    public class TeamsController : ApiController
    {
        private readonly IDataAccess _dataAccess;

        public TeamsController() : this(new DataAccess()) { }
        public TeamsController(IDataAccess access)
        {
            _dataAccess = access;
        }

        public List<Models.Team> Get()
        {
            IQueryable<Squadra> query = null;
            if (!string.IsNullOrEmpty(Request.RequestUri.Query))
            {
                var qs = HttpUtility.ParseQueryString(Request.RequestUri.Query);

                if (qs.HasKeys())
                {
                    var seasonQS = qs["season"];
                    int seasonID;
                    if (int.TryParse(seasonQS, out seasonID))
                        query = _dataAccess.GetTeams(seasonID);
                }
            }
            if (query == null)
                query = _dataAccess.GetTeams();

            return query.OrderBy(x => x.Nome).ToList().Select(x => x.Map()).ToList();
        }

    }
}