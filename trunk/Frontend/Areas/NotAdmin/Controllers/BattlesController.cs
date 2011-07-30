using System;
using System.Linq;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battle;
using BetTeamsBattle.Frontend.Localization.Metadata.Localizers.InDays.Interfaces;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers
{
    public partial class BattlesController : Controller
    {
        private readonly IRepository<Battle> _repositoryOfBattle;
        private readonly IInDaysLocalizer _inDaysLocalizer;

        public BattlesController(IRepository<Battle> repositoryOfBattle, IInDaysLocalizer inDaysLocalizer)
        {
            _repositoryOfBattle = repositoryOfBattle;
            _inDaysLocalizer = inDaysLocalizer;
        }

        [ChildActionOnly]
        public virtual ActionResult NextBattleStartsIn()
        {
            //ToDo: Order by here is part of query logic. May be put it in Repository/BattlesRepository?
            var nextBattleDate = _repositoryOfBattle.Filter(BattleSpecifications.StartDateIsInFuture()).OrderBy(b => b.StartDate).Select(b => b.StartDate).FirstOrDefault();
            if (nextBattleDate == default(DateTime))
                return new EmptyResult();

            var inDays = (nextBattleDate - DateTime.Now).Days;
            var inDaysCaption = _inDaysLocalizer.Localize(inDays);
            var inDaysViewModel = new NextBattleStartsInViewModel(inDays, inDaysCaption);

            return View(MVC.NotAdmin.Battles.Views.NextBattleStartsIn, inDaysViewModel);
        }
    }
}
