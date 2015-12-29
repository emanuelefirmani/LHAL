// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LHAL.WebAPI.Models
{
    public class TeamPlayer
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public bool IsCaptain { get; set; }
        public bool IsAssistant { get; set; }
        public bool IsEx { get; set; }
        public string Role { get; set; }
        public string ShirtNumbers { get; set; }
        public int PlayedRSMatches { get; set; }
    }
}