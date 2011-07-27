#region

using System;
using System.Web;

#endregion

namespace BetTeamsBattle.Frontend.Localization.Infrastructure.Cookies.Interfaces
{
    public interface ICookiesService
    {
        HttpCookie CreateCrossSiteCookie(string name, string value, DateTime expires);
    }
}