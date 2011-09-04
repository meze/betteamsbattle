using System.Linq;
using System.Transactions;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Data.Services.Tests.DI;
using BetTeamsBattle.Data.Services.Tests.Helpers;
using NUnit.Framework;
using Ninject;

namespace BetTeamsBattle.Data.Services.Tests
{
    [TestFixture]
    internal class UsersServiceTests
    {
        private TransactionScope _transactionScope;
        private EntityAssert _entityAssert;

        private IUsersService _usersService;

        [SetUp]
        public void Setup()
        {
            _transactionScope = new TransactionScope();

            var kernel = TestNinjectKernel.Kernel;
            _entityAssert = kernel.Get<EntityAssert>();

            _usersService = kernel.Get<IUsersService>();
        }

        [TearDown]
        public void TearDown()
        {
            _transactionScope.Dispose();
        }

        [Test]
        public void Register()
        {
            const string login = "login";
            const string openIdUrl = "openIdUrl";
            const Language language = Language.English;

            _usersService.Register(login, openIdUrl, language);

            _entityAssert.UserAndPersonalTeam(login, openIdUrl, language);
        }
    }
}