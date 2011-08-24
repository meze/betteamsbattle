using System.Linq;
using System.Transactions;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Data.Services.Tests.DI;
using NUnit.Framework;
using Ninject;

namespace BetTeamsBattle.Data.Services.Tests
{
    [TestFixture]
    internal class UsersServiceTests
    {
        private TransactionScope _transactionScope;

        private IUsersService _usersService;
        private IRepository<User> _repositoryOfUser;
        private IRepository<Team> _repositoryOfTeam;
        private IRepository<TeamUser> _repositoryOfTeamUser;

        [SetUp]
        public void Setup()
        {
            _transactionScope = new TransactionScope();

            var kernel = TestNinjectKernel.Kernel;
            _usersService = kernel.Get<IUsersService>();
            _repositoryOfUser = kernel.Get<IRepository<User>>();
            _repositoryOfTeam = kernel.Get<IRepository<Team>>();
            _repositoryOfTeamUser = kernel.Get<IRepository<TeamUser>>();
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

            var userId = _usersService.Register(login, openIdUrl, language);

            _repositoryOfUser.All().Where(u => u.Login == login && u.OpenIdUrl == openIdUrl && u.UserProfile.Language == (sbyte)language).Single();
            _repositoryOfTeam.All().Where(t => t.Title == login && t.IsPersonal && t.Rating == 0 && t.TeamUsers.Count() == 1 && t.TeamUsers.Any(tu => tu.UserId == userId)).Single();
        }
    }
}