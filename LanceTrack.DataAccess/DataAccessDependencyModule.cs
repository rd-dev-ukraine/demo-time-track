using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanceTrack.DataAccess.UserAccounts;
using LanceTrack.Server.UserAccounts;
using Ninject.Modules;

namespace LanceTrack.DataAccess
{
    public class DataAccessDependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserAccountDataAccessor>().To<DatabaseUserAccountAccessor>();
        }
    }
}
