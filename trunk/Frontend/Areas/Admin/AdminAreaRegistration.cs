using System.Web.Mvc;
using BetTeamsBattle.Frontend.AspNetMvc.Routes;

namespace BetTeamsBattle.Frontend.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            #region AdminBattlesController
            context.MapLanguageRoute("admin/battles", MVC.Admin.AdminBattles.GetBattles());
            context.MapLanguageRoute("admin/battles/create", MVC.Admin.AdminBattles.CreateBattle());
            #endregion

            #region AdminTeamsControoler
            context.MapLanguageRoute("admin/teams/pro", MVC.Admin.AdminTeams.GetProTeams());
            context.MapLanguageRoute("admin/teams/pro/create", MVC.Admin.AdminTeams.CreateProTeam());
            #endregion AdminTeamsController
        }
    }
}
