#region

using System;
using System.Web;
using BetTeamsBattle.Frontend.Localization.Infrastructure.Cookies.Interfaces;

#endregion

namespace BetTeamsBattle.Frontend.Localization.Infrastructure.Cookies
{
    public class CookiesService : ICookiesService
    {
        public HttpCookie CreateCrossSiteCookie(string name, string value, DateTime expires)
        {
            return new HttpCookie(name, value) {Domain = "." + FrontendConstants.Domain, Expires = expires};
        }
    }
}