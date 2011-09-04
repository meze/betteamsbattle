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
    public partial class TeamsController : Controller
    {
        private readonly ITeamsViewService _teamsViewService;

        public TeamsController(ITeamsViewService teamsViewService)
        {
            _teamsViewService = teamsViewService;
        }

        public virtual ActionResult Team(long teamId)
        {


            return View();
        }

        [ChildActionOnly]
        public virtual PartialViewResult TopTeams()
        {
            var topTeams = _teamsViewService.TopTeams();

            return PartialView(topTeams);
        }

        [ChildActionOnly]
        public virtual ActionResult BattleTopTeams(long battleId)
        {
            var battleTopTeams = _teamsViewService.BattleTopTeams(battleId);

            return PartialView(battleTopTeams);
        }
    }
}
