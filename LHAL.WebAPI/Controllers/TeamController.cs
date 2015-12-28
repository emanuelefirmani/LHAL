using System.Linq;
using System.Web.Http;
using LHAL.WebAPI.DAL;

namespace LHAL.WebAPI.Controllers
{
    public class TeamController : ApiController
    {
        private readonly IDataAccess _dataAccess;

        public TeamController() : this(new DataAccess()) { }
        public TeamController(IDataAccess access)
        {
            _dataAccess = access;
        }

        [Route("api/team/{teamID:int}")]
        [HttpGet]
        public Models.TeamDetails Get(int teamID)
        {
            var teamInfo = _dataAccess.GetTeams().SingleOrDefault(x => x.ID == teamID);

            if (teamInfo == null)
                return null;

            return new Models.TeamDetails
            {
                Name = teamInfo.Nome,
                Guid = teamInfo.GUID,
                FoundationYear = teamInfo.AnnoFondazione,
                Email = teamInfo.Email,
                ID = teamInfo.ID,
                ImagePath = teamInfo.ImagePath,
                Responsible = teamInfo.Responsabili
            };
        }
    }
}