namespace BetTeamsBattle.Data.Model.Entities
{
    public class TeamBattleStatistics
    {
        public virtual long Id { get; set; }
        public virtual long BattleId { get; set; }
        public virtual long TeamId { get; set; }
        public virtual double Gain { get; set; }
        public virtual int OpenedBetsCount { get; set; }
        public virtual int ClosedBetsCount { get; set; }

        public Battle Battle { get; set; }
        public Team Team { get; set; }

        public TeamBattleStatistics()
        {
            Gain = 0;
            OpenedBetsCount = 0;
            ClosedBetsCount = 0;
        }

        public TeamBattleStatistics(long battleId, long teamId) : this()
        {
            BattleId = battleId;
            TeamId = teamId;
        }
    }
}