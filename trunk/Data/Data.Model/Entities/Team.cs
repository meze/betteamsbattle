using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class Team : IEntity
    {
        public virtual long Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Site { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsPersonal { get; set; }
        public virtual double Rating { get; set; }
        public virtual bool IsPro { get; set; }

        private ICollection<TeamUser> _teamUsers;
        public virtual ICollection<TeamUser> TeamUsers
        {
            get 
            {  
                if (_teamUsers == null)
                    _teamUsers = new List<TeamUser>();
                return _teamUsers;
            }
            set { _teamUsers = value; }
        }
        public virtual ICollection<BattleBet> BattlesBets { get; set; }
        public virtual ICollection<BattleTeamStatistics> BattlesTeamStatistics { get; set; }

        public Team()
        {
        }

        public Team(string title, bool isPersonal, double rating)
            : this()
        {
            Title = title;
            IsPersonal = isPersonal;
            Rating = rating;
            Rating = 0;
        }
    }
}