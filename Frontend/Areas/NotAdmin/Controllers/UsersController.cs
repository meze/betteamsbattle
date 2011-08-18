using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers
{
    public partial class UsersController : Controller
    {
        private readonly IUsersViewService _usersViewService;

        public UsersController(IUsersViewService usersViewService)
        {
            _usersViewService = usersViewService;
        }

        [ChildActionOnly]
        public virtual PartialViewResult UsersRating()
        {
            var usersRatingUsersViewModels = _usersViewService.UsersRating();

            return PartialView(usersRatingUsersViewModels);
        }

    }
}
