using System;
using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class BattleSpecifications
    {
        public static LinqSpec<Battle> StartDateIsInFuture()
        {
            DateTime now = DateTime.UtcNow;
            return LinqSpec.For<Battle>(b => b.StartDate > now);
        }

        public static LinqSpec<Battle> NotFinishedOrNotStarted()
        {
            DateTime now = DateTime.UtcNow;
            return LinqSpec.For<Battle>(b => b.StartDate > now || b.EndDate > now);
        }
    }
}