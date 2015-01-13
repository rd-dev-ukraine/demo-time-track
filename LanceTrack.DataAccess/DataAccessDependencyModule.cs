using LanceTrack.Server.DataAccess.Invoicing;
using LanceTrack.Server.DataAccess.Projects;
using LanceTrack.Server.DataAccess.UserAccounts;
using LanceTrack.Server.Dependencies.Invoicing;
using LanceTrack.Server.Dependencies.Projects;
using LanceTrack.Server.Dependencies.UserAccounts;
using Ninject.Modules;

namespace LanceTrack.Server.DataAccess
{
    public class DataAccessDependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectRepository>().To<DatabaseProjectRepository>();
            Bind<IUserAccountRepository>().To<DatabaseUserAccountRepository>();
            Bind<IInvoiceRepository>().To<DatabaseInvoiceRepository>();
        }
    }
}