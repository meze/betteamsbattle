namespace BetTeamsBattle.Frontend.Localization.Infrastructure.IP.Interfaces
{
    public interface IIpToNumberConverter
    {
        long IpToNumber(string ipAddress);
    }
}