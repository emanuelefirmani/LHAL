using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LHAL.WebAPI.DAL;

namespace LHAL.WebAPI.Controllers
{
    public class SeasonsController : ApiController
    {
        private readonly IDataAccess _dataAccess;

        public SeasonsController() : this(new DataAccess()) { }
        public SeasonsController(IDataAccess access)
        {
            _dataAccess = access;
        }

        public List<Models.Season> Get()
        {
            return _dataAccess.GetSeasons().SelectSeasons().ToList();
        }
    }
}