using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
