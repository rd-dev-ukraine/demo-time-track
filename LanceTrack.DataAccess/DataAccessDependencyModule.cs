using LanceTrack.DataAccess.Projects;
using LanceTrack.DataAccess.TimeTracking;
using LanceTrack.DataAccess.UserAccounts;
using LanceTrack.Server.Projects;
using LanceTrack.Server.TimeTracking;
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
            Bind<ITimeTrackingEventRepository>().To<DatabaseTimeTrackingEventRepository>();
            Bind<IUserAccountDataAccessor>().To<DatabaseUserAccountAccessor>();
        }
    }
}
