using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ExchangeRateApi.Infrastructure;
using ExchangeRateApi.Infrastructure.Bot;

namespace ExchangeRateApi
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            
            Bot.SetWebhook();
        }
        
        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            Logger.Log.Fatal(exception.Message, exception);
        }
    }
}