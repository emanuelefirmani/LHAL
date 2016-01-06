using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using LHAL.WebAPI.DAL;
using LHAL.WebAPI.Models;

namespace LHAL.WebAPI.Controllers
{
    public class PlayersController : ApiController
    {
        private readonly IDataAccess _dataAccess;

        public PlayersController() : this(new DataAccess()) { }
        public PlayersController(IDataAccess access)
        {
            _dataAccess = access;
        }

        public List<Player> Get(string id = null,string name = null, string lastname = null, string initialLetter = null)
        {
            var query = _dataAccess.GetPlayers();

            if (id != null)
            {
                int playerId;
                if (int.TryParse(id, out playerId))
                    query = query.Where(x => x.ID == playerId);
                else
                    return null;
            }

            if (name != null)
                query = query.Where(x => x.Nome == name);

            if (lastname != null)
                query = query.Where(x => x.Cognome == lastname);

            if (initialLetter != null)
                query = query.Where(x => x.Cognome.StartsWith(initialLetter));

            return query.OrderBy(x => x.Cognome).ThenBy(x => x.Nome).SelectPlayers().ToList();
        }

        [Route("v1/players/lastname-initials")]
        [HttpGet]
        public List<string> GetLastnameInititials()
        {
            return _dataAccess.GetLastnameInititials();
        } 
    }
}