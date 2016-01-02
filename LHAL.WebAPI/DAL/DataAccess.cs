using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LHAL.WebAPI.Models;

namespace LHAL.WebAPI.DAL
{
    public class DataAccess : IDataAccess
    {
        private readonly LHAL_AppEntities _context = new LHAL_AppEntities();

        public IQueryable<Giocatore> GetPlayers()
        {
            return _context.Giocatore;
        }

        public IQueryable<Partita> GetMatches()
        {
            return _context.Partita;
        }

        public List<Round> GetRounds(int seasonID)
        {
            var query = _context.GironeStagione.Where(x => x.IDStagione == seasonID)
                .Include(x => x.Girone)
                .Include(x => x.Squadra);

            var output = new List<Round>();
            foreach (var girone in query.Select(x => x.Girone).Distinct().ToList())
            {
                var round = new Round {Name = girone.Nome};
                round.Teams = query.Where(x => x.IDGirone == girone.ID).Select(x => x.Squadra).SelecTeams().ToList();
                output.Add(round);
            }

            if (!output.Any())
                return null;

            return output;
        }

        public IQueryable<Stagione> GetSeasons()
        {
            return _context.Stagione;
        }

        public IQueryable<Squadra> GetTeams()
        {
            return _context.Squadra;
        }

        public IQueryable<Squadra> GetTeams(int seasonID)
        {
            return _context.Squadra.Where(x => x.Rosa.Any(r => r.IDStagione == seasonID));
        }

        public List<TeamPlayer> GetTeamPlayers(int teamID, int seasonID)
        {
            var results = _context.Rosa.Where(x => x.IDSquadra == teamID && x.IDStagione == seasonID && x.Attivo).Select(x => new {
                x.Giocatore.Nome,
                x.Giocatore.Cognome,
                x.IsCapitano,
                x.IsAssistente,
                x.Giocatore.ExTesserato,
                x.Ruolo,
                x.NrMaglia1,
                x.NrMaglia2,
                PartiteGiocate = _context.Tabellino.Count(t => t.IDRosa == x.ID)
            }).OrderBy(x => x.Cognome).ThenBy(x => x.Nome).ToList();

            return results.Select(x => new TeamPlayer
            {
                Name = x.Nome,
                Lastname = x.Cognome,
                IsCaptain = x.IsCapitano,
                IsAssistant = x.IsAssistente,
                IsEx = x.ExTesserato,
                Role = x.Ruolo,
                ShirtNumbers = ((x.NrMaglia1.HasValue ? x.NrMaglia1.Value.ToString() : "") + " " + ((x.NrMaglia2.HasValue && x.NrMaglia2 != x.NrMaglia1) ? x.NrMaglia2.Value.ToString() : "")).Trim(),
                PlayedRSMatches = x.PartiteGiocate

            }).ToList();
        }

        public List<string> GetLastnameInititials()
        {
            return _context.Giocatore.Select(x => x.Cognome.Substring(0, 1).ToUpper()).ToList();
        }
    }
}