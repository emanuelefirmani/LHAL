using System;

namespace LHAL.WebAPI.Models
{
    public class Team
    {
        public string Name { get; set; }
        public Guid Guid { get; set; }
        public int? FoundationYear { get; set; }
        public string Email { get; set; }
        public int ID { get; set; }
        public string ImagePath { get; set; }
        public string Responsible { get; set; }
    }
}