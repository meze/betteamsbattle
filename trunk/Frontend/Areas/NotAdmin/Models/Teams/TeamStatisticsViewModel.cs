namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Teams
{
    public class TeamStatisticsViewModel
    {
        public string Title { get; set; }

        public double Rating { get; set; }
        public int ClosedBetsCount { get; set; }
        public int OpenedBetsCount { get; set; }

        public TeamStatisticsViewModel(string title, double rating, int closedBetsCount, int openedBetsCount)
        {
            Title = title;
            Rating = rating;
            ClosedBetsCount = closedBetsCount;
            OpenedBetsCount = openedBetsCount;
        }
    }
}