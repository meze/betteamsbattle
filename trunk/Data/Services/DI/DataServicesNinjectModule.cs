using BetTeamsBattle.Data.Services.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.Data.Services.DI
{
    public class DataServicesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBattlesService>().To<BattlesService>();
            Bind<IUsersService>().To<UsersService>();
            Bind<ITeamsService>().To<TeamsService>();
        }
    }
}