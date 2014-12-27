using LanceTrack.Domain.UserAccounts;
using LanceTrack.Server.UserAccounts;
using Ninject.Modules;

namespace LanceTrack.Server
{
    public class ServerDependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserAccountService>().To<UserAccountService>();
        }
    }
}