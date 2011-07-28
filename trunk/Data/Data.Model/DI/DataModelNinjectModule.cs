using Ninject.Modules;

namespace BetTeamsBattle.Data.Model.DI
{
    public class DataRepositoriesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ModelContext>().To<ModelContext>().InRequestScope();
        }
    }
}