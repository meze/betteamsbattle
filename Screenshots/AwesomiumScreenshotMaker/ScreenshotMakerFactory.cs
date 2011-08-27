using BetTeamsBattle.AwesomiumScreenshotMaker.Interfaces;
using Ninject;

namespace BetTeamsBattle.AwesomiumScreenshotMaker
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