﻿using System.Web.Mvc;
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
            context.MapLanguageRoute("battles/{battleId}", MVC.NotAdmin.Battles.Battle());
            context.MapLanguageRoute("battles/{battleId}/topteams", MVC.NotAdmin.Battles.BattleTopTeams());
            #endregion

            #region BattleBetsController
            context.MapLanguageRoute("battles/{battleId}/mybets", MVC.NotAdmin.BattleBets.MyBets());
            context.MapLanguageRoute("battles/{battleId}/mybets/new", MVC.NotAdmin.BattleBets.MakeBet());
            context.MapLanguageRoute("battles/{battleId}/mybets/{battleBetId}/succeeded", MVC.NotAdmin.BattleBets.BetSucceeded());
            context.MapLanguageRoute("battles/{battleId}/mybets/{battleBetId}/failed", MVC.NotAdmin.BattleBets.BetFailed());
            context.MapLanguageRoute("battles/{battleId}/mybets/{battleBetId}/canceledbybookmaker", MVC.NotAdmin.BattleBets.BetCanceledByBookmaker());
            #endregion

            #region AccountsController
            context.MapLanguageRoute("account/signin", MVC.NotAdmin.Accounts.SignIn());
            context.MapLanguageRoute("account/authenticate", MVC.NotAdmin.Accounts.Authenticate());
            context.MapLanguageRoute("account/signup", MVC.NotAdmin.Accounts.SignUp());
            context.MapLanguageRoute("account/signout", MVC.NotAdmin.Accounts.SignOut());
            #endregion

            #region TeamsController
            context.MapLanguageRoute("teams/top", MVC.NotAdmin.Teams.TopTeams());
            #endregion
        }
    }
}
