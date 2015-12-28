using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace LHAL.WebAPI.Test.Integration
{
    class TeamsControllerTest
    {
        [Test]
        public void GETShouldReturnArray()
        {
            var request = new RestRequest("api/teams", Method.GET);

            var response = Fixtures.Client.Execute<List<Models.Team>>(request);

            response.Data.Count.Should().Be(3);
            response.Data.FirstOrDefault(x => x.ID == 1).Should().NotBeNull();
            response.Data.FirstOrDefault(x => x.Name == "Team A").Should().NotBeNull();
            response.Data.FirstOrDefault(x => x.Guid == Guid.Parse("AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAAA")).Should().NotBeNull();
        }

        [TestCase("season")]
        [TestCase("Season")]
        [TestCase("SEASON")]
        public void GETShouldReturnAFilteredArrayBySeason(string parameterName)
        {
            var request = new RestRequest("api/teams", Method.GET);
            request.AddQueryParameter(parameterName, "1");

            var response = Fixtures.Client.Execute<List<Models.Team>>(request);

            response.Data.Count.Should().Be(2);
        }

        [Test]
        public void GETShouldReturnAnOrderedArray()
        {
            var request = new RestRequest("api/teams", Method.GET);

            var response = Fixtures.Client.Execute<List<Models.Team>>(request);

            Models.Team previousTeam = null;
            foreach (var curr in response.Data)
            {
                if (previousTeam != null)
                {
                    var comparison = string.Compare(previousTeam.Name, curr.Name, StringComparison.OrdinalIgnoreCase);
                    if (comparison >= 0)
                    {
                        Assert.Fail();
                    }
                }

                previousTeam = curr;
            }

        }
    }
}
