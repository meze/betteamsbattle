using BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces;
using Ninject;

namespace BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker
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