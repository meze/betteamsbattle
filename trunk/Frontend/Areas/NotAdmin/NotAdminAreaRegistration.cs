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
            context.MapLanguageRoute("battles/actual", MVC.NotAdmin.Battles.ActualBattles());
            context.MapLanguageRoute("battles/{battleId}/join", MVC.NotAdmin.Battles.JoinBattle());
            context.MapLanguageRoute("battles/{battleId}/leave", MVC.NotAdmin.Battles.LeaveBattle());
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
