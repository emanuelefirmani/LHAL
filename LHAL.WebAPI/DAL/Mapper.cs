using System.Collections.Generic;
using System.Linq;

namespace LHAL.WebAPI.DAL
{
    public static class Mapper
    {
        public static IEnumerable<Models.Player> SelectPlayer(this IQueryable<Giocatore> query)
        {
            var context = new LHAL_AppEntities();
            var seasonID = context.Stagione.OrderByDescending(x => x.Ordine).First().ID;


            var list = query
                .Select(x => new
                {
                    x.Nome,
                    x.ID,
                    x.Cognome,
                    CurrentTeamID = (x.Rosa.FirstOrDefault(r => r.IDStagione == seasonID && r.Attivo) == null) ? 0 : x.Rosa.FirstOrDefault(r => r.IDStagione == seasonID && r.Attivo).Squadra.ID,
                    CurrentTeamName = (x.Rosa.FirstOrDefault(r => r.IDStagione == seasonID && r.Attivo) == null) ? null : x.Rosa.FirstOrDefault(r => r.IDStagione == seasonID && r.Attivo).Squadra.Nome
                })
                
                .ToList();

            // query must be materialized, otherwise a SQL exception could be raised
            return list.Select(player => new Models.Player
                    {
                        ID = player.ID,
                        Name = player.Nome,
                        Lastname = player.Cognome,
                        TeamID = player.CurrentTeamID,
                        TeamName = player.CurrentTeamName
                    }
                );
        }

        public static Models.Season Map(this Stagione season)
        {
            return new Models.Season
            {
                ID = season.ID,
                Description = season.Testo
            };
        }

        public static Models.Team Map(this Squadra team)
        {
            return new Models.Team
            {
                Name = team.Nome,
                Guid = team.GUID,
                FoundationYear = team.AnnoFondazione,
                Email = team.Email,
                ID = team.ID,
                ImagePath = team.ImagePath,
                Responsible = team.Responsabili
            };
        }
    }
}