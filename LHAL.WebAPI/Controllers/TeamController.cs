using System.Collections.Generic;
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
        public Models.Team GetDetails(int teamID)
        {
            var teamInfo = _dataAccess.GetTeams().SingleOrDefault(x => x.ID == teamID);

            if (teamInfo == null)
                return null;

            return teamInfo.Map();
        }

        [Route("api/team/{teamID:int}/{sessionID:int}/players")]
        [HttpGet]
        public List<Models.TeamPlayer> GetPlayers(int teamID, int sessionID)
        {
            return _dataAccess.GetTeamPlayers(teamID, sessionID);
        }
    }
}