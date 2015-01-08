using LanceTrack.DataAccess.Projects;
using LanceTrack.DataAccess.ProjectTime;
using LanceTrack.DataAccess.ProjectUserSummary;
using LanceTrack.DataAccess.UserAccounts;
using LanceTrack.Server.Dependencies.Project;
using LanceTrack.Server.Dependencies.ProjectDailyTime;
using LanceTrack.Server.Dependencies.ProjectUserInfo;
using LanceTrack.Server.Dependencies.UserAccounts;
using Ninject.Modules;

namespace LanceTrack.DataAccess
{
    public class DataAccessDependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProjectRepository>().To<DatabaseProjectRepository>();
            Bind<IUserAccountDataAccessor>().To<DatabaseUserAccountAccessor>();
            Bind<IProjectTimeRepository>().To<DatabaseProjectTimeRepository>();
            Bind<IProjectUserSummaryAccessor>().To<DatabaseProjectUserSummaryAccessor>();
        }
    }
}