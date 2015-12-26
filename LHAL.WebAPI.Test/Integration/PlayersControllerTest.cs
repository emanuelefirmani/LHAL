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
            response.Data.FirstOrDefault(x => x.Name == "Tim").Should().NotBeNull();
        }

        public void GETShouldReturnArrayWhenQueryStringIsMalformed()
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("", "tim");

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Count().Should().Be(3);
        }

        [NUnit.Framework.TestCase("Tim", 2)]
        [NUnit.Framework.TestCase("John", 1)]
        [NUnit.Framework.TestCase("Tom", 0)]
        public void GETShouldReturnAnArrayFilteredByName(string name, int count)
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("name", name);

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Count.Should().Be(count);
        }
    }
}
