#region

using System;
using BetTeamsBattle.Frontend.Localization.Infrastructure.IP.Interfaces;

#endregion

namespace BetTeamsBattle.Frontend.Localization.Infrastructure.IP
{
    public class IpToNumberConverter : IIpToNumberConverter
    {
        public long IpToNumber(string ipAddress)
        {
            var ipTokens = ipAddress.Split('.');
            if (ipTokens.Length != 4) throw new ArgumentException(String.Format("Invalid ip address string: {0}", ipAddress), "ipAddress");

            byte [] ip = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                if (!byte.TryParse(ipTokens[i], out ip[i])) throw new ArgumentException("Invalid ip address string");
            }

            long ipNumber = 16777216*ip[0] + 65536*ip[1] + 256*ip[2] + ip[3];

            return ipNumber;
        }
    }
}