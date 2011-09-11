namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battles
{
    public class BattleViewModel : BattleBaseViewModel
    {
        public bool BattleIsActive { get; set; }

        public BattleStatisticsViewModel BattleStatistics { get; set; }

        public BattleViewModel(long battleId, string startDate, string endDate, int budget, int betLimit, bool battleIsActive) : base(battleId, startDate, endDate, budget, betLimit)
        {
            BattleIsActive = battleIsActive;
        }
    }
}