using System.Transactions;
using BetTeamsBattle.Data.Repositories.TransactionScope.Interfaces;

namespace BetTeamsBattle.Data.Repositories.TransactionScope
{
    internal class TransactionScopeFactory : ITransactionScopeFactory
    {
         public System.Transactions.TransactionScope Create()
         {
             return new System.Transactions.TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = IsolationLevel.ReadCommitted});
         }
    }
}