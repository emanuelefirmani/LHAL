using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LHAL.WebAPI.DAL
{
    public class DataAccess : DAL.IDataAccess
    {
        public IQueryable<Giocatore> GetPlayers()
        {
            var context = new LHAL_AppEntities();
            return context.Giocatore;
        }
    }
}