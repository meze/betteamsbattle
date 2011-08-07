using System;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class QueuedBetUrl : IEntity
    {
        public virtual long Id { get; set; }
        public virtual long BattleBetId { get; set; }
        public virtual string Url { get; set; }
        public virtual sbyte Type { get; set; }
        public QueuedBetUrlType TypeEnum
        {
            get { return (QueuedBetUrlType) Type; }
            set { Type = (sbyte) value; }
        }
        public virtual DateTime CreationDateTime { get; set; }
        public virtual DateTime? StartDateTime { get; set; }
        public virtual DateTime? FinishDateTime { get; set; }

        public virtual BattleBet BattleBet { get; set; }

        public QueuedBetUrl()
        {
        }

        public QueuedBetUrl(long battleBetId, string url, QueuedBetUrlType type)
        {
            BattleBetId = battleBetId;
            Url = url;
            TypeEnum = type;
            CreationDateTime = DateTime.UtcNow;
        }
    }
}