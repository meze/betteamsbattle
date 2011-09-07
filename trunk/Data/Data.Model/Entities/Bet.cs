using System;
using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class Bet : IEntity
    {
        public virtual long Id { get; set; }
        public virtual long BattleId { get; set; }
        public virtual long TeamId { get; set; }
        public virtual long UserId { get; set; }
        public virtual string Title { get; set; }
        public virtual double Amount { get; set; }
        public virtual double Coefficient { get; set; }
        public virtual string Url { get; set; }
        public virtual DateTime OpenDateTime { get; set; }
        public virtual long OpenBetScreenshotId { get; set; }
        public virtual DateTime? CloseDateTime { get; set; }
        public virtual long? CloseBetScreenshotId { get; set; }
        public virtual sbyte Status { get; set; }
        public virtual BattleBetStatus StatusEnum { get { return (BattleBetStatus)Status; } set { Status = (sbyte) value; } }
        public virtual double? Result { get; set; }
        public virtual bool IsPrivate { get; set; }
        public bool IsClosed
        {
            get { return CloseDateTime != null; }
        }

        public virtual Battle Battle { get; set; }
        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
        public virtual BetScreenshot OpenBetScreenshot { get; set; }
        public virtual BetScreenshot CloseBetScreenshot { get; set; }

        public Bet()
        {
        }

        public Bet(long battleId, long teamId, long userId, string title, double amount, double coefficient, string url, bool isPrivate) : this()
        {
            BattleId = battleId;
            TeamId = teamId;
            UserId = userId;
            Title = title;
            Amount = amount;
            Coefficient = coefficient;
            Url = url;
            OpenDateTime = DateTime.UtcNow;
            IsPrivate = isPrivate;
            StatusEnum = BattleBetStatus.NotFinished;
        }
    }
}