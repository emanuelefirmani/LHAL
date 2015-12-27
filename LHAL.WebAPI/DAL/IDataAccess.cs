using System;
namespace LHAL.WebAPI.DAL
{
    public interface IDataAccess
    {
        System.Linq.IQueryable<Giocatore> GetPlayers();
    }
}
