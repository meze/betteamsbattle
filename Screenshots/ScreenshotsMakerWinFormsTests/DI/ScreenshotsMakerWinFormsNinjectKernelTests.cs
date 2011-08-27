using BetTeamsBattle.BettScreenshotsManager.ScreenshotMakingManager.Interfaces;
using BetTeamsBattle.ScreenshotsMakerWinForms.DI;
using NUnit.Framework;
using Ninject;

namespace BetTeamsBattle.ScreenshotsMakerWinFormsTests.DI
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