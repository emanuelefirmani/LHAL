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
    }
}
