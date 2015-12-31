using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LHAL.WebAPI.DAL;
using LHAL.WebAPI.Models;

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

        [Route("v1/team/{teamID:int}")]
        [HttpGet]
        public Team GetDetails(int teamID)
        {
            return _dataAccess.GetTeams().Where(x => x.ID == teamID).SelecTeams().SingleOrDefault();
        }

        [Route("v1/team/{teamID:int}/season/{sessionID:int}/players")]
        [HttpGet]
        public List<TeamPlayer> GetPlayers(int teamID, int sessionID)
        {
            return _dataAccess.GetTeamPlayers(teamID, sessionID);
        }
    }
}