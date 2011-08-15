﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets;
using BetTeamsBattle.Frontend.Authentication;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers
{
    public partial class BattleBetsController : Controller
    {
        private readonly IRepository<BattleBet> _repositoryOfBattleBet;
        private readonly IBattlesService _battlesService;

        public BattleBetsController(IRepository<BattleBet> repositoryOfBattleBet, IBattlesService battlesService)
        {
            _repositoryOfBattleBet = repositoryOfBattleBet;
            _battlesService = battlesService;
        }

        [Authorize]
        public virtual ActionResult MyBets(long battleId)
        {
            var myBets = _repositoryOfBattleBet.
                Get(BattleBetSpecifications.BattleIdAndUserIdAreEqualTo(battleId, CurrentUser.UserId)).
                ToList();

            var myBetsViewModel = new MyBetsViewModel(battleId, myBets);

            return View(myBetsViewModel);
        }

        [Authorize]
        [HttpGet]
        public virtual ActionResult MakeBet(long battleId)
        {
            var makeBetViewModel = new MakeBetViewModel();

            return View(makeBetViewModel);
        }

        [Authorize]
        [HttpPost]
        public virtual ActionResult MakeBet(long battleId, MakeBetViewModel makeBetViewModel)
        {
            if (!ModelState.IsValid)
                return View(makeBetViewModel);

            _battlesService.MakeBet(battleId, CurrentUser.UserId, makeBetViewModel.Bet, makeBetViewModel.Coefficient, makeBetViewModel.Url);

            return RedirectToAction(MVC.NotAdmin.BattleBets.MyBets(battleId));
        }

        [Authorize]
        public virtual ActionResult BetSucceeded(long battleBetId)
        {
            long battleId;
            _battlesService.BetSucceeded(battleBetId, CurrentUser.UserId, out battleId);

            return RedirectToAction(MVC.NotAdmin.BattleBets.MyBets(battleId));
        }

        [Authorize]
        public virtual ActionResult BetFailed(long battleBetId)
        {
            long battleId;
            _battlesService.BetFailed(battleBetId, CurrentUser.UserId, out battleId);

            return RedirectToAction(MVC.NotAdmin.BattleBets.MyBets(battleId));
        }
    }
}
