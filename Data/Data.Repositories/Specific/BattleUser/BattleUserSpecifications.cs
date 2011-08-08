using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class BattleUserSpecifications
    {
        public static LinqSpec<BattleUser> UserIdIsEqualTo(long userId)
        {
            return LinqSpec.For<BattleUser>(bu => bu.UserId == userId);
        }

        public static LinqSpec<BattleUser> BattleIdIsEqualTo(long battleId)
        {
            return LinqSpec.For<BattleUser>(bu => bu.BattleId == battleId);
        }

        public static LinqSpec<BattleUser> BattleIdAndUserIdAreEqualTo(long battleId, long userId)
        {
            return BattleIdIsEqualTo(battleId) && UserIdIsEqualTo(userId);
        }

        public static LinqSpec<BattleUser> BattleIdIsContainedIn(IEnumerable<long> battlesIds)
        {
            return LinqSpec.For<BattleUser>(bu => battlesIds.Contains(bu.BattleId));
        }
    }
}