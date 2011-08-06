using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class BattleUserSpecifications
    {
        public static LinqSpec<BattleUser> UserIdIsEqualTo(long userId)
        {
            return LinqSpec.For<BattleUser>(bu => bu.UserId == userId);
        }

        public static LinqSpec<BattleUser> BattleIdIsContainedIn(IEnumerable<long> battlesIds)
        {
            return LinqSpec.For<BattleUser>(bu => battlesIds.Contains(bu.BattleId));
        }
    }
}