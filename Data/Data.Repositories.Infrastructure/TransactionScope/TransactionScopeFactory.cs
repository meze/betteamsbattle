using System.Transactions;
using BetTeamsBattle.Data.Repositories.Infrastructure.TransactionScope.Interfaces;

namespace BetTeamsBattle.Data.Repositories.Infrastructure.TransactionScope
{
    internal class TransactionScopeFactory : ITransactionScopeFactory
    {
         public System.Transactions.TransactionScope Create()
         {
             return new System.Transactions.TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted});
         }
    }
}