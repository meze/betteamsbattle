using BetTeamsBattle.Frontend.Areas.Admin.Models;
using FluentValidation;

namespace BetTeamsBattle.Frontend.Areas.Admin.Validators
{
    public class BattleViewModelValidator : AbstractValidator<BattleViewModel>
    {
        public BattleViewModelValidator()
        {
            RuleFor(b => b.Budget).GreaterThan(0).WithMessage("Budget should be positive");
        }
    }
}