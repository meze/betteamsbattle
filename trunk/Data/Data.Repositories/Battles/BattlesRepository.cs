using System;
using System.Linq;
using BetTeamsBattle.Data.Model.Entities;
using BetTeamsBattle.Data.Repositories.Battles.Interfaces;
using BetTeamsBattle.Data.Repositories.Battles.Specifications;

namespace BetTeamsBattle.Data.Repositories.Battles
{
    public class BattlesRepository : Repository<Battle>, IBattlesRepository
    {
        public DateTime? NextBattleStartDate()
        {
            return Context.Battles.
                Where(BattleSpecifications.FilterFutureBattles().IsSatisfiedBy()).
                OrderBy(b => b.StartDate).
                Select(b => b.StartDate).
                FirstOrDefault();
        }
    }
}