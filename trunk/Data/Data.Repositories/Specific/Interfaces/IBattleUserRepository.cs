﻿using System.Collections.Generic;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Base;

namespace BetTeamsBattle.Data.Repositories.Specific.Interfaces
{
    public interface IBattleUserRepository : IRepository<BattleUser>
    {
        IQueryable<BattleUser> GetLastBattleUsers(long userId, IEnumerable<long> battlesIds);
    }
}