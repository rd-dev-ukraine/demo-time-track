using System.Web;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server;
using LanceTrack.Server.Cqrs;
using LanceTrack.Server.Cqrs.DataAccess;
using LanceTrack.Server.DataAccess;
using LanceTrack.Web.Infrastructure;
using LinqToDB.Data;
using Ninject;
using Ninject.Web.Common;

namespace LanceTrack.Web
{
    public static class CqrsConfig
    {
        public static IKernel Configure()
        {
            var kernel = new StandardKernel(
                new ServerDependencyModule(), 
                new DataAccessDependencyModule(), 
                new CqrsDependencyModule(), 
                new CqrsDataAccessDependencyModule());

            kernel.Bind<UserAccount>()
                  .ConstructUsing((IUserAccountService svc) => svc.FindByEmail(HttpContext.Current.User.Identity.Name))
                  .InRequestScope();

            kernel.Bind<DataConnection>()
                  .ToMethod(ctx => new DataConnection("ConnectionString"))
                  .InTransientScope();

            return kernel;
        }
    }
}