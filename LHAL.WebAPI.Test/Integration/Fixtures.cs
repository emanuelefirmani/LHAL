using NUnit.Framework;
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
        internal const string ADDRESS = "http://localhost:9000/";

        [OneTimeSetUpAttribute]
        public void SetUp()
        {
            Microsoft.Owin.Hosting.WebApp.Start<Startup>(url: ADDRESS);
        }
    }
}
