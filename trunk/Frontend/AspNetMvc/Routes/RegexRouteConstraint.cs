#region

using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

#endregion

namespace BetTeamsBattle.Frontend.AspNetMvc.Routes
{
    public class RegexRouteConstraint : IRouteConstraint 
    {
        private readonly string _pattern;

        public RegexRouteConstraint(string pattern)
        {
            _pattern = pattern;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            string value = (string)values[parameterName];

            if (value == null)
                return false;

            return Regex.Match(value, _pattern).Success;
        }
    }
}