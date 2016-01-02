using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using LHAL.WebAPI.Models;
using NUnit.Framework;
using RestSharp;

namespace LHAL.WebAPI.Test.Integration
{
    class TeamsControllerTest
    {
        [Test]
        public void APITeams_ShouldNotAcceptPOSTs()
        {
            var request = new RestRequest("v1/teams", Method.POST);

            var response = Fixtures.Client.Execute<List<Team>>(request);

            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [Test]
        public void APITeams_ShouldReturnArray()
        {
            var request = new RestRequest("v1/teams", Method.GET);

            var response = Fixtures.Client.Execute<List<Team>>(request);

            response.Data.Count.Should().BeGreaterThan(3);
            response.Data.FirstOrDefault(x => x.ID == 1).Should().NotBeNull();
            response.Data.FirstOrDefault(x => x.Name == "Team A").Should().NotBeNull();
            response.Data.FirstOrDefault(x => x.Guid == Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA")).Should().NotBeNull();
        }

        [TestCase("season")]
        [TestCase("Season")]
        [TestCase("SEASON")]
        public void APITeams_ShouldReturnAFilteredArrayBySeason(string parameterName)
        {
            var request = new RestRequest("v1/teams", Method.GET);
            request.AddQueryParameter(parameterName, "1");

            var response = Fixtures.Client.Execute<List<Team>>(request);

            response.Data.Count.Should().Be(2);
        }

        [Test]
        public void APITeams_ShouldReturnAnOrderedArray()
        {
            var request = new RestRequest("v1/teams", Method.GET);

            var response = Fixtures.Client.Execute<List<Team>>(request);

            var orderedList = response.Data.OrderBy(x => x.Name).ToList();
            response.Data.ShouldBeEquivalentTo(orderedList, options => options.WithStrictOrdering());
        }
    }
}
