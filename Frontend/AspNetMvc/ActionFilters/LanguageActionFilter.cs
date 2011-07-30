#region

using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Frontend.Authentication;
using BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageMapping;
using BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageService.Interfaces;

#endregion

namespace BetTeamsBattle.Frontend.AspNetMvc.ActionFilters
{
    public class LanguageActionFilter : ActionFilterAttribute
    {
        private readonly ILanguageService _languageService;

        public LanguageActionFilter(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            var language = _languageService.ProccessLanguageForRequest(CurrentUser.User, HttpContext.Current.Request, HttpContext.Current.Response);

            HttpContext.Current.Items[FrontendConstants.LanguageKey] = language;

            var cultureInfo = LanguageMappingHelper.GetDefaultCultureInfo(language);
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;

            base.OnActionExecuting(filterContext);
        }
    }
}