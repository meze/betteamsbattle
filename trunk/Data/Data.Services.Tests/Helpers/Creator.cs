using System;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.UnitOfWork.Interfaces;

namespace BetTeamsBattle.Data.Services.Tests.Helpers
{
    public class Creator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Battle> _repositoryOfBattle;
        private IRepository<User> _repositoryOfUser;
        private readonly IRepository<Team> _repositoryOfTeam;

        public Creator(IUnitOfWork unitOfWork, IRepository<Battle> repositoryOfBattle, IRepository<User> repositoryOfUser, IRepository<Team> repositoryOfTeam)
        {
            _unitOfWork = unitOfWork;
            _repositoryOfBattle = repositoryOfBattle;
            _repositoryOfUser = repositoryOfUser;
            _repositoryOfTeam = repositoryOfTeam;
        }

        public Battle CreateBattle()
        {
            var battle = new Battle(DateTime.UtcNow, DateTime.UtcNow, BattleType.FixedBudget, 10000);
            _repositoryOfBattle.Add(battle);
            _unitOfWork.SaveChanges();
            return battle;
        }

        public User CreateUser(string login, string openIdUrl)
        {
            var user = new User(login, openIdUrl);
            //user.UserStatistics = new UserStatistics() { Rating = 0 };
            _repositoryOfUser.Add(user);
            _unitOfWork.SaveChanges();
            return user;
        }

        public User CreateUser()
        {
            return CreateUser("login", "openIdUrl");
        }

        public Team CreateTeam(User user)
        {
            var team = Team.CreatePersonalTeam("PersonalTeam", user);

            _repositoryOfTeam.Add(team);

            _unitOfWork.SaveChanges();

            return team;
        }
    }
}