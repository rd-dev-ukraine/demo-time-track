using LanceTrack.DataAccess.Projects;
using LanceTrack.DataAccess.ProjectTime;
using LanceTrack.DataAccess.TimeTracking;
using LanceTrack.DataAccess.UserAccounts;
using LanceTrack.Server.Dependencies.Project;
using LanceTrack.Server.Dependencies.ProjectTime;
using LanceTrack.Server.Dependencies.TimeTracking.Event;
using LanceTrack.Server.Dependencies.TimeTracking.ReadModels.ProjectDailyTime;
using LanceTrack.Server.Dependencies.UserAccounts;
using Ninject.Modules;

namespace LanceTrack.DataAccess
{
    public class DataAccessDependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectRepository>().To<DatabaseProjectRepository>();
            Bind<IProjectDailyTimeStorage>().To<DatabaseProjectTimeRepository>();
            Bind<ITimeTrackingEventRepository>().To<DatabaseTimeTrackingEventRepository>();
            Bind<IUserAccountDataAccessor>().To<DatabaseUserAccountAccessor>();
            Bind<IProjectTimeRepository>().To<DatabaseProjectTimeRepository>();
        }
    }
}