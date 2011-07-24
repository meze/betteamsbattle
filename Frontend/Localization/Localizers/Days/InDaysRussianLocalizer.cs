namespace BetTeamsBattle.Frontend.Localization.Localizers.Days
{
    internal class InDaysRussianLocalizer
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