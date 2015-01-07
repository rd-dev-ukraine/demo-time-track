using System;
using System.Configuration;
using System.Web;
using BLToolkit.Data;
using BLToolkit.Data.DataProvider;
using LanceTrack.DataAccess;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server;
using LanceTrack.Server.Cqrs;
using LanceTrack.Server.Cqrs.DataAccess;
using LanceTrack.Web;
using LanceTrack.Web.Infrastructure;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof (DependencyConfig), "Start")]
[assembly: ApplicationShutdownMethod(typeof (DependencyConfig), "Stop")]

namespace LanceTrack.Web
{
    public static class DependencyConfig
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        ///     Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        ///     Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        ///     Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new ServerDependencyModule(), new DataAccessDependencyModule(), new CqrsDependencyModule(), new CqrsDataAccessDependencyModule());
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        ///     Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbManager>()
                .ToSelf().WhenAnyAncestorMatches(c =>
                {
                    var result = c.Request.Service.Namespace.StartsWith(typeof(CqrsDependencyModule).Namespace) ||
                                 c.Request.Service.Namespace.StartsWith("LanceTrack.Cqrs");
                    return result;
                })
                //.When(r =>
                //{
                //    var result =  r.Target.Type.Namespace.StartsWith(typeof(CqrsDependencyModule).Namespace);
                //    return result;
                //})
                .InSingletonScope();

            kernel.Bind<DbManager>()
                  .ToSelf()
                  .InRequestScope();

            kernel.Bind<UserAccount>()
                  .ConstructUsing((IUserAccountService svc) => svc.FindByEmail(HttpContext.Current.User.Identity.Name))
                  .InRequestScope();
        }
    }
}