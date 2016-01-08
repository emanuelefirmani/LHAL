using System.Web.Http;
using LHAL.WebAPI.Models;

namespace LHAL.WebAPI.Controllers
{
    public class PlayerController : ApiController
    {
        [Route("v1/player/{playerID:int}")]
        [HttpGet]
        public Player Get(int playerID)
        {
            return null;
        }
    }
}