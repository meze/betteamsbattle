using System.Collections.Generic;
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
             return Get(BattleUserSpecifications.UserIdIsEqualTo(userId) & BattleUserSpecifications.UserIdIsEqualTo(userId)).
                 Where(bu => bu.DateTime == Context.BattlesUsers.
                                                Where(bu1 => bu1.UserId == userId && bu1.BattleId == bu.BattleId).
                                                Max(bu1 => bu1.DateTime));
         }
    }
}