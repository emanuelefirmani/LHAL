using System.Linq;

namespace LHAL.WebAPI.DAL
{
    public class DataAccess : IDataAccess
    {
        public IQueryable<Giocatore> GetPlayers()
        {
            var context = new LHAL_AppEntities();
            return context.Giocatore;
        }
    }
}