using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace LHAL.WebAPI.Test.Integration
{
    class SeasonsControllerTest
    {
        [Test]
        public void GETShouldReturnArray()
        {
            var request = new RestRequest("api/seasons", Method.GET);

            var response = Fixtures.Client.Execute<List<Models.Season>>(request);

            response.Data.Count.Should().Be(3);
            response.Data.FirstOrDefault(x => x.ID == 1).Should().NotBeNull();
            response.Data.FirstOrDefault(x => x.Description == "2015/16").Should().NotBeNull();
        }
    }
}