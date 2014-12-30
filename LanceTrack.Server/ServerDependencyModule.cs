using LanceTrack.Domain.TimeTracking;
using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.Dependencies.TimeTracking.ReadModels;
using LanceTrack.Server.Dependencies.TimeTracking.ReadModels.ProjectDailyTime;
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
            Bind<ITimeTrackingService>().To<TimeTrackingService>();
            Bind<IProjectTimeReadModelHandler>().To<ProjectDailyTimeReadModelHandler>();
            Bind<ProjectService>().ToSelf();
        }
    }
}