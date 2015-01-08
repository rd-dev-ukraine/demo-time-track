using System;
using LanceTrack.Cqrs.Contract;
using LanceTrack.Cqrs.Server;
using LanceTrack.Server.Cqrs.ProjectTime;
using LanceTrack.Server.Cqrs.ProjectTime.ReadModels;
using Ninject;
using Ninject.Modules;

namespace LanceTrack.Server.Cqrs
{
    public class CqrsDependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICqrs>().To<CqrsServer>().InSingletonScope();
            Bind<IAggregateRootServer>().To<ProjectTimeAggregateRootServer>();

            Bind<ProjectTimeAggregateRoot>().ToSelf();
            Bind<Func<ProjectTimeAggregateRoot>>().ToMethod(context => () => context.Kernel.Get<ProjectTimeAggregateRoot>());

            Bind<IAggregateRootReadModelManager<ProjectTimeAggregateRoot, int>>().To<DailyTimeReadModelManager>();
        }
    }
}
