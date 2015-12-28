using System.Linq;

namespace LHAL.WebAPI.DAL
{
    public interface IDataAccess
    {
        IQueryable<Giocatore> GetPlayers();
        IQueryable<Stagione> GetSeasons();
        IQueryable<Squadra> GetTeams();
    }
}
