namespace LHAL.WebAPI.DAL
{
    public static class Mapper
    {
        public static Models.Player Map(this Giocatore player)
        {
            return new Models.Player
            {
                ID = player.ID,
                Name = player.Nome,
                Lastname = player.Cognome
            };
        }

        public static Models.Season Map(this Stagione season)
        {
            return new Models.Season
            {
                ID = season.ID,
                Description = season.Testo
            };
        }
    }
}