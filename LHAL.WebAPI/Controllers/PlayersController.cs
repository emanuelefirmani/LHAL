using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LHAL.WebAPI.Controllers
{
    public class PlayersController : ApiController
    {
        List<Models.Player> players = new List<Models.Player> { 
            new Models.Player { Name = "John" },
            new Models.Player { Name = "Steve" },
            new Models.Player { Name = "Tim" }, 
        };

        public List<Models.Player> Get()
        {
            return players;
        }
    }
}