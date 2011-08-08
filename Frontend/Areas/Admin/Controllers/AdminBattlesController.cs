using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Frontend.Areas.Admin.Models;

namespace BetTeamsBattle.Frontend.Areas.Admin.Controllers
{
    public partial class AdminBattlesController : Controller
    {
        private readonly IRepository<Battle> _repositoryOfBattles;
        private readonly IBattlesService _battlesService;

        public AdminBattlesController(IRepository<Battle> repositoryOfBattles, IBattlesService battlesService)
        {
            _repositoryOfBattles = repositoryOfBattles;
            _battlesService = battlesService;
        }

        [HttpGet]
        public virtual ActionResult CreateBattle()
        {
            var battleViewModel = new CreateBattleViewModel();

            return View(battleViewModel);
        }

        [HttpPost]
        public virtual ActionResult CreateBattle(CreateBattleViewModel createBattleViewModel)
        {
            if (!ModelState.IsValid)
                return View(createBattleViewModel);

            _battlesService.CreateBattle(createBattleViewModel.StartDate, createBattleViewModel.EndDate, createBattleViewModel.BattleType, createBattleViewModel.Budget);

            return RedirectToAction(MVC.Admin.AdminBattles.GetBattles());
        }

        public virtual ActionResult GetBattles()
        {
            var battles = _repositoryOfBattles.Get(BooleanSpecifications<Battle>.True()).ToList();

            return View(battles);
        }
    }
}
