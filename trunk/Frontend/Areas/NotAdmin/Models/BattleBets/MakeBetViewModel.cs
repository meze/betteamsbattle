namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.BattleBets
{
    public class MakeBetViewModel
    {
        public long BattleId { get; set; }

        public string Title { get; set; }
        public double Bet { get; set; }
        public double Coefficient { get; set; }
        public string Url { get; set; }
        public bool IsPrivate { get; set; }
    }
}