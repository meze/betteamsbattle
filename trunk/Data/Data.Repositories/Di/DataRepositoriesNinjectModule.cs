using BetTeamsBattle.Data.Model;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specific;
using BetTeamsBattle.Data.Repositories.Specific.BattleUser;
using BetTeamsBattle.Data.Repositories.Specific.BattleUser.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.Data.Repositories.DI
{
    public class DataRepositoriesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IRepository<>)).To(typeof(Repository<>));

            Bind<IBattleUserRepository>().To<BattleUserRepository>();
        }
    }
}