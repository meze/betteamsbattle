﻿using System.Threading;
using Amazon.S3;

namespace BetTeamsBattle.ScreenShotsMaker.QueuedBetUrlProcessor.Interfaces
{
    internal interface IQueuedBetUrlProcessor
    {
        void Process(long queuedBetUrlId, AmazonS3 amazonS3Client, SynchronizationContext synchronizationContext);
    }
}