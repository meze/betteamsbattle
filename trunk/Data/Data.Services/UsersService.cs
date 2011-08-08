using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.UnitOfWork;
using BetTeamsBattle.Data.Services.Interfaces;

namespace BetTeamsBattle.Data.Services
{
    internal class UsersService : IUsersService
    {
        private readonly IRepository<User> _repositoryOfUser;

        public UsersService(IRepository<User> repositoryOfUser)
        {
            _repositoryOfUser = repositoryOfUser;
        }

        public void Register(string login, string openIdUrl, Language language)
        {
            using (var contextScope = new UnitOfWorkScope())
            {
                var user = new User(login, openIdUrl);
                user.UserProfile = new UserProfile() {LanguageEnum = language};

                _repositoryOfUser.Add(user);

                contextScope.SaveChanges();
            }
        }
    }
}