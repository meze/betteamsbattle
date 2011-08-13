using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Specific.BattleUser;
using BetTeamsBattle.Data.Repositories.Specific.BattleUser.Interfaces;
using BetTeamsBattle.Data.Repositories.UnitOfWork;
using NUnit.Framework;
using BetTeamsBattle.Data.Repositories.Specific;

namespace Data.Repositories.Tests.Specific
{
    [TestFixture]
    internal class BattleUserRepositoryTests
    {
        private TransactionScope _transactionScope;
        private IBattleUserRepository _battleUserRepository;
        private Battle _battle1, _battle2;
        private User _user1, _user2, _user3;
        private BattleUser _battleUser1, _battleUser2, _battleUser3, _battleUser4;

        [SetUp]
        public void Setup()
        {
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

            _transactionScope = new TransactionScope();

            var unitOfWorkScope = new UnitOfWorkScope();

            _battleUserRepository = new BattleUserRepository();

            var repositoryOfBattle = new Repository<Battle>();
            _battle1 = new Battle(DateTime.UtcNow, DateTime.UtcNow, BattleType.FixedBudget, 10000);
            repositoryOfBattle.Add(_battle1);
            _battle2 = new Battle(DateTime.UtcNow, DateTime.UtcNow, BattleType.FixedBudget, 10000);
            repositoryOfBattle.Add(_battle2);

            var repositoryOfUser = new Repository<User>();
            _user1 = new User("login", "openIdUrl");
            repositoryOfUser.Add(_user1);
            _user2 = new User("login2", "openIdUrl2");
            repositoryOfUser.Add(_user2);
            _user3 = new User("login3", "openIdUrl3");
            repositoryOfUser.Add(_user3);

            unitOfWorkScope.SaveChanges();

            _battleUser1 = new BattleUser(_battle1.Id, _user1.Id, BattleUserAction.Join) { DateTime = DateTime.Now };
            _battleUserRepository.Add(_battleUser1);
            _battleUser2 = new BattleUser(_battle1.Id, _user1.Id, BattleUserAction.Leave) { DateTime = DateTime.Now.AddSeconds(5) };
            _battleUserRepository.Add(_battleUser2);
            _battleUser3 = new BattleUser(_battle1.Id, _user2.Id, BattleUserAction.Join) { DateTime = DateTime.Now.AddSeconds(10) };
            _battleUserRepository.Add(_battleUser3);

            _battleUser4 = new BattleUser(_battle2.Id, _user1.Id, BattleUserAction.Join) { DateTime = DateTime.Now };
            _battleUserRepository.Add(_battleUser4);

            unitOfWorkScope.SaveChanges();
        }

        [Test]
        public void GetLastBattleUsers_NoBattleUsers_EmptyList()
        {
            var battleUsers = _battleUserRepository.GetLastBattleUsers(_user3.Id, new List<long>() { _battle1.Id }).ToList();

            Assert.AreEqual(0, battleUsers.Count);
        }

        [Test]
        public void GetLastBattleUsers_OneBattleUser_ItIsChosen()
        {
            var battleUsers = _battleUserRepository.GetLastBattleUsers(_user2.Id, new List<long>() { _battle1.Id }).ToList();

            Assert.AreEqual(1, battleUsers.Count);
            Assert.AreEqual(_battleUser3.Id, battleUsers[0].Id);
        }

        [Test]
        public void GetLastBattleUsers_SeveralBattleUsers_LastIsChosen()
        {
            var battleUsers = _battleUserRepository.GetLastBattleUsers(_user1.Id, new List<long> {_battle1.Id}).ToList();

            Assert.AreEqual(1, battleUsers.Count);
            Assert.AreEqual(_battleUser2.Id, battleUsers[0].Id);
        }

        [Test]
        public void GetLastBattleUsers_SeveralBattles_LastForEachBattleIsChosen()
        {
            var battleUsers = _battleUserRepository.GetLastBattleUsers(_user1.Id, new List<long> { _battle1.Id, _battle2.Id }).ToList();

            CollectionAssert.AreEquivalent(new List<long>() {_battleUser2.Id, _battleUser4.Id},
                                           battleUsers.Select(bu => bu.Id).ToList());
        }

        [TearDown]
        public void TearDown()
        {
            _transactionScope.Dispose();
        }
    }
}