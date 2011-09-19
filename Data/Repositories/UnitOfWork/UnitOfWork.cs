using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces;

namespace BetTeamsBattle.Data.Repositories.UnitOfWork
{
    internal class UnitOfWork : ContextManagerBase, IUnitOfWork
    {
        #region IUnitOfWork Members

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        #endregion
    }
}