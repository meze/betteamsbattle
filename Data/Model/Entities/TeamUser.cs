using System;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class TeamUser
    {
        public virtual long Id { get; set; }
        public virtual long TeamId { get; set; }
        public virtual long UserId { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual sbyte Action { get; set; }
        public virtual TeamUserAction ActionEnum
        {
            get { return (TeamUserAction)Action; }
            set { Action = (sbyte)value; }
        }

        public virtual Team Team { get; set; }
        public virtual User User { get; set; }

        internal TeamUser()
        {
            DateTime = DateTime.UtcNow;
        }

        public TeamUser(long userId, TeamUserAction action) : this()
        {
            UserId = userId;
            ActionEnum = action;
        }

        public TeamUser(User user, TeamUserAction action) : this()
        {
            User = user;
            ActionEnum = action;
        }
    }
}