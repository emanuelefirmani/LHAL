using System.Web.Http;

namespace LHAL.WebAPI.Controllers
{
    public class TestController : ApiController
    {
        public string Get(string message)
        {
            return "Hello " + message;
        }

        public string Post(Models.TestEchoModel model)
        {
            return "Hello " + model.Message;
        }
    }
}