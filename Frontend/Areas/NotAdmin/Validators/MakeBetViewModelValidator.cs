using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Resources.Views.BattleBets;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Accounts;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets;
using BetTeamsBattle.Frontend.Authentication;
using BetTeamsBattle.Frontend.Localization.Infrastructure;
using FluentValidation;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Validators
{
    public class MakeBetViewModelValidator : AbstractValidator<MakeBetFormViewModel>
    {
        private readonly IRepository<BattleTeamStatistics> _repositoryOfBattleTeamStatistics;

        public MakeBetViewModelValidator(IRepository<BattleTeamStatistics> repositoryOfBattleTeamStatistics)
        {
            _repositoryOfBattleTeamStatistics = repositoryOfBattleTeamStatistics;

            RuleFor(mb => mb.Title).NotEmpty().WithMessage(BattleBets.TitleShouldNotBeEmpty);
            RuleFor(mb => mb.Title).Length(1, 200).WithMessage(BattleBets.TitleIsTooLong);

            RuleFor(mb => mb.Bet).GreaterThan(0).WithMessage(BattleBets.BetShouldBeMoreThanZero);
            RuleFor(mb => mb.Bet).Must((mb, bet) =>
                {
                    var battleId = Convert.ToInt32(RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current)).Values["battleId"]);
                    var teamId = Convert.ToInt32(RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current)).Values["teamId"]);

                    var battleTeamStatistics = _repositoryOfBattleTeamStatistics.Get(BattleTeamStatisticsSpecifications.BattleIdAndTeamIdAreEqualTo(battleId, teamId)).Include(bus => bus.Battle).Single();

                    var betLimit = battleTeamStatistics.Balance * (battleTeamStatistics.Battle.BetLimit / 100d);

                    return bet <= betLimit;
                }).WithMessage(BattleBets.BetIsOutOfYourLimit);

            RuleFor(mb => mb.Coefficient).GreaterThan(1).WithMessage(BattleBets.CoefficientShouldBeGreaterThanOne);

            RuleFor(mb => mb.Url).Must(url =>
                {
                    return Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
                }).WithMessage(BattleBets.UrlIsIncorrect);
        }
    }
}