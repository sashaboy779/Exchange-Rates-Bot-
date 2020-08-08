using Ninject;
using Ninject.Web.WebApi;
using System.Web.Http.Dependencies;

namespace ExchangeRateApi.App_Start
{
    public class NinjectResolver : NinjectDependencyScope, IDependencyResolver
    {
        private IKernel kernel;
        public NinjectResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }
    }
}