using System;
using BetTeamsBattle.Data.Model;

namespace BetTeamsBattle.Data.Repositories.ContextScope
{
    public class ContextScope : IDisposable
    {
        [ThreadStatic]
        public static ModelContext CurrentContext;

        private bool _savedChanges = false;

        public ContextScope()
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