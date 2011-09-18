using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Model.Specifications
{
    public class BetSpecifications
    {
        public static LinqSpec<Bet> BattleIdIsEqualTo(long battleId)
        {
            return LinqSpec.For<Bet>(bb => bb.BattleId == battleId);
        }

        public static LinqSpec<Bet> TeamIdIsEqualTo(long teamId)
        {
            return LinqSpec.For<Bet>(bb => bb.TeamId == teamId);
        }

        public static LinqSpec<Bet> UserIdIsEqualTo(long userId)
        {
            return LinqSpec.For<Bet>(bb => bb.UserId == userId);
        }

        public static LinqSpec<Bet> BattleIdAndUserIdAreEqualTo(long battleId, long userId)
        {
            return BattleIdIsEqualTo(battleId) && UserIdIsEqualTo(userId);
        }

        public static LinqSpec<Bet> BetScreenshotOwner(long betScreenshotId)
        {
            return LinqSpec.For<Bet>(bb => bb.OpenBetScreenshotId == betScreenshotId || bb.CloseBetScreenshotId == betScreenshotId);
        }
    }
}