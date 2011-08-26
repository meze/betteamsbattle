using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;
using BetTeamsBattle.Frontend.Authentication;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers
{
    public partial class BattleBetsController : Controller
    {
        private readonly IRepository<BattleBet> _repositoryOfBattleBet;
        private readonly IBattleBetsViewService _battleBetsViewService;
        private readonly IBattlesService _battlesService;

        public BattleBetsController(IRepository<BattleBet> repositoryOfBattleBet, IBattleBetsViewService battleBetsViewService, IBattlesService battlesService)
        {
            _repositoryOfBattleBet = repositoryOfBattleBet;
            _battleBetsViewService = battleBetsViewService;
            _battlesService = battlesService;
        }

        [ChildActionOnly]
        public virtual ActionResult MyBets(long battleId)
        {
            var myBets = _battleBetsViewService.MyBets(battleId, CurrentUser.UserId);

            return View(myBets);
        }

        [Authorize]
        [HttpGet]
        public virtual ActionResult MakeBet(long battleId)
        {
            var makeBetViewModel = _battleBetsViewService.MakeBet(battleId, CurrentUser.UserId);

            return View(makeBetViewModel);
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult MakeBet(long battleId, MakeBetFormViewModel makeBetForm)
        {
            if (!ModelState.IsValid)
                return View(_battleBetsViewService.MakeBet(battleId, CurrentUser.UserId, makeBetForm));

            _battlesService.MakeBet(battleId, makeBetForm.TeamId, CurrentUser.UserId, makeBetForm.Title, makeBetForm.Bet, makeBetForm.Coefficient, makeBetForm.Url, makeBetForm.IsPrivate);

            return RedirectToAction(MVC.NotAdmin.Battles.Battle(battleId));
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult BetSucceeded(long battleBetId)
        {
            long battleId;
            _battlesService.BetSucceeded(battleBetId, CurrentUser.UserId, out battleId);

            return RedirectToAction(MVC.NotAdmin.BattleBets.MyBets(battleId));
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult BetFailed(long battleBetId)
        {
            long battleId;
            _battlesService.BetFailed(battleBetId, CurrentUser.UserId, out battleId);

            return RedirectToAction(MVC.NotAdmin.BattleBets.MyBets(battleId));
        }
    }
}
