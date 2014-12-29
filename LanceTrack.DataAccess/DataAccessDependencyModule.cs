using LanceTrack.DataAccess.Projects;
using LanceTrack.DataAccess.ProjectTime;
using LanceTrack.DataAccess.TimeTracking;
using LanceTrack.DataAccess.UserAccounts;
using LanceTrack.Server.Projects;
using LanceTrack.Server.TimeTracking.Events;
using LanceTrack.Server.TimeTracking.ReadModels;
using LanceTrack.Server.UserAccounts;
using Ninject.Modules;

namespace LanceTrack.DataAccess
{
    public class DataAccessDependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectAccessor>().To<DatabaseProjectAccessor>();
            Bind<IProjectPermissionsAccessor>().To<DatabaseProjectPermissionsAccessor>();
            Bind<IProjectDailyTimeStorage>().To<DatabaseProjectTimeRepository>();
            Bind<ITimeTrackingEventRepository>().To<DatabaseTimeTrackingEventRepository>();
            Bind<IUserAccountDataAccessor>().To<DatabaseUserAccountAccessor>();
        }
    }
}