using BetTeamsBattle.ScreenShotsMaker.DI;
using BetTeamsBattle.ScreenShotsMaker.ScreenShotMakingManager.Interfaces;
using NUnit.Framework;
using Ninject;

namespace ScreenshotsMaker.Tests
{
    [TestFixture]
    internal class ScreenshotsMakerWinFormsNinjectKernelTests
    {
        private IScreenshotsMakingManager _screenshotsMakingManager;

        [SetUp]
        public void Setup()
        {
            var kernel = ScreenshotsMakerNinjectKernel.CreateKernel();
            _screenshotsMakingManager = kernel.Get<IScreenshotsMakingManager>();
        }

        [Test]
        public void Ctor_IsResolved()
        {
            //implicit test in setup
        }
    }
}