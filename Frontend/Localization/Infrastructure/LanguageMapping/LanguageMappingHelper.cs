#region

using System;
using System.Collections.Generic;
using System.Globalization;
using BetTeamsBattle.Data.Model.Entities;

#endregion

namespace BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageMapping
{
    //ToDo: Reimplement it. Create one dictionary of LanguageDescription(Language, DefaultCultureInfo, CountryCode)
    public class LanguageMappingHelper
    {
        public static IList<Language> SupportedLanguages = new List<Language>() { Language.English, Language.Russian };

        public static CultureInfo GetDefaultCultureInfo(Language language)
        {
            switch (language)
            {
                case Language.English: return new CultureInfo("en-US");
                case Language.Russian: return new CultureInfo("ru-RU");
                default: throw new ArgumentOutOfRangeException("language");
            }
        }

        public static bool CountryCodeIsSupported(string countryCode)
        {
            if (countryCode == "en" || countryCode == "ru")
                return true;

            return false;
        }

        public static Language GetLanguageByCountryCode(string countrCode)
        {
            switch (countrCode)
            {
                case "en":
                    return Language.English;
                case "ru":
                    return Language.Russian;
                default:
                    throw new ArgumentOutOfRangeException("countrCode");
            }
        }

        public static string GetCountryCodeByLanguage(Language language)
        {
            switch(language)
            {
                case Language.English:
                    return "en";
                case Language.Russian:
                    return "ru";
                default:
                    throw new ArgumentOutOfRangeException("language");
            }
        }
    }
}