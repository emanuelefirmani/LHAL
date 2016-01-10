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
    }
}