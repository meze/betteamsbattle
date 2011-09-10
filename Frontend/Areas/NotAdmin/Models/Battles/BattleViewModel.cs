namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battles
{
    public class BattleViewModel : BattleBaseViewModel
    {
        public bool BattleIsActive { get; set; }

        public double Balance { get; set; }
        public double Gain { get; set; }
        public double GainInPercents { get; set; }
        public int TotalBetsCount { get; set; }
        public int OpenBetsCount { get; set; }

        public BattleViewModel(long battleId, string startDate, string endDate, int budget, int betLimit, bool battleIsActive) : base(battleId, startDate, endDate, budget, betLimit)
        {
            BattleIsActive = battleIsActive;
        }
    }
}