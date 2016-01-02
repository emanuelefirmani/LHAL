using System.Collections.Generic;

namespace LHAL.WebAPI.Models
{
    public class Round
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
    }
}