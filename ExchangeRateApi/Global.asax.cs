using System.Web;
using System.Web.Mvc;

namespace ExchangeRateApi
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
        }
    }
}