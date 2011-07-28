using System.Web.Mvc;

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
            context.MapRoute(
                "NotAdmin_default",
                "NotAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
