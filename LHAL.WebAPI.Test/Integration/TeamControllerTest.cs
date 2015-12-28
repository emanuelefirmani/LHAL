using System;
using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace LHAL.WebAPI.Test.Integration
{
    class TeamControllerTest
    {
        [Test]
        public void GETShouldReturnNullIfTeamIDDoesntExist()
        {
            var request = new RestRequest("api/team/1000", Method.GET);

            var response = Fixtures.Client.Execute<Models.Team>(request);

            response.Data.Should().BeNull();
        }

        [TestCase("abc")]
        [TestCase("")]
        public void GETShouldReturnErrorIfTeamIDIsntValid(string teamID)
        {
            var request = new RestRequest("api/team/{teamID}", Method.GET);
            request.AddUrlSegment("teamID", teamID);

            var response = Fixtures.Client.Execute<Models.Team>(request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void GETShouldReturnTheTeamDetails()
        {
            var request = new RestRequest("api/team/1", Method.GET);

            var response = Fixtures.Client.Execute<Models.Team>(request);

            response.Data.Should().NotBeNull();
        }

    }
}
