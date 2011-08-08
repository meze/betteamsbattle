using BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker.Interfaces;
using Ninject;

namespace BetTeamsBattle.ScreenShotsMaker.ScreenShotMaker
{
    internal class ScreenShotMakerFactory : IScreenShotMakerFactory
    {
        private readonly IKernel _kernel;

        public ScreenShotMakerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        #region IScreenShotMakerFactory Members

        public IScreenShotMaker Create()
        {
            return _kernel.Get<IScreenShotMaker>();
        }

        #endregion
    }
}