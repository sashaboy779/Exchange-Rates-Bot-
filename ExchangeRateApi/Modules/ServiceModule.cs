using ExchangeRateApi.Services;
using ExchangeRateApi.Services.Interfaces;
using Ninject.Modules;
using Ninject.Web.Common;

namespace ExchangeRateApi.Modules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IApiService>().To<ApiService>().InRequestScope();
            Kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            Kernel.Bind<ICurrenciesService>().To<CurrenciesService>().InRequestScope();
            Kernel.Bind<IExchangeRateService>().To<ExchangeRateService>().InRequestScope();
            Kernel.Bind<ILocalizationService>().To<LocalizationService>().InRequestScope();
        }
    }
}