using BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Localizers.InDays.Interfaces;

namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Localizers.InDays
{
    internal class InDaysRussianLocalizer : IInDaysLocalizer
    {
        public string Localize(int days)
        {
            string dayPart;
            if (days == 1)
                dayPart = "день";
            else if (days >= 2 && days <= 4)
                dayPart = "дня";
            else
                dayPart = "дней";
            return string.Format("через {0} {1}", days, dayPart);
        }
    }
}