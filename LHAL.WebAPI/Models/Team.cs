using System;

namespace LHAL.WebAPI.Models
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }
    }
}