using System.Linq;
using System.Threading;
using System.Transactions;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
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
    public class 
        BattlesServiceTests
    {
        private DateTime _battleStartDate = DateTime.UtcNow;
        private DateTime _battleEndDate = DateTime.UtcNow;
        private BattleType _battleType = BattleType.FixedBudget;
        private int _battleBudget = 10000;

        private string _betTitle = "_betTitle";
        private double _bet = 100;
        private double _betCoefficient = 2.5;
        private string _betUrl = "http://url";
        private bool _betIsPrivate = true;

        private TransactionScope _transactionScope;
        private IUnitOfWork _unitOfWork;
        private Creator _creator;

        private IRepository<Battle> _repositoryOfBattle;
        private IRepository<Team> _repositoryOfTeam;
        private IRepository<BattleTeamStatistics> _repositoryOfBattleTeamStatistics; 
        private IRepository<QueuedBetUrl> _repositoryOfQueuedBetUrl;
        private IRepository<BattleBet> _repositoryOfBattleBet;

        private IUsersService _usersService;
        private IBattlesService _battlesService;

        [SetUp]
        public void Setup()
        {
            _transactionScope = new TransactionScope();

            var kernel = TestNinjectKernel.Kernel;
            _unitOfWork = kernel.Get<IUnitOfWork>();
            _creator = kernel.Get<Creator>();

            _repositoryOfBattle = kernel.Get<IRepository<Battle>>();
            _repositoryOfTeam = kernel.Get<IRepository<Team>>();
            _repositoryOfBattleTeamStatistics = kernel.Get<IRepository<BattleTeamStatistics>>();
            _repositoryOfQueuedBetUrl = kernel.Get<IRepository<QueuedBetUrl>>();
            _repositoryOfBattleBet = kernel.Get<IRepository<BattleBet>>();

            _usersService = kernel.Get<IUsersService>();
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
            _battlesService.CreateBattle(_battleStartDate, _battleEndDate, _battleType, _battleBudget);

            AssertBattle(_battleStartDate, _battleEndDate, _battleType, _battleBudget);
        }

        private void SetupBattleAndUserAndTeam(out Battle battle, out Team team, out User user)
        {
            battle = _creator.CreateBattle();
            user = _creator.CreateUser();
            team = _creator.CreateTeam(user);
        }

        [Test]
        public void MakeBet()
        {
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);
            
            var battleBetId = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _bet, _betCoefficient, _betUrl, _betIsPrivate);

            AssertOpenedBattleBet(battleBetId, battle.Id, team.Id, user.Id, _betTitle, _bet, _betCoefficient, _betUrl, _betIsPrivate);
            AssertQueuedBetUrl(battleBetId, QueuedBetUrlType.Open, _betUrl);

            AssertBattleTeamStatistics(battle.Id, team.Id, battle.Budget - _bet, 1, 0);
        }

        [Test]
        public void MakeBet_CallTwice_BattleTeamStatisticsIsNotDuplicated()
        {
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);

            var battleBetId1 = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _bet, _betCoefficient, _betUrl, _betIsPrivate);
            var battleBetId2 = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _bet, _betCoefficient, _betUrl, _betIsPrivate);

            AssertOpenedBattleBet(battleBetId1, battle.Id, team.Id, user.Id, _betTitle, _bet, _betCoefficient, _betUrl, _betIsPrivate);
            AssertQueuedBetUrl(battleBetId1, QueuedBetUrlType.Open, _betUrl);

            AssertOpenedBattleBet(battleBetId2, battle.Id, team.Id, user.Id, _betTitle, _bet, _betCoefficient, _betUrl, _betIsPrivate);
            AssertQueuedBetUrl(battleBetId2, QueuedBetUrlType.Open, _betUrl);

            AssertBattleTeamStatistics(battle.Id, team.Id, battle.Budget - _bet * 2, 2, 0);
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
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);

            if (!userId.HasValue)
                userId = user.Id;

            var battleBetId = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _bet, _betCoefficient, _betUrl, _betIsPrivate);
            long battleId;
            if (success)
                _battlesService.BetSucceeded(battleBetId, userId.Value, out battleId);
            else
                _battlesService.BetFailed(battleBetId, userId.Value, out battleId);

            var newRating = 0d;
            var newBalance = battle.Budget - _bet;
            if (success)
            {
                newRating += _bet * _betCoefficient;
                newBalance += _bet * _betCoefficient;
            }
            else
                newRating -= _bet;

            AssertTeam(team.Id, newRating);
            AssertClosedBattleBet(battleBetId, success);
            AssertBattleTeamStatistics(battle.Id, team.Id, newBalance, 0, 1);
        }

        private void AssertOpenedBattleBet(long battleBetId, long battleId, long teamId, long userId, string _betTitle, double bet, double coefficient, string url, bool isPrivate)
        {
            _repositoryOfBattleBet.All().Where(bb => bb.Id == battleBetId && bb.BattleId == battleId && bb.TeamId == teamId && bb.UserId == userId && bb.Title == _betTitle && bb.Bet == bet && bb.Coefficient == coefficient && bb.Url == url && bb.IsPrivate == isPrivate).Single();
        }

        private void AssertClosedBattleBet(long battleBetId, bool success)
        {
            _repositoryOfBattleBet.All().Where(bb => bb.Id == battleBetId && bb.CloseDateTime != null && bb.Success == success).Single();
        }

        private void AssertQueuedBetUrl(long battleBetId, QueuedBetUrlType type, string url)
        {
            _repositoryOfQueuedBetUrl.All().Where(qbu => qbu.BattleBetId == battleBetId && qbu.Type == (sbyte)type && qbu.Url == url).Single();
        }

        private void AssertBattleTeamStatistics(long battleId, long teamId, double balance, int openedBetsCount, int closedBetsCount)
        {
            _repositoryOfBattleTeamStatistics.All().Where(bts => bts.BattleId == battleId && bts.TeamId == teamId && bts.Balance == balance && bts.OpenedBetsCount == openedBetsCount && bts.ClosedBetsCount == closedBetsCount).Single();
        }

        private void AssertBattle(DateTime startDate, DateTime endDate, BattleType battleType, double budget)
        {
            _repositoryOfBattle.All().Where(b => b.StartDate == startDate && b.EndDate == endDate && b.BattleType == (sbyte)battleType && b.Budget == budget).Single();
        }

        private void AssertTeam(long teamId, double rating)
        {
            _repositoryOfTeam.All().Where(t => t.Id == teamId && t.Rating == rating).Single();
        }
    }
}