using System;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.UnitOfWork;
using BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces;
using BetTeamsBattle.Data.Services.Interfaces;

namespace BetTeamsBattle.Data.Services
{
    internal class UsersService : IUsersService
    {
        private readonly IUnitOfWorkScopeFactory _unitOfWorkScopeFactory;
        private readonly IRepository<User> _repositoryOfUser;
        private readonly IRepository<Team> _repositoryOfTeam;

        public UsersService(IUnitOfWorkScopeFactory unitOfWorkScopeFactory, IRepository<User> repositoryOfUser, IRepository<Team> repositoryOfTeam)
        {
            _unitOfWorkScopeFactory = unitOfWorkScopeFactory;
            _repositoryOfUser = repositoryOfUser;
            _repositoryOfTeam = repositoryOfTeam;
        }

        public long Register(string login, string openIdUrl, Language language)
        {
            using (var unitOfWorkScope = _unitOfWorkScopeFactory.Create())
            {
                var user = new User(login, openIdUrl);
                user.UserProfile = new UserProfile() {LanguageEnum = language};
                _repositoryOfUser.Add(user);

                var personalTeam = Team.CreatePersonalTeam(login, user);
                _repositoryOfTeam.Add(personalTeam);

                unitOfWorkScope.SaveChanges();

                return user.Id;
            }
        }
    }
}