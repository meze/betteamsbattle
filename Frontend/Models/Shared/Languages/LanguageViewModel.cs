using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Frontend.Localization.Infrastructure;

namespace BetTeamsBattle.Frontend.Models.Shared.Languages
{
    public class LanguageViewModel
    {
        public Language Language { get; set; }

        public bool IsCurrentLanguage { get; private set; }
        public string LanguageClass { get; private set; }

        public LanguageViewModel(Language language, string notCurrentLanguageClass, string currentLanguageClass)
        {
            Language = language;

            IsCurrentLanguage = language == CurrentLanguage.Language;
            LanguageClass = IsCurrentLanguage ? currentLanguageClass : notCurrentLanguageClass;
        }
    }
}