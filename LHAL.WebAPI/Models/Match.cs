using System;

namespace LHAL.WebAPI.Models
{
    public class Match
    {
        public int ID { get; set; }
        public int SeasonID { get; set; }
        public int HomeTeamID { get; set; }
        public string HomeTeamName { get; set; }
        public DateTime Date { get; set; }
    }
}