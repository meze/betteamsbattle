using BetTeamsBattle.Data.Repositories.Infrastructure.TransactionScope;
using BetTeamsBattle.Data.Repositories.Infrastructure.TransactionScope.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.Data.Repositories.Infrastructure.DI
{
    public class DataRepositoriesInfrastructureNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITransactionScopeFactory>().To<TransactionScopeFactory>();
        }
    }
}