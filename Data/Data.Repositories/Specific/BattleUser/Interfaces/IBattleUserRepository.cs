using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Repositories.Base.Interfaces;

namespace BetTeamsBattle.Data.Repositories.Specific.BattleUser.Interfaces
{
    public interface IBattleUserRepository : IRepository<Model.Entities.BattleUser>
    {
        IQueryable<Model.Entities.BattleUser> GetLastBattleUsers(long userId, IEnumerable<long> battlesIds);
    }
}