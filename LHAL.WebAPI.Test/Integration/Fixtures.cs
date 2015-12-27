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
                        new { lastname = "White", name = "Tim" }
                    }
                );

                conn.Execute("INSERT INTO [dbo].[Stagione]([Testo], [Ordine]) VALUES(@description, @order)",
                    new[] {
                        new { description = "2013/14", order = 0 },
                        new { description = "2014/15", order = 1 },
                        new { description = "2015/16", order = 2 }
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
