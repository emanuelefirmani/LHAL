using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LHAL.WebAPI.Controllers
{
    public class TestController : ApiController
    {
        public string Get()
        {
            return "OK";
        }

        [HttpGet]
        public string Echo(string message)
        {
            return "Hello " + message;
        }

        [HttpPost]
        public string EchoFromPOST([FromBody] LHAL.WebAPI.Models.TestEchoModel model)
        {
            return "Hello " + model.Message;
        }
    }
}