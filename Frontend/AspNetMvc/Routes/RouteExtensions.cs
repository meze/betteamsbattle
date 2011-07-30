using System;
using System.Web.Routing;
using System.Web.Mvc;

namespace BetTeamsBattle.Frontend.AspNetMvc.Routes
{
    internal static class RouteExtensions
    {
        public static void MapRoute(this RouteCollection routes, string url, ActionResult actionResult)
        {
            routes.MapRoute(GetRouteName(), url, actionResult);
        }

        public static void MapRoute(this RouteCollection routes, string url, ActionResult actionResult, object constraints)
        {
            routes.MapRoute(GetRouteName(), url, actionResult, null, constraints);
        }

        public static void MapLanguageRoute(this AreaRegistrationContext context, string url, ActionResult actionResult)
        {
            if (url != String.Empty)
                url = "/" + url;
            url = "{language}" + url;

            var route = context.MapRouteArea(GetRouteName(), url, actionResult, new { language = RegexRouteConstraints.LanguageConstraint });
           // route.RouteHandler = new LanguagelessMvcRouteHandler();
        }

        private static string GetRouteName()
        {
            // route names should be different to keep RouteCollection search efficient
            return Guid.NewGuid().ToString() + DateTime.UtcNow.ToString();
        }
    }
}