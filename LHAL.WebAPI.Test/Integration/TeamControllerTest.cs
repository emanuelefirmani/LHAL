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
    class TeamControllerTest
    {
        [TestCase("v1/team/1")]
        [TestCase("v1/team/3/season/3/players")]
        public void APITeam_ShouldNotAcceptPOSTs(string url)
        {
            var request = new RestRequest(url, Method.POST);

            var response = Fixtures.Client.Execute<List<Team>>(request);

            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [Test]
        public void APITeam_ShouldReturnNullIfTeamIDDoesntExist()
        {
            var request = new RestRequest("v1/team/1000", Method.GET);

            var response = Fixtures.Client.Execute<Team>(request);

            response.Data.Should().BeNull();
        }

        [TestCase("abc")]
        [TestCase("")]
        public void APITeam_ShouldReturnErrorIfTeamIDIsntValid(string teamID)
        {
            var request = new RestRequest("v1/team/{teamID}", Method.GET);
            request.AddUrlSegment("teamID", teamID);

            var response = Fixtures.Client.Execute<Team>(request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void APITeam_ShouldReturnTheTeamDetails()
        {
            var request = new RestRequest("v1/team/1", Method.GET);

            var response = Fixtures.Client.Execute<Team>(request);

            response.Data.Name.Should().Be("Team C");
        }

        [Test]
        public void APITeamPlayers_ShouldReturnNullIfSeasonIDDoesntExist()
        {
            var request = new RestRequest("v1/team/1/1000/players", Method.GET);

            var response = Fixtures.Client.Execute<Team>(request);

            response.Data.Should().BeNull();
        }

        [TestCase("abc")]
        [TestCase("")]
        public void APITeamPlayers_ShouldReturnErrorIfSeasonIDIsntValid(string seasonID)
        {
            var request = new RestRequest("v1/team/1/season/{seasonID}/players", Method.GET);
            request.AddUrlSegment("seasonID", seasonID);

            var response = Fixtures.Client.Execute<Team>(request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void APITeamPlayers_ShouldReturnTeamOrderedArraOfPlayersForSeason()
        {
            var request = new RestRequest("v1/team/3/season/2/players", Method.GET);

            var response = Fixtures.Client.Execute<List<TeamPlayer>>(request);

            response.Data.Any(x => x.Lastname == "NoMorePlaying").Should().BeTrue();
            var orderedList = response.Data.OrderBy(x => x.Lastname).ThenBy(x => x.Name).ToList();
            response.Data.ShouldBeEquivalentTo(orderedList, options => options.WithStrictOrdering());
        }

        [Test]
        public void APITeamPlayers_ShouldNotReturnNoMorePlayingPlayers()
        {
            var request = new RestRequest("v1/team/3/season/3/players", Method.GET);

            var response = Fixtures.Client.Execute<List<TeamPlayer>>(request);

            response.Data.Any(x => x.Lastname == "NoMorePlaying").Should().BeFalse();
        }

    }
}
