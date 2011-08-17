namespace BetTeamsBattle.Data.Model.Entities
{
    public class UserStatistics
    {
        public virtual long Id { get; set; }
        public virtual double Rating { get; set; }

        public virtual User User { get; set; }
    }
}