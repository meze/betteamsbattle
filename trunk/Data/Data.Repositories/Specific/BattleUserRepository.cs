﻿using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Specific.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;

namespace BetTeamsBattle.Data.Repositories.Specific
{
    internal class BattleUserRepository : Repository<BattleUser>, IBattleUserRepository
    {
         public IQueryable<BattleUser> GetLastBattleUsers(long userId, IEnumerable<long> battlesIds)
         {
             //ToDo: Report to MySQL team that query with GroupBy().Select(g.OrderByDescending(DateTime).FirstOrDefault()) doesn't work
             return Get(BattleUserSpecifications.UserIdIsEqualTo(userId) &&
                        BattleUserSpecifications.BattleIdIsContainedIn(battlesIds)).
                 Where(bu => bu.DateTime == Context.BattlesUsers.
                                                Where(BattleUserSpecifications.BattleIdAndUserIdAreEqualTo(bu.BattleId, userId)).
                                                Max(bu1 => bu1.DateTime));
         }
    }
}