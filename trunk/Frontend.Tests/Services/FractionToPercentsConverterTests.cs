using BetTeamsBattle.Frontend.Services;
using BetTeamsBattle.Frontend.Services.Interfaces;
using NUnit.Framework;

namespace BetTeamsBattle.Frontend.Tests.Services
{
    [TestFixture]
    internal class FractionToPercentsConverterTests
    {
        private IFractionToPercentsConverter _fractionToPercentsConverter;

        [SetUp]
        public void Setup()
        {
            _fractionToPercentsConverter = new FractionToPercentsConverter();
        }

        [Test]
        public void GetPercents_0dot01()
        {
            var fraction = 0.01;

            var result = _fractionToPercentsConverter.GetPercents(fraction);

            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetPercents_0dot1()
        {
            var fraction = 0.1;

            var result = _fractionToPercentsConverter.GetPercents(fraction);

            Assert.AreEqual(10, result);
        }

        [Test]
        public void GetPercents_1()
        {
            var fraction = 1;

            var result = _fractionToPercentsConverter.GetPercents(fraction);

            Assert.AreEqual(100, result);
        }
    }
}