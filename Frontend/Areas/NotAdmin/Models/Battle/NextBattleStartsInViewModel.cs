namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battle
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