using System.Collections.Generic;
using System.Web.Http;
using LHAL.WebAPI.DAL;
using LHAL.WebAPI.Models;

namespace LHAL.WebAPI.Controllers
{
    public class SeasonController : ApiController
    {
        private readonly IDataAccess _dataAccess;

        public SeasonController() : this(new DataAccess()) { }
        public SeasonController(IDataAccess access)
        {
            _dataAccess = access;
        }

        [Route("v1/season/{seasonID:int}/rounds")]
        [HttpGet]
        public List<Round> GetRounds(int seasonID)
        {
            return _dataAccess.GetRounds(seasonID);
        }
    }
}