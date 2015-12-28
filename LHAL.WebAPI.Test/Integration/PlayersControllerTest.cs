using System;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace LHAL.WebAPI.Test.Integration
{
    public class PlayersControllerTest
    {
        [Test]
        public void GETShouldReturnArray()
        {
            var request = new RestRequest("api/players", Method.GET);

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Count.Should().Be(4);
            response.Data.FirstOrDefault(x => x.Name == "Tim").Should().NotBeNull();
        }

        [Test]
        public void GETShouldReturnArrayWhenQueryStringIsMalformed()
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("", "tim");

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Count.Should().Be(4);
        }

        [TestCase("Tim", 2)]
        [TestCase("tim", 2)]
        [TestCase("TIM", 2)]
        [TestCase("John", 1)]
        [TestCase("Tom", 0)]
        public void GETShouldReturnAnArrayFilteredByName(string name, int count)
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
        public void GETShouldReturnAFilteredArrayIndependentlyOfParameterCase(string parameterName)
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter(parameterName, "tim");

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Count.Should().Be(2);
        }

        [TestCase(1, "John")]
        [TestCase(2, "Tim")]
        [TestCase(3, "Steve")]
        public void GETShouldReturnAnArrayFilteredByID(int id, string name)
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
        public void GETShouldReturnNullForNonNumericIDs(string id)
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
        public void GETShouldReturnAnArrayFilteredByLastname(string lastname, int count)
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
        public void GETShouldReturnAnArrayFilteredByInitialLetterOfLastname(string initialLetter, int count)
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("initialletter", initialLetter);

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Count.Should().Be(count);
        }

        [Test]
        public void GETShouldReturnAnArrayFilteredByLastnameAndName()
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("name", "tim");
            request.AddQueryParameter("lastname", "white");

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Single().Name.Should().Be("Tim");
            response.Data.Single().Lastname.Should().Be("White");
        }

        [Test]
        public void GETShouldReturnAnArrayFilteredByInitialLetterAndName()
        {
            var request = new RestRequest("api/players", Method.GET);
            request.AddQueryParameter("name", "tim");
            request.AddQueryParameter("initialletter", "w");

            var response = Fixtures.Client.Execute<List<Models.Player>>(request);

            response.Data.Single().Name.Should().Be("Tim");
            response.Data.Single().Lastname.Should().Be("White");
        }

        [Test]
        public void GETShouldReturnAnOrderedArray()
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
    }
}
