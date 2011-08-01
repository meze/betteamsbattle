using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
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
            var user = new User(login, openIdUrl);
            user.Profile = new UserProfile() { LanguageEnum = language };

            _repositoryOfUser.Add(user);

            _repositoryOfUser.SaveChanges();
        }
    }
}