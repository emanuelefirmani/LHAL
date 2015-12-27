namespace LHAL.WebAPI.Models
{
    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }

        public Player() { }
        public Player(DAL.Giocatore player)
        {
            ID = player.ID;
            Name = player.Nome;
            Lastname = player.Cognome;
        }
    }
}