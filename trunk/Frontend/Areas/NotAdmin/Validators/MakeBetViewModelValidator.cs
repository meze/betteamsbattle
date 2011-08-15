using System;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Accounts;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets;
using BetTeamsBattle.Frontend.Authentication;
using BetTeamsBattle.Frontend.Localization.Infrastructure;
using FluentValidation;
using Resources;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Validators
{
    public class MakeBetViewModelValidator : AbstractValidator<MakeBetViewModel>
    {
        private IRepository<BattleUserStatistics> _repositoryOfBattleUserStatistics;

        public MakeBetViewModelValidator(IRepository<BattleUserStatistics> repositoryOfBattleUserStatistics)
        {
            _repositoryOfBattleUserStatistics = repositoryOfBattleUserStatistics;

            RuleFor(mb => mb.Title).NotEmpty().WithMessage(BattleBets.TitleShouldNotBeEmpty);
            RuleFor(mb => mb.Title).Length(1, 200).WithMessage(BattleBets.TitleIsTooLong);

            RuleFor(mb => mb.Bet).GreaterThan(0).WithMessage(BattleBets.BetShouldBeMoreThanZero);
            RuleFor(mb => mb.Bet).Must((mb, bet) =>
                {
                    var battleUserStatistics = _repositoryOfBattleUserStatistics.Get(BattleUserStatisticsSpecifications.BattleIdAndUserIdAreEqualTo(mb.BattleId, CurrentUser.UserId)).Include(bus => bus.Battle).Single();

                    var betLimit = battleUserStatistics.Balance * (battleUserStatistics.Battle.BetLimit / 100.0);

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