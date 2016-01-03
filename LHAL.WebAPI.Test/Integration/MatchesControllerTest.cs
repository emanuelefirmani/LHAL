﻿using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LHAL.WebAPI.Models;
using NUnit.Framework;
using RestSharp;

namespace LHAL.WebAPI.Test.Integration
{
    class MatchesControllerTest
    {
        [Test]
        public void APIMatches_ShouldReturnAnArray()
        {
            var request = new RestRequest("v1/matches", Method.GET);

            var response = Fixtures.Client.Execute<List<Match>>(request);

            response.Data.Count.Should().BeGreaterThan(1);
            response.Data.FirstOrDefault(x => x.HomeTeamID == 1).Should().NotBeNull();
        }

        [Test]
        public void APIMatches_ShouldReturnAFilteredArray()
        {
            var request = new RestRequest("v1/matches", Method.GET);
            request.AddQueryParameter("season", "1");

            var response = Fixtures.Client.Execute<List<Match>>(request);

            response.Data.Count.Should().Be(1);
        }

        [Test]
        public void APIMatches_ShouldReturnNullForInvalidSeasonID()
        {
            var request = new RestRequest("v1/matches", Method.GET);
            request.AddQueryParameter("season", "1000");

            var response = Fixtures.Client.Execute<List<Match>>(request);

            response.Data.Should().BeNull();
        }

        [Test]
        public void APIMatches_ShouldReturnAnOrderedArrayByDate()
        {
            var request = new RestRequest("v1/matches", Method.GET);

            var response = Fixtures.Client.Execute<List<Match>>(request);

            var orderedList = response.Data.OrderBy(x => x.Date).ToList();
            response.Data.ShouldBeEquivalentTo(orderedList, options => options.WithStrictOrdering());
        }
    }
}
