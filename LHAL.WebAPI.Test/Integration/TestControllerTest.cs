using RestSharp;
using FluentAssertions;
using System.Net;

namespace LHAL.WebAPI.Test.Integration
{
    class TestControllerTest
    {
        [NUnit.Framework.Test]
        public void EchoReturnsEchoedStringWithGET()
        {
            var request = new RestRequest("v1/test", Method.GET);
            request.AddParameter("message", "Tim");
            var der = new RestSharp.Deserializers.JsonDeserializer();

            var response = Fixtures.Client.Execute(request);

            response.ResponseStatus.Should().Be(ResponseStatus.Completed);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            der.Deserialize<string>(response).Should().Be("Hello Tim");
        }

        [NUnit.Framework.Test]
        public void EchoReturnsEchoedStringWithPOST()
        {
            var request = new RestRequest("v1/test", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(new Models.TestEchoModel { Message = "Tim" });
            var der = new RestSharp.Deserializers.JsonDeserializer();

            var response = Fixtures.Client.Execute(request);

            response.ResponseStatus.Should().Be(ResponseStatus.Completed);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            der.Deserialize<string>(response).Should().Be("Hello Tim");
        }
    }
}
