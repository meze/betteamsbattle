namespace BetTeamsBattle.Data.Model.Entities
{
    public class BattleUserStatistics
    {
        public virtual long Id { get; set; }
        public virtual long BattleId { get; set; }
        public virtual long UserId { get; set; }
        public virtual double Balance { get; set; }
        public virtual int OpenedBetsCount { get; set; }
        public virtual int ClosedBetsCount { get; set; }

        public virtual Battle Battle { get; set; }
        public virtual User User { get; set; }

        public BattleUserStatistics()
        {
        }

        public BattleUserStatistics(long battleId, long userId, double balance)
        {
            BattleId = battleId;
            UserId = userId;

            Balance = balance;
            OpenedBetsCount = 0;
            ClosedBetsCount = 0;
        }
    }
}