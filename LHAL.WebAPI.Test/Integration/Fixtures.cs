using System;
using NUnit.Framework;
using RestSharp;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Transactions;
using Dapper;

namespace LHAL.WebAPI.Test.Integration
{
    [SetUpFixture]
    public class Fixtures
    {
        private const string Address = "http://localhost:9000/";
        internal static RestClient Client { get; private set; }

        [OneTimeSetUpAttribute]
        public void SetUp()
        {
            Microsoft.Owin.Hosting.WebApp.Start<Startup>(Address);
            Client = new RestClient(Address);
            InitDB();
        }

        [OneTimeTearDownAttribute]
        public void TearDown()
        {
            Database.Delete((new DAL.LHAL_AppEntities()).Database.Connection);
        }

        private void InitDB()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<DAL.LHAL_AppEntities>());
            // Forces initialization of database on model changes. Needed in order to avoid "Login failed" error when accessing DB
            using (var context = new DAL.LHAL_AppEntities())
            {
                context.Database.Initialize(true);
            }

            using (var conn = new SqlConnection((new DAL.LHAL_AppEntities()).Database.Connection.ConnectionString))
            {
                conn.Execute("INSERT INTO [dbo].[Giocatore]([Nome], [Cognome], [ExTesserato]) VALUES(@name, @lastname, 0)",
                    new[] {
                        new { lastname = "Black", name = "John" },
                        new { lastname = "Black", name = "Tim" },
                        new { lastname = "Bear", name = "Steve" },
                        new { lastname = "White", name = "Tim" },
                        new { lastname = "NoMorePlaying", name = "Player" }
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
                        new { name = "Team B", guid = Guid.Parse("BBBBBBBB-BBBB-BBBB-BBBB-BBBBBBBBBBBB"), resp = "resp B", year = 2012, email = "emailB@nowhere.com", path = "path B" }
                    }
                );

                conn.Execute("INSERT INTO [dbo].[Rosa]([IDSquadra],[IDStagione],[IDGiocatore],[Attivo],[Ruolo],[IsCapitano],[IsAssistente]) VALUES(@teamID, @seasonID, @playerID, @active, 'P', 0, 0)",
                    new[] {
                        new { teamID = 1, seasonID = 1, playerID = 1, active = 1 },
                        new { teamID = 2, seasonID = 1, playerID = 2, active = 1 },
                        new { teamID = 3, seasonID = 2, playerID = 1, active = 1 },
                        new { teamID = 3, seasonID = 2, playerID = 4, active = 1 },
                        new { teamID = 3, seasonID = 2, playerID = 5, active = 1 },
                        new { teamID = 1, seasonID = 3, playerID = 1, active = 1 },
                        new { teamID = 2, seasonID = 3, playerID = 2, active = 1 },
                        new { teamID = 3, seasonID = 3, playerID = 3, active = 1 },
                        new { teamID = 3, seasonID = 3, playerID = 5, active = 0 },
                    }
                );
            }
        }
    }

    [TestFixture]
    public class TestFixtures
    {
        private TransactionScope _transaction;

        [SetUpAttribute]
        public void TestSetUp()
        {
            _transaction = new TransactionScope();
        }

        [TearDownAttribute]
        public void TestTearDown()
        {
            _transaction.Dispose();
        }
    }
}
