using System.Collections.Generic;
using System.Transactions;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Data.Services.Tests.DI;
using BetTeamsBattle.Data.Services.Tests.Helpers;
using NUnit.Framework;
using Ninject;

namespace BetTeamsBattle.Data.Services.Tests
{
    [TestFixture]
    internal class TeamsServiceTests
    {
        private TransactionScope _transactionScope;
        private EntityAssert _entityAssert;

        private IUsersService _usersService;
        private ITeamsService _teamsService;

        [SetUp]
        public void Setup()
        {
            _transactionScope = new TransactionScope();

            var kernel = TestNinjectKernel.Kernel;
            _entityAssert = kernel.Get<EntityAssert>();

            _usersService = kernel.Get<IUsersService>();
            _teamsService = kernel.Get<ITeamsService>();
        }

        [TearDown]
        public void TearDown()
        {
            _transactionScope.Dispose();
        }

        [Test]
        public void CreateProTeam()
        {
            const string title = "title";
            const string description = "description";
            const string site = "http://url";
            var user1Id = _usersService.Register("login1", "openIdUrl1", Language.English);
            var user2Id = _usersService.Register("login2", "openIdUrl2", Language.English);
            var usersIds = new List<long> {user1Id, user2Id};

            _teamsService.CreateProTeam(title, description, site, usersIds);

            _entityAssert.ProTeam(title, description, site, usersIds);
        }
    }
}