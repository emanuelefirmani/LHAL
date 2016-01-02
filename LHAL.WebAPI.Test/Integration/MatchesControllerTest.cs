using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LHAL.WebAPI.Models;
using NUnit.Framework;
using RestSharp;

namespace LHAL.WebAPI.Test.Integration
{
    class MatchesControllerTest
    {
        [Test]
        public void APIMatches_ShouldReturnAnArray()
        {
            var request = new RestRequest("v1/matches", Method.GET);

            var response = Fixtures.Client.Execute<List<Match>>(request);

            response.Data.Count.Should().BeGreaterThan(1);
            response.Data.FirstOrDefault(x => x.HomeTeamID == 1).Should().NotBeNull();
        }
    }
}
