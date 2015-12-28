using System;
using System.Collections.Generic;
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

        [Test]
        public void GETShouldReturnTheTeamDetails()
        {
            var request = new RestRequest("api/team/1", Method.GET);

            var response = Fixtures.Client.Execute<Models.Team>(request);

            response.Data.Should().NotBeNull();
        }

    }
}
