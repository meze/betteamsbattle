#region

using System.Web;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageMapping;

#endregion

namespace BetTeamsBattle.Frontend.Localization.Infrastructure
{
    public class CurrentLanguage
    {
        public static Language Language
        {
            get
            {
                return (Language) HttpContext.Current.Items[FrontendConstants.LanguageKey];
            }
        }

        public static string CountryCode
        {
            get
            {
                return LanguageMappingHelper.GetCountryCodeByLanguage(Language);
            }
        }
    }
}