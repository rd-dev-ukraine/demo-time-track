using LanceTrack.Domain.Invoicing;
using LanceTrack.Domain.ProjectTime;
using LanceTrack.Domain.ProjectUserInfo;
using LanceTrack.Domain.TimeTracking;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Dependencies.Project;
using LanceTrack.Server.Invoicing;
using LanceTrack.Server.Projects;
using LanceTrack.Server.ProjectTime;
using LanceTrack.Server.ProjectUserInfo;
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
            Bind<ITimeTrackingService>().To<TimeTrackingService>();
            Bind<IProjectTimeService>().To<ProjectTimeService>();
            Bind<IProjectService>().To<ProjectService>();
            Bind<IProjectUserInfoService>().To<ProjectUserInfoService>();
            Bind<IInvoiceService>().To<InvoiceService>();
        }
    }
}