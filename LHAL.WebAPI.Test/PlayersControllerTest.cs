using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net;

namespace LHAL.WebAPI.Test
{
    public class PlayersControllerTest
    {
        [NUnit.Framework.Test]
        public void GETShouldReturnTim()
        {
            var address = "http://localhost:9000/";

            using (Microsoft.Owin.Hosting.WebApp.Start<Startup>(url: address))
            {
                var request = new RestRequest("api/players", Method.GET);
                var client = new RestClient(address);

                var response = client.Execute<List<Models.Player>>(request);

                response.ResponseStatus.Should().Be(ResponseStatus.Completed);
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Data.Count.Should().Be(1);
                response.Data.First().Name.Should().Be("Tim");
            }
        }
    }
}
