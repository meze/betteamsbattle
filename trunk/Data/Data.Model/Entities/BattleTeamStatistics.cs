namespace BetTeamsBattle.Data.Model.Entities
{
    public class BattleTeamStatistics
    {
        public virtual long Id { get; set; }
        public virtual long BattleId { get; set; }
        public virtual long TeamId { get; set; }
        public virtual double Balance { get; set; }
        public virtual int OpenedBetsCount { get; set; }
        public virtual int ClosedBetsCount { get; set; }

        public Battle Battle { get; set; }
        public Team Team { get; set; }

        public BattleTeamStatistics()
        {
            OpenedBetsCount = 0;
            ClosedBetsCount = 0;
        }

        public BattleTeamStatistics(long battleId, long teamId, double balance) : this()
        {
            BattleId = battleId;
            TeamId = teamId;
            Balance = balance;
        }
    }
}