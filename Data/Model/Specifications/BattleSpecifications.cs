using System;
using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Model.Specifications
{
    public class BattleSpecifications
    {
        public static LinqSpec<Battle> Current()
        {
            DateTime now = DateTime.UtcNow;
            return LinqSpec.For<Battle>(b => b.StartDate <= now && b.EndDate >= now);
        }

        public static LinqSpec<Battle> NotStarted()
        {
            DateTime now = DateTime.UtcNow;
            return LinqSpec.For<Battle>(b => b.StartDate > now);
        }

        public static LinqSpec<Battle> Finished()
        {
            DateTime now = DateTime.UtcNow;
            return LinqSpec.For<Battle>(b => b.EndDate < now);
        }
    }
}