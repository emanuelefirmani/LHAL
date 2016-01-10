﻿using System.Collections.Generic;
using System.Linq;
using LHAL.WebAPI.Models;

namespace LHAL.WebAPI.DAL
{
    public static class Mapper
    {
        private static int _currentSeason;

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

        public static IEnumerable<Player> SelectPlayers(this IQueryable<Giocatore> query)
        {
            var list = query
                .Select(x => new
                {
                    x.Nome,
                    x.ID,
                    x.Cognome,
                    x.ExTesserato,
                    CurrentTeamID = (x.Rosa.FirstOrDefault(r => r.IDStagione == CurrentSeason && r.Attivo) == null) ? 0 : x.Rosa.FirstOrDefault(r => r.IDStagione == CurrentSeason && r.Attivo).Squadra.ID,
                    CurrentTeamName = (x.Rosa.FirstOrDefault(r => r.IDStagione == CurrentSeason && r.Attivo) == null) ? null : x.Rosa.FirstOrDefault(r => r.IDStagione == CurrentSeason && r.Attivo).Squadra.Nome
                })
                .ToList();

            return list.Select(player => new Player
                    {
                        ID = player.ID,
                        Name = player.Nome,
                        Lastname = player.Cognome,
                        TeamID = player.CurrentTeamID,
                        TeamName = player.CurrentTeamName,
                        Ex = player.ExTesserato
                    }
                );
        }

        public static IEnumerable<Season> SelectSeasons(this IQueryable<Stagione> query)
        {
            return query.ToList().Select(season => new Season
            {
                ID = season.ID,
                Description = season.Testo
            });
        }

        public static IEnumerable<Match> SelectMatches(this IQueryable<Partita> query)
        {
            return query.ToList().Select(match => new Match
            {
                ID = match.ID,
                SeasonID = match.Stagione,
                HomeTeamID = match.SquadraC,
                HomeTeamName = match.Squadra.Nome,
                AwayTeamID = match.SquadraF,
                AwayTeamName = match.Squadra1.Nome,
                HomeGoals = match.RetiC ?? 0,
                AwayGoals = match.RetiF ?? 0,
                Date = match.Data,
                ReportURL = match.ImgReferto,
                SubSeason = match.SottoStagione == 0 ? Match.SeasonPart.Regular : Match.SeasonPart.Post,
                Result = GetMatchResult(match.Rigori)
            });
        }

        public static IEnumerable<Team> SelectTeams(this IQueryable<Squadra> query)
        {
            return query.ToList().Select(team => new Team
            {
                Name = team.Nome,
                Guid = team.GUID,
                FoundationYear = team.AnnoFondazione,
                Email = team.Email,
                ID = team.ID,
                ImagePath = team.ImagePath,
                Responsible = team.Responsabili
            });
        }

        public static IEnumerable<PlayerMatchStatistics> SelectPlayerMatchStats(this IQueryable<Tabellino> query)
        {
            return query.ToList().Select(x => new PlayerMatchStatistics
            {
                AwayTeamID = x.Partita.SquadraF,
                AwayTeamName = x.Partita.Squadra1.Nome,
                HomeTeamID = x.Partita.SquadraC,
                HomeTeamName = x.Partita.Squadra.Nome,
                PlayerTeamID = x.Rosa.IDSquadra,
                PlayerTeamName = x.Rosa.Squadra.Nome,
                Date = x.Partita.Data,
                SeasonID = x.Partita.Stagione,
                SeasonDescription = x.Partita.Stagione1.Testo,
                SubSeason = x.Partita.SottoStagione == 0 ? Match.SeasonPart.Regular : Match.SeasonPart.Post,
                MatchResult = GetMatchResult(x.Partita.Rigori),
                HomeGoals = x.Partita.RetiC ?? 0,
                AwayGoals = x.Partita.RetiF ?? 0
            });
        }

        private static Match.MatchResult GetMatchResult(int rigori)
        {
            return rigori == -1 ? Match.MatchResult.NotPlayed :
                    rigori == 0 ? Match.MatchResult.Played :
                    rigori == 1 ? Match.MatchResult.HomeShootOut :
                                  Match.MatchResult.AwayShootOut;
        }
    }
}