using System;
using System.Text.RegularExpressions;
using System.Web;

namespace BetTeamsBattle.Frontend.Helpers
{
    internal class DomainHelper
    {
        private static readonly Regex _wwwRegex = new Regex(@"www\.(?<mainDomain>.*)", RegexOptions.Compiled |
                                                                                       RegexOptions.IgnoreCase |
                                                                                       RegexOptions.Singleline);

        public static void RedirectToNonWww()
        {
            HttpRequest request = HttpContext.Current.Request;

            string hostName = request.Headers["x-forwarded-host"];
            hostName = string.IsNullOrEmpty(hostName) ? request.Url.Host : hostName;
            Match match = _wwwRegex.Match(hostName);
            if (match.Success)
            {
                string mainDomain = match.Groups["mainDomain"].Value;
                var builder = new UriBuilder(request.Url)
                {
                    Host = mainDomain
                };
                string redirectUrl = builder.Uri.ToString();

                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.StatusCode = 301;
                response.StatusDescription = "Moved Permanently";
                response.AddHeader("Location", redirectUrl);
                response.End();
            }
        }
    }
}