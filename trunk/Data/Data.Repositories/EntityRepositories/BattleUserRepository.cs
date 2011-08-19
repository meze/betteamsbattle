﻿using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.EntityRepositories.Interfaces;
using BetTeamsBattle.Data.Repositories.Specifications;

namespace BetTeamsBattle.Data.Repositories.EntityRepositories
{
    internal class BattleUserRepository : Repository<BattleUser>, IBattleUserRepository
    {
        public IQueryable<BattleUser> GetLastBattleUser(long userId, long battleId)
        {
            //ToDo: Report to MySQL team that query with GroupBy().Select(g.OrderByDescending(DateTime).FirstOrDefault()) doesn't work
            return Get(BattleUserSpecifications.UserIdIsEqualTo(userId) &&
                       BattleUserSpecifications.BattleIdIsEqualTo(battleId)).
                Where(bu => bu.DateTime == Context.BattlesUsers.
                                               Where(bu1 => bu1.BattleId == bu.BattleId && bu1.UserId == userId).
                                               Max(bu1 => bu1.DateTime));
        }
    }
}