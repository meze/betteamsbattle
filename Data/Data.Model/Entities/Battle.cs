using System;
using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class Battle : IEntity
    {
        public virtual long Id { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual sbyte BattleType { get; set; }
        public virtual BattleType BattleTypeEnum 
        {
            get { return (BattleType)BattleType; }
            set { BattleType = (sbyte) value; } 
        }
        public virtual int Budget { get; set; }
        public virtual int BetLimit { get; set; }

        public virtual ICollection<BattleUser> BattleUsers { get; set; }
        public virtual ICollection<BattleBet> BattleBets { get; set; }
        public virtual ICollection<BattleUserStatistics> BattleUsersStatistics { get; set; }

        public Battle()
        {
        }

        public Battle(DateTime startDate, DateTime endDate, BattleType battleType, int budget)
        {
            StartDate = startDate;
            EndDate = endDate;
            BattleTypeEnum = battleType;
            Budget = budget;
        }
    }
}