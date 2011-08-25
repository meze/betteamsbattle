using System;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class BetScreenshot : IEntity
    {
        public virtual long Id { get; set; }
        public virtual long BattleBetId { get; set; }
        public virtual DateTime CreationDateTime { get; set; }
        public virtual DateTime? ProcessingStartDateTime { get; set; }
        public virtual DateTime? ProcessingFinishDateTime { get; set; }

        public virtual BattleBet BattleBet { get; set; }

        public BetScreenshot()
        {
        }

        public BetScreenshot(long battleBetId)
        {
            BattleBetId = battleBetId;
            CreationDateTime = DateTime.UtcNow;
        }
    }

    public enum BetScreenshotStatus
    {
        
    }
}