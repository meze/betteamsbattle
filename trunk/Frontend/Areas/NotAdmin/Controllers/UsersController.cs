using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers
{
    public partial class UsersController : Controller
    {
        private readonly IRepository<User> _repositoryOfUser;

        public UsersController(IRepository<User> repositoryOfUser)
        {
            _repositoryOfUser = repositoryOfUser;
        }

        public virtual ActionResult GetUser(long userId)
        {
            var user = _repositoryOfUser.Get(EntitySpecifications.IdIsEqualTo<User>(userId)).Single();

            return View(user);
        }
    }
}
