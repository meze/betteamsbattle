using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces;

namespace BetTeamsBattle.Data.Repositories.UnitOfWork
{
    internal class UnitOfWork : ContextManagerBase, IUnitOfWork
    {
        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}