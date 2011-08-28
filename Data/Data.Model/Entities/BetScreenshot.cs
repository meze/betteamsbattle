using System;
using BetTeamsBattle.Data.Model.Enums;
using BetTeamsBattle.Data.Model.Interfaces;

namespace BetTeamsBattle.Data.Model.Entities
{
    public class BetScreenshot : IEntity
    {
        public virtual long Id { get; set; }
        public virtual DateTime CreationDateTime { get; set; }
        public virtual DateTime? StartedProcessingDateTime { get; set; }
        public virtual DateTime? StartedScreenshotRetrievalDateTime { get; set; }
        public virtual DateTime? FinishedScreenshotRetrievalDateTime { get; set; }
        public virtual DateTime? StartedScreenshotSavingDateTime { get; set; }
        public virtual DateTime? FinishedScreenshotSavingDateTime { get; set; }
        public virtual DateTime? FinishedProcessingDateTime { get; set; }
        public virtual sbyte Status { get; set; }
        public virtual BetScreenshotStatus StatusEnum
        {
            get { return (BetScreenshotStatus)Status; }
            set { Status = (sbyte) value; }
        }

        public BetScreenshot()
        {
            CreationDateTime = DateTime.UtcNow;
            StatusEnum = BetScreenshotStatus.NotProcessed;
        }
    }
}