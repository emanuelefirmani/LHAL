using RestSharp;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace LHAL.WebAPI.Test
{
    class TestControllerTest
    {
        [NUnit.Framework.Test]
        public void GetReturnsOK()
        {
            var address = "http://localhost:9000/";

            using (Microsoft.Owin.Hosting.WebApp.Start<Startup>(url: address))
            {
                var request = new RestRequest("api/test", Method.GET);

                var client = new RestClient(address);
                var response = client.Execute(request);

                response.ResponseStatus.Should().Be(ResponseStatus.Completed);
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var der = new RestSharp.Deserializers.JsonDeserializer();
                der.Deserialize<string>(response).Should().Be("OK");
            }
        }

        [NUnit.Framework.Test]
        public void EchoReturnsEchoedString()
        {
            var address = "http://localhost:9000/";

            using (Microsoft.Owin.Hosting.WebApp.Start<Startup>(url: address))
            {
                var request = new RestRequest("api/test/echo", Method.GET);
                request.AddParameter("message", "Tim");

                var client = new RestClient(address);
                var response = client.Execute(request);

                response.ResponseStatus.Should().Be(ResponseStatus.Completed);
                response.StatusCode.Should().Be(HttpStatusCode.OK);

                var der = new RestSharp.Deserializers.JsonDeserializer();
                der.Deserialize<string>(response).Should().Be("Hello Tim");
            }
        }
    }
}
