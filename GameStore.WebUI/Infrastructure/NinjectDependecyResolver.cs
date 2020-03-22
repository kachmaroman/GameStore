using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using GameStore.Domain.Abstract;
using GameStore.Domain.Concrete;


namespace GameStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType) => kernel.TryGet(serviceType);

        public IEnumerable<object> GetServices(Type serviceType) => kernel.GetAll(serviceType);

        private void AddBindings()
        {
	        kernel.Bind<IGameRepository>().To<EfGameRepository>();
	        kernel.Bind<IAccountRepository>().To<EfAccountRepository>();
        }
    }
}