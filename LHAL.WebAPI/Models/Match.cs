using System;

namespace LHAL.WebAPI.Models
{
    public class Match
    {
        public enum MatchResult
        {
            NotPlayed,
            HomeShootOut,
            AwayShootOut,
            Played
        }

        public enum SeasonPart
        {
            Regular,
            Post
        }

        public int ID { get; set; }
        public int SeasonID { get; set; }
        public int HomeTeamID { get; set; }
        public string HomeTeamName { get; set; }
        public int AwayTeamID { get; set; }
        public string AwayTeamName { get; set; }
        public DateTime Date { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public MatchResult Result { get; set; }
        public SeasonPart SubSeason { get; set; }
        public string ReportURL { get; set; }
    }
}
