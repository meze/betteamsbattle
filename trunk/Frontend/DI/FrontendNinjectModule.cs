using System.Web.Mvc;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Frontend.AspNetMvc.ActionFilters;
using BetTeamsBattle.Frontend.Localization.Infrastructure;
using BetTeamsBattle.Frontend.Localization.Infrastructure.Cookies;
using BetTeamsBattle.Frontend.Localization.Infrastructure.Cookies.Interfaces;
using BetTeamsBattle.Frontend.Localization.Infrastructure.IP;
using BetTeamsBattle.Frontend.Localization.Infrastructure.IP.Interfaces;
using BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageService;
using BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageService.Interfaces;
using BetTeamsBattle.Frontend.Localization.Metadata.Localizers.InDays;
using BetTeamsBattle.Frontend.Localization.Metadata.Localizers.InDays.Interfaces;
using Ninject.Modules;
using Ninject.Web.Mvc.FilterBindingSyntax;

namespace BetTeamsBattle.Frontend.DI
{
    internal class FrontendNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICookiesService>().To<CookiesService>();

            Bind<IIpToNumberConverter>().To<IpToNumberConverter>();
            Bind<IIpToLanguageConverter>().To<IpToLanguageConverter>();
            
            Bind<ILanguageService>().To<LanguageService>();

            Bind<IInDaysLocalizer>().To<InDaysEnglishLocalizer>().When(r => CurrentLanguage.Language == Language.English);
            Bind<IInDaysLocalizer>().To<InDaysRussianLocalizer>().When(r => CurrentLanguage.Language == Language.Russian);

            this.BindFilter<HandleErrorAttribute>(FilterScope.Global, 0);
            this.BindFilter<LanguageGlobalActionFilter>(FilterScope.Global, 0);
            this.BindFilter<AdminRightsGlobalFilter>(FilterScope.Global, 0);
        }
    }
}