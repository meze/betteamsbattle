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
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Accounts;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets;
using BetTeamsBattle.Frontend.Authentication;
using BetTeamsBattle.Frontend.Localization.Infrastructure;
using FluentValidation;
using Resources;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Validators
{
    public class MakeBetViewModelValidator : AbstractValidator<MakeBetFormViewModel>
    {
        private readonly IRepository<TeamBattleStatistics> _repositoryOfBattleTeamStatistics;
        private IBattlesService _battlesService;
        private IRepository<Battle> _repositoryOfBattle;

        public MakeBetViewModelValidator(IRepository<TeamBattleStatistics> repositoryOfBattleTeamStatistics, IBattlesService battlesService, IRepository<Battle> repositoryOfBattle)
        {
            _repositoryOfBattleTeamStatistics = repositoryOfBattleTeamStatistics;
            _battlesService = battlesService;
            _repositoryOfBattle = repositoryOfBattle;

            RuleFor(mb => mb.Title).NotEmpty().WithMessage(BattleBets.TitleShouldNotBeEmpty);
            RuleFor(mb => mb.Title).Length(1, 200).WithMessage(BattleBets.TitleIsTooLong);

            RuleFor(mb => mb.Bet).GreaterThan(0).WithMessage(BattleBets.BetShouldBeMoreThanZero);
            RuleFor(mb => mb.Bet).Must((mb, bet) =>
                {
                    var battleId = Convert.ToInt32(RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current)).Values["battleId"]);
                    var teamId = Convert.ToInt32(RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current)).Values["teamId"]);

                    var battle = _repositoryOfBattle.Get(EntitySpecifications.IdIsEqualTo<Battle>(battleId)).Single();

                    var gain = _repositoryOfBattleTeamStatistics.Get(TeamBattleStatisticsSpecifications.BattleIdAndTeamIdAreEqualTo(battleId, teamId)).Select(bts => bts.Gain).SingleOrDefault();
                    var balance = battle.Budget + gain;

                    var betLimit = balance * (battle.BetLimit / 100d);

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