using LanceTrack.Domain.TimeTracking;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Projects;
using LanceTrack.Server.TimeTracking;
using LanceTrack.Server.TimeTracking.ReadModels;
using LanceTrack.Server.UserAccounts;
using Ninject.Modules;

namespace LanceTrack.Server
{
    public class ServerDependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectPermissionsService>().To<ProjectPermissionsService>();
            Bind<IProjectService>().To<ProjectService>();
            Bind<IUserAccountService>().To<UserAccountService>();
            Bind<ITimeTrackingService>().To<TimeTrackingService>();
            Bind<IProjectTimeReadModel>().To<ProjectDailyTimeReadModel>();
        }
    }
}