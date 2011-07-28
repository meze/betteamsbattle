#region

using System.Collections.Generic;
using System.Web.Mvc;

#endregion

namespace BetTeamsBattle.Frontend.AspNetMvc.ActionFilters
{
    public class AdminRightsGlobalFilter : ActionFilterAttribute
    {
        private static IList<string> _adminUsers = new List<string>() { "Idsa" };

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToUpper().
                StartsWith("ADMIN"))
            {
                if (!(filterContext.HttpContext.User.Identity.IsAuthenticated &&
                    _adminUsers.Contains(filterContext.HttpContext.User.Identity.Name)))
                    filterContext.HttpContext.Response.Redirect("/");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}