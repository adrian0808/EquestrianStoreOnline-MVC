using Ninject;
using Ninject.Modules;
using Sklep_internetowy.DAL;
using Sklep_internetowy.DAL.Interfaces;
using Sklep_internetowy.Service;
using Sklep_internetowy.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sklep_internetowy.Infrastructure
{

    public class NinjectDependencyResolver: IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IProductDbContext>().To<ProductDbContext>();
            kernel.Bind<ICacheProvider>().To<DefaultCacheProvider>();
            kernel.Bind<IClock>().To<SystemClock>();
            kernel.Bind<IContextServices>().To<ContextServices>();
            kernel.Bind<ISessionManager>().To<SessionManager>();
            kernel.Bind<IShoppingCartServices>().To<ShoppingCartServices>();
        }
    }
}