using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LHAL.WebAPI.DAL;
using LHAL.WebAPI.Models;

namespace LHAL.WebAPI.Controllers
{
    public class PlayerController : ApiController
    {
        private readonly IDataAccess _dataAccess;

        public PlayerController() : this(new DataAccess()) { }
        public PlayerController(IDataAccess access)
        {
            _dataAccess = access;
        }

        [Route("v1/player/{playerID:int}")]
        [HttpGet]
        public Player GetPlayer(int playerID)
        {
            return _dataAccess.GetPlayers().Where(x => x.ID == playerID).SelectPlayers().SingleOrDefault();
        }

        [Route("v1/player/{playerID:int}/matchstats")]
        [HttpGet]
        public List<PlayerMatchStatistics> GetPlayerStats(int playerID)
        {
            var output = _dataAccess.GetPlayerStats(playerID).SelecTeamsMatchPlayerStats().ToList();
            if (!output.Any())
                return null;
            return output;
        }
    }
}