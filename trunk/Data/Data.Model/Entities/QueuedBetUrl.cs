using System;
using BetTeamsBattle.Data.Model.Enums;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class QueuedBetUrl
    {
        public virtual long Id { get; set; }
        public virtual long BattleBetId { get; set; }
        public virtual string Url { get; set; }
        public virtual sbyte Type { get; set; }
        private QueuedBetUrlType TypeEnum
        {
            get { return (QueuedBetUrlType) Type; }
            set { Type = (sbyte) value; }
        }
        public virtual DateTime StartDateTime { get; set; }
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
            StartDateTime = DateTime.UtcNow;
        }
    }
}