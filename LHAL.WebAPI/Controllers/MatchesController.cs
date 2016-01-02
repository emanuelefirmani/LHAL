using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LHAL.WebAPI.DAL;
using LHAL.WebAPI.Models;

namespace LHAL.WebAPI.Controllers
{
    public class MatchesController : ApiController
    {
        private readonly IDataAccess _dataAccess;

        public MatchesController() : this(new DataAccess()) { }
        public MatchesController(IDataAccess access)
        {
            _dataAccess = access;
        }


        public List<Match> Get()
        {
            return _dataAccess.GetMatches().SelectMatches().ToList();
        }
    }
}
