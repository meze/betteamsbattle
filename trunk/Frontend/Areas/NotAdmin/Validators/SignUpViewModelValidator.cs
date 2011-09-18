using System;
using System.Text.RegularExpressions;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Specifications;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Accounts;
using BetTeamsBattle.Frontend.Localization.Infrastructure;
using FluentValidation;
using Resources;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Validators
{
    public class SignUpViewModelValidator : AbstractValidator<SignUpViewModel>
    {
        private readonly IRepository<User> _repositoryOfUser;

        public SignUpViewModelValidator(IRepository<User> repositoryOfUser)
        {
            _repositoryOfUser = repositoryOfUser;
 
            RuleFor(u => u.Login).Length(3, 20).WithMessage(Accounts.LoginLengthError);
            RuleFor(u => u.Login).Matches("^[a-zA-Z0-9]*$").WithMessage(Accounts.LoginFormatError);
            RuleFor(u => u.Login).Must(l => !_repositoryOfUser.Any(UserSpecifications.LoginIsEqual(l))).
                WithMessage(Accounts.LoginIsBusy);

            RuleFor(u => u.OpenIdUrl).NotEmpty().WithMessage(Accounts.OpenIdIsIncorrectTryLoginOnceMore);
            RuleFor(u => u.OpenIdUrl).Must(oiu => !_repositoryOfUser.Any(UserSpecifications.OpenIdUrlIsEqual(oiu))).
                WithMessage(Accounts.OpenIdIsIncorrectTryLoginOnceMore);
        }
    }
}