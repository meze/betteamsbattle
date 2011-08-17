using BetTeamsBattle.Frontend.Services.Interfaces;

namespace BetTeamsBattle.Frontend.Services
{
    internal class FractionToPercentsConverter : IFractionToPercentsConverter
    {
        public double GetPercents(double fraction)
        {
            return fraction * 100;
        }
    }
}