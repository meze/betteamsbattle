using BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces;
using Ninject;

namespace BetTeamsBattle.Data.Repositories.UnitOfWork
{
    internal class UnitOfWorkScopeFactory : IUnitOfWorkScopeFactory
    {
        private readonly IKernel _kernel;

        public UnitOfWorkScopeFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IUnitOfWorkScope Create()
        {
            return _kernel.Get<UnitOfWorkScope>();
        }
    }
}