﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LHAL.WebAPI.Controllers
{
    public class PlayersController : ApiController
    {
        private readonly DAL.IDataAccess dataAccess;

        public PlayersController() : this(new DAL.DataAccess()) { }
        public PlayersController(DAL.IDataAccess access)
        {
            dataAccess = access;
        }

        public List<Models.Player> Get()
        {
            return FilterArray(dataAccess.GetPlayers());
        }

        private List<Models.Player> FilterArray(IQueryable<DAL.Giocatore> query)
        {
            if (!string.IsNullOrEmpty(Request.RequestUri.Query))
            {
                var qs = HttpUtility.ParseQueryString(Request.RequestUri.Query);

                foreach (var key in qs.AllKeys)
                {
                    var value = qs[key];
                    switch (key.ToLower())
                    {
                        case "id":
                            int id;
                            if (!int.TryParse(value, out id))
                                return null;

                            query = query.Where(x => x.ID == id);
                            break;
                        case "name":
                            query = query.Where(x => x.Nome == value);
                            break;
                        case "lastname":
                            query = query.Where(x => x.Cognome == value);
                            break;
                        case "initialletter":
                            query = query.Where(x => x.Cognome.StartsWith(value));
                            break;
                    }
                }
            }

            return query.OrderBy(x => x.Cognome).ThenBy(x => x.Nome).ToList().Select(x => new Models.Player(x)).ToList();
        }
    }
}