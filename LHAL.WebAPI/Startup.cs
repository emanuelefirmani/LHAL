using Microsoft.Owin;
using Owin;
using System.Web.Http;
#pragma warning disable 657

namespace LHAL.WebAPI
{
    [assembly: OwinStartup(typeof(Startup))]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "v1/{controller}");

            app.UseWebApi(config);
        }
    }
}