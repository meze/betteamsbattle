using BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Localizers.InDays.Interfaces;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Localizers.InDays
{
    internal class InDaysEnglishLocalizer : IInDaysLocalizer
    {
        public string Localize(int days)
        {
            string dayPart;
            if (days == 1)
                dayPart = "day";
            else
                dayPart = "days";
            return string.Format("in {0} {1}", days, dayPart);
        }
    }
}