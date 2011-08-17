using BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Localizers.InDays;
using BetTeamsBattle.Frontend.Areas.NotAdmin.Localization.Localizers.InDays.Interfaces;
using NUnit.Framework;

namespace Frontend.Tests.Areas.NotAdmin.Localizers.InDays
{
    [TestFixture]
    internal class InDaysRussianLocalizerTests
    {
        private IInDaysLocalizer _inDaysLocalizer;

        [SetUp]
        public void Setup()
        {
            _inDaysLocalizer = new InDaysRussianLocalizer();
        }

        [Test]
        public void Localize_0Days()
        {
            var days = 0;

            var result = _inDaysLocalizer.Localize(days);

            Assert.AreEqual("через 0 дней", result);
        }

        [Test]
        public void Localize_1Day()
        {
            var days = 1;

            var result = _inDaysLocalizer.Localize(days);

            Assert.AreEqual("через 1 день", result);
        }

        [Test]
        public void Localize_2Days()
        {
            var days = 2;

            var result = _inDaysLocalizer.Localize(days);

            Assert.AreEqual("через 2 дня", result);
        }

        [Test]
        public void Localize_4Days()
        {
            var days = 4;

            var result = _inDaysLocalizer.Localize(days);

            Assert.AreEqual("через 4 дня", result);
        }

        [Test]
        public void Localize_10Days()
        {
            var days = 10;

            var result = _inDaysLocalizer.Localize(days);

            Assert.AreEqual("через 10 дней", result);
        }
    }
}