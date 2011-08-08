using System;
using BetTeamsBattle.Data.Model;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.ContextScope.Interfaces;

namespace BetTeamsBattle.Data.Repositories.ContextScope
{
    internal class UnitOfWork : ContextManagerBase, IUnitOfWork
    {
        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}