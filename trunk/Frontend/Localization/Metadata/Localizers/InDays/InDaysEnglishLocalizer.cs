using BetTeamsBattle.Frontend.Localization.Localizers.InDays.Interfaces;

namespace BetTeamsBattle.Frontend.Localization.Localizers.InDays
{
    internal class InDaysEnglishLocalizer : IInDaysLocalizer
    {
        public string Localize(int days)
        {
            if (days == 1)
                return "day";
            else
                return "days";
        }
    }
}