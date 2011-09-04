using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Frontend.Areas.Admin.Models;
using BetTeamsBattle.Frontend.Areas.Admin.Models.Battles;

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

        public virtual ActionResult GetBattles()
        {
            var battles = _repositoryOfBattles.All().ToList();

            return View(battles);
        }

        [HttpGet]
        public virtual ActionResult CreateBattle()
        {
            var battleViewModel = new CreateBattleViewModel();

            return View(battleViewModel);
        }

        [HttpPost]
        public virtual ActionResult CreateBattle(CreateBattleFormViewModel createBattleForm)
        {
            if (!ModelState.IsValid)
                return View(new CreateBattleViewModel(createBattleForm));

            _battlesService.CreateBattle(createBattleForm.StartDate, createBattleForm.EndDate, createBattleForm.BattleType, createBattleForm.Budget);

            return RedirectToAction(MVC.Admin.AdminBattles.GetBattles());
        }
    }
}
