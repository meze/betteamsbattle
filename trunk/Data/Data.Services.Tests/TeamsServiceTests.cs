using System.Transactions;
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

        private ITeamsService _teamsService;

        [SetUp]
        public void Setup()
        {
            _transactionScope = new TransactionScope();

            var kernel = TestNinjectKernel.Kernel;
            _entityAssert = kernel.Get<EntityAssert>();

            _teamsService = kernel.Get<ITeamsService>();
        }

        [Test]
        public void CreateProTeam()
        {
            const string title = "title";
            const string description = "description";
            const string site = "http://url";

            _teamsService.CreateProTeam(title, description, site);

            _entityAssert.ProTeam(title, description, site);
        }
    }
}