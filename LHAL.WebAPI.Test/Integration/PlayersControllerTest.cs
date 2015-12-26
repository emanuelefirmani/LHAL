using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;

namespace LHAL.WebAPI.Test.Integration
{
    public class PlayersControllerTest
    {
        [NUnit.Framework.Test]
        public void GETShouldReturnArray()
        {
            var request = new RestRequest("api/players", Method.GET);

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Count.Should().Be(3);
            response.Data.Single(x => x.Name == "Tim").Should().NotBeNull();
        }

        [NUnit.Framework.Test]
        public void GETShouldReturnAnArrayFilteredByName()
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("name", "tim");

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Single().Should().NotBeNull();
        }
    }
}
