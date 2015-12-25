using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LHAL.WebAPI.Controllers
{
    public class PlayersController : ApiController
    {
        public List<Models.Player> Get()
        {
            return new List<Models.Player> { new Models.Player { Name = "Tim" } };
        }
    }
}