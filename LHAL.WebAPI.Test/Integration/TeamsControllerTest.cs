﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace LHAL.WebAPI.Test.Integration
{
    class TeamsControllerTest
    {
        [Test]
        public void APITeams_ShouldNotAcceptPOSTs()
        {
            var request = new RestRequest("api/teams", Method.POST);

            var response = Fixtures.Client.Execute<List<Models.Team>>(request);

            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [Test]
        public void APITeams_ShouldReturnArray()
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
        public void APITeams_ShouldReturnAFilteredArrayBySeason(string parameterName)
        {
            var request = new RestRequest("api/teams", Method.GET);
            request.AddQueryParameter(parameterName, "1");

            var response = Fixtures.Client.Execute<List<Models.Team>>(request);

            response.Data.Count.Should().Be(2);
        }

        [Test]
        public void APITeams_ShouldReturnAnOrderedArray()
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
