using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class TeamStatistics : IEntity
    {
        public virtual long Id { get; set; }
        public virtual double Rating { get; set; }
        public virtual int OpenedBetsCount { get; set; }
        public virtual int ClosedBetsCount { get; set; }

        public virtual Team Team { get; set; }

        public TeamStatistics()
        {
            Rating = 0;
            OpenedBetsCount = 0;
            ClosedBetsCount = 0;
        }
    }
}