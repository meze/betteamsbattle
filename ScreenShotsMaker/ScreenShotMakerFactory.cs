using Ninject;
using ScreenShotsMaker.Interfaces;

namespace ScreenShotsMaker
{
    internal class ScreenShotMakerFactory : IScreenShotMakerFactory
    {
        private readonly IKernel _kernel;

        public ScreenShotMakerFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IScreenShotMaker Create()
        {
            return _kernel.Get<IScreenShotMaker>();
        }
    }
}