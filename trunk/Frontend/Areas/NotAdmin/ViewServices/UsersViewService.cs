using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Users;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles
{
    internal class UsersViewService : IUsersViewService
    {
        private readonly IRepository<User> _repositoryOfUser;

        public UsersViewService(IRepository<User> repositoryOfUser)
        {
            _repositoryOfUser = repositoryOfUser;
        }

        public IEnumerable<UsersRatingUserViewModel> UsersRating()
        {
            var users = _repositoryOfUser.All().Include(u => u.UserStatistics).OrderByDescending(u => u.UserStatistics.Rating).Skip(0).Take(10).ToList();

            var usersRatingsUsersViewModels = new List<UsersRatingUserViewModel>();
            foreach (var user in users)
            {
                var usersRatingUserViewModel = new UsersRatingUserViewModel(user.Id, user.Login, user.UserStatistics.Rating);
                usersRatingsUsersViewModels.Add(usersRatingUserViewModel);
            }
            return usersRatingsUsersViewModels;
        }
    }
}