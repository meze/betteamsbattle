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
    public class MakeBetFormViewModelValidator : AbstractValidator<MakeBetFormViewModel>
    {
        private readonly IRepository<TeamBattleStatistics> _repositoryOfBattleTeamStatistics;
        private IBattlesService _battlesService;
        private IRepository<Battle> _repositoryOfBattle;

        public MakeBetFormViewModelValidator(IRepository<TeamBattleStatistics> repositoryOfBattleTeamStatistics, IBattlesService battlesService, IRepository<Battle> repositoryOfBattle)
        {
            _repositoryOfBattleTeamStatistics = repositoryOfBattleTeamStatistics;
            _battlesService = battlesService;
            _repositoryOfBattle = repositoryOfBattle;

            RuleFor(mb => mb.Title).NotEmpty().WithMessage(BattleBets.TitleShouldNotBeEmpty);
            RuleFor(mb => mb.Title).Length(1, 200).WithMessage(BattleBets.TitleIsTooLong);

            RuleFor(mb => mb.AmountString).NotNull().WithMessage(BattleBets.PleaseInputCorrectFloatingNumber);
            RuleFor(mb => mb.AmountString).Must((mb, amountString) => mb.Amount.Value > 0).WithMessage(BattleBets.BetShouldBeMoreThanZero);
            RuleFor(mb => mb.AmountString).Must((mb, amountString) =>
                {
                    var battleId = Convert.ToInt32(RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current)).Values["battleId"]);
                    var teamId = Convert.ToInt32(RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current)).Values["teamId"]);

                    mb.BetLimit = _battlesService.GetBetLimit(battleId, teamId);

                    return mb.Amount.Value <= mb.BetLimit;
                }).WithMessage(BattleBets.BetShouldBeLessThan, mb => mb.BetLimit);

            RuleFor(mb => mb.CoefficientString).NotNull().WithMessage(BattleBets.PleaseInputCorrectFloatingNumber);
            RuleFor(mb => mb.CoefficientString).Must((mb, coefficientString) => mb.Coefficient.Value > 1).WithMessage(BattleBets.CoefficientShouldBeGreaterThanOne);

            RuleFor(mb => mb.Url).Must(url => Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute)).WithMessage(BattleBets.UrlIsIncorrect);
        }
    }
}