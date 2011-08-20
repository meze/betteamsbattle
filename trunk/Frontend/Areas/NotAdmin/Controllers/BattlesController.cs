using System;
using System.Data.Entity;
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
        private readonly IRepository<BattleUserStatistics> _repositoryOfBattleUserStatistics;
        private readonly IBattlesService _battlesService;
        private readonly IBattlesViewService _battlesViewService;
        private readonly IInDaysLocalizer _inDaysLocalizer;

        public BattlesController(IRepository<Battle> repositoryOfBattle, IRepository<BattleUserStatistics> repositoryOfBattleUserStatistics, IBattlesService battlesService, IBattlesViewService battlesViewService, IInDaysLocalizer inDaysLocalizer)
        {
            _repositoryOfBattle = repositoryOfBattle;
            _repositoryOfBattleUserStatistics = repositoryOfBattleUserStatistics;
            _battlesService = battlesService;
            _battlesViewService = battlesViewService;
            _inDaysLocalizer = inDaysLocalizer;
        }

        [ChildActionOnly]
        public virtual PartialViewResult AllBattles()
        {
            var allBattlesViewModel = _battlesViewService.AllBattles();

            return PartialView(allBattlesViewModel);
        }

        [HttpGet]
        public virtual ActionResult Battle(long battleId)
        {
            var battleViewModel = _battlesViewService.Battle(battleId, CurrentUser.NullableUserId);

            return View(battleViewModel);
        }

        [ChildActionOnly]
        public virtual ActionResult BattleTopUsers(long battleId)
        {
            var battleTopUsers = _battlesViewService.BattleTopUsers(battleId);

            return PartialView(battleTopUsers);
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult JoinBattle(long battleId)
        {
            _battlesService.JoinToBattle(battleId, CurrentUser.UserId);

            return RedirectToAction(Actions.Battle(battleId));
        }

        [HttpGet]
        [Authorize]
        public virtual ActionResult LeaveBattle(long battleId)
        {
            _battlesService.LeaveBattle(battleId, CurrentUser.UserId);

            return RedirectToAction(Actions.Battle(battleId));
        }
    }
}
