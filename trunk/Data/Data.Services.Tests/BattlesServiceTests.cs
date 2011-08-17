using System.Linq;
using System.Threading;
using System.Transactions;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.UnitOfWork;
using BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Data.Services.Tests.DI;
using BetTeamsBattle.Data.Services.Tests.Helpers;
using NUnit.Framework;
using Ninject;
using System;

namespace BetTeamsBattle.Data.Services.Tests
{
    [TestFixture]
    public class BattlesServiceTests
    {
        private TransactionScope _transactionScope;
        private IUnitOfWork _unitOfWork;
        private Creator _creator;

        private IRepository<Battle> _repositoryOfBattle;
        private IRepository<User> _repositoryOfUser;
        private IRepository<UserStatistics> _repositoryOfUserStatistics; 
        private IRepository<BattleUser> _repositoryOfBattleUser;
        private IRepository<BattleUserStatistics> _repositoryOfBattleUserStatistics;
        private IRepository<QueuedBetUrl> _repositoryOfQueuedBetUrl;
        private IRepository<BattleBet> _repositoryOfBattleBet;

        private IBattlesService _battlesService;
        
        [SetUp]
        public void Setup()
        {
            _transactionScope = new TransactionScope();

            var kernel = TestNinjectKernel.Kernel;
            _unitOfWork = kernel.Get<IUnitOfWork>();
            _creator = kernel.Get<Creator>();

            _repositoryOfBattle = kernel.Get<IRepository<Battle>>();
            _repositoryOfUser = kernel.Get<IRepository<User>>();
            _repositoryOfUserStatistics = kernel.Get<IRepository<UserStatistics>>();
            _repositoryOfBattleUser = kernel.Get<IRepository<BattleUser>>();
            _repositoryOfBattleUserStatistics = kernel.Get<IRepository<BattleUserStatistics>>();
            _repositoryOfQueuedBetUrl = kernel.Get<IRepository<QueuedBetUrl>>();
            _repositoryOfBattleBet = kernel.Get<IRepository<BattleBet>>();

            _battlesService = kernel.Get<IBattlesService>();
         }

        [TearDown]
        public void TearDown()
        {
            _transactionScope.Dispose();
        }

        [Test]
        public void CreateBattle()
        {
            var startDate = DateTime.UtcNow;
            var endDate = DateTime.UtcNow;
            var battleType = BattleType.FixedBudget;
            var budget = 10000;

            _battlesService.CreateBattle(startDate, endDate, battleType, budget);

            _repositoryOfBattle.All().Where(b => b.StartDate == startDate && b.EndDate == endDate && b.BattleType == (sbyte) battleType && b.Budget == budget).Single();
        }

        [Test]
        public void JoinBattle_FirstJoin_JoinAndInitializeStatistics()
        {
            var battle = _creator.CreateBattle();
            var user = _creator.CreateUser();

            _battlesService.JoinToBattle(battle.Id, user.Id);

            _repositoryOfBattleUser.All().Where(bu => bu.BattleId == battle.Id && bu.UserId == user.Id && bu.Action == (sbyte) BattleUserAction.Join).Single();
            _repositoryOfBattleUserStatistics.All().Where(bus => bus.BattleId == battle.Id && bus.UserId == user.Id && bus.Balance == battle.Budget && bus.OpenedBetsCount == 0 && bus.ClosedBetsCount == 0).Single();
        }

        [Test]
        public void JoinBattle_SecondJoin_JoinAndStillOneStatistics()
        {
            var battle = _creator.CreateBattle();
            var user = _creator.CreateUser();

            _battlesService.JoinToBattle(battle.Id, user.Id);
            _battlesService.JoinToBattle(battle.Id, user.Id);

            Assert.AreEqual(2, _repositoryOfBattleUser.All().Where(bu => bu.BattleId == battle.Id && bu.UserId == user.Id && bu.Action == (sbyte)BattleUserAction.Join).Count());
            _repositoryOfBattleUserStatistics.All().Where(bus => bus.BattleId == battle.Id && bus.UserId == user.Id && bus.Balance == battle.Budget && bus.OpenedBetsCount == 0 && bus.ClosedBetsCount == 0).Single();
        }

        [Test]
        public void LeaveBattle()
        {
            var battle = _creator.CreateBattle();
            var user = _creator.CreateUser();

            _battlesService.LeaveBattle(battle.Id, user.Id);

            _repositoryOfBattleUser.All().Where(bu => bu.BattleId == battle.Id && bu.UserId == user.Id && bu.Action == (sbyte)BattleUserAction.Leave).Single();
        }

        [Test]
        public void MakeBet()
        {
            var battle = _creator.CreateBattle();
            var user = _creator.CreateUser();

            const string title = "title";
            const double bet = 100;
            const double coefficient = 2.5;
            const string url = "http://url";
            const bool isPrivate = true;

            _battlesService.JoinToBattle(battle.Id, user.Id);
            var battleBetId = _battlesService.MakeBet(battle.Id, user.Id, title, bet, coefficient, url, isPrivate);

            _repositoryOfBattleBet.All().Where(bb => bb.BattleId == battle.Id && bb.UserId == user.Id && bb.Title == title && bb.Bet == bet && bb.Coefficient == coefficient && bb.Url == url && bb.IsPrivate == isPrivate).Single();
            _repositoryOfBattleUserStatistics.All().Where(bus => bus.BattleId == battle.Id && bus.UserId == user.Id && bus.Balance == battle.Budget - bet && bus.OpenedBetsCount == 1 && bus.ClosedBetsCount == 0).Single();
            _repositoryOfQueuedBetUrl.All().Where(qbu => qbu.BattleBetId == battleBetId && qbu.Type == (sbyte)QueuedBetUrlType.Open && qbu.Url == url).Single();
        }

        [Test]
        public void MakeBet_UserIsNotJoinedToThisBattle_Exception()
        {
            var battle = _creator.CreateBattle();
            var user = _creator.CreateUser();

            Assert.Throws<ArgumentException>(() => _battlesService.MakeBet(battle.Id, user.Id, String.Empty, 0, 0, String.Empty, false));
        }

        [Test]
        public void BattleBetSucceeded()
        {
            TestCloseBattleBet(true, null);
        }

        [Test]
        public void BattleBetFailed()
        {
            TestCloseBattleBet(false, null);
        }

        [Test]
        public void BattleBetFailed_NotMineBattleBet_Exception()
        {
            var user = _creator.CreateUser("login1", "openIdUrl1");
            Assert.Throws<ArgumentException>(() => TestCloseBattleBet(false, user.Id));
        }

        private void TestCloseBattleBet(bool success, long? userId)
        {
            var battle = _creator.CreateBattle();
            var user = _creator.CreateUser();

            if (!userId.HasValue)
                userId = user.Id;

            const string title = "title";
            const double bet = 100;
            const double coefficient = 150;
            const string url = "http://url";
            const bool isPrivate = false;

            _battlesService.JoinToBattle(battle.Id, user.Id);
            var battleBetId = _battlesService.MakeBet(battle.Id, user.Id, title, bet, coefficient, url, isPrivate);
            long battleId;
            if (success)
                _battlesService.BetSucceeded(battleBetId, userId.Value, out battleId);
            else
                _battlesService.BetFailed(battleBetId, userId.Value, out battleId);

            var newRating = 0d;
            var newBalance = battle.Budget - bet;
            if (success)
            {
                newRating += bet * coefficient;
                newBalance += bet * coefficient;
            }
            else
                newRating -= bet;

            _repositoryOfUserStatistics.All().Where(us => us.Id == user.Id && us.Rating == newRating).Single();
            _repositoryOfBattleBet.All().Where(bb => bb.Id == battleBetId && bb.CloseDateTime != null && bb.Success == success).Single();
            _repositoryOfBattleUserStatistics.All().Where(bus => bus.BattleId == battle.Id && bus.UserId == user.Id && bus.Balance == newBalance && bus.OpenedBetsCount == 0 && bus.ClosedBetsCount == 1).Single();
        }

        [Test]
        public void UserIsJoinedToBattle_NotJoined_False()
        {
            var battle = _creator.CreateBattle();
            var user = _creator.CreateUser();

            var result = _battlesService.UserIsJoinedToBattle(user.Id, battle.Id);

            Assert.IsFalse(result);
        }

        [Test]
        public void UserIsJoinedToBattle_Joined_True()
        {
            var battle = _creator.CreateBattle();
            var user = _creator.CreateUser();

            _battlesService.JoinToBattle(battle.Id, user.Id);
            var result = _battlesService.UserIsJoinedToBattle(user.Id, battle.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void UserIsJoinedToBattle_Left_False()
        {
            var battle = _creator.CreateBattle();
            var user = _creator.CreateUser();

            _battlesService.JoinToBattle(battle.Id, user.Id);
            _battlesService.LeaveBattle(battle.Id, user.Id);
            var result = _battlesService.UserIsJoinedToBattle(user.Id, battle.Id);

            Assert.IsFalse(result);
        }
    }
}