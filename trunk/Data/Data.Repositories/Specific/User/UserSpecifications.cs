namespace BetTeamsBattle.Data.Repositories.Specific.User
{
    public class UserSpecifications
    {
        public static LinqSpec<Model.Entities.User> LoginIsEqual(string login)
        {
            return LinqSpec.For<Model.Entities.User>(u => u.Login == login);
        }

        public static LinqSpec<Model.Entities.User> OpenIdUrlIsEqual(string openIdUrl)
        {
            return LinqSpec.For<Model.Entities.User>(u => u.OpenIdUrl == openIdUrl);
        }
    }
}