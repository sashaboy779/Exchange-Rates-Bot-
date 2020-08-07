using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using ExchangeRateApi.Infrastructure.Exceptions;

namespace ExchangeRateApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // TODO Add Log4NetExceptionLogger
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }
}