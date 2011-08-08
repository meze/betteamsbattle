using System.Collections.Generic;
using System.Linq;

namespace BetTeamsBattle.Data.Repositories.Specific.BattleUser
{
    public class BattleUserSpecifications
    {
        public static LinqSpec<Model.Entities.BattleUser> UserIdIsEqualTo(long userId)
        {
            return LinqSpec.For<Model.Entities.BattleUser>(bu => bu.UserId == userId);
        }

        public static LinqSpec<Model.Entities.BattleUser> BattleIdIsEqualTo(long battleId)
        {
            return LinqSpec.For<Model.Entities.BattleUser>(bu => bu.BattleId == battleId);
        }

        public static LinqSpec<Model.Entities.BattleUser> BattleIdAndUserIdAreEqualTo(long battleId, long userId)
        {
            return BattleIdIsEqualTo(battleId) && UserIdIsEqualTo(userId);
        }

        public static LinqSpec<Model.Entities.BattleUser> BattleIdIsContainedIn(IEnumerable<long> battlesIds)
        {
            return LinqSpec.For<Model.Entities.BattleUser>(bu => battlesIds.Contains(bu.BattleId));
        }
    }
}