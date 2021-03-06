﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using LHAL.WebAPI.Models;
using NUnit.Framework;
using RestSharp;

namespace LHAL.WebAPI.Test.Integration
{
    class SeasonControllerTest
    {
        [Test]
        public void APISeason_ShouldReturnArrayOfRounds()
        {
            var request = new RestRequest("v1/season/2/rounds", Method.GET);

            var response = Fixtures.Client.Execute<List<Round>>(request);

            response.Data.Count.Should().Be(2);
            response.Data.Single(x => x.Name == "Round A").Should().NotBeNull();
            response.Data.Single(x => x.Name == "Round B").Should().NotBeNull();
        }

        [Test]
        public void APISeason_ShouldReturnNullIfSeasonIDDoesntExist()
        {
            var request = new RestRequest("v1/season/1000/rounds", Method.GET);

            var response = Fixtures.Client.Execute<List<Round>>(request);

            response.Data.Should().BeNull();
        }

        [Test]
        public void APISeason_ShouldReturnNullIfSeasonIDHasntRounds()
        {
            var request = new RestRequest("v1/season/1/rounds", Method.GET);

            var response = Fixtures.Client.Execute<List<Round>>(request);

            response.Data.Should().BeNull();
        }

        [TestCase("abc")]
        [TestCase("")]
        public void APISeason_ShouldReturnErrorIfSeasonIDIsntValid(string seasonID)
        {
            var request = new RestRequest("v1/season/{seasonID}/rounds", Method.GET);
            request.AddUrlSegment("seasonID", seasonID);

            var response = Fixtures.Client.Execute<Team>(request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void APISeason_RoundsShouldContainTeams()
        {
            var request = new RestRequest("v1/season/3/rounds", Method.GET);

            var response = Fixtures.Client.Execute<List<Round>>(request);

            response.Data.Count.Should().Be(2);
            response.Data.Single(x => x.Name == "Round A").Teams.Count.Should().Be(2);
            response.Data.Single(x => x.Name == "Round A").Teams.Single(x => x.Name == "Team C").Should().NotBeNull();
            response.Data.Single(x => x.Name == "Round A").Teams.Single(x => x.Name == "Team A").Should().NotBeNull();
            response.Data.Single(x => x.Name == "Round B").Teams.Count.Should().Be(2);
            response.Data.Single(x => x.Name == "Round B").Teams.Single(x => x.Name == "Team B").Should().NotBeNull();
            response.Data.Single(x => x.Name == "Round B").Teams.Single(x => x.Name == "Team D").Should().NotBeNull();
        }
    }
}
