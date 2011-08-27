﻿using BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker.Interfaces;
using Ninject;

namespace BetTeamsBattle.Screenshots.AwesomiumScreenshotMaker
{
    internal class ScreenshotMakerFactory : IScreenshotMakerFactory
    {
        private readonly IKernel _kernel;

        public ScreenshotMakerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        #region IScreenshotMakerFactory Members

        public IScreenshotMaker Create()
        {
            return _kernel.Get<IScreenshotMaker>();
        }

        #endregion
    }
}