using System;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class BattleUser : IEntity
    {
        public virtual long Id { get; set; }
        public virtual long BattleId { get; set; }
        public virtual long UserId { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual sbyte Action { get; set; }
        public virtual BattleAction ActionEnum
        {
            get { return (BattleAction) Action; }
            set { Action = (sbyte) value; }
        }

        public virtual Battle Battle { get; set; }
        public virtual User User { get; set; }

        public BattleUser()
        {
        }

        public BattleUser(long battleId, long userId, BattleAction battleAction)
        {
            BattleId = battleId;
            UserId = userId;
            DateTime = DateTime.UtcNow;
            ActionEnum = battleAction;
        }
    }
}