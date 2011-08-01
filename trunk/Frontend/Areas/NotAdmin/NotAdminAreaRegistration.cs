using System.Web.Mvc;
using BetTeamsBattle.Frontend.AspNetMvc.Routes;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin
{
    public class NotAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "NotAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            #region HomeController
            context.MapLanguageRoute("", MVC.NotAdmin.Home.Index());
            #endregion

            #region BattlesController
            context.MapLanguageRoute("battles/nextbattlestartsin", MVC.NotAdmin.Battles.NextBattleStartsIn());
            #endregion

            #region AccountsController
            context.MapLanguageRoute("account/signin", MVC.NotAdmin.Accounts.SignIn());
            context.MapLanguageRoute("account/authenticate", MVC.NotAdmin.Accounts.Authenticate());
            context.MapLanguageRoute("account/signup", MVC.NotAdmin.Accounts.SignUp());
            context.MapLanguageRoute("account/signout", MVC.NotAdmin.Accounts.SignOut());
            #endregion
        }
    }
}
