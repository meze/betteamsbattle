namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Battles
{
    public class BattleStatisticsViewModel
    {
        public double Balance { get; set; }
        public double Gain { get; set; }
        public double GainInPercents { get; set; }
        public int TotalBetsCount { get; set; }
        public int OpenBetsCount { get; set; } 
    }
}