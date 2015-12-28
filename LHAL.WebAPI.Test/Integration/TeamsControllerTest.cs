using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace LHAL.WebAPI.Test.Integration
{
    class TeamsControllerTest
    {
        [Test]
        public void GETShouldReturnArray()
        {
            var request = new RestRequest("api/teams", Method.GET);

            var response = Fixtures.Client.Execute<List<Models.Team>>(request);

            response.Data.Count.Should().Be(3);
            response.Data.FirstOrDefault(x => x.ID == 1).Should().NotBeNull();
            response.Data.FirstOrDefault(x => x.Name == "Team A").Should().NotBeNull();
            response.Data.FirstOrDefault(x => x.Guid == Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA")).Should().NotBeNull();
        }

        [Test]
        public void GETShouldReturnAFilteredArrayBySeason()
        {
            var request = new RestRequest("api/teams", Method.GET);
            request.AddQueryParameter("season", "1");

            var response = Fixtures.Client.Execute<List<Models.Team>>(request);

            response.Data.Count.Should().Be(2);
        }
    }
}
