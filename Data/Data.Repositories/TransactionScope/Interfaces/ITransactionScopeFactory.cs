namespace BetTeamsBattle.Data.Repositories.TransactionScope.Interfaces
{
    public interface ITransactionScopeFactory
    {
        System.Transactions.TransactionScope Create();
    }
}