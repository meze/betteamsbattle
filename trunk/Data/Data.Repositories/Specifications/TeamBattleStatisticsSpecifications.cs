using BetTeamsBattle.Data.Model.Entities;

namespace BetTeamsBattle.Data.Repositories.Specifications
{
    public class TeamBattleStatisticsSpecifications
    {
        public static LinqSpec<TeamBattleStatistics> BattleIdIsEqualTo(long battleId)
        {
            return LinqSpec.For<TeamBattleStatistics>(bts => bts.BattleId == battleId);
        }

        public static LinqSpec<TeamBattleStatistics> TeamIdIsEqualTo(long teamId)
        {
            return LinqSpec.For<TeamBattleStatistics>(bts => bts.TeamId == teamId);
        }

         public static LinqSpec<TeamBattleStatistics> BattleIdAndTeamIdAreEqualTo(long battleId, long teamId)
         {
             return BattleIdIsEqualTo(battleId) && TeamIdIsEqualTo(teamId);
         }
    }
}