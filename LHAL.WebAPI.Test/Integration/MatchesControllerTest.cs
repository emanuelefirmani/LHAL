using System;
using System.Collections.Generic;
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
        public void APIMatches_ShouldReturnTheMatchWithID1()
        {
            var request = new RestRequest("v1/matches", Method.GET);

            var response = Fixtures.Client.Execute<List<Match>>(request);

            var match = response.Data.Single(x => x.ID == 1);
            match.Should().NotBeNull();
            match.Date.Should().Be(new DateTime(2013, 11, 1, 16, 00, 00));
            match.HomeTeamID.Should().Be(1);
            match.HomeTeamName.Should().Be("Team C");
            match.AwayTeamID.Should().Be(2);
            match.AwayTeamName.Should().Be("Team A");
            match.SeasonID.Should().Be(1);
            match.HomeGoals.Should().Be(3);
            match.AwayGoals.Should().Be(1);
            match.Result.Should().Be(Match.MatchResult.Played);
            match.SubSeason.Should().Be(Match.SeasonPart.Regular);
            match.ReportURL.Should().Be("img");
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
            var request = new RestRequest(" ", Method.GET);
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
