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
            context.MapLanguageRoute("battles/all", MVC.NotAdmin.Battles.AllBattles());
            context.MapLanguageRoute("battles/{battleId}", MVC.NotAdmin.Battles.GetBattle());
            #endregion

            #region BetsController
            context.MapLanguageRoute("battles/{battleId}/mybets", MVC.NotAdmin.Bets.GetMyBattleBets());
            context.MapLanguageRoute("users/{userId}/bets", MVC.NotAdmin.Bets.GetUserBets());
            context.MapLanguageRoute("teasm/{teamId}/bets", MVC.NotAdmin.Bets.GetTeamBets());
            context.MapLanguageRoute("battles/{battleId}/mybets/new", MVC.NotAdmin.Bets.MakeBet());
            context.MapLanguageRoute("battles/{battleId}/mybets/{battleBetId}/succeeded", MVC.NotAdmin.Bets.BetSucceeded());
            context.MapLanguageRoute("battles/{battleId}/mybets/{battleBetId}/failed", MVC.NotAdmin.Bets.BetFailed());
            context.MapLanguageRoute("battles/{battleId}/mybets/{battleBetId}/canceledbybookmaker", MVC.NotAdmin.Bets.BetCanceledByBookmaker());
            #endregion

            #region AccountsController
            context.MapLanguageRoute("account/signin", MVC.NotAdmin.Accounts.SignIn());
            context.MapLanguageRoute("account/authenticate", MVC.NotAdmin.Accounts.Authenticate());
            context.MapLanguageRoute("account/signup", MVC.NotAdmin.Accounts.SignUp());
            context.MapLanguageRoute("account/signout", MVC.NotAdmin.Accounts.SignOut());
            #endregion

            #region TeamsController
            context.MapLanguageRoute("teams/{teamId}", MVC.NotAdmin.Teams.GetTeam());
            context.MapLanguageRoute("users/{userId}", MVC.NotAdmin.Teams.GetPersonalTeam());
            context.MapLanguageRoute("teams/top", MVC.NotAdmin.Teams.TopTeams());
            context.MapLanguageRoute("battles/{battleId}/topteams", MVC.NotAdmin.Teams.BattleTopTeams());
            #endregion

            #region UsersController
            
            #endregion
        }
    }
}
