#region

using System.Web.Security;
using BetTeamsBattle.Frontend.Authentication.Services.Interfaces;

#endregion

namespace BetTeamsBattle.Frontend.Authentication.Services
{
    // The FormsAuthentication type is sealed and contains static members, so it is difficult to
    // unit test code that calls its members. The interface and helper class below demonstrate
    // how to create an abstract wrapper around such a type in order to make the AccountController
    // code unit testable.

    /// <summary>
    /// Forms authentication service
    /// </summary>
    internal class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void RedirectFromLoginPage(string userName, bool createPersistentCookie)
        {
            FormsAuthentication.RedirectFromLoginPage(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}