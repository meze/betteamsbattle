using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;
using BetTeamsBattle.Frontend.Authentication;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers
{
    public partial class BattleBetsController : Controller
    {
        private readonly IBetsViewService _betsViewService;
        private readonly IBattlesService _battlesService;

        public BattleBetsController(IBetsViewService betsViewService, IBattlesService battlesService)
        {
            _betsViewService = betsViewService;
            _battlesService = battlesService;
        }

        [ChildActionOnly]
        public virtual ActionResult GetMyBattleBets(long battleId)
        {
            IEnumerable<BetViewModel> myBattleBets = new List<BetViewModel>();
            if (CurrentUser.NullableUserId.HasValue)
                myBattleBets = _betsViewService.GetMyBattleBets(battleId, CurrentUser.UserId);

            return View(Views.Bets, myBattleBets);
        }

        [ChildActionOnly]
        public virtual ActionResult GetUserBets(long userId)
        {
            var userBets = _betsViewService.GetUserBets(userId, CurrentUser.NullableUserId);

            return View(Views.Bets, userBets);
        }

        [ChildActionOnly]
        public virtual ActionResult GetTeamBets(long teamId)
        {
            var teamBets = _betsViewService.GetTeamBets(teamId);

            return View(Views.Bets, teamBets);
        }

        [Authorize]
        [HttpGet]
        public virtual ActionResult MakeBet(long battleId)
        {
            var makeBetViewModel = _betsViewService.MakeBet(battleId, CurrentUser.UserId);

            return View(makeBetViewModel);
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult MakeBet(long battleId, MakeBetFormViewModel makeBetForm)
        {
            if (!ModelState.IsValid)
                return View(_betsViewService.MakeBet(battleId, CurrentUser.UserId, makeBetForm));

            _battlesService.MakeBet(battleId, makeBetForm.TeamId, CurrentUser.UserId, makeBetForm.Title, makeBetForm.Bet, makeBetForm.Coefficient, makeBetForm.Url, makeBetForm.IsPrivate);

            return RedirectToAction(MVC.NotAdmin.Battles.Battle(battleId));
        }

        [Authorize]
        //[HttpPost]
        public virtual ActionResult BetSucceeded(long battleBetId)
        {
            long battleId;
            _battlesService.BetSucceeded(battleBetId, CurrentUser.UserId, out battleId);

            return RedirectToAction(MVC.NotAdmin.Battles.Battle(battleId));
        }

        [Authorize]
        //[HttpPost]
        public virtual ActionResult BetFailed(long battleBetId)
        {
            long battleId;
            _battlesService.BetFailed(battleBetId, CurrentUser.UserId, out battleId);

            return RedirectToAction(MVC.NotAdmin.Battles.Battle(battleId));
        }

        [Authorize]
        //[HttpPost]
        public virtual ActionResult BetCanceledByBookmaker(long battleBetId)
        {
            long battleId;
            _battlesService.BetCanceledByBookmaker(battleBetId, CurrentUser.UserId, out battleId);

            return RedirectToAction(MVC.NotAdmin.Battles.Battle(battleId));
        }
    }
}
