using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class BattleUserStatisticsSpecifications
    {
        public static LinqSpec<BattleUserStatistics> BattleIdIsEqualTo(long battleId)
        {
            return LinqSpec.For<BattleUserStatistics>(bus => bus.BattleId == battleId);
        }

        public static LinqSpec<BattleUserStatistics> UserIdIsEqualTo(long userId)
        {
            return LinqSpec.For<BattleUserStatistics>(bus => bus.UserId == userId);
        }

        public static LinqSpec<BattleUserStatistics> BattleIdAndUserIdAreEqualTo(long battleId, long userId)
        {
            return BattleIdIsEqualTo(battleId) && UserIdIsEqualTo(userId);
        }
    }
}