using System.Web.Mvc;
using System.Web.Routing;

namespace BetTeamsBattle.Frontend.AspNetMvc.Routes
{
    internal class LanguagelessMvcRouteHandler : MvcRouteHandler
    {
        protected override System.Web.IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            //requestContext.RouteData.Values.Remove("language");
            return base.GetHttpHandler(requestContext);
        }
    }
}