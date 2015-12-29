﻿using System.Collections.Generic;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace LHAL.WebAPI.Test.Integration
{
    class TeamControllerTest
    {
        [Test]
        public void APITeam_ShouldReturnNullIfTeamIDDoesntExist()
        {
            var request = new RestRequest("api/team/1000", Method.GET);

            var response = Fixtures.Client.Execute<Models.Team>(request);

            response.Data.Should().BeNull();
        }

        [TestCase("abc")]
        [TestCase("")]
        public void APITeam_ShouldReturnErrorIfTeamIDIsntValid(string teamID)
        {
            var request = new RestRequest("api/team/{teamID}", Method.GET);
            request.AddUrlSegment("teamID", teamID);

            var response = Fixtures.Client.Execute<Models.Team>(request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void APITeam_ShouldReturnTheTeamDetails()
        {
            var request = new RestRequest("api/team/1", Method.GET);

            var response = Fixtures.Client.Execute<Models.Team>(request);

            response.Data.Should().NotBeNull();
        }

        [Test]
        public void APITeamPlayers_ShouldReturnNullIfSeasonIDDoesntExist()
        {
            var request = new RestRequest("api/team/1/1000/players", Method.GET);

            var response = Fixtures.Client.Execute<Models.Team>(request);

            response.Data.Should().BeNull();
        }

        [TestCase("abc")]
        [TestCase("")]
        public void APITeamPlayers_ShouldReturnErrorIfSeasonIDIsntValid(string seasonID)
        {
            var request = new RestRequest("api/team/1/{seasonID}/players", Method.GET);
            request.AddUrlSegment("seasonID", seasonID);

            var response = Fixtures.Client.Execute<Models.Team>(request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void APITeamPlayers_ShouldReturnTeamPlayersForSeason()
        {
            var request = new RestRequest("api/team/1/1/players", Method.GET);

            var response = Fixtures.Client.Execute<List<Models.TeamPlayer>>(request);

            response.Data.Should().NotBeNull();
        }

    }
}
