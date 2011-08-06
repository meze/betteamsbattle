using BetTeamsBattle.Data.Model.Entities;
using System;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class BattleSpecifications
    {
        public static LinqSpec<Battle> StartDateIsInFuture()
        {
            var now = DateTime.UtcNow;
            return LinqSpec.For<Battle>(b => b.StartDate > now);
        }

        public static LinqSpec<Battle> NotFinishedOrNotStarted()
        {
            var now = DateTime.UtcNow;
            return LinqSpec.For<Battle>(b => b.EndDate < now || b.StartDate > now);
        }
    }
}