#region

using System;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageMapping;

#endregion

namespace BetTeamsBattle.Frontend.Localization.Infrastructure.Url
{
    public class UrlCountryCodeHelper
    {
        public static string ChangeUrlCountryCodePart(Uri uri, Language language)
        {
            var path = uri.GetComponents(UriComponents.Path, UriFormat.Unescaped);

            string pathCountryCode;
            GetPathCountryCodeParts(path, out pathCountryCode);
            if (LanguageMappingHelper.CountryCodeIsSupported(pathCountryCode))
            {
                path = path.Substring(2);
                if (path == String.Empty)
                    path = "/";
            }

            var countryCode = LanguageMappingHelper.GetCountryCodeByLanguage(language);

            var uriBuilder = new UriBuilder(uri);
            if (path == String.Empty)
                uriBuilder.Path = countryCode;
            else
            {
                if (!path.StartsWith("/"))
                    path = "/" + path;
                uriBuilder.Path = String.Format("{0}{1}", countryCode, path);
            }

            return uriBuilder.Uri.ToString();
        }

        public static void GetPathCountryCodeParts(string path, out string pathCountryCode)
        {
            pathCountryCode = String.Empty;

            if (path.Length >= 2)
                if (path.Length == 2 || path.Substring(2, 1) == "/")
                    pathCountryCode = path.Substring(0, 2);
        }
    }
}