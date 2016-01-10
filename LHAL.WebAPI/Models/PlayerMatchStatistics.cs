using System;

namespace LHAL.WebAPI.Models
{
    public class PlayerMatchStatistics
    {
        public int ID { get; set; }
        public int PlayerTeamID { get; set; }
        public string PlayerTeamName { get; set; }
        public int HomeTeamID { get; set; }
        public string HomeTeamName { get; set; }
        public int AwayTeamID { get; set; }
        public string AwayTeamName { get; set; }
        public int SeasonID { get; set; }
        public string SeasonDescription { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public Match.MatchResult MatchResult { get; set; }
        public Match.SeasonPart SubSeason { get; set; }
        public DateTime Date { get; set; }
    }
}