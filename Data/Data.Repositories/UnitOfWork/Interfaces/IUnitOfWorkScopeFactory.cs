namespace BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces
{
    public interface IUnitOfWorkScopeFactory
    {
        IUnitOfWorkScope Create();
    }
}