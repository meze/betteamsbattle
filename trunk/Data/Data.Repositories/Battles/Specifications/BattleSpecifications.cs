using BetTeamsBattle.Data.Model.Entities;
using LinqSpecs;
using System;

namespace BetTeamsBattle.Data.Repositories.Battles.Specifications
{
    internal class BattleSpecifications
    {
        public static Specification<Battle> FilterFutureBattles()
        {
            return new AdHocSpecification<Battle>(b => b.StartDate > DateTime.Now);
        }
    }
}