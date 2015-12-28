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

        public static Models.Team Map(this Squadra team)
        {
            return new Models.Team
            {
                Name = team.Nome,
                Guid = team.GUID,
                FoundationYear = team.AnnoFondazione,
                Email = team.Email,
                ID = team.ID,
                ImagePath = team.ImagePath,
                Responsible = team.Responsabili
            };
        }
    }
}