using System;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using NUnit.Framework;

namespace LHAL.WebAPI.Test.Integration
{
    public class PlayersControllerTest
    {
        [Test]
        public void APIPlayers_ShouldNotAcceptPOSTs()
        {
            var request = new RestRequest("api/players", Method.POST);

            var response = Fixtures.Client.Execute<List<Models.Team>>(request);

            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }


        [Test]
        public void APIPlayers_ShouldReturnArray()
        {
            var request = new RestRequest("api/players", Method.GET);

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Count.Should().BeGreaterThan(1);
            response.Data.FirstOrDefault(x => x.Name == "Tim").Should().NotBeNull();
        }

        [Test]
        public void APIPlayers_ShouldReturnArrayWhenQueryStringIsMalformed()
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("", "tim");

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Count.Should().BeGreaterThan(1);
        }

        [TestCase("Tim", 2)]
        [TestCase("tim", 2)]
        [TestCase("TIM", 2)]
        [TestCase("John", 1)]
        [TestCase("Tom", 0)]
        public void APIPlayers_ShouldReturnAnArrayFilteredByName(string name, int count)
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("name", name);

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Count.Should().Be(count);
        }

        [TestCase("name")]
        [TestCase("Name")]
        [TestCase("NAME")]
        [TestCase("nAmE")]
        public void APIPlayers_ShouldReturnAFilteredArrayIndependentlyOfParameterCase(string parameterName)
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter(parameterName, "tim");

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Count.Should().Be(2);
        }

        [TestCase(1, "John")]
        [TestCase(2, "Tim")]
        [TestCase(3, "Steve")]
        public void APIPlayers_ShouldReturnAnArrayFilteredByID(int id, string name)
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("id", id.ToString());

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Single().Name.Should().Be(name);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("abc")]
        [TestCase("1.0")]
        [TestCase("1,0")]
        public void APIPlayers_ShouldReturnNullForNonNumericIDs(string id)
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("id", id);

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Should().BeNull();
        }

        [TestCase("Black", 2)]
        [TestCase("black", 2)]
        [TestCase("White", 1)]
        [TestCase("Brown", 0)]
        public void APIPlayers_ShouldReturnAnArrayFilteredByLastname(string lastname, int count)
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("lastname", lastname);

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Count.Should().Be(count);
        }

        [TestCase("B", 3)]
        [TestCase("b", 3)]
        [TestCase("W", 1)]
        [TestCase("w", 1)]
        [TestCase("X", 0)]
        [TestCase("x", 0)]
        public void APIPlayers_ShouldReturnAnArrayFilteredByInitialLetterOfLastname(string initialLetter, int count)
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("initialletter", initialLetter);

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Count.Should().Be(count);
        }

        [Test]
        public void APIPlayers_ShouldReturnAnArrayFilteredByLastnameAndName()
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("name", "tim");
            request.AddQueryParameter("lastname", "white");

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Single().Name.Should().Be("Tim");
            response.Data.Single().Lastname.Should().Be("White");
        }

        [Test]
        public void APIPlayers_ShouldReturnAnArrayFilteredByInitialLetterAndName()
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("name", "tim");
            request.AddQueryParameter("initialletter", "w");

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Single().Name.Should().Be("Tim");
            response.Data.Single().Lastname.Should().Be("White");
        }

        [Test]
        public void APIPlayers_ShouldReturnAnOrderedArray()
        {
            var request = new RestRequest("api/players", Method.GET);

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            Models.Player previousPlayer = null;
            foreach (var curr in response.Data)
            {
                if (previousPlayer != null)
                {
                    var comparison = string.Compare(previousPlayer.Lastname, curr.Lastname, StringComparison.OrdinalIgnoreCase);
                    if (comparison == 0)
                    {
                        string.Compare(previousPlayer.Name, curr.Name, StringComparison.OrdinalIgnoreCase).Should().BeLessOrEqualTo(0);
                    }
                    else if (comparison > 0)
                    {
                        Assert.Fail();
                    }
                }

                previousPlayer = curr;
            }
        }

        [Test]
        public void APIPlayers_ShouldReturnTheCurrentTeam()
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("name", "tim");
            request.AddQueryParameter("initialletter", "b");// select Tim Black id: 2

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Single().Name.Should().Be("Tim");
            response.Data.Single().Lastname.Should().Be("Black");
            response.Data.Single().TeamID.Should().Be(2);
            response.Data.Single().TeamName.Should().Be("Team A");
        }

        [Test]
        public void APIPlayers_ShouldReturnNoTeamForNonPlayingPlayers()
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("lastname", "NoMorePlaying");

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Single().ID.Should().Be(5);
            response.Data.Single().TeamID.Should().Be(0);
            response.Data.Single().TeamName.Should().BeNullOrEmpty();
        }

    }
}
