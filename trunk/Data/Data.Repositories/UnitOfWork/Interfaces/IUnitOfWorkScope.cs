using System;

namespace BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkScope : IDisposable
    {
        void SaveChanges();
        void Dispose();
    }
}