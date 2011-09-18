using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Frontend.Areas.Admin.Models.Teams;
using BetTeamsBattle.Frontend.Authentication;

namespace BetTeamsBattle.Frontend.Areas.Admin.Controllers
{
    public partial class AdminTeamsController : Controller
    {
        private IRepository<Team> _repositoryOfTeam;
        private ITeamsService _teamsService;

        public AdminTeamsController(IRepository<Team> repositoryOfTeam, ITeamsService teamsService)
        {
            _repositoryOfTeam = repositoryOfTeam;
            _teamsService = teamsService;
        }

        public virtual ActionResult GetProTeams()
        {
            var proTeams = _repositoryOfTeam.Get(TeamSpecifications.IsPro()).Include(t => t.TeamUsers).OrderBy(t => t.Title).ToList();

            return View(proTeams);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult CreateProTeam()
        {
            var createProTeamViewModel = new CreateProTeamViewModel();

            return View(createProTeamViewModel);
        }
        
        [HttpPost]
        [Authorize]
        public virtual ActionResult CreateProTeam(CreateProTeamViewModel createProTeamViewModel)
        {
            if (!ModelState.IsValid)
                return View(createProTeamViewModel);

            _teamsService.CreateProTeam(createProTeamViewModel.Title, createProTeamViewModel.Description, createProTeamViewModel.Url, createProTeamViewModel.UsersIds.Where(ui => ui.HasValue).Select(ui => ui.Value).ToList());

            return RedirectToAction(MVC.Admin.AdminTeams.GetProTeams());
        }
    }
}
