using System;
using BetTeamsBattle.Data.Model;
using BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces;

namespace BetTeamsBattle.Data.Repositories.UnitOfWork
{
    public class UnitOfWorkScope : IUnitOfWorkScope
    {
        [ThreadStatic] public static ModelContext CurrentContext;

        public UnitOfWorkScope()
        {
            CurrentContext = new ModelContext();
        }

        #region IUnitOfWorkScope Members

        public void SaveChanges()
        {
            CurrentContext.SaveChanges();
        }

        public void Dispose()
        {
            CurrentContext.Dispose();
            CurrentContext = null;
        }

        #endregion
    }
}