using System.Linq;
using System.Web.Http;
using LHAL.WebAPI.DAL;

namespace LHAL.WebAPI.Controllers
{
    public class TeamController : ApiController
    {
        private readonly IDataAccess _dataAccess;

        public TeamController() : this(new DataAccess()) { }
        public TeamController(IDataAccess access)
        {
            _dataAccess = access;
        }

        [Route("api/team/{teamID:int}")]
        [HttpGet]
        public Models.Team Get(int teamID)
        {
            var teamInfo = _dataAccess.GetTeams().SingleOrDefault(x => x.ID == teamID);

            if (teamInfo == null)
                return null;

            return teamInfo.Map();
        }
    }
}