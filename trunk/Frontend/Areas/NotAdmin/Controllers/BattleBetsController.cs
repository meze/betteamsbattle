using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specific.BattleBet;
using BetTeamsBattle.Data.Services.Interfaces;
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
            var battleBets =
                _repositoryOfBattleBet.Get(BattleBetSpecifications.BattleIdIsEqualTo(battleId) &&
                                           BattleBetSpecifications.UserIdIsEqualTo(CurrentUser.UserId)).ToList();



            return View();
        }

        [Authorize]
        public virtual ActionResult BattleBetSucceeded(long battleBetId)
        {
            long battleId;
            _battlesService.CloseBattleBetAsSucceeded(battleBetId, CurrentUser.UserId, out battleId);

            return RedirectToAction(MVC.NotAdmin.BattleBets.MyBets(battleId));
        }

        [Authorize]
        public virtual ActionResult BattleBetFailed(long battleBetId)
        {
            long battleId;
            _battlesService.CloseBattleBetAsFailed(battleBetId, CurrentUser.UserId, out battleId);

            return RedirectToAction(MVC.NotAdmin.BattleBets.MyBets(battleId));
        }
    }
}
