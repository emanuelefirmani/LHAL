using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using LHAL.WebAPI.DAL;

namespace LHAL.WebAPI.Test.Integration
{
    public class DBWriter
    {
        private readonly SqlConnection _conn = (new LHAL_AppEntities().Database.Connection) as SqlConnection;
        //private readonly SqlConnection _conn = new SqlConnection("data source=(LocalDB)\\v11.0;initial catalog=LHAL_App;integrated security=True;Application Name=EntityFrameworkMUE;");

        private readonly List<Giocatore> _players = new List<Giocatore>();
        private readonly List<Stagione> _seasons = new List<Stagione>();

        public DBWriter WritePlayer(string name, string lastname, DateTime? birthdate = null, bool ex = false)
        {
            var id = _conn.Query<int>(
                "INSERT INTO [dbo].[Giocatore]([Nome], [Cognome], [DataNascita], [ExTesserato]) VALUES(@name, @lastname, @birthdate, @ex) " +
                "SELECT CAST(SCOPE_IDENTITY() as int)",
                new {lastname, name, birthdate, ex}
            ).Single();

            var player = _conn.Query<Giocatore>("SELECT * FROM Giocatore WHERE ID = @id", new {id}).Single();
            _players.Add(player);

            return this;
        }
        
        public DBWriter WriteSeason(string description, int order)
        {

            var id = _conn.Query<int>(
                "INSERT INTO [dbo].[Stagione]([Testo], [Ordine]) VALUES(@description, @order) " +
                "SELECT CAST(SCOPE_IDENTITY() as int)",
                new {description, order}
            ).Single();

            var season = _conn.Query<Stagione>("SELECT * FROM Stagione WHERE ID = @id", new {id}).Single();
            _seasons.Add(season);

            return this;
        }
    }
}
