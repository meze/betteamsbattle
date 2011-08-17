using BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Localizers.InDays;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Localizers.InDays.Interfaces;
using NUnit.Framework;

namespace Frontend.Tests.Areas.NotAdmin.Localizers.InDays
{
    [TestFixture]
    internal class InDaysEnglishLocalizerTests
    {
        private IInDaysLocalizer _inDaysLocalizer;

        [SetUp]
        public void Setup()
        {
            _inDaysLocalizer = new InDaysEnglishLocalizer();
        }

        [Test]
        public void Localize_0Days()
        {
            var days = 0;

            var result = _inDaysLocalizer.Localize(days);

            Assert.AreEqual("in 0 days", result);
        }

        [Test]
        public void Localize_1Day()
        {
            var days = 1;

            var result = _inDaysLocalizer.Localize(days);

            Assert.AreEqual("in 1 day", result);
        }

        [Test]
        public void Localize_10Days()
        {
            var days = 10;

            var result = _inDaysLocalizer.Localize(days);

            Assert.AreEqual("in 10 days", result);
        }
    }
}