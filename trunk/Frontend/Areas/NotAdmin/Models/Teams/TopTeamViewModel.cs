namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Teams
{
    public class TopTeamViewModel
    {
        public long TeamId { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public bool IsPro { get; set; }

        public TopTeamViewModel()
        {
        }

        public TopTeamViewModel(long teamId, string login, double rating, bool isPro)
        {
            TeamId = teamId;
            Title = login;
            Rating = rating;
            IsPro = isPro;
        }
    }
}