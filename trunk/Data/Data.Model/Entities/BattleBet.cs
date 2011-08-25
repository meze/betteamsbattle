﻿using System;
using System.Collections.Generic;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class BattleBet : IEntity
    {
        public virtual long Id { get; set; }
        public virtual long BattleId { get; set; }
        public virtual long TeamId { get; set; }
        public virtual long UserId { get; set; }
        public virtual string Title { get; set; }
        public virtual double Bet { get; set; }
        public virtual double Coefficient { get; set; }
        public virtual string Url { get; set; }
        public virtual DateTime OpenDateTime { get; set; }
        public virtual long OpenBetScreenshotId { get; set; }
        public virtual DateTime? CloseDateTime { get; set; }
        public virtual long? CloseBetScreenshotId { get; set; }
        public virtual bool? Success { get; set; }
        public virtual bool IsPrivate { get; set; }

        public virtual Battle Battle { get; set; }
        public virtual Team Team { get; set; }
        public virtual User User { get; set; }
        public virtual BetScreenshot OpenBetScreenshot { get; set; }
        public virtual BetScreenshot CloseBetScreenshot { get; set; }

        public BattleBet()
        {
        }

        public BattleBet(long battleId, long teamId, long userId, string title, double bet, double coefficient, string url, bool isPrivate) : this()
        {
            BattleId = battleId;
            TeamId = teamId;
            UserId = userId;
            Title = title;
            Bet = bet;
            Coefficient = coefficient;
            Url = url;
            OpenDateTime = DateTime.UtcNow;
            IsPrivate = isPrivate;
        }
    }
}