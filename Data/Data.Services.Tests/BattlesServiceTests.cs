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
    public class BattlesServiceTests
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
        private IRepository<BetScreenshot> _repositoryOfBetScreenshot;
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
            _repositoryOfBetScreenshot = kernel.Get<IRepository<BetScreenshot>>();
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

            AssertOpenedBattleBet(battleBetId2, battle.Id, team.Id, user.Id, _betTitle, _bet, _betCoefficient, _betUrl, _betIsPrivate);

            AssertBattleTeamStatistics(battle.Id, team.Id, battle.Budget - _bet * 2, 2, 0);
        }

        [Test]
        public void CloseBattleBet_Succeeded()
        {
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);

            var battleBetId = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _bet, _betCoefficient, _betUrl, _betIsPrivate);
            long battleId;
            _battlesService.BetSucceeded(battleBetId, user.Id, out battleId);

            var newRating = _bet * _betCoefficient;
            var newBalance = battle.Budget - _bet + _bet * _betCoefficient;

            AssertTeam(team.Id, newRating);
            AssertClosedBattleBet(battleBetId, BattleBetStatus.Succeeded);
            AssertBattleTeamStatistics(battle.Id, team.Id, newBalance, 0, 1);
        }

        [Test]
        public void CloseBattleBet_Failed()
        {
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);

            var battleBetId = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _bet, _betCoefficient, _betUrl, _betIsPrivate);
            long battleId;
            _battlesService.BetFailed(battleBetId, user.Id, out battleId);

            var newRating = -_bet;
            var newBalance = battle.Budget - _bet;

            AssertTeam(team.Id, newRating);
            AssertClosedBattleBet(battleBetId, BattleBetStatus.Failed);
            AssertBattleTeamStatistics(battle.Id, team.Id, newBalance, 0, 1);
        }

        [Test]
        public void CloseBattleBet_CanceledByBookmaker()
        {
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);

            var battleBetId = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _bet, _betCoefficient, _betUrl, _betIsPrivate);
            long battleId;
            _battlesService.BetCanceledByBookmaker(battleBetId, user.Id, out battleId);

            var newRating = 0;
            var newBalance = battle.Budget;

            AssertTeam(team.Id, newRating);
            AssertClosedBattleBet(battleBetId, BattleBetStatus.CanceledByBookmaker);
            AssertBattleTeamStatistics(battle.Id, team.Id, newBalance, 0, 1);
        }

        [Test]
        public void CloseBattleBet_NotMineBattleBet_Exception()
        {
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);

            var otherUser = _creator.CreateUser("login1", "openIdUrl1");

            var battleBetId = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _bet, _betCoefficient, _betUrl, _betIsPrivate);
            long battleId;
            Assert.Throws<ArgumentException>(() => _battlesService.BetFailed(battleBetId, otherUser.Id, out battleId));
        }

        [Test]
        public void CloseBattleBet_CloseAlreadyClosedBattleBet_Exception()
        {
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);

            var battleBetId = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _bet, _betCoefficient, _betUrl, _betIsPrivate);
            long battleId;
            _battlesService.BetFailed(battleBetId, user.Id, out battleId);
            Assert.Throws<ArgumentException>(() => _battlesService.BetFailed(battleBetId, user.Id, out battleId));
        }

        private void AssertOpenedBattleBet(long battleBetId, long battleId, long teamId, long userId, string _betTitle, double bet, double coefficient, string url, bool isPrivate)
        {
            _repositoryOfBattleBet.All().Where(bb => bb.Id == battleBetId && bb.BattleId == battleId && bb.TeamId == teamId && bb.UserId == userId && bb.Title == _betTitle && bb.Bet == bet && bb.Coefficient == coefficient && bb.Url == url && bb.IsPrivate == isPrivate && bb.OpenBetScreenshot.Status == (sbyte)BetScreenshotStatus.NotProcessed).Single();
        }

        private void AssertClosedBattleBet(long battleBetId, BattleBetStatus status)
        {
            _repositoryOfBattleBet.All().Where(bb => bb.Id == battleBetId && bb.CloseDateTime != null && bb.CloseBetScreenshotId != null && bb.Status == (sbyte)status).Single();
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