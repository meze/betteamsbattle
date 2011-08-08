using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Repositories.Base;
using BetTeamsBattle.Data.Repositories.Specific.BattleUser.Interfaces;

namespace BetTeamsBattle.Data.Repositories.Specific.BattleUser
{
    internal class BattleUserRepository : Repository<Model.Entities.BattleUser>, IBattleUserRepository
    {
         public IQueryable<Model.Entities.BattleUser> GetLastBattleUsers(long userId, IEnumerable<long> battlesIds)
         {
             //ToDo: Report to MySQL team that query with GroupBy().Select(g.OrderByDescending(DateTime).FirstOrDefault()) doesn't work
             return Get(BattleUserSpecifications.UserIdIsEqualTo(userId) &&
                        BattleUserSpecifications.BattleIdIsContainedIn(battlesIds)).
                 Where(bu => bu.DateTime == Context.BattlesUsers.
                                                Where(bu1 => bu1.BattleId == bu.BattleId && bu1.UserId == userId).
                                                Max(bu1 => bu1.DateTime));
         }
    }
}