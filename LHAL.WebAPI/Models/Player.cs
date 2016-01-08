// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace LHAL.WebAPI.Models
{
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string TeamName { get; set; }
        public int TeamID { get; set; }
        public bool Ex { get; set; }
    }
}