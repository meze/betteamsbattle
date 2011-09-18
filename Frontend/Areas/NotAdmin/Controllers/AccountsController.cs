#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Specifications;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;
using BetTeamsBattle.Data.Services.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Accounts;
using BetTeamsBattle.Frontend.Authentication.Services.Interfaces;
using BetTeamsBattle.Frontend.Localization.Infrastructure;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;

#endregion

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Controllers
{
    public partial class AccountsController : Controller
    {
        private readonly IFormsAuthenticationService _formsAuthenticationService;
        private readonly IRepository<User> _repositoryOfUser;
        private readonly IUsersService _usersService;
        private readonly OpenIdRelyingParty _openIdRelyingParty;

        public AccountsController(IFormsAuthenticationService formsAuthenticationService,
            IRepository<User> repositoryOfUser,
            IUsersService usersService,
            OpenIdRelyingParty openIdRelyingParty)
        {
            _formsAuthenticationService = formsAuthenticationService;
            _repositoryOfUser = repositoryOfUser;
            _usersService = usersService;
            _openIdRelyingParty = openIdRelyingParty;
        }

        [HttpGet]
        public virtual ActionResult SignIn(string message)
        {
            // Stage 1: display login form to user
            return View(new SignInViewModel(message));
        }

        [ValidateInput(false)]
        public virtual ActionResult Authenticate(string openIdIdentifier)
        {
            var response = _openIdRelyingParty.GetResponse();
            if (response == null)
            {
                // Stage 2: user submitting Identifier
                Identifier id;
                if (Identifier.TryParse(openIdIdentifier, out id))
                {
                    try
                    {
                        return _openIdRelyingParty.CreateRequest(openIdIdentifier).RedirectingResponse.AsActionResult();
                    }
                    catch (ProtocolException ex)
                    {
                        return RedirectToAction(Actions.SignIn(ex.Message));
                    }
                }
                else
                    return RedirectToAction(Actions.SignIn("Invalid identifier"));
            }
            else
            {
                // Stage 3: OpenID Provider sending assertion response
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        string openIdUrl = response.ClaimedIdentifier;

                        string login = _repositoryOfUser.Get(UserSpecifications.OpenIdUrlIsEqual(openIdUrl)).Select(u => u.Login).SingleOrDefault();

                        if (string.IsNullOrEmpty(login))
                            return RedirectToAction(Actions.SignUp(openIdUrl));

                        _formsAuthenticationService.RedirectFromLoginPage(login, true);

                        break;
                    case AuthenticationStatus.Canceled:
                        return RedirectToAction(Actions.SignIn("Canceled at provider side"));
                    case AuthenticationStatus.Failed:
                        return RedirectToAction(Actions.SignIn(response.Exception.Message));
                }
            }

            return new EmptyResult();
        }

        public virtual ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction(MVC.NotAdmin.Home.Index());
        }

        [HttpGet]
        public virtual ActionResult SignUp(string openIdUrl)
        {
            return View(new SignUpViewModel() { OpenIdUrl = openIdUrl });
        }

        [HttpPost]
        public virtual ActionResult SignUp(SignUpViewModel userViewModel)
        {
            if (!ModelState.IsValid)
                return View(userViewModel);

            _usersService.Register(userViewModel.Login, userViewModel.OpenIdUrl, CurrentLanguage.Language);

            FormsAuthentication.RedirectFromLoginPage(userViewModel.Login, true);

            return new EmptyResult();
        }
    }
}
