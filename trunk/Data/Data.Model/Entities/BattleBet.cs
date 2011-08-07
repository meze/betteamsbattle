using System;
using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class BattleBet : IEntity
    {
        public virtual long Id { get; set; }
        public virtual long BattleId { get; set; }
        public virtual long UserId { get; set; }
        public virtual double Bet { get; set; }
        public virtual double Coefficient { get; set; }
        public virtual string Url { get; set; }
        public virtual DateTime OpenDateTime { get; set; }
        public virtual DateTime? CloseDateTime { get; set; }
        public virtual bool? Success { get; set; }

        public virtual Battle Battle { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<QueuedBetUrl> QueuedBetUrls { get; set; }

        public BattleBet()
        {
        }

        public BattleBet(long battleId, long userId, double bet, double coefficient, string url)
        {
            BattleId = battleId;
            UserId = userId;
            Bet = bet;
            Coefficient = coefficient;
            Url = url;
            OpenDateTime = DateTime.UtcNow;
        }
    }
}