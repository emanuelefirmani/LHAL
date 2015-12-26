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
            return FilterArray(players);
        }

        private List<Models.Player> FilterArray(List<Models.Player> array)
        {
            if (string.IsNullOrEmpty(Request.RequestUri.Query))
                return array;

            var qs = HttpUtility.ParseQueryString(Request.RequestUri.Query);
            var q = array.AsQueryable<Models.Player>();

            foreach(var p in qs.AllKeys)
            {
                switch (p.ToLower())
                {
                    case "name":
                        q = q.Where(x => string.Compare(x.Name, qs[p], true) == 0);
                        break;
                }
            }

            return q.ToList();
        }
    }
}