namespace BetTeamsBattle.Frontend.Models.Views.Battle
{
    public class NextBattleStartsInViewModel
    {
        public int Days { get; set; }
        public string DaysCaption { get; set; }

        public NextBattleStartsInViewModel(int days, string daysCaption)
        {
            Days = days;
            DaysCaption = daysCaption;
        }
    }
}