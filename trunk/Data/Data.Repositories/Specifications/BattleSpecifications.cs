using BetTeamsBattle.Data.Model.Entities;
using LinqSpecs;
using System;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class BattleSpecifications
    {
        public static Specification<Battle> StartDateIsInFuture()
        {
            return new AdHocSpecification<Battle>(b => b.StartDate > DateTime.UtcNow);
        }

        public static Specification<Battle> NotFinishedOrNotStarted()
        {
            return new AdHocSpecification<Battle>(b => b.EndDate < DateTime.UtcNow || b.StartDate > DateTime.UtcNow);
        }
    }
}