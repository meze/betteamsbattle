using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;

namespace BetTeamsBattle.Data.Repositories.EntityRepositories.Interfaces
{
    public interface IBattleUserRepository : IRepository<BattleUser>
    {
        IQueryable<BattleUser> GetLastBattleUsers(long userId, IEnumerable<long> battlesIds);
    }
}