using BetTeamsBattle.Frontend.Localization.Metadata.Localizers.InDays.Interfaces;

namespace BetTeamsBattle.Frontend.Localization.Metadata.Localizers.InDays
{
    internal class InDaysRussianLocalizer : IInDaysLocalizer
    {
        public string Localize(int days)
        {
            if (days == 1)
                return "день";
            else if (days >= 2 && days <= 4)
                return "дня";
            else
                return "дней";
        }
    }
}