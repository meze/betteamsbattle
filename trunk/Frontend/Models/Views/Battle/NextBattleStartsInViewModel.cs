namespace BetTeamsBattle.Frontend.Models.Views.Battle
{
    public class NextBattleStartsInViewModel
    {
        public int Days { get; set; }
        public int DaysCaption { get; set; }

        public NextBattleStartsInViewModel(int days, int daysCaption)
        {
            Days = days;
            DaysCaption = daysCaption;
        }
    }
}