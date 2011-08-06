using System;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class BattleBet : IEntity
    {
        public long Id { get; set; }
        public long BattleId { get; set; }
        public long UserId { get; set; }
        public double Bet { get; set; }
        public double Coefficient { get; set; }
        public string Url { get; set; }
        public DateTime OpenDateTime { get; set; }
        public DateTime? CloseDateTime { get; set; }

        public Battle Battle { get; set; }
        public User User { get; set; }
    }
}