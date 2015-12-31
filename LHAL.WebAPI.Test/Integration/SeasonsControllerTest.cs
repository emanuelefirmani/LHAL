using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentAssertions;
using LHAL.WebAPI.Models;
using NUnit.Framework;
using RestSharp;

namespace LHAL.WebAPI.Test.Integration
{
    class SeasonsControllerTest
    {
        [Test]
        public void APISeasons_ShouldNotAcceptPOSTs()
        {
            var request = new RestRequest("v1/seasons", Method.POST);

            var response = Fixtures.Client.Execute<List<Team>>(request);

            response.StatusCode.Should().Be(HttpStatusCode.MethodNotAllowed);
        }

        [Test]
        public void APISeasons_ShouldReturnArray()
        {
            var request = new RestRequest("v1/seasons", Method.GET);

            var response = Fixtures.Client.Execute<List<Season>>(request);

            response.Data.Count.Should().Be(3);
            response.Data.FirstOrDefault(x => x.ID == 1).Should().NotBeNull();
            response.Data.FirstOrDefault(x => x.Description == "2015/16").Should().NotBeNull();
        }
    }
}