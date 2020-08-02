using ExchangeRateApi.DataAccess;
using ExchangeRateApi.DataAccess.Repository;
using ExchangeRateApi.DataAccess.UnitOfWork;
using ExchangeRateApi.Infrastructure.Constants;
using ExchangeRateApi.Models.User;
using Ninject.Modules;
using Ninject.Web.Common;

namespace ExchangeRateApi.Modules
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ExchangeRateBotContext>().ToSelf().InRequestScope()
                .WithConstructorArgument(AppSettings.DatabaseName);

            Kernel.Bind<IUnitOfWork>().To<UnitOfWork>()
                .InRequestScope();
            
            Kernel.Bind<IGenericRepository<User>>().To<GenericRepository<User>>()
                .InRequestScope();
            Kernel.Bind<IGenericRepository<UserCurrency>>().To<GenericRepository<UserCurrency>>()
                .InRequestScope();
        }
    }
}