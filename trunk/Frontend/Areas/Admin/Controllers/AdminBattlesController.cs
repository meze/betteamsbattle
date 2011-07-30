using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Frontend.Areas.Admin.Models;

namespace BetTeamsBattle.Frontend.Areas.Admin.Controllers
{
    public partial class AdminBattlesController : Controller
    {
        private readonly IRepository<Battle> _repositoryOfBattles;

        public AdminBattlesController(IRepository<Battle> repositoryOfBattles)
        {
            _repositoryOfBattles = repositoryOfBattles;
        }

        [HttpGet]
        public virtual ActionResult CreateBattle()
        {
            var fixedBudgetBattleType = BattleType.FixedBudget;
            var battleViewModel = new BattleViewModel();

            return View(battleViewModel);
        }

        [HttpPost]
        public virtual ActionResult CreateBattle(BattleViewModel battleViewModel)
        {
            if (!ModelState.IsValid)
                return View(battleViewModel);

            var battle = new Battle()
                             {
                                 StartDate = battleViewModel.StartDate,
                                 EndDate = battleViewModel.EndDate,
                                 BattleTypeEnum = battleViewModel.BattleType,
                                 Budget = battleViewModel.Budget
                             };
            _repositoryOfBattles.Add(battle);
            _repositoryOfBattles.SaveChanges();

            return RedirectToAction(MVC.Admin.AdminBattles.GetBattles());
        }

        public virtual ActionResult GetBattles()
        {
            var battles = _repositoryOfBattles.All().ToList();

            return View(battles);
        }
    }
}
