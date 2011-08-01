using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
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
            var battleViewModel = new BattleViewModel();

            return View(battleViewModel);
        }

        [HttpPost]
        public virtual ActionResult CreateBattle(BattleViewModel battleViewModel)
        {
            if (!ModelState.IsValid)
                return View(battleViewModel);

            _battlesService.Create(battleViewModel.StartDate, battleViewModel.EndDate, battleViewModel.BattleType, battleViewModel.Budget);

            return RedirectToAction(MVC.Admin.AdminBattles.GetBattles());
        }

        public virtual ActionResult GetBattles()
        {
            var battles = _repositoryOfBattles.GetAll().ToList();

            return View(battles);
        }
    }
}
