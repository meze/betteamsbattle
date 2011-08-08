using System;

namespace BetTeamsBattle.Data.Repositories.Specific.Battle
{
    public class BattleSpecifications
    {
        public static LinqSpec<Model.Entities.Battle> StartDateIsInFuture()
        {
            var now = DateTime.UtcNow;
            return LinqSpec.For<Model.Entities.Battle>(b => b.StartDate > now);
        }

        public static LinqSpec<Model.Entities.Battle> NotFinishedOrNotStarted()
        {
            var now = DateTime.UtcNow;
            return LinqSpec.For<Model.Entities.Battle>(b => b.StartDate > now || b.EndDate > now);
        }
    }
}