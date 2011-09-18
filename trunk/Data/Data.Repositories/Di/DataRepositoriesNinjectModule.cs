using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.TransactionScope;
using BetTeamsBattle.Data.Repositories.TransactionScope.Interfaces;
using BetTeamsBattle.Data.Repositories.UnitOfWork;
using BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.Data.Repositories.DI
{
    public class DataRepositoriesNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IRepository<>)).To(typeof(Repository<>));

            Bind<IUnitOfWork>().To<UnitOfWork.UnitOfWork>();
            Bind<IUnitOfWorkScopeFactory>().To<UnitOfWorkScopeFactory>();

            Bind<ITransactionScopeFactory>().To<TransactionScopeFactory>();
        }
    }
}