using BetTeamsBattle.Screenshots.BettScreenshotsManager.Interfaces;
using BetTeamsBattle.Screenshots.Gui.DI;
using NUnit.Framework;
using Ninject;

namespace BetTeamsBattle.Gui.Tests.DI
{
    [TestFixture]
    internal class ScreenshotsMakerWinFormsNinjectKernelTests
    {
        private IScreenshotsMakingManager _screenshotsMakingManager;

        [SetUp]
        public void Setup()
        {
            var kernel = ScreenshotsGuiNinjectKernel.CreateKernel();
            _screenshotsMakingManager = kernel.Get<IScreenshotsMakingManager>();
        }

        [Test]
        public void Ctor_IsResolved()
        {
            //implicit test in setup
        }
    }
}