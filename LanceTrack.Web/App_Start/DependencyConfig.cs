using System;
using System.Web;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server;
using LanceTrack.Server.DataAccess;
using LanceTrack.Web;
using LanceTrack.Web.Infrastructure;
using LinqToDB.Data;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DependencyConfig), "Start")]
[assembly: ApplicationShutdownMethod(typeof(DependencyConfig), "Stop")]

namespace LanceTrack.Web
{
    public static class DependencyConfig
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();
        private static IKernel _cqrsKernel;

        /// <summary>
        ///     Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));

            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        ///     Stops the application.
        /// </summary>
        public static void Stop()
        {
            if (_cqrsKernel != null)
                _cqrsKernel.Dispose();

            bootstrapper.ShutDown();
        }

        /// <summary>
        ///     Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            _cqrsKernel = CqrsConfig.Configure();
            var kernel = new StandardKernel(new ServerDependencyModule(), new DataAccessDependencyModule());

            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel, _cqrsKernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                _cqrsKernel.Dispose();
                throw;
            }
        }

        /// <summary>
        ///     Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel, IKernel cqrsKernel)
        {
            kernel.Bind<ICqrs>()
                  .ToMethod(ctx => cqrsKernel.Get<ICqrs>())
                  .InSingletonScope();

            
            kernel.Bind<UserAccount>()
                  .ConstructUsing((IUserAccountService svc) => svc.FindByEmail(HttpContext.Current.User.Identity.Name))
                  .InRequestScope();

            kernel.Bind<DataConnection>()
                .ToMethod(ctx => new DataConnection( "ConnectionString"))
                .InRequestScope();
        }
    }
}