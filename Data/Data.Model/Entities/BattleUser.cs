using System;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class BattleUser
    {
        public virtual long Id { get; set; }
        public virtual long BattleId { get; set; }
        public virtual long UserId { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual BattleAction Action { get; set; }

        public virtual Battle Battle { get; set; }
        public virtual User User { get; set; }
    }
}