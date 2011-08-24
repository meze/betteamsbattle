namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battles
{
    public class BattleViewModel
    {
        public long BattleId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int Budget { get; set; }
        public int BetLimit { get; set; }

        public bool BattleIsActive { get; set; }

        public double Earned { get; set; }
        public double EarnedPercents { get; set; }
        public int TotalBetsCount { get; set; }
        public int OpenBetsCount { get; set; }

        public BattleViewModel(long battleId, string startDate, string endDate, int budget, int betLimit, bool battleIsActive)
        {
            BattleId = battleId;
            StartDate = startDate;
            EndDate = endDate;
            Budget = budget;
            BetLimit = betLimit;
            BattleIsActive = battleIsActive;
        }
    }
}