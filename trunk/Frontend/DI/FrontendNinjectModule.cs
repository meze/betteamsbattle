using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Frontend.Localization.Infrastructure;
using BetTeamsBattle.Frontend.Localization.Localizers.InDays;
using BetTeamsBattle.Frontend.Localization.Localizers.InDays.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.Frontend.DI
{
    internal class FrontendNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IInDaysLocalizer>().To<InDaysEnglishLocalizer>().When(r => CurrentLanguage.Language == Language.English);
            Bind<IInDaysLocalizer>().To<InDaysRussianLocalizer>().When(r => CurrentLanguage.Language == Language.Russian);
        }
    }
}