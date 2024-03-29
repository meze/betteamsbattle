﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;
using BetTeamsBattle.Frontend.Authentication;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers
{
    public partial class BetsController : Controller
    {
        private readonly IBetsViewService _betsViewService;
        private readonly IBattlesService _battlesService;

        public BetsController(IBetsViewService betsViewService, IBattlesService battlesService)
        {
            _betsViewService = betsViewService;
            _battlesService = battlesService;
        }

        [ChildActionOnly]
        public virtual ActionResult GetMyBattleBets(long battleId)
        {
            var betsViewModel = _betsViewService.GetMyBattleBets(battleId, CurrentUser.NullableUserId);

            return View(Views.GetBets, betsViewModel);
        }

        [ChildActionOnly]
        public virtual ActionResult GetUserBets(long userId)
        {
            var betViewModel = _betsViewService.GetUserBets(userId, CurrentUser.NullableUserId);

            return View(Views.GetBets, betViewModel);
        }

        [ChildActionOnly]
        public virtual ActionResult GetTeamBets(long teamId)
        {
            var betsViewModel = _betsViewService.GetTeamBets(teamId);

            return View(Views.GetBets, betsViewModel);
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

            _battlesService.MakeBet(battleId, makeBetForm.TeamId, CurrentUser.UserId, makeBetForm.Title, makeBetForm.Amount.Value, makeBetForm.Coefficient.Value, makeBetForm.Url, makeBetForm.IsPrivate);

            return RedirectToAction(MVC.NotAdmin.Battles.GetBattle(battleId));
        }

        [Authorize]
        //[HttpPost]
        public virtual ActionResult BetSucceeded(long battleBetId)
        {
            long battleId;
            _battlesService.BetSucceeded(battleBetId, CurrentUser.UserId, out battleId);

            return RedirectToAction(MVC.NotAdmin.Battles.GetBattle(battleId));
        }

        [Authorize]
        //[HttpPost]
        public virtual ActionResult BetFailed(long battleBetId)
        {
            long battleId;
            _battlesService.BetFailed(battleBetId, CurrentUser.UserId, out battleId);

            return RedirectToAction(MVC.NotAdmin.Battles.GetBattle(battleId));
        }

        [Authorize]
        //[HttpPost]
        public virtual ActionResult BetCanceledByBookmaker(long battleBetId)
        {
            long battleId;
            _battlesService.BetCanceledByBookmaker(battleBetId, CurrentUser.UserId, out battleId);

            return RedirectToAction(MVC.NotAdmin.Battles.GetBattle(battleId));
        }
    }
}
