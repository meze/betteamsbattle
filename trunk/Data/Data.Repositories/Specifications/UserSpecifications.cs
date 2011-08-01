using BetTeamsBattle.Data.Model.Entities;
using LinqSpecs;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class UserSpecifications
    {
        public static Specification<User> LoginIsEqual(string login)
        {
            return new AdHocSpecification<User>(u => u.Login == login);
        }

        public static Specification<User> OpenIdUrlIsEqual(string openIdUrl)
        {
            return new AdHocSpecification<User>(u => u.OpenIdUrl == openIdUrl);
        }
    }
}