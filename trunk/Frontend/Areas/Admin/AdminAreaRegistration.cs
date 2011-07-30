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
            #region BattlesController
            context.MapLanguageRoute("admin/battles", MVC.Admin.AdminBattles.Actions.GetBattles());
            context.MapLanguageRoute("admin/battles/create", MVC.Admin.AdminBattles.Actions.CreateBattle());
            #endregion
        }
    }
}
