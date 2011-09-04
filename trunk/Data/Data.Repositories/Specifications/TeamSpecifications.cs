using System.Linq;
using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class TeamSpecifications
    {
         public static LinqSpec<Team> UserIsMember(long userId)
         {
             return LinqSpec.For<Team>(t => t.TeamUsers.Any(tu => tu.UserId == userId));
         }

        public static LinqSpec<Team> IsPro()
        {
            return LinqSpec.For<Team>(t => t.IsPro);
        }
    }
}