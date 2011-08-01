namespace BetTeamsBattle.Frontend.Authentication.Services.Interfaces
{
    /// <summary>
    /// Forms authentication service interface 
    /// </summary>
    public interface IFormsAuthenticationService
    {
        /// <summary>
        /// Signs user in
        /// </summary>
        /// <param name="userName">Username to sign in</param>
        /// <param name="createPersistentCookie">Creates cookie if true</param>
        void RedirectFromLoginPage(string userName, bool createPersistentCookie);

        /// <summary>
        /// Signs current user out
        /// </summary>
        void SignOut();
    }
}