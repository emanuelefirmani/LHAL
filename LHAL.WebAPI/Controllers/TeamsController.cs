using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using LHAL.WebAPI.DAL;
using LHAL.WebAPI.Models;

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

        public List<Team> Get(string season = null)
        {
            IQueryable<Squadra> query = null;
            if (season != null)
            {
                int seasonID;
                if (int.TryParse(season, out seasonID))
                    query = _dataAccess.GetTeams(seasonID);
            }
            if (query == null)
                query = _dataAccess.GetTeams();

            return query.OrderBy(x => x.Nome).SelectTeams().ToList();
        }

    }
}