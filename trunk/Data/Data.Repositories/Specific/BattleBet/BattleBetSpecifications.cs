using BattleBet = BetTeamsBattle.Data.Model.Entities.BattleBet;

namespace BetTeamsBattle.Data.Repositories.Specific.BattleBet
{
    public class BattleBetSpecifications
    {
        public static LinqSpec<BetTeamsBattle.Data.Model.Entities.BattleBet> BattleIdIsEqualTo(long battleId)
        {
            return LinqSpec.For<BetTeamsBattle.Data.Model.Entities.BattleBet>(bb => bb.BattleId == battleId);
        }

        public static LinqSpec<BetTeamsBattle.Data.Model.Entities.BattleBet> UserIdIsEqualTo(long userId)
        {
            return LinqSpec.For<BetTeamsBattle.Data.Model.Entities.BattleBet>(bb => bb.UserId == userId);
        }
    }
}