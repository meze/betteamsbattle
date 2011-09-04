using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers
{
    public partial class UsersController : Controller
    {
        public virtual ActionResult User(long userId)
        {
            

            return View();
        }
    }
}
