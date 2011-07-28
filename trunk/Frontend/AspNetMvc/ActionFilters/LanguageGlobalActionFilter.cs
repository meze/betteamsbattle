#region

using System.Web;
using System.Web.Mvc;
using BetTeamsBattle.Frontend.Authentication;
using BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageService.Interfaces;

#endregion

namespace BetTeamsBattle.Frontend.AspNetMvc.ActionFilters
{
    public class LanguageGlobalActionFilter : ActionFilterAttribute
    {
        private readonly ILanguageService _languageService;

        public LanguageGlobalActionFilter(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _languageService.ProccessLanguageForRequest(
                CurrentUser.User, HttpContext.Current.Request, HttpContext.Current.Response);

            base.OnActionExecuting(filterContext);
        }
    }
}