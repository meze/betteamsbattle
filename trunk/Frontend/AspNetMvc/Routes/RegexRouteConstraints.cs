namespace BetTeamsBattle.Frontend.AspNetMvc.Routes
{
    internal class RegexRouteConstraints
    {
        public static RegexRouteConstraint LanguageConstraint = new RegexRouteConstraint("^(en|ru)$");
        public static RegexRouteConstraint StartsNotFromLanguageConstraint = new RegexRouteConstraint("^(?!((en|ru)($|/)))"); 
    }
}