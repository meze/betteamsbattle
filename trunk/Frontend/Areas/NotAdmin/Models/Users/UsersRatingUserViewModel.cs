namespace BetTeamsBattle.Frontend.Areas.NotAdmin.Models.Users
{
    public class UsersRatingUserViewModel
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public double Rating { get; set; }

        public UsersRatingUserViewModel(long id, string login, double rating)
        {
            Id = id;
            Login = login;
            Rating = rating;
        }
    }
}