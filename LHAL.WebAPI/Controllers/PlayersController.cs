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
            new Models.Player { Lastname = "Black", Name = "John" },
            new Models.Player { Lastname = "Black", Name = "Tim" },
            new Models.Player { Lastname = "Bear", Name = "Steve" },
            new Models.Player { Lastname = "White", Name = "Tim" }, 
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

            foreach(var key in qs.AllKeys)
            {
                switch (key.ToLower())
                {
                    case "name":
                        q = q.Where(x => string.Compare(x.Name, qs[key], true) == 0);
                        break;
                    case "lastname":
                        q = q.Where(x => string.Compare(x.Lastname, qs[key], true) == 0);
                        break;
                    case "initialletter":
                        q = q.Where(x => string.Compare(x.Lastname.Substring(0, 1), qs[key], true) == 0);
                        break;
                }
            }

            return q.ToList();
        }
    }
}