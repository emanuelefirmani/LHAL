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
        public Player Get(int playerID)
        {
            return _dataAccess.GetPlayers().Where(x => x.ID == playerID).SelectPlayers().SingleOrDefault();
        }
    }
}