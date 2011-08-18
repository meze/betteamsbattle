using System.Reflection;
using System.Web.Mvc;
using BetTeamsBattle.Data.Model;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Localizers.InDays;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Localizers.InDays.Interfaces;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles;
using BetTeamsBattle.Frontend.Areas.NotAdmin.ViewServices.Battles.Interfaces;
using BetTeamsBattle.Frontend.AspNetMvc.ActionFilters;
using BetTeamsBattle.Frontend.Authentication.Services;
using BetTeamsBattle.Frontend.Authentication.Services.Interfaces;
using BetTeamsBattle.Frontend.Localization.Infrastructure;
using BetTeamsBattle.Frontend.Localization.Infrastructure.Cookies;
using BetTeamsBattle.Frontend.Localization.Infrastructure.Cookies.Interfaces;
using BetTeamsBattle.Frontend.Localization.Infrastructure.IP;
using BetTeamsBattle.Frontend.Localization.Infrastructure.IP.Interfaces;
using BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageService;
using BetTeamsBattle.Frontend.Localization.Infrastructure.LanguageService.Interfaces;
using FluentValidation;
using Ninject.Modules;
using Ninject.Web.Mvc.FilterBindingSyntax;

namespace BetTeamsBattle.Frontend.DI
{
    internal class FrontendNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ModelContext>().To<ModelContext>().InRequestScope();

            Bind<ICookiesService>().To<CookiesService>();

            Bind<IIpToNumberConverter>().To<IpToNumberConverter>();
            Bind<IIpToLanguageConverter>().To<IpToLanguageConverter>();
            
            Bind<ILanguageService>().To<LanguageService>();

            Bind<IFormsAuthenticationService>().To<FormsAuthenticationService>();

            Bind<IBattlesViewService>().To<BattlesViewService>();
            Bind<IUsersViewService>().To<UsersViewService>();

            Bind<IInDaysLocalizer>().To<InDaysEnglishLocalizer>().When(r => CurrentLanguage.Language == Language.English);
            Bind<IInDaysLocalizer>().To<InDaysRussianLocalizer>().When(r => CurrentLanguage.Language == Language.Russian);

            AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly()).
                ForEach(match => Bind(match.InterfaceType).To(match.ValidatorType));

            this.BindFilter<HandleErrorAttribute>(FilterScope.Global, 0);
            this.BindFilter<AdminRightsActionFilter>(FilterScope.Global, 0);
        }
    }
}