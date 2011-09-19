using System.Transactions;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
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
        private double _amount = 100;
        private double _betCoefficient = 2.5;
        private string _betUrl = "http://url";
        private bool _betIsPrivate = true;

        private TransactionScope _transactionScope;
        private Creator _creator;
        private EntityAssert _entityAssert;

        private IBattlesService _battlesService;

        [SetUp]
        public void Setup()
        {
            _transactionScope = new TransactionScope();

            var kernel = TestNinjectKernel.Kernel;
            kernel.Get<IUnitOfWork>();
            _creator = kernel.Get<Creator>();
            _entityAssert = kernel.Get<EntityAssert>();

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

            _entityAssert.Battle(_battleStartDate, _battleEndDate, _battleType, _battleBudget);
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

            var battleBetId = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _amount, _betCoefficient, _betUrl, _betIsPrivate);

            _entityAssert.OpenedBet(battleBetId, battle.Id, team.Id, user.Id, _betTitle, _amount, _betCoefficient, _betUrl, _betIsPrivate, -_amount, -_amount, 1, 0);
        }

        [Test]
        public void MakeBet_CallTwice_BattleTeamStatisticsIsNotDuplicated()
        {
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);

            var battleBetId1 = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _amount, _betCoefficient, _betUrl, _betIsPrivate);
            var battleBetId2 = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _amount, _betCoefficient, _betUrl, _betIsPrivate);

            _entityAssert.OpenedBet(battleBetId2, battle.Id, team.Id, user.Id, _betTitle, _amount, _betCoefficient, _betUrl, _betIsPrivate, -_amount * 2, -_amount * 2, 2, 0);
        }

        [Test]
        public void CloseBet_Succeeded()
        {
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);

            var battleBetId = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _amount, _betCoefficient, _betUrl, _betIsPrivate);
            long battleId;
            _battlesService.BetSucceeded(battleBetId, user.Id, out battleId);

            _entityAssert.ClosedBet(battleBetId, BetStatus.Succeeded, -_amount + _amount * _betCoefficient, -_amount + _amount * _betCoefficient, -_amount + _amount * _betCoefficient, 0, 1);
        }

        [Test]
        public void CloseBet_Failed()
        {
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);

            var battleBetId = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _amount, _betCoefficient, _betUrl, _betIsPrivate);
            long battleId;
            _battlesService.BetFailed(battleBetId, user.Id, out battleId);

            _entityAssert.ClosedBet(battleBetId, BetStatus.Failed, -_amount, -_amount, -_amount, 0, 1);
        }

        [Test]
        public void CloseBet_CanceledByBookmaker()
        {
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);

            var battleBetId = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _amount, _betCoefficient, _betUrl, _betIsPrivate);
            long battleId;
            _battlesService.BetCanceledByBookmaker(battleBetId, user.Id, out battleId);

            _entityAssert.ClosedBet(battleBetId, BetStatus.CanceledByBookmaker, 0, 0, 0, 0, 1);
        }

        [Test]
        public void CloseBet_NotMineBattleBet_Exception()
        {
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);

            var otherUser = _creator.CreateUser("login1", "openIdUrl1");

            var battleBetId = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _amount, _betCoefficient, _betUrl, _betIsPrivate);
            long battleId;
            Assert.Throws<ArgumentException>(() => _battlesService.BetFailed(battleBetId, otherUser.Id, out battleId));
        }

        [Test]
        public void CloseBet_CloseAlreadyClosedBet_Exception()
        {
            Battle battle;
            Team team;
            User user;
            SetupBattleAndUserAndTeam(out battle, out team, out user);

            var battleBetId = _battlesService.MakeBet(battle.Id, team.Id, user.Id, _betTitle, _amount, _betCoefficient, _betUrl, _betIsPrivate);
            long battleId;
            _battlesService.BetFailed(battleBetId, user.Id, out battleId);
            Assert.Throws<ArgumentException>(() => _battlesService.BetFailed(battleBetId, user.Id, out battleId));
        }
    }
}