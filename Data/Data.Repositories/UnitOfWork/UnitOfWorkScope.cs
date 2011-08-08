using System;
using BetTeamsBattle.Data.Model;

namespace BetTeamsBattle.Data.Repositories.ContextScope
{
    public class UnitOfWorkScope : IDisposable
    {
        [ThreadStatic]
        public static ModelContext CurrentContext;

        public UnitOfWorkScope()
        {
            CurrentContext = new ModelContext();
        }

        public void SaveChanges()
        {
            CurrentContext.SaveChanges();
        }

        public void Dispose()
        {
            CurrentContext.Dispose();
        }
    }
}