using System;
using System.Linq;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battle;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;
using BetTeamsBattle.Frontend.Authentication;
using BetTeamsBattle.Frontend.Localization.Metadata.Localizers.InDays.Interfaces;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers
{
    public partial class BattlesController : Controller
    {
        private readonly IRepository<Battle> _repositoryOfBattle;
        private readonly IBattlesService _battlesService;
        private readonly IBattlesViewService _battlesViewService;
        private readonly IInDaysLocalizer _inDaysLocalizer;

        public BattlesController(IRepository<Battle> repositoryOfBattle, IBattlesService battlesService, IBattlesViewService battlesViewService)
        {
            _repositoryOfBattle = repositoryOfBattle;
            _battlesService = battlesService;
            _battlesViewService = battlesViewService;
        }

        [ChildActionOnly]
        public virtual ActionResult NextBattleStartsIn()
        {
            //ToDo: Order by here is part of query logic. May be put it in Repository/BattlesRepository?
            var nextBattleDate = _repositoryOfBattle.Get(BattleSpecifications.StartDateIsInFuture()).OrderBy(b => b.StartDate).Select(b => b.StartDate).FirstOrDefault();
            if (nextBattleDate == default(DateTime))
                return new EmptyResult();

            var inDays = (nextBattleDate - DateTime.Now).Days;
            var inDaysCaption = _inDaysLocalizer.Localize(inDays);
            var inDaysViewModel = new NextBattleStartsInViewModel(inDays, inDaysCaption);

            return View(MVC.NotAdmin.Battles.Views.NextBattleStartsIn, inDaysViewModel);
        }

        public virtual ActionResult ActualBattles()
        {
            var actualBattlesViewModels = _battlesViewService.ActualBattlesViewModels(CurrentUser.UserId);

            return View(actualBattlesViewModels);
        }

        [HttpGet]
        public virtual JsonResult JoinBattle(long battleId)
        {
            _battlesService.JoinToBattle(battleId, CurrentUser.UserId);

            return Json(null);
        }

        [HttpGet]
        public virtual JsonResult LeaveBattle(long battleId)
        {
            _battlesService.LeaveBattle(battleId, CurrentUser.UserId);

            return Json(null);
        }
    }
}
