using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Frontend.Localization.Infrastructure;

namespace BetTeamsBattle.Frontend.Models.Shared.Languages
{
    public class LanguageViewModel
    {
        public Language Language { get; set; }
        public ActionResult ChangeLanguageActionResult { get; set; }
        public string LanguageClass { get; set; }
        public string CurrentLanguageClass { get; set; }

        public bool IsCurrentLanguage
        {
            get { return Language == CurrentLanguage.Language; }
        }

        public LanguageViewModel(Language language, string languageClass, string currentLanguageClass)
        {
            Language = language;
            LanguageClass = languageClass;
            CurrentLanguageClass = currentLanguageClass;
        }
    }
}