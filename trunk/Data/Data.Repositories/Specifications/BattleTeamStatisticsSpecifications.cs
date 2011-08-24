using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class BattleTeamStatisticsSpecifications
    {
        public static LinqSpec<BattleTeamStatistics> BattleIdIsEqualTo(long battleId)
        {
            return LinqSpec.For<BattleTeamStatistics>(bts => bts.BattleId == battleId);
        }

        public static LinqSpec<BattleTeamStatistics> TeamIdIsEqualTo(long teamId)
        {
            return LinqSpec.For<BattleTeamStatistics>(bts => bts.TeamId == teamId);
        }

         public static LinqSpec<BattleTeamStatistics> BattleIdAndTeamIdAreEqualTo(long battleId, long teamId)
         {
             return BattleIdIsEqualTo(battleId) && TeamIdIsEqualTo(teamId);
         }
    }
}