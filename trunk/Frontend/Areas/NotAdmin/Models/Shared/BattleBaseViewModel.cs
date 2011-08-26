namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models
{
    public class BattleBaseViewModel
    {
        public long BattleId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Budget { get; set; }
        public int BetLimit { get; set; }

        public BattleBaseViewModel(long battleId, string startDate, string endDate, int budget, int betLimit)
        {
            BattleId = battleId;
            StartDate = startDate;
            EndDate = endDate;
            Budget = budget;
            BetLimit = betLimit;
        }
    }
}