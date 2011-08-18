using System;
using System.Linq;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Localizers.InDays.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battle;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;
using BetTeamsBattle.Frontend.Authentication;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers
{
    public partial class BattlesController : Controller
    {
        private readonly IRepository<Battle> _repositoryOfBattle;
        private readonly IBattlesService _battlesService;
        private readonly IBattlesViewService _battlesViewService;
        private readonly IInDaysLocalizer _inDaysLocalizer;

        public BattlesController(IRepository<Battle> repositoryOfBattle, IBattlesService battlesService, IBattlesViewService battlesViewService, IInDaysLocalizer inDaysLocalizer)
        {
            _repositoryOfBattle = repositoryOfBattle;
            _battlesService = battlesService;
            _battlesViewService = battlesViewService;
            _inDaysLocalizer = inDaysLocalizer;
        }

        public virtual ActionResult Battle(long battleId)
        {


            return View();
        }

        [ChildActionOnly]
        public virtual PartialViewResult AllBattles()
        {
            var allBattlesViewModel = _battlesViewService.AllBattles();

            return PartialView(allBattlesViewModel);
        }

        [HttpGet]
        public virtual ActionResult ActualBattles()
        {
            var actualBattlesViewModels = _battlesViewService.ActualBattlesViewModels(CurrentUser.NullableUserId);

            return View(actualBattlesViewModels);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult JoinBattle(long battleId)
        {
            _battlesService.JoinToBattle(battleId, CurrentUser.UserId);

            return RedirectToAction(Actions.ActualBattles());
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult LeaveBattle(long battleId)
        {
            _battlesService.LeaveBattle(battleId, CurrentUser.UserId);

            return RedirectToAction(Actions.ActualBattles());
        }
    }
}
