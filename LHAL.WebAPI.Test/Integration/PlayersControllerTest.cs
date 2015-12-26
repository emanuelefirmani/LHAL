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

            response.ResponseStatus.Should().Be(ResponseStatus.Completed);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data.Count.Should().Be(3);
            response.Data.FirstOrDefault(x => x.Name == "Tim").Should().NotBeNull();
        }
    }
}
