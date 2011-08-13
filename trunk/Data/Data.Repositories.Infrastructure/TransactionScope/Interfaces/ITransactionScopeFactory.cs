namespace BetTeamsBattle.Data.Repositories.Infrastructure.TransactionScope.Interfaces
{
    public interface ITransactionScopeFactory
    {
        System.Transactions.TransactionScope Create();
    }
}