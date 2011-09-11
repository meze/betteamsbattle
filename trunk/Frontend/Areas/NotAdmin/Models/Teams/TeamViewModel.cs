namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Teams
{
    public class TeamViewModel
    {
        public long TeamId { get; set; }
        public string Title { get; set; }

        public TeamStatisticsViewModel Statistics { get; set; }
    }
}