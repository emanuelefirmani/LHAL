using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;

namespace LHAL.WebAPI.Test.Integration
{
    [SetUpFixture]
    public class Fixtures
    {
        private const string ADDRESS = "http://localhost:9000/";
        internal static RestClient Client { get; private set; }

        [OneTimeSetUpAttribute]
        public void SetUp()
        {
            Microsoft.Owin.Hosting.WebApp.Start<Startup>(url: ADDRESS);
            Client = new RestClient(ADDRESS);
            InitDB();
        }

        [OneTimeTearDownAttribute]
        public void TearDown()
        {
            Database.Delete((new DAL.LHAL_AppEntities()).Database.Connection);
        }

        private void InitDB()
        {
            Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseAlways<DAL.LHAL_AppEntities>());
            // Forces initialization of database on model changes. Needed in order to avoid "Login failed" error when accessing DB
            using (var context = new DAL.LHAL_AppEntities())
            {
                context.Database.Initialize(force: true);
            }  

            using (var conn = new SqlConnection((new DAL.LHAL_AppEntities()).Database.Connection.ConnectionString))
            {
                conn.Execute("INSERT INTO [dbo].[Giocatore]([Nome], [Cognome], [ExTesserato]) VALUES(@name, @lastname, 0)",
                    new [] {
                        new { lastname = "Black", name = "John" },
                        new { lastname = "Black", name = "Tim" },
                        new { lastname = "Bear", name = "Steve" },
                        new { lastname = "White", name = "Tim" }
                    }
                );
            }
        }
    }

    [TestFixture]
    public class TestFixtures
    {
        private TransactionScope transaction;

        [SetUpAttribute]
        public void TestSetUp()
        {
            transaction = new TransactionScope();
        }

        [TearDownAttribute]
        public void TestTearDown()
        {
            transaction.Dispose();
        }
    }
}
