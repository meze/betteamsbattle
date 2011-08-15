using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class BattleBetSpecifications
    {
        public static LinqSpec<BattleBet> BattleIdIsEqualTo(long battleId)
        {
            return LinqSpec.For<BattleBet>(bb => bb.BattleId == battleId);
        }

        public static LinqSpec<BattleBet> UserIdIsEqualTo(long userId)
        {
            return LinqSpec.For<BattleBet>(bb => bb.UserId == userId);
        }

        public static LinqSpec<BattleBet> BattleIdAndUserIdAreEqualTo(long battleId, long userId)
        {
            return BattleIdIsEqualTo(battleId) && UserIdIsEqualTo(userId);
        }
    }
}