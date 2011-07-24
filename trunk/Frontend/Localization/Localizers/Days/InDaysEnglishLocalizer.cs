namespace BetTeamsBattle.Frontend.Localization.Localizers.Days
{
    internal class InDaysEnglishLocalizer
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