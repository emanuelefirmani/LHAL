using System.Collections.Generic;
using System.Linq;
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
            return _dataAccess.GetTeams().ToList().Select(x => x.Map()).ToList();
        }

    }
}