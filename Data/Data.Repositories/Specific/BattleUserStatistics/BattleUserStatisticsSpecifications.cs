namespace BetTeamsBattle.Data.Repositories.Specific.BattleUserStatistics
{
    public class BattleUserStatisticsSpecifications
    {
        public static LinqSpec<Model.Entities.BattleUserStatistics> BattleIdIsEqualTo(long battleId)
        {
            return LinqSpec.For<Model.Entities.BattleUserStatistics>(bus => bus.BattleId == battleId);
        }

        public static LinqSpec<Model.Entities.BattleUserStatistics> UserIdIsEqualTo(long userId)
        {
            return LinqSpec.For<Model.Entities.BattleUserStatistics>(bus => bus.UserId == userId);
        }

        public static LinqSpec<Model.Entities.BattleUserStatistics> BattleIdAndUserIdAreEqualTo(long battleId, long userId)
        {
            return BattleIdIsEqualTo(battleId) && UserIdIsEqualTo(userId);
        }
    }
}