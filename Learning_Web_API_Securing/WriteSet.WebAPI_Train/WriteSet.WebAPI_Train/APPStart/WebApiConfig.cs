using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace WriteSet.WebAPI_Train.APPStart
{
    // Đường router Template 
    [System.Web.Http.Route("api/[controller]")]
    [ApiController]
    public class WebApiConfig : ControllerBase
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services 

            // Web API routers 
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{action}/{id}",
            defaults: new { id = RouteParameter.Optional });
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

        }
    }
}
