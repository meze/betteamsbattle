using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace BetTeamsBattle.Frontend.AspNetMvc.Routes
{
    internal static class RouteExtensions
    {
        public static void MapRoute(this RouteCollection routes, string url, ActionResult actionResult)
        {
            T4Extensions.MapRoute(routes, GetRouteName(), url, actionResult);
        }

        public static void MapRoute(this RouteCollection routes, string url, ActionResult actionResult, object constraints)
        {
            T4Extensions.MapRoute(routes, GetRouteName(), url, (ActionResult) actionResult, null, constraints);
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