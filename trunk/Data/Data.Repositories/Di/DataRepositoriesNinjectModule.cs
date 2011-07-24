using BetTeamsBattle.Data.Repositories.Battles;
using BetTeamsBattle.Data.Repositories.Battles.Interfaces;
using BetTeamsBattle.Data.Repositories.Users;
using BetTeamsBattle.Data.Repositories.Users.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.Data.Repositories.DI
{
    public class DataRepositoriesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUsersRepository>().To<UsersRepository>();
            Bind<IBattlesRepository>().To<BattlesRepository>();
        }
    }
}