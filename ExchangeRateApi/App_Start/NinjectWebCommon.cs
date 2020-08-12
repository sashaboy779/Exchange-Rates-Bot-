using System.Runtime.Caching;
using ExchangeRateApi.Infrastructure.Bot;
using ExchangeRateApi.Infrastructure.Bot.Commands.Hidden;
using ExchangeRateApi.Modules;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ExchangeRateApi.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ExchangeRateApi.NinjectWebCommon), "Stop")]

namespace ExchangeRateApi
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IBot>().To<Bot>().InRequestScope();
            kernel.Bind<ObjectCache>().ToConstant(MemoryCache.Default);
            kernel.Bind<Rate>().ToSelf();

            kernel.Load<RepositoryModule>();
            kernel.Load<ServiceModule>();
        }
    }
}