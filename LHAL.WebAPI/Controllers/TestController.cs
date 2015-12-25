using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LHAL.WebAPI.Controllers
{
    public class TestController : ApiController
    {
        public string Get(string message)
        {
            return "Hello " + message;
        }

        public string Post(LHAL.WebAPI.Models.TestEchoModel model)
        {
            return "Hello " + model.Message;
        }
    }
}