using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Model.Specifications
{
    public class UserSpecifications
    {
        public static LinqSpec<User> LoginIsEqual(string login)
        {
            return LinqSpec.For<User>(u => u.Login == login);
        }

        public static LinqSpec<User> OpenIdUrlIsEqual(string openIdUrl)
        {
            return LinqSpec.For<User>(u => u.OpenIdUrl == openIdUrl);
        }
    }
}