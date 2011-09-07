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

        public static void MapLanguageRoute(this RouteCollection routes, string url, ActionResult actionResult)
        {
            routes.MapRoute(GetRouteName(), GetLanguageRouteUrl(url), actionResult, null, GetLangaugeRouteConstraints());
        }

        public static void MapLanguageRoute(this AreaRegistrationContext context, string url, ActionResult actionResult)
        {
            context.MapRouteArea(GetRouteName(), GetLanguageRouteUrl(url), actionResult, null, GetLangaugeRouteConstraints());
        }

        private static string GetRouteName()
        {
            // route names should be different to keep RouteCollection search efficient
            return Guid.NewGuid().ToString() + DateTime.UtcNow.ToString();
        }

        private static string GetLanguageRouteUrl(string url)
        {
            if (url != String.Empty)
                url = "/" + url;
            return "{language}" + url;
        }

        private static object GetLangaugeRouteConstraints()
        {
            return new {language = RegexRouteConstraints.LanguageConstraint};
        }
    }
}