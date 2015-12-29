using System.Collections.Generic;
using System.Linq;

namespace LHAL.WebAPI.DAL
{
    public static class Mapper
    {
        private static int _currentSeason = 0;

        private static int CurrentSeason
        {
            get
            {
                if (_currentSeason == 0)
                {
                    var context = new LHAL_AppEntities();
                    _currentSeason = context.Stagione.OrderByDescending(x => x.Ordine).First().ID;
                }
                return _currentSeason;
            }
        }

        public static IEnumerable<Models.Player> SelectPlayer(this IQueryable<Giocatore> query)
        {
            var list = query
                .Select(x => new
                {
                    x.Nome,
                    x.ID,
                    x.Cognome,
                    CurrentTeamID = (x.Rosa.FirstOrDefault(r => r.IDStagione == CurrentSeason && r.Attivo) == null) ? 0 : x.Rosa.FirstOrDefault(r => r.IDStagione == CurrentSeason && r.Attivo).Squadra.ID,
                    CurrentTeamName = (x.Rosa.FirstOrDefault(r => r.IDStagione == CurrentSeason && r.Attivo) == null) ? null : x.Rosa.FirstOrDefault(r => r.IDStagione == CurrentSeason && r.Attivo).Squadra.Nome
                })
                .ToList();

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