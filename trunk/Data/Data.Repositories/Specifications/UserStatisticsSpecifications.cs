using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class UserStatisticsSpecifications
    {
         public static LinqSpec<UserStatistics> UserIdIsEqualTo(long userId)
         {
             return LinqSpec.For<UserStatistics>(us => us.Id == userId);
         }
    }
}