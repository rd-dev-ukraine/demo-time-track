using LanceTrack.Domain.Invoicing;
using LanceTrack.Domain.Projects;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Invoicing;
using LanceTrack.Server.Projects;
using LanceTrack.Server.TimeTracking;
using LanceTrack.Server.UserAccounts;
using Ninject.Modules;

namespace LanceTrack.Server
{
    public class ServerDependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserAccountService>().To<UserAccountService>();
            Bind<IUserService>().To<UserService>();
            Bind<ITimeTrackingService>().To<TimeTrackingService>();
            Bind<IProjectService>().To<ProjectService>();
            Bind<IInvoiceService>().To<InvoiceService>();
        }
    }
}