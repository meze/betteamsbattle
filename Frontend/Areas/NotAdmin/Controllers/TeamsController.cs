﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Teams;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers
{
    public partial class TeamsController : Controller
    {
        private readonly ITeamsViewService _teamsViewService;
        private readonly IRepository<Team> _repositoryOfTeam;

        public TeamsController(ITeamsViewService teamsViewService, IRepository<Team> repositoryOfTeam)
        {
            _teamsViewService = teamsViewService;
            _repositoryOfTeam = repositoryOfTeam;
        }

        public virtual ActionResult GetTeam(long teamId)
        {
            var team = _teamsViewService.GetTeam(teamId);

            return View(team);
        }

        public virtual ActionResult GetPersonalTeam(long userId)
        {
            var personalTeam = _teamsViewService.GetPersonalTeam(userId);

            return View(personalTeam);
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
