using System.Transactions;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Data.Services.Tests.DI;
using BetTeamsBattle.Data.Services.Tests.Helpers;
using NUnit.Framework;
using Ninject;

namespace BetTeamsBattle.Data.Services.Tests
{
    [TestFixture]
    internal class BattleUsersServiceTests
    {
        private TransactionScope _transactionScope;

        private Creator _creator;
        private IBattleUsersService _battleUsersService;

        [SetUp]
        public void Setup()
        {
            _transactionScope = new TransactionScope();

            var kernel = TestNinjectKernel.Kernel;

            _creator = kernel.Get<Creator>();
            _battleUsersService = kernel.Get<IBattleUsersService>();
        }

        [TearDown]
        public void TearDown()
        {
            _transactionScope.Dispose();
        }

        
    }
}