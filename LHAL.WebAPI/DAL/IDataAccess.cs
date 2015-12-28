using System.Collections.Generic;
using System.Linq;
using LHAL.WebAPI.Models;

namespace LHAL.WebAPI.DAL
{
    public interface IDataAccess
    {
        IQueryable<Giocatore> GetPlayers();
        IQueryable<Stagione> GetSeasons();
        IQueryable<Squadra> GetTeams();
        IQueryable<Squadra> GetTeams(int seasonID);
        List<TeamPlayer> GetTeamPlayers(int teamID, int seasonID);
    }
}
