#region

using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Web.Mvc;

#endregion

namespace BetTeamsBattle.Frontend.AspNetMvc.ActionFilters
{
    public class AdminRightsActionFilter : ActionFilterAttribute
    {
        private static IList<string> _adminUsers = new List<string>() { "Idsa" };

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var areaName = AreaHelpers.GetAreaName(filterContext.RouteData) ?? string.Empty;
            if (areaName.ToUpper().StartsWith("ADMIN"))
            {
                if (!(filterContext.HttpContext.User.Identity.IsAuthenticated &&
                    _adminUsers.Contains(filterContext.HttpContext.User.Identity.Name)))
                    filterContext.HttpContext.Response.Redirect("/");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}