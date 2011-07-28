using BetTeamsBattle.Data.Model;
using BetTeamsBattle.Data.Repositories.Base;
using Ninject.Modules;

namespace BetTeamsBattle.Data.Repositories.DI
{
    public class DataModelNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IRepository<>)).To(typeof(Repository<>));
        }
    }
}