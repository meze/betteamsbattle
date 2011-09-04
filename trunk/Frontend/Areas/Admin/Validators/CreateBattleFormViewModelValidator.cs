using BetTeamsBattle.Frontend.Areas.Admin.Models;
using BetTeamsBattle.Frontend.Areas.Admin.Models.Battles;
using FluentValidation;

namespace BetTeamsBattle.Frontend.Areas.Admin.Validators
{
    public class CreateBattleFormViewModelValidator : AbstractValidator<CreateBattleFormViewModel>
    {
        public CreateBattleFormViewModelValidator()
        {
            RuleFor(b => b.Budget).GreaterThan(0).WithMessage("Budget should be positive");
        }
    }
}