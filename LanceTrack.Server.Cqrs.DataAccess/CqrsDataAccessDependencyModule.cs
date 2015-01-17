using LanceTrack.Cqrs.Contract;
using LanceTrack.Server.Cqrs.DataAccess.ProjectTime;
using LanceTrack.Server.Cqrs.ProjectTime;
using LanceTrack.Server.Cqrs.ProjectTime.Dependencies;
using Ninject.Modules;

namespace LanceTrack.Server.Cqrs.DataAccess
{
    public class CqrsDataAccessDependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEventStore<ProjectTimeAggregateRoot, int>>().To<ProjectTimeAggregateRootEventStore>().InTransientScope();

            Bind<IDailyTimeStorage>().To<DatabaseDailyTimeStorage>();
            Bind<IProjectUserSummaryStorage>().To<DatabaseProjectUserSummaryStorage>();
            Bind<IInvoiceStorage>().To<DatabaseInvoiceStorage>();
        }
    }
}