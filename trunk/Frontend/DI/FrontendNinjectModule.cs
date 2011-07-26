using BetTeamsBattle.Frontend.Localization.Localizers.InDays;
using BetTeamsBattle.Frontend.Localization.Localizers.InDays.Interfaces;
using Ninject.Modules;

namespace BetTeamsBattle.Frontend.DI
{
    internal class FrontendNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IInDaysLocalizer>().To<InDaysEnglishLocalizer>().When().
        }
    }
}