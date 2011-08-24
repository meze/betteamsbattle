namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Teams
{
    public class TopTeamsTeamViewModel
    {
        public long TeamId { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }

        public TopTeamsTeamViewModel()
        {
        }

        public TopTeamsTeamViewModel(long teamId, string login, double rating)
        {
            TeamId = teamId;
            Title = login;
            Rating = rating;
        }
    }
}