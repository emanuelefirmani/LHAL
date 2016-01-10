using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using LHAL.WebAPI.Models;
using NUnit.Framework;
using RestSharp;

namespace LHAL.WebAPI.Test.Integration
{
    class PlayerControllerTest
    {
        [Test]
        public void APIPlayer_ShouldReturnNullIfPlayerIDDoesntExist()
        {
            var request = new RestRequest("v1/player/1000", Method.GET);

            var response = Fixtures.Client.Execute<Player>(request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Data.Should().BeNull();
        }

        [TestCase("abc")]
        [TestCase("")]
        public void APIPlayer_ShouldReturnErrorIfPlayerIDIsntValid(string teamID)
        {
            var request = new RestRequest("v1/player/{playerID}", Method.GET);
            request.AddUrlSegment("playerID", teamID);

            var response = Fixtures.Client.Execute<Player>(request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test]
        public void APIPlayer_ShouldReturnTim()
        {
            var request = new RestRequest("v1/player/4", Method.GET);

            var response = Fixtures.Client.Execute<Player>(request);

            response.Data.Lastname.Should().Be("White");
            response.Data.Name.Should().Be("Tim");
            response.Data.Ex.Should().Be(true);
        }
        
        [Test]
        public void APIPlayerMatchStats_ShouldReturnArray()
        {
            var request = new RestRequest("v1/player/1/matchstats", Method.GET);

            var response = Fixtures.Client.Execute<List<PlayerMatchStatistics>>(request);

            response.Data.First().ID.Should().Be(1);

        }

        [Test]
        public void APIPlayerMatchStats_ShouldReturnNullIfPlayerIDDoesntExist()
        {
            var request = new RestRequest("v1/player/1000/matchstats", Method.GET);

            var response = Fixtures.Client.Execute<List<PlayerMatchStatistics>>(request);

            response.Data.Should().BeNull();
        }

        [TestCase("abc")]
        [TestCase("")]
        public void APIPlayerMatchStats_ShouldReturnErrorIfPlayerIDIsntValid(string teamID)
        {
            var request = new RestRequest("v1/player/{playerID}/matchstats", Method.GET);
            request.AddUrlSegment("playerID", teamID);

            var response = Fixtures.Client.Execute<Player>(request);

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
