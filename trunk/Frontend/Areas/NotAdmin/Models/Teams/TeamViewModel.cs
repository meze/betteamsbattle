namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Teams
{
    public class TeamViewModel
    {
        public long TeamId { get; set; }
        public string Title { get; set; }

        public double Rating { get; set; }
        public int TotalBetsCount { get; set; }
        public int OpenedBetsCount { get; set; }
    }
}