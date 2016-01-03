using System;
using System.Data.Entity;
using System.Data.SqlClient;
using Dapper;
using LHAL.WebAPI.DAL;
using Microsoft.Owin.Hosting;
using NUnit.Framework;
using RestSharp;

namespace LHAL.WebAPI.Test.Integration
{
    [SetUpFixture]
    public class Fixtures
    {
        private const string Address = "http://localhost:9000/";
        internal static RestClient Client { get; private set; }

        [SetUp]
        public void SetUp()
        {
            WebApp.Start<Startup>(Address);
            Client = new RestClient(Address);
            InitDB();
        }

        [TearDown]
        public void TearDown()
        {
            Database.Delete((new LHAL_AppEntities()).Database.Connection);
        }

        private void InitDB()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<LHAL_AppEntities>());
            // Forces initialization of database on model changes. Needed in order to avoid "Login failed" error when accessing DB
            using (var context = new LHAL_AppEntities())
            {
                context.Database.Initialize(true);
            }

            using (var conn = new SqlConnection((new LHAL_AppEntities()).Database.Connection.ConnectionString))
            {
                conn.Execute("INSERT INTO [dbo].[Giocatore]([Nome], [Cognome], [ExTesserato]) VALUES(@name, @lastname, 0)",
                    new[] {
                        new { lastname = "Black", name = "John" },
                        new { lastname = "Black", name = "Tim" },
                        new { lastname = "Bear", name = "Steve" },
                        new { lastname = "White", name = "Tim" },
                        new { lastname = "NoMorePlaying", name = "Player" },
                        new { lastname = "lowerLastname", name = "Player" }
                    }
                );

                conn.Execute("INSERT INTO [dbo].[Stagione]([Testo], [Ordine]) VALUES(@description, @order)",
                    new[] {
                        new { description = "2013/14", order = 0 },
                        new { description = "2014/15", order = 1 },
                        new { description = "2015/16", order = 2 }
                    }
                );

                conn.Execute("INSERT INTO [dbo].[Squadra]([Nome],[GUID],[Responsabili],[AnnoFondazione],[Email],[ImagePath]) VALUES(@name, @guid, @resp, @year, @email, @path)",
                    new[] {
                        new { name = "Team C", guid = Guid.Parse("CCCCCCCC-CCCC-CCCC-CCCC-CCCCCCCCCCCC"), resp = "resp C", year = 2010, email = "emailC@nowhere.com", path = "path C" },
                        new { name = "Team A", guid = Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA"), resp = "resp A", year = 2011, email = "emailA@nowhere.com", path = "path A" },
                        new { name = "Team B", guid = Guid.Parse("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), resp = "resp B", year = 2012, email = "emailB@nowhere.com", path = "path B" },
                        new { name = "Team D", guid = Guid.Parse("DDDDDDDD-DDDD-DDDD-DDDD-DDDDDDDDDDDD"), resp = "resp D", year = 2012, email = "emailD@nowhere.com", path = "path D" }
                    }
                );

                conn.Execute("INSERT INTO [dbo].[Rosa]([IDSquadra],[IDStagione],[IDGiocatore],[Attivo],[Ruolo],[IsCapitano],[IsAssistente]) VALUES(@teamID, @seasonID, @playerID, @active, 'P', 0, 0)",
                    new[] {
                        new { teamID = 1, seasonID = 1, playerID = 1, active = 1 },
                        new { teamID = 2, seasonID = 1, playerID = 2, active = 1 },
                        new { teamID = 2, seasonID = 1, playerID = 6, active = 1 },
                        new { teamID = 3, seasonID = 2, playerID = 1, active = 1 },
                        new { teamID = 3, seasonID = 2, playerID = 4, active = 1 },
                        new { teamID = 3, seasonID = 2, playerID = 5, active = 1 },
                        new { teamID = 1, seasonID = 3, playerID = 1, active = 1 },
                        new { teamID = 2, seasonID = 3, playerID = 2, active = 1 },
                        new { teamID = 3, seasonID = 3, playerID = 3, active = 1 },
                        new { teamID = 3, seasonID = 3, playerID = 5, active = 0 }
                    }
                );

                conn.Execute("INSERT INTO [dbo].[Partita]([SquadraC],[SquadraF],[Rigori],[Data],[Stagione],[SottoStagione],[PrgStagione],[UpdTMS]) VALUES(@homeTeam, @awayTeam, -1, @date, @season, 0, @number, GETDATE())",
                    new[] {
                        new { homeTeam = 1, awayTeam = 2, date = new DateTime(2015, 11, 1, 16, 00, 00), season = 1, number = 1 },
                        new { homeTeam = 1, awayTeam = 2, date = new DateTime(2015, 11, 1, 16, 00, 00), season = 3, number = 1 },
                        new { homeTeam = 1, awayTeam = 3, date = new DateTime(2015, 11, 1, 17, 00, 00), season = 3, number = 2 },
                        new { homeTeam = 2, awayTeam = 3, date = new DateTime(2015, 11, 1, 18, 00, 00), season = 3, number = 3 }
                    }
                );

                conn.Execute("INSERT INTO [dbo].[Girone]([Nome]) VALUES(@name)",
                    new[] {
                        new { name = "Round A" },
                        new { name = "Round B" }
                    }
                );

                conn.Execute("INSERT INTO [dbo].[GironeStagione]([IDGirone],[IDStagione],[IDSquadra]) VALUES(@roundID, @seasonID, @teamID)",
                    new[] {
                        new { teamID = 1, seasonID = 2, roundID = 1 },
                        new { teamID = 2, seasonID = 2, roundID = 2 },
                        new { teamID = 1, seasonID = 3, roundID = 1 },
                        new { teamID = 2, seasonID = 3, roundID = 1 },
                        new { teamID = 3, seasonID = 3, roundID = 2 },
                        new { teamID = 4, seasonID = 3, roundID = 2 }
                    }
                );

                
            }
        }
    }
}
